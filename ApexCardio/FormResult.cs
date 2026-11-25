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

            AbiMeasurementResult abi = CalculateAbi(ri.Data.ABP1[0], ri.Data.ABP3[0], ri.Data.ABP2[0], ri.Data.ABP4[0]);
            tbResultLeft.AppendText("ABI : " + abi.LeftAbi.ToString());
            tbResultRight.AppendText("ABI : " + abi.RightAbi.ToString());
        }

        /// <summary>
        /// Вычисляет левый и правый лодыжечно-плечевые индексы (ABI) по значениям систолического АД.
        /// </summary>
        /// <param name="leftArmSbp">Систолическое АД на левой руке (например, data.NIBP1_Syst)</param>
        /// <param name="rightArmSbp">Систолическое АД на правой руке (например, data.NIBP2_Syst)</param>
        /// <param name="leftLegSbp">Систолическое АД на левой лодыжке (например, data.NIBP3_Syst)</param>
        /// <param name="rightLegSbp">Систолическое АД на правой лодыжке (например, data.NIBP4_Syst)</param>
        public static AbiMeasurementResult CalculateAbi(
            double leftArmSbp,
            double rightArmSbp,
            double leftLegSbp,
            double rightLegSbp)
        {
            // По клиническому стандарту в знаменатель берётся НАИВЫСШЕЕ систолическое давление из двух рук
            double maxArmSbp = Math.Max(leftArmSbp, rightArmSbp);

            if (maxArmSbp <= 0)
                return new AbiMeasurementResult();

            double leftAbi = leftLegSbp > 0 ? Math.Round(leftLegSbp / maxArmSbp, 2) : 0;
            double rightAbi = rightLegSbp > 0 ? Math.Round(rightLegSbp / maxArmSbp, 2) : 0;

            return new AbiMeasurementResult
            {
                LeftAbi = leftAbi,
                RightAbi = rightAbi,
                MaxArmSystolic = maxArmSbp
            };
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
