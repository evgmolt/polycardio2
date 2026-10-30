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
    public partial class FormConfig : Form
    {
        private readonly ApexConfig cfg;
        CheckBox[] CBArr;
        public FormConfig(ApexConfig cfgarg)
        {
            InitializeComponent();
            cfg = cfgarg;
            CBArr = new CheckBox[ApexConstants.NumOfChannels];
            CBArr[0] = checkBox1;
            CBArr[1] = checkBox2;
            CBArr[2] = checkBox3;
            CBArr[3] = checkBox4;
            CBArr[4] = checkBox5;
            CBArr[5] = checkBox6;
            CBArr[6] = checkBox7;
            CBArr[7] = checkBox8;
            CBArr[8] = checkBox9;
            CBArr[9] = checkBox10;
            CBArr[10] = checkBox11;
            for (int i = 0; i < ApexConstants.NumOfChannels; i++)
            {
                CBArr[i].Checked = cfg.VisibleGraphs[i];
            }
            numUDRecLen.Value = cfg.RecordLength;
            numUDStartDelay.Value = cfg.StartDelay;
            CB_Filter.Checked = cfg.FilterOn;
        }

        public ApexConfig GetDialogData()
        {
            return cfg;
        }

        private void butOk_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < ApexConstants.NumOfChannels; i++)
            {
                cfg.VisibleGraphs[i] = CBArr[i].Checked;
            }
            cfg.RecordLength = (int)numUDRecLen.Value;
            cfg.StartDelay = (int)numUDStartDelay.Value;
            cfg.FilterOn = CB_Filter.Checked;
        }

    }
}
