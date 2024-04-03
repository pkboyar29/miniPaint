using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace miniPaent
{
    internal class FigBrush : Figura
    {
        public Stack<Point> Points { set; get; }
        public FigBrush() { }

        public FigBrush(float width, Color color, Stack<Point> points) : base(width, color)
        {
            this.Points = points;
        }
        public override void Draw(Graphics gr)
        {
            if (Points.Count < 2) return;
            gr.DrawLines(new Pen(Color, Width), Points.ToArray());
        }

        public override void PointAction(Point point)
        {
            Points.Push(point);
        }
    }
}
