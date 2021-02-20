using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using AdvRegionsDesigner.Models;
using AdvRegionsDesigner.Models.Cursors;
using AdvRegionsDesigner.Models.UI;
using RFile;
using RFile.Models;
using SkiaSharp;

namespace AdvRegionsDesigner
{
    public partial class MainWindow : Form
    {
        public RenderClient r = new RenderClient();

        public TaskFactory TaskFactory = new TaskFactory();

        public bool AlwaysRender { get; set; } = true;

        public bool RequiresRender { get; set; } = false;

        public bool RunRender { get; set; } = true;

        public int TargetFPS { get; set; } = 60;

        public bool NoFPSCap { get; set; } = false;

        public Point LastClientPoint { get; set; } = new Point(0, 0);

        public EEditMode EditMode { get; set; } = EEditMode.SELECT;

        public Region SelectedRegion { get; set; } = null;

        public ICursor SelectCursor = new SelectCursor();
        public ICursor EditRegionCursor = new CrossCursor();

        public int SelectedPointIndex = 0;

        public MainWindow()
        {
            InitializeComponent();
            r.OutControl = pbMain;
            r.LoadBaseMap(new FileStream(@"Map.png", FileMode.Open));
            pbMain.SizeChanged += PbMain_SizeChanged;
            new Thread(async () => await RenderLoop()).Start();
            FormClosed += (a, b) => RunRender = false;
            Shown += Form1_Shown;

            pbMain.MouseEnter += PbMain_MouseEnter;
            pbMain.MouseLeave += PbMain_MouseLeave;
            CheckForIllegalCrossThreadCalls = false;
            pbMain.MouseWheel += PbMain_MouseWheel;

            pbMain.MouseDown += PbMain_MouseDown;
            pbMain.MouseUp += PbMain_MouseUp;
            pbMain.MouseMove += PbMain_MouseMove;

            Button acc = new Button();
            AcceptButton = acc;
            acc.Click += Acc_Click;
        }

        private void PbMain_MouseMove(object sender, MouseEventArgs e)
        {
            if (EditMode == EEditMode.EDIT_POINT)
            {
                SKPoint pt = PixelDedicator.GetPixelPoint(pbMain, Cursor.Position);
                pt = r.LocalisePixelPoint(pt);
                RunEditPoint(pt);
                r.SetRegionSelected(SelectedRegion);
            }
        }

        private void PbMain_MouseUp(object sender, MouseEventArgs e)
        {
            if (EditMode == EEditMode.EDIT_POINT)
            {
                EditMode = SelectedRegion != null ? EEditMode.CREATE_REGION : EEditMode.SELECT;
            }
        }

        private void PbMain_MouseDown(object sender, MouseEventArgs e)
        {
            if (SelectedRegion != null && EditMode == EEditMode.CREATE_REGION)
            {
                SKPoint pt = PixelDedicator.GetPixelPoint(pbMain, Cursor.Position);
                pt = r.LocalisePixelPoint(pt);

                foreach (var pts in SelectedRegion.Points)
                {
                    float distance = SKPoint.Distance(pt, pts);
                    if (distance * r.ZoomLevel <= 5)
                    {
                        SelectedPointIndex = SelectedRegion.Points.IndexOf(pts);
                        EditMode = EEditMode.EDIT_POINT;
                    }
                }
            }
        }

        private void Acc_Click(object sender, EventArgs e)
        {
            Focus();
        }

        private void PbMain_MouseWheel(object sender, MouseEventArgs e)
        {
            var Res = OpenTK.Input.Keyboard.GetState();
            if (Res.IsKeyDown(OpenTK.Input.Key.LControl))
            {
                if (e.Delta > 0)
                {
                    r.ZoomLevel += 0.1f;
                }
                else
                {
                    r.ZoomLevel -= 0.1f;
                }
                int tabZoomValue = (int)(r.ZoomLevel / 0.1);
                if (tabZoomValue >= tbZoom.Minimum && tabZoomValue <= tbZoom.Maximum)
                {
                    tbZoom.Value = tabZoomValue;
                }
                Render();
            }
            else if (Res.IsKeyDown(OpenTK.Input.Key.ShiftLeft))
            {
                r.ZoomX -= e.Delta / r.ZoomLevel;
            }
            else
            {
                r.ZoomY -= e.Delta / r.ZoomLevel;
            }
        }

        private void PbMain_MouseLeave(object sender, EventArgs e)
        {
            Cursor.Show();
        }

        private void PbMain_MouseEnter(object sender, EventArgs e)
        {
            Cursor.Hide();
        }

        private async void Form1_Shown(object sender, EventArgs e)
        {
            await RenderWatcher();
        }

        private void PbMain_SizeChanged(object sender, EventArgs e)
        {
            pbMain.Width = pbMain.Height;
        }

        private void SkMain_Click(object sender, EventArgs e)
        {
        }

        private void pbMain_Click(object sender, EventArgs e)
        {
            SKPoint pt = PixelDedicator.GetPixelPoint(pbMain, Cursor.Position);
            pt = r.LocalisePixelPoint(pt);

            if (EditMode == EEditMode.CREATE_REGION)
            {
                RunRegionEdit(pt);
            }
            else if (EditMode == EEditMode.SELECT)
            {
                RunSelect(pt);
            }
            Render();
        }

        private void RunRegionEdit(SKPoint pixel)
        {
            if (SelectedRegion != null)
            {
                SelectedRegion.Points.Add(pixel);
                r.SetRegionSelected(SelectedRegion);

                foreach (var ct in flowRegions.Controls.OfType<RegionControl>().Where(x => x.CRegion == SelectedRegion))
                {
                    ct.SendPointsChanged();
                }
            }
        }

        private void RunEditPoint(SKPoint pixel)
        {
            if (SelectedRegion != null && SelectedPointIndex > 0 && SelectedPointIndex <= SelectedRegion.Points.Count - 1)
            {
                SelectedRegion.Points[SelectedPointIndex] = pixel;
                r.SetRegionSelected(SelectedRegion);
            }
        }

        private void RunSelect(SKPoint pixel)
        {
            Region selectRegion = null;
            lock (r.Regions)
            {
                foreach (Region region in r.Regions)
                {
                    if (PolygonalCollider.IsInPolygon(region.Points, pixel))
                    {
                        selectRegion = region;
                        break;
                    }
                }

                if (selectRegion != null)
                {
                    r.SetRegionSelected(selectRegion);
                    foreach (RegionControl ct in flowRegions.Controls.OfType<RegionControl>())
                    {
                        ct.SendSelected(ct.CRegion == selectRegion);
                        if (ct.CRegion == selectRegion)
                        {
                            flowRegions.ScrollControlIntoView(ct);
                        }
                    }
                }
                else
                {
                    foreach (RegionControl ct in flowRegions.Controls.OfType<RegionControl>())
                    {
                        ct.SendSelected(false);
                    }
                }
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                EditMode = EEditMode.SELECT;
                lock (r.Cursor)
                {
                    r.Cursor = SelectCursor;
                }
                foreach (RegionControl ct in flowRegions.Controls.OfType<RegionControl>())
                {
                    ct.SendSelected(false);
                }
                r.SetRegionSelected(null);
                return true;
            }
            else if (keyData == Keys.Enter)
            {
                pbMain.Focus();
            }

            return false;
        }

        public void Render()
        {
            RequiresRender = true;
        }

        private int FramesCount = 0;
        private List<int> FrameMS = new List<int>();

        public async Task RenderLoop()
        {
            Stopwatch w = new Stopwatch();

            while (RunRender)
            {
                if (RequiresRender || AlwaysRender)
                {
                    RequiresRender = false;
                    w.Restart();

                    Point location = pbMain.Location;
                    SKPoint rp = PixelDedicator.GetPixelPoint(pbMain.Location, PointToClient(Cursor.Position));
                    Image newImage = r.Render(AlwaysRender ? rp : default);

                    await TaskFactory.StartNew(() =>
                    {
                        pbMain.Image?.Dispose();
                        pbMain.Image = newImage;
                    });
                    FramesCount++;

                    w.Stop();
                    lock (FrameMS)
                    {
                        FrameMS.Add((int)w.ElapsedMilliseconds);
                    }
                }
                await Task.Delay(NoFPSCap ? 1 : GetWait(TargetFPS, (int)w.ElapsedMilliseconds));
            }
        }

        private int GetWait(int TFPS, int ENL)
        {
            int FPSW = (1000 / TFPS) - (ENL);
            return FPSW < 0 ? 0 : FPSW;
        }

        private int _LastFPS = 0;

        public int FPS
        {
            get => _LastFPS;
            set
            {
                _LastFPS = value;
                lblFPS.Text = "FPS: " + value;
            }
        }

        private async Task RenderWatcher()
        {
            int upd = 0;
            while (RunRender)
            {
                await Task.Delay(1000);
                int framesLastSecond = FramesCount;
                FramesCount = 0;
                FPS = framesLastSecond;
                upd++;
                if (upd % 10 == 0)
                {
                    double avg = 0;
                    lock (FrameMS)
                    {
                        if (FrameMS.Count != 0)
                        {
                            avg = FrameMS.Average();
                            FrameMS.Clear();
                        }
                    }
                    Console.WriteLine($"Average render time over last 10sec: {avg}ms");
                }
            }
        }

        private void cbAlwaysRender_CheckedChanged(object sender, EventArgs e)
        {
            AlwaysRender = cbAlwaysRender.Checked;
        }

        private void tbZoom_Scroll(object sender, EventArgs e)
        {
            r.ZoomLevel = (float)(tbZoom.Value * 0.1);
            Render();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            r.Regions.Add(new AdvRegionsDesigner.Region());
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void btnAddRegion_Click(object sender, EventArgs e)
        {
            int rc = 0;
            lock (r.Regions)
            {
                if (r.Regions.Count > 0)
                    rc = r.Regions.Count(x => x.Name.ToLower().StartsWith("new region"));
            }

            string name = $"New Region {(rc == 0 ? "" : rc.ToString())}";

            AdvRegionsDesigner.Region.SelectRandomColor(out SKPaint selected, out SKPaint paint, out Color color);

            Region NR = new Region() { Name = name, SelectedPaint = selected, Paint = paint };
            SelectedRegion = NR;

            foreach (RegionControl c in flowRegions.Controls.OfType<RegionControl>())
            {
                c.SendSelected(false);
            }

            RunCreateRegionControl(NR, color);

            lock (r.Regions)
            {
                r.Regions.Add(NR);
            }
            lock (r.Cursor)
            {
                r.Cursor = EditRegionCursor;
            }
            r.SetRegionSelected(NR);
            EditMode = EEditMode.CREATE_REGION;
        }

        public RegionControl RunCreateRegionControl(Region NR, Color color)
        {
            RegionControl regionControl = new RegionControl(NR);
            flowRegions.Controls.Add(regionControl);
            regionControl.RegionSelected += RegionControl_RegionSelected;
            regionControl.ActionRequested += RegionControl_ActionRequested;

            regionControl.SelectName();
            regionControl.SendSelected(true);
            regionControl.SendColor(color);
            return regionControl;
        }

        private void RegionControl_ActionRequested(RegionControl control, int action)
        {
            lock (r.Regions)
            {
                if (action == 0)
                {
                    SelectedRegion = control.CRegion;
                    EditMode = EEditMode.CREATE_REGION;
                    RegionControl_RegionSelected(control);
                    lock (r.Cursor)
                        r.Cursor = EditRegionCursor;
                }
                if (action == 1)
                {
                    control.CRegion.Points = new List<SKPoint>();
                    RegionControl_RegionSelected(control);
                    control.SendPointsChanged();
                }
                else if (action == 2)
                {
                    r.Regions.Remove(control.CRegion);
                    flowRegions.Controls.Remove(control);
                    RegionControl_RegionSelected(null);
                    control.CRegion.Paint.Dispose();
                    control.CRegion.SelectedPaint.Dispose();
                    control.Dispose();
                }
            }
        }

        private void RegionControl_RegionSelected(RegionControl control)
        {
            foreach (RegionControl c in flowRegions.Controls.OfType<RegionControl>())
            {
                c.SendSelected(false);
            }
            control?.SendSelected(true);
            r.SetRegionSelected(control?.CRegion);
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            lock (r)
            {
                using (MemoryStream stream = new MemoryStream())
                using (RFileWriter writer = new RFileWriter(stream))
                {
                    writer.WriteMapContext(r.BaseMapheight, r.BaseMapWidth);
                    lock (r.Regions)
                    {
                        foreach (var region in r.Regions)
                        {
                            writer.WriteRegionContext(region.Name);
                            foreach (var point in region.Points)
                            {
                                writer.WriteRegionPoint(point.X, point.Y);
                            }
                        }
                    }
                    writer.Flush();
                    string dat = Encoding.UTF8.GetString(stream.ToArray());
                    new ExportWindow(dat).ShowDialog();
                }
            }
        }

        private void btnLoadFrom_Click(object sender, EventArgs e)
        {
            OpenFileDialog OFD = new OpenFileDialog();
            if (OFD.ShowDialog() == DialogResult.OK)
            {
                RFileReader reader = new RFileReader();

                using (FileStream stream = new FileStream(OFD.FileName, FileMode.Open))
                {
                    if (reader.Load(stream, out List<FRegion> regions, out _))
                    {
                        lock (r.Regions)
                        {
                            foreach (var region in regions)
                            {
                                var newRegion = new Region() { Name = region.Name, Points = region.Positions.Select(x => new SKPoint(x.Item1, x.Item2)).ToList() }.WithRandomColor(out Color disp);
                                r.Regions.Add(newRegion);
                                RunCreateRegionControl(newRegion, disp);
                            }
                        }

                        Console.WriteLine($"LOADED {regions.Count} REGIONS!");
                    }
                    else
                    {
                        Console.WriteLine($"Failed to load file!. Failure at character: {stream.Position}");
                    }
                }
            }
        }
    }
}