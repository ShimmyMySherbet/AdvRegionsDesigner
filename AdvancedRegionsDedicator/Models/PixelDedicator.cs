using System.Drawing;
using System.Windows.Forms;
using SkiaSharp;

namespace AdvRegionsDesigner
{
    public static class PixelDedicator
    {
        public static SKPoint GetPixelPoint(Control ct, Point CursorPos)
        {
            Form parent = ct.FindForm();

            Point cl = parent.PointToClient(CursorPos);

            Point n = new Point(cl.X - ct.Location.X, cl.Y - ct.Location.Y);

            return new SKPoint(n.X, n.Y);
        }
        public static SKPoint GetPixelPoint(Form parent, Point location, Point CursorPos)
        {
            Point cl = ThreadSavePointToClient(CursorPos, parent);
            Point n = new Point(cl.X - location.X, cl.Y - location.Y);

            return new SKPoint(n.X, n.Y);
        }

        public static SKPoint GetPixelPoint(Point location, Point ClientPos)
        {
            Point n = new Point(ClientPos.X - location.X, ClientPos.Y - location.Y);
            return new SKPoint(n.X, n.Y);
        }


        public static Point ThreadSavePointToClient(Point point, Control client)
        {
            Point cl = new Point(point.X - client.Location.X, point.Y - client.Location.Y);
            return cl;
        }
    }
}