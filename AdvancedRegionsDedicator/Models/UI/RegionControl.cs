using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AdvancedRegionsDedicator;
using SkiaSharp;

namespace AdvRegionsDesigner.Models.UI
{
    public partial class RegionControl : UserControl
    {
        public Region CRegion;

        public static ColorDialog SharedColorDialog;

        public delegate void RegionSelectedArgs(RegionControl control);
        public event RegionSelectedArgs RegionSelected;

        public delegate void ActionRequestedArgs(RegionControl control, int action);
        public event ActionRequestedArgs ActionRequested;

        public RegionControl()
        {
            InitializeComponent();
            DoubleClick += RegionControl_DoubleClick;
        }

        public RegionControl(Region region)
        {
            InitializeComponent();
            DoubleClick += RegionControl_DoubleClick;
            CRegion = region;
            txtRegionName.Text = CRegion.Name;
            region.VisibilityUpdated += Region_VisibilityUpdated;
        }

        private void Region_VisibilityUpdated(Region region)
        {

            if (region.Visible)
            {
                if (region.OnScreen)
                {
                    pbVisibility.Image = RenderAssets.Visible;
                } else
                {
                    pbVisibility.Image = RenderAssets.Hidden;
                }



            } else
            {
                if (region.OnScreen)
                {
                    pbVisibility.Image = RenderAssets.Invisible;


                } else
                {
                    pbVisibility.Image = RenderAssets.HiddenInvisible;
                }
            }
        }

        public void SendColor(Color color) => pbColor.BackColor = color;

        public void SendPointsChanged()
        {
            lblPoints.Text = $"Points: {CRegion.Points.Count}";
        }

        public void SelectName()
        {
            txtRegionName.Focus();
        }

        private void RegionControl_DoubleClick(object sender, EventArgs e)
        {
            RegionSelected?.Invoke(this);
        }

        public void SendSelected(bool selected)
        {
            if (selected)
            {
                BackColor = SystemColors.ActiveCaption;
            } else
            {
                BackColor = SystemColors.Control;
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            ActionRequested?.Invoke(this, 0);
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            ActionRequested?.Invoke(this, 1);

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            ActionRequested?.Invoke(this, 2);
        }

        private void txtRegionName_TextChanged(object sender, EventArgs e)
        {
            lock(CRegion)
            {
                CRegion.Name = txtRegionName.Text;
            }
        }

        private void resetColorToolStripMenuItem_Click(object sender, EventArgs e)
        {

            AdvRegionsDesigner.Region.SelectRandomColor(out SKPaint a, out var c, out var cc);
            lock(CRegion)
            {
                CRegion.Paint.Dispose();
                CRegion.SelectedPaint.Dispose();

                CRegion.Paint = c;
                CRegion.SelectedPaint = a;
                SendColor(cc);
            }


        }

        private void pbColor_Click(object sender, EventArgs e)
        {

            if (SharedColorDialog == null)
                SharedColorDialog = new ColorDialog();

                if (SharedColorDialog.ShowDialog() == DialogResult.OK)
                {
                    SKColor NC = new SKColor(SharedColorDialog.Color.R, SharedColorDialog.Color.G, SharedColorDialog.Color.B);

                    pbColor.BackColor = SharedColorDialog.Color;

                    lock(CRegion)
                    {

                        CRegion.Paint.Dispose();
                        CRegion.SelectedPaint.Dispose();

                        CRegion.Paint = RenderClient.CreatePaint(NC.WithAlpha(50), true);
                        CRegion.SelectedPaint = RenderClient.CreatePaint(NC.WithAlpha(100), true);

                    }


                }

        }

        private void pbVisibility_Click(object sender, EventArgs e)
        {
            CRegion.Visible = !CRegion.Visible;
            Region_VisibilityUpdated(CRegion);
        }
    }
}
