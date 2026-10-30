namespace Polycardio2
{
    partial class Form1
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            timerRead = new System.Windows.Forms.Timer(components);
            tableLayoutPanel1 = new TableLayoutPanel();
            panelGraph = new Panel();
            panelButtons = new Panel();
            butStartFlow = new Button();
            butPumps34Stop = new Button();
            butPumps34Start = new Button();
            butPump4stop = new Button();
            butPump4start = new Button();
            butPump3stop = new Button();
            butPump3start = new Button();
            label4 = new Label();
            numUDplevel4 = new NumericUpDown();
            label5 = new Label();
            numUDplevel3 = new NumericUpDown();
            labBP4 = new Label();
            labBP3 = new Label();
            butBPabort4 = new Button();
            butBPmeas4 = new Button();
            butBPabort3 = new Button();
            butBPmeas3 = new Button();
            butPumps12Stop = new Button();
            butPumps12Start = new Button();
            labBP2 = new Label();
            labBP1 = new Label();
            butBPabort2 = new Button();
            butBPmeas2 = new Button();
            labStatus = new Label();
            butBPabort1 = new Button();
            butBPMeas1 = new Button();
            butNewRecord = new Button();
            pbRecordProgress = new ProgressBar();
            butPump2stop = new Button();
            butPump2start = new Button();
            butPump1stop = new Button();
            butPump1start = new Button();
            label3 = new Label();
            numUDplevel2 = new NumericUpDown();
            label2 = new Label();
            numUDplevel1 = new NumericUpDown();
            labP3 = new Label();
            labP2 = new Label();
            labP1 = new Label();
            butEditRecInfo = new Button();
            butStopFlow = new Button();
            butOpenFile = new Button();
            textBoxRecInfo = new TextBox();
            labRecordSize = new Label();
            butSaveRecord = new Button();
            butStartRecord = new Button();
            butSettings = new Button();
            hScrollBar1 = new HScrollBar();
            labConnected = new Label();
            panelAmp = new Panel();
            panel1 = new Panel();
            timerPaint = new System.Windows.Forms.Timer(components);
            timerStatus = new System.Windows.Forms.Timer(components);
            openFileDialog1 = new OpenFileDialog();
            saveFileDialog1 = new SaveFileDialog();
            timerDelay = new System.Windows.Forms.Timer(components);
            labPressure1 = new Label();
            labPressure2 = new Label();
            labPressure3 = new Label();
            labPressure4 = new Label();
            tableLayoutPanel1.SuspendLayout();
            panelButtons.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numUDplevel4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numUDplevel3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numUDplevel2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numUDplevel1).BeginInit();
            SuspendLayout();
            // 
            // timerRead
            // 
            timerRead.Enabled = true;
            timerRead.Tick += timerRead_Tick;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 3;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 400F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 45F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(panelGraph, 2, 0);
            tableLayoutPanel1.Controls.Add(panelButtons, 0, 0);
            tableLayoutPanel1.Controls.Add(hScrollBar1, 2, 1);
            tableLayoutPanel1.Controls.Add(labConnected, 0, 1);
            tableLayoutPanel1.Controls.Add(panelAmp, 1, 0);
            tableLayoutPanel1.Controls.Add(panel1, 2, 2);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Margin = new Padding(4, 3, 4, 3);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 3;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 29F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 46F));
            tableLayoutPanel1.Size = new Size(985, 774);
            tableLayoutPanel1.TabIndex = 1;
            // 
            // panelGraph
            // 
            panelGraph.Dock = DockStyle.Fill;
            panelGraph.Location = new Point(449, 3);
            panelGraph.Margin = new Padding(4, 3, 4, 3);
            panelGraph.Name = "panelGraph";
            panelGraph.Size = new Size(532, 693);
            panelGraph.TabIndex = 0;
            panelGraph.Resize += panelGraph_Resize;
            // 
            // panelButtons
            // 
            panelButtons.Controls.Add(labPressure4);
            panelButtons.Controls.Add(labPressure3);
            panelButtons.Controls.Add(labPressure2);
            panelButtons.Controls.Add(labPressure1);
            panelButtons.Controls.Add(butStartFlow);
            panelButtons.Controls.Add(butPumps34Stop);
            panelButtons.Controls.Add(butPumps34Start);
            panelButtons.Controls.Add(butPump4stop);
            panelButtons.Controls.Add(butPump4start);
            panelButtons.Controls.Add(butPump3stop);
            panelButtons.Controls.Add(butPump3start);
            panelButtons.Controls.Add(label4);
            panelButtons.Controls.Add(numUDplevel4);
            panelButtons.Controls.Add(label5);
            panelButtons.Controls.Add(numUDplevel3);
            panelButtons.Controls.Add(labBP4);
            panelButtons.Controls.Add(labBP3);
            panelButtons.Controls.Add(butBPabort4);
            panelButtons.Controls.Add(butBPmeas4);
            panelButtons.Controls.Add(butBPabort3);
            panelButtons.Controls.Add(butBPmeas3);
            panelButtons.Controls.Add(butPumps12Stop);
            panelButtons.Controls.Add(butPumps12Start);
            panelButtons.Controls.Add(labBP2);
            panelButtons.Controls.Add(labBP1);
            panelButtons.Controls.Add(butBPabort2);
            panelButtons.Controls.Add(butBPmeas2);
            panelButtons.Controls.Add(labStatus);
            panelButtons.Controls.Add(butBPabort1);
            panelButtons.Controls.Add(butBPMeas1);
            panelButtons.Controls.Add(butNewRecord);
            panelButtons.Controls.Add(pbRecordProgress);
            panelButtons.Controls.Add(butPump2stop);
            panelButtons.Controls.Add(butPump2start);
            panelButtons.Controls.Add(butPump1stop);
            panelButtons.Controls.Add(butPump1start);
            panelButtons.Controls.Add(label3);
            panelButtons.Controls.Add(numUDplevel2);
            panelButtons.Controls.Add(label2);
            panelButtons.Controls.Add(numUDplevel1);
            panelButtons.Controls.Add(labP3);
            panelButtons.Controls.Add(labP2);
            panelButtons.Controls.Add(labP1);
            panelButtons.Controls.Add(butEditRecInfo);
            panelButtons.Controls.Add(butStopFlow);
            panelButtons.Controls.Add(butOpenFile);
            panelButtons.Controls.Add(textBoxRecInfo);
            panelButtons.Controls.Add(labRecordSize);
            panelButtons.Controls.Add(butSaveRecord);
            panelButtons.Controls.Add(butStartRecord);
            panelButtons.Controls.Add(butSettings);
            panelButtons.Dock = DockStyle.Fill;
            panelButtons.Location = new Point(4, 3);
            panelButtons.Margin = new Padding(4, 3, 4, 3);
            panelButtons.Name = "panelButtons";
            panelButtons.Size = new Size(392, 693);
            panelButtons.TabIndex = 1;
            // 
            // butStartFlow
            // 
            butStartFlow.Location = new Point(16, 11);
            butStartFlow.Margin = new Padding(4, 3, 4, 3);
            butStartFlow.Name = "butStartFlow";
            butStartFlow.Size = new Size(88, 27);
            butStartFlow.TabIndex = 49;
            butStartFlow.Text = "Start flow";
            butStartFlow.UseVisualStyleBackColor = true;
            butStartFlow.Click += butStartFlow_Click;
            // 
            // butPumps34Stop
            // 
            butPumps34Stop.Location = new Point(254, 277);
            butPumps34Stop.Margin = new Padding(4, 3, 4, 3);
            butPumps34Stop.Name = "butPumps34Stop";
            butPumps34Stop.Size = new Size(23, 60);
            butPumps34Stop.TabIndex = 48;
            butPumps34Stop.Text = "X";
            butPumps34Stop.UseVisualStyleBackColor = true;
            // 
            // butPumps34Start
            // 
            butPumps34Start.Location = new Point(180, 277);
            butPumps34Start.Margin = new Padding(4, 3, 4, 3);
            butPumps34Start.Name = "butPumps34Start";
            butPumps34Start.Size = new Size(23, 60);
            butPumps34Start.TabIndex = 47;
            butPumps34Start.Text = ">";
            butPumps34Start.UseVisualStyleBackColor = true;
            // 
            // butPump4stop
            // 
            butPump4stop.Location = new Point(279, 310);
            butPump4stop.Margin = new Padding(4, 3, 4, 3);
            butPump4stop.Name = "butPump4stop";
            butPump4stop.Size = new Size(43, 27);
            butPump4stop.TabIndex = 46;
            butPump4stop.Text = "Stop";
            butPump4stop.UseVisualStyleBackColor = true;
            butPump4stop.Click += butPump4stop_Click;
            // 
            // butPump4start
            // 
            butPump4start.Location = new Point(203, 310);
            butPump4start.Margin = new Padding(4, 3, 4, 3);
            butPump4start.Name = "butPump4start";
            butPump4start.Size = new Size(43, 27);
            butPump4start.TabIndex = 45;
            butPump4start.Text = "Start";
            butPump4start.UseVisualStyleBackColor = true;
            butPump4start.Click += butPump4start_Click;
            // 
            // butPump3stop
            // 
            butPump3stop.Location = new Point(279, 277);
            butPump3stop.Margin = new Padding(4, 3, 4, 3);
            butPump3stop.Name = "butPump3stop";
            butPump3stop.Size = new Size(43, 27);
            butPump3stop.TabIndex = 44;
            butPump3stop.Text = "Stop";
            butPump3stop.UseVisualStyleBackColor = true;
            butPump3stop.Click += butPump3stop_Click;
            // 
            // butPump3start
            // 
            butPump3start.Location = new Point(203, 277);
            butPump3start.Margin = new Padding(4, 3, 4, 3);
            butPump3start.Name = "butPump3start";
            butPump3start.Size = new Size(43, 27);
            butPump3start.TabIndex = 43;
            butPump3start.Text = "Start";
            butPump3start.UseVisualStyleBackColor = true;
            butPump3start.Click += butPump3start_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(13, 316);
            label4.Margin = new Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new Size(87, 15);
            label4.TabIndex = 42;
            label4.Text = "Pressure level 4";
            // 
            // numUDplevel4
            // 
            numUDplevel4.Location = new Point(117, 314);
            numUDplevel4.Margin = new Padding(4, 3, 4, 3);
            numUDplevel4.Name = "numUDplevel4";
            numUDplevel4.Size = new Size(56, 23);
            numUDplevel4.TabIndex = 41;
            numUDplevel4.ValueChanged += numUDplevel4_ValueChanged;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(13, 283);
            label5.Margin = new Padding(4, 0, 4, 0);
            label5.Name = "label5";
            label5.Size = new Size(87, 15);
            label5.TabIndex = 40;
            label5.Text = "Pressure level 3";
            // 
            // numUDplevel3
            // 
            numUDplevel3.Location = new Point(117, 280);
            numUDplevel3.Margin = new Padding(4, 3, 4, 3);
            numUDplevel3.Name = "numUDplevel3";
            numUDplevel3.Size = new Size(56, 23);
            numUDplevel3.TabIndex = 39;
            numUDplevel3.ValueChanged += numUDplevel3_ValueChanged;
            // 
            // labBP4
            // 
            labBP4.AutoSize = true;
            labBP4.Location = new Point(224, 184);
            labBP4.Margin = new Padding(4, 0, 4, 0);
            labBP4.Name = "labBP4";
            labBP4.Size = new Size(38, 15);
            labBP4.TabIndex = 38;
            labBP4.Text = "ABP 4";
            // 
            // labBP3
            // 
            labBP3.AutoSize = true;
            labBP3.Location = new Point(224, 150);
            labBP3.Margin = new Padding(4, 0, 4, 0);
            labBP3.Name = "labBP3";
            labBP3.Size = new Size(38, 15);
            labBP3.TabIndex = 37;
            labBP3.Text = "ABP 3";
            // 
            // butBPabort4
            // 
            butBPabort4.Location = new Point(115, 178);
            butBPabort4.Margin = new Padding(4, 3, 4, 3);
            butBPabort4.Name = "butBPabort4";
            butBPabort4.Size = new Size(88, 27);
            butBPabort4.TabIndex = 36;
            butBPabort4.Text = "Abort ABP";
            butBPabort4.UseVisualStyleBackColor = true;
            butBPabort4.Click += butBPabort4_Click;
            // 
            // butBPmeas4
            // 
            butBPmeas4.Location = new Point(15, 178);
            butBPmeas4.Margin = new Padding(4, 3, 4, 3);
            butBPmeas4.Name = "butBPmeas4";
            butBPmeas4.Size = new Size(88, 27);
            butBPmeas4.TabIndex = 35;
            butBPmeas4.Text = "Start ABP 2";
            butBPmeas4.UseVisualStyleBackColor = true;
            butBPmeas4.Click += butBPmeas4_Click;
            // 
            // butBPabort3
            // 
            butBPabort3.Location = new Point(115, 144);
            butBPabort3.Margin = new Padding(4, 3, 4, 3);
            butBPabort3.Name = "butBPabort3";
            butBPabort3.Size = new Size(88, 27);
            butBPabort3.TabIndex = 34;
            butBPabort3.Text = "Abort ABP";
            butBPabort3.UseVisualStyleBackColor = true;
            butBPabort3.Click += butBPabort3_Click;
            // 
            // butBPmeas3
            // 
            butBPmeas3.Location = new Point(15, 144);
            butBPmeas3.Margin = new Padding(4, 3, 4, 3);
            butBPmeas3.Name = "butBPmeas3";
            butBPmeas3.Size = new Size(88, 27);
            butBPmeas3.TabIndex = 33;
            butBPmeas3.Text = "Start ABP 3";
            butBPmeas3.UseVisualStyleBackColor = true;
            butBPmeas3.Click += butBPmeas3_Click;
            // 
            // butPumps12Stop
            // 
            butPumps12Stop.Location = new Point(254, 211);
            butPumps12Stop.Margin = new Padding(4, 3, 4, 3);
            butPumps12Stop.Name = "butPumps12Stop";
            butPumps12Stop.Size = new Size(23, 60);
            butPumps12Stop.TabIndex = 31;
            butPumps12Stop.Text = "X";
            butPumps12Stop.UseVisualStyleBackColor = true;
            butPumps12Stop.Click += butPumpsStop_Click;
            // 
            // butPumps12Start
            // 
            butPumps12Start.Location = new Point(180, 211);
            butPumps12Start.Margin = new Padding(4, 3, 4, 3);
            butPumps12Start.Name = "butPumps12Start";
            butPumps12Start.Size = new Size(23, 60);
            butPumps12Start.TabIndex = 30;
            butPumps12Start.Text = ">";
            butPumps12Start.UseVisualStyleBackColor = true;
            butPumps12Start.Click += butPumps12Start_Click;
            // 
            // labBP2
            // 
            labBP2.AutoSize = true;
            labBP2.Location = new Point(224, 117);
            labBP2.Margin = new Padding(4, 0, 4, 0);
            labBP2.Name = "labBP2";
            labBP2.Size = new Size(38, 15);
            labBP2.TabIndex = 29;
            labBP2.Text = "ABP 2";
            // 
            // labBP1
            // 
            labBP1.AutoSize = true;
            labBP1.Location = new Point(224, 83);
            labBP1.Margin = new Padding(4, 0, 4, 0);
            labBP1.Name = "labBP1";
            labBP1.Size = new Size(38, 15);
            labBP1.TabIndex = 28;
            labBP1.Text = "ABP 1";
            // 
            // butBPabort2
            // 
            butBPabort2.Location = new Point(115, 111);
            butBPabort2.Margin = new Padding(4, 3, 4, 3);
            butBPabort2.Name = "butBPabort2";
            butBPabort2.Size = new Size(88, 27);
            butBPabort2.TabIndex = 27;
            butBPabort2.Text = "Abort ABP";
            butBPabort2.UseVisualStyleBackColor = true;
            butBPabort2.Click += butBPabort2_Click;
            // 
            // butBPmeas2
            // 
            butBPmeas2.Location = new Point(15, 111);
            butBPmeas2.Margin = new Padding(4, 3, 4, 3);
            butBPmeas2.Name = "butBPmeas2";
            butBPmeas2.Size = new Size(88, 27);
            butBPmeas2.TabIndex = 26;
            butBPmeas2.Text = "Start ABP 2";
            butBPmeas2.UseVisualStyleBackColor = true;
            butBPmeas2.Click += butBPmeas2_Click;
            // 
            // labStatus
            // 
            labStatus.AutoSize = true;
            labStatus.Location = new Point(131, 647);
            labStatus.Margin = new Padding(4, 0, 4, 0);
            labStatus.Name = "labStatus";
            labStatus.Size = new Size(38, 15);
            labStatus.TabIndex = 25;
            labStatus.Text = "label1";
            // 
            // butBPabort1
            // 
            butBPabort1.Location = new Point(115, 77);
            butBPabort1.Margin = new Padding(4, 3, 4, 3);
            butBPabort1.Name = "butBPabort1";
            butBPabort1.Size = new Size(88, 27);
            butBPabort1.TabIndex = 24;
            butBPabort1.Text = "Abort ABP";
            butBPabort1.UseVisualStyleBackColor = true;
            butBPabort1.Click += butBPabort_Click;
            // 
            // butBPMeas1
            // 
            butBPMeas1.Location = new Point(15, 77);
            butBPMeas1.Margin = new Padding(4, 3, 4, 3);
            butBPMeas1.Name = "butBPMeas1";
            butBPMeas1.Size = new Size(88, 27);
            butBPMeas1.TabIndex = 23;
            butBPMeas1.Text = "Start ABP 1";
            butBPMeas1.UseVisualStyleBackColor = true;
            butBPMeas1.Click += butBPMeas_Click;
            // 
            // butNewRecord
            // 
            butNewRecord.Location = new Point(15, 44);
            butNewRecord.Margin = new Padding(4, 3, 4, 3);
            butNewRecord.Name = "butNewRecord";
            butNewRecord.Size = new Size(88, 27);
            butNewRecord.TabIndex = 22;
            butNewRecord.Text = "New record";
            butNewRecord.UseVisualStyleBackColor = true;
            butNewRecord.Click += butNewRecord_Click;
            // 
            // pbRecordProgress
            // 
            pbRecordProgress.Location = new Point(112, 376);
            pbRecordProgress.Margin = new Padding(4, 3, 4, 3);
            pbRecordProgress.Name = "pbRecordProgress";
            pbRecordProgress.Size = new Size(210, 27);
            pbRecordProgress.TabIndex = 21;
            pbRecordProgress.Visible = false;
            // 
            // butPump2stop
            // 
            butPump2stop.Location = new Point(279, 244);
            butPump2stop.Margin = new Padding(4, 3, 4, 3);
            butPump2stop.Name = "butPump2stop";
            butPump2stop.Size = new Size(43, 27);
            butPump2stop.TabIndex = 20;
            butPump2stop.Text = "Stop";
            butPump2stop.UseVisualStyleBackColor = true;
            butPump2stop.Click += butPump2stop_Click;
            // 
            // butPump2start
            // 
            butPump2start.Location = new Point(203, 244);
            butPump2start.Margin = new Padding(4, 3, 4, 3);
            butPump2start.Name = "butPump2start";
            butPump2start.Size = new Size(43, 27);
            butPump2start.TabIndex = 19;
            butPump2start.Text = "Start";
            butPump2start.UseVisualStyleBackColor = true;
            butPump2start.Click += butPump2start_Click;
            // 
            // butPump1stop
            // 
            butPump1stop.Location = new Point(279, 211);
            butPump1stop.Margin = new Padding(4, 3, 4, 3);
            butPump1stop.Name = "butPump1stop";
            butPump1stop.Size = new Size(43, 27);
            butPump1stop.TabIndex = 18;
            butPump1stop.Text = "Stop";
            butPump1stop.UseVisualStyleBackColor = true;
            butPump1stop.Click += butPump1stop_Click;
            // 
            // butPump1start
            // 
            butPump1start.Location = new Point(203, 211);
            butPump1start.Margin = new Padding(4, 3, 4, 3);
            butPump1start.Name = "butPump1start";
            butPump1start.Size = new Size(43, 27);
            butPump1start.TabIndex = 17;
            butPump1start.Text = "Start";
            butPump1start.UseVisualStyleBackColor = true;
            butPump1start.Click += butPump1start_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(13, 250);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new Size(87, 15);
            label3.TabIndex = 16;
            label3.Text = "Pressure level 2";
            // 
            // numUDplevel2
            // 
            numUDplevel2.Location = new Point(117, 248);
            numUDplevel2.Margin = new Padding(4, 3, 4, 3);
            numUDplevel2.Name = "numUDplevel2";
            numUDplevel2.Size = new Size(56, 23);
            numUDplevel2.TabIndex = 15;
            numUDplevel2.ValueChanged += numUDplevel2_ValueChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(13, 217);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(87, 15);
            label2.TabIndex = 14;
            label2.Text = "Pressure level 1";
            // 
            // numUDplevel1
            // 
            numUDplevel1.Location = new Point(117, 214);
            numUDplevel1.Margin = new Padding(4, 3, 4, 3);
            numUDplevel1.Name = "numUDplevel1";
            numUDplevel1.Size = new Size(56, 23);
            numUDplevel1.TabIndex = 13;
            numUDplevel1.ValueChanged += numUDplevel1_ValueChanged;
            // 
            // labP3
            // 
            labP3.AutoSize = true;
            labP3.Location = new Point(264, 673);
            labP3.Margin = new Padding(4, 0, 4, 0);
            labP3.Name = "labP3";
            labP3.Size = new Size(20, 15);
            labP3.TabIndex = 12;
            labP3.Text = "P3";
            labP3.Visible = false;
            // 
            // labP2
            // 
            labP2.AutoSize = true;
            labP2.Location = new Point(264, 658);
            labP2.Margin = new Padding(4, 0, 4, 0);
            labP2.Name = "labP2";
            labP2.Size = new Size(20, 15);
            labP2.TabIndex = 11;
            labP2.Text = "P2";
            labP2.Visible = false;
            // 
            // labP1
            // 
            labP1.AutoSize = true;
            labP1.Location = new Point(264, 643);
            labP1.Margin = new Padding(4, 0, 4, 0);
            labP1.Name = "labP1";
            labP1.Size = new Size(20, 15);
            labP1.TabIndex = 10;
            labP1.Text = "P1";
            labP1.Visible = false;
            // 
            // butEditRecInfo
            // 
            butEditRecInfo.Location = new Point(16, 647);
            butEditRecInfo.Margin = new Padding(4, 3, 4, 3);
            butEditRecInfo.Name = "butEditRecInfo";
            butEditRecInfo.Size = new Size(88, 27);
            butEditRecInfo.TabIndex = 2;
            butEditRecInfo.Text = "Edit";
            butEditRecInfo.UseVisualStyleBackColor = true;
            butEditRecInfo.Click += butEditRecInfo_Click;
            // 
            // butStopFlow
            // 
            butStopFlow.Location = new Point(115, 11);
            butStopFlow.Margin = new Padding(4, 3, 4, 3);
            butStopFlow.Name = "butStopFlow";
            butStopFlow.Size = new Size(88, 27);
            butStopFlow.TabIndex = 9;
            butStopFlow.Text = "Stop flow";
            butStopFlow.UseVisualStyleBackColor = true;
            butStopFlow.Click += butStopFlow_Click;
            // 
            // butOpenFile
            // 
            butOpenFile.Location = new Point(238, 44);
            butOpenFile.Margin = new Padding(4, 3, 4, 3);
            butOpenFile.Name = "butOpenFile";
            butOpenFile.Size = new Size(88, 27);
            butOpenFile.TabIndex = 8;
            butOpenFile.Text = "OpenFile";
            butOpenFile.UseVisualStyleBackColor = true;
            butOpenFile.Click += butOpenFile_Click;
            // 
            // textBoxRecInfo
            // 
            textBoxRecInfo.BorderStyle = BorderStyle.FixedSingle;
            textBoxRecInfo.Location = new Point(16, 445);
            textBoxRecInfo.Margin = new Padding(4, 3, 4, 3);
            textBoxRecInfo.Multiline = true;
            textBoxRecInfo.Name = "textBoxRecInfo";
            textBoxRecInfo.ReadOnly = true;
            textBoxRecInfo.Size = new Size(310, 195);
            textBoxRecInfo.TabIndex = 7;
            // 
            // labRecordSize
            // 
            labRecordSize.AutoSize = true;
            labRecordSize.Location = new Point(109, 410);
            labRecordSize.Margin = new Padding(4, 0, 4, 0);
            labRecordSize.Name = "labRecordSize";
            labRecordSize.Size = new Size(38, 15);
            labRecordSize.TabIndex = 6;
            labRecordSize.Text = "label2";
            labRecordSize.Visible = false;
            // 
            // butSaveRecord
            // 
            butSaveRecord.Location = new Point(12, 410);
            butSaveRecord.Margin = new Padding(4, 3, 4, 3);
            butSaveRecord.Name = "butSaveRecord";
            butSaveRecord.Size = new Size(88, 27);
            butSaveRecord.TabIndex = 5;
            butSaveRecord.Text = "Save record";
            butSaveRecord.UseVisualStyleBackColor = true;
            butSaveRecord.Click += butSaveRecord_Click;
            // 
            // butStartRecord
            // 
            butStartRecord.Location = new Point(12, 376);
            butStartRecord.Margin = new Padding(4, 3, 4, 3);
            butStartRecord.Name = "butStartRecord";
            butStartRecord.Size = new Size(88, 27);
            butStartRecord.TabIndex = 4;
            butStartRecord.Text = "Start record";
            butStartRecord.UseVisualStyleBackColor = true;
            butStartRecord.Click += butStartRecord_Click;
            // 
            // butSettings
            // 
            butSettings.Location = new Point(238, 10);
            butSettings.Margin = new Padding(4, 3, 4, 3);
            butSettings.Name = "butSettings";
            butSettings.Size = new Size(88, 27);
            butSettings.TabIndex = 0;
            butSettings.Text = "Settings";
            butSettings.UseVisualStyleBackColor = true;
            butSettings.Click += butSettings_Click;
            // 
            // hScrollBar1
            // 
            hScrollBar1.Dock = DockStyle.Bottom;
            hScrollBar1.Location = new Point(445, 708);
            hScrollBar1.Name = "hScrollBar1";
            hScrollBar1.Size = new Size(540, 20);
            hScrollBar1.TabIndex = 2;
            hScrollBar1.Visible = false;
            hScrollBar1.ValueChanged += hScrollBar1_ValueChanged;
            // 
            // labConnected
            // 
            labConnected.AutoSize = true;
            labConnected.Dock = DockStyle.Left;
            labConnected.Location = new Point(4, 699);
            labConnected.Margin = new Padding(4, 0, 4, 0);
            labConnected.Name = "labConnected";
            labConnected.Size = new Size(79, 29);
            labConnected.TabIndex = 3;
            labConnected.Text = "Disconnected";
            // 
            // panelAmp
            // 
            panelAmp.Dock = DockStyle.Fill;
            panelAmp.Location = new Point(404, 3);
            panelAmp.Margin = new Padding(4, 3, 4, 3);
            panelAmp.Name = "panelAmp";
            panelAmp.Size = new Size(37, 693);
            panelAmp.TabIndex = 4;
            // 
            // panel1
            // 
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(449, 731);
            panel1.Margin = new Padding(4, 3, 4, 3);
            panel1.Name = "panel1";
            panel1.Size = new Size(532, 40);
            panel1.TabIndex = 5;
            // 
            // timerPaint
            // 
            timerPaint.Enabled = true;
            timerPaint.Interval = 110;
            timerPaint.Tick += timerPaint_Tick;
            // 
            // timerStatus
            // 
            timerStatus.Enabled = true;
            timerStatus.Interval = 500;
            timerStatus.Tick += timerStatus_Tick;
            // 
            // openFileDialog1
            // 
            openFileDialog1.FileName = "openFileDialog1";
            openFileDialog1.Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*";
            // 
            // saveFileDialog1
            // 
            saveFileDialog1.DefaultExt = "txt";
            saveFileDialog1.ValidateNames = false;
            // 
            // timerDelay
            // 
            timerDelay.Interval = 1000;
            timerDelay.Tick += timerDelay_Tick;
            // 
            // labPressure1
            // 
            labPressure1.AutoSize = true;
            labPressure1.Location = new Point(338, 216);
            labPressure1.Name = "labPressure1";
            labPressure1.Size = new Size(13, 15);
            labPressure1.TabIndex = 50;
            labPressure1.Text = "0";
            // 
            // labPressure2
            // 
            labPressure2.AutoSize = true;
            labPressure2.Location = new Point(338, 250);
            labPressure2.Name = "labPressure2";
            labPressure2.Size = new Size(13, 15);
            labPressure2.TabIndex = 51;
            labPressure2.Text = "0";
            // 
            // labPressure3
            // 
            labPressure3.AutoSize = true;
            labPressure3.Location = new Point(338, 283);
            labPressure3.Name = "labPressure3";
            labPressure3.Size = new Size(13, 15);
            labPressure3.TabIndex = 52;
            labPressure3.Text = "0";
            // 
            // labPressure4
            // 
            labPressure4.AutoSize = true;
            labPressure4.Location = new Point(338, 316);
            labPressure4.Name = "labPressure4";
            labPressure4.Size = new Size(13, 15);
            labPressure4.TabIndex = 53;
            labPressure4.Text = "0";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(985, 774);
            Controls.Add(tableLayoutPanel1);
            Margin = new Padding(4, 3, 4, 3);
            MinimumSize = new Size(1001, 617);
            Name = "Form1";
            Text = "Form1";
            FormClosing += Form1_FormClosing;
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            panelButtons.ResumeLayout(false);
            panelButtons.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numUDplevel4).EndInit();
            ((System.ComponentModel.ISupportInitialize)numUDplevel3).EndInit();
            ((System.ComponentModel.ISupportInitialize)numUDplevel2).EndInit();
            ((System.ComponentModel.ISupportInitialize)numUDplevel1).EndInit();
            ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer timerRead;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panelGraph;
        private System.Windows.Forms.Panel panelButtons;
        private System.Windows.Forms.Button butSettings;
        private System.Windows.Forms.Timer timerPaint;
        private System.Windows.Forms.Label labConnected;
        private System.Windows.Forms.Button butSaveRecord;
        private System.Windows.Forms.Button butStartRecord;
        private System.Windows.Forms.Timer timerStatus;
        private System.Windows.Forms.Label labRecordSize;
        private System.Windows.Forms.TextBox textBoxRecInfo;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.HScrollBar hScrollBar1;
        private System.Windows.Forms.Panel panelAmp;
        private System.Windows.Forms.Button butOpenFile;
        private System.Windows.Forms.Button butStopFlow;
        private System.Windows.Forms.Button butEditRecInfo;
        private System.Windows.Forms.Label labP3;
        private System.Windows.Forms.Label labP2;
        private System.Windows.Forms.Label labP1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numUDplevel2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numUDplevel1;
        private System.Windows.Forms.Button butPump2stop;
        private System.Windows.Forms.Button butPump2start;
        private System.Windows.Forms.Button butPump1stop;
        private System.Windows.Forms.Button butPump1start;
        private System.Windows.Forms.ProgressBar pbRecordProgress;
        private System.Windows.Forms.Button butNewRecord;
        private System.Windows.Forms.Button butBPMeas1;
        private System.Windows.Forms.Button butBPabort1;
        private System.Windows.Forms.Label labStatus;
        private System.Windows.Forms.Button butBPabort2;
        private System.Windows.Forms.Button butBPmeas2;
        private System.Windows.Forms.Label labBP2;
        private System.Windows.Forms.Label labBP1;
        private System.Windows.Forms.Button butPumps12Stop;
        private System.Windows.Forms.Button butPumps12Start;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Timer timerDelay;
        private System.Windows.Forms.Panel panel1;
        private Label labBP4;
        private Label labBP3;
        private Button butBPabort4;
        private Button butBPmeas4;
        private Button butBPabort3;
        private Button butBPmeas3;
        private Button butPumps34Stop;
        private Button butPumps34Start;
        private Button butPump4stop;
        private Button butPump4start;
        private Button butPump3stop;
        private Button butPump3start;
        private Label label4;
        private NumericUpDown numUDplevel4;
        private Label label5;
        private NumericUpDown numUDplevel3;
        private Button butStartFlow;
        private Label labPressure4;
        private Label labPressure3;
        private Label labPressure2;
        private Label labPressure1;
    }
}

