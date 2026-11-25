namespace Polycardio2
{
    partial class FormResult
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
            tbResultLeft = new TextBox();
            tbResultRight = new TextBox();
            tbInfo = new TextBox();
            butSave = new Button();
            butCancel = new Button();
            SuspendLayout();
            // 
            // tbResultLeft
            // 
            tbResultLeft.Location = new Point(13, 129);
            tbResultLeft.Margin = new Padding(4, 3, 4, 3);
            tbResultLeft.Multiline = true;
            tbResultLeft.Name = "tbResultLeft";
            tbResultLeft.Size = new Size(277, 224);
            tbResultLeft.TabIndex = 0;
            // 
            // tbResultRight
            // 
            tbResultRight.Location = new Point(334, 129);
            tbResultRight.Margin = new Padding(4, 3, 4, 3);
            tbResultRight.Multiline = true;
            tbResultRight.Name = "tbResultRight";
            tbResultRight.Size = new Size(277, 224);
            tbResultRight.TabIndex = 1;
            // 
            // tbInfo
            // 
            tbInfo.Location = new Point(13, 12);
            tbInfo.Multiline = true;
            tbInfo.Name = "tbInfo";
            tbInfo.Size = new Size(598, 96);
            tbInfo.TabIndex = 2;
            // 
            // butSave
            // 
            butSave.DialogResult = DialogResult.OK;
            butSave.Location = new Point(215, 378);
            butSave.Name = "butSave";
            butSave.Size = new Size(75, 23);
            butSave.TabIndex = 3;
            butSave.Text = "Save";
            butSave.UseVisualStyleBackColor = true;
            butSave.Click += butSave_Click;
            // 
            // butCancel
            // 
            butCancel.DialogResult = DialogResult.Cancel;
            butCancel.Location = new Point(334, 378);
            butCancel.Name = "butCancel";
            butCancel.Size = new Size(75, 23);
            butCancel.TabIndex = 4;
            butCancel.Text = "Cancel";
            butCancel.UseVisualStyleBackColor = true;
            // 
            // FormResult
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(624, 432);
            Controls.Add(butCancel);
            Controls.Add(butSave);
            Controls.Add(tbInfo);
            Controls.Add(tbResultRight);
            Controls.Add(tbResultLeft);
            Margin = new Padding(4, 3, 4, 3);
            Name = "FormResult";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FormResult";
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbResultLeft;
        private TextBox tbResultRight;
        private TextBox tbInfo;
        private Button butSave;
        private Button butCancel;
    }
}