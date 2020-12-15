using System;
using System.Windows;
using System.Windows.Forms;
using System.Drawing;

namespace LocalPLC.Base
{
    class pictest: PictureBox
    {
        public pictest()
        {
            this.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.TMouseDoubleClick);
            //this.MouseLeave += new System.Windows.Forms.EventHandler(this.TMouseDoubleClick);

            this.MouseEnter += new System.EventHandler(this.TMouseEnter);
            this.MouseLeave += new System.EventHandler(this.TMouseLeave);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TMouseDown);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.TMouseUp);
        }

        public bool MValue
        {
            get { return pic3Selected; }

            set { pic3Selected = value; }

        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if(pic3Selected)
            {
                if (pic3Down)
                {
                    Graphics gc1 = e.Graphics;
                    //gc.DrawLine(new Pen(Color.Red, 5), 0, 0, 500, 500);

                    Pen pen1 = new Pen(Color.DodgerBlue, 6);
                    gc1.DrawRectangle(pen1, 0, 0, this.Width /*- borderWidth*/, this.Height /*- borderWidth*/);


                    base.OnPaint(e);
                    return;
                }

                if (pic3Up)
                {
                    Graphics gc2 = e.Graphics;
                    //gc.DrawLine(new Pen(Color.Red, 5), 0, 0, 500, 500);

                    Pen pen2 = new Pen(Color.DodgerBlue, 3);
                    gc2.DrawRectangle(pen2, 0, 0, this.Width /*- borderWidth*/, this.Height /*- borderWidth*/);


                    base.OnPaint(e);
                    return;
                }

                Graphics gc = e.Graphics;
                //gc.DrawLine(new Pen(Color.Red, 5), 0, 0, 500, 500);

                Pen pen = new Pen(Color.DodgerBlue, 3);
                gc.DrawRectangle(pen, 0, 0, this.Width /*- borderWidth*/, this.Height /*- borderWidth*/);


                base.OnPaint(e);

                return;
            }
            else
            {
                if (pic3Down)
                {
                    Graphics gc = e.Graphics;
                    //gc.DrawLine(new Pen(Color.Red, 5), 0, 0, 500, 500);

                    Pen pen = new Pen(Color.DodgerBlue, 6);
                    gc.DrawRectangle(pen, 0, 0, this.Width /*- borderWidth*/, this.Height /*- borderWidth*/);


                    base.OnPaint(e);
                    return;
                }

                if (pic3Up)
                {
                    Graphics gc = e.Graphics;
                    //gc.DrawLine(new Pen(Color.Red, 5), 0, 0, 500, 500);

                    Pen pen = new Pen(Color.DodgerBlue, 3);
                    gc.DrawRectangle(pen, 0, 0, this.Width /*- borderWidth*/, this.Height /*- borderWidth*/);


                    base.OnPaint(e);
                    return;
                }

                if (pic3Enter)
                {
                    Graphics gc = e.Graphics;
                    //gc.DrawLine(new Pen(Color.Red, 5), 0, 0, 500, 500);

                    Pen pen = new Pen(Color.DodgerBlue, 3);
                    gc.DrawRectangle(pen, 0, 0, this.Width /*- borderWidth*/, this.Height /*- borderWidth*/);


                    base.OnPaint(e);

                }

                if (!pic3Enter)
                {
                    Graphics gc = e.Graphics;

                    //gc.DrawLine(new Pen(Color.Red, 5), 0, 0, 500, 500);
                    //gc.Clear(Color.Transparent);
                    //Pen pen = new Pen(Color.Transparent, 3);
                    //gc.DrawRectangle(pen, 0, 0, this.Width /*- borderWidth*/, this.Height /*- borderWidth*/);
                    //this.BorderStyle = BorderStyle.None;


                    base.OnPaint(e);
                }
            }
            
            
            
        }

        bool pic3Selected = false;
        bool pic3Enter = false;
        bool pic3Down = false;
        bool pic3Up = false;

        public void SetAllFlagFalse()
        {
            pic3Selected = false;
            pic3Enter = false;
            pic3Down = false;
            pic3Up = false;
        }
        private void TMouseDoubleClick(object sender, MouseEventArgs e)
        {
            var v = (PictureBox)this.Parent;


            foreach (Control ct in v.Controls)
            {
                if (ct is pictest)
                {
                    var tmp = (pictest)ct;
                    if (tmp == this)
                    {
                        pic3Down = false;
                        pic3Up = false;
                        this.MValue = true;
                    }
                    else
                    {
                        tmp.MValue = false;
                        tmp.pic3Down = false;
                        tmp.pic3Up = false;
                        tmp.Refresh();
                    }
                }
            }

                pic3Selected = true;
            this.Image = null;
            this.Invalidate();
            //OnPaint(new PaintEventArgs(CreateGraphics(), ClientRectangle));
        }

        private void TMouseLeave(object sender, EventArgs e)
        {
            pic3Enter = false;
            pic3Down = false;
            pic3Up = false;
            this.Image = null;
            this.Invalidate();


            //OnPaint(new PaintEventArgs(CreateGraphics(), ClientRectangle));
        }

        private void TMouseEnter(object sender, EventArgs e)
        {
            pic3Enter = true;
            this.Image = null;
            this.Invalidate();
            //OnPaint(new PaintEventArgs(CreateGraphics(), ClientRectangle));
        }

        private void TMouseDown(object sender, MouseEventArgs e)
        {
            var v = (PictureBox)this.Parent;


            foreach (Control ct in v.Controls)
            {
                if (ct is pictest)
                {
                    var tmp = (pictest)ct;


                    if (tmp == this)
                    {
                        tmp.pic3Down = true;
                        tmp.pic3Up = false;
                        Invalidate();
                    }
                    else
                    {
                        tmp.MValue = false;
                        tmp.pic3Down = false;
                        tmp.pic3Up = false;
                        tmp.Invalidate();
                    }
                }
            }





        }

        private void TMouseUp(object sender, MouseEventArgs e)
        {
            pic3Up = true;
            pic3Down = false;
            this.Image = null;
            this.Invalidate();
        }
    }
}
