using SkiaSharp;

namespace AdvRegionsDesigner.Models
{
    public interface ICursor
    {
        void Draw(SKCanvas canvas, SKPoint cursorOrigin, float zoom, SKPaint requestedPaint = default);
    }
}