using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LocalPLC.custom
{
    class RichTextBoxEx : RichTextBox
    {
        public RichTextBoxEx()
        {
            this.WordWrap = false;
            this.ScrollBars = RichTextBoxScrollBars.None;
  
            
            this.ScrollToCaret();
            this.MaxLength = 30;
        }
    }
}
