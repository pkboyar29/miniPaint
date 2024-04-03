using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Text;

namespace miniPaent
{
    internal class FigLine : Figura
    {
        /// <summary>
        /// первая точка (точка, где мы зажимаем мышку изначально)
        /// </summary>
        public Point StartPoint { set; get; }

        /// <summary>
        /// вторая точка (точка, которую мы постоянно передвигаем)
        /// </summary>
        public Point EndPoint { set; get; }

        public FigLine() { }

        public FigLine(float width, Color color, Point startPoint, Point endPoint) : base(width, color)
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
            gr.DrawLine(new Pen(Color, Width), StartPoint, EndPoint);
        }
    }
}
