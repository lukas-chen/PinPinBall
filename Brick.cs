﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PinBall
{
    public class Brick : Button, IMovable, IHittable, IHitter
    {
        private BrickStatus _Status = BrickStatus.Normal;
        public BrickStatus Status
        {
            get
            {
                return _Status;
            }
            set
            {
                _Status = value;
            }
        }

        public bool HitBy(IHitter hitter)
        {
            if (!this.Visible) return false;

            Control c = (Control)hitter;
            Rectangle r1 = new Rectangle(this.Left, this.Top, this.Width, this.Height);
            Rectangle r2 = new Rectangle(c.Left, c.Top, c.Width, c.Height);
            bool hit = r1.IntersectsWith(r2);

            if (hit && hitter is Ball)
            {
                switch (this.Status)
                {
                    case BrickStatus.Normal:
                        this.Status = BrickStatus.Falling;
                        break;
                    case BrickStatus.Falling:
                        this.Status = BrickStatus.Floating;
                        break;
                    case BrickStatus.Floating:
                        this.Status = BrickStatus.Falling;
                        break;
                    case BrickStatus.Dead:
                        this.Visible = false;
                        //hit = false;
                        break;
                }
            }

            return hit;
        }

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
                case BrickStatus.Normal:

                    break;
                case BrickStatus.Falling:
                    this.Top += 2;

                    if (this.Top > this.Parent.ClientRectangle.Bottom)
                    {
                        //this.Visible = false;
                        this.Status = BrickStatus.Dead;
                    }
                    break;
                case BrickStatus.Floating:
                    this.Top -= 2;

                    if (this.Top <= 2) this.Status = BrickStatus.Normal;
                    var h = this.HitOthers();
                    if (h != null && h is Brick)
                    {
                        this.Top += 5;
                        this.Status = BrickStatus.Normal;
                    }
                    break;
                case BrickStatus.Dead:

                    break;
            }

        }
    }
}
