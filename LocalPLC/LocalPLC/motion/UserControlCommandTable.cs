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
		enum NextStep {Done, Blending_previous, Probe_input_event, SW_event, Delay}
		List<string> typeList = new List<string>();
		List<string> noneList = new List<string>();

		Dictionary<int, string> nextStepDic = new Dictionary<int, string>();

		int nextColumn = 0;
		//int typeColumn = 1;
		int disColumn = 1;
		int speedColumn = 2;
		int accColumn = 3;
		int decColumn = 4;
		int jerkColumn = 5;
		int eventColumn = 6;
		int delayColumn = 7;
		int noteColumn = 8;

		int typeColumn = 1;
		int nextStepColumn = 6;

		int defaultRows = 16;
		#endregion

		#region
		string columnStepName = "步骤";
		string columnTypeName = "类型";
		string columnNextStepName = "下一步";
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
			this.dataGridView1.DataError += delegate (object sender, DataGridViewDataErrorEventArgs e) { };

			typeList.Clear();
			typeList.Add(Type.None.ToString());
			typeList.Add(Type.MC_MoveAbsolute.ToString());
			typeList.Add(Type.MC_Relative.ToString());
			typeList.Add(Type.MC_SetPosition.ToString());
			typeList.Add(Type.MC_MoveVelocity.ToString());
			typeList.Add(Type.MC_Halt.ToString());

			noneList.Clear();
			noneList.Add("Done");

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

        private void bindData()
        {
			dtData = new DataTable();
            dtData.Columns.Add(columnStepName, typeof(string));
            //类型
            //dtData.Columns.Add("类型", typeof(string));
            dtData.Columns.Add("距离", typeof(string));
            dtData.Columns.Add("速度", typeof(float));
            dtData.Columns.Add("加速度", typeof(int));
            dtData.Columns.Add("减速度", typeof(float));
            dtData.Columns.Add("Jerk", typeof(string));
            //dtData.Columns.Add("下一步", typeof(string));
            dtData.Columns.Add("事件变量", typeof(string));
            dtData.Columns.Add("延迟时间(ms)", typeof(float));
            dtData.Columns.Add("备注", typeof(string));

            this.dataGridView1.DataSource = dtData;

			

			for (int i = 0; i < 16; i++)
			{
				DataRow drData;
				drData = dtData.NewRow();
				drData[nextColumn] = i.ToString();
				drData[disColumn] = i.ToString();
				drData[speedColumn] = i.ToString();
				drData[accColumn] = i.ToString();
				drData[decColumn] = i.ToString();
				drData[jerkColumn] = i.ToString();
				drData[eventColumn] = i.ToString();
				drData[delayColumn] = i.ToString();
				drData[noteColumn] = i.ToString();


				dtData.Rows.Add(drData);
				//((DataGridViewComboBoxColumn)dataGridView1.Columns[columnTypeName]).DefaultCellStyle.NullValue = "None";
			}

			this.dataGridView1.DataSource = dtData;

			//插入类型combobox列
			DataGridViewComboBoxColumn comboBoxColumn = new DataGridViewComboBoxColumn();
			comboBoxColumn.DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox;
			comboBoxColumn.Name = columnTypeName;
			dgvCombobox(ref comboBoxColumn, typeList);
			comboBoxColumn.DefaultCellStyle.NullValue = "None";


			int columnIndex = typeColumn;
			if (dataGridView1.Columns[columnTypeName] == null)
			{
				dataGridView1.Columns.Insert(columnIndex, comboBoxColumn);
			}

			comboBoxColumn = new DataGridViewComboBoxColumn();
			comboBoxColumn.DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox;
			comboBoxColumn.Name = "下一步";
			dgvCombobox(ref comboBoxColumn, noneList);
			comboBoxColumn.DefaultCellStyle.NullValue = "None";

			if (dataGridView1.Columns["下一步"] == null)
			{
				dataGridView1.Columns.Insert(nextStepColumn, comboBoxColumn);
			}


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


		private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
			if (this.dataGridView1.CurrentCell.OwningColumn.Name == columnTypeName)
			{
				((ComboBox)e.Control).SelectedIndexChanged +=
									new EventHandler(comboBox_SelectedIndexChanged);
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

				string tmp = "";
				if (str == "None")
                {

					DataRow dr = dt.NewRow();
					dr[0] = str;
					dr[1] = str;
					dt.Rows.Add(dr);

					tmp = str;
				}
				else
                {
					for (int i = 0; i < 20; i++)
					{
						if (i == 0)
						{
							tmp = str + i;
						}

						DataRow dr = dt.NewRow();
						dr[0] = str + i;
						dr[1] = str + i;
						dt.Rows.Add(dr);
					}
				}


				((DataGridViewComboBoxCell)this.dataGridView1.CurrentRow.Cells[columnNextStepName]).DataSource = dt;
				((DataGridViewComboBoxCell)this.dataGridView1.CurrentRow.Cells[columnNextStepName]).DisplayMember = "text";
				((DataGridViewComboBoxCell)this.dataGridView1.CurrentRow.Cells[columnNextStepName]).ValueMember = "id";
				//((ComboBox)sender).SelectedIndexChanged -= new EventHandler(comboBox_SelectedIndexChanged);
				//这里比较重要
				((ComboBox)sender).Leave += new EventHandler(combox_Leave);
				DataGridViewComboBoxCell cell = ((DataGridViewComboBoxCell)this.dataGridView1.CurrentRow.Cells[columnNextStepName]);
				if(cell.Items.Count > 0)
                {
					cell.Value = (cell.Items[0] as DataRowView)["id"].ToString();
                }


				DataGridViewCell normalCell = ((DataGridViewCell)this.dataGridView1.CurrentRow.Cells[columnStepName]);
				normalCell.ReadOnly = true;
			}
		}

		public void combox_Leave(object sender, EventArgs e)
		{
			ComboBox combox = sender as ComboBox;
			//做完处理，须撤销动态事件
			combox.SelectedIndexChanged -= new EventHandler(comboBox_SelectedIndexChanged);
			((ComboBox)sender).Leave -= new EventHandler(combox_Leave);
		}

		private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

		}

        private void dataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
			if(e.RowIndex < 0 || e.ColumnIndex < 0)
            {
				return;
            }



            DataGridViewComboBoxCell cell = ((DataGridViewComboBoxCell)this.dataGridView1.CurrentRow.Cells[columnNextStepName]);
            DataGridViewComboBoxCell cellType = ((DataGridViewComboBoxCell)this.dataGridView1.CurrentRow.Cells[columnTypeName]);
            if (cellType.FormattedValue.ToString() == "None")
            {
				if(dataGridView1.CurrentCell.OwningColumn.Name != columnTypeName)
                {
					e.Cancel = true;
                }
                //
                //cellType.ReadOnly = false;
            }
            else if (cellType.FormattedValue.ToString() != "None")
            {
                //cell.DisplayMember = "test1";
            }




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
    }
}
