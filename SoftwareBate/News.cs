using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cute_debug
{
    public class News
    {
        public static void NewsText(string text,string Moth)
        {
            System.Windows.Forms.MessageBox.Show(text,Moth,System.Windows.Forms.MessageBoxButtons.OK,System.Windows.Forms.MessageBoxIcon.Warning,System.Windows.Forms.MessageBoxDefaultButton.Button1);
        }
    }
}
