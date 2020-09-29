using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;



namespace demo_splitter
{



    public partial class Form1 : Form
    {
        public bool MoveFlag = false;
        int xPos;
        int yPos;
        public bool DoubleFlag = false;
        public Form1()
        {
            InitializeComponent();

            //最近做winform程序，其中有个需求：有两个PictureBox完全重叠，上面一个需要透明，不能遮挡下面的，以为设置上面的BackColor为透明色就可以了，结果不行，上网搜了一下，发现对于我这种需求只需要把上面的PictureBox的Parent设置成下面的PictureBox，同时设置BackColor为透明色就可以了
           // pictureBox4.Parent = pictureBox3;

           //pictureBox4.Location = new Point(0, 0);

            //this.pictureBox3.Controls.Add(pictureBox4);

        }

        private void pictureBox3_MouseDown(object sender, MouseEventArgs e)
        {
            MoveFlag = true;
            xPos = e.X;//当前x坐标.
            yPos = e.Y;//当前y坐标.
        }

        private void pictureBox3_MouseUp(object sender, MouseEventArgs e)
        {
            MoveFlag = false;
        }

        private void pictureBox3_MouseMove(object sender, MouseEventArgs e)
        {
            if (MoveFlag)
            {
                pictureBox3.Left += Convert.ToInt16(e.X - xPos);//设置x坐标.
                pictureBox3.Top += Convert.ToInt16(e.Y - yPos);//设置y坐标.
                //
                //test
            }
        }



        private void pictureBox4_Validating(object sender, CancelEventArgs e)
        {
            int a = 5;
            a = 6;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            pictureBox4.BackColor = Color.Transparent;
            pictureBox4.Parent = pictureBox3;
            pictureBox4.Location = new Point(0, 59);//重新设定标签的位置，这个位置时相对于父控件的左上角

            label1.Parent = pictureBox3;
            label1.Size = new Size(70, 30);
            label1.Location = new Point(0, 0);
        }

        private Random m_Random = new Random();
        private Color m_BorderColor = Color.Transparent;
        private int m_BorderWidth = 1;
        private void pictureBox4_Paint(object sender, PaintEventArgs e)
        {
            PictureBox pic = (PictureBox)sender;

            Pen pen = new Pen(this.m_BorderColor);
            pen.Width = this.m_BorderWidth;
            e.Graphics.DrawRectangle(pen, /*e.ClipRectangle.X*/ 0, /*e.ClipRectangle.Y*/ 0
                , this.pictureBox4.Width - this.m_BorderWidth
                , this.pictureBox4.Height - this.m_BorderWidth);
        }

        private void pictureBox4_MouseMove(object sender, MouseEventArgs e)
        {
            ////随机颜色
            //int A = this.m_Random.Next(0, 255);
            //int R = this.m_Random.Next(0, 255);
            //int G = this.m_Random.Next(0, 255);
            //int B = this.m_Random.Next(0, 255);
            //this.m_BorderColor = Color.FromArgb(A, R, G, B);

            ////随机宽度
            //this.m_BorderWidth = this.m_Random.Next(1, 5);

            ////刷新边界
            //this.pictureBox4.Invalidate();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void label1_Paint(object sender, PaintEventArgs e)
        {
            if (label1.Focused)
            {

                Label lab = (Label)sender;

                Pen pen = new Pen(this.m_BorderColor);
                pen.Width = this.m_BorderWidth;
                e.Graphics.DrawRectangle(pen, /*e.ClipRectangle.X*/ 0, /*e.ClipRectangle.Y*/ 0
                    , this.pictureBox4.Width - this.m_BorderWidth
                    , this.pictureBox4.Height - this.m_BorderWidth);
            }
        }

        private void pictureBox4_Validated(object sender, EventArgs e)
        {

        }

        private void pictureBox4_CursorChanged(object sender, EventArgs e)
        {

        }

        private void label1_Enter(object sender, EventArgs e)
        {

        }

        private void textBox1_Enter(object sender, EventArgs e)
        {

        }

        private void textBox1_Leave(object sender, EventArgs e)
        {

        }

        private void label1_Leave(object sender, EventArgs e)
        {

        }

        private void label1_MouseHover(object sender, EventArgs e)
        {

        }

        private void pictureBox4_MouseEnter(object sender, EventArgs e)
        {
            //随机颜色
            int A = this.m_Random.Next(0, 255);
            int R = this.m_Random.Next(0, 255);
            int G = this.m_Random.Next(0, 255);
            int B = this.m_Random.Next(0, 255);
            this.m_BorderColor = Color.SeaGreen;

            //随机宽度
            this.m_BorderWidth = /*this.m_Random.Next(1, 5);*/ 6;

            //刷新边界
            this.pictureBox4.Invalidate();
        }

        private void pictureBox4_MouseLeave(object sender, EventArgs e)
        {

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox4_MouseDoubleClick(object sender, MouseEventArgs e)
        {

        }

        private void label1_DoubleClick(object sender, EventArgs e)
        {
            DoubleFlag = false;

            //pictureBox1没有边框颜色
            //pictureBox1.BorderStyle = BorderStyle.None;
            this.m_BorderColor = Color.Transparent;

        }

        private void pictureBox4_MouseDoubleClick_1(object sender, MouseEventArgs e)
        {
            this.m_BorderColor = Color.SeaGreen;

            //随机宽度
            this.m_BorderWidth = /*this.m_Random.Next(1, 5);*/ 6;

            //刷新边界
            this.pictureBox4.Invalidate();

            DoubleFlag = true;

            DoubleFlag = true;
        }
    }
}
