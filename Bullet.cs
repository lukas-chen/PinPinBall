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
                    if (obj != null && obj is Ball)
                    {
                        this.Status = BulletStatus.Dead;
                    }
                    //if (obj != null && obj is Ball)
                    //{

                    //    Random rnd = new Random();
                    //    for (int i = 0; i < 2; i++)
                    //    {
                    //        var b = new Ball(rnd.Next(-5,5), rnd.Next(-5,5));
                    //        b.Left = this.Left;
                    //        b.Top = this.Top;
                    //        b.Checked = true;
                    //        this.Parent.Controls.Add(b);
                    //    }
                    //    this.Status = BulletStatus.Dead;
                    //    this.Visible = false;
                    //}
                    break;
                case BulletStatus.Dead:
                    this.Visible = false;
                    break;

            }

        }
    }
}
