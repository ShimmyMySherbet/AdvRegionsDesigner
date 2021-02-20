using SkiaSharp;

namespace AdvRegionsDesigner.Models.Cursors
{
    public class CrossCursor : ICursor
    {
        public static readonly SKPoint[] CursorProfile = new SKPoint[] { new SKPoint(-5, 0), new SKPoint(5, 0), new SKPoint(0, -5), new SKPoint(0, 5) };
        public SKPaint CursorCrossPaint = RenderClient.CreatePaint(SKColors.Red);

        public void Draw(SKCanvas canvas, SKPoint cursorOrigin, float zoom, SKPaint requestedPaint = null)
        {
            if (requestedPaint == default)
            {
                requestedPaint = CursorCrossPaint;
            }
            canvas.DrawLine(cursorOrigin + CursorProfile[0], cursorOrigin + CursorProfile[1], requestedPaint);
            canvas.DrawLine(cursorOrigin + CursorProfile[2], cursorOrigin + CursorProfile[3], requestedPaint);
        }
    }
}