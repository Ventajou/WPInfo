using System.Drawing;

namespace Ventajou.WPInfo
{
    public class SerializableColor
    {
        public int A;
        public int R;
        public int G;
        public int B;

        public SerializableColor(Color color)
        {
            this.A = color.A;
            this.R = color.R;
            this.G = color.G;
            this.B = color.B;
        }

        public SerializableColor()
        { }

        public static SerializableColor FromColor(Color color)
        {
            return new SerializableColor(color);
        }

        public Color ToColor()
        {
            return Color.FromArgb(A, R, G, B);
        }
    }
}
