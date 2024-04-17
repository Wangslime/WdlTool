namespace WpfControlLibrary1
{
    public record ControlUiData
    {
        /// <summary>
        /// 进料皮带上是否有半片
        /// </summary>
        public bool infeedBelt1 { get; set; }
        public bool infeedBelt2 { get; set; }
        public bool infeedBelt3 { get; set; }
        public bool infeedBelt4 { get; set; }
        public bool infeedBelt5 { get; set; }
        public bool infeedBelt6 { get; set; }
        public bool infeedBelt7 { get; set; }
        public bool infeedBelt8 { get; set; }


        /// <summary>
        /// 定位相机
        /// </summary>
        public CameraStatus cameraLocalization1 { get; set; }
        public CameraStatus cameraLocalization2 { get; set; }
        /// <summary>
        /// 检测相机
        /// </summary>
        public CameraStatus cameraDetect1 { get; set; }
        public CameraStatus cameraDetect2 { get; set; }
        /// <summary>
        /// 出料AOI相机
        /// </summary>
        public CameraStatus cameraAoiOutfeed { get; set; }


        /// <summary>
        /// 印刷头
        /// </summary>
        public bool printing1Working { get; set; }
        public bool printing2Working { get; set; }


        /// <summary>
        /// 出料皮带半片
        /// </summary>
        public bool outfeedBelt1 { get; set; }
        public bool outfeedBelt2 { get; set; }
        public bool outfeedBelt3 { get; set; }
        public bool outfeedBelt4 { get; set; }
        public bool outfeedBelt5 { get; set; }
        public bool outfeedBelt6 { get; set; }
        public bool outfeedBelt7 { get; set; }
        public bool outfeedBelt8 { get; set; }

        /// <summary>
        /// 两端抬料臂
        /// </summary>
        public LiftingFeedArm liftingFeedArm1 { get; set; } = new LiftingFeedArm();
        public LiftingFeedArm liftingFeedArm2 { get; set; } = new LiftingFeedArm();



        /// <summary>
        /// 4动子
        /// </summary>
        public TrayActuator trayActuator1 { get; set; } = new TrayActuator();
        public TrayActuator trayActuator2 { get; set; } = new TrayActuator();
        public TrayActuator trayActuator3 { get; set; } = new TrayActuator();
        public TrayActuator trayActuator4 { get; set; } = new TrayActuator();

        public int NgHalfSliceNum { get; set; }
    }



    public record TrayActuator
    {
        /// <summary>
        /// 动子实际位置
        /// </summary>
        public double xpos { get; set; } = 1000;

        /// <summary>
        /// 是否顶起
        /// </summary>
        public bool jackFeed { get; set; }

        //private double _zpos = 0;
        //public double zpos
        //{
        //    get
        //    { 
        //        return _zpos;
        //    }
        //    set
        //    {
        //        _zpos = value;
        //        jackFeed = _zpos > 3000;
        //    }
        //}
    }

    /// <summary>
    /// 台料臂
    /// </summary>
    public record LiftingFeedArm
    {
        /// <summary>
        /// 位置是否在左侧
        /// </summary>
        public bool posLeft { get; set; }


        /// <summary>
        /// 是否有料
        /// </summary>
        public bool existFeed { get; set; }
    }


    public enum CameraStatus
    {
        NotConnect,
        Connect,
        Runing,
        End,
        Exception,
    }
}
