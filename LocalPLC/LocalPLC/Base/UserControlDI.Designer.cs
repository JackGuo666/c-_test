﻿namespace LocalPLC.Base
{
    partial class UserControlDI
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("DI0");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("DI1");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("DI2");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("DI3");
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("DI4");
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("DI5");
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("DI6");
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("DI7");
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.SuspendLayout();
            // 
            // treeView1
            // 
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.Location = new System.Drawing.Point(0, 0);
            this.treeView1.Name = "treeView1";
            treeNode1.Name = "节点0";
            treeNode1.Text = "DI0";
            treeNode2.Name = "节点1";
            treeNode2.Text = "DI1";
            treeNode3.Name = "节点2";
            treeNode3.Text = "DI2";
            treeNode4.Name = "节点3";
            treeNode4.Text = "DI3";
            treeNode5.Name = "节点4";
            treeNode5.Text = "DI4";
            treeNode6.Name = "节点5";
            treeNode6.Text = "DI5";
            treeNode7.Name = "节点6";
            treeNode7.Text = "DI6";
            treeNode8.Name = "节点7";
            treeNode8.Text = "DI7";
            this.treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode3,
            treeNode4,
            treeNode5,
            treeNode6,
            treeNode7,
            treeNode8});
            this.treeView1.Size = new System.Drawing.Size(687, 445);
            this.treeView1.TabIndex = 0;
            // 
            // UserControlDI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.treeView1);
            this.Name = "UserControlDI";
            this.Size = new System.Drawing.Size(687, 445);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView treeView1;
    }
}
