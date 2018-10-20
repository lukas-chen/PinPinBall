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
        public BulletStatus Status { get; set; } = BulletStatus.Flying;
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
            switch (this.Status)
            {
                case BulletStatus.Flying:
                    this.Top -= 4;
                    var obj = this.HitOthers();

                    if (obj != null && obj is Brick)
                    {
                        var b = (Brick)obj;
                        b.Status = BrickStatus.Dead;
                        b.Visible = false;
                        this.Status = BulletStatus.Dead;

                    }

                    break;
                case BulletStatus.Dead:
                    this.Visible = false;   
                    break;

            }

        }
    }
}
