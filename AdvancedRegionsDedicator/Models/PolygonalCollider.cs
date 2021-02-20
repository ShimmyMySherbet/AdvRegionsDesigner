using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkiaSharp;

namespace AdvRegionsDesigner.Models
{
    public class PolygonalCollider
    {
        public static bool IsInPolygon(List<SKPoint> poly, SKPoint p)
        {
            SKPoint p1, p2;
            bool inside = false;

            if (poly.Count < 3)
            {
                return inside;
            }

            var oldPoint = new SKPoint(
                poly[poly.Count - 1].X, poly[poly.Count - 1].Y);

            for (int i = 0; i < poly.Count; i++)
            {
                var newPoint = new SKPoint(poly[i].X, poly[i].Y);

                if (newPoint.X > oldPoint.X)
                {
                    p1 = oldPoint;
                    p2 = newPoint;
                }
                else
                {
                    p1 = newPoint;
                    p2 = oldPoint;
                }

                if ((newPoint.X < p.X) == (p.X <= oldPoint.X)
                    && (p.Y - (long)p1.Y) * (p2.X - p1.X)
                    < (p2.Y - (long)p1.Y) * (p.X - p1.X))
                {
                    inside = !inside;
                }

                oldPoint = newPoint;
            }

            return inside;
        }
    }
}
