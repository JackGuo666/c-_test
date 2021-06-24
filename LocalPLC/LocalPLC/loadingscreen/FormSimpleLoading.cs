using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LocalPLC.loadingscreen
{
    public partial class FormSimpleLoading : Form
    {
        UserControl parent_;
        public FormSimpleLoading(UserControl parent)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            parent_ = parent;
        }

        private void FormSimpleLoading_Load(object sender, EventArgs e)
        {
            ////设置一些Loading窗体信息
            //this.FormBorderStyle = FormBorderStyle.FixedSingle;
            //this.ControlBox = false;
            //// 下面的方法用来使得Loading窗体居中父窗体显示

            //int parentForm_Position_x = this.parent_.Location.X;
            //int parentForm_Position_y = this.parent_.Location.Y;
            //int parentForm_Width = this.parent_.Width;
            //int parentForm_Height = this.parent_.Height;

            //int start_x = (int)(parentForm_Position_x + (parentForm_Width - this.Width) / 2);
            //int start_y = (int)(parentForm_Position_y + (parentForm_Height - this.Height) / 2);
            //this.Location = new System.Drawing.Point(start_x, start_y);

        }
    }
}
