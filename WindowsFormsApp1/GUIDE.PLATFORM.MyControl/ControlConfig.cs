namespace GUIDE.PLATFORM.MyControl
{
    public class ControlConfig
    {
        private static object lockObj = new object();
        private static ControlConfig _Instance = null;
        public static ControlConfig Instance
        {
            get
            {
                if (_Instance == null)
                {
                    lock (lockObj)
                    {
                        if (_Instance == null)
                        {
                            _Instance = new ControlConfig();
                        }
                    }
                }
                return _Instance;
            }
        }
        public BridgeColor bridgeColor = BridgeColor.Yellow;
        public SkinStyle skinStyle = SkinStyle.Black;

        public int BeamHeight = 34;
        public int LeftLegStartPos = 0;
        public int RightLegEndPos = 0;

        public int SpreaderHeight = 40;
        public int SpreaderWidth = 25;
    }
}
