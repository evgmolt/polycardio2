using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Polycardio2
{
    public partial class FormRecordInfo : Form
    {
        private string maleSt = "Male";
        private string femaleSt = "Female";
        private readonly RecordInfo recInfo;
        public FormRecordInfo(RecordInfo recInfoArg)
        {
            InitializeComponent();
            recInfo = recInfoArg;

            labDateTime.Text = recInfo.Data.Date.ToString();
            labLength.Text = "Length : "+recInfo.Data.Length.ToString();
            tbName.Text = recInfo.Data.Name;
            if (recInfoArg.Data.Sex == maleSt) rbMale.Checked = true;
            if (recInfoArg.Data.Sex == femaleSt) rbFemale.Checked = true;
            nudAge.Value = recInfo.Data.Age;
            nudHeight.Value = recInfo.Data.Height;
            nudWeight.Value = recInfo.Data.Weight;
            tbComment.Text = recInfo.Data.Note;
        }

        private void butOk_Click(object sender, EventArgs e)
        {
            recInfo.Data.Name = tbName.Text;
            recInfo.Data.Sex = rbMale.Checked ? maleSt : femaleSt;
            recInfo.Data.Age = nudAge.Value;
            recInfo.Data.Height = nudHeight.Value;
            recInfo.Data.Weight = nudWeight.Value;
            recInfo.Data.Note = tbComment.Text;
        }

        public RecordInfo GetDialogData()
        {
            return recInfo;
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void nudAge_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void nudHeight_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void nudWeight_ValueChanged(object sender, EventArgs e)
        {

        }

    }
}
