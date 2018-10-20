using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PinBall
{
    public class Bat : Button, IMovable, IHittable
    {
        int originalX, originalY;

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDoubleClick(e);

            if (e.Button == MouseButtons.Left)
            {
                originalX = e.X;
                originalY = e.Y;
            }

            if (e.Button == MouseButtons.Right)
            {
                var b = new Bullet();
                b.Width = 4;
                b.Height = 10;
                b.Left = this.Left + (this.Width + b.Width) / 2;
                b.Top = this.Top - b.Top;
                b.BackColor = Color.Red;

                this.Parent.Controls.Add(b);

            }
        }

        protected override void OnMouseMove(MouseEventArgs mevent)
        {
            base.OnMouseMove(mevent);

            if (mevent.Button == MouseButtons.Left)
            {
                int dx = mevent.X - originalX;
                int dy = mevent.Y - originalY;

                this.Top += dy;
                this.Left += dx;
            }
        }


        public bool HitBy(IHitter hitter)
        {
            Control c = (Control)hitter;
            Rectangle r1 = new Rectangle(this.Left, this.Top, this.Width, this.Height);
            Rectangle r2 = new Rectangle(c.Left, c.Top, c.Width, c.Height);
            bool hit = r1.IntersectsWith(r2);

            return hit;
        }
        void IMovable.Move()
        {

        }
    }
}
