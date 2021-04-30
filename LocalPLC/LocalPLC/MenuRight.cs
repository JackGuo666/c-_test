using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pub
{
    public class CRichTestBoxMenu
    {

        public ContextMenuStrip richMenu = new ContextMenuStrip();
        public ToolStripMenuItem CMcopy = new ToolStripMenuItem("复制");
        public ToolStripMenuItem CMcut = new ToolStripMenuItem("剪贴");
        public ToolStripMenuItem CMdel = new ToolStripMenuItem("删除");
        public ToolStripMenuItem CMcancle = new ToolStripMenuItem("撤销");
        public ToolStripMenuItem CMpaste = new ToolStripMenuItem("粘贴");
        public ToolStripMenuItem CMselectall = new ToolStripMenuItem("全选");
        public ToolStripMenuItem CMpastemul = new ToolStripMenuItem("粘贴");

        public RichTextBox richTextBox;
        public DataGridView dataGridView1 = null;

        public CRichTestBoxMenu()
        {
            init();
        }

        public CRichTestBoxMenu(RichTextBox rchTBox, DataGridView dataGridView)
        {
            richTextBox = rchTBox;
            dataGridView1 = dataGridView;
            richTextBox.ContextMenuStrip = richMenu;
            init();
        }

        public void init()
        {
            richMenu.Items.Add(CMcopy);
            richMenu.Items.Add(CMcut);
            richMenu.Items.Add(CMdel);
            //richMenu.Items.Add(CMcancle);
            //richMenu.Items.Add(CMpaste);
            //richMenu.Items.Add(CMselectall);
            richMenu.Items.Add(CMpastemul);

            CMcopy.Click += CMcopy_Click;
            CMcut.Click += CMcut_Click;
            CMdel.Click += CMdel_Click;
            CMcancle.Click += CMcancle_Click;
            CMpaste.Click += CMpaste_Click;
            CMselectall.Click += CMselectall_Click;
            CMpastemul.Click += CMpastemul_Click;

            richMenu.Opened += contextMenuStrip1_Opened;
        }

        //右键菜单 按钮可见   
        private void contextMenuStrip1_Opened(object sender, EventArgs e)
        {
            if (richTextBox.SelectedText.Length > 0)
            {
                CMcopy.Enabled = true;
                CMcut.Enabled = true;
                CMdel.Enabled = true;
            }
            else
            {
                CMcopy.Enabled = false;
                CMcut.Enabled = false;
                CMdel.Enabled = false;
            }

            if (richTextBox.CanUndo == true)
            {
                this.CMcancle.Enabled = true;
            }
            else
            {
                this.CMcancle.Enabled = false;
            }

            if (Clipboard.GetDataObject().GetDataPresent(DataFormats.Text))
            {
                this.CMpaste.Enabled = true;
            }
            else
            {
                this.CMpaste.Enabled = false;
            }

            if (richTextBox.Text != "")
            {
                CMselectall.Enabled = true;
            }
            else
            {
                CMselectall.Enabled = false;
            }

        }

        //右键菜单 撤销   
        private void CMcancle_Click(object sender, EventArgs e)
        {
            if (CMcancle.Enabled == true)
            {
                richTextBox.Undo();
                richTextBox.ClearUndo();
            }
        }

        //右键菜单剪切   
        private void CMcut_Click(object sender, EventArgs e)
        {
            if (CMcut.Enabled == true)
            {
                richTextBox.Cut();
            }
        }

        //右键菜单 复制   
        private void CMcopy_Click(object sender, EventArgs e)
        {
            if (CMcopy.Enabled == true)
            {
                richTextBox.Copy();
            }
        }


        private void PasteData()
        {
            try
            {


                string clipboardText = Clipboard.GetText(); //获取剪贴板中的内容
                clipboardText = clipboardText.TrimEnd(new char[] { '\r', '\n' });
                if (string.IsNullOrEmpty(clipboardText))
                {
                    return;
                }

                string[] ss = clipboardText.Split(new string[] { "\r\n" }, StringSplitOptions.None);
                int selectedRowIndex = dataGridView1.CurrentRow.Index;
                int selectedColIndex = dataGridView1.CurrentCell.ColumnIndex;
                for (int i = 0; i < ss.Length; i++)
                {
                    string[] ssRow = ss[i].Split(new string[] { "\t" }, StringSplitOptions.None);
                    if (ssRow.Length >= 1)
                    {
                        if(ssRow[0].Length == 0)
                        {
                            //continue;
                        }
                        if(i == 0)
                        {
                            richTextBox.Text = ssRow[0];
                            continue;
                        }

                        if(selectedRowIndex + i == dataGridView1.RowCount)
                        {
                            richTextBox.Visible = false;
                            dataGridView1.CurrentCell = null;
                        }

                        dataGridView1.CurrentCell = this.dataGridView1.Rows[selectedRowIndex + i].Cells[selectedColIndex];
                        this.dataGridView1.Rows[selectedRowIndex + i].Cells[selectedColIndex].Value = ssRow[0];


                    }
                }

                richTextBox.Visible = false;
                dataGridView1.CurrentCell = null;
            }




            catch
            {
               
            }
        }


        //右键菜单 粘贴   
        private void CMpaste_Click(object sender, EventArgs e)
        {
            if (CMpaste.Enabled == true)
            {
                richTextBox.Paste();
            }
        }

        //右键菜单 删除   
        private void CMdel_Click(object sender, EventArgs e)
        {
            if (CMdel.Enabled == true)
            {
                richTextBox.SelectedText = "";
            }
        }

        //右键菜单 全选   
        private void CMselectall_Click(object sender, EventArgs e)
        {
            richTextBox.SelectAll();
        }

        //右键菜单 阅读顺序   
        private void CMpastemul_Click(object sender, EventArgs e)
        {
            if (CMpastemul.Enabled == true)
            {
                PasteData();
            }

            //CMpastemul.Checked = !CMpastemul.Checked;
            //if (CMpastemul.Checked == true)
            //{
            //    richTextBox.SelectionAlignment = HorizontalAlignment.Right;
            //}
            //else
            //{
            //    richTextBox.SelectionAlignment = HorizontalAlignment.Left;
            //}
        }

    }
}
