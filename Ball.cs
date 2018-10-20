using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PinBall
{
    public class Ball : RadioButton, IMovable, IHitter
    {
        int dx = 3;
        int dy = -3;

        public IHittable HitOthers()
        {
            foreach(Control c in this.Parent.Controls)
            {
                if (c is IHittable && c != this )
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

        void IMovable.Move()
        {
            this.Left += dx;
            this.Top += dy;

            if (this.Left <= 0  || this.Right >= this.Parent.ClientRectangle.Right) dx = -dx;
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
