using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using WpfControlLibrary1;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool IsRun = false;
        private CancellationTokenSource cts = new CancellationTokenSource();
        private readonly ControlUiData ControlUiData;

        public MainWindow()
        {
            InitializeComponent();

            ControlUiData = mainUiControl.ControlUiData;

            Task.Factory.StartNew(async () => { await ThreadTrayActuator12(); }, TaskCreationOptions.LongRunning);
            Task.Factory.StartNew(async () => { await ThreadTrayActuator34(); }, TaskCreationOptions.LongRunning);
            Task.Factory.StartNew(async () => { await ThreadInFeedBelt(); }, TaskCreationOptions.LongRunning);
            Task.Factory.StartNew(async () => { await ThreadOutFeedBelt(); }, TaskCreationOptions.LongRunning);
           
        }

        #region 进料皮带半片
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ControlUiData.infeedBelt1 = !ControlUiData.infeedBelt1;
            mainUiControl.RefershUi();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ControlUiData.infeedBelt2 = !ControlUiData.infeedBelt2;
            mainUiControl.RefershUi();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            ControlUiData.infeedBelt3 = !ControlUiData.infeedBelt3;
            mainUiControl.RefershUi();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            ControlUiData.infeedBelt4 = !ControlUiData.infeedBelt4;
            mainUiControl.RefershUi();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            ControlUiData.infeedBelt5 = !ControlUiData.infeedBelt5;
            mainUiControl.RefershUi();
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            ControlUiData.infeedBelt6 = !ControlUiData.infeedBelt6;
            mainUiControl.RefershUi();
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            ControlUiData.infeedBelt7 = !ControlUiData.infeedBelt7;
            mainUiControl.RefershUi();
        }

        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            ControlUiData.infeedBelt8 = !ControlUiData.infeedBelt8;
            mainUiControl.RefershUi();
        }
        #endregion

        #region 出料皮带半片
        private void Button_Click_8(object sender, RoutedEventArgs e)
        {
            ControlUiData.outfeedBelt1 = !ControlUiData.outfeedBelt1;
            mainUiControl.RefershUi();
        }

        private void Button_Click_9(object sender, RoutedEventArgs e)
        {
            ControlUiData.outfeedBelt2 = !ControlUiData.outfeedBelt2;
            mainUiControl.RefershUi();
        }

        private void Button_Click_10(object sender, RoutedEventArgs e)
        {
            ControlUiData.outfeedBelt3 = !ControlUiData.outfeedBelt3;
            mainUiControl.RefershUi();
        }

        private void Button_Click_11(object sender, RoutedEventArgs e)
        {
            ControlUiData.outfeedBelt4 = !ControlUiData.outfeedBelt4;
            mainUiControl.RefershUi();
        }

        private void Button_Click_12(object sender, RoutedEventArgs e)
        {
            ControlUiData.outfeedBelt5 = !ControlUiData.outfeedBelt5;
            mainUiControl.RefershUi();
        }

        private void Button_Click_13(object sender, RoutedEventArgs e)
        {
            ControlUiData.outfeedBelt6 = !ControlUiData.outfeedBelt6;
            mainUiControl.RefershUi();
        }

        private void Button_Click_14(object sender, RoutedEventArgs e)
        {
            ControlUiData.outfeedBelt7 = !ControlUiData.outfeedBelt7;
            mainUiControl.RefershUi();
        }

        private void Button_Click_15(object sender, RoutedEventArgs e)
        {
            ControlUiData.outfeedBelt8 = !ControlUiData.outfeedBelt8;
            mainUiControl.RefershUi();
        }
        #endregion

        private void Button_Click_16(object sender, RoutedEventArgs e)
        {
            ControlUiData.trayActuator1.jackFeed = !ControlUiData.trayActuator1.jackFeed;
            mainUiControl.RefershUi();
        }

        private void Button_Click_17(object sender, RoutedEventArgs e)
        {
            ControlUiData.trayActuator1.xpos += 400;
            mainUiControl.RefershUi();
        }

        private void Button_Click_18(object sender, RoutedEventArgs e)
        {
            ControlUiData.trayActuator1.xpos = 1000;
            mainUiControl.RefershUi();
        }

        private void Button_Click_19(object sender, RoutedEventArgs e)
        {
            ControlUiData.trayActuator2.jackFeed = !ControlUiData.trayActuator2.jackFeed;
            mainUiControl.RefershUi();
        }

        private void Button_Click_20(object sender, RoutedEventArgs e)
        {
            ControlUiData.trayActuator2.xpos += 400;
            mainUiControl.RefershUi();
        }

        private void Button_Click_21(object sender, RoutedEventArgs e)
        {
            ControlUiData.trayActuator2.xpos = 1000;
            mainUiControl.RefershUi();
        }

        private void Button_Click_22(object sender, RoutedEventArgs e)
        {
            ControlUiData.trayActuator3.jackFeed = !ControlUiData.trayActuator3.jackFeed;
            mainUiControl.RefershUi();
        }

        private void Button_Click_23(object sender, RoutedEventArgs e)
        {
            ControlUiData.trayActuator3.xpos += 400;
            mainUiControl.RefershUi();
        }

        private void Button_Click_24(object sender, RoutedEventArgs e)
        {
            ControlUiData.trayActuator3.xpos = 1000;
            mainUiControl.RefershUi();
        }

        private void Button_Click_25(object sender, RoutedEventArgs e)
        {
            ControlUiData.trayActuator4.jackFeed = !ControlUiData.trayActuator4.jackFeed;
            mainUiControl.RefershUi();
        }

        private void Button_Click_27(object sender, RoutedEventArgs e)
        {
            ControlUiData.trayActuator4.xpos += 400;
            mainUiControl.RefershUi();
        }
        private void Button_Click_26(object sender, RoutedEventArgs e)
        {
            ControlUiData.trayActuator4.xpos = 1000;
            mainUiControl.RefershUi();
        }

        private void Button_Click_28(object sender, RoutedEventArgs e)
        {
            ControlUiData.liftingFeedArm1.existFeed = !ControlUiData.liftingFeedArm1.existFeed;
            mainUiControl.RefershUi();
        }

        private void Button_Click_29(object sender, RoutedEventArgs e)
        {
            ControlUiData.liftingFeedArm1.posLeft = !ControlUiData.liftingFeedArm1.posLeft;
            mainUiControl.RefershUi();
        }

        private void Button_Click_30(object sender, RoutedEventArgs e)
        {
            ControlUiData.liftingFeedArm2.existFeed = !ControlUiData.liftingFeedArm2.existFeed;
            mainUiControl.RefershUi();
        }

        private void Button_Click_31(object sender, RoutedEventArgs e)
        {
            ControlUiData.liftingFeedArm2.posLeft = !ControlUiData.liftingFeedArm2.posLeft;
            mainUiControl.RefershUi();
        }

        private void Button_Click_32(object sender, RoutedEventArgs e)
        {
            ControlUiData.printing1Working = !ControlUiData.printing1Working;
            mainUiControl.RefershUi();
        }

        private void Button_Click_33(object sender, RoutedEventArgs e)
        {
            ControlUiData.printing2Working = !ControlUiData.printing2Working;
            mainUiControl.RefershUi();
        }


        private void Button_Click_34(object sender, RoutedEventArgs e)
        {
            int a = (int)ControlUiData.cameraLocalization1;
            a += 1;
            if (a > 4)
            {
                a = 0;
            }
            ControlUiData.cameraLocalization1 = (CameraStatus)a;
            mainUiControl.RefershUi();
        }

        private void Button_Click_35(object sender, RoutedEventArgs e)
        {
            int a = (int)ControlUiData.cameraLocalization2;
            a += 1;
            if (a > 4)
            {
                a = 0;
            }
            ControlUiData.cameraLocalization2 = (CameraStatus)a;
            mainUiControl.RefershUi();
        }

        private void Button_Click_36(object sender, RoutedEventArgs e)
        {
            int a = (int)ControlUiData.cameraDetect1;
            a += 1;
            if (a > 4)
            {
                a = 0;
            }
            ControlUiData.cameraDetect1 = (CameraStatus)a;
            mainUiControl.RefershUi();

        }

        private void Button_Click_37(object sender, RoutedEventArgs e)
        {
            int a = (int)ControlUiData.cameraDetect2;
            a += 1;
            if (a > 4)
            {
                a = 0;
            }
            ControlUiData.cameraDetect2 = (CameraStatus)a;
            mainUiControl.RefershUi();
        }

        private void Button_Click_38(object sender, RoutedEventArgs e)
        {
            int a = (int)ControlUiData.cameraAoiOutfeed;
            a += 1;
            if (a > 4)
            {
                a = 0;
            }
            ControlUiData.cameraAoiOutfeed = (CameraStatus)a;
            mainUiControl.RefershUi();
        }

        private void Button_Click_39(object sender, RoutedEventArgs e)
        {
            ControlUiData.NgHalfSliceNum += 1;
            if (ControlUiData.NgHalfSliceNum > 4)
            {
                ControlUiData.NgHalfSliceNum = 0;
            }
            mainUiControl.RefershUi();
        }



        /// <summary>
        /// 启动
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_40(object sender, RoutedEventArgs e)
        {
            IsRun = true;
        }


        /// <summary>
        /// 停止
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_41(object sender, RoutedEventArgs e)
        {
            IsRun = false;
        }


        int printing1Pos = 4500;
        int printing2Pos = 7150;

        int xpos1 = 1000;
        int xpos2 = 2600;
        int xpos3 = 8400;
        int xpos4 = 10000;

        #region 动子12
        public async Task ThreadTrayActuator12()
        {
            WorkFlow workFlow = WorkFlow.动子移动到初始位;
            ControlUiData.trayActuator1.xpos = xpos1;
            ControlUiData.trayActuator2.xpos = xpos2;
            //ControlUiData.trayActuator3.xpos = xpos3;
            //ControlUiData.trayActuator4.xpos = xpos4;


            //ControlUiData.trayActuator3.zpos = 2000;
            //ControlUiData.trayActuator4.zpos = 2000;


            mainUiControl.RefershUi();

            bool isRefersh = false;
            bool isReferhsPos1 = false;
            bool isReferhsPos2 = false;
            bool isRightMove1 = true;
            bool isRightMove2 = true;

            while (!cts.IsCancellationRequested)
            {
                if (IsRun)
                {
                    try
                    {
                        if (isReferhsPos1)
                        {
                            if (ControlUiData.trayActuator1.xpos > xpos3 + 200)
                            {
                                if (isRightMove1)
                                {
                                    isRightMove1 = false;
                                }
                            }
                            if (ControlUiData.trayActuator1.xpos < xpos1 - 200)
                            {
                                if (!isRightMove1)
                                {
                                    isRightMove1 = true;
                                }
                            }
                            if (isRightMove1)
                            {
                                ControlUiData.trayActuator1.xpos += 50;
                            }
                            else
                            {
                                ControlUiData.trayActuator1.xpos -= 50;
                            }
                            mainUiControl.RefershUi();
                            isReferhsPos1 = false;
                        }
                        if (isReferhsPos2)
                        {
                            if (ControlUiData.trayActuator2.xpos > xpos4 + 200)
                            {
                                if (isRightMove2)
                                {
                                    isRightMove2 = false;
                                }
                            }
                            if (ControlUiData.trayActuator2.xpos < xpos2 - 200)
                            {
                                if (!isRightMove2)
                                {
                                    isRightMove2 = true;
                                }
                            }
                            if (isRightMove2)
                            {
                                ControlUiData.trayActuator2.xpos += 50;
                            }
                            else
                            {
                                ControlUiData.trayActuator2.xpos -= 50;
                            }
                            mainUiControl.RefershUi();
                            isReferhsPos2 = false;
                        }
                        if (isRefersh)
                        {
                            mainUiControl.RefershUi();
                            isRefersh = false;
                        }

                        switch (workFlow)
                        {
                            case WorkFlow.动子移动到初始位:
                                if (ControlUiData.trayActuator1.xpos <= xpos1 && ControlUiData.trayActuator2.xpos <= xpos2)
                                {
                                    if (ControlUiData.infeedBelt5 && ControlUiData.infeedBelt6 && ControlUiData.infeedBelt7 && ControlUiData.infeedBelt8)
                                    {
                                        workFlow = WorkFlow.抬料臂1移动;
                                    }
                                }
                                else
                                {
                                    if (ControlUiData.trayActuator1.xpos > xpos1)
                                    {
                                        if (!isReferhsPos1)
                                        {
                                            isReferhsPos1 = true;
                                        }
                                    }
                                    if (ControlUiData.trayActuator2.xpos > xpos2)
                                    {
                                        if (!isReferhsPos2)
                                        {
                                            isReferhsPos2 = true;
                                        }
                                    }
                                }
                                break;
                            case WorkFlow.抬料臂1移动:
                                if (!ControlUiData.liftingFeedArm1.posLeft)
                                {
                                    ControlUiData.liftingFeedArm1.posLeft = true;
                                    mainUiControl.RefershUi();
                                    await Task.Delay(1000);
                                }
                                else
                                {
                                    if (!ControlUiData.liftingFeedArm1.existFeed)
                                    {
                                        ControlUiData.liftingFeedArm1.existFeed = true;
                                        ControlUiData.infeedBelt5 = false;
                                        ControlUiData.infeedBelt6 = false;
                                        ControlUiData.infeedBelt7 = false;
                                        ControlUiData.infeedBelt8 = false;
                                        mainUiControl.RefershUi();
                                        await Task.Delay(200);
                                    }
                                    else
                                    {
                                        if (ControlUiData.liftingFeedArm1.posLeft)
                                        {
                                            ControlUiData.liftingFeedArm1.posLeft = false;
                                            mainUiControl.RefershUi();
                                            await Task.Delay(1000);
                                            workFlow = WorkFlow.动子接料;
                                        }
                                    }
                                }

                                break;
                            case WorkFlow.动子接料:
                                if (!ControlUiData.trayActuator1.jackFeed || !ControlUiData.trayActuator2.jackFeed)
                                {
                                    ControlUiData.trayActuator1.jackFeed = true;
                                    ControlUiData.trayActuator2.jackFeed = true;
                                    mainUiControl.RefershUi();
                                    await Task.Delay(500);
                                }
                                else
                                {
                                    ControlUiData.liftingFeedArm1.existFeed = false;
                                    mainUiControl.RefershUi();
                                    if (!actuator34Run)
                                    {
                                        actuator34Run = true;
                                    }
                                    workFlow = WorkFlow.动子移动到印刷位;
                                }
                                break;
                            case WorkFlow.动子移动到印刷位:

                                if (ControlUiData.trayActuator1.xpos == printing1Pos && ControlUiData.trayActuator2.xpos == printing2Pos)
                                {
                                    ControlUiData.printing1Working = true;
                                    ControlUiData.printing2Working = true;
                                    mainUiControl.RefershUi();
                                    await Task.Delay(1000);


                                    ControlUiData.printing1Working = false;
                                    ControlUiData.printing2Working = false;
                                    mainUiControl.RefershUi();
                                    await Task.Delay(1000);
                                    workFlow = WorkFlow.动子移动到放料位;
                                }
                                else
                                {
                                    if (ControlUiData.trayActuator1.xpos != printing1Pos)
                                    {
                                        isReferhsPos1 = true;
                                    }
                                    if (ControlUiData.trayActuator2.xpos != printing2Pos)
                                    {
                                        isReferhsPos2 = true;
                                    }
                                }
                                break;
                            case WorkFlow.动子移动到放料位:
                                if (ControlUiData.trayActuator1.xpos >= xpos3 && ControlUiData.trayActuator2.xpos >= xpos4)
                                {
                                    workFlow = WorkFlow.抬料臂2移动;
                                }
                                else
                                {
                                    if (ControlUiData.trayActuator1.xpos < xpos3)
                                    {
                                        isReferhsPos1 = true;
                                    }
                                    if (ControlUiData.trayActuator2.xpos < xpos4)
                                    {
                                        isReferhsPos2 = true;
                                    }
                                }
                                break;
                            case WorkFlow.抬料臂2移动:
                                if (!ControlUiData.liftingFeedArm2.posLeft)
                                {
                                    ControlUiData.liftingFeedArm2.posLeft = true;
                                    mainUiControl.RefershUi();
                                    await Task.Delay(1000);
                                }
                                else
                                {
                                    workFlow = WorkFlow.动子放料;
                                }
                                break;
                            case WorkFlow.动子放料:
                                if (ControlUiData.trayActuator1.jackFeed || ControlUiData.trayActuator2.jackFeed)
                                {
                                    ControlUiData.trayActuator1.jackFeed = false;
                                    ControlUiData.trayActuator2.jackFeed = false;

                                    ControlUiData.liftingFeedArm2.existFeed = true;
                                    mainUiControl.RefershUi();
                                    await Task.Delay(1000);
                                }
                                else
                                {
                                    if (ControlUiData.liftingFeedArm2.posLeft)
                                    {
                                        ControlUiData.liftingFeedArm2.posLeft = false;
                                        mainUiControl.RefershUi();
                                        await Task.Delay(500);
                                    }
                                    else
                                    {
                                        if (ControlUiData.liftingFeedArm2.existFeed)
                                        {
                                            ControlUiData.liftingFeedArm2.existFeed = false;
                                            ControlUiData.outfeedBelt1 = true;
                                            ControlUiData.outfeedBelt2 = true;
                                            ControlUiData.outfeedBelt3 = true;
                                            ControlUiData.outfeedBelt4 = true;
                                            mainUiControl.RefershUi();
                                        }
                                        else
                                        {
                                            workFlow = WorkFlow.出料12位置有半片;
                                        }
                                    }
                                }
                                break;
                            case WorkFlow.出料12位置有半片:
                                workFlow = WorkFlow.动子移动到初始位;
                                break;
                            default:
                                break;
                        }

                        await Task.Delay(10);
                    }
                    catch (System.Exception ex)
                    {
                        await Task.Delay(1000);
                    }
                }
                else
                {
                    await Task.Delay(1000);
                }

            }
        }

        #endregion

        #region 动子34
        bool actuator34Run = false;
        public async Task ThreadTrayActuator34()
        {
            WorkFlow workFlow = WorkFlow.动子移动到初始位;
            ControlUiData.trayActuator3.xpos = xpos3;
            ControlUiData.trayActuator4.xpos = xpos4;

            mainUiControl.RefershUi();

            bool isRefersh = false;
            bool isReferhsPos1 = false;
            bool isReferhsPos2 = false;
            bool isRightMove1 = true;
            bool isRightMove2 = true;

            while (!cts.IsCancellationRequested)
            {
                if (IsRun && actuator34Run)
                {
                    try
                    {
                        if (isReferhsPos1)
                        {
                            if (ControlUiData.trayActuator3.xpos > xpos3 + 200)
                            {
                                if (isRightMove1)
                                {
                                    isRightMove1 = false;
                                }
                            }
                            if (ControlUiData.trayActuator3.xpos < xpos1 - 200)
                            {
                                if (!isRightMove1)
                                {
                                    isRightMove1 = true;
                                }
                            }
                            if (isRightMove1)
                            {
                                ControlUiData.trayActuator3.xpos += 50;
                            }
                            else
                            {
                                ControlUiData.trayActuator3.xpos -= 50;
                            }
                            mainUiControl.RefershUi();
                            isReferhsPos1 = false;
                        }
                        if (isReferhsPos2)
                        {
                            if (ControlUiData.trayActuator4.xpos > xpos4 + 200)
                            {
                                if (isRightMove2)
                                {
                                    isRightMove2 = false;
                                }
                            }
                            if (ControlUiData.trayActuator4.xpos < xpos2 - 200)
                            {
                                if (!isRightMove2)
                                {
                                    isRightMove2 = true;
                                }
                            }
                            if (isRightMove2)
                            {
                                ControlUiData.trayActuator4.xpos += 50;
                            }
                            else
                            {
                                ControlUiData.trayActuator4.xpos -= 50;
                            }
                            mainUiControl.RefershUi();
                            isReferhsPos2 = false;
                        }
                        if (isRefersh)
                        {
                            mainUiControl.RefershUi();
                            isRefersh = false;
                        }

                        switch (workFlow)
                        {
                            case WorkFlow.动子移动到初始位:
                                if (ControlUiData.trayActuator3.xpos <= xpos1 && ControlUiData.trayActuator4.xpos <= xpos2)
                                {
                                    if (ControlUiData.infeedBelt5 && ControlUiData.infeedBelt6 && ControlUiData.infeedBelt7 && ControlUiData.infeedBelt8)
                                    {
                                        workFlow = WorkFlow.抬料臂1移动;
                                    }
                                }
                                else
                                {
                                    if (ControlUiData.trayActuator3.xpos > xpos1)
                                    {
                                        if (!isReferhsPos1)
                                        {
                                            isReferhsPos1 = true;
                                        }
                                    }
                                    if (ControlUiData.trayActuator4.xpos > xpos2)
                                    {
                                        if (!isReferhsPos2)
                                        {
                                            isReferhsPos2 = true;
                                        }
                                    }
                                }
                                break;
                            case WorkFlow.抬料臂1移动:
                                if (!ControlUiData.liftingFeedArm1.posLeft)
                                {
                                    ControlUiData.liftingFeedArm1.posLeft = true;
                                    mainUiControl.RefershUi();
                                    await Task.Delay(1000);
                                }
                                else
                                {
                                    if (!ControlUiData.liftingFeedArm1.existFeed)
                                    {
                                        ControlUiData.liftingFeedArm1.existFeed = true;
                                        ControlUiData.infeedBelt5 = false;
                                        ControlUiData.infeedBelt6 = false;
                                        ControlUiData.infeedBelt7 = false;
                                        ControlUiData.infeedBelt8 = false;
                                        mainUiControl.RefershUi();
                                        await Task.Delay(200);
                                    }
                                    else
                                    {
                                        if (ControlUiData.liftingFeedArm1.posLeft)
                                        {
                                            ControlUiData.liftingFeedArm1.posLeft = false;
                                            mainUiControl.RefershUi();
                                            await Task.Delay(1000);
                                            workFlow = WorkFlow.动子接料;
                                        }
                                    }
                                }

                                break;
                            case WorkFlow.动子接料:
                                if (!ControlUiData.trayActuator3.jackFeed || !ControlUiData.trayActuator4.jackFeed)
                                {
                                    ControlUiData.trayActuator3.jackFeed = true;
                                    ControlUiData.trayActuator4.jackFeed = true;
                                    mainUiControl.RefershUi();
                                    await Task.Delay(500);
                                }
                                else
                                {
                                    ControlUiData.liftingFeedArm1.existFeed = false;
                                    mainUiControl.RefershUi();
                                    workFlow = WorkFlow.动子移动到印刷位;
                                }
                                break;
                            case WorkFlow.动子移动到印刷位:

                                if (ControlUiData.trayActuator3.xpos == printing1Pos && ControlUiData.trayActuator4.xpos == printing2Pos)
                                {
                                    ControlUiData.printing1Working = true;
                                    ControlUiData.printing2Working = true;
                                    mainUiControl.RefershUi();
                                    await Task.Delay(1000);


                                    ControlUiData.printing1Working = false;
                                    ControlUiData.printing2Working = false;
                                    mainUiControl.RefershUi();
                                    await Task.Delay(1000);
                                    workFlow = WorkFlow.动子移动到放料位;
                                }
                                else
                                {
                                    if (ControlUiData.trayActuator3.xpos != printing1Pos)
                                    {
                                        isReferhsPos1 = true;
                                    }
                                    if (ControlUiData.trayActuator4.xpos != printing2Pos)
                                    {
                                        isReferhsPos2 = true;
                                    }
                                }
                                break;
                            case WorkFlow.动子移动到放料位:
                                if (ControlUiData.trayActuator3.xpos >= xpos3 && ControlUiData.trayActuator4.xpos >= xpos4)
                                {
                                    workFlow = WorkFlow.抬料臂2移动;
                                }
                                else
                                {
                                    if (ControlUiData.trayActuator3.xpos < xpos3)
                                    {
                                        isReferhsPos1 = true;
                                    }
                                    if (ControlUiData.trayActuator4.xpos < xpos4)
                                    {
                                        isReferhsPos2 = true;
                                    }
                                }
                                break;
                            case WorkFlow.抬料臂2移动:
                                if (!ControlUiData.liftingFeedArm2.posLeft)
                                {
                                    ControlUiData.liftingFeedArm2.posLeft = true;
                                    mainUiControl.RefershUi();
                                    await Task.Delay(1000);
                                }
                                else
                                {
                                    workFlow = WorkFlow.动子放料;
                                }
                                break;
                            case WorkFlow.动子放料:
                                if (ControlUiData.trayActuator3.jackFeed || ControlUiData.trayActuator4.jackFeed)
                                {
                                    ControlUiData.trayActuator3.jackFeed = false;
                                    ControlUiData.trayActuator4.jackFeed = false;

                                    ControlUiData.liftingFeedArm2.existFeed = true;
                                    mainUiControl.RefershUi();
                                    await Task.Delay(1000);
                                }
                                else
                                {
                                    if (ControlUiData.liftingFeedArm2.posLeft)
                                    {
                                        ControlUiData.liftingFeedArm2.posLeft = false;
                                        mainUiControl.RefershUi();
                                        await Task.Delay(500);
                                    }
                                    else
                                    {
                                        if (ControlUiData.liftingFeedArm2.existFeed)
                                        {
                                            ControlUiData.liftingFeedArm2.existFeed = false;
                                            ControlUiData.outfeedBelt1 = true;
                                            ControlUiData.outfeedBelt2 = true;
                                            ControlUiData.outfeedBelt3 = true;
                                            ControlUiData.outfeedBelt4 = true;
                                            mainUiControl.RefershUi();
                                        }
                                        else
                                        {
                                            workFlow = WorkFlow.出料12位置有半片;
                                        }
                                    }
                                }
                                break;
                            case WorkFlow.出料12位置有半片:
                                workFlow = WorkFlow.动子移动到初始位;
                                break;
                            default:
                                break;
                        }

                        await Task.Delay(10);
                    }
                    catch (System.Exception ex)
                    {
                        await Task.Delay(1000);
                    }
                }
                else
                {
                    await Task.Delay(1000);
                }

            }
        }
        #endregion

        #region 进料皮带
        int inNum = 0;
        public async Task ThreadInFeedBelt()
        {
            bool isRefersh = false;
            bool isRefershNmu = false;
            while (!cts.IsCancellationRequested)
            {
                if (IsRun)
                {
                    if (isRefersh)
                    {
                        mainUiControl.RefershUi();
                        isRefersh = false;
                    }
                    if (!ControlUiData.infeedBelt5 && !ControlUiData.infeedBelt6 && !ControlUiData.infeedBelt7 && !ControlUiData.infeedBelt8)
                    {
                        isRefershNmu = true;
                    }
                    if (isRefershNmu)
                    {
                        await Task.Delay(800);
                        inNum++;
                        switch (inNum)
                        {
                            case 1:
                                if (!ControlUiData.infeedBelt1)
                                {
                                    ControlUiData.infeedBelt1 = true;
                                    isRefersh = true;
                                }
                                if (!ControlUiData.infeedBelt2)
                                {
                                    ControlUiData.infeedBelt2 = true;
                                    isRefersh = true;
                                }
                                break;
                            case 2:
                                if (!ControlUiData.infeedBelt3)
                                {
                                    ControlUiData.infeedBelt3 = true;
                                    isRefersh = true;
                                }
                                if (!ControlUiData.infeedBelt4)
                                {
                                    ControlUiData.infeedBelt4 = true;
                                    isRefersh = true;
                                }
                                break;
                            case 3:
                                if (!ControlUiData.infeedBelt5)
                                {
                                    ControlUiData.infeedBelt5 = true;
                                    isRefersh = true;
                                }
                                if (!ControlUiData.infeedBelt6)
                                {
                                    ControlUiData.infeedBelt6 = true;
                                    isRefersh = true;
                                }
                                break;
                            case 4:
                                if (!ControlUiData.infeedBelt7)
                                {
                                    ControlUiData.infeedBelt7 = true;
                                    isRefersh = true;
                                }
                                if (!ControlUiData.infeedBelt8)
                                {
                                    ControlUiData.infeedBelt8 = true;
                                    isRefersh = true;
                                }
                                isRefershNmu = false;
                                inNum = 0;
                                break;
                        }
                    }
                    await Task.Delay(50);
                }
                else
                {
                    await Task.Delay(1000);
                }
            }
        }
        #endregion


        #region 出料皮带
        int outNum = 0;
        public async Task ThreadOutFeedBelt()
        {
            bool isRefershNum = false;
            int num = 0;
            while (!cts.IsCancellationRequested)
            {
                if (IsRun)
                {
                    if (ControlUiData.outfeedBelt1 && ControlUiData.outfeedBelt2 && ControlUiData.outfeedBelt3 && ControlUiData.outfeedBelt4)
                    {
                        isRefershNum = true;
                    }
                    if (isRefershNum)
                    {
                        num++;
                        await Task.Delay(800);
                        switch (num)
                        {
                            case 1:
                                ControlUiData.outfeedBelt1 = false;
                                ControlUiData.outfeedBelt2 = false;
                                ControlUiData.outfeedBelt5 = true;
                                ControlUiData.outfeedBelt6 = true;
                                mainUiControl.RefershUi();
                                break;
                            case 2:
                                ControlUiData.outfeedBelt3 = false;
                                ControlUiData.outfeedBelt4 = false;
                                ControlUiData.outfeedBelt7 = true;
                                ControlUiData.outfeedBelt8 = true;
                                mainUiControl.RefershUi();
                                break;
                            case 3:
                                ControlUiData.outfeedBelt5 = false;
                                ControlUiData.outfeedBelt6 = false;
                                mainUiControl.RefershUi();
                                break;
                            case 4:
                                ControlUiData.outfeedBelt7 = false;
                                ControlUiData.outfeedBelt8 = false;
                                mainUiControl.RefershUi();
                                num = 0;
                                isRefershNum = false;
                                break;
                            default:
                                break;
                        }
                    }
                    await Task.Delay(50);
                }
                else
                {
                    await Task.Delay(1000);
                }
            }
        }
        #endregion

        public enum WorkFlow
        { 
            动子移动到初始位,
            抬料臂1移动,
            动子接料,
            动子移动到印刷位,
            动子移动到放料位,
            抬料臂2移动,
            动子放料,
            出料12位置有半片
        }
    }
}
