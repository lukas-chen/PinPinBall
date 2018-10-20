using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PinBall
{
    public class Ball : RadioButton, IMovable, IHitter, IHittable
    {
        int dx = 3;
        int dy = -3;

        int[,] boom = {
            { 1, 0, -2, -4 },
            { 2, 0, +2, -4 },
            { 3, 1, +4, -2 },
            { 3, 2, +4, +2 },
            { 2, 3, +2, +4 },
            { 1, 3, -2, +4 },
            { 0, 2, -4, +2 },
            { 0, 1, -4, -4 }
        };

        public Ball()
        {
            this.Width = 14;
            this.Height = 14;
            this.Checked = true;
        }

        public Ball(int _dx, int _dy) : this()
        {
            dx = _dx;
            dy = _dy;
        }

        public IHittable HitOthers()
        {
            if (!this.Visible) return null;

            foreach (Control c in this.Parent.Controls)
            {
                if (c is IHittable && c != this)
                {
                    //if (c is Brick && ((Brick)c).Status == BrickStatus.Dead)
                    //{
                    //    return null;
                    //}
                    IHittable obj = (IHittable)c;
                    if (obj.HitBy(this)) return obj;
                }
            }
            return null;
        }

        bool IHittable.HitBy(IHitter hitter)
        {
            if (!this.Visible) return false;

            Control c = (Control)hitter;
            Rectangle r1 = new Rectangle(this.Left, this.Top, this.Width, this.Height);
            Rectangle r2 = new Rectangle(c.Left, c.Top, c.Width, c.Height);
            bool hit = r1.IntersectsWith(r2);

            if (hit && hitter is Bullet)
            {
                this.Visible = false;

                for (int i = 0; i <= boom.GetUpperBound(0); i++)
                {
                    var b = new Ball(boom[i,2], boom[i,3]);
                    b.Left = this.Left - this.Width * 3 / 2 + boom[i, 0] * b.Width;
                    b.Top = this.Top - this.Height * 3 / 2 + boom[i, 1] * b.Height;

                    this.Parent.Controls.Add(b);

                }
            }

            if (hit && hitter is Ball) hit = false;

            return hit;
        }

        void IMovable.Move()
        {
            this.Left += dx;
            this.Top += dy;

            if (this.Left <= 0 || this.Right >= this.Parent.ClientRectangle.Right) dx = -dx;
            if (this.Top <= 0 || this.Bottom >= this.Parent.ClientRectangle.Bottom) dy = -dy;

            IHittable obj = HitOthers();
            if (obj != null)
            {
                Control c = (Control)obj;
                if (dy < 0)
                    this.Top = c.Bottom + 1;
                else
                    this.Top = c.Top - this.Height;

                dy = -dy;
            }

        }
    }
}
