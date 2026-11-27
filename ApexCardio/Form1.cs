using System.Text;

namespace Polycardio2
{
    struct GraphicsInfo
    {
        public BufferedPanel BufPanel;
        public bool Visible;
        public Label LabName;
    }

    public partial class Form1 : Form, IMessageHandler
    {
        public event Action<Message> WindowsMessage;
        private DataArrays DataA;
        public ApexConfig Cfg;
        USBserialPort USBPort;
        public bool Connected;
        private ByteDecomposer decomposer;
        GraphicsInfo[] GInfoArr;
        TrackBar[] AmpBarArr;
        int[] AmpBarVals;
        private enum _ampBarNames
        {
            a_ecg,
            a_reo,
            a_sphig12,
            a_sphig34,
            a_apex,
            a_ppg,
            a_sound
        }
        private string[] ChannelsNames = {
            "ECG 1",
            "ECG 2",
            "Reogram 1",
            "Reogram 2",
            "Sphigmogram 1",
            "Sphigmogram 2",
            "Sphigmogram 3",
            "Sphigmogram 4",
            "Apex cardiogram",
             "PPG",
             "Sound"
        };

        private enum ChNames
        {
            chECG1,
            chECG2,
            chReo1,
            chReo2,
            chSphigmo1,
            chSphigmo2,
            chSphigmo3,
            chSphigmo4,
            chApex,
            chPPG,
            chSound

        }


        private double[] ChannelsScaleY;
        private int ViewShift = 0;
        private bool ViewMode = false;
        FileStream filestream;
        StreamWriter textWriter;
        StreamWriter textSoundWriter;
        private string CurrentFile;
        private RecordInfo CurrentRecordInfo;
        private int DelayCounter;
        private ApexVisirs V;

        public Form1()
        {
            InitializeComponent();
            Cfg = ApexConfig.GetConfig();
            GInfoArr = new GraphicsInfo[ApexConstants.NumOfChannels];
            AmpBarArr = new TrackBar[ApexConstants.NumOfAmpBars];
            AmpBarVals = new int[ApexConstants.NumOfAmpBars];
            ChannelsScaleY = new double[ApexConstants.NumOfAmpBars];
            for (int i = 0; i < ApexConstants.NumOfAmpBars; i++)
            {
                ChannelsScaleY[i] = 1;
                AmpBarVals[i] = 0;
            }
            USBPort = new USBserialPort(this, 115200);
            USBPort.ConnectionFailure += onConnectionFailure;
            USBPort.ConnectionOk += onConnectionOk;

            USBPort.Connect();


            numUDplevel1.Value = Cfg.PressLevel1;
            numUDplevel2.Value = Cfg.PressLevel2;
            numUDplevel3.Value = Cfg.PressLevel3;
            numUDplevel4.Value = Cfg.PressLevel4;

            InitArraysForFlow();
            CheckDataDir();

            if (Cfg.Maximized) WindowState = FormWindowState.Maximized;
            else
            {
                WindowState = FormWindowState.Normal;
                Width = Cfg.WindowWidth;
                Height = Cfg.WindowHeight;
            }


            for (int i = 0; i < ApexConstants.NumOfChannels; i++)
            {
                GInfoArr[i].Visible = Cfg.VisibleGraphs[i];
            }
            V = new ApexVisirs();
            UpdateGraphics();
        }

        private void onConnectionOk()
        {
            InitDevice();
        }

        private void InitDevice()
        {
            USBPort.WriteByte(ApexConstants.cmdSetPressSfig1);
            USBPort.WriteByte(Cfg.PressLevel1);
            USBPort.WriteByte(ApexConstants.cmdSetPressSfig2);
            USBPort.WriteByte(Cfg.PressLevel2);
            USBPort.WriteByte(ApexConstants.cmdSetPressSfig3);
            USBPort.WriteByte(Cfg.PressLevel3);
            USBPort.WriteByte(ApexConstants.cmdSetPressSfig4);
            USBPort.WriteByte(Cfg.PressLevel4);
        }

        private void InitArraysForFlow()
        {
            DataA = new DataArrays(ByteDecomposer.DataArrSize);
            decomposer = new ByteDecomposer(DataA);
            decomposer.ConnectionBreakdown -= ConnectionBreak;
            decomposer.ConnectionBreakdown += ConnectionBreak;
            decomposer.DecomposeLineEvent -= LineReceived;
            decomposer.DecomposeLineEvent += LineReceived;
            decomposer.BPcompleted -= BPcomplete;
            decomposer.BPcompleted += BPcomplete;
        }

        private void CheckDataDir()
        {
            openFileDialog1.InitialDirectory = Cfg.DataDir;
            DirectoryInfo datadir = new DirectoryInfo(Cfg.DataDir);
            if (!datadir.Exists) datadir.Create();
        }

        void ConnectionBreak(object sender, EventArgs arg)
        {
        }

        void LineReceived(object sender, EventArgs arg)
        {
#pragma warning disable CA1416 // Проверка совместимости платформы
            this.Invoke(new Action(() =>
            {
                label1.Text = "Счетчик пакетов " + decomposer.MainIndex.ToString();


                uint status = DataA.DeviceStatus;
                cb1_InProgress.Checked = (status & (uint)DeviceStatusFlags.dsBP1_InProgress) != 0;
                cb1_OK.Checked = (status & (uint)DeviceStatusFlags.dsBP1_OK) != 0;
                cb1_Error.Checked = (status & (uint)DeviceStatusFlags.dsBP1_Error) != 0;
                cb1_Hold.Checked = (status & (uint)DeviceStatusFlags.dsBP1_Hold) != 0;
                cb2_InProgress.Checked = (status & (uint)DeviceStatusFlags.dsBP2_InProgress) != 0;
                cb2_OK.Checked = (status & (uint)DeviceStatusFlags.dsBP2_OK) != 0;
                cb2_Error.Checked = (status & (uint)DeviceStatusFlags.dsBP2_Error) != 0;
                cb2_Hold.Checked = (status & (uint)DeviceStatusFlags.dsBP2_Hold) != 0;
                cb3_InProgress.Checked = (status & (uint)DeviceStatusFlags.dsBP3_InProgress) != 0;
                cb3_OK.Checked = (status & (uint)DeviceStatusFlags.dsBP3_OK) != 0;
                cb3_Error.Checked = (status & (uint)DeviceStatusFlags.dsBP3_Error) != 0;
                cb3_Hold.Checked = (status & (uint)DeviceStatusFlags.dsBP3_Hold) != 0;
                cb4_InProgress.Checked = (status & (uint)DeviceStatusFlags.dsBP4_InProgress) != 0;
                cb4_OK.Checked = (status & (uint)DeviceStatusFlags.dsBP4_OK) != 0;
                cb4_Error.Checked = (status & (uint)DeviceStatusFlags.dsBP4_Error) != 0;
                cb4_Hold.Checked = (status & (uint)DeviceStatusFlags.dsBP4_Hold) != 0;
                cbPPG_Error.Checked = (status & (uint)DeviceStatusFlags.dsPPG_Error) != 0;

                labNIBP1_Press.Text = "Текущее давление : " + DataA.NIBP1_Press.ToString();
                labNIBP2_Press.Text = "Текущее давление : " + DataA.NIBP2_Press.ToString();
                labNIBP3_Press.Text = "Текущее давление : " + DataA.NIBP3_Press.ToString();
                labNIBP4_Press.Text = "Текущее давление : " + DataA.NIBP4_Press.ToString();

                labBP1.Text = (cb1_InProgress.Checked)? DataA.NIBP1_Press.ToString() :
                                                        DataA.NIBP1_Syst.ToString() + " / " + DataA.NIBP1_Diast.ToString();
                labBP2.Text = (cb2_InProgress.Checked)? DataA.NIBP2_Press.ToString() :
                                                        DataA.NIBP2_Syst.ToString() + " / " + DataA.NIBP2_Diast.ToString();
                labBP3.Text = (cb3_InProgress.Checked)? DataA.NIBP3_Press.ToString() :
                                                        DataA.NIBP3_Syst.ToString() + " / " + DataA.NIBP3_Diast.ToString();
                labBP4.Text = (cb4_InProgress.Checked)? DataA.NIBP4_Press.ToString() :
                                                        DataA.NIBP4_Syst.ToString() + " / " + DataA.NIBP4_Diast.ToString();

                if (CurrentRecordInfo != null)
                {
                    CurrentRecordInfo.Data.ABP1 = [DataA.NIBP1_Syst, DataA.NIBP1_Diast];
                    CurrentRecordInfo.Data.ABP2 = [DataA.NIBP2_Syst, DataA.NIBP2_Diast];
                    CurrentRecordInfo.Data.ABP3 = [DataA.NIBP3_Syst, DataA.NIBP3_Diast];
                    CurrentRecordInfo.Data.ABP4 = [DataA.NIBP4_Syst, DataA.NIBP4_Diast];
                }

                labSys1.Text = "СИС : " + DataA.NIBP1_Syst.ToString();
                labDia1.Text = "ДИА : " + DataA.NIBP1_Diast.ToString();

                labSys2.Text = "СИС : " + DataA.NIBP2_Syst.ToString();
                labDia2.Text = "ДИА : " + DataA.NIBP2_Diast.ToString();

                labSys3.Text = "СИС : " + DataA.NIBP3_Syst.ToString();
                labDia3.Text = "ДИА : " + DataA.NIBP3_Diast.ToString();

                labSys4.Text = "СИС : " + DataA.NIBP4_Syst.ToString();
                labDia4.Text = "ДИА : " + DataA.NIBP4_Diast.ToString();
            }));
#pragma warning restore CA1416 // Проверка совместимости платформы
        }

        void NewLineReceived(object sender, EventArgs agr)
        {
            //            if (decomposer.LineCounter % ByteDecomposer.SamplingFrequency != 0) return;
            int lc = decomposer.LineCounter / DataProcessing.SamplingFrequency;
            labRecordSize.Text = String.Concat("Record size : ",
                                               lc.ToString(),
                                               " sec");
            if (decomposer.LineCounter > (Cfg.RecordLength * DataProcessing.SamplingFrequency))
            {
                StopRecord();
                return;
            }
            pbRecordProgress.Value = decomposer.LineCounter;
        }


        private void BPcomplete(object sender, EventArgs arg)
        {
            string mess;
#pragma warning disable CA1416 // Проверка совместимости платформы
            this.Invoke(new Action(() =>
            {

                if (decomposer.Status.HasFlag(DeviceStatusFlags.dsBP1_OK))
                {
                    mess = "BP 1 " + decomposer.Data.NIBP1_Syst.ToString() + "/" +
                                     decomposer.Data.NIBP1_Diast.ToString();
                    labBP1.Text = decomposer.Data.NIBP1_Syst.ToString() + " / " + decomposer.Data.NIBP1_Diast.ToString();
                    if (CurrentRecordInfo != null)
                    {
                        CurrentRecordInfo.Data.ABP1[0] = decomposer.Data.NIBP1_Syst;
                        CurrentRecordInfo.Data.ABP1[1] = decomposer.Data.NIBP1_Diast;
                        CurrentRecordInfo.Save(Cfg);
                        UpdateRecInfoBox(CurrentRecordInfo);
                    }
                }
                if (decomposer.Status.HasFlag(DeviceStatusFlags.dsBP1_Error))
                {
                    mess = "BP 1 error";
                    labBP1.Text = "Error";
                }

                if (decomposer.Status.HasFlag(DeviceStatusFlags.dsBP2_OK))
                {
                    mess = "BP 2 " + decomposer.Data.NIBP2_Syst.ToString() + "/" +
                                     decomposer.Data.NIBP2_Diast.ToString();
                    labBP2.Text = decomposer.Data.NIBP2_Syst.ToString() + " / " + decomposer.Data.NIBP2_Diast.ToString();
                    if (CurrentRecordInfo != null)
                    {
                        CurrentRecordInfo.Data.ABP2[0] = decomposer.Data.NIBP2_Syst;
                        CurrentRecordInfo.Data.ABP2[1] = decomposer.Data.NIBP2_Diast;
                        CurrentRecordInfo.Save(Cfg);
                        UpdateRecInfoBox(CurrentRecordInfo);
                    }
                }
                if (decomposer.Status.HasFlag(DeviceStatusFlags.dsBP2_Error))
                {
                    mess = "BP 2 error";
                    labBP2.Text = "Error";
                }

                if (decomposer.Status.HasFlag(DeviceStatusFlags.dsBP3_OK))
                {
                    mess = "BP 2 " + decomposer.Data.NIBP3_Syst.ToString() + "/" +
                                     decomposer.Data.NIBP3_Diast.ToString();
                    labBP2.Text = decomposer.Data.NIBP3_Syst.ToString() + " / " + decomposer.Data.NIBP3_Diast.ToString();
                    if (CurrentRecordInfo != null)
                    {
                        CurrentRecordInfo.Data.ABP3[0] = decomposer.Data.NIBP3_Syst;
                        CurrentRecordInfo.Data.ABP3[1] = decomposer.Data.NIBP3_Diast;
                        CurrentRecordInfo.Save(Cfg);
                        UpdateRecInfoBox(CurrentRecordInfo);
                    }
                }
                if (decomposer.Status.HasFlag(DeviceStatusFlags.dsBP3_Error))
                {
                    mess = "BP 3 error";
                    labBP3.Text = "Error";
                }

                if (decomposer.Status.HasFlag(DeviceStatusFlags.dsBP4_OK))
                {
                    mess = "BP 2 " + decomposer.Data.NIBP4_Syst.ToString() + "/" +
                                     decomposer.Data.NIBP3_Diast.ToString();
                    labBP2.Text = decomposer.Data.NIBP4_Syst.ToString() + " / " + decomposer.Data.NIBP3_Diast.ToString();
                    if (CurrentRecordInfo != null)
                    {
                        CurrentRecordInfo.Data.ABP4[0] = decomposer.Data.NIBP4_Syst;
                        CurrentRecordInfo.Data.ABP4[1] = decomposer.Data.NIBP3_Diast;
                        CurrentRecordInfo.Save(Cfg);
                        UpdateRecInfoBox(CurrentRecordInfo);
                    }
                }
                if (decomposer.Status.HasFlag(DeviceStatusFlags.dsBP4_Error))
                {
                    mess = "BP 4 error";
                    labBP4.Text = "Error";
                }
            }));
#pragma warning restore CA1416 // Проверка совместимости платформы
        }



        public void UpdateGraphics()
        {
            for (int i = 0; i < ApexConstants.NumOfChannels; i++)
            {
                GInfoArr[i].Visible = Cfg.VisibleGraphs[i];
            }
            int num = 0;
            foreach (GraphicsInfo gi in GInfoArr)
            {
                if (gi.Visible) num++;
                if (gi.BufPanel != null) gi.BufPanel.Dispose();
                if (gi.LabName != null) gi.LabName.Dispose();
                panelGraph.Controls.Remove(gi.BufPanel);
                panelGraph.Controls.Remove(gi.LabName);
            }
            if (num == 0) return;
            int space = 14;
            int Y = 0;
            int singleHeight = panelGraph.Height / num - space;
            for (int i = 0; i < ApexConstants.NumOfChannels; i++)
            {
                GraphicsInfo gi = GInfoArr[i];
                if (gi.Visible)
                {
                    gi.LabName = new Label
                    {
                        Location = new Point(space, Y),
                        Text = ChannelsNames[i]
                    };
                    gi.BufPanel = new BufferedPanel(i)
                    {
                        Cursor = Cursors.Cross,
                        Location = new Point(space, Y + space)
                    };
                    Y = Y + singleHeight + space;
                    gi.BufPanel.Size = new Size(panelGraph.Width - space, singleHeight);
                    if (i == 0) gi.BufPanel.Paint += bufferedPanel0_Paint;
                    if (i == 1) gi.BufPanel.Paint += bufferedPanel1_Paint;
                    if (i == 2) gi.BufPanel.Paint += bufferedPanel2_Paint;
                    if (i == 3) gi.BufPanel.Paint += bufferedPanel3_Paint;
                    if (i == 4) gi.BufPanel.Paint += bufferedPanel4_Paint;
                    if (i == 5) gi.BufPanel.Paint += bufferedPanel5_Paint;
                    if (i == 6) gi.BufPanel.Paint += bufferedPanel6_Paint;
                    if (i == 7) gi.BufPanel.Paint += bufferedPanel7_Paint;
                    if (i == 8) gi.BufPanel.Paint += bufferedPanel8_Paint;
                    if (i == 9) gi.BufPanel.Paint += bufferedPanel9_Paint;
                    if (i == 10) gi.BufPanel.Paint += bufferedPanel10_Paint;
                    panelGraph.Controls.Add(gi.BufPanel);
                    panelGraph.Controls.Add(gi.LabName);
                    GInfoArr[i] = gi;
                    gi.BufPanel.Refresh();
                }
                else
                {
                    gi.BufPanel = null;
                }
            }
            UpdateAmpBars(space, singleHeight);
        }


        private void UpdateAmpBars(int space, int singleHeight)
        {
            for (int i = 0; i < AmpBarArr.Count(); i++)
            {
                if (AmpBarArr[i] != null)
                {
                    AmpBarArr[i].Dispose();
                    panelAmp.Controls.Remove(AmpBarArr[i]);
                    AmpBarArr[i] = null;
                }
            }
            if (GInfoArr[(int)ChNames.chECG1].Visible | GInfoArr[(int)ChNames.chECG2].Visible)
            {
                AmpBarArr[(int)_ampBarNames.a_ecg] = new TrackBar();
                AmpBarArr[(int)_ampBarNames.a_ecg].ValueChanged += trackBarECG_ValueChanged;
            }
            if (GInfoArr[(int)ChNames.chReo1].Visible | GInfoArr[(int)ChNames.chReo2].Visible)
            {
                AmpBarArr[(int)_ampBarNames.a_reo] = new TrackBar();
                AmpBarArr[(int)_ampBarNames.a_reo].ValueChanged += trackBarReo_ValueChanged;
            }
            if (GInfoArr[(int)ChNames.chSphigmo1].Visible | GInfoArr[(int)ChNames.chSphigmo2].Visible)
            {
                AmpBarArr[(int)_ampBarNames.a_sphig12] = new TrackBar();
                AmpBarArr[(int)_ampBarNames.a_sphig12].ValueChanged += trackBarSphigmo12_ValueChanged;
            }
            if (GInfoArr[(int)ChNames.chSphigmo3].Visible | GInfoArr[(int)ChNames.chSphigmo4].Visible)
            {
                AmpBarArr[(int)_ampBarNames.a_sphig34] = new TrackBar();
                AmpBarArr[(int)_ampBarNames.a_sphig34].ValueChanged += trackBarSphigmo34_ValueChanged;
            }
            if (GInfoArr[(int)ChNames.chApex].Visible)
            {
                AmpBarArr[(int)_ampBarNames.a_apex] = new TrackBar();
                AmpBarArr[(int)_ampBarNames.a_apex].ValueChanged += trackBarApex_ValueChanged;
            }
            if (GInfoArr[(int)ChNames.chPPG].Visible)
            {
                AmpBarArr[(int)_ampBarNames.a_ppg] = new TrackBar();
                AmpBarArr[(int)_ampBarNames.a_ppg].ValueChanged += trackBarPPG_ValueChanged;
            }
            if (GInfoArr[(int)ChNames.chSound].Visible)
            {
                AmpBarArr[(int)_ampBarNames.a_sound] = new TrackBar();
                AmpBarArr[(int)_ampBarNames.a_sound].ValueChanged += trackBarSound_ValueChanged;
            }

            for (int i = 0; i < ApexConstants.NumOfAmpBars; i++)
            {
                if (AmpBarArr[i] != null)
                {
                    AmpBarArr[i].Minimum = -5;
                    AmpBarArr[i].Maximum = 5;
                    AmpBarArr[i].Orientation = Orientation.Vertical;
                    AmpBarArr[i].Value = AmpBarVals[i];
                    AmpBarArr[i].LargeChange = 1;
                }
            }
            if (AmpBarArr[(int)_ampBarNames.a_ecg] != null)
            {
                if ((GInfoArr[(int)ChNames.chECG1].Visible ^ GInfoArr[(int)ChNames.chECG2].Visible))
                {
                    AmpBarArr[(int)_ampBarNames.a_ecg].Size = new Size(45, singleHeight);
                }
                else
                {
                    AmpBarArr[(int)_ampBarNames.a_ecg].Size = new Size(45, singleHeight * 2);
                }
            }

            if (AmpBarArr[(int)_ampBarNames.a_reo] != null)
            {
                if ((GInfoArr[(int)ChNames.chReo1].Visible ^ GInfoArr[(int)ChNames.chReo2].Visible))
                {
                    AmpBarArr[(int)_ampBarNames.a_reo].Size = new Size(45, singleHeight);
                }
                else
                {
                    AmpBarArr[(int)_ampBarNames.a_reo].Size = new Size(45, singleHeight * 2);
                }
            }

            if (AmpBarArr[(int)_ampBarNames.a_sphig12] != null)
            {
                if ((GInfoArr[(int)ChNames.chSphigmo1].Visible ^ GInfoArr[(int)ChNames.chSphigmo2].Visible))
                {
                    AmpBarArr[(int)_ampBarNames.a_sphig12].Size = new Size(45, singleHeight);
                }
                else
                {
                    AmpBarArr[(int)_ampBarNames.a_sphig12].Size = new Size(45, singleHeight * 2);
                }
            }

            if (AmpBarArr[(int)_ampBarNames.a_sphig34] != null)
            {
                if ((GInfoArr[(int)ChNames.chSphigmo3].Visible ^ GInfoArr[(int)ChNames.chSphigmo4].Visible))
                {
                    AmpBarArr[(int)_ampBarNames.a_sphig34].Size = new Size(45, singleHeight);
                }
                else
                {
                    AmpBarArr[(int)_ampBarNames.a_sphig34].Size = new Size(45, singleHeight * 2);
                }
            }

            if (AmpBarArr[(int)_ampBarNames.a_apex] != null)
            {
                AmpBarArr[(int)_ampBarNames.a_apex].Size = new Size(45, singleHeight);
            }

            if (AmpBarArr[(int)_ampBarNames.a_ppg] != null)
            {
                AmpBarArr[(int)_ampBarNames.a_ppg].Size = new Size(45, singleHeight);
            }

            if (AmpBarArr[(int)_ampBarNames.a_sound] != null)
            {
                AmpBarArr[(int)_ampBarNames.a_sound].Size = new Size(45, singleHeight);
            }

            int Y = space;
            for (int i = 0; i < AmpBarArr.Count(); i++)
            {
                if (AmpBarArr[i] != null)
                {
                    int a;
                    if (AmpBarArr[i].Height == singleHeight)
                        a = space;
                    else
                        a = space * 2;
                    AmpBarArr[i].Location = new Point(0, Y);
                    Y = Y + AmpBarArr[i].Height + a;
                    panelAmp.Controls.Add(AmpBarArr[i]);
                }
            }
        }


        private void buffPanel_Paint(int[] data, int[] derivdata, Control panel, double ScaleY, int MaxSize, PaintEventArgs e)
        {
            //            if (decomposer.MainIndex < 2 & !ViewMode) return;
            float tension = 0.1F;
            var R0 = new Rectangle(0, 0, panel.Width - 1, panel.Height - 1);
            var pen0 = new Pen(Color.Black, 1);
            e.Graphics.Clear(Color.White);
            e.Graphics.DrawRectangle(pen0, R0);
            pen0.Dispose();
            Point[] OutArray;
            if (!ViewMode)
            {
                OutArray = ViewArrayMaker.MakeArray(panel, data, decomposer.MainIndex, MaxSize, ScaleY);
            }
            else
            {
                OutArray = ViewArrayMaker.MakeArrayForView(panel, data, ViewShift, MaxSize, ScaleY);
            }
            var pen = new Pen(Color.Red, 1);
            e.Graphics.DrawCurve(pen, OutArray, tension);
            pen.Dispose();
            if (derivdata != null)
            {
                Point[] DerivArray = ViewArrayMaker.MakeArrayForView(panel, derivdata, ViewShift, MaxSize, ScaleY);
                var penD = new Pen(Color.Black, 1);
                e.Graphics.DrawCurve(penD, DerivArray, tension);
                penD.Dispose();
            }

            var pen1 = new Pen(Color.Blue, 1);
            if (V.Visir1.Visible)
                e.Graphics.DrawLine(pen1, V.Visir1.X - ViewShift, R0.Top, V.Visir1.X - ViewShift, R0.Height);
            if (V.Visir2.Visible)
                e.Graphics.DrawLine(pen1, V.Visir2.X - ViewShift, R0.Top, V.Visir2.X - ViewShift, R0.Height);
            pen1.Dispose();

            if (data == DataA.ECG1ViewArray)
            {
                var penR = new Pen(Color.Green, 1);
                for (int i = 0; i < DataA.RInd; i++)
                {
                    e.Graphics.DrawLine(penR, DataA.RArray[i] - ViewShift, R0.Top, DataA.RArray[i] - ViewShift, R0.Height);
                }
                penR.Dispose();
            }
        }

        private void bufferedPanel0_Paint(object sender, PaintEventArgs e)
        {
            buffPanel_Paint(DataA.ECG1ViewArray, null, GInfoArr[0].BufPanel, ChannelsScaleY[(int)_ampBarNames.a_ecg], Cfg.ChannelsMaxSize[0], e);
        }
        private void bufferedPanel1_Paint(object sender, PaintEventArgs e)
        {
            buffPanel_Paint(DataA.ECG2ViewArray, null, GInfoArr[1].BufPanel, ChannelsScaleY[(int)_ampBarNames.a_ecg], Cfg.ChannelsMaxSize[1], e);
        }
        private void bufferedPanel2_Paint(object sender, PaintEventArgs e)
        {
            buffPanel_Paint(DataA.Reo1ViewArray, null, GInfoArr[2].BufPanel, ChannelsScaleY[(int)_ampBarNames.a_reo], Cfg.ChannelsMaxSize[2], e);
        }
        private void bufferedPanel3_Paint(object sender, PaintEventArgs e)
        {
            buffPanel_Paint(DataA.Reo2ViewArray, null, GInfoArr[3].BufPanel, ChannelsScaleY[(int)_ampBarNames.a_reo], Cfg.ChannelsMaxSize[3], e);
        }
        private void bufferedPanel4_Paint(object sender, PaintEventArgs e)
        {
            buffPanel_Paint(DataA.SphigmoLeftHandViewArray, null, GInfoArr[4].BufPanel, ChannelsScaleY[(int)_ampBarNames.a_sphig12], Cfg.ChannelsMaxSize[4], e);
        }
        private void bufferedPanel5_Paint(object sender, PaintEventArgs e)
        {
            buffPanel_Paint(DataA.SphigmoLeftLegViewArray, null, GInfoArr[5].BufPanel, ChannelsScaleY[(int)_ampBarNames.a_sphig12], Cfg.ChannelsMaxSize[5], e);
        }
        private void bufferedPanel6_Paint(object sender, PaintEventArgs e)
        {
            buffPanel_Paint(DataA.SphigmoRightHandViewArray, null, GInfoArr[6].BufPanel, ChannelsScaleY[(int)_ampBarNames.a_sphig34], Cfg.ChannelsMaxSize[4], e);
        }
        private void bufferedPanel7_Paint(object sender, PaintEventArgs e)
        {
            buffPanel_Paint(DataA.SphigmoRightLegViewArray, null, GInfoArr[7].BufPanel, ChannelsScaleY[(int)_ampBarNames.a_sphig34], Cfg.ChannelsMaxSize[5], e);
        }
        private void bufferedPanel8_Paint(object sender, PaintEventArgs e)
        {
            buffPanel_Paint(DataA.ApexViewArray, null, GInfoArr[8].BufPanel, ChannelsScaleY[(int)_ampBarNames.a_apex], Cfg.ChannelsMaxSize[6], e);
        }
        private void bufferedPanel9_Paint(object sender, PaintEventArgs e)
        {
            buffPanel_Paint(DataA.PPGViewArray, null, GInfoArr[9].BufPanel, ChannelsScaleY[(int)_ampBarNames.a_ppg], Cfg.ChannelsMaxSize[7], e);
        }
        private void bufferedPanel10_Paint(object sender, PaintEventArgs e)
        {
            buffPanel_Paint(DataA.SoundViewArray, null, GInfoArr[10].BufPanel, ChannelsScaleY[(int)_ampBarNames.a_sound], Cfg.ChannelsMaxSize[7], e);
        }


        private void onConnectionFailure(Exception obj)
        {
            Connected = false;
        }

        protected override void WndProc(ref Message m)
        {
            const int WM_DEVICECHANGE = 0x0219;
            if (WindowsMessage != null)
            {
                if (m.Msg == WM_DEVICECHANGE) WindowsMessage(m);
            }
            base.WndProc(ref m);
        }


        private void panelGraph_Resize(object sender, EventArgs e)
        {
            UpdateGraphics();
            /*            if (ViewMode & decomposer != null)
                        {
                            UpdateScrollBar(decomposer.Data.ApexArray.Length);
                        }
                        else
                        {
                            hScrollBar1.Visible = false;
                        }*/
        }

        private void butSettings_Click(object sender, EventArgs e)
        {
            var fc = new FormConfig(Cfg);
            if (fc.ShowDialog() == DialogResult.OK)
            {
                Cfg = fc.GetDialogData();
                ApexConfig.SaveConfig(Cfg);
                UpdateGraphics();
            }
            fc.Dispose();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (decomposer != null)
            {
                if (Connected & decomposer.DeviceTurnedOn)
                {
                    USBPort.WriteByte(ApexConstants.cmdStopSfig1);
                    USBPort.WriteByte(ApexConstants.cmdStopSfig2);
                }
            }
            if (WindowState == FormWindowState.Maximized) Cfg.Maximized = true;
            else Cfg.Maximized = false;
            Cfg.WindowWidth = Width;
            Cfg.WindowHeight = Height;
            ApexConfig.SaveConfig(Cfg);
        }

        private void timerPaint_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < ApexConstants.NumOfChannels; i++)
            {
                if (GInfoArr[i].Visible)
                {
                    GInfoArr[i].BufPanel.Refresh();
                }
            }
        }

        private void timerRead_Tick(object sender, EventArgs e)
        {
            if (USBPort == null) return;
            if (USBPort.PortHandle == null) return;
            if (!USBPort.PortHandle.IsOpen) return;
            if (decomposer != null)
            {
                decomposer.Decompos(USBPort, filestream, textWriter, textSoundWriter, Cfg);
            }
        }

        private void timerStatus_Tick(object sender, EventArgs e)
        {
            panelFlow.Enabled = Connected;

            if (USBPort == null)
            {
                labConnected.Text = "Disconnected";
                Connected = false;
                return;
            }
            if (USBPort.PortHandle == null)
            {
                labConnected.Text = "Disconnected";
                Connected = false;
                return;
            }
            if (USBPort.PortHandle.IsOpen)
            {
                string s = "Connected to " + USBPort.PortNames[USBPort.CurrentPort];
                if (decomposer != null)
                    if (!decomposer.DeviceTurnedOn)
                    {
                        s = s + "    Device turned off";
                        labConnected.ForeColor = System.Drawing.Color.Red;
                        labConnected.Font = new Font(this.Font, FontStyle.Bold);
                    }
                    else
                    {
                        labConnected.ForeColor = System.Drawing.Color.Black;
                        labConnected.Font = new Font(this.Font, FontStyle.Regular);
                    }
                labConnected.Text = s;
                Connected = true;
            }
            else
            {
                labConnected.Text = "Disconnected";
                Connected = false;
            }

        }

        private void UpdateRecInfoBox(RecordInfo riarg)
        {
            textBoxRecInfo.Clear();
            textBoxRecInfo.Text += "File : " + Convert.ToChar(9) + Path.GetFileName(riarg.FileName) + System.Environment.NewLine;
            string[] lines = riarg.GetRecInfo(true);
            for (int i = 0; i < lines.Length - 1; i++)
            {
                string s = lines[i];
                    if (i != (lines.Length - 2)) 
                        s = s + System.Environment.NewLine;
                    textBoxRecInfo.Text += s;
            }
            textBoxRecInfo.SelectionStart = 0;
        }

        private void butNewRecord_Click(object sender, EventArgs e)
        {
            //            CurrentFile = Cfg.DataFileNum.ToString("D4");
            saveFileDialog1.FileName = "";
            saveFileDialog1.InitialDirectory = Cfg.DataDir.ToString();
            saveFileDialog1.Filter = "Text files | *.txt|All files | *.*";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Cfg.DataDir = Path.GetDirectoryName(saveFileDialog1.FileName) + @"\";
                ApexConfig.SaveConfig(Cfg);
                CurrentFile = Path.GetFileName(saveFileDialog1.FileName);
                var ri = new RecordInfo(Cfg, CurrentFile);
                ri.Data.Date = DateTime.Now;
                ri.Data.Age = 40;
                ri.Data.Height = 175;
                ri.Data.Weight = 75;
                ri.Save(Cfg);
                CurrentRecordInfo = ri;
                using (FormRecordInfo fri = new FormRecordInfo(ri))
                {
                    if (fri.ShowDialog() == DialogResult.OK)
                    {
                        butStartRecord.Enabled = true;
                    }
                }
                UpdateRecInfoBox(ri);
            }
        }

        private void StartRecord()
        {
            CurrentRecordInfo.Data.Date = DateTime.Now;
            CurrentRecordInfo.Save(Cfg);
            UpdateRecInfoBox(CurrentRecordInfo);

            pbRecordProgress.Maximum = Cfg.RecordLength * DataProcessing.SamplingFrequency;
            //            string fname = String.Concat(Cfg.DataDir.ToString(), CurrentFile, ApexConstants.DataFileExtension);
            //            filestream = new FileStream(fname, FileMode.Create);
            textWriter = new StreamWriter(Cfg.DataDir.ToString() + ApexConstants.TmpTextFile);
            string sf = Path.GetFileName(CurrentRecordInfo.FileName);
            textSoundWriter = new StreamWriter(Cfg.DataDir + sf + "_sound.txt");
            decomposer.TotalBytes = 0;
            decomposer.LineCounter = 0;
            decomposer.DecomposeLineEvent += NewLineReceived;
            decomposer.RecordStarted = true;
            decomposer.StartIndex = decomposer.MainIndex;

        }

        private void butStartRecord_Click(object sender, EventArgs e)
        {
            //            timerDelay.Interval = Cfg.StartDelay * 1000;
            UpdateRecInfoBox(CurrentRecordInfo);
            timerDelay.Enabled = true;
            DelayCounter = Cfg.StartDelay;
            labRecordSize.Text = "Start delay : " + DelayCounter.ToString() + " sec";
            labRecordSize.Visible = true;
            pbRecordProgress.Visible = true;
            pbRecordProgress.Value = 0;
            pbRecordProgress.Maximum = Cfg.StartDelay;
        }

        private void timerDelay_Tick(object sender, EventArgs e)
        {
            if (DelayCounter != 0)
            {
                DelayCounter--;
                pbRecordProgress.Value++;
                labRecordSize.Text = "Start delay : " + DelayCounter.ToString() + " sec";
            }
            if (DelayCounter == 0)
            {
                StartRecord();
                timerDelay.Enabled = false;
            }
        }

        private void UpdateScrollBar(int size)
        {
            int space = 14;
            hScrollBar1.Maximum = size;
            hScrollBar1.LargeChange = panelGraph.Width - space - 50;
            hScrollBar1.SmallChange = panelGraph.Width - space / 10;
            hScrollBar1.AutoSize = true;
            hScrollBar1.Value = 0;
            hScrollBar1.Visible = hScrollBar1.Maximum > hScrollBar1.Width;
        }

        private void StopRecord()
        {
            
            V.ResetVisirs();
            USBPort.WriteByte(ApexConstants.cmdStreamOff);
            CurrentRecordInfo.Data.Length = DataA.RInd;
            pbRecordProgress.Visible = false;
            labRecordSize.Visible = false;
            CurrentRecordInfo.Data.Length = (int)(decomposer.MainIndex - decomposer.StartIndex);
            CurrentRecordInfo.Save(Cfg);
            UpdateRecInfoBox(CurrentRecordInfo);
            decomposer.DecomposeLineEvent -= NewLineReceived;
            decomposer.DecomposeLineEvent -= LineReceived;
            //            String fname = filestream.Name;
            decomposer.RecordStarted = false;
            if (textSoundWriter != null)
            {
                textSoundWriter.Close();
                textSoundWriter.Dispose();
            }
            if (textWriter != null)
            {
                textWriter.Close();
                textWriter.Dispose();
            }
            string[] lines = File.ReadAllLines(Cfg.DataDir.ToString() + ApexConstants.TmpTextFile);
            File.AppendAllLines(CurrentRecordInfo.FileName, lines);

            ViewMode = true;
            timerRead.Enabled = false;
            ProcessCavi(CurrentRecordInfo);

/*            DataA = new DataArrays(lines.Length);
            decomposer = DataProcessing.ParseData(lines, DataA, 0);
            decomposer.CountViewArrays(lines.Length, Cfg.FilterOn);
            SetScale(lines.Length);
            for (int i = 0; i < AmpBarVals.Length; i++)
            {
                AmpBarVals[i] = 0;
            }
            for (int i = 0; i < ChannelsScaleY.Length; i++)
            {
                ChannelsScaleY[i] = 1;
            }
            for (int i = 0; i < AmpBarArr.Length; i++)
            {
                if (AmpBarArr[i] != null)
                {
                    AmpBarArr[i].Value = 0;
                }
            }*/
            UpdateScrollBar(lines.Length);
            foreach (GraphicsInfo gi in GInfoArr)
            {
                if (gi.Visible) gi.BufPanel.Refresh();
            }
            Cfg.DataFileNum++;
            ApexConfig.SaveConfig(Cfg);
            decomposer = null;

        }

        private void EditRecInfo()
        {
            if (decomposer == null) return;
            if (decomposer.RecordStarted)
            {
                if (textBoxRecInfo.Text.Length == 0) return;
                textBoxRecInfo.SelectionStart = 0;
                var ri = new RecordInfo(Cfg, CurrentFile);
                var fri = new FormRecordInfo(ri);
                if (fri.ShowDialog() == DialogResult.OK)
                {
                    ri = fri.GetDialogData();
                    ri.Save(Cfg);
                    UpdateRecInfoBox(ri);
                }
                fri.Dispose();
            }
            else
            {
                string[] lines = File.ReadAllLines(Cfg.DataDir.ToString() + CurrentFile, Encoding.Default);
                string[] ldata = lines.Skip(ApexConstants.HeaderSize).ToArray();
                string[] lrec = lines.Take(ApexConstants.HeaderSize).ToArray();
                var ri = new RecordInfo(Cfg, CurrentFile, lrec);
                var fri = new FormRecordInfo(ri);
                if (fri.ShowDialog() == DialogResult.OK)
                {
                    ri = fri.GetDialogData();
                    ri.Save(Cfg);
                    UpdateRecInfoBox(ri);
                }
                CurrentRecordInfo = ri;
                File.AppendAllLines(ri.FileName, ldata);
                fri.Dispose();
            }
        }

        private void trackBarECG_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                int ind = (int)_ampBarNames.a_ecg;
                AmpBarVals[ind] = AmpBarArr[ind].Value;
                double a = AmpBarArr[ind].Value;
                ChannelsScaleY[ind] = Math.Pow(2, a / 2);
                if (ViewMode)
                {
                    if (GInfoArr[(int)ChNames.chECG1].Visible) GInfoArr[(int)ChNames.chECG1].BufPanel.Refresh();
                    if (GInfoArr[(int)ChNames.chECG2].Visible) GInfoArr[(int)ChNames.chECG2].BufPanel.Refresh();
                }
            }
            catch (Exception)
            {
            }
        }

        private void trackBarReo_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                int ind = (int)_ampBarNames.a_reo;
                AmpBarVals[ind] = AmpBarArr[ind].Value;
                double a = AmpBarArr[ind].Value;
                ChannelsScaleY[ind] = Math.Pow(2, a / 2);
                if (ViewMode)
                {
                    if (GInfoArr[(int)ChNames.chReo1].Visible) GInfoArr[(int)ChNames.chReo1].BufPanel.Refresh();
                    if (GInfoArr[(int)ChNames.chReo2].Visible) GInfoArr[(int)ChNames.chReo2].BufPanel.Refresh();
                }
            }
            catch (Exception)
            {
            }
        }

        private void trackBarSphigmo12_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                int ind = (int)_ampBarNames.a_sphig12;
                AmpBarVals[ind] = AmpBarArr[ind].Value;
                double a = AmpBarArr[ind].Value;
                ChannelsScaleY[ind] = Math.Pow(2, a / 2);
                if (ViewMode)
                {
                    if (GInfoArr[(int)ChNames.chSphigmo1].Visible) GInfoArr[(int)ChNames.chSphigmo1].BufPanel.Refresh();
                    if (GInfoArr[(int)ChNames.chSphigmo2].Visible) GInfoArr[(int)ChNames.chSphigmo2].BufPanel.Refresh();
                }
            }
            catch (Exception)
            {
            }
        }
        private void trackBarSphigmo34_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                int ind = (int)_ampBarNames.a_sphig34;
                AmpBarVals[ind] = AmpBarArr[ind].Value;
                double a = AmpBarArr[ind].Value;
                ChannelsScaleY[ind] = Math.Pow(2, a / 2);
                if (ViewMode)
                {
                    if (GInfoArr[(int)ChNames.chSphigmo3].Visible) GInfoArr[(int)ChNames.chSphigmo3].BufPanel.Refresh();
                    if (GInfoArr[(int)ChNames.chSphigmo4].Visible) GInfoArr[(int)ChNames.chSphigmo4].BufPanel.Refresh();
                }
            }
            catch (Exception)
            {
            }
        }

        private void trackBarApex_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                int ind = (int)_ampBarNames.a_apex;
                AmpBarVals[ind] = AmpBarArr[ind].Value;
                double a = AmpBarArr[ind].Value;
                ChannelsScaleY[ind] = Math.Pow(2, a / 2);
                if (ViewMode)
                {
                    if (GInfoArr[(int)ChNames.chApex].Visible) GInfoArr[(int)ChNames.chApex].BufPanel.Refresh();
                }
            }
            catch (Exception)
            {
            }
        }

        private void trackBarPPG_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                int ind = (int)_ampBarNames.a_ppg;
                AmpBarVals[ind] = AmpBarArr[ind].Value;
                double a = AmpBarArr[ind].Value;
                ChannelsScaleY[ind] = Math.Pow(2, a / 2);
                if (ViewMode)
                {
                    if (GInfoArr[(int)ChNames.chPPG].Visible) GInfoArr[(int)ChNames.chPPG].BufPanel.Refresh();
                }
            }
            catch (Exception)
            {
            }
        }
        private void trackBarSound_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                int ind = (int)_ampBarNames.a_sound;
                AmpBarVals[ind] = AmpBarArr[ind].Value;
                double a = AmpBarArr[ind].Value;
                ChannelsScaleY[ind] = Math.Pow(2, a / 2);
                if (ViewMode)
                {
                    if (GInfoArr[(int)ChNames.chSound].Visible) GInfoArr[(int)ChNames.chSound].BufPanel.Refresh();
                }
            }
            catch (Exception)
            {
            }
        }




        private void butOpenFile_Click(object sender, EventArgs e)
        {
            openFileDialog1.FileName = "";
            openFileDialog1.InitialDirectory = Cfg.DataDir.ToString();
            textBoxRecInfo.SelectionStart = 0;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if (File.Exists(openFileDialog1.FileName))
                {
                    V.ResetVisirs();
                    Cfg.DataDir = Path.GetDirectoryName(openFileDialog1.FileName) + @"\";
                    ApexConfig.SaveConfig(Cfg);
                    ViewMode = true;
                    ViewShift = 0;
                    timerRead.Enabled = false;

                    CurrentFile = Path.GetFileName(openFileDialog1.FileName);
                    

                    string[] lines = File.ReadAllLines(openFileDialog1.FileName);
                    int size = lines.Count() - ApexConstants.HeaderSize;
                    if (size <= 0)
                    {
                        MessageBox.Show("Error reading file " + openFileDialog1.FileName);
                        return;
                    }
                    

                    DataA = new DataArrays(size + 1000);
                    decomposer = DataProcessing.ParseData(lines, DataA, ApexConstants.HeaderSize);
                    decomposer.CountViewArrays(lines.Length - ApexConstants.HeaderSize, Cfg.FilterOn);
                    SetScale(size);
                    var ri = new RecordInfo(Cfg, CurrentFile, lines);
                    ri.Data.Length = size;
                    UpdateDataFromRI(DataA, ri.Data);
                    UpdateRecInfoBox(ri);
                    CurrentRecordInfo = ri;
                    UpdateScrollBar(size);
                    foreach (GraphicsInfo gi in GInfoArr)
                    {
                        if (gi.Visible) gi.BufPanel.Refresh();
                    }
                    //                    textBoxRecInfo.SelectionStart = 0;

                    ProcessCavi(ri);
                }
            }

        }
        
        private void ProcessCavi(RecordInfo ri)
        {
            DataProcessing.DetectECG(DataA, decomposer.MainIndex);

            // Расчёт для левой стороны (берет NIBP1_Syst и NIBP1_Diast автоматически)
            var leftCavi = CaviProcessor.CalculateCavi(
                DataA,
                BodySide.Left,
                ri.Data.Height,
                ri.Data.Length);

            // Расчёт для правой стороны ( если АД правой руки в NIBP3)
            var rightCavi = CaviProcessor.CalculateCavi(
                DataA,
                BodySide.Right,
                ri.Data.Height,
                ri.Data.Length);
            using (FormResult fr = new FormResult(ri, leftCavi, rightCavi))
            {
                fr.ShowDialog();
            }
        }
        private void UpdateDataFromRI(DataArrays d, RecordInfoData recdata)
        {
            d.NIBP1_Syst = recdata.ABP1[0];
            d.NIBP1_Diast = recdata.ABP1[1];
            d.NIBP2_Syst = recdata.ABP2[0];
            d.NIBP2_Diast = recdata.ABP2[1];
            d.NIBP3_Syst = recdata.ABP3[0];
            d.NIBP3_Diast = recdata.ABP3[1];
            d.NIBP4_Syst = recdata.ABP4[0];
            d.NIBP4_Diast = recdata.ABP4[1];
        }
        private void SetScale(int size)
        {
            double coeff = 1.5;
            int r1 = 0;
            int r2 = 0;
            r1 = DataProcessing.GetRange(DataA.ECG1ViewArray, size);
            if (GInfoArr[1].Visible)
            {
                r2 = DataProcessing.GetRange(DataA.ECG2ViewArray, size);
            }
            Cfg.ChannelsMaxSize[0] = (int)(coeff * Math.Max(r1, r2));
            Cfg.ChannelsMaxSize[1] = (int)(coeff * Math.Max(r1, r2));
            r1 = 0;
            r2 = 0;
            r1 = DataProcessing.GetRange(DataA.Reo1ViewArray, size);
            if (GInfoArr[3].Visible)
            {
                r2 = DataProcessing.GetRange(DataA.Reo2ViewArray, size);
            }
            Cfg.ChannelsMaxSize[2] = (int)(coeff * Math.Max(r1, r2));
            Cfg.ChannelsMaxSize[3] = (int)(coeff * Math.Max(r1, r2));
            r1 = DataProcessing.GetRange(DataA.SphigmoLeftHandViewArray, size);
            if (GInfoArr[5].Visible)
            {
                r2 = DataProcessing.GetRange(DataA.SphigmoLeftLegViewArray, size);
            }
            Cfg.ChannelsMaxSize[4] = (int)(coeff * Math.Max(r1, r2));
            Cfg.ChannelsMaxSize[5] = (int)(coeff * Math.Max(r1, r2));
            Cfg.ChannelsMaxSize[6] = (int)(DataProcessing.GetRange(DataA.ApexViewArray, size) * coeff);
        }

        private void butStopFlow_Click(object sender, EventArgs e)
        {
            ViewMode = true;
            timerRead.Enabled = false;
            timerPaint.Enabled = false;
            USBPort.WriteByte(ApexConstants.cmdStreamOff);
        }

        private void hScrollBar1_ValueChanged(object sender, EventArgs e)
        {
            ViewShift = hScrollBar1.Value;
        }

        private void butEditRecInfo_Click(object sender, EventArgs e)
        {
            EditRecInfo();
        }


        private void numUDplevel1_ValueChanged(object sender, EventArgs e)
        {
            Cfg.PressLevel1 = (byte)numUDplevel1.Value;
            USBPort.WriteByte(ApexConstants.cmdSetPressSfig1);
            USBPort.WriteByte(Cfg.PressLevel1);
        }

        private void numUDplevel2_ValueChanged(object sender, EventArgs e)
        {
            Cfg.PressLevel2 = (byte)numUDplevel2.Value;
            USBPort.WriteByte(ApexConstants.cmdSetPressSfig2);
            USBPort.WriteByte(Cfg.PressLevel2);
        }

        private void butPumps12Start_Click(object sender, EventArgs e)
        {
            USBPort.WriteByte(ApexConstants.cmdSetPressSfig1);
            USBPort.WriteByte(Cfg.PressLevel1);
            USBPort.WriteByte(ApexConstants.cmdSetPressSfig2);
            USBPort.WriteByte(Cfg.PressLevel2);
            USBPort.WriteByte(ApexConstants.cmdStartSfig1);
            USBPort.WriteByte(ApexConstants.cmdStartSfig2);
        }

        private void butPumps34Start_Click(object sender, EventArgs e)
        {
            USBPort.WriteByte(ApexConstants.cmdSetPressSfig3);
            USBPort.WriteByte(Cfg.PressLevel3);
            USBPort.WriteByte(ApexConstants.cmdSetPressSfig4);
            USBPort.WriteByte(Cfg.PressLevel4);
            USBPort.WriteByte(ApexConstants.cmdStartSfig3);
            USBPort.WriteByte(ApexConstants.cmdStartSfig4);
        }
        private void butPump1start_Click(object sender, EventArgs e)
        {
            USBPort.WriteByte(ApexConstants.cmdSetPressSfig1);
            USBPort.WriteByte(Cfg.PressLevel1);
            USBPort.WriteByte(ApexConstants.cmdStartSfig1);
        }

        private void butPump2start_Click(object sender, EventArgs e)
        {
            USBPort.WriteByte(ApexConstants.cmdSetPressSfig2);
            USBPort.WriteByte(Cfg.PressLevel2);
            USBPort.WriteByte(ApexConstants.cmdStartSfig2);
        }

        private void butPumps12Stop_Click(object sender, EventArgs e)
        {
            USBPort.WriteByte(ApexConstants.cmdStopSfig1);
            USBPort.WriteByte(ApexConstants.cmdStopSfig2);
        }

        private void butPumps34Stop_Click(object sender, EventArgs e)
        {
            USBPort.WriteByte(ApexConstants.cmdStopSfig3);
            USBPort.WriteByte(ApexConstants.cmdStopSfig4);
        }

        private void butPump1stop_Click(object sender, EventArgs e)
        {
            USBPort.WriteByte(ApexConstants.cmdStopSfig1);
        }

        private void butPump2stop_Click(object sender, EventArgs e)
        {
            USBPort.WriteByte(ApexConstants.cmdStopSfig2);
        }

        private void butBPMeas_Click(object sender, EventArgs e)
        {
            USBPort.WriteByte(ApexConstants.cmdStartBP1);
            DataA.bp1status = 1;
        }

        private void butBPabort_Click(object sender, EventArgs e)
        {
            USBPort.WriteByte(ApexConstants.cmdCancelBP1);
            DataA.bp1status = 0;
        }

        private void butBPmeas2_Click(object sender, EventArgs e)
        {
            USBPort.WriteByte(ApexConstants.cmdStartBP2);
            DataA.bp2status = 1;
        }

        private void butBPabort2_Click(object sender, EventArgs e)
        {
            USBPort.WriteByte(ApexConstants.cmdCancelBP2);
            DataA.bp2status = 0;
        }

        private void butBPmeas3_Click(object sender, EventArgs e)
        {
            USBPort.WriteByte(ApexConstants.cmdStartBP3);
            DataA.bp3status = 1;
        }

        private void butBPabort3_Click(object sender, EventArgs e)
        {
            USBPort.WriteByte(ApexConstants.cmdCancelBP3);
            DataA.bp3status = 0;
        }

        private void butBPmeas4_Click(object sender, EventArgs e)
        {
            USBPort.WriteByte(ApexConstants.cmdStartBP4);
            DataA.bp4status = 1;
        }

        private void butBPabort4_Click(object sender, EventArgs e)
        {
            USBPort.WriteByte(ApexConstants.cmdCancelBP4);
            DataA.bp4status = 0;
        }

        private void butPump3start_Click(object sender, EventArgs e)
        {
            USBPort.WriteByte(ApexConstants.cmdSetPressSfig3);
            USBPort.WriteByte(Cfg.PressLevel3);
            USBPort.WriteByte(ApexConstants.cmdStartSfig3);
        }

        private void butPump4start_Click(object sender, EventArgs e)
        {
            USBPort.WriteByte(ApexConstants.cmdSetPressSfig4);
            USBPort.WriteByte(Cfg.PressLevel4);
            USBPort.WriteByte(ApexConstants.cmdStartSfig4);
        }

        private void butPump3stop_Click(object sender, EventArgs e)
        {
            USBPort.WriteByte(ApexConstants.cmdStopSfig3);
        }

        private void butPump4stop_Click(object sender, EventArgs e)
        {
            USBPort.WriteByte(ApexConstants.cmdStopSfig4);
        }

        private void numUDplevel3_ValueChanged(object sender, EventArgs e)
        {
            Cfg.PressLevel3 = (byte)numUDplevel3.Value;
            USBPort.WriteByte(ApexConstants.cmdSetPressSfig3);
            USBPort.WriteByte(Cfg.PressLevel3);
        }

        private void numUDplevel4_ValueChanged(object sender, EventArgs e)
        {
            Cfg.PressLevel4 = (byte)numUDplevel4.Value;
            USBPort.WriteByte(ApexConstants.cmdSetPressSfig4);
            USBPort.WriteByte(Cfg.PressLevel4);
        }

        private void butStartFlow_Click(object sender, EventArgs e)
        {
            InitArraysForFlow();
            ViewMode = false;
            timerRead.Enabled = true;
            timerPaint.Enabled = true;
            decomposer.MainIndex = 0;
            USBPort.WriteByte(ApexConstants.cmdStreamOn);
        }

        private void butImitatorOn_Click(object sender, EventArgs e)
        {
            USBPort.WriteByte(ApexConstants.cmdImitatorOn);
        }

        private void butImitatorOff_Click(object sender, EventArgs e)
        {
            USBPort.WriteByte(ApexConstants.cmdImitatorOff);
        }        
    }
}

