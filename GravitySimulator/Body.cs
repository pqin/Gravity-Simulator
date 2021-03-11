using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GravitySimulator
{
    class Body
    {
        public Body(Color color, float mass, bool moveable)
        {
            Color = color;
            Mass = mass;
            Alive = true;
            Moveable = moveable;
        }

        public bool Moveable
        {
            get;
            private set;
        }

        public Color Color
        {
            get;
            private set;
        }

        public float Mass
        {
            get;
            set;
        }

        public bool Alive
        {
            get; set;
        }

        public float Radius
        {
            get
            {
                return Math.Max(1.0f, (float)Math.Log10(Mass));
            }
        }

        public float Diameter
        {
            get
            {
                return Radius * 2.0f;
            }
        }

        public PointF Position
        {
            get; set;
        }

        public PointF Velocity
        {
            get; set;
        }

        public float Momentum
        {
            get
            {
                return (float)(Mass * Math.Sqrt((Velocity.X * Velocity.X) + (Velocity.Y * Velocity.Y)));
            }
        }

        public void Update()
        {
            if (Moveable)
            {
                float x = Position.X + Velocity.X;
                float y = Position.Y + Velocity.Y;

                Position = new PointF(x, y);
            }
        }

        public void Render(Graphics graphics)
        {
            Brush brush = new SolidBrush(Moveable ? Color.White : Color.Yellow);
            graphics.FillEllipse(brush, Position.X - Radius, Position.Y - Radius, Diameter, Diameter);
        }
    }
}
