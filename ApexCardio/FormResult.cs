namespace Polycardio2
{
    public partial class FormResult : Form
    {
        string resultFileName;
        public FormResult(RecordInfo ri, CaviMeasurementResult caviLeft, CaviMeasurementResult caviRight)
        {
            InitializeComponent();
            resultFileName = Path.GetFileNameWithoutExtension(ri.FileName) + "_result.txt";
            tbInfo.AppendText("Data file : " + ri.FileName + "\r\n");
            tbInfo.AppendText("Result file : " + resultFileName + "\r\n");
            tbInfo.AppendText("Sex : " + ri.Data.Sex.ToString() + "\r\n");
            tbInfo.AppendText("Age : " + ri.Data.Age.ToString() + "\r\n");
            tbInfo.AppendText("Height : " + ri.Data.Height.ToString() + "\r\n");
            AddCaviToTextBox(tbResultLeft, "Left", caviLeft);
            AddCaviToTextBox(tbResultRight, "Right", caviRight);
        }

        private void AddCaviToTextBox(TextBox textBox, string title, CaviMeasurementResult cavi)
        {
            textBox.AppendText(title + "\r\n");
            textBox.AppendText("\r\n");
            textBox.AppendText("CAVI : " + cavi.Cavi.ToString("F2") + "\r\n");
            textBox.AppendText("Pwv : " + cavi.PwvMetersPerSec.ToString("F2") + "\r\n");

        }

        private void butSave_Click(object sender, EventArgs e)
        {
            if (File.Exists(resultFileName)) File.Delete(resultFileName);
            File.AppendAllText(resultFileName, tbInfo.Text);
            File.AppendAllText(resultFileName, ("\r\n"));
            File.AppendAllText(resultFileName, tbResultLeft.Text);
            File.AppendAllText(resultFileName, ("\r\n"));
            File.AppendAllText(resultFileName, tbResultRight.Text);
        }
    }
}
