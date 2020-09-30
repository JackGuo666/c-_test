using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MyControl
{
    public partial class Form1 : Form
    {
        public Form1(int PopWidth, int PopHeigth, string PopText)
        {
            InitializeComponent();

            this.Size = new System.Drawing.Size(PopWidth, PopHeigth);
            this.Text = PopText;
        }
    }
}
