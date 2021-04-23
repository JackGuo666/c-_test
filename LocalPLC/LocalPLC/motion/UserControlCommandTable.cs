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
		Dictionary<int, string> typeDic = new Dictionary<int, string>();

		Dictionary<int, string> nextStepDic = new Dictionary<int, string>();

		int typeColumn = 1;

		int nextStepColumn = 6;

		int defaultRows = 16;
		#endregion

		#region
		string columnStepName = "步骤";
		string columnTypeName = "类型";
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


			typeDic.Clear();
			typeDic.Add((int)Type.None, Type.None.ToString());
			typeDic.Add((int)Type.MC_MoveAbsolute, Type.MC_MoveAbsolute.ToString());
			typeDic.Add((int)Type.MC_Relative, Type.MC_Relative.ToString());
			typeDic.Add((int)Type.MC_SetPosition, Type.MC_SetPosition.ToString());
			typeDic.Add((int)Type.MC_MoveVelocity, Type.MC_MoveVelocity.ToString());
			typeDic.Add((int)Type.MC_Halt, Type.MC_Halt.ToString());

			nextStepDic.Clear();
			nextStepDic.Add((int)NextStep.Done, "Done");

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

			//插入combobox列
			DataGridViewComboBoxColumn comboBoxColumn = new DataGridViewComboBoxColumn();
			comboBoxColumn.DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox;
			comboBoxColumn.Name = columnTypeName;
			foreach(var elem in typeDic)
            {
				//ComboboxItem item = new ComboboxItem();
				//item.Value = elem.Key;
				comboBoxColumn.Items.Add(elem.Value);
			}



			int columnIndex = typeColumn;
			if (dataGridView1.Columns[columnTypeName] == null)
			{
				dataGridView1.Columns.Insert(columnIndex, comboBoxColumn);
			}

			comboBoxColumn = new DataGridViewComboBoxColumn();
			comboBoxColumn.DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox;
			comboBoxColumn.Name = "下一步";
			foreach(var elem in nextStepDic)
            {
				comboBoxColumn.Items.Add(elem.Value);
			}
			if (dataGridView1.Columns["下一步"] == null)
			{
				dataGridView1.Columns.Insert(nextStepColumn, comboBoxColumn);
			}

			for (int i = 0; i < 16; i++)
			{
				DataRow drData;
				drData = dtData.NewRow();
				drData[columnStepName] = i.ToString();
				dtData.Rows.Add(drData);
				((DataGridViewComboBoxColumn)dataGridView1.Columns[columnTypeName]).DefaultCellStyle.NullValue = "None";
			}

			this.dataGridView1.DataSource = dtData;


		}

        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {

		}

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
			if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
				DataGridViewComboBoxColumn combo = dataGridView1.Columns[e.ColumnIndex] as DataGridViewComboBoxColumn;
				if (combo != null)  //如果该列是ComboBox列
				{
					dataGridView1.BeginEdit(false); //结束该列的编辑状态
					DataGridViewComboBoxEditingControl comboEdite = dataGridView1.EditingControl as DataGridViewComboBoxEditingControl;
					if (comboEdite != null)
					{
						comboEdite.DroppedDown = true; //展现下拉列表
					}
				}
			}
		}

        private void dataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
			if(e.RowIndex < 0 || e.ColumnIndex < 0)
            {
				return;
            }

			DataGridView dgv = (DataGridView)sender;
            //判断是否可以编辑  
            if (dgv.Columns[e.ColumnIndex].Name == columnTypeName /*&&
                !(bool)dgv["Column2", e.RowIndex].Value*/)
            {
				//编辑不能  
				//e.Cancel = true;
			}
			

			var typeName = dgv[columnTypeName, e.RowIndex].EditedFormattedValue.ToString();


			if (typeName == "None" && columnTypeName != (dgv.Columns[e.ColumnIndex].Name))
            {
				e.Cancel = true;
				var next = dgv.Columns["下一步"] as DataGridViewComboBoxColumn;
				next.Items.Clear();
				//DataGridViewComboBoxCell dd = (DataGridViewComboBoxCell)dataGridView1[e.ColumnIndex, e.RowIndex];
				//dd.ReadOnly = true;
			}
			else if(typeName != "None" &&columnTypeName != (dgv.Columns[e.ColumnIndex].Name))
            {
				var next = dgv.Columns["下一步"] as DataGridViewComboBoxColumn;
				next.Items.Clear();
				next.Items.Add("test");
			}
		}

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
			int index = e.RowIndex;
			//实现单击一次显示下拉列表框
			if (dataGridView1.Columns[e.ColumnIndex] is DataGridViewComboBoxColumn && e.RowIndex != -1)
			{
				//SendKeys.Send("{F4}");
				SendKeys.SendWait("{F4}");
			}

		}

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
