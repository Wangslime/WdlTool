using DRsoft.Runtime.Core.Nlog;
using FeatureCommon.UIComponent;
namespace Drsoft.Plugin.CatchException
{
    public class CatchEx
    {
        NLogger logger = NLogger.Instance;

        #region 单例模式
        private static readonly object locked = new object();
        private static CatchEx _Instance = new CatchEx();
        public static CatchEx Instance
        {
            get
            {
                if (_Instance == null)
                {
                    lock (locked)
                    {
                        if (_Instance == null)
                        {
                            _Instance = new CatchEx();
                        }
                    }
                }
                return _Instance;
            }
            set
            {
                lock (locked)
                {
                    _Instance = value;
                }
            }
        }
        #endregion

        public void ShowBox(Exception ex, string title = "提示", bool isSync = false)
        {
            string dispMsg = ex.ToExString();
            logger.Error(dispMsg);
            if (isSync)
            {
                DrMessageBox.ShowWindowStopTheard(dispMsg, title);
            }
            else
            {
                DrMessageBox.ShowWindow(dispMsg, title);
            }
        }
        public void ShowBox(string title, Exception ex, bool isSync = false)
        {
            string dispMsg = ex.ToExString();
            logger.Error(dispMsg);
            if (isSync)
            {
                DrMessageBox.ShowWindowStopTheard(dispMsg, title);
            }
            else
            {
                DrMessageBox.ShowWindow(dispMsg, title);
            }
        }

        public void Log(Exception ex)
        {
            logger.Error(ex.ToExString());
        }
        public void Log(string msg, Exception ex)
        {
            logger.Error($"{msg}, {ex.ToExString()}");
        }
    }
}
