using System;
using System.Drawing;

namespace QuickPlot
{
    public class ColorScaler
    {
        // Private objects
        private double minValue;
        private double maxValue;

        // Public objects
        //reserved

        // Class constructor
        public ColorScaler(double min, double max)
        {
            minValue = min;
            maxValue = max;
        }

        // Method used to convert an input double in range [_minValue, _maxValue]
        // into corresponding rainbow-ordered Color
        public Color GetScaledColor(double input)
        {
            double hue = 0.667d * (maxValue - input) / (1.0d * (maxValue - minValue));
            return ColorScaler.HSL2RGB(hue, 1.0d, 0.5d);
        }

        // Static method to convert an HSL input to RGB output
        public static Color HSL2RGB(double h, double s, double l)
        {
            double r = 1.0d;
            double g = 1.0d;
            double b = 1.0d;
            double v = (l <= 0.5d) ? (l * (1.0d + s)) : (l + s - l * s);

            if (v > 0.0d)
            {
                double m;
                double sv;
                int sextant;
                double fract, vsf, mid1, mid2;

                m = 2 * l - v;
                sv = (v - m) / v;
                h *= 6.0d;
                sextant = (int)h;
                fract = h - sextant;
                vsf = v * sv * fract;
                mid1 = m + vsf;
                mid2 = v - vsf;
                switch (sextant)
                {
                    case 0:
                        r = v;
                        g = mid1;
                        b = m;
                        break;
                    case 1:
                        r = mid2;
                        g = v;
                        b = m;
                        break;
                    case 2:
                        r = m;
                        g = v;
                        b = mid1;
                        break;
                    case 3:
                        r = m;
                        g = mid2;
                        b = v;
                        break;
                    case 4:
                        r = mid1;
                        g = m;
                        b = v;
                        break;
                    case 5:
                        r = v;
                        g = m;
                        b = mid2;
                        break;
                }
            }

            return Color.FromArgb((int)(255.0d * r), (int)(255.0d * g), (int)(255.0d * b));
        }
    }
}