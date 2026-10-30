using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Polycardio2
{
    public partial class FormResult : Form
    {
        List<List<double>> ResList;
        public FormResult(List<List<double>> reslist1, List<List<double>> reslist2, List<string> namelist)
        {
            InitializeComponent();
            tbResult.AppendText("First deriv (speed)" + Convert.ToChar(0xA));
            tbResult.AppendText("  " + Convert.ToChar(0xA));
            AppendDataFromList(reslist1, namelist);
            tbResult.AppendText("  " + Convert.ToChar(0xA));
            tbResult.AppendText("Second deriv (accel)" + Convert.ToChar(0xA));
            tbResult.AppendText("  " + Convert.ToChar(0xA));
            AppendDataFromList(reslist2, namelist);
        }

        private void AppendDataFromList(List<List<double>> reslist, List<string> names)
        {
            Char tab = Convert.ToChar(9);
            string ApexHeader = "T5-T6" + tab + "T6-T7" + tab + "T7-T8" + tab + "T8-T0" + tab + "T0-T1" + tab + "T1-T2" + tab + "T2-T3" + tab + "T3-T4" + tab + "T4-T5";
            string SphigmoHeader = "0-1" + tab + "1-2" + tab + "2-3" + tab + "3-4" + tab + "4-5" + tab + "5-6" + tab + "6-7" + tab + "7-0";
            string ReoHeader = "0-1" + tab + "1-2" + tab + "2-3" + tab + "3-4" + tab + "4-0";
            for (int k = 0; k < reslist.Count(); k++)
            {
                string s = "";
                if ((names[k] != ApexConstants.ECG1name) | (names[k] != ApexConstants.ECG2name))
                {
                    tbResult.AppendText(names[k] + Convert.ToChar(0xA));
                }
                if (reslist[k] != null)
                {
                    for (int i = 0; i < reslist[k].Count(); i++)
                    {
                        s += String.Format("{0:0.00}", Convert.ToDouble(reslist[k][i])) + Convert.ToChar(9);
                    }
                    if (names[k] == ApexConstants.Apexname)
                    {
                        tbResult.AppendText(ApexHeader + Convert.ToChar(0xA));
                    }
                    if ((names[k] == ApexConstants.Sphigmo1name) | (names[k] == ApexConstants.Sphigmo2name))
                    {
                        tbResult.AppendText(SphigmoHeader + Convert.ToChar(0xA));
                    }
                    if ((names[k] == ApexConstants.Reo1name) | (names[k] == ApexConstants.Reo2name))
                    {
                        tbResult.AppendText(ReoHeader + Convert.ToChar(0xA));
                    }
                    tbResult.AppendText(s + Convert.ToChar(0xA));
                    tbResult.AppendText("" + Convert.ToChar(0xA));
                }
            }
        }

    }
}
