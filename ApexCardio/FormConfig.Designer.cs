namespace Polycardio2
{
    partial class FormConfig
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
            butOk = new Button();
            butCancel = new Button();
            checkBox1 = new CheckBox();
            checkBox2 = new CheckBox();
            checkBox3 = new CheckBox();
            checkBox4 = new CheckBox();
            checkBox5 = new CheckBox();
            checkBox6 = new CheckBox();
            checkBox9 = new CheckBox();
            label1 = new Label();
            label2 = new Label();
            numUDRecLen = new NumericUpDown();
            folderBrowserDialog1 = new FolderBrowserDialog();
            CB_Filter = new CheckBox();
            label4 = new Label();
            numUDStartDelay = new NumericUpDown();
            checkBox8 = new CheckBox();
            checkBox7 = new CheckBox();
            checkBox10 = new CheckBox();
            checkBox11 = new CheckBox();
            ((System.ComponentModel.ISupportInitialize)numUDRecLen).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numUDStartDelay).BeginInit();
            SuspendLayout();
            // 
            // butOk
            // 
            butOk.DialogResult = DialogResult.OK;
            butOk.Location = new Point(482, 14);
            butOk.Margin = new Padding(4, 3, 4, 3);
            butOk.Name = "butOk";
            butOk.Size = new Size(88, 27);
            butOk.TabIndex = 0;
            butOk.Text = "OK";
            butOk.UseVisualStyleBackColor = true;
            butOk.Click += butOk_Click;
            // 
            // butCancel
            // 
            butCancel.DialogResult = DialogResult.Cancel;
            butCancel.Location = new Point(482, 58);
            butCancel.Margin = new Padding(4, 3, 4, 3);
            butCancel.Name = "butCancel";
            butCancel.Size = new Size(88, 27);
            butCancel.TabIndex = 1;
            butCancel.Text = "Cancel";
            butCancel.UseVisualStyleBackColor = true;
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Location = new Point(14, 35);
            checkBox1.Margin = new Padding(4, 3, 4, 3);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(57, 19);
            checkBox1.TabIndex = 2;
            checkBox1.Text = "ECG 1";
            checkBox1.UseVisualStyleBackColor = true;
            // 
            // checkBox2
            // 
            checkBox2.AutoSize = true;
            checkBox2.Location = new Point(14, 61);
            checkBox2.Margin = new Padding(4, 3, 4, 3);
            checkBox2.Name = "checkBox2";
            checkBox2.Size = new Size(57, 19);
            checkBox2.TabIndex = 3;
            checkBox2.Text = "ECG 2";
            checkBox2.UseVisualStyleBackColor = true;
            // 
            // checkBox3
            // 
            checkBox3.AutoSize = true;
            checkBox3.Location = new Point(14, 88);
            checkBox3.Margin = new Padding(4, 3, 4, 3);
            checkBox3.Name = "checkBox3";
            checkBox3.Size = new Size(83, 19);
            checkBox3.TabIndex = 4;
            checkBox3.Text = "Reogram 1";
            checkBox3.UseVisualStyleBackColor = true;
            // 
            // checkBox4
            // 
            checkBox4.AutoSize = true;
            checkBox4.Location = new Point(14, 114);
            checkBox4.Margin = new Padding(4, 3, 4, 3);
            checkBox4.Name = "checkBox4";
            checkBox4.Size = new Size(83, 19);
            checkBox4.TabIndex = 5;
            checkBox4.Text = "Reogram 2";
            checkBox4.UseVisualStyleBackColor = true;
            // 
            // checkBox5
            // 
            checkBox5.AutoSize = true;
            checkBox5.Location = new Point(14, 141);
            checkBox5.Margin = new Padding(4, 3, 4, 3);
            checkBox5.Name = "checkBox5";
            checkBox5.Size = new Size(111, 19);
            checkBox5.TabIndex = 6;
            checkBox5.Text = "Sphigmogram 1";
            checkBox5.UseVisualStyleBackColor = true;
            // 
            // checkBox6
            // 
            checkBox6.AutoSize = true;
            checkBox6.Location = new Point(14, 167);
            checkBox6.Margin = new Padding(4, 3, 4, 3);
            checkBox6.Name = "checkBox6";
            checkBox6.Size = new Size(111, 19);
            checkBox6.TabIndex = 7;
            checkBox6.Text = "Sphigmogram 2";
            checkBox6.UseVisualStyleBackColor = true;
            // 
            // checkBox9
            // 
            checkBox9.AutoSize = true;
            checkBox9.Location = new Point(14, 243);
            checkBox9.Margin = new Padding(4, 3, 4, 3);
            checkBox9.Name = "checkBox9";
            checkBox9.Size = new Size(117, 19);
            checkBox9.TabIndex = 8;
            checkBox9.Text = "Apex cardiogram";
            checkBox9.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(14, 10);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(35, 15);
            label1.TabIndex = 9;
            label1.Text = "View:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(395, 275);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(104, 15);
            label2.TabIndex = 10;
            label2.Text = "Record length, sec";
            // 
            // numUDRecLen
            // 
            numUDRecLen.Location = new Point(516, 272);
            numUDRecLen.Margin = new Padding(4, 3, 4, 3);
            numUDRecLen.Maximum = new decimal(new int[] { 20, 0, 0, 0 });
            numUDRecLen.Minimum = new decimal(new int[] { 3, 0, 0, 0 });
            numUDRecLen.Name = "numUDRecLen";
            numUDRecLen.Size = new Size(54, 23);
            numUDRecLen.TabIndex = 11;
            numUDRecLen.Value = new decimal(new int[] { 10, 0, 0, 0 });
            // 
            // CB_Filter
            // 
            CB_Filter.AutoSize = true;
            CB_Filter.Location = new Point(399, 378);
            CB_Filter.Margin = new Padding(4, 3, 4, 3);
            CB_Filter.Name = "CB_Filter";
            CB_Filter.Size = new Size(52, 19);
            CB_Filter.TabIndex = 15;
            CB_Filter.Text = "Filter";
            CB_Filter.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(399, 309);
            label4.Margin = new Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new Size(85, 15);
            label4.TabIndex = 16;
            label4.Text = "Start delay, sec";
            // 
            // numUDStartDelay
            // 
            numUDStartDelay.Location = new Point(516, 307);
            numUDStartDelay.Margin = new Padding(4, 3, 4, 3);
            numUDStartDelay.Maximum = new decimal(new int[] { 20, 0, 0, 0 });
            numUDStartDelay.Name = "numUDStartDelay";
            numUDStartDelay.Size = new Size(54, 23);
            numUDStartDelay.TabIndex = 17;
            // 
            // checkBox8
            // 
            checkBox8.AutoSize = true;
            checkBox8.Location = new Point(14, 218);
            checkBox8.Margin = new Padding(4, 3, 4, 3);
            checkBox8.Name = "checkBox8";
            checkBox8.Size = new Size(111, 19);
            checkBox8.TabIndex = 19;
            checkBox8.Text = "Sphigmogram 4";
            checkBox8.UseVisualStyleBackColor = true;
            // 
            // checkBox7
            // 
            checkBox7.AutoSize = true;
            checkBox7.Location = new Point(14, 192);
            checkBox7.Margin = new Padding(4, 3, 4, 3);
            checkBox7.Name = "checkBox7";
            checkBox7.Size = new Size(111, 19);
            checkBox7.TabIndex = 18;
            checkBox7.Text = "Sphigmogram 3";
            checkBox7.UseVisualStyleBackColor = true;
            // 
            // checkBox10
            // 
            checkBox10.AutoSize = true;
            checkBox10.Location = new Point(14, 271);
            checkBox10.Margin = new Padding(4, 3, 4, 3);
            checkBox10.Name = "checkBox10";
            checkBox10.Size = new Size(48, 19);
            checkBox10.TabIndex = 20;
            checkBox10.Text = "PPG";
            checkBox10.UseVisualStyleBackColor = true;
            // 
            // checkBox11
            // 
            checkBox11.AutoSize = true;
            checkBox11.Location = new Point(14, 296);
            checkBox11.Margin = new Padding(4, 3, 4, 3);
            checkBox11.Name = "checkBox11";
            checkBox11.Size = new Size(60, 19);
            checkBox11.TabIndex = 21;
            checkBox11.Text = "Sound";
            checkBox11.UseVisualStyleBackColor = true;
            // 
            // FormConfig
            // 
            AcceptButton = butOk;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = butCancel;
            ClientSize = new Size(583, 407);
            Controls.Add(checkBox11);
            Controls.Add(checkBox10);
            Controls.Add(checkBox8);
            Controls.Add(checkBox7);
            Controls.Add(numUDStartDelay);
            Controls.Add(label4);
            Controls.Add(CB_Filter);
            Controls.Add(numUDRecLen);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(checkBox9);
            Controls.Add(checkBox6);
            Controls.Add(checkBox5);
            Controls.Add(checkBox4);
            Controls.Add(checkBox3);
            Controls.Add(checkBox2);
            Controls.Add(checkBox1);
            Controls.Add(butCancel);
            Controls.Add(butOk);
            KeyPreview = true;
            Margin = new Padding(4, 3, 4, 3);
            Name = "FormConfig";
            Text = "Settings";
            ((System.ComponentModel.ISupportInitialize)numUDRecLen).EndInit();
            ((System.ComponentModel.ISupportInitialize)numUDStartDelay).EndInit();
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button butOk;
        private System.Windows.Forms.Button butCancel;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.CheckBox checkBox3;
        private System.Windows.Forms.CheckBox checkBox4;
        private System.Windows.Forms.CheckBox checkBox5;
        private System.Windows.Forms.CheckBox checkBox6;
        private System.Windows.Forms.CheckBox checkBox9;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numUDRecLen;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.CheckBox CB_Filter;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown numUDStartDelay;
        private CheckBox checkBox8;
        private CheckBox checkBox7;
        private CheckBox checkBox10;
        private CheckBox checkBox11;
    }
}