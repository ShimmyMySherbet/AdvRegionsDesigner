using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkiaSharp;

namespace AdvRegionsDesigner.Models.Cursors
{
    public class SelectCursor : ICursor
    {
        public static readonly SKPaint DefaultColor = RenderClient.CreatePaint(SKColors.Blue, true);
        public static readonly SKPoint[] PointOffsets = new SKPoint[] { new SKPoint(-3, -4), new SKPoint(3, -4) };

        public void Draw(SKCanvas canvas, SKPoint cursorOrigin, float zoom, SKPaint requestedPaint = null)
        {
            if (requestedPaint == null)
            {
                requestedPaint = DefaultColor;
            }
            requestedPaint.StrokeWidth = 10;
            canvas.DrawLine(cursorOrigin, cursorOrigin + PointOffsets[0], requestedPaint);
            canvas.DrawLine(cursorOrigin, cursorOrigin + PointOffsets[1], requestedPaint);



        }
    }
}
