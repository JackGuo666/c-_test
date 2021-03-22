using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LocalPLC.motion
{
    public partial class UserControlBackOrigin : UserControl
    {
        #region
        Axis data = null;
        TreeNode node_ = null;
        Dictionary<int, string> levelDic = new Dictionary<int, string>();
        enum TypeLevel { HIGH_LEVEL, LOW_LEVEL }
        #endregion

        void initBackOriginal()
        {
            LocalPLC.Base.xml.DataManageBase dataManage = null;
            LocalPLC.UserControl1.UC.getDataManager(ref dataManage);
            foreach (var di in dataManage.diList)
            {
                if (!di.used)
                {
                    comboBox_BackOriginal.Items.Add(di.channelName);
                    comboBox_ZPulseSignal.Items.Add(di.channelName);
                }
            }

            levelDic.Clear();
            levelDic.Add((int)TypeLevel.HIGH_LEVEL, "高电平有效");
            levelDic.Add((int)TypeLevel.LOW_LEVEL, "低电平有效");
            foreach (var level in levelDic)
            {
                comboBox_BackOriginalSelectLevel.Items.Add(level.Value);
            }
        }

        public UserControlBackOrigin(TreeNode node_)
        {
            InitializeComponent();


            initBackOriginal();
        }
    }
}
