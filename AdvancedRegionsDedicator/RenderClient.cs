using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using AdvRegionsDesigner.Models;
using AdvRegionsDesigner.Models.Cursors;
using SkiaSharp;
using SkiaSharp.Views.Desktop;

namespace AdvRegionsDesigner
{
    public class RenderClient
    {
        public Control OutControl;

        public float ZoomLevel = 1;
        public float ZoomX = 0;
        public float ZoomY = 0;

        public List<Region> Regions = new List<Region>();

        public List<SKPoint> Highlights = new List<SKPoint>();
        public List<SKPoint> TempHighlights = new List<SKPoint>();

        public SKPaint Highlightpaint = CreatePaint(SKColors.Red.WithAlpha(100));

        public SKBitmap BaseMap = null;

        public ICursor Cursor = new SelectCursor();

        public int BaseMapWidth => BaseMap.Width;
        public int BaseMapheight => BaseMap.Height;

        public void LoadBaseMap(Stream stream) => BaseMap = SKBitmap.Decode(stream);

        public SKPaint CursorColor = CreatePaint(SKColors.Blue.WithAlpha(200), false);
        public bool UseCustomCursorColor { get; set; } = false;

        public static SKPaint CreatePaint(SKColor color, bool fill = false)
        {
            SKPaint p = new SKPaint();
            if (fill)
                p.Style = SKPaintStyle.StrokeAndFill;
            p.SetColor(color, SKColorSpace.CreateSrgb());
            return p;
        }

        public SKSize MaxSizeClippingOperation(SKSize Outer, SKSize Inner)
        {
            float WidthRatio = Inner.Width / Outer.Width;
            float HeightRatio = Inner.Height / Outer.Height;
            if (WidthRatio > HeightRatio)
            {
                float SourceBaseWith = Outer.Width;
                float CalcRatio = SourceBaseWith / Inner.Width;
                float CalcHeight = CalcRatio * Inner.Height;
                return new SKSize(SourceBaseWith, CalcHeight);
            }
            else
            {
                float SourceBaseHeight = Outer.Height;
                float CalcRatio = SourceBaseHeight / Inner.Height;
                float CalcWidth = CalcRatio * Inner.Width;
                return new SKSize(CalcWidth, SourceBaseHeight);
            }
        }

        public void SetRegionSelected(Region region)
        {
            lock (Regions)
                lock (TempHighlights)
                {
                    Regions.ForEach(x => x.IsSelected = false);
                    TempHighlights.Clear();
                    if (region != null)
                    {
                        region.IsSelected = true;
                        TempHighlights = region.Points.ToList();
                    }
                }
        }

        public SKRect MaxRectangleClippingOperation(SKSize Outer, SKSize Inner)
        {
            float WidthRatio = Inner.Width / Outer.Width;
            float HeightRatio = Inner.Height / Outer.Height;
            if (WidthRatio > HeightRatio)
            {
                float SourceBaseWith = Outer.Width;
                float CalcRatio = SourceBaseWith / Inner.Width;
                float CalcHeight = CalcRatio * Inner.Height;
                float CalcSourceX = 0;
                float CalcSourceY = (Outer.Height - CalcHeight) / 2;
                var Rect = SKRect.Create(CalcSourceX, CalcSourceY, SourceBaseWith, CalcHeight);
                return Rect;
            }
            else
            {
                float SourceBaseHeight = Outer.Height;
                float CalcRatio = SourceBaseHeight / Inner.Height;
                float CalcWidth = CalcRatio * Inner.Width;
                float CalcSourceX = (Outer.Width - CalcWidth) / 2;
                float CalcSourceY = 0;
                var Rect = SKRect.Create(CalcSourceX, CalcSourceY, CalcWidth, SourceBaseHeight);
                return Rect;
            }
        }

        public System.Drawing.Image Render(SKPoint CursorPos = default)
        {
            lock (this)
                lock (BaseMap)
                {
                    using (SKBitmap BMP = new SKBitmap(new SKImageInfo(OutControl.Width, OutControl.Height)))
                    {
                        using (SKCanvas canvas = new SKCanvas(BMP))
                        {
                            float sourceX = ZoomX;
                            float sourceY = ZoomY;

                            float sourceWidth = OutControl.Width / ZoomLevel;
                            float sourceHeight = OutControl.Height / ZoomLevel;

                            SKRect sourceDestination = MaxRectangleClippingOperation(new SKSize(BMP.Width, BMP.Height), new SKSize(BaseMap.Width, BaseMap.Height));
                            SKRect source = SKRect.Create(sourceX, sourceY, sourceWidth, sourceHeight);

                            canvas.DrawBitmap(BaseMap, source, sourceDestination);
                            lock (Highlights)
                                lock (TempHighlights)
                                {
                                    foreach (SKPoint RPoint in Highlights.Concat(TempHighlights))
                                    {
                                        SKPoint local = LocalisePoint(RPoint, source, out bool onScreen);

                                        if (onScreen)
                                        {
                                            canvas.DrawCircle(local, 5, Highlightpaint);
                                        }
                                    }
                                }
                            lock (Regions)
                            {
                                foreach (Region Region in Regions)
                                {
                                    lock (Region)
                                    {
                                        if (Region.Points.Count > 2)
                                        {
                                            bool ShowRegion = false;

                                            List<SKPoint> LocalPoints = new List<SKPoint>();
                                            foreach (SKPoint pixelPoint in Region.Points)
                                            {
                                                LocalPoints.Add(LocalisePoint(pixelPoint, source, out bool onScreen));
                                                if (onScreen) ShowRegion = true;
                                            }

                                            if (ShowRegion && Region.Visible)
                                            {
                                                using (SKPath path = new SKPath())
                                                {
                                                    SKPoint origin = LocalPoints[0];
                                                    path.MoveTo(origin);
                                                    for (int i = 1; i < LocalPoints.Count; i++)
                                                    {
                                                        path.LineTo(LocalPoints[i]);
                                                    }
                                                    path.Close();
                                                    canvas.DrawPath(path, Region.IsSelected ? Region.SelectedPaint : Region.Paint);
                                                }
                                            }
                                            if (Region.OnScreen != ShowRegion)
                                            {
                                                Region.OnScreen = ShowRegion;
                                            }
                                        }
                                    }
                                }
                            }

                            if (CursorPos != default)
                            {
                                SKPaint p = UseCustomCursorColor ? CursorColor : default;

                                Cursor.Draw(canvas, CursorPos, ZoomLevel, p);
                            }
                        }

                        return BMP.ToBitmap();
                    }
                }
        }

        private SKPoint LocalisePoint(SKPoint pixel, SKRect source, Bitmap BMP, out bool onScreen)
        {
            SKPoint local = new SKPoint((pixel.X - source.Location.X) * ZoomLevel, (pixel.Y - source.Location.Y) * ZoomLevel);
            onScreen = local.X > 0 && local.X < BMP.Width && local.Y > 0 && local.Y < BMP.Height;
            return local;
        }

        private SKPoint LocalisePoint(SKPoint pixel, SKRect source, out bool onScreen)
        {
            lock (OutControl)
            {
                SKPoint local = new SKPoint((pixel.X - source.Location.X) * ZoomLevel, (pixel.Y - source.Location.Y) * ZoomLevel);
                onScreen = local.X > 0 && local.X < OutControl.Width && local.Y > 0 && local.Y < OutControl.Height;
                return local;
            }
        }

        public SKPoint LocalisePixelPoint(SKPoint pt)
        {
            lock (BaseMap)
                lock (OutControl)
                {
                    float sourceX = ZoomX;
                    float sourceY = ZoomY;

                    float sourceWidth = OutControl.Width / ZoomLevel;
                    float sourceHeight = OutControl.Height / ZoomLevel;

                    SKRect sourceDestination = MaxRectangleClippingOperation(new SKSize(OutControl.Width, OutControl.Height), new SKSize(BaseMap.Width, BaseMap.Height));
                    SKRect source = SKRect.Create(sourceX, sourceY, sourceWidth, sourceHeight);

                    double XPerc = pt.X / sourceDestination.Width;
                    double YPerc = pt.Y / sourceDestination.Height;
                    return new SKPoint((source.Width * (float)XPerc) + ZoomX, (source.Height * (float)YPerc) + ZoomY);
                }
        }
    }
}