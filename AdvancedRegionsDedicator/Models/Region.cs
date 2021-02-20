using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Threading.Tasks;
using SkiaSharp;

namespace AdvRegionsDesigner
{
    public class Region
    {
        public static readonly SKColor[] Paints = new SKColor[] { SKColors.Red, SKColors.Blue, SKColors.Green, SKColors.AliceBlue, SKColors.Aqua, SKColors.Azure, SKColors.BlueViolet, SKColors.Brown, SKColors.Crimson, SKColors.DarkRed, SKColors.DeepPink, SKColors.Khaki };

        public delegate void RegionEvent(Region region);
        public event RegionEvent VisibilityUpdated;

        public TaskFactory TaskFactory = new TaskFactory();

        private bool _Visible = true;
        public bool Visible
        {
            get => _Visible;
            set
            {
                _Visible = value;
                VisibilityUpdated?.Invoke(this);
            }
        }

        private bool _onScreen = false;
        public bool OnScreen
        {
            get => _onScreen;
            set
            {
                _onScreen = value;
                TaskFactory.StartNew(() => VisibilityUpdated?.Invoke(this));
            }
        }


        public static void SelectRandomColor(out SKPaint selected, out SKPaint paint, out Color color)
        {
            int r;
            using(RNGCryptoServiceProvider RNG = new RNGCryptoServiceProvider())
            {
                byte[] b = new byte[4];
                RNG.GetBytes(b);
                r = Math.Abs(BitConverter.ToInt32(b, 0));
            }
            Console.WriteLine($"Random Number: {r}");

            int index = Math.Abs(r % Paints.Length - 1);
            Console.WriteLine($"calc index: {index}");
            SKColor c = Paints[index];
            selected = RenderClient.CreatePaint(Paints[index].WithAlpha(100), true);
            paint = RenderClient.CreatePaint(Paints[index].WithAlpha(50), true);
            color = Color.FromArgb(c.Red, c.Green, c.Blue);

        }

        public Region WithRandomColor(out Color dispColor)
        {
            SelectRandomColor(out SelectedPaint, out Paint, out dispColor);
            return this;
        }


        public List<SKPoint> Points = new List<SKPoint>();
        public string Name;
        public Dictionary<string, string> Attributes = new Dictionary<string, string>();
        public bool IsSelected = false;

        public SKPaint SelectedPaint;
        public SKPaint Paint;
    }
}