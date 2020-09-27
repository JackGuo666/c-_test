using System;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using System.Collections.Generic;



class Form1 : Form
{
    private Button button1;
    private Panel panel1;
    private MenuStrip menuStrip1;
    private ToolStripMenuItem testToolStripMenuItem;
    private ToolStripMenuItem testToolStripMenuItem1;
    private ToolStrip toolStrip1;
    private ToolStripButton toolStripButton1;
    private DataGridView dataGridView1 = new DataGridView();

    private class Student
    {
        public Student(Boolean bValue, string str2, string str3)
        {
            value = bValue;
            test2 = str2;
            test3 = str3;
        }

        public Boolean value { get; set; }
        public string test2 { get; set; }
        public string test3 { get; set; }
    }


    [STAThread]
    public static void Main()
    {
        Application.EnableVisualStyles();
        Application.Run(new Form1());
    }

    public Form1()
    {
        this.AutoSize = true;
        this.Load += new EventHandler(Form1_Load);

        InitializeComponent();
    }

    public void Form1_Load(object sender, EventArgs e)
    {

        DataGridViewCheckBoxColumn column0 =
            new DataGridViewCheckBoxColumn();
        DataGridViewDisableButtonColumn column1 =
            new DataGridViewDisableButtonColumn();

        DataGridViewComboBoxColumn column2 = new DataGridViewComboBoxColumn();
        column2.Items.Add("test1");
        column2.Items.Add("test2");

        column0.Name = "CheckBoxes";
        column1.Name = "Buttons";
        column2.Name = "Combobox";

        dataGridView1.Columns.Add(column0);
        dataGridView1.Columns.Add(column1);
        dataGridView1.Columns.Add(column2);
        dataGridView1.RowCount = 8;
        dataGridView1.AutoSize = true;
        dataGridView1.AllowUserToAddRows = false;
        dataGridView1.ColumnHeadersDefaultCellStyle.Alignment =
            DataGridViewContentAlignment.MiddleCenter;

        // Set the text for each button.
        int row = dataGridView1.RowCount;
        for (int i = 0; i < dataGridView1.RowCount; i++)
        {
            dataGridView1.Rows[i].Cells["Buttons"].Value =
                "Button " + i.ToString();

            dataGridView1.Rows[i].Cells["Combobox"].Value = "test2";
        }

        this.panel1.Controls.Clear();
        this.panel1.Controls.Add(dataGridView1);
        //dataGridView1.AutoGenerateColumns = true;
        //var source = new BindingSource();

        ////使用List<>泛型集合填充DataGridView  
        //List<Student> students = new List<Student>();
        //Student hat = new Student(true, "12", "Male");
        //Student peter = new Student(false, "14", "Male");
        //Student dell = new Student(true, "16", "Male");
        //Student anne = new Student(false, "19", "Female");
        //students.Add(hat);
        //students.Add(peter);
        //students.Add(dell);
        //students.Add(anne);

        //source.DataSource = students;

        //this.dataGridView1.DataSource = source;
        //this.dataGridView1.Refresh();

        //dataGridView1.CellValueChanged +=
        //    new DataGridViewCellEventHandler(dataGridView1_CellValueChanged);
        //dataGridView1.CurrentCellDirtyStateChanged +=
        //    new EventHandler(dataGridView1_CurrentCellDirtyStateChanged);
        //dataGridView1.CellClick +=
        //    new DataGridViewCellEventHandler(dataGridView1_CellClick);

        //this.Controls.Add(dataGridView1);
    }

    // This event handler manually raises the CellValueChanged event
    // by calling the CommitEdit method.
    void dataGridView1_CurrentCellDirtyStateChanged(object sender,
        EventArgs e)
    {
        if (dataGridView1.IsCurrentCellDirty)
        {
            dataGridView1.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }
    }

    // If a check box cell is clicked, this event handler disables  
    // or enables the button in the same row as the clicked cell.
    public void dataGridView1_CellValueChanged(object sender,
        DataGridViewCellEventArgs e)
    {
        if (dataGridView1.Columns[e.ColumnIndex].Name == "CheckBoxes")
        {
            DataGridViewDisableButtonCell buttonCell =
                (DataGridViewDisableButtonCell)dataGridView1.
                Rows[e.RowIndex].Cells["Buttons"];

            DataGridViewCheckBoxCell checkCell =
                (DataGridViewCheckBoxCell)dataGridView1.
                Rows[e.RowIndex].Cells["CheckBoxes"];
            buttonCell.Enabled = !(Boolean)checkCell.Value;

            dataGridView1.Invalidate();
        }
    }

    // If the user clicks on an enabled button cell, this event handler  
    // reports that the button is enabled.
    void dataGridView1_CellClick(object sender,
        DataGridViewCellEventArgs e)
    {
        if (dataGridView1.Columns[e.ColumnIndex].Name == "Buttons")
        {
            DataGridViewDisableButtonCell buttonCell =
                (DataGridViewDisableButtonCell)dataGridView1.
                Rows[e.RowIndex].Cells["Buttons"];

            if (buttonCell.Enabled)
            {
                MessageBox.Show(dataGridView1.Rows[e.RowIndex].
                    Cells[e.ColumnIndex].Value.ToString() +
                    " is enabled");
            }
        }
    }

    private void InitializeComponent()
    {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.button1 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.testToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.testToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.menuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 62);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(117, 36);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // panel1
            // 
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 104);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(757, 468);
            this.panel1.TabIndex = 1;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.testToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(757, 25);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // testToolStripMenuItem
            // 
            this.testToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.testToolStripMenuItem1});
            this.testToolStripMenuItem.Name = "testToolStripMenuItem";
            this.testToolStripMenuItem.Size = new System.Drawing.Size(41, 21);
            this.testToolStripMenuItem.Text = "test";
            // 
            // testToolStripMenuItem1
            // 
            this.testToolStripMenuItem1.Name = "testToolStripMenuItem1";
            this.testToolStripMenuItem1.Size = new System.Drawing.Size(180, 22);
            this.testToolStripMenuItem1.Text = "test";
            this.testToolStripMenuItem1.Click += new System.EventHandler(this.testToolStripMenuItem1_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 25);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(757, 25);
            this.toolStrip1.TabIndex = 3;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton1.Text = "toolStripButton1";
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(757, 572);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.menuStrip1);
            this.Name = "Form1";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    private void panel1_Paint(object sender, PaintEventArgs e)
    {

    }

    private void button1_Click(object sender, EventArgs e)
    {
        int row = dataGridView1.RowCount;
        dataGridView1.RowCount += 1;

        // Set the text for each button.
        int i = row;

       // for (int i = 0; i < dataGridView1.RowCount; i++)
        {
            dataGridView1.Rows[i].Cells["CheckBoxes"].Value = 1;

            dataGridView1.Rows[i].Cells["Buttons"].Value = "Button " + i.ToString();

            dataGridView1.Rows[i].Cells["Combobox"].Value = "test2";
        }

        dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
    }

    private void testToolStripMenuItem1_Click(object sender, EventArgs e)
    {
        int a = 5;
        a = 6;
    }
}

public class DataGridViewDisableButtonColumn : DataGridViewButtonColumn
{
    public DataGridViewDisableButtonColumn()
    {
        this.CellTemplate = new DataGridViewDisableButtonCell();
    }
}

public class DataGridViewDisableButtonCell : DataGridViewButtonCell
{
    private bool enabledValue;
    public bool Enabled
    {
        get
        {
            return enabledValue;
        }
        set
        {
            enabledValue = value;
        }
    }

    // Override the Clone method so that the Enabled property is copied.
    public override object Clone()
    {
        DataGridViewDisableButtonCell cell =
            (DataGridViewDisableButtonCell)base.Clone();
        cell.Enabled = this.Enabled;
        return cell;
    }

    // By default, enable the button cell.
    public DataGridViewDisableButtonCell()
    {
        this.enabledValue = true;
    }

    protected override void Paint(Graphics graphics,
        Rectangle clipBounds, Rectangle cellBounds, int rowIndex,
        DataGridViewElementStates elementState, object value,
        object formattedValue, string errorText,
        DataGridViewCellStyle cellStyle,
        DataGridViewAdvancedBorderStyle advancedBorderStyle,
        DataGridViewPaintParts paintParts)
    {
        // The button cell is disabled, so paint the border,  
        // background, and disabled button for the cell.
        if (!this.enabledValue)
        {
            // Draw the cell background, if specified.
            if ((paintParts & DataGridViewPaintParts.Background) ==
                DataGridViewPaintParts.Background)
            {
                SolidBrush cellBackground =
                    new SolidBrush(cellStyle.BackColor);
                graphics.FillRectangle(cellBackground, cellBounds);
                cellBackground.Dispose();
            }

            // Draw the cell borders, if specified.
            if ((paintParts & DataGridViewPaintParts.Border) ==
                DataGridViewPaintParts.Border)
            {
                PaintBorder(graphics, clipBounds, cellBounds, cellStyle,
                    advancedBorderStyle);
            }

            // Calculate the area in which to draw the button.
            Rectangle buttonArea = cellBounds;
            Rectangle buttonAdjustment =
                this.BorderWidths(advancedBorderStyle);
            buttonArea.X += buttonAdjustment.X;
            buttonArea.Y += buttonAdjustment.Y;
            buttonArea.Height -= buttonAdjustment.Height;
            buttonArea.Width -= buttonAdjustment.Width;

            // Draw the disabled button.                
            ButtonRenderer.DrawButton(graphics, buttonArea,
                PushButtonState.Disabled);

            // Draw the disabled button text. 
            if (this.FormattedValue is String)
            {
                TextRenderer.DrawText(graphics,
                    (string)this.FormattedValue,
                    this.DataGridView.Font,
                    buttonArea, SystemColors.GrayText);
            }
        }
        else
        {
            // The button cell is enabled, so let the base class 
            // handle the painting.
            base.Paint(graphics, clipBounds, cellBounds, rowIndex,
                elementState, value, formattedValue, errorText,
                cellStyle, advancedBorderStyle, paintParts);
        }
    }
}