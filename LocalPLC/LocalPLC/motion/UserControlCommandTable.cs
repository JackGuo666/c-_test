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
        #region 
        //变量
        DataTable dtData = null;

        #endregion
        public UserControlCommandTable()
        {
            InitializeComponent();
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
            dtData.Columns.Add("步骤", typeof(string));
            //类型
            dtData.Columns.Add("类型", typeof(string));
            dtData.Columns.Add("距离", typeof(string));
            dtData.Columns.Add("速度", typeof(float));
            dtData.Columns.Add("加速度", typeof(int));
            dtData.Columns.Add("减速度", typeof(float));
            dtData.Columns.Add("Jerk", typeof(string));
            dtData.Columns.Add("下一步", typeof(string));
            dtData.Columns.Add("事件变量", typeof(string));
            dtData.Columns.Add("延迟时间(ms)", typeof(float));
            dtData.Columns.Add("备注", typeof(string));

            this.dataGridView1.DataSource = dtData;
        }
    }
}
