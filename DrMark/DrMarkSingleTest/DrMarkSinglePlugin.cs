﻿using AxMMEditx64Lib;
using AxMMIOx64Lib;
using AxMMLensCalx64Lib;
using AxMMMarkx64Lib;
using AxMMStatusx64Lib;
using System.Windows.Forms.Integration;

namespace DRsoft.Runtime.Core.XcMarkCard
{
    public class DrMarkSinglePlugin
    {
        internal int Num = 0;

        AxMMMarkx64 m_MMMark_1 = new AxMMMarkx64();
        AxMMEditx64 m_MMEdit_1 = new AxMMEditx64();
        AxMMIOx64 m_MMIO_1 = new AxMMIOx64();
        AxMMStatusx64 m_MMStatus_1 = new AxMMStatusx64();
        AxMMLensCalx64 m_MMLensCal_1 = new AxMMLensCalx64();

        private int _CardNo = 0;
        public int CardNo { get => _CardNo; set => _CardNo = value; }


        /// <summary>
        // 1：正常結束。
        // 0：無雕刻資料。
        //-1：以 ESC 按鍵結束雕刻。打標過程中，使用者以 ESC 按鍵結束雕刻時，會回傳-1。
        //-2：緊急停止。打標過程中，軟體收到外部緊急停止訊號或者使用 StopMarking 呼叫停止雕刻，即會回傳 - 2。
        /// </summary>
        public event Action<int, int> MarkEndStatus;

        /// <summary>
        /// 超出鏡頭範圍!
        /// 依使用者設置的機器檢查訊息而定
        /// 未發現硬體裝置!
        /// 找不到保護鎖。(#1)
        /// 找不到保護鎖!
        /// 檔案讀取錯誤
        /// 自動文字已超過範圍!
        /// 自動文字已被使用者中斷!
        /// 已達目標量。
        /// 檔案讀取錯誤
        /// 檔案格式不支援!
        /// (xxxxx檔案名稱)檔案使用中。請輸入新名稱或關閉其他程式開啟的檔案。
        /// </summary>
        //public void SetMarkAlarmStatus(Action<int, string> MarkAlarmStatus)
        //{
        //    MarkAlarmStatus += this.MarkAlarmStatus;
        //}
        public event Action<int, string> MarkAlarmStatus;

        #region 打标卡初始化
        bool isInitiaPanel = false;
        public bool InitiaPanel(System.Windows.Controls.Panel panels, int cardNo)
        {
            if (isInitiaPanel)
            {
                return true;
            }
            try
            {
                WindowsFormsHost formsHost = new WindowsFormsHost();
                formsHost.Name = $"MMMark{cardNo}";
                //m_MMMark_1.Size = new Size(1, 1);
                formsHost.Child = m_MMMark_1;

                WindowsFormsHost editHost = new WindowsFormsHost();
                editHost.Name = $"MMEdit{cardNo}";
                // m_MMEdit_1.Size = new Size(1, 1);
                editHost.Child = m_MMEdit_1;
                editHost.Child!.Hide();

                WindowsFormsHost statusHost = new WindowsFormsHost();
                statusHost.Name = $"MMStatus{cardNo}";
                // m_MMEdit_1.Size = new Size(1, 1);
                statusHost.Child = m_MMStatus_1;
                statusHost.Child!.Hide();

                WindowsFormsHost ioHost = new WindowsFormsHost();
                ioHost.Name = $"MMIO{cardNo}";
                // m_MMIO_1.Size = new Size(1, 1);
                ioHost.Child = m_MMIO_1;
                ioHost.Child!.Hide();

                WindowsFormsHost lensCal = new WindowsFormsHost();
                lensCal.Name = $"MMLensCal{cardNo}";
                lensCal.Child = m_MMLensCal_1;
                lensCal.Child.Hide();

                ((System.ComponentModel.ISupportInitialize)(m_MMLensCal_1!)).BeginInit();
                panels.Children.Add(lensCal);
                ((System.ComponentModel.ISupportInitialize)(m_MMLensCal_1!)).EndInit();

                ((System.ComponentModel.ISupportInitialize)(m_MMEdit_1!)).BeginInit();
                panels.Children.Add(editHost);
                ((System.ComponentModel.ISupportInitialize)(m_MMEdit_1)).EndInit();

                ((System.ComponentModel.ISupportInitialize)(m_MMStatus_1!)).BeginInit();
                panels.Children.Add(statusHost);
                ((System.ComponentModel.ISupportInitialize)(m_MMStatus_1)).EndInit();

                ((System.ComponentModel.ISupportInitialize)(m_MMIO_1!)).BeginInit();
                panels.Children.Add(ioHost);
                ((System.ComponentModel.ISupportInitialize)(m_MMIO_1)).EndInit();

                ((System.ComponentModel.ISupportInitialize)(m_MMMark_1!)).BeginInit();
                panels.Children.Add(formsHost);
                ((System.ComponentModel.ISupportInitialize)(m_MMMark_1)).EndInit();

                //初始化命令

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                isInitiaPanel = true;
            }
        }

        bool isInitialExt = false;
        public bool InitialExt(int cardNo)
        {
            if (isInitialExt)
            {
                return true;
            }
            int ret = m_MMMark_1.SetCloseErrorMsgBox(1);
            ret += m_MMMark_1.Initial();
            ret += m_MMEdit_1.Initial();
            ret += m_MMIO_1.Initial();
            ret += m_MMLensCal_1.Initial();
            ret += m_MMStatus_1.Initial();
            ret += m_MMMark_1.MarkStandBy();
            ret += m_MMEdit_1.ThermalDrift_EnableExt(1, 1, 1);
            ret += m_MMMark_1.LaserOff();

            ret += m_MMMark_1.Redraw();
            ret += m_MMMark_1.SetFrequency("default", 100);
            ret += m_MMMark_1.SetPreviewMode(1);
            ret += m_MMEdit_1.SetIntFrequencyExt("default", 1, 100);

            //更新初始化板卡
            int count = 100;
            int[] iEnable = new int[count];
            double[] dOffX = new double[count];
            double[] dOffY = new double[count];
            double[] dRCX = new double[count];
            double[] dRCY = new double[count];
            double[] dRotate = new double[count];
            for (int i = 0; i < count; i++)
            {
                iEnable[i] = 1;
                dOffX[i] = 0;
                dOffY[i] = 0;
                dRotate[i] = 0;
                dRCX[i] = dOffX[i];
                dRCY[i] = dOffY[i];
            }
            unsafe
            {
                fixed (int* piEn = iEnable)
                {
                    fixed (double* pdOffX = dOffX, pdOffY = dOffY, pdRcX = dRCX, pdRcY = dRCY, pdRotate = dRotate)
                    {

                        IntPtr ptr1 = (IntPtr)piEn;
                        long IdATA1 = ptr1.ToInt64();

                        IntPtr ptr2 = (IntPtr)pdOffX;
                        long IdATA2 = ptr2.ToInt64();

                        IntPtr ptr3 = (IntPtr)pdOffY;
                        long IdATA3 = ptr3.ToInt64();

                        IntPtr ptr4 = (IntPtr)pdRcX;
                        long IdATA4 = ptr4.ToInt64();

                        IntPtr ptr5 = (IntPtr)pdRcY;
                        long IdATA5 = ptr5.ToInt64();

                        IntPtr ptr6 = (IntPtr)pdRotate;
                        long IdATA6 = ptr6.ToInt64();
                        ret += m_MMEdit_1.ModifyLayerByTable(count, IdATA1, IdATA2, IdATA3, IdATA4, IdATA5, IdATA6);
                    }
                }
            }
            ret += m_MMIO_1.SetOutput(1, 1);

            //建立MarkEnd事件
            m_MMStatus_1.MarkEnd += M_MMMark_1_MarkEnd;
            //建立Alarm事件
            m_MMStatus_1.Alarm += M_MMMark_1_Alarm;

            isInitialExt = true;
            if (ret != 0)
            {
                return false;
            }
            return true;
        }

        public void MarkingFinish()
        {
            try
            {
                m_MMMark_1.StopAutomation();
                m_MMMark_1.LaserOff();
                m_MMEdit_1.Finish();
                m_MMIO_1.Finish();
                m_MMMark_1.Finish();
                m_MMLensCal_1.Finish();
            }
            catch (Exception ex)
            {
            }
        }

        //MarkEnd事件
        private void M_MMMark_1_MarkEnd(object sender, _DMMStatusx64Events_MarkEndEvent e)
        {
            int iStatus = e.lStatus;
            //e.lStatus
            //1：正常結束。
            //0：無雕刻資料。
            //-1：以 ESC 按鍵結束雕刻。打標過程中，使用者以 ESC 按鍵結束雕刻時，會回傳-1。
            //-2：緊急停止。打標過程中，軟體收到外部緊急停止訊號或者使用 StopMarking 呼叫停止雕刻，即會回傳 - 2。
            MarkEndStatus?.Invoke(CardNo, iStatus);
        }

        //Alarm事件
        private void M_MMMark_1_Alarm(object sender, _DMMStatusx64Events_AlarmEvent e)
        {
            string strAlarm = e.lStatus;
            MarkAlarmStatus?.Invoke(CardNo, strAlarm);
            //超出鏡頭範圍!
            //依使用者設置的機器檢查訊息而定
            //未發現硬體裝置!
            //找不到保護鎖。(#1)
            //找不到保護鎖!
            //檔案讀取錯誤
            //自動文字已超過範圍!
            //自動文字已被使用者中斷!
            //已達目標量。
            //檔案讀取錯誤
            //檔案格式不支援!
            //(xxxxx檔案名稱)檔案使用中。請輸入新名稱或關閉其他程式開啟的檔案。
        }
        #endregion

        public int GetVersion(ref string pstrVersion)
        {
            return m_MMMark_1.GetVersion(ref pstrVersion);
        }
        public int LoadFile(string fileName)
        {
            return m_MMMark_1.LoadFile(fileName);
        }

        public int MoveObject(string objectName, double dX, double dY, int lRel)
        {
            return m_MMMark_1.MoveObject(objectName, dX, dY, lRel);
        }

        public int RotateObject(string objectName, double dAngle, int lRel)
        {
            return m_MMMark_1.RotateObject(objectName, dAngle, lRel);
        }

        public int SetMatrixExt(double dXOffset, double dYOffset, double dRotateCenterX, double dRotateCenterY, double dAngle,
            double dScaleCenterX, double dScaleCenterY, double dScaleX, double dScaleY)
        {
            return m_MMMark_1.SetMatrixExt(dXOffset, dYOffset, dRotateCenterX, dRotateCenterY, dAngle, dScaleCenterX, dScaleCenterY, dScaleX, dScaleY);
        }

        public int SetMatrix(double dXOffset, double dYOffset, double dAngle)
        {
            return m_MMMark_1.SetMatrix(dXOffset, dYOffset, dAngle);
        }

        public int StartMarking(int iMode)
        {
            return m_MMMark_1.StartMarking(iMode);
        }

        public int LaserOn()
        {
            return m_MMMark_1.LaserOn();
        }

        public int LaserOff()
        {
            return m_MMMark_1.LaserOff();
        }

        public int StartAutomation()
        {
            return m_MMMark_1.StartAutomation();
        }

        public int StopAutomation()
        {
            return m_MMMark_1.StopAutomation();
        }

        public int SetLayerOutput(string objectName, int iCanOutput)
        {
            return m_MMEdit_1.SetLayerOutput(objectName, iCanOutput);
        }

        public int SetOutput(int lPort, int lOn)
        {
            return m_MMIO_1.SetOutput(lPort, lOn);
        }

        public int JumpToStartPos()
        {
            return m_MMMark_1.JumpToStartPos();
        }

        public int ExportJpg(string filePath, int iDpi)
        {
            return m_MMMark_1.ExportJPG(filePath, iDpi);
        }

        public int RunMarkingMate(string filePath)
        {
            //var n1 = m_MMMark_1.LoadFile(filePath);
            var n2 = m_MMMark_1.RunMarkingMate(filePath, 0);
            //var rtn = n1 + n2;
            return n2;
        }

        public int ModifyLayerTable(int lCnt, int[] iEnable, double[] dOffX, double[] dOffY, double[] dRcx, double[] dRcy, double[] dRotate)
        {
            int rtn;
            unsafe
            {
                fixed (int* piEn = iEnable)
                {
                    fixed (double* pdOffX = dOffX, pdOffY = dOffY, pdRcX = dRcx, pdRcY = dRcy, pdRotate = dRotate)
                    {
                        IntPtr ptr1 = (IntPtr)piEn;
                        long idAta1 = ptr1.ToInt64();

                        IntPtr ptr2 = (IntPtr)pdOffX;
                        long idAta2 = ptr2.ToInt64();

                        IntPtr ptr3 = (IntPtr)pdOffY;
                        long idAta3 = ptr3.ToInt64();

                        IntPtr ptr4 = (IntPtr)pdRcX;
                        long idAta4 = ptr4.ToInt64();

                        IntPtr ptr5 = (IntPtr)pdRcY;
                        long idAta5 = ptr5.ToInt64();

                        IntPtr ptr6 = (IntPtr)pdRotate;
                        long idAta6 = ptr6.ToInt64();
                        rtn = m_MMEdit_1.ModifyLayerByTable(lCnt, idAta1, idAta2, idAta3, idAta4, idAta5, idAta6);
                    }
                }
            }
            return rtn;
        }

        public int MultiCard_ModifyLayerByTable(int headId,int lCnt, int[] iEnable, double[] dOffX, double[] dOffY, double[] dRcx, double[] dRcy, double[] dRotate)
        {
            int rtn;
            unsafe
            {
                fixed (int* piEn = iEnable)
                {
                    fixed (double* pdOffX = dOffX, pdOffY = dOffY, pdRcX = dRcx, pdRcY = dRcy, pdRotate = dRotate)
                    {
                        IntPtr ptr1 = (IntPtr)piEn;
                        long idAta1 = ptr1.ToInt64();

                        IntPtr ptr2 = (IntPtr)pdOffX;
                        long idAta2 = ptr2.ToInt64();

                        IntPtr ptr3 = (IntPtr)pdOffY;
                        long idAta3 = ptr3.ToInt64();

                        IntPtr ptr4 = (IntPtr)pdRcX;
                        long idAta4 = ptr4.ToInt64();

                        IntPtr ptr5 = (IntPtr)pdRcY;
                        long idAta5 = ptr5.ToInt64();

                        IntPtr ptr6 = (IntPtr)pdRotate;
                        long idAta6 = ptr6.ToInt64();
                        rtn = m_MMEdit_1.MultiCard_ModifyLayerByTable(headId,lCnt, idAta1, idAta2, idAta3, idAta4, idAta5, idAta6);
                    }
                }
            }
            return rtn;
        }


        public int GetLayerCount()
        {
            return m_MMEdit_1.GetLayerCount();
        }

        public int GetLayerName(int iIndex, out string pszName)
        {
            pszName = "";
            int ret = m_MMEdit_1.GetLayerName(iIndex, ref pszName);
            return ret;
        }

        public int ChangeLens(string lpName)
        {
            return m_MMEdit_1.ChangeLens(lpName);
        }

        public int ChangeLensMulti(int iHead, string lpName)
        {
            return m_MMEdit_1.ChangeLensMulti(iHead, lpName);
        }

        public double GetCenterX(string strName)
        {
            return m_MMEdit_1.GetCenterX(strName);
        }

        public double GetCenterY(string strName)
        {
            return m_MMEdit_1.GetCenterY(strName);
        }

        public int SelectClearObjects()
        {
            return m_MMMark_1.SelectClearObjects();
        }

        public int SetMarkSelect(int iSelect)
        {
            return m_MMMark_1.SetMarkSelect(iSelect);
        }

        public int SelectAddObjectExt(string strParent, string strobj)
        {
            return m_MMMark_1.SelectAddObjectExt(strParent, strobj);
        }

        public int SetPreviewMode(int iMode)
        {
            return m_MMMark_1.SetPreviewMode(iMode);
        }

        public int StopMarking()
        {
            return m_MMMark_1.StopMarking();
        }

        public int ThermalDrift_ClearExt(int iScanHead, int iTable)
        {
            return m_MMEdit_1.ThermalDrift_ClearExt(iScanHead, iTable);
        }

        public int ThermalDrift_SetStretchDataExt(int lScanHead, int lTable, double dx1, double dy1, double dx2,
            double dy2, double dx3, double dy3, double dx4, double dy4, double dRx1, double dRy1, double dRx2, double dRy2,
            double dRx3, double dRy3, double dRx4, double dRy4)
        {
            return m_MMEdit_1.ThermalDrift_SetStretchDataExt(lScanHead, lTable, dx1, dy1, dx2, dy2, dx3, dy3, dx4, dy4, dRx1, dRy1, dRx2, dRy2, dRx3, dRy3, dRx4, dRy4);
        }

        public int ThermalDrift_ChangeTableExt(int iScanHead, int iTable)
        {
            return m_MMEdit_1.ThermalDrift_ChangeTableExt(iScanHead, iTable);
        }

        public int JumpTo(double dx, double dy)
        {
            return m_MMMark_1.JumpTo(dx, dy);
        }

        public int SetPower(string strName, double dPerc)
        {
            return m_MMMark_1.SetPower(strName, dPerc);
        }

        public int SetFrequency(string strName, double dKHz)
        {
            return m_MMMark_1.SetFrequency(strName, dKHz);
        }

        public int SetPulseWidth(string strName, double dPulseWidth)
        {
            return m_MMMark_1.SetPulseWidth(strName, dPulseWidth);
        }

        public int GetMarkTime()
        {
            return m_MMMark_1.GetMarkTime();
        }

        public int GetInput(long lport)
        {
            return m_MMIO_1.GetInput((int)lport);
        }

        public int GetInputExt(int lFirstPort, int lPortCnt)
        {
            return m_MMIO_1.GetInputExt(lFirstPort, lPortCnt);
        }

        public double GetSpeed(string strName)
        {
            return m_MMMark_1.GetSpeed(strName);
        }

        public double GetFrequency(string strName)
        {
            return m_MMMark_1.GetFrequency(strName);
        }

        public double GetPower(string strName)
        {
            return m_MMMark_1.GetPower(strName);
        }

        public double GetPulseWidth(string strName)
        {
            return m_MMMark_1.GetPulseWidth(strName);
        }

        public int GetLaserOnDelay(string strName)
        {
            return m_MMMark_1.GetLaserOnDelay(strName);
        }

        public int GetPolyDelay(string strName)
        {
            return m_MMMark_1.GetPolyDelay(strName);
        }

        public int GetLaserOffDelay(string strName)
        {
            return m_MMMark_1.GetLaserOffDelay(strName);
        }

        public int GetMarkDelay(string strName)
        {
            return m_MMMark_1.GetMarkDelay(strName);
        }

        public double GetJumpSpeed(string strName)
        {
            return m_MMMark_1.GetJumpSpeed(strName);
        }

        public int GetJumpDelay(string strName)
        {
            return m_MMMark_1.GetJumpDelay(strName);
        }

        public int MarkStandBy()
        {
            return m_MMMark_1.MarkStandBy();
        }

        public int ThermalDrift_EnableExt(int lScanHead, int lTable, int lEnable)
        {
            return m_MMEdit_1.ThermalDrift_EnableExt(lScanHead, lTable, lEnable);
        }

        public int SetIntFrequencyExt(string strName, int lPassIndex, double dKHz)
        {
            return m_MMEdit_1.SetIntFrequencyExt(strName, lPassIndex, dKHz);
        }

        public int ResetFile()
        {
            return m_MMMark_1.ResetFile();
        }

        public int Redraw()
        {
            return m_MMMark_1.Redraw();
        }

        public int LoadCorrectTable(int lHead, int lTb, string strFilePath)
        {
            return m_MMEdit_1.LoadCorrectTable(lHead, lTb, strFilePath);
        }

        public int SetLensCorParameterByName(string lpName, int lParamType, double dParam)
        {
            return m_MMEdit_1.SetLensCorParameterByName(lpName, lParamType, dParam);
        }

        public double GetLensCorParameterByName(string lpName, int lParamType)
        {
            return m_MMEdit_1.GetLensCorParameterByName(lpName, lParamType);
        }

        public int IsMarking()
        {
            return m_MMMark_1.IsMarking();
        }


        ~DrMarkSinglePlugin()
        {
            MarkClosing();
        }
        public void MarkClosing()
        {
            //結束命令
            m_MMIO_1.Finish();
            m_MMEdit_1.Finish();
            m_MMMark_1.Finish();
            m_MMStatus_1.Finish();
            // 結束命令执行完成
            // 打标卡硬件結束完成
        }

        public int SetCurrentHead(int num)
        {
            return m_MMMark_1.SetCurrentHead(num);
        }

        public int MarkData_UnLock()
        {
            return m_MMMark_1.MarkData_UnLock();
        }

        public int IsAutomation()
        {
            return m_MMMark_1.IsAutomation();
        }

        public int GetDriverInfo(ref string pstrName, ref string pStrVer)
        {
            return m_MMMark_1.GetDriverInfo(ref pstrName, ref pStrVer);
        }

        public int GetLensName(ref string pstrName)
        {
            return m_MMLensCal_1.GetLensName(ref pstrName);
        }

        public double GetEstimatedTotalTime()
        {
            return m_MMMark_1.GetEstimatedTotalTime();
        }

        public int SetFillDashLineParamABMultiGroupEnable(string strName, bool enable)
        {
            return m_MMEdit_1.SetFillDashLineParamABMultiGroupEnable(strName, Convert.ToInt32(enable));
        }

        public bool GetFillDashLineParamABMultiGroupEnable(string strName)
        {
            return m_MMEdit_1.GetFillDashLineParamABMultiGroupEnable(strName) == 1;
        }

        public int SetFillDashLineParamAB_GroupCnt(string strName, int groupCnt)
        {
            return m_MMEdit_1.SetFillDashLineParamAB_GroupCnt(strName, groupCnt);
        }

        public int GetFillDashLineParamAB_GroupCnt(string strName)
        {
            return m_MMEdit_1.GetFillDashLineParamAB_GroupCnt(strName);
        }

        public int SetFillDashLineParamA_Group(string strName, int group, double paramA)
        {
            return m_MMEdit_1.SetFillDashLineParamA_Group(strName, group, paramA);
        }

        public double GetFillDashLineParamA_Group(string strName, int group)
        {
            return m_MMEdit_1.GetFillDashLineParamA_Group(strName, group);
        }

        public int SetFillDashLineParamB_Group(string strName, int group, double paramB)
        {
            return m_MMEdit_1.SetFillDashLineParamB_Group(strName, group, paramB);
        }

        public double GetFillDashLineParamB_Group(string strName, int group)
        {
            return m_MMEdit_1.GetFillDashLineParamB_Group(strName, group);
        }

        /// <summary>
        /// 预览中间增加停顿间隔
        /// </summary>
        /// <param name="dSec">:兩次預覽間的延遲時間[秒]:</param>
        /// <returns>0:成功 1:失敗</returns>
        public int SetPreviewDelay(double dSec)
        {
            return m_MMMark_1.SetPreviewDelay(dSec);
        }

        /// <summary>
        /// 控制卡是否连线
        /// </summary>
        /// <returns></returns>
        public int IsCardConnect()
        {
            return m_MMMark_1.IsCardConnect();
        }

        /// <summary>
        /// 雷射重新连线
        /// </summary>
        /// <returns></returns>
        public int LaserReconnect()
        {
            return m_MMMark_1.LaserReconnect();
        }

        /// <summary>
        /// 可新增一個图层
        /// </summary>
        /// <param name="layerName">新图层的名称</param>
        /// <returns>0：成功，非零值則表失敗。10：失敗，目前在MarkData_Lock下無法使用。(僅適用於V2.7 A-35.19、V2.7 D-4.22、V2.7 D_x64-2.12以上版本)</returns>
        public int AddLayer(string layerName)
        {
            return m_MMEdit_1.AddLayer(layerName);
        }

        /// <summary>
        /// 可删除指定图层
        /// </summary>
        /// <param name="layerName">指定图层的名称</param>
        /// <returns>0：成功，非零值则表失败。10：失败，目前在MarkData_Lock下无法使用。 （仅适用于V2.7 A-35.19、V2.7 D-4.22、V2.7 D_x64-2.12以上版本）</returns>
        public int DeleteLayer(string layerName)
        {
            return m_MMEdit_1.DeleteLayer(layerName);
        }

        /// <summary>
        /// 可设定指定图层是否可编辑
        /// </summary>
        /// <param name="strName">指定物件名稱</param>
        /// <param name="canEdit"></param>
        /// <returns>0：成功，非零值则表失败。10：失败，目前在MarkData_Lock下无法使用。 （仅适用于V2.7 A-35.19、V2.7 D-4.22、V2.7 D_x64-2.12以上版本）</returns>
        public int SetLayerEdit(string strName, bool canEdit)
        {
            return m_MMEdit_1.SetLayerEdit(strName, Convert.ToInt32(canEdit));
        }

        /// <summary>
        /// 可将目前的数据库储存至ezm档案
        /// </summary>
        /// <param name="strFileName">ezm档案的全路径</param>
        /// <returns></returns>
        public int SaveFile(string strFileName)
        {
            return m_MMMark_1.SaveFile(strFileName);
        }

        /// <summary>
        /// 可将目标对象复制至指定组下
        /// </summary>
        /// <param name="strOrg">目标对象的名称</param>
        /// <param name="strCopy">复制出的新对象名称</param>
        /// <param name="strGroup">指定组或图层的名称。（可配置为空字符串（“”），即复制到当前图层下）</param>
        /// <returns>0：成功，非零值则表失败。10：失败，目前在MarkData_Lock下无法使用。 （仅适用于V2.7 A-35.19、V2.7 D-4.22、V2.7 D_x64-2.12以上版本）</returns>
        public int CopyObject(string strOrg, string strCopy, string strGroup)
        {
            return m_MMEdit_1.CopyObject(strOrg, strCopy, strGroup);
        }

        /// <summary>
        /// 將當前圖檔與作為模板的ezm檔做比對, 判斷是否內容一樣。
        /// </summary>
        /// <param name="lpTemplateFile">作為模板的ezm檔的絕對路徑</param>
        /// <returns>0不同, 1相同, -1失敗</returns>
        public int IsEqualFile(string lpTemplateFile)
        {
            return m_MMMark_1.IsEqualFile(lpTemplateFile);
        }
    }
}
