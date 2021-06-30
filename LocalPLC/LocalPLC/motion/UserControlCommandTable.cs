using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZedGraph;
using LocalPLC.custom;

namespace LocalPLC.motion
{
    public partial class UserControlCommandTable : UserControl
    {
		//public class DataGridViewComboBoxColumnEx : DataGridViewComboBoxColumn
		//{
		//    public DataGridViewComboBoxColumnEx()
		//    {
		//        this.CellTemplate = new DataGridViewComboBoxCellEx();
		//    }
		//}

		//public class DataGridViewComboBoxCellEx : DataGridViewComboBoxCell
		//{

		//    // Override the Clone method so that the Enabled property is copied.
		//    public override object Clone()
		//    {
		//        DataGridViewComboBoxCellEx cell =
		//            (DataGridViewComboBoxCellEx)base.Clone();
		//        return cell;
		//    }

		//    // By default, enable the button cell.
		//    public DataGridViewComboBoxCellEx()
		//    {

		//    }
		//}

		#region 
		//变量
		DataTable dtData = null;

		enum Type {None, MC_MoveAbsolute, MC_Relative, MC_SetPosition, MC_MoveVelocity, MC_Halt}

		RichTextBoxEx richTextBox = new RichTextBoxEx();
		enum NextStep {Done, Blending_previous, Probe_input_event, SW_event, Delay}
		List<string> typeList = new List<string>();
		List<string> noneList = new List<string>();
		List<string> noneListVel = new List<string>();
		List<string> posList = new List<string>();

		Dictionary<int, string> nextStepDic = new Dictionary<int, string>();

		TreeNode node_ = null;
		CommandTable data = null;
		int stepColumn = 0;
		int typeColumn = 1;
		int posColumn = 2;
		int disColumn = 3;
		int speedColumn = 4;
		int accColumn = 5;
		int decColumn = 6;
		int jerkColumn = 7;
		int nextStepColumn = 8;
		int eventColumn = 9;
		int delayColumn = 10;
		int noteColumn = 11;

		int defaultRows = 16;
		#endregion



		#region
		string columnStepName = "步骤";
		string columnTypeName = "类型";

		string columnPosName = "位置";
		string columnDisName = "距离";
		string columnSpeedName = "速度";
		string columnAccName = "加速度";

		string columnDecName = "减速度";
		string columnJerkName = "Jerk";
		string columnNextStepName = "下一步";
		string columnEventVarName = "事件变量";
		string columnDelayName = "延迟时间(ms)";
		string columnNoteName = "备注";

		#endregion

		public class ComboboxItem
		{
			public string Text { get; set; }
			public object Value { get; set; }

			public override string ToString()
			{
				return Text;
			}
		}

		public UserControlCommandTable()
        {
            InitializeComponent();
			this.dataGridView1.DataError += delegate (object sender, DataGridViewDataErrorEventArgs e)
			{
			};

			typeList.Clear();
			typeList.Add(Type.None.ToString());
			typeList.Add(Type.MC_MoveAbsolute.ToString());
			typeList.Add(Type.MC_Relative.ToString());
			typeList.Add(Type.MC_SetPosition.ToString());
			typeList.Add(Type.MC_MoveVelocity.ToString());
			typeList.Add(Type.MC_Halt.ToString());

			noneList.Clear();
			noneList.Add("Done");
			noneList.Add("Blending previous");
			noneList.Add("Blending next");
			noneList.Add("Probe input event");
			noneList.Add("SW event");
			noneList.Add("Delay");

			noneListVel.Add("In velocity");
			noneListVel.Add("Blending previous");
			noneListVel.Add("Blending next");
			noneListVel.Add("Probe input event");
			noneListVel.Add("SW event");
			noneListVel.Add("Delay");

			posList.Clear();
			posList.Add("Done");
			posList.Add("Probe input event");
			posList.Add("SW event");
			posList.Add("Delay");

			this.dataGridView1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
			bindData();

			var control = zedGraphControl1;
			//是否允许横向缩放
			control.IsEnableZoom = false;
			//是否允许纵向缩放
			control.IsEnableVZoom = false;

			//是否显示右键菜单
			control.IsShowContextMenu = false;

			//control.IsShowCursorValues = true;



			control.IsShowPointValues = true;

			GraphPane myPane = zedGraphControl1.GraphPane;
			//myPane.Fill = new Fill(Color.BlueViolet, Color.BlueViolet, 45.0f);


			myPane.YAxis.MajorGrid.IsVisible = true;// '水平辅助线

			myPane.XAxis.MajorGrid.IsVisible = true;// '垂直辅助线

			myPane.YAxis.MajorGrid.DashOn = 0;

			myPane.XAxis.MinorGrid.DashOff = 0;

			// Set the title and axis labels
			myPane.Title.Text = "My Test Graph\n(For CodeProject Sample)";
			myPane.XAxis.Title.Text = "My X Axis";
			myPane.YAxis.Title.Text = "My Y Axis";

			// Make up some data arrays based on the Sine function
			PointPairList list1 = new PointPairList();
			PointPairList list2 = new PointPairList();
			for (int i = 0; i < 36; i++)
			{
				double x = (double)i + 5;
				double y1 = 1.5 + Math.Sin((double)i * 0.2);
				double y2 = 3.0 * (1.5 + Math.Sin((double)i * 0.2));
				list1.Add(x, y1);
				list2.Add(x, y2);
			}

			// Generate a red curve with diamond
			// symbols, and "Porsche" in the legend
			LineItem myCurve = myPane.AddCurve("Porsche",
				list1, Color.Red, SymbolType.Diamond);

			// Generate a blue curve with circle
			// symbols, and "Piper" in the legend
			LineItem myCurve2 = myPane.AddCurve("Piper",
				list2, Color.Blue, SymbolType.Circle);

			zedGraphControl1.AxisChange();
		}

		public void initData(TreeNode node)
        {
			button_valid.Enabled = true;
			button_cancel.Enabled = false;
			node_ = node;
			data = node.Tag as CommandTable;

			string empty = "";
			dtData.Clear();
			for (int i = 0; i < data.stepList.Count; i++)
			{
				DataRow drData;
				drData = dtData.NewRow();
				drData[stepColumn] = data.stepList[i].step;
				drData[typeColumn] = data.stepList[i].type;
				drData[posColumn] = data.stepList[i].pos;
				drData[disColumn] = data.stepList[i].dis;
				drData[speedColumn] = data.stepList[i].speed;
				drData[accColumn] = data.stepList[i].acc;
				drData[decColumn] = data.stepList[i].dec;
				drData[jerkColumn] = data.stepList[i].jerk;
				drData[nextStepColumn] = data.stepList[i].nextStep;
				drData[eventColumn] = data.stepList[i].eventVar;
				drData[delayColumn] = data.stepList[i].delay;
				drData[noteColumn] = data.stepList[i].note;


				dtData.Rows.Add(drData);
				//((DataGridViewComboBoxColumn)dataGridView1.Columns[columnTypeName]).DefaultCellStyle.NullValue = "None";


				//var temp = (DataGridViewComboBoxCell)dataGridView1.Rows[i].Cells[columnTypeName];
				//var count = temp.Items.Count;
				////temp.DisplayMember = "MC_Halt";
				////temp.ValueMember = "MC_Halt";
				//var column = dataGridView1.Columns[columnTypeName] as DataGridViewComboBoxColumn;
				//DataTable dt = column.DataSource as DataTable;
				
				//for (int j = 0; j < column.Items.Count; j++)
    //            {
				//	//column.Items[i]

				//}

				////((DataGridViewComboBoxCell)dataGridView1.Rows[i].Cells[columnTypeName]).Style.NullValue = "MC_Halt";
				////((DataGridViewComboBoxCell)dataGridView1.Rows[i].Cells[columnTypeName]).Value = "MC_Halt";
			}

			this.dataGridView1.DataSource = dtData;
			this.dataGridView1.Refresh();
			

		}

        private void bindData()
        {
			dtData = new DataTable();
            dtData.Columns.Add(columnStepName, typeof(string));
			//类型
			dtData.Columns.Add("类型", typeof(string));
			dtData.Columns.Add(/*"距离"*/columnPosName, typeof(string));
			dtData.Columns.Add(/*"距离"*/columnDisName, typeof(string));
            dtData.Columns.Add(/*"速度"*/columnSpeedName, typeof(string));
            dtData.Columns.Add(/*"加速度"*/columnAccName, typeof(string));
            dtData.Columns.Add(/*"减速度"*/columnDecName, typeof(string));
            dtData.Columns.Add(/*"Jerk"*/columnJerkName, typeof(string));
            dtData.Columns.Add("下一步", typeof(string));
            dtData.Columns.Add(/*"事件变量"*/columnEventVarName, typeof(string));
            dtData.Columns.Add(/*"延迟时间(ms)"*/columnDelayName, typeof(string));
            dtData.Columns.Add(/*"备注"*/columnNoteName, typeof(string));

            this.dataGridView1.DataSource = dtData;


			string empty = "";
			for (int i = 0; i < 16; i++)
			{
				DataRow drData;
				drData = dtData.NewRow();
				drData[stepColumn] = i.ToString();
				drData[typeColumn] = "None";
				drData[posColumn] = empty;
				drData[disColumn] = empty;
				drData[speedColumn] = empty;
				drData[accColumn] = empty;
				drData[decColumn] = empty;
				drData[jerkColumn] = empty;
				drData[eventColumn] = empty;
				drData[delayColumn] = empty;
				drData[noteColumn] = empty;


				dtData.Rows.Add(drData);
				//((DataGridViewComboBoxColumn)dataGridView1.Columns[columnTypeName]).DefaultCellStyle.NullValue = "None";
			}

			this.dataGridView1.DataSource = dtData;

			for (int i = 0; i < dataGridView1.RowCount; i++)
			{
				if (dataGridView1.Rows[i].Cells[columnTypeName].Value.ToString() == "None")
				{
					dataGridView1[0, i].ReadOnly = true;

				}
			}

			////插入类型combobox列
			//DataGridViewComboBoxColumn comboBoxColumn = new DataGridViewComboBoxColumn();
			//comboBoxColumn.DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox;
			//comboBoxColumn.Name = columnTypeName;
			//dgvCombobox(ref comboBoxColumn, typeList);
			//comboBoxColumn.DefaultCellStyle.NullValue = "None";

			//int columnIndex = typeColumn;
			//if (dataGridView1.Columns[columnTypeName] == null)
			//{
			//	dataGridView1.Columns.Insert(columnIndex, comboBoxColumn);
			//}

			//comboBoxColumn = new DataGridViewComboBoxColumn();
			//comboBoxColumn.DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox;
			//comboBoxColumn.Name = "下一步";
			//dgvCombobox(ref comboBoxColumn, noneList);
			//comboBoxColumn.DefaultCellStyle.NullValue = "None";

			//if (dataGridView1.Columns["下一步"] == null)
			//{
			//	dataGridView1.Columns.Insert(nextStepColumn + 2, comboBoxColumn);
			//}


		}

		public void dgvCombobox(ref DataGridViewComboBoxColumn column, List<string> strTmp)
		{
			DataTable dt = new DataTable();
			DataColumn dc = new DataColumn("tmp", typeof(string));
			dt.Columns.Add(dc);
			dt.Columns.Add(new DataColumn("ID", typeof(string)));
			DataRow dr;
			for (int i = 0; i < strTmp.Count; i++)
			{
				dr = dt.NewRow();
				dr["tmp"] = strTmp[i];
				dr["id"] = i;
				dt.Rows.Add(dr);
			}
			//为combobox绑定生成的表  
			column.DataSource = dt; //combobox列的数据源，绑定为生成的表  
			column.DisplayMember = "tmp";//要显示的名称，表的文字例  
			column.ValueMember = dt.Columns[1].ToString();//文字对应的值，此列将和columns2.DataPropertyName 属性的值对应来显示选中的值  

		}

		private void comboBox_KeyPress(object sender, KeyPressEventArgs e)
		{
			e.Handled = true;
		}



		Panel p;
		private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
			if (this.dataGridView1.CurrentCell.OwningColumn.Name == columnTypeName)
			{
				TextBox textbox = (TextBox)e.Control;
				var text = textbox.Text;
				p = (Panel)e.Control.Parent;
				p.Controls.Clear();
				ComboBox combo = new ComboBox();
				combo.Dock = DockStyle.Fill;
				//btn.Click += new EventHandler(btn_Click);
				combo.Parent = p;
				p.Controls.Add(combo);
				combo.DataSource = typeList;
				combo.Text = text;

				combo.KeyPress -= new KeyPressEventHandler(comboBox_KeyPress);
				combo.KeyPress += new KeyPressEventHandler(comboBox_KeyPress);

				combo.SelectedIndexChanged +=
									new EventHandler(comboBox_SelectedIndexChanged);
			}
			else if(this.dataGridView1.CurrentCell.OwningColumn.Name == columnNextStepName)
            {
				var value = this.dataGridView1.Rows[this.dataGridView1.CurrentCell.RowIndex].Cells[columnTypeName].Value.ToString();
				if (value == Type.MC_MoveAbsolute.ToString()
					|| value == Type.MC_Relative.ToString()
					|| value == Type.MC_SetPosition.ToString()
					|| value == Type.MC_Halt.ToString()
					|| value == Type.MC_MoveVelocity.ToString()
					)
                {
					//noneList
					TextBox textbox = (TextBox)e.Control;
					var text = textbox.Text;
					p = (Panel)e.Control.Parent;
					p.Controls.Clear();
					ComboBox combo = new ComboBox();
					combo.Dock = DockStyle.Fill;
					combo.Parent = p;
					p.Controls.Add(combo);

					var type = dataGridView1.Rows[this.dataGridView1.CurrentCell.RowIndex].Cells[columnTypeName].Value.ToString();
					if (type == Type.MC_SetPosition.ToString())
                    {
						combo.DataSource = posList;
					}
					else if(type == Type.MC_MoveVelocity.ToString())
                    {
						combo.DataSource = noneListVel;
					}
					else
                    {
						combo.DataSource = noneList;
					}

					
					//this.comboBox_BackOriginal.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.comboBox_BackOriginal_KeyPress);//禁止用户改变DataGridView1の所有行的行高


					combo.Text = text;
					combo.KeyPress -= new KeyPressEventHandler(comboBox_KeyPress);
					combo.KeyPress += new KeyPressEventHandler(comboBox_KeyPress);
					combo.SelectedIndexChanged +=
										new EventHandler(comboBox_SelectedIndexChanged);

				}

				//((ComboBox)e.Control).SelectedIndexChanged +=
				//	new EventHandler(comboBox_SelectedIndexChanged);
			}
			else if(this.dataGridView1.CurrentCell.OwningColumn.Name == columnNoteName
				|| this.dataGridView1.CurrentCell.OwningColumn.Name == columnEventVarName)
            {
				TextBox control = (TextBox)e.Control;
				richTextBox.Text = control.Text;
				p = (Panel)e.Control.Parent;
				p.Controls.Clear();
				richTextBox.Dock = DockStyle.Fill;
				richTextBox.Parent = dataGridView1;
				p.Controls.Add(richTextBox);
				richTextBox.Focus();
				richTextBox.SelectAll();
				richTextBox.SelectionStart = this.Text.Length;


				control.MaxLength = 30;
			}
			else if(this.dataGridView1.CurrentCell.OwningColumn.Name == columnDisName ||
				this.dataGridView1.CurrentCell.OwningColumn.Name == columnPosName ||
				this.dataGridView1.CurrentCell.OwningColumn.Name == columnSpeedName ||
				this.dataGridView1.CurrentCell.OwningColumn.Name == columnAccName ||
				this.dataGridView1.CurrentCell.OwningColumn.Name == columnDecName ||
				this.dataGridView1.CurrentCell.OwningColumn.Name == columnJerkName ||
				this.dataGridView1.CurrentCell.OwningColumn.Index == delayColumn)
            {
				TextBox control = (TextBox)e.Control;
				control.KeyPress -= new KeyPressEventHandler(control_KeyPress);
				control.KeyPress += new KeyPressEventHandler(control_KeyPress);
				//control.TextChanged -= new EventHandler(control_TextChange);
				//control.TextChanged += new EventHandler(control_TextChange);
			}
			else
            {

            }
		}

		void control_TextChange(object sender, EventArgs e)
        {

		}

		void control_KeyPress(object sender, KeyPressEventArgs e)
		{
			//if (e.KeyChar != 8 && !Char.IsDigit(e.KeyChar))//如果不是输入数字就不让输入
			//{
			//	e.Handled = true;
			//}

			//该事件用于控制只接收数字、小数点和退格键输入，别且不能输入两个小数点。

			System.Text.RegularExpressions.Regex rg = new System.Text.RegularExpressions.Regex(@"\.");

			string textBoxStr = ((TextBox)sender).Text;

			var mc = rg.Matches(textBoxStr);

			int textBoxCount = mc.Count;

			//允许输入数字，小数点，退格键，不允许输入大于18为的数字，不允许输入两个小数点

			if ((int)e.KeyChar >= 48 && (int)e.KeyChar <= 57 || (int)e.KeyChar == 8 || e.KeyChar == '.')//只能输入0-9数字和BackSpace
			{
				if (textBoxStr.Length < 18 && textBoxCount < 2)

				{
					if (e.KeyChar != '.' || textBoxCount == 0)

					{
						e.Handled = false;

					}

					else

					{
						e.Handled = true;

					}

				}

				else

				{
					if((int)e.KeyChar == 8)
                    {
						e.Handled = false;
					}
					else
                    {
						e.Handled = true;
					}


				}

			}

			else

			{
				e.Handled = true;

			}

			////小数点后2位
			//string[] ddrs = textBoxStr.Split('.');
			//if (textBoxStr.Contains(".")/* && ddrs[1].Length == 5*/)
			//{
			//	e.Handled = true;
			//}
		}

		void setCellValue(string columnName, string value, bool readOnly = false)
        {
			var disCell = ((DataGridViewCell)this.dataGridView1.CurrentRow.Cells[columnName]);
			var column = disCell.ColumnIndex;
			disCell.Value = value;
			disCell.ReadOnly = readOnly;
			if(readOnly)
            {
				disCell.Style.BackColor = Color.Lavender;
			}
			else
            {
				disCell.Style.BackColor = Color.White;
			}

		}

		private void comboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (this.dataGridView1.CurrentCell.OwningColumn.Name == columnTypeName)
			{
				string str = ((ComboBox)sender).Text.ToString();
				//绑定第二个COMBOX  
				DataTable dt = new DataTable();
				dt.Columns.Add(new DataColumn("id"));
				dt.Columns.Add(new DataColumn("text"));

				this.dataGridView1.CurrentCell.Value = str;

				if (str == Type.MC_MoveAbsolute.ToString() ||
					str == Type.MC_Relative.ToString())
                {
					if(str == Type.MC_MoveAbsolute.ToString())
                    {
						//位置
						setCellValue(columnPosName, "0", false);
						//距离
						setCellValue(columnDisName, "", true);
					}
					else if(str == Type.MC_Relative.ToString())
                    {
						//位置
						setCellValue(columnPosName, "", true);
						//距离
						setCellValue(columnDisName, "0", false);
					}

					//速度
					setCellValue(columnSpeedName, "0");
					//加速度
					setCellValue(columnAccName, "1");
					//减速度
					setCellValue(columnDecName, "1");
					//Jerk
					setCellValue(columnJerkName, "0");
					//下一步
					setCellValue(columnNextStepName, "Done", false);
					//事件变量
					setCellValue(columnEventVarName, "", true);
					//延时时间
					setCellValue(columnDelayName, "0");
					setCellValue(columnNoteName, "", false);
				}
				else if(str == Type.MC_Halt.ToString())
                {
					setCellValue(columnPosName, "", true);
					setCellValue(columnDisName, "", true);
					setCellValue(columnSpeedName, "", true);
					setCellValue(columnAccName, "", true);
					//减速度
					setCellValue(columnDecName, "1");
					//Jerk
					setCellValue(columnJerkName, "0");
					//下一步
					setCellValue(columnNextStepName, "Done", false);
					//事件变量
					setCellValue(columnEventVarName, "", true);
					//延时时间
					setCellValue(columnDelayName, "0");
					//note
					setCellValue(columnNoteName, "0", false);
				}
				else if(str == Type.MC_SetPosition.ToString())
                {
                    setCellValue(columnPosName, "0", false);
                    setCellValue(columnDisName, "", true);
                    setCellValue(columnSpeedName, "", true);
                    setCellValue(columnAccName, "", true);
                    setCellValue(columnDecName, "", true);
                    setCellValue(columnJerkName, "", true);
					setCellValue(columnNextStepName, "Done", false);
					setCellValue(columnEventVarName, "", true);
					setCellValue(columnDelayName, "0", false);
					setCellValue(columnNoteName, "", false);
				}
				else if(str == Type.MC_MoveVelocity.ToString())
                {
					setCellValue(columnPosName, "", true);
					setCellValue(columnDisName, "", true);
					setCellValue(columnSpeedName, "0", false);
					setCellValue(columnAccName, "1", false);
					setCellValue(columnDecName, "1", false);
					setCellValue(columnJerkName, "0", false);
					setCellValue(columnNextStepName, "In velocity");
					setCellValue(columnEventVarName, "", true);
					setCellValue(columnDelayName, "0", false);
					setCellValue(columnNoteName, "", false);
				}

				string tmp = "";
				if (str == "None")
                {
					setCellValue(columnPosName, "", true);
					setCellValue(columnDisName, "", true);
					setCellValue(columnSpeedName, "", true);
					setCellValue(columnAccName, "", true);
					setCellValue(columnDecName, "", true);
					setCellValue(columnJerkName, "", true);
					setCellValue(columnNextStepName, "", true);
					setCellValue(columnEventVarName, "", true);
					setCellValue(columnDelayName, "", true);
					setCellValue(columnNoteName, "", true);

					DataRow dr = dt.NewRow();
					dr[0] = str;
					dr[1] = str;
					dt.Rows.Add(dr);

					tmp = str;
				}
                else if (str == Type.MC_MoveVelocity.ToString())
                {
                    foreach (var step in noneListVel)
                    {
                        if (step == "Probe input event")
                        {
                            continue;
                        }
                        DataRow dr = dt.NewRow();
                        dr[0] = step;
                        dr[1] = step;
                        dt.Rows.Add(dr);
                    }
                }
                else
				{
					foreach (var step in noneList)
					{
                        DataRow dr = dt.NewRow();
                        dr[0] = step;
                        dr[1] = step;
                        dt.Rows.Add(dr);
                    }
				}


				//((DataGridViewComboBoxCell)this.dataGridView1.CurrentRow.Cells[columnNextStepName]).DataSource = dt;
				//((DataGridViewComboBoxCell)this.dataGridView1.CurrentRow.Cells[columnNextStepName]).DisplayMember = "text";
				//((DataGridViewComboBoxCell)this.dataGridView1.CurrentRow.Cells[columnNextStepName]).ValueMember = "id";
				////((ComboBox)sender).SelectedIndexChanged -= new EventHandler(comboBox_SelectedIndexChanged);
				//这里比较重要
				((ComboBox)sender).Leave += new EventHandler(combox_Leave);
				//DataGridViewComboBoxCell cell = ((DataGridViewComboBoxCell)this.dataGridView1.CurrentRow.Cells[columnNextStepName]);
				//if(cell.Items.Count > 0)
    //            {//very important
				//	cell.Value = (cell.Items[0] as DataRowView)["id"].ToString();
    //            }


				DataGridViewCell normalCell = ((DataGridViewCell)this.dataGridView1.CurrentRow.Cells[columnStepName]);
				normalCell.ReadOnly = true;

			}
			else if(this.dataGridView1.CurrentCell.OwningColumn.Name == columnNextStepName)
            {
				//下一步
				string str = ((ComboBox)sender).Text.ToString();
				this.dataGridView1.CurrentCell.Value = str;
				if (str == "Done")
                {
					setCellValue(columnEventVarName, "", true);
					setCellValue(columnDelayName, "0", false);
				}
				else if(str == "Blending previous" || str == "Blending next")
                {
                    setCellValue(columnEventVarName, "", true);
                    setCellValue(columnDelayName, "", true);
                }
				else if(str == "Probe input event" || str == "SW event")
                {
					setCellValue(columnEventVarName, "", false);
					setCellValue(columnDelayName, "0", false);
                }
				else if(str == "Delay")
                {
					setCellValue(columnEventVarName, "", true);
					setCellValue(columnDelayName, "0", false);
                }
				else if(str == "In velocity")
                {
					setCellValue(columnEventVarName, "", true);
					setCellValue(columnDelayName, "0", false);
				}


				////这里比较重要
				((ComboBox)sender).Leave += new EventHandler(combox_Leave);
			}
		}

		public void combox_Leave(object sender, EventArgs e)
		{
			ComboBox combox = sender as ComboBox;
			//var panel = combox.Parent as Panel;
			//panel.Controls.Clear();

			//做完处理，须撤销动态事件
			combox.SelectedIndexChanged -= new EventHandler(comboBox_SelectedIndexChanged);
			((ComboBox)sender).Leave -= new EventHandler(combox_Leave);
			((ComboBox)sender).KeyPress -= new KeyPressEventHandler(comboBox_KeyPress);
		}

		private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

		}

		private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
		{
			if(this.dataGridView1.CurrentCell == null)
            {
				return;
            }

			if (this.dataGridView1.CurrentCell.OwningColumn.Name == columnDisName ||
				this.dataGridView1.CurrentCell.OwningColumn.Name == columnPosName ||
				this.dataGridView1.CurrentCell.OwningColumn.Name == columnSpeedName ||
				this.dataGridView1.CurrentCell.OwningColumn.Name == columnAccName ||
				this.dataGridView1.CurrentCell.OwningColumn.Name == columnDecName ||
				this.dataGridView1.CurrentCell.OwningColumn.Name == columnJerkName)
			{
				DataGridViewTextBoxCell cell = ((DataGridViewTextBoxCell)this.dataGridView1.CurrentRow.Cells[this.dataGridView1.CurrentCell.OwningColumn.Name]);
				var value = cell.FormattedValue.ToString();
				double res = 0;
				if(value.EndsWith(@"."))
                {
					cell.Value = res;
					MessageBox.Show("输入格式错误!");
					

					return;
				}

				//^([1-9][0-9]*)+(.[0-9]{1,2})?$
				//System.Text.RegularExpressions.Regex rg = new System.Text.RegularExpressions.Regex(@"^([1-9][0-9]*)+(.[0-9]{1,2})?$");

				bool flag = double.TryParse(value, out res);
				if(!flag)
                {
					cell.Value = res;
					MessageBox.Show("输入格式错误!");
				}
				else
                {
					cell.Value = res;
				}

			}
			else if(this.dataGridView1.CurrentCell.OwningColumn.Name == columnTypeName
				|| this.dataGridView1.CurrentCell.OwningColumn.Name == columnNextStepName)
            {
				if(p != null)
                {
					p.Controls.Clear();
				}

			}
			else if (this.dataGridView1.CurrentCell.OwningColumn.Index == noteColumn
				|| this.dataGridView1.CurrentCell.OwningColumn.Index == eventColumn)
            {
				if (p != null)
				{
					p.Controls.Clear();
					this.dataGridView1.CurrentCell.Value = richTextBox.Text;
				}
			}
		}

		private void dataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
			if(e.RowIndex < 0 || e.ColumnIndex < 0)
            {
				return;
            }



    //        DataGridViewComboBoxCell cell = ((DataGridViewComboBoxCell)this.dataGridView1.CurrentRow.Cells[columnNextStepName]);
            var cellType = this.dataGridView1.CurrentRow.Cells[columnTypeName].Value.ToString();
            if (cellType == "None")
            {
                if (dataGridView1.CurrentCell.OwningColumn.Name != columnTypeName)
                {
                    e.Cancel = true;
                }
                //
                //cellType.ReadOnly = false;
            }
            //        else if (cellType.FormattedValue.ToString() != "None")
            //        {
            //            //cell.DisplayMember = "test1";
            //        }




            //DataGridView dgv = (DataGridView)sender;
            ////判断是否可以编辑  
            //if (dgv.Columns[e.ColumnIndex].Name == columnTypeName /*&&
            //             !(bool)dgv["Column2", e.RowIndex].Value*/)
            //{
            //	//编辑不能  
            //	//e.Cancel = true;
            //}


            //var typeName = dgv[columnTypeName, e.RowIndex].EditedFormattedValue.ToString();


            //if (typeName == "None" && columnTypeName != (dgv.Columns[e.ColumnIndex].Name))
            //         {
            //	e.Cancel = true;
            //	var next = dgv.Columns["下一步"] as DataGridViewComboBoxColumn;
            //	next.Items.Clear();
            //	//DataGridViewComboBoxCell dd = (DataGridViewComboBoxCell)dataGridView1[e.ColumnIndex, e.RowIndex];
            //	//dd.ReadOnly = true;
            //}
            //else if(typeName != "None" &&columnTypeName != (dgv.Columns[e.ColumnIndex].Name))
            //         {
            //	var next = dgv.Columns["下一步"] as DataGridViewComboBoxColumn;
            //	next.Items.Clear();
            //	next.Items.Add("test");
            //}
        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {

		}

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_DataSourceChanged(object sender, EventArgs e)
        {
			//绑定事件DataBindingComplete 之后设置才有效果
			//dataGridView1.Columns[0].ReadOnly = true;
			//dataGridView1.Columns[3].ReadOnly = true;
			////背景设置灰色只读
			//dataGridView1.Columns[0].DefaultCellStyle.BackColor = Color.Lavender;
		}

		void setCellStaus(int row, int column, bool readOnly = false)
        {
			DataGridViewCell cell = dataGridView1.Rows[row].Cells[column] as DataGridViewCell;
			cell.ReadOnly = readOnly;
			if(readOnly)
            {
				cell.Style.BackColor = Color.Lavender;
			}
			else
            {
				cell.Style.BackColor = Color.White;
			}

		}

		void setTableCellStatus()
        {
			for (int i = 0; i < dataGridView1.RowCount; i++)
			{
				var str = dataGridView1.Rows[i].Cells[columnTypeName].Value.ToString();
				if (str == "None")
				{
					setCellStaus(i, stepColumn, true);

					DataGridViewCell cell = dataGridView1.Rows[i].Cells[columnStepName] as DataGridViewCell;
					cell.ReadOnly = true;
					cell.Style.BackColor = Color.Lavender;

					cell = dataGridView1.Rows[i].Cells[columnPosName] as DataGridViewCell;
					cell.ReadOnly = true;
					cell.Style.BackColor = Color.Lavender;

					cell = dataGridView1.Rows[i].Cells[columnDisName] as DataGridViewCell;
					cell.ReadOnly = true;
					cell.Style.BackColor = Color.Lavender;

					cell = dataGridView1.Rows[i].Cells[columnSpeedName] as DataGridViewCell;
					cell.ReadOnly = true;
					cell.Style.BackColor = Color.Lavender;

					cell = dataGridView1.Rows[i].Cells[columnAccName] as DataGridViewCell;
					cell.ReadOnly = true;
					cell.Style.BackColor = Color.Lavender;

					cell = dataGridView1.Rows[i].Cells[columnDecName] as DataGridViewCell;
					cell.ReadOnly = true;
					cell.Style.BackColor = Color.Lavender;

					cell = dataGridView1.Rows[i].Cells[columnJerkName] as DataGridViewCell;
					cell.ReadOnly = true;
					cell.Style.BackColor = Color.Lavender;

					cell = dataGridView1.Rows[i].Cells[columnNextStepName] as DataGridViewCell;
					cell.ReadOnly = true;
					cell.Style.BackColor = Color.Lavender;

					cell = dataGridView1.Rows[i].Cells[columnEventVarName] as DataGridViewCell;
					cell.ReadOnly = true;
					cell.Style.BackColor = Color.Lavender;

					cell = dataGridView1.Rows[i].Cells[columnDelayName] as DataGridViewCell;
					cell.ReadOnly = true;
					cell.Style.BackColor = Color.Lavender;

					cell = dataGridView1.Rows[i].Cells[columnNoteName] as DataGridViewCell;
					cell.ReadOnly = true;
					cell.Style.BackColor = Color.Lavender;
				}
				else if (str == Type.MC_MoveAbsolute.ToString()
					|| str == Type.MC_Relative.ToString())
				{
					setCellStaus(i, stepColumn, true);

					if (str == Type.MC_MoveAbsolute.ToString())
					{
						//位置
						setCellStaus(i, posColumn, false);

						//距离
						setCellStaus(i, disColumn, true);
					}
					else if (str == Type.MC_Relative.ToString())
					{
						//位置
						setCellStaus(i, posColumn, true);
						//距离
						setCellStaus(i, disColumn, false);
					}

					//速度
					setCellStaus(i, speedColumn, false);
					//加速度
					setCellStaus(i, accColumn, false);
					//减速度
					setCellStaus(i, decColumn, false);
					//Jerk
					setCellStaus(i, jerkColumn, false);
					//下一步
					setCellStaus(i, nextStepColumn, false);
					//事件变量
					setCellStaus(i, eventColumn, true);
					//延时时间
					setCellStaus(i, delayColumn, false);
					setCellStaus(i, noteColumn, false);

					str = dataGridView1.Rows[i].Cells[columnNextStepName].Value.ToString();
					if (str == "Blending previous")
					{
						setCellStaus(i, eventColumn, true);
						setCellStaus(i, delayColumn, true);
					}
				}
				else if (str == Type.MC_SetPosition.ToString())
				{
					setCellStaus(i, stepColumn, true);
					setCellStaus(i, posColumn, false);
					setCellStaus(i, disColumn, true);
					setCellStaus(i, speedColumn, true);
					setCellStaus(i, accColumn, true);
					setCellStaus(i, decColumn, true);
					setCellStaus(i, jerkColumn, true);
					setCellStaus(i, nextStepColumn, false);
					setCellStaus(i, eventColumn, true);
					setCellStaus(i, delayColumn, false);
					setCellStaus(i, noteColumn, false);
				}
				else if (str == Type.MC_MoveVelocity.ToString())
				{
					setCellStaus(i, stepColumn, true);
					setCellStaus(i, posColumn, true);
					setCellStaus(i, disColumn, true);
					setCellStaus(i, speedColumn, false);
					setCellStaus(i, accColumn, false);
					setCellStaus(i, decColumn, false);
					setCellStaus(i, jerkColumn, false);
					setCellStaus(i, nextStepColumn, false);
					setCellStaus(i, eventColumn, true);
					setCellStaus(i, delayColumn, false);
					setCellStaus(i, noteColumn, false);
				}
				else if (str == Type.MC_Halt.ToString())
				{
					setCellStaus(i, stepColumn, true);
					setCellStaus(i, posColumn, true);
					setCellStaus(i, disColumn, true);
					setCellStaus(i, speedColumn, true);
					setCellStaus(i, accColumn, true);
					//减速度
					setCellStaus(i, decColumn, false);
					//Jerk
					setCellStaus(i, jerkColumn, false);
					//下一步
					setCellStaus(i, nextStepColumn, false);
					//事件变量
					setCellStaus(i, eventColumn, true);
					//延时时间
					setCellStaus(i, delayColumn, false);
					//note
					setCellStaus(i, noteColumn, false);
				}
				else
				{

				}


			}
		}

        private void dataGridView1_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
			return;

			if(e.RowIndex < 0)
            {
				return;
            }
			//for (int i = 0; i < dataGridView1.RowCount; i++)
			{
				int i = e.RowIndex;
				var str = dataGridView1.Rows[i].Cells[columnTypeName].Value.ToString();
				if (str == "None")
				{
					setCellStaus(i, stepColumn, true);

					DataGridViewCell cell = dataGridView1.Rows[i].Cells[columnStepName] as DataGridViewCell;
					cell.ReadOnly = true;
					cell.Style.BackColor = Color.Lavender;

					cell = dataGridView1.Rows[i].Cells[columnPosName] as DataGridViewCell;
					cell.ReadOnly = true;
					cell.Style.BackColor = Color.Lavender;

					cell = dataGridView1.Rows[i].Cells[columnDisName] as DataGridViewCell;
					cell.ReadOnly = true;
					cell.Style.BackColor = Color.Lavender;

					cell = dataGridView1.Rows[i].Cells[columnSpeedName] as DataGridViewCell;
					cell.ReadOnly = true;
					cell.Style.BackColor = Color.Lavender;

					cell = dataGridView1.Rows[i].Cells[columnAccName] as DataGridViewCell;
					cell.ReadOnly = true;
					cell.Style.BackColor = Color.Lavender;

					cell = dataGridView1.Rows[i].Cells[columnDecName] as DataGridViewCell;
					cell.ReadOnly = true;
					cell.Style.BackColor = Color.Lavender;

					cell = dataGridView1.Rows[i].Cells[columnJerkName] as DataGridViewCell;
					cell.ReadOnly = true;
					cell.Style.BackColor = Color.Lavender;

					cell = dataGridView1.Rows[i].Cells[columnNextStepName] as DataGridViewCell;
					cell.ReadOnly = true;
					cell.Style.BackColor = Color.Lavender;

					cell = dataGridView1.Rows[i].Cells[columnEventVarName] as DataGridViewCell;
					cell.ReadOnly = true;
					cell.Style.BackColor = Color.Lavender;

					cell = dataGridView1.Rows[i].Cells[columnDelayName] as DataGridViewCell;
					cell.ReadOnly = true;
					cell.Style.BackColor = Color.Lavender;

					cell = dataGridView1.Rows[i].Cells[columnNoteName] as DataGridViewCell;
					cell.ReadOnly = true;
					cell.Style.BackColor = Color.Lavender;
				}
				else if(str == Type.MC_MoveAbsolute.ToString()
					|| str == Type.MC_Relative.ToString())
                {
					setCellStaus(i, stepColumn, true);

					if (str == Type.MC_MoveAbsolute.ToString())
					{
						//位置
						setCellStaus(i, posColumn, false);
						
						//距离
						setCellStaus(i, disColumn, true);
					}
					else if (str == Type.MC_Relative.ToString())
					{
						//位置
						setCellStaus(i, posColumn, true);
						//距离
						setCellStaus(i, disColumn, false);
					}

					//速度
					setCellStaus(i, speedColumn, false);
					//加速度
					setCellStaus(i, accColumn, false);
					//减速度
					setCellStaus(i, decColumn, false);
					//Jerk
					setCellStaus(i, jerkColumn, false);
					//下一步
					setCellStaus(i, nextStepColumn, false);
					//事件变量
					setCellStaus(i, eventColumn, true);
					//延时时间
					setCellStaus(i, delayColumn, false);
					setCellStaus(i, noteColumn, false);

					str = dataGridView1.Rows[i].Cells[columnNextStepName].Value.ToString();
					if (str == "Blending previous")
					{
						setCellStaus(i, eventColumn, true);
						setCellStaus(i, delayColumn, true);
					}
				}
				else if(str == Type.MC_SetPosition.ToString())
                {
					setCellStaus(i, stepColumn, true);
					setCellStaus(i, posColumn, false);
					setCellStaus(i, disColumn, true);
					setCellStaus(i, speedColumn, true);
					setCellStaus(i, accColumn, true);
					setCellStaus(i, decColumn, true);
					setCellStaus(i, jerkColumn, true);
					setCellStaus(i, nextStepColumn, false);
					setCellStaus(i, eventColumn, true);
					setCellStaus(i, delayColumn, false);
					setCellStaus(i, noteColumn, false);
				}
				else if(str == Type.MC_MoveVelocity.ToString())
                {
					setCellStaus(i, stepColumn, true);
					setCellStaus(i, posColumn, true);
					setCellStaus(i, disColumn, true);
					setCellStaus(i, speedColumn, false);
					setCellStaus(i, accColumn, false);
					setCellStaus(i, decColumn, false);
					setCellStaus(i, jerkColumn, false);
					setCellStaus(i, nextStepColumn, false);
					setCellStaus(i, eventColumn, true);
					setCellStaus(i, delayColumn, false);
					setCellStaus(i, noteColumn, false);
				}
				else if(str == Type.MC_Halt.ToString())
                {
					setCellStaus(i, stepColumn, true);
					setCellStaus(i, posColumn, true);
					setCellStaus(i, disColumn, true);
					setCellStaus(i, speedColumn, true);
					setCellStaus(i, accColumn, true);
					//减速度
					setCellStaus(i, decColumn, false);
					//Jerk
					setCellStaus(i, jerkColumn, false);
					//下一步
					setCellStaus(i, nextStepColumn, false);
					//事件变量
					setCellStaus(i, eventColumn, true);
					//延时时间
					setCellStaus(i, delayColumn, false);
					//note
					setCellStaus(i, noteColumn, false);
				}
				else
                {

                }


			}

		}

        private void button_valid_Click(object sender, EventArgs e)
        {
			if(!getDataFromUI())
            {
				return;
            }

			button_valid.Enabled = false;
			button_cancel.Enabled = false;
		}

        private void button_cancel_Click(object sender, EventArgs e)
        {
			refreshData();
			button_valid.Enabled = false;
			button_cancel.Enabled = false;
		}

		private bool getDataFromUI()
        {
			bool ret = true;
			int noneIndex = 0;
			for (int i = 0; i < dtData.Rows.Count; i++)
			{
				var dr = dtData.Rows[i];
				var type = dr[typeColumn].ToString();
				if (type == Type.None.ToString())
				{
					if (i + 1 < dtData.Rows.Count)
					{
						dr = dtData.Rows[i + 1];
						type = dr[typeColumn].ToString();
						if (type != Type.None.ToString())
						{
							ret = false;
						}
					}
				}

				if (!ret)
				{
					string str = "步骤之间不可以有空步骤!";
					utility.PrintError(str);

					return ret;
				}

			}

			var stepList = data.stepList;
			stepList.Clear();
			int row = 0;
			foreach (DataRow dr in dtData.Rows)
			{
				Step step = new Step();
				step.step = dr[columnStepName].ToString();
				step.type = dr[columnTypeName].ToString();
				step.pos = dr[columnPosName].ToString();
				step.dis = dr[columnDisName].ToString();
				step.speed = dr[columnSpeedName].ToString();
				step.acc = dr[columnAccName].ToString();
				step.dec = dr[columnDecName].ToString();
				step.jerk = dr[columnJerkName].ToString();
				step.nextStep = dr[columnNextStepName].ToString();
				step.eventVar = dr[columnEventVarName].ToString();
				step.delay = dr[delayColumn].ToString();
				step.note = dr[columnNoteName].ToString();

				stepList.Add(step);
			}

			return ret;
		}

		private void refreshData()
		{
			var stepList = data.stepList;
			dtData.Clear();
			foreach(var step in stepList)
            {
				DataRow drData = dtData.NewRow();
				drData[stepColumn] = step.step;
				drData[typeColumn] = step.type;
				drData[posColumn] = step.pos;
				drData[disColumn] = step.dis;
				drData[speedColumn] = step.speed;
				drData[accColumn] = step.acc;
				drData[decColumn] = step.dec;
				drData[jerkColumn] = step.jerk;
				drData[nextStepColumn] = step.nextStep;
				drData[eventColumn] = step.eventVar;
				drData[delayColumn] = step.delay;
				drData[noteColumn] = step.note;

				dtData.Rows.Add(drData);
			}

			this.dataGridView1.DataSource = dtData;

			setTableCellStatus();
		}

		private void UserControlCommandTable_Load(object sender, EventArgs e)
        {
			foreach (DataGridViewColumn column in dataGridView1.Columns)
			{
				column.SortMode = DataGridViewColumnSortMode.NotSortable;

				dataGridView1.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.False;  //设置列标题不换行
				dataGridView1.Columns[typeColumn].Width = 150;
				dataGridView1.Columns[nextStepColumn].Width = 150;
			}

			//禁止用户改变DataGridView1の所有行的行高
			dataGridView1.AllowUserToResizeRows = false;

			setTableCellStatus();
			button_valid.Enabled = false;
			button_cancel.Enabled = false;

			SetStyle(
					 ControlStyles.OptimizedDoubleBuffer
					 | ControlStyles.ResizeRedraw
					 | ControlStyles.Selectable
					 | ControlStyles.AllPaintingInWmPaint
					 | ControlStyles.UserPaint
					 | ControlStyles.SupportsTransparentBackColor,
					 true);
		}

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
			var row = e.RowIndex;
			var column = e.ColumnIndex;
			if(row < 0 || column < 0)
            {
				return;
            }

			var cell = dtData.Rows[row][column].ToString();
			var stepList = data.stepList;
			if(column == typeColumn)
            {
				if (cell != stepList[row].type)
                {
					button_valid.Enabled = true;
					button_cancel.Enabled = true;
				}
			}
			else if(column == posColumn)
            {
				if (cell != stepList[row].pos)
				{
					button_valid.Enabled = true;
					button_cancel.Enabled = true;
				}
			}



        }
    }
}
