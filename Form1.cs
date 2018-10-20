﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PinBall
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            foreach(Control c in this.Controls)
            {
                if (c is IMovable)
                {
                    IMovable obj = (IMovable)c;
                    obj.Move();
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
