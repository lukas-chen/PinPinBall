using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PinBall
{
    public class Bullet : Panel, IMovable, IHitter
    {
        public IHittable HitOthers()
        {
            foreach (Control c in this.Parent.Controls)
            {
                if (c is IHittable && c != this)
                {
                    IHittable obj = (IHittable)c;
                    if (obj.HitBy(this)) return obj;
                }
            }
            return null;
        }

        void IMovable.Move()
        {
            this.Top -= 4;
        }
    }
}
