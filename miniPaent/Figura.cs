using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net;
using System.Text;

namespace miniPaent
{
    abstract class Figura
    {
        public float Width { get; set; }
        public Color Color { get; set; }

        public Figura() { }

        public Figura(float width, Color color)
        {
            Width = width;
            Color = color;
        }
        public abstract void Draw(Graphics gr);
        public virtual void PointAction(Point point) { }
    }
}
