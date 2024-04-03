using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net;
using System.Text;

namespace miniPaent
{
    internal class FigRect : Figura
    {
        /// <summary>
        /// первая точка (точка, где мы зажимаем мышку изначально)
        /// </summary>
        public Point StartPoint { set; get; }

        /// <summary>
        /// вторая точка (точка, которую мы постоянно передвигаем)
        /// </summary>
        public Point EndPoint { set; get; }

        public FigRect() { }

        public FigRect(float width, Color color, Point startPoint, Point endPoint) : base(width, color)
        {
            StartPoint = startPoint;
            EndPoint = endPoint;
        }

        public override void PointAction(Point endPoint)
        {
            EndPoint = endPoint;
        }

        public override void Draw(Graphics gr)
        {
            Rectangle rect = new Rectangle(
            new Point(Math.Min(StartPoint.X, EndPoint.X), Math.Min(StartPoint.Y, EndPoint.Y)),
            new Size(Math.Max(StartPoint.X, EndPoint.X) - Math.Min(StartPoint.X, EndPoint.X), Math.Max(StartPoint.Y, EndPoint.Y) - Math.Min(StartPoint.Y, EndPoint.Y)));

            gr.DrawRectangle(new Pen(Color, Width), rect);
        }
    }
}
