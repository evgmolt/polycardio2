namespace Polycardio2
{
    partial class FormRecordInfo
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            labDateTime = new Label();
            gbSex = new GroupBox();
            rbFemale = new RadioButton();
            rbMale = new RadioButton();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            butOk = new Button();
            butCancel = new Button();
            nudAge = new NumericUpDown();
            nudHeight = new NumericUpDown();
            nudWeight = new NumericUpDown();
            labLength = new Label();
            gbSex.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudAge).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudHeight).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudWeight).BeginInit();
            SuspendLayout();
            // 
            // labDateTime
            // 
            labDateTime.AutoSize = true;
            labDateTime.Location = new Point(26, 21);
            labDateTime.Margin = new Padding(4, 0, 4, 0);
            labDateTime.Name = "labDateTime";
            labDateTime.Size = new Size(73, 15);
            labDateTime.TabIndex = 0;
            labDateTime.Text = "labDateTime";
            // 
            // gbSex
            // 
            gbSex.Controls.Add(rbFemale);
            gbSex.Controls.Add(rbMale);
            gbSex.Location = new Point(24, 79);
            gbSex.Margin = new Padding(4, 3, 4, 3);
            gbSex.Name = "gbSex";
            gbSex.Padding = new Padding(4, 3, 4, 3);
            gbSex.Size = new Size(153, 48);
            gbSex.TabIndex = 1;
            gbSex.TabStop = false;
            gbSex.Text = "Sex";
            // 
            // rbFemale
            // 
            rbFemale.AutoSize = true;
            rbFemale.Location = new Point(77, 22);
            rbFemale.Margin = new Padding(4, 3, 4, 3);
            rbFemale.Name = "rbFemale";
            rbFemale.Size = new Size(63, 19);
            rbFemale.TabIndex = 1;
            rbFemale.TabStop = true;
            rbFemale.Text = "Female";
            rbFemale.UseVisualStyleBackColor = true;
            // 
            // rbMale
            // 
            rbMale.AutoSize = true;
            rbMale.Location = new Point(7, 22);
            rbMale.Margin = new Padding(4, 3, 4, 3);
            rbMale.Name = "rbMale";
            rbMale.Size = new Size(51, 19);
            rbMale.TabIndex = 0;
            rbMale.TabStop = true;
            rbMale.Text = "Male";
            rbMale.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(345, 99);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(28, 15);
            label2.TabIndex = 6;
            label2.Text = "Age";
            label2.Click += label2_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(345, 132);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new Size(43, 15);
            label3.TabIndex = 7;
            label3.Text = "Height";
            label3.Click += label3_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(345, 165);
            label4.Margin = new Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new Size(45, 15);
            label4.TabIndex = 8;
            label4.Text = "Weight";
            label4.Click += label4_Click;
            // 
            // butOk
            // 
            butOk.DialogResult = DialogResult.OK;
            butOk.Location = new Point(302, 216);
            butOk.Margin = new Padding(4, 3, 4, 3);
            butOk.Name = "butOk";
            butOk.Size = new Size(88, 27);
            butOk.TabIndex = 5;
            butOk.Text = "Ok";
            butOk.UseVisualStyleBackColor = true;
            butOk.Click += butOk_Click;
            // 
            // butCancel
            // 
            butCancel.DialogResult = DialogResult.Cancel;
            butCancel.Location = new Point(407, 216);
            butCancel.Margin = new Padding(4, 3, 4, 3);
            butCancel.Name = "butCancel";
            butCancel.Size = new Size(88, 27);
            butCancel.TabIndex = 6;
            butCancel.Text = "Cancel";
            butCancel.UseVisualStyleBackColor = true;
            // 
            // nudAge
            // 
            nudAge.Location = new Point(419, 97);
            nudAge.Margin = new Padding(4, 3, 4, 3);
            nudAge.Name = "nudAge";
            nudAge.Size = new Size(76, 23);
            nudAge.TabIndex = 2;
            nudAge.ValueChanged += nudAge_ValueChanged;
            // 
            // nudHeight
            // 
            nudHeight.Location = new Point(419, 129);
            nudHeight.Margin = new Padding(4, 3, 4, 3);
            nudHeight.Maximum = new decimal(new int[] { 250, 0, 0, 0 });
            nudHeight.Name = "nudHeight";
            nudHeight.Size = new Size(76, 23);
            nudHeight.TabIndex = 3;
            nudHeight.ValueChanged += nudHeight_ValueChanged;
            // 
            // nudWeight
            // 
            nudWeight.Location = new Point(419, 163);
            nudWeight.Margin = new Padding(4, 3, 4, 3);
            nudWeight.Maximum = new decimal(new int[] { 200, 0, 0, 0 });
            nudWeight.Name = "nudWeight";
            nudWeight.Size = new Size(76, 23);
            nudWeight.TabIndex = 4;
            nudWeight.ValueChanged += nudWeight_ValueChanged;
            // 
            // labLength
            // 
            labLength.AutoSize = true;
            labLength.Location = new Point(26, 46);
            labLength.Margin = new Padding(4, 0, 4, 0);
            labLength.Name = "labLength";
            labLength.Size = new Size(60, 15);
            labLength.TabIndex = 18;
            labLength.Text = "labLength";
            // 
            // FormRecordInfo
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = butCancel;
            ClientSize = new Size(537, 267);
            Controls.Add(labLength);
            Controls.Add(nudWeight);
            Controls.Add(nudHeight);
            Controls.Add(nudAge);
            Controls.Add(butCancel);
            Controls.Add(butOk);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(gbSex);
            Controls.Add(labDateTime);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            KeyPreview = true;
            Margin = new Padding(4, 3, 4, 3);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FormRecordInfo";
            Text = "Record information";
            gbSex.ResumeLayout(false);
            gbSex.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudAge).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudHeight).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudWeight).EndInit();
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labDateTime;
        private System.Windows.Forms.GroupBox gbSex;
        private System.Windows.Forms.RadioButton rbFemale;
        private System.Windows.Forms.RadioButton rbMale;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button butOk;
        private System.Windows.Forms.Button butCancel;
        private System.Windows.Forms.NumericUpDown nudAge;
        private System.Windows.Forms.NumericUpDown nudHeight;
        private System.Windows.Forms.NumericUpDown nudWeight;
        private System.Windows.Forms.Label labLength;
    }
}