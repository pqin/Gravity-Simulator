using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GravitySimulator
{
    public partial class Form1 : Form
    {
        private const float GRAVITY_CONSTANT = 0.2f;
        private const int UPDATE_INTERVAL = 1000 / 25;

        private Timer timer;
        private List<Body> bodies = new List<Body>();
        private List<Body> additions = new List<Body>();
        private bool mouseDown = false;
        private Point ptInitial = new Point();
        private Point ptFinal = new Point();
        private bool moveable = true;

        public Form1()
        {
            InitializeComponent();

            _btnClear.Click += _btnClear_Click;

            _panelCanvas.MouseDown += _panelCanvas_MouseDown;
            _panelCanvas.MouseUp += _panelCanvas_MouseUp;
            _panelCanvas.MouseMove += _panelCanvas_MouseMove;

            timer = new Timer();
            timer.Interval = UPDATE_INTERVAL;
            timer.Tick += Timer_Tick;
            timer.Enabled = true;
        }

        private void _btnClear_Click(object sender, EventArgs e)
        {
            bodies.Clear();
            additions.Clear();
        }

        private void _panelCanvas_MouseDown(object sender, MouseEventArgs e)
        {
            if (!mouseDown)
            {
                ptInitial = e.Location;
                ptFinal = e.Location;
            }
            mouseDown = true;
            moveable = (e.Button == MouseButtons.Left);
        }

        private void _panelCanvas_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;

            Body body = new Body(Color.White, (float)_numMass.Value, moveable);
            body.Position = ptInitial;
            body.Velocity = new Point(ptInitial.X - ptFinal.X, ptInitial.Y - ptFinal.Y);
            additions.Add(body);
        }

        private void _panelCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                float dx = e.Location.X - ptInitial.X;
                float dy = e.Location.Y - ptInitial.Y;
                double angle = Math.Atan2(dy, dx);
                double length = Math.Min(50.0f, Math.Sqrt((dx * dx) + (dy * dy)));
                int x = (int)(length * Math.Cos(angle)) + ptInitial.X;
                int y = (int)(length * Math.Sin(angle)) + ptInitial.Y;
                ptFinal = new Point(x, y);
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < bodies.Count(); ++i)
            {
                for (int j = i + 1; j < bodies.Count(); ++j)
                {
                    float dx = (bodies[j].Position.X - bodies[i].Position.X);
                    float dy = (bodies[j].Position.Y - bodies[i].Position.Y);
                    // distance between the two bodies, squared
                    float r = (float)Math.Sqrt((dx * dx) + (dy * dy));

                    // body i collided with body j
                    if (r < bodies[i].Radius + bodies[j].Radius)
                    {
                        bodies[i].Alive = false;
                        bodies[j].Alive = false;

                        Body collision = new Body(Color.White, bodies[i].Mass + bodies[j].Mass, bodies[i].Moveable & bodies[j].Moveable);
                        // create at midpoint weighed according to mass of component bodies
                        if (bodies[i].Moveable == bodies[j].Moveable)
                        {
                            collision.Position = new PointF(
                                ((bodies[i].Mass * bodies[i].Position.X) + (bodies[j].Mass * bodies[j].Position.X)) / collision.Mass,
                                ((bodies[i].Mass * bodies[i].Position.Y) + (bodies[j].Mass * bodies[j].Position.Y)) / collision.Mass);
                        }
                        // create at immovable body's position
                        else if (!bodies[i].Moveable)
                        {
                            collision.Position = bodies[i].Position;
                        }
                        // create at immovable body's position
                        else if (!bodies[j].Moveable)
                        {
                            collision.Position = bodies[j].Position;
                        }
                        
                        collision.Velocity = new PointF(
                            (bodies[i].Velocity.X + bodies[j].Velocity.X) / collision.Mass,
                            (bodies[i].Velocity.Y + bodies[j].Velocity.Y) / collision.Mass);
                        additions.Add(collision);
                    }
                    // body i has not collided with body j
                    else
                    {
                        // Newton's Law of Universal Gravitation: F = G m1 m2 / r^2
                        float f = GRAVITY_CONSTANT * (bodies[i].Mass * bodies[j].Mass) / (r * r);

                        // body i's velocity changes proportional to its mass
                        bodies[i].Velocity = new PointF(
                            bodies[i].Velocity.X + (f * dx / bodies[i].Mass),
                            bodies[i].Velocity.Y + (f * dy / bodies[i].Mass));
                        // body j is attracted to body i
                        bodies[j].Velocity = new PointF(
                            bodies[j].Velocity.X + (f * -dx / bodies[j].Mass),
                            bodies[j].Velocity.Y + (f * -dy / bodies[j].Mass));
                    }
                }
            }

            for (int i = 0; i < bodies.Count(); ++i)
            {
                // update position of bodies
                bodies[i].Update();

                // remove bodies which went off-screen
                if (!_panelCanvas.ClientRectangle.Contains(Point.Truncate(bodies[i].Position)))
                {
                    bodies[i].Alive = false;
                }
            }
            // remove bodies that died during this frame
            bodies.RemoveAll(b => !b.Alive);

            // add new created bodies to list
            bodies.AddRange(additions);
            additions.Clear();

            // update display
            using (BufferedGraphicsContext context = new BufferedGraphicsContext())
            {
                using (BufferedGraphics b = context.Allocate(_panelCanvas.CreateGraphics(), _panelCanvas.ClientRectangle))
                {
                    b.Graphics.Clear(Color.Black);

                    foreach (Body body in bodies)
                    {
                        body.Render(b.Graphics);
                    }

                    // draw potential new body at size proportional to mass
                    if (mouseDown)
                    {
                        // velocity vector relative to body's position
                        PointF vf = new PointF(ptFinal.X - ptInitial.X, ptFinal.Y - ptInitial.Y);
                        // predicted direction that new body will proceed along
                        PointF ptPrediction = new PointF(ptInitial.X - 5f * vf.X, ptInitial.Y - 5f * vf.Y);

                        b.Graphics.DrawLine(new Pen(Color.White, 1), ptInitial, ptFinal);
                        b.Graphics.DrawLine(new Pen(Color.Blue, 1), ptInitial, ptPrediction);

                        // draw body above all the lines
                        int radius = (int)Math.Ceiling(Math.Log10((double)_numMass.Value));
                        b.Graphics.FillEllipse(new SolidBrush(Color.Gray), ptInitial.X - radius, ptInitial.Y - radius, 2 * radius, 2 * radius);
                        // show velocity
                        double velocity = Math.Sqrt((vf.X * vf.X) + (vf.Y * vf.Y));
                        b.Graphics.DrawString($"{velocity:F2}", DefaultFont, new SolidBrush(Color.White), ptInitial);
                    }

                    b.Render();
                }
            }
        }
    }
}
