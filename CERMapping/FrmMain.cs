using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.IO;
using System.Configuration;
using System.Collections;
using System.Runtime.InteropServices;
using System.Diagnostics;
using stdole;

using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.ADF;
using ESRI.ArcGIS.SystemUI;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.DataSourcesRaster;
using ESRI.ArcGIS.Analyst3D;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.DataSourcesFile;
using ESRI.ArcGIS.DataSourcesGDB;

using System.Threading;

using DevComponents.DotNetBar.SuperGrid;


using LibCerMap;
using LibEngineCmd;
using LibModelGen;
using System.Xml;
using DevComponents.DotNetBar.Controls;

namespace CERMapping
{
    public partial class FrmMain : System.Windows.Forms.Form //DevComponents.DotNetBar.Office2007RibbonForm//
    {
        #region class private members
        private IMapControl3 m_mapControl = null;
        private string m_mapDocumentName = string.Empty;
        private EngineEditor m_EngineEditor = new EngineEditorClass();//全局只有一个Editor
        private IEngineEditSketch m_EngineEditSketch;

        private string m_strApplicationName = "数据对齐软件系统  ";

        private bool m_bAlwaysSame = false;//金字塔是否创建执行同一操作
        private bool m_bPyramidCreate = false ;//是否创建金字塔
        

        #endregion

        #region 右键菜单
        private LibCerMap.ControlsSynchronizer m_controlsSynchronizer = null;
        private IToolbarMenu m_menuFrame = null;//地图框架右键菜单
        private IToolbarMenu m_menuLayer = null;//图层右键菜单
        private IToolbarMenu m_menuElement = null;//布局界面添加要素属性菜单;
        private IToolbarMenu m_menuMap = null;//地图控件右键菜单
        private IToolbarMenu m_menuEditor = null;//编辑右键菜单
        #endregion

        #region 自定义工具对话框事件
        private ICustomizeDialog m_CustomizeDialog;
        private ICustomizeDialogEvents_OnStartDialogEventHandler startDialogE;			//The CustomizeDialog start event
        private ICustomizeDialogEvents_OnCloseDialogEventHandler closeDialogE;
        #endregion

        #region 私有成员

         //属性表
        private CmdAttributeTable m_AttributeTable;
        //开始编辑命令
        private CmdStartEditTinLayer m_cmdStartEditTinLayer;
        //停止编辑命令
        private CmdStopEditTinLayer m_cmdStopEditTinLayer;
        //保存编辑命令
        private CmdSaveEditTinLayer m_cmdSaveEditTinLayer;
        //TIN另存为命令
        private CmdTinSaveAs m_cmdTinSaveAs;
        //添加节点命令
        private ToolAddTinNode m_toolAddTinNode;
        //删除节点命令
        private ToolDeleteTINNode m_toolDeleteTINNode;
        //修改节点命令
        private ToolModifyTINNode m_toolModifyTinNode;
        //tin 添加tin平面
        private ToolAddTinPlane m_toolAddTinPlane;
        // 删除区域tin节点
        private ToolDeleteNodeByArea m_ToolDeleteNodeByArea;
        // 添加撞击坑
        private ToolAddCraterOnTIN m_ToolAddCraterOnTIN;
        // 添加石块                                                                   
        private ToolAddStone m_ToolAddStone;
        //选取撞击坑样本
        private ToolExtractCraterSample m_ToolExtractCraterSample;
        // 编辑石块撞击坑
        private ToolEditFeatures m_ToolEditFeatures;
        private ToolEditFeatures m_ToolEditFeaturesInMapCtrl;
        //tin编辑目标图层
        private ITinLayer m_TargetTinLayer;
        //添加撞击坑和石块的栅格图层
        private IRasterLayer m_EditRasterLayer;
        //添加撞击坑图层
        private IFeatureLayer m_CraterLayer;
        //添加非撞击坑图层
        private IFeatureLayer m_NonCraterLayer;
        //矫正栅格图层 
        private IRasterLayer m_TargetRasterLayer;
        //平移
        private ToolRasterShift m_ToolRasterShift;
        //添加控制点
        private ToolAddControlPoints m_ToolAddControlPoints;
        //SIFT匹配
        private CmdRasterRegister m_cmdRasterSiftMatching;
        //平移指定距离
        private CmdRasterShift m_cmdRasterShift;
        //旋转指定角度
        private CmdRasterRotate m_cmdRasterRotate;
        //栅格翻转
        private CmdRasterFlip m_cmdRasterFlip;
        //栅格镜像
        private CmdRasterMirror m_cmdRasterMirror;
        //栅格灰度值变换
        private CmdRasterTransZ m_cmdRasterTransZ;
        //栅格重采样
        private CmdRasterResample m_cmdRasterResample;
        //栅格配准
        private CmdRasterRegister m_cmdRasterRegister;

        private CmdRasterNED2ENU m_cmdRasterNED2ENU;
        //栅格裁切
        private CmdRasterSubset m_cmdRasterSubset;
        //栅格镶嵌
        private CmdRasterMosaic m_cmdRasterMosaic;
        //栅格计算
        private CmdRasterMeaAtt m_cmdRasterMeaAtt;
        //栅格表面分析
        private CmdSurfaceOp m_cmdSurfaceOp;
        //栅格剖面分析
        private CmdProfileAnalysis m_cmdProfileAnalysis;
        //设置栅格区域为无效值
        private ToolRasterEdit m_ToolRasterEdit;

        //图层标注
        private CmdLabelDesign m_labeldesign;
        //转接表
        private CmdJoinAttribute m_joinattribute;
        //修改scene图层偏移
        private CmdSceneLayerOffset m_CmdSceneLayerOffset;

        //矢量图空间校正
        //添加连接点
        private ToolNewDisplacement m_NewDisplacement;
        //查看连接点表
        private CmdViewLinkTable m_ViewLinkTable;
        //设置待校正数据
        private CmdSetAdjustData m_SetAdjustData;
        //校正
        private CmdAdjust m_Adjust;

        private EngineInkEnvironmentClass m_EngineInkEnvironmentClass = new EngineInkEnvironmentClass();

        //页面设置、打印预览、打印参数
        private System.Drawing.Printing.PrintDocument document = new System.Drawing.Printing.PrintDocument();

        //用于记忆目前已经打开的样式
        private string[] SymbolStyle = new string[100];

        //太阳高度角
        //private cmdGetSunAltXmlFile m_cmdGetSunAltXmlFile=new cmdGetSunAltXmlFile();
        private ToolSunAltCheckState m_toolSunAltCheckState = new ToolSunAltCheckState();
        private CmdSunAltProperty m_cmdSunAltProperty = new CmdSunAltProperty();
        private List<ClsPointInfo> m_SunAltPointCollection = new List<ClsPointInfo>();
        private CmdClearAllSunAltPts m_cmdClearAllSunAltPts = new CmdClearAllSunAltPts();
        private ClsCameraPara m_cameraPara = null;
        //private IPointCollection m_OriginPoints = new MultipointClass();
        //图层设置
        ILayer pLayerEffects = null;

        //画笔
        Pen penred = new Pen(new SolidBrush(Color.Red));
        Pen penblue = new Pen(new SolidBrush(Color.Blue));
        Pen pengreen = new Pen(new SolidBrush(Color.Green));
        Pen penyelow = new Pen(new SolidBrush(Color.Yellow));
        Brush strBrush = new SolidBrush(Color.Yellow);
        System.Drawing.Font strFont = new System.Drawing.Font("宋体", 9F);
        Graphics gcs;

        //模型链表
        List<String> m_CraterlistModels;
        List<String> m_NonCraterlistModels;
        List<Model> m_manualAddModels;// = new List<Model>();

        //记录属性表选中记录
        //List<int> selindex = new List<int>();
        //string strvalue = "";
        //bool istrue = true;
        //int startrow = 0;
        //int endrow = 0;

        double rolldegree = 15.0;//三维正向翻转角度
        double mrolldegree = -15.0;//三维负向翻转角度

        //布局cmd声明，主要为了方便制图菜单HOOK的初始化和判定
        CmdNorthArrow cmdNorthArrow = new CmdNorthArrow();
        CmdScaleBar cmdScaleBar = new CmdScaleBar();
        CmdScaleText cmdScaleText = new CmdScaleText();
        CmdLegend cmdLeg;
        CmdMapGrid cmdMapGrid;
        CmdAddText cmdAddText = new CmdAddText();
        CmdExportActiveViewFig cmdExpActFig = new CmdExportActiveViewFig();
        CmdPrintMap cmdPrintMap = new CmdPrintMap();

        CmdMapUnits cmdMapUnit;
        CmdCoordSystem cmdCoordSystem;
        int m_DegreeMode = 0;


        //下面几个变量用于进行栅格量测
        public IGeometry pPolygonMeaAtt;
        public bool isMeaAttStart;
        public IGraphicsContainer pGraphiccsContainer;

        #endregion

//        #region 数据监听查询
//        public List<_ZF_FILETYPE> m_pListFielType = new List<_ZF_FILETYPE>();


//        public Thread thdLis;//数据监听线程
//        public bool m_isFirstStartRecvw=true;

//#region 丢弃

//        //const uint IFLI_RNAVIP = 5051;   //导航点确定结果文件
//        //const uint IFLI_RPACPA = 5054;   //路径单元规划控制参数文件
//        //const uint IFLI_YSDHX = 5000;   //导航相机原始图像文件
//        //const uint IFLI_YSQJX = 5001;    //全景相机原始图像文件
//        //const uint IFLI_YSBZX = 5002;    //避障相机原始图像文件
//        //const uint IFLI_ZS = 5016;    //DOM文件
//        //const uint IFLI_FYQJX = 5026;    //巡视器全景相机原始彩色复原图像文件
//        //const uint IFLI_GC_100CM = 5027;    //100厘米量级DEM地形文件
//        //const uint IFLI_GC_10CM = 5028;    //10厘米量级DEM地形文件
//        //const uint IFLI_GC_1CM = 5029;    //1厘米量级DEM地形文件
//        //const uint IFLI_GC_HIGH = 5030;    //1厘米以下高分辨率DEM地形文件

//        //电文类型集
//        //uint[] uiMsgType = new uint[11] { IFLI_RNAVIP, IFLI_RPACPA, IFLI_YSDHX, IFLI_YSQJX, IFLI_YSBZX, IFLI_ZS, IFLI_FYQJX, IFLI_GC_100CM, IFLI_GC_10CM, IFLI_GC_1CM, IFLI_GC_HIGH };
//#endregion

//        uint[] uiMsgType = new uint[1] { 100 };
//        const uint ZM_LOC = 0;               /* 队列类型：本地队列           */
//        const uint ZM_REM_MAS = 1;           /* 队列类型：网络队列(只收主机) */
//        const uint ZM_REM_SLA = 2;           /* 队列类型：网络队列(只收备机) */
//        const uint ZM_REM_DUP = 3;           /* 队列类型：网络队列(主备均收) */

//        const uint ZM_TASK_CODE_WILDCARD = 0;  /* 通配任务码                   */
//        const uint ZM_OBJ_CODE_WILDCARD = 0;   /* 通配目标码                   */

//        const int MRPP_MAILBUF_MAXLEN = 500000; /*最大的电文长度*/

//        private int siRtn ;//指示初始化返回值
//        private int iQueueID ;//指示open返回值
//        private int siStatus ;//指示电文接受返回值

//        public string fdpath;//传入的存储路径

//        _ZM_MsgHead MsgHead;
//        _ZM_Addr zmaddr;
//        _ZF_FileHead filehead;

//        [DllImport("ZM.dll", EntryPoint = "zm_init")]
//        static extern int zm_init();//电文传输服务初始化

//        [DllImport("ZM.dll", EntryPoint = "zm_open", CallingConvention = CallingConvention.Cdecl)]
//        static extern int zm_open(uint uiMsgTypeNum,
//                                  uint[] uiMsgType,
//                                  uint uiQueType,
//                                  uint uiTaskCode,
//                                  uint uiObjCode,
//                                  uint uiQueBufLen);//打开一个电文队列


//        // 阻塞接收
//        [DllImport("ZM.dll", EntryPoint = "zm_recvw", CallingConvention = CallingConvention.Cdecl)]
//        static extern int zm_recvw(int siQueID,
//                                   out _ZM_MsgHead pstMsgHead,
//                                   char[] pvBuf,
//                                   int siBufLen,
//                                   out _ZM_Addr pstAddr);//阻塞式接受电文
   
//        //[DllImport("ZM.dll")]
//        //static extern System.IntPtr zm_strerr(int siErrCode);//返回错误码,
//        ////调用方式：StringBuilder stest =new StringBuilder("abc");zm_strerr();string resule=stest.tostring();

//        [DllImport("ZF2.dll")]
//        static extern int zf2_init();//获取文件初始化

//        [DllImport("ZF2.dll", EntryPoint = "zf2_getone", CallingConvention = CallingConvention.Cdecl)]
//        static extern int zf2_getone(_ZF_FileHead pstFileHead,
//                                     char[] szFileName,
//                                     char[] szFileNameS);//获取单个文件

//        char[] pvbuf = new char[1024*64];
//        unsafe void* cmlDocHandler;//记录文档句柄


//        [DllImport("cmldll.dll", EntryPoint = "cmlLoadDocFromMem", CallingConvention = CallingConvention.Cdecl)]
//        unsafe static extern void* cmlLoadDocFromMem(char* pucContent, Int32 siSize);//解析XML文档数据，返回文档句柄

//        [DllImport("cmldll.dll", EntryPoint = "cmlGetString", CallingConvention = CallingConvention.Cdecl)]
//        unsafe static extern Int32 cmlGetString(void* handle,
//                                                char* pscPath,
//                                                char* pucContent,
//                                                Int32 siSize);//获取文档中某元素的内容
//        [DllImport("cmldll.dll", EntryPoint = "cmlGetUInt", CallingConvention = CallingConvention.Cdecl)]
//        unsafe static extern Int32 cmlGetUInt(void* handle,
//                                              char* pscPath,
//                                              out UInt32 puiValue);//UInt32[] puiValue);//获取文档中某元素的内容

        
//        public IntPtr hwnd;//传递当前窗口句柄
//        public int m_nQueryFileNum=0;

//        [DllImport("LibZmsgDLL.dll", EntryPoint = "zfRecvw", CallingConvention = CallingConvention.Cdecl)]
//        static extern int zfRecvw(string floderpath,IntPtr hwnd);
//        [DllImport("LibZmsgDLL.dll", EntryPoint = "zfclose", CallingConvention = CallingConvention.Cdecl)]
//        static extern int zfclose();

//        [DllImport("LibZmsgDLL.dll", EntryPoint = "zfget", CallingConvention = CallingConvention.Cdecl)]
//        static extern int zfget(string szfileName,string szfilenames,int uiFileTypes,int uiTaskCode,int uiObjCode);

//        //定义委托 ZDSTATIONID
//        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
//        public delegate void StationIDCallBack(UInt32 stationID);
//        StationIDCallBack m_callStID;
//        [DllImport("LibZmsgDLL.dll", CallingConvention = CallingConvention.Cdecl)]
//        public static extern void Register_CallBack_StationID([MarshalAs(UnmanagedType.FunctionPtr)]StationIDCallBack call);
//        [DllImport("LibZmsgDLL.dll", EntryPoint = "zdStationID", CallingConvention = CallingConvention.Cdecl)]
//        static extern int zdStationID();
//        void InitStationIDList(UInt32 stationID)
//        {
//            string stID = stationID.ToString();
//            cmbStationID.Items.Add(stID);
//        }

//        //定义委托 ZDFILE
//        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
//        public delegate void DatabaseCallBack(IntPtr pName, int nName, IntPtr pNames, int nNames, int FileTypes,int TaskCode, int ObjCode);
//        DatabaseCallBack m_call;
//        [DllImport("LibZmsgDLL.dll", CallingConvention = CallingConvention.Cdecl)]
//        public static extern void Register_CallBack_Database([MarshalAs(UnmanagedType.FunctionPtr)]DatabaseCallBack call);

//        [DllImport("LibZmsgDLL.dll", EntryPoint = "zdFile", CallingConvention = CallingConvention.Cdecl)]
//        static extern int zdFile(int filetype,string strsql, IntPtr hwnd);

//        [DllImport("LibZmsgDLL.dll", EntryPoint = "zdFileCount", CallingConvention = CallingConvention.Cdecl)]
//        static extern int zdFileCount(int filetype, string strsql, IntPtr hwnd);

//        //输出数据库查询结果到GRID
//        void OutputSeekData(IntPtr pXml, int nXml, IntPtr pTar, int nTar, int uiFileTypes,int uiTaskCode, int uiObjCode)
//        {
//            m_nQueryFileNum++;
//            System.Text.ASCIIEncoding converter = new System.Text.ASCIIEncoding();            
            
//            byte[] bXml = new byte[nXml];
//            Marshal.Copy(pXml, bXml, 0, bXml.Length);
//            string strXml = converter.GetString(bXml);
//            string[] strXmlArray = strXml.Split('\0');
//            string strXmlname = strXmlArray[0];

//            byte[] bTar = new byte[nTar];
//            Marshal.Copy(pTar, bTar, 0, bTar.Length);
//            string strTar = converter.GetString(bTar);
//            string[] strTarArray = strTar.Split('\0');
//            string strTarname = strTarArray[0];

//            DataRow rowfile = zdFileTable.NewRow();
//            rowfile[1] = strXmlname;
//            rowfile[2] = strTarname;
//            rowfile[0] = m_nQueryFileNum;
//            rowfile[5] = uiFileTypes;
//            rowfile[3] = uiTaskCode;
//            rowfile[4] = uiObjCode;
//            zdFileTable.Rows.Add(rowfile);
            
//        }

//        public int dataGridRvselectIndex;//记录dataGridRv被选中的行数的Index
//        public int dataGridLocalSelectIndex;//记录dataGridLocal被选中的行数的Index
//        public int dataGridzdFileselectIndex;//记录dataGridzdFile被选中的行数的Index

//        public string strSaveFileFloder;//用于存储接收到的文件的路径

//        ////zg库定义委托
//        //[UnmanagedFunctionPointer(CallingConvention.StdCall)]
//        //public delegate void VecCallBack(double value1,double value2,double value3);
//        //VecCallBack zg_m_call;
//        //[DllImport("libzg.dll", CallingConvention = CallingConvention.Cdecl)]
//        //public static extern void Register_CallBack_Vec([MarshalAs(UnmanagedType.FunctionPtr)]VecCallBack call);

//        //[DllImport("libzg.dll", EntryPoint = "zg_vec", CallingConvention = CallingConvention.Cdecl)]
//        //static extern int zg_vec(int uiTaskCode, int uiObjCode, int vecindex);
//        //void OutLouVec(double Value1,double Value2,double Value3)
//        //{

//        //}
//        #endregion

        #region 环拍范围 太阳高度角 参数获取
      

        //public struct tagInstallMatrix
        //{
        //    //public double[] dOriMatrix;
        //    //public double[] dPosMatrix;
        //    public unsafe fixed double dOriMatrix[9];
        //    public unsafe fixed double dPosMatrix[3];
        //}

        //public struct Ex_OriPara
        //{
        //    public Pt_3d pos;//!<外方位线元素
        //    public Ori_Angle ori;//!<外方位角元素
        //}

        //public struct Pt_3d
        //{
        //    public double X, Y, Z;
        //    public ulong lID;
        //}

        //public struct Ori_Angle
        //{
        //    public double omg;//!<绕X轴转角
        //    public double phi;//!<绕Y轴转角
        //    public double kap;//!<绕Z轴转角
        //}


        [DllImport("MatrixDLL.dll", EntryPoint = "mlCalcCamOriInWorldByGivenInsMatPosX", CallingConvention = CallingConvention.Cdecl)]
        static extern bool mlCalcCamOriInWorldByGivenInsMatPosX(tagInstallMatrix matBase, double dExpAngle, double dYawAngle, double dPitchAngle,
                                                            tagInstallMatrix matMastExp2Body, tagInstallMatrix matMastYaw2Exp, tagInstallMatrix matMastPitch2Yaw,
                                                            tagInstallMatrix matLCamBase2Pitch, tagInstallMatrix matRCamBase2LCamBase,
                                                            tagInstallMatrix matLCamCap2Base, tagInstallMatrix matRCamCap2Base, 
                                                            out Ex_OriPara exOriCamL,  out Ex_OriPara exOriCamR );

#endregion

        public FrmMain()
        {
            InitializeComponent();
            //设置查询对话框位置
            this.panelLocal.Dock = DockStyle.Fill;
            this.panelRemote.Dock = DockStyle.Fill;
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {  
            this.Text = m_strApplicationName;
            #region //初始化仿真参数, syw
            cmbModelType.SelectedItem = cmbiCrater;
            cmbTritype.SelectedItem = cmbiForward;
            cmbMappingType.SelectedItem = cmbiPlat;
            cmbSubdivisionCount.SelectedItem = cmbiSubdivisionZero;
            m_CraterlistModels = new List<String>();
            m_CraterlistModels.Clear();
            m_NonCraterlistModels = new List<String>();
            m_NonCraterlistModels.Clear();
            m_manualAddModels = new List<Model>();
            m_manualAddModels.Clear();
            #endregion
            //m_CraterlistModels.Add(GetParentPathofExe() + @"Resource\DefaultFileGDB\DefaultCrater.3ds");
            //m_NonCraterlistModels.Add(GetParentPathofExe() + @"Resource\DefaultFileGDB\DefaultNonCrater.3ds");
            //*******************************************************************************************************************************
            SymbolStyle[0] = GetParentPathofExe() + @"Resource\Style\ESRI.ServerStyle";
            SymbolStyle[1] = GetParentPathofExe() + @"Resource\Style\ArcScene Basic.ServerStyle";
            //initialize the controls synchronization class
            m_controlsSynchronizer = new LibCerMap.ControlsSynchronizer((IMapControl3)axMapCtlMain.Object, (IPageLayoutControl2)axPageLayoutCtlMain.Object
                , (ISceneControl)axSceneCtlMain.Object);

            #region 布局工具栏增加自定义工具
            cmdLeg = new CmdLegend(SymbolStyle);
            cmdMapGrid = new CmdMapGrid(SymbolStyle);
            axToolbarControlLayout.AddItem(new LibEngineCmd.CmdOpenMxdDoc(m_controlsSynchronizer,barAttributeTable, AttributeTable,null), -1, 0, false, 0, esriCommandStyles.esriCommandStyleIconOnly);
            axToolbarControlLayout.AddItem(new LibEngineCmd.CmdSaveMxdDoc(), -1, 1, false, 0, esriCommandStyles.esriCommandStyleIconOnly);
            axToolbarControlLayout.AddItem(new CmdSetDefaultElemetStyle(SymbolStyle), -1, -1, true, 0, esriCommandStyles.esriCommandStyleIconOnly);
            axToolbarControlLayout.AddItem(cmdNorthArrow, -1, -1, false, 0, esriCommandStyles.esriCommandStyleIconOnly);
            axToolbarControlLayout.AddItem(cmdScaleBar, -1, -1, false, 0, esriCommandStyles.esriCommandStyleIconOnly);
            axToolbarControlLayout.AddItem(cmdScaleText, -1, -1, false, 0, esriCommandStyles.esriCommandStyleIconOnly);
            axToolbarControlLayout.AddItem(cmdLeg, -1, -1, false, 0, esriCommandStyles.esriCommandStyleIconOnly);
            axToolbarControlLayout.AddItem(cmdMapGrid, -1, -1, false, 0, esriCommandStyles.esriCommandStyleIconOnly);
            axToolbarControlLayout.AddItem(cmdAddText, -1, -1, false, 0, esriCommandStyles.esriCommandStyleIconOnly);
            axToolbarControlLayout.AddItem(new CmdNeatLine(SymbolStyle), -1, -1, false, 0, esriCommandStyles.esriCommandStyleIconOnly);
            axToolbarControlLayout.AddItem(new CmdPictureElement(), -1, -1, false, 0, esriCommandStyles.esriCommandStyleIconOnly);
            axToolbarControlLayout.AddItem(new CmdPageSetup(document), -1, -1, true, 0, esriCommandStyles.esriCommandStyleIconOnly);
            axToolbarControlLayout.AddItem(new CmdPrintPreview(document), -1, -1, false, 0, esriCommandStyles.esriCommandStyleIconOnly);
            axToolbarControlLayout.AddItem(cmdPrintMap, -1, -1, false, 0, esriCommandStyles.esriCommandStyleIconOnly);
            axToolbarControlLayout.AddItem(cmdExpActFig, -1, -1, false, 0, esriCommandStyles.esriCommandStyleIconOnly);
            axToolbarControlLayout.SetBuddyControl(axPageLayoutCtlMain);
            #endregion
            
            #region 三维工具栏增加自定义工具
            ArrayList arrayControls = new ArrayList();
            arrayControls.Add(axMapCtlMain);
            arrayControls.Add(axSceneCtlMain);
            arrayControls.Add(axTOCCtlLayer);
            m_ToolEditFeatures = new ToolEditFeatures(arrayControls);
            //编辑物体的功能放到mapcontrol
           // axToolbarControlScene.AddItem(m_ToolEditFeatures, -1, -1, false, 0, esriCommandStyles.esriCommandStyleIconOnly);
            axToolbarControlScene.AddItem(new CmdExportScene(), -1, -1, true, 0, esriCommandStyles.esriCommandStyleIconOnly);
            axToolbarControlScene.AddItem(new CmdPrintSceneView((ISceneControl)axSceneCtlMain.Object), -1, -1, false, 0, esriCommandStyles.esriCommandStyleIconOnly);
            axToolbarControlScene.AddItem(new CmdOpenSceneDoc((ISceneControl)axSceneCtlMain.Object), -1, -1, true, 0, esriCommandStyles.esriCommandStyleIconOnly);            
            axToolbarControlScene.AddItem(new CmdSaveSceneDoc((ISceneControl)axSceneCtlMain.Object), -1, -1, false, 0, esriCommandStyles.esriCommandStyleIconOnly);
            axToolbarControlScene.AddItem(new CmdCreateScene(axMapCtlMain.Object as IMapControl2), -1, -1, false, 0, esriCommandStyles.esriCommandStyleIconOnly);
            #endregion
            
            #region Toc控件选中地图右键菜单
            m_menuFrame = new ToolbarMenuClass();
            cmdMapUnit = new CmdMapUnits(this.axMapCtlMain);
            cmdCoordSystem = new CmdCoordSystem((IMapControl3)axMapCtlMain.Object, (IMapControl3)axMapControlHide.Object);
            //cmdCoordSystem.pMapControl = this.axMapCtlMain.Object as IMapControl3;
            //cmdCoordSystem.pMapControlSpacial = this.axMapControlHide.Object as IMapControl3;

            m_menuFrame.AddItem(new CmdTurnOnAllLayer(), 1, -1, false, esriCommandStyles.esriCommandStyleIconAndText);
            m_menuFrame.AddItem(new CmdTurnOffAllLayer(), 1, -1, false, esriCommandStyles.esriCommandStyleIconAndText);
            m_menuFrame.AddItem(cmdMapUnit, 1, -1, false, esriCommandStyles.esriCommandStyleIconAndText);
            m_menuFrame.AddItem(cmdCoordSystem, 1, -1, false, esriCommandStyles.esriCommandStyleIconAndText);
            //m_menuFrame.AddItem(new CmdActivMap(),0,-1,false,esriCommandStyles.esriCommandStyleIconAndText);
            #endregion
            
            #region   Toc控件右键菜单
            m_menuLayer = new ToolbarMenuClass();
            m_AttributeTable = new CmdAttributeTable(barAttributeTable);
            m_labeldesign = new CmdLabelDesign();
            
            m_joinattribute = new CmdJoinAttribute();
            m_menuLayer.AddItem(new CmdRemoveLayer((ITOCControlDefault)axTOCCtlLayer.Object,barAttributeTable, this.AttributeTable), -1, -1, false, esriCommandStyles.esriCommandStyleIconAndText);
            m_menuLayer.AddItem(new CmdZoomToLayer((ITOCControlDefault)axTOCCtlLayer.Object), -1, -1, false, esriCommandStyles.esriCommandStyleIconAndText);
            m_menuLayer.AddItem(m_AttributeTable, -1, -1, true, esriCommandStyles.esriCommandStyleIconAndText);
            m_menuLayer.AddItem(m_joinattribute, -1, -1, false, esriCommandStyles.esriCommandStyleIconAndText);
            IToolbarMenu subToolBar = new ToolbarMenuClass();
            subToolBar.Caption = "设置显示范围";
            subToolBar.AddItem(new CmdScaleThresholds(), 1, -1, false, esriCommandStyles.esriCommandStyleIconAndText);
            subToolBar.AddItem(new CmdScaleThresholds(), 2, -1, false, esriCommandStyles.esriCommandStyleIconAndText);
            subToolBar.AddItem(new CmdScaleThresholds(), 3, -1, false, esriCommandStyles.esriCommandStyleIconAndText);
            m_menuLayer.AddSubMenu(subToolBar);
            m_menuLayer.AddItem(new CmdLayerSelectable(), -1, -1, true, esriCommandStyles.esriCommandStyleIconAndText);
            m_menuLayer.AddItem(new CmdLayerSelectableFalse(), -1, -1, false, esriCommandStyles.esriCommandStyleIconAndText);
            m_menuLayer.AddItem(new CmdCreateSelectionLayer((ITOCControlDefault)axTOCCtlLayer.Object), -1, -1, false, esriCommandStyles.esriCommandStyleIconAndText);

            m_menuLayer.AddItem(m_labeldesign, -1, -1, true, esriCommandStyles.esriCommandStyleIconAndText);
            m_menuLayer.AddItem(new CmdExportSHP((ITOCControlDefault)axTOCCtlLayer.Object), -1, -1, true, esriCommandStyles.esriCommandStyleIconAndText);
           // m_menuLayer.AddItem(new CmdCreateRasterBandLayer((ITOCControlDefault)axTOCCtlLayer.Object), -1, -1, true, esriCommandStyles.esriCommandStyleIconAndText);
            m_menuLayer.AddItem(new CmdChDataSource((IMapControl2)axMapControlHide.Object,(ITOCControlDefault)axTOCCtlLayer.Object), -1, -1, false, esriCommandStyles.esriCommandStyleIconAndText);
            
            m_CmdSceneLayerOffset = new CmdSceneLayerOffset();
            m_menuLayer.AddItem(m_CmdSceneLayerOffset, -1, -1, true, esriCommandStyles.esriCommandStyleIconAndText);

            m_menuLayer.AddItem(new CmdLayerProperties(), -1, -1, false, esriCommandStyles.esriCommandStyleIconAndText);
            #endregion

            #region 布局右键菜单
            m_menuElement = new ToolbarMenuClass();
            
            m_menuElement.AddItem(new CmdElementAttribute(SymbolStyle), -1, -1, false, esriCommandStyles.esriCommandStyleIconAndText);
            m_menuElement.AddItem("esriControls.ControlsAlignToMarginsCommand", -1, -1, true, esriCommandStyles.esriCommandStyleIconAndText);
            m_menuElement.AddItem("esriControls.ControlsAlignCenterCommand", -1, -1, false, esriCommandStyles.esriCommandStyleIconAndText);
            m_menuElement.AddItem("esriControls.ControlsAlignMiddleCommand", -1, -1, false, esriCommandStyles.esriCommandStyleIconAndText);
            m_menuElement.AddItem("esriControls.ControlsAlignLeftCommand", -1, -1, false, esriCommandStyles.esriCommandStyleIconAndText);
            m_menuElement.AddItem("esriControls.ControlsAlignRightCommand", -1, -1, false, esriCommandStyles.esriCommandStyleIconAndText);
            m_menuElement.AddItem("esriControls.ControlsAlignTopCommand", -1, -1, false, esriCommandStyles.esriCommandStyleIconAndText);
            m_menuElement.AddItem("esriControls.ControlsAlignBottomCommand", -1, -1, false, esriCommandStyles.esriCommandStyleIconAndText);

            m_menuElement.AddItem("esriControls.ControlsEditingCutCommand", -1, -1, true, esriCommandStyles.esriCommandStyleIconAndText);
            m_menuElement.AddItem("esriControls.ControlsEditingCopyCommand", -1, -1, false, esriCommandStyles.esriCommandStyleIconAndText);
            //m_menuElement.AddItem("esriControls.ControlsEditingPasteCommand", -1, -1, true, esriCommandStyles.esriCommandStyleIconAndText);            
            m_menuElement.AddItem("esriControls.ControlsEditingClearCommand", -1, -1, false, esriCommandStyles.esriCommandStyleIconAndText);
            //m_menuElement.AddItem(new CmdDelectSelElement(), -1, -1, true, esriCommandStyles.esriCommandStyleIconAndText);
            

            
            m_menuElement.SetHook(axPageLayoutCtlMain);
            #endregion

            #region TIN工具栏
            axTOCCtlLayer.EnableLayerDragDrop = true;
            axToolbarControlTinEdit.AddItem(new CmdAddTinLayer(), -1, -1, false, 0, esriCommandStyles.esriCommandStyleIconOnly);
            m_cmdStartEditTinLayer = new CmdStartEditTinLayer();
            axToolbarControlTinEdit.AddItem(m_cmdStartEditTinLayer, -1, -1, false, 0, esriCommandStyles.esriCommandStyleIconOnly);
            m_cmdTinSaveAs = new CmdTinSaveAs();
            axToolbarControlTinEdit.AddItem(m_cmdTinSaveAs, -1, -1, false, 0, esriCommandStyles.esriCommandStyleIconOnly);

            m_cmdStopEditTinLayer = new CmdStopEditTinLayer();
            axToolbarControlTinEdit.AddItem(m_cmdStopEditTinLayer, -1, -1, false, 0, esriCommandStyles.esriCommandStyleIconOnly);
            m_cmdSaveEditTinLayer = new CmdSaveEditTinLayer();
            axToolbarControlTinEdit.AddItem(m_cmdSaveEditTinLayer, -1, -1, false, 0, esriCommandStyles.esriCommandStyleIconOnly);
            m_toolAddTinNode = new ToolAddTinNode();
            axToolbarControlTinEdit.AddItem(m_toolAddTinNode, -1, -1, false, 0, esriCommandStyles.esriCommandStyleIconOnly);
            m_toolDeleteTINNode = new ToolDeleteTINNode();
            axToolbarControlTinEdit.AddItem(m_toolDeleteTINNode, -1, -1, false, 0, esriCommandStyles.esriCommandStyleIconOnly);
            m_toolModifyTinNode = new ToolModifyTINNode();
            axToolbarControlTinEdit.AddItem(m_toolModifyTinNode, -1, -1, false, 0, esriCommandStyles.esriCommandStyleIconOnly);
            
            m_toolAddTinPlane = new ToolAddTinPlane();
            axToolbarControlTinEdit.AddItem(m_toolAddTinPlane, -1, -1, false, 0, esriCommandStyles.esriCommandStyleIconOnly);
            
            m_ToolDeleteNodeByArea = new ToolDeleteNodeByArea();
            axToolbarControlTinEdit.AddItem(m_ToolDeleteNodeByArea, -1, -1, false, 0, esriCommandStyles.esriCommandStyleIconOnly);

            m_ToolAddCraterOnTIN = new ToolAddCraterOnTIN();
            m_ToolAddCraterOnTIN.m_listModels = m_CraterlistModels;
            m_ToolAddCraterOnTIN.m_manualAddModels = m_manualAddModels;
            axToolbarControlTinEdit.AddItem(m_ToolAddCraterOnTIN, -1, -1, false, 0, esriCommandStyles.esriCommandStyleIconOnly);

            m_ToolAddStone = new ToolAddStone();
            m_ToolAddStone.m_listModels = m_NonCraterlistModels;
            m_ToolAddStone.m_manualAddModels = m_manualAddModels;
            axToolbarControlTinEdit.AddItem(m_ToolAddStone, -1, -1, false, 0, esriCommandStyles.esriCommandStyleIconOnly);

            m_ToolExtractCraterSample = new ToolExtractCraterSample();
            axToolbarControlTinEdit.AddItem(m_ToolExtractCraterSample, -1, -1, false, 0, esriCommandStyles.esriCommandStyleIconOnly);

            m_ToolEditFeaturesInMapCtrl = new ToolEditFeatures(arrayControls);
            axToolbarControlTinEdit.AddItem(m_ToolEditFeaturesInMapCtrl, -1, -1, false, 0, esriCommandStyles.esriCommandStyleIconOnly);
            #endregion

            #region 栅格处理工具
               
            m_ToolAddControlPoints = new ToolAddControlPoints();
            CmdDeleteControlPoints CDC = new CmdDeleteControlPoints();
            CDC.m_ToolAddControlPoints = m_ToolAddControlPoints;
            m_cmdRasterRegister = new CmdRasterRegister(m_ToolAddControlPoints.m_FrmLinkTableRaster);   
        
            axToolbarControlRectifyRaster.AddItem(m_cmdRasterRegister, -1, -1, false, 1, esriCommandStyles.esriCommandStyleIconOnly);                   
            axToolbarControlRectifyRaster.AddItem(m_ToolAddControlPoints, -1, -1, false, 0, esriCommandStyles.esriCommandStyleIconOnly);
            //查看控制点表
            CmdViewLinkTabale m_CmdViewLinkTabale = new CmdViewLinkTabale();
            m_CmdViewLinkTabale.m_ToolAddControlPoints = m_ToolAddControlPoints;
            axToolbarControlRectifyRaster.AddItem(m_CmdViewLinkTabale, -1, -1, false, 0, esriCommandStyles.esriCommandStyleIconOnly);            
            axToolbarControlRectifyRaster.AddItem(CDC, -1, -1, false, 0, esriCommandStyles.esriCommandStyleIconOnly);            
        
            m_ToolRasterShift = new ToolRasterShift();
            axToolbarControlRectifyRaster.AddItem(m_ToolRasterShift, -1, -1, true, 0, esriCommandStyles.esriCommandStyleIconOnly);                   
            m_cmdRasterShift = new CmdRasterShift();
            axToolbarControlRectifyRaster.AddItem(m_cmdRasterShift, -1, -1, false, 1, esriCommandStyles.esriCommandStyleIconOnly);
            m_cmdRasterRotate = new CmdRasterRotate();
            axToolbarControlRectifyRaster.AddItem(m_cmdRasterRotate, -1, -1, false, 1, esriCommandStyles.esriCommandStyleIconOnly);
            m_cmdRasterMirror = new CmdRasterMirror();
            axToolbarControlRectifyRaster.AddItem(m_cmdRasterMirror, -1, -1, false, 1, esriCommandStyles.esriCommandStyleIconOnly);
            m_cmdRasterFlip = new CmdRasterFlip();
            axToolbarControlRectifyRaster.AddItem(m_cmdRasterFlip, -1, -1, false, 1, esriCommandStyles.esriCommandStyleIconOnly);          
            m_cmdRasterResample = new CmdRasterResample();
            axToolbarControlRectifyRaster.AddItem(m_cmdRasterResample, -1, -1, false, 1, esriCommandStyles.esriCommandStyleIconOnly);

            m_cmdRasterNED2ENU = new CmdRasterNED2ENU();
            axToolbarControlRectifyRaster.AddItem(m_cmdRasterNED2ENU, -1, -1, true, 1, esriCommandStyles.esriCommandStyleIconOnly);
            m_cmdRasterTransZ = new CmdRasterTransZ(axTOCCtlLayer.Object as ITOCControl);
            axToolbarControlRectifyRaster.AddItem(m_cmdRasterTransZ, -1, -1, false, 1, esriCommandStyles.esriCommandStyleIconOnly);
            m_cmdRasterMeaAtt = new CmdRasterMeaAtt(this.axMapCtlMain, axToolbarControlCommon);
            axToolbarControlRectifyRaster.AddItem(m_cmdRasterMeaAtt, -1, -1, false, 1, esriCommandStyles.esriCommandStyleIconOnly);
            m_ToolRasterEdit = new ToolRasterEdit();
            axToolbarControlRectifyRaster.AddItem(m_ToolRasterEdit, -1, -1, false, 1, esriCommandStyles.esriCommandStyleIconOnly);

            //栅格处理与分析      
            m_cmdRasterSubset = new CmdRasterSubset();
            axToolbarControlRectifyRaster.AddItem(m_cmdRasterSubset, -1, -1, true, 1, esriCommandStyles.esriCommandStyleIconOnly);
            m_cmdRasterMosaic = new CmdRasterMosaic();
            axToolbarControlRectifyRaster.AddItem(m_cmdRasterMosaic, -1, -1, false, 1, esriCommandStyles.esriCommandStyleIconOnly);
            
            //m_cmdSurfaceOp = new CmdSurfaceOp();
            //axToolbarControlRectifyRaster.AddItem(m_cmdSurfaceOp, -1, -1, false, 1, esriCommandStyles.esriCommandStyleIconOnly); 
            //m_cmdProfileAnalysis = new CmdProfileAnalysis();
            //axToolbarControlRectifyRaster.AddItem(m_cmdProfileAnalysis, -1, -1, false, 1, esriCommandStyles.esriCommandStyleIconOnly);
            #endregion

            #region 编辑工具栏
            axToolbarControlEdit.AddItem(new ToolSplitLine(), -1, -1, false, 0, esriCommandStyles.esriCommandStyleIconOnly);
            axToolbarControlEdit.AddItem(new ToolCreateCircle(), -1, -1, false, 0, esriCommandStyles.esriCommandStyleIconOnly);
            axToolbarControlEdit.AddItem(new CmdCreateParrallel(), -1, -1, false, 0, esriCommandStyles.esriCommandStyleIconOnly);
            axToolbarControlEdit.AddItem(new CmdChangeAnnotationSymbol(), -1, -1, false, 0, esriCommandStyles.esriCommandStyleIconOnly);
            axToolbarControlEdit.AddItem(new CmdFeatureMerge(), -1, -1, false, 0, esriCommandStyles.esriCommandStyleIconOnly);
            #endregion

            #region 矢量图空间校正
            m_ViewLinkTable = new CmdViewLinkTable();
            m_SetAdjustData = new CmdSetAdjustData();
            m_Adjust = new CmdAdjust();
            m_NewDisplacement = new ToolNewDisplacement();
            axToolbarControlSpatialAdjust.AddItem(m_SetAdjustData, -1, -1, false, 0, esriCommandStyles.esriCommandStyleIconOnly);
            axToolbarControlSpatialAdjust.AddItem(m_NewDisplacement, -1, -1, false, 0, esriCommandStyles.esriCommandStyleIconOnly);
            axToolbarControlSpatialAdjust.AddItem(m_ViewLinkTable, -1, -1, false, 0, esriCommandStyles.esriCommandStyleIconOnly);
            axToolbarControlSpatialAdjust.AddItem(m_Adjust, -1, -1, false, 0, esriCommandStyles.esriCommandStyleIconOnly);

            m_SetAdjustData.NewDisplacement = m_NewDisplacement;
            m_NewDisplacement.ListLayers = m_SetAdjustData.ListLayers;
            //cmbAdjustMethod.SelectedItem = cbiAffine;

            m_ViewLinkTable.NewDisplacement = m_NewDisplacement;
            m_Adjust.m_AdjustMehod = CmdAdjust.AdjustMethod.AdjustMethod_Affine;
            m_Adjust.m_NewDisplacement = m_NewDisplacement;
            m_Adjust.m_CmdSetAdjustData = m_SetAdjustData;
            m_Adjust.m_pControl = axMapCtlMain.Object as IMapControl2;

            //地图事件绑定不起作用
            //ICompositeLayer cgl = (ICompositeLayer)axMapCtlMain.Map.BasicGraphicsLayer;
            //if (cgl.Count == 0)
            //{
            //    m_NewDisplacement.pGraphicsLayer = ((ICompositeGraphicsLayer)axMapCtlMain.Map.BasicGraphicsLayer).AddLayer("AdjustLayer", null);
            //    IGraphicsContainerEvents_Event pGCE = axMapCtlMain.Map.BasicGraphicsLayer as IGraphicsContainerEvents_Event;
            //    pGCE.ElementAdded += new ESRI.ArcGIS.Carto.IGraphicsContainerEvents_ElementAddedEventHandler(ElementAddedMethod);
            //    pGCE.ElementUpdated += new ESRI.ArcGIS.Carto.IGraphicsContainerEvents_ElementUpdatedEventHandler(ElementUpdatedMethod);
            //    pGCE.ElementDeleted += new ESRI.ArcGIS.Carto.IGraphicsContainerEvents_ElementDeletedEventHandler(ElementDeletedMethod);
            //}
            #endregion

            #region 地平线高度角测量
            axToolbarSunAltitude.AddItem(m_toolSunAltCheckState, -1, -1, false, 0, esriCommandStyles.esriCommandStyleIconOnly);
            //axToolbarSunAltitude.AddItem(m_cmdGetSunAltXmlFile, -1, -1, false, 0, esriCommandStyles.esriCommandStyleIconOnly);
            axToolbarSunAltitude.AddItem(m_cmdSunAltProperty, -1, -1, false, 0, esriCommandStyles.esriCommandStyleIconOnly);
            axToolbarSunAltitude.AddItem(m_cmdClearAllSunAltPts, -1, -1, false, 0, esriCommandStyles.esriCommandStyleIconOnly);

            //设置属性值
            m_toolSunAltCheckState.m_pLayer = null;
            m_toolSunAltCheckState.m_szXmlName = this.txtSunAltXmlFile.Text;
            m_cmdSunAltProperty.m_toolSunAltCheckState = m_toolSunAltCheckState;
            m_cmdSunAltProperty.m_frmSunAltitude.m_sunAltInfoList = this.m_SunAltPointCollection;
            //m_cmdSunAltProperty.m_frmSunAltitude.m_clsCameraPara = this.m_cameraPara;
            m_cmdSunAltProperty.m_frmSunAltitude.m_pMapControl = this.axMapCtlMain.Object as IMapControl3;
            m_cmdClearAllSunAltPts.m_sunAltPts = this.m_SunAltPointCollection;
            m_cmdClearAllSunAltPts.m_frmAltitude = this.m_cmdSunAltProperty.m_frmSunAltitude;

            ////initialize the controls synchronization class
            //m_controlsSynchronizer = new ControlsSynchronizer((IMapControl3)axMapCtlMain.Object, (IPageLayoutControl2)axPageLayoutCtlMain.Object
            //    , (ISceneControl)axSceneCtlMain.Object);
            #endregion

            #region 如果有map和layout两个空间公用的工具栏应该用以下方法绑定
            //add the framework controls (TOC and Toolbars) in order to synchronize then when the
            //active control changes (call SetBuddyControl)                  
            m_controlsSynchronizer.AddFrameworkControl(axTOCCtlLayer.Object);
            m_controlsSynchronizer.AddFrameworkControl(axToolbarControlEdit.Object);
            m_controlsSynchronizer.AddFrameworkControl(axToolbarControlCommon.Object);
            m_controlsSynchronizer.AddFrameworkControl(axToolbarControlLayout.Object);
            m_controlsSynchronizer.AddFrameworkControl(axToolbarControlTinEdit.Object);
            m_controlsSynchronizer.AddFrameworkControl(m_menuFrame);
            //////////////////////////////////////////////////////////////////////////
            //Add by Liuzhaoqin
            m_controlsSynchronizer.AddFrameworkControl(axToolbarControlRectifyRaster.Object);
            m_controlsSynchronizer.AddFrameworkControl(axToolbarControlSpatialAdjust.Object);
            m_controlsSynchronizer.AddFrameworkControl(axToolbarControlScene.Object);
            m_controlsSynchronizer.AddFrameworkControl(axToolbarControlCustom.Object);
            m_controlsSynchronizer.AddFrameworkControl(axToolbarCtlMenuEditor.Object);
            //////////////////////////////////////////////////////////////////////////
           // m_controlsSynchronizer.AddFrameworkControl(m_menuLayer);
            // m_menuLayer需要绑定在mapcontrol上，里边的command有Imapcontrol属性，如果绑定在pagelayout上会有问题
            m_menuLayer.SetHook(axMapCtlMain);
            //m_controlsSynchronizer.m_mapReplaceEvent += new ControlsSynchronizer.MapReplaceEvent(axMapCtlMain_OnMapReplaced);
            #endregion

            #region Multi GDB测试
            ///// GDBdataacess.Create_FileGDB(path, "MultiPathGDB");
            //IWorkspace pWS = GDBdataacess.OpenFromFileGDB(path + "MultiPatch.gdb");
            // IFeatureWorkspace pFW = pWS as IFeatureWorkspace;
            //// GDBdataacess.CreateFeatureClass((IWorkspace2)pWS, null, "www", null, null, null, null);
            // IFeatureClass pFC = pFW.OpenFeatureClass("zzz");           

            // //IMultiPatch pMP = new MultiPatchClass();
            // for (int i = 0; i < 5; i++)
            // {
            //     IFeature pF = pFC.CreateFeature();
            //     IImport3DFile pI3D = new Import3DFileClass();
            //     pI3D.CreateFromFile(@"C:\Users\Administrator\Desktop\sampleAnalysis\1000.3ds");
            //     IMultiPatch pMP = pI3D.Geometry as IMultiPatch;
            //     // ITransform3D pT3D = pMP as ITransform3D;
            //    // pF.Shape = pMP as IGeometry;
            //     ITransform3D pT3D = pF.Shape as ITransform3D;
            //     pT3D.Move3D(5293 + 1600, -20427, 800 + i * 400);
            //     pF.Shape = pI3D.Geometry;
            //     pF.Store();
            // }
            #endregion

            #region 测试用
            /*
            FrmEdit3DFeatures frme3d = new FrmEdit3DFeatures();
            IWorkspace pWS = GDBdataacess.OpenFromFileGDB(path + "MultiPatch.gdb");
            IFeatureWorkspace pFW = pWS as IFeatureWorkspace;
            IFeatureClass pFC = pFW.OpenFeatureClass("www");

            IFeatureCursor pFCursor = pFC.Update(null, false);
            IFeature pFeature = pFCursor.NextFeature();
            frme3d.StartEditing(pFeature);
            IMultiPatch pMpatch = pFeature.Shape as IMultiPatch;
            ITransform3D pTf3D = pMpatch as ITransform3D;
            pTf3D.Move3D(1000, 0, 0);
            pFeature.Shape = pMpatch as IGeometry;
            pFeature.set_Value(2, "bad");
            pFeature.Store();
           // pFCursor.UpdateFeature(pFeature);
           frme3d.StopEditing(pFeature);

            pFCursor = pFC.Search(null, true);
            pFeature = pFCursor.NextFeature();
            */


            //ceshi
            //IWorkspaceFactory pWSFact = new TinWorkspaceFactoryClass();
            //IWorkspace pWS = pWSFact.OpenFromFile(@"C:\Users\Administrator\Desktop\sampleAnalysis\TIN\", 0);
            //ITinWorkspace pTinWS = pWS as ITinWorkspace;
            //ITin pTin = pTinWS.OpenTin("wactin.tin");
            ////将TIN变为TIN图层
            //ITinLayer pTinLayer = new TinLayerClass();
            //pTinLayer.Dataset = pTin;
            //pTinLayer.Name = "wactin.tin";
            //axMapCtlMain.Map.AddLayer((ILayer)pTinLayer);
            //
            #endregion

            #region 用于MapControl上绘制图形
            IntPtr ptr = new IntPtr(axMapCtlMain.hWnd);
            penblue.Width = 2;
            penyelow.Width = 2;
            gcs = Graphics.FromHwnd(ptr);
            #endregion

            #region 设置自定义工具窗口
            //Create a new customize dialog
            m_CustomizeDialog = new CustomizeDialogClass();
            //Set the customize dialog events 
            startDialogE = new ICustomizeDialogEvents_OnStartDialogEventHandler(OnStartCustomDialog);
            ((ICustomizeDialogEvents_Event)m_CustomizeDialog).OnStartDialog += startDialogE;
            closeDialogE = new ICustomizeDialogEvents_OnCloseDialogEventHandler(OnCloseCustomDialog);
            ((ICustomizeDialogEvents_Event)m_CustomizeDialog).OnCloseDialog += closeDialogE;
            m_CustomizeDialog.SetDoubleClickDestination(axToolbarControlCommon);

           
            #endregion

            #region SceneControl添加键盘快捷键
            IGraphicsContainerEvents_Event pGCE1 = m_EngineInkEnvironmentClass as IGraphicsContainerEvents_Event;
            axSceneCtlMain.KeyIntercept = (int)esriKeyIntercept.esriKeyInterceptArrowKeys;
            #endregion


            #region 读取工具栏状态
             try
            {
                dotNetBarManagerMain.LoadLayout(GetParentPathofExe() + @"Resource\configure\Layout.cfg");   
                LoadToolbarControl(axToolbarControlCustom);
                //LoadToolbarControl(axToolbarCtlMenuEditor);//暂时不用
            }
            catch (System.Exception ex)
            {

            }
            #endregion

            #region 地图窗口编辑右键菜单,放到调入工具栏之后，从自定义工具栏上动态获取命令
            m_menuEditor = new ToolbarMenu();
            m_menuEditor.CommandPool = axToolbarCtlMenuEditor.CommandPool;
            m_menuEditor.AddItem(new CmdChangeAnnotationSymbol(), 0, 0, false, esriCommandStyles.esriCommandStyleIconAndText);
            m_menuEditor.AddItem("esriControls.ControlsEditingSketchContextMenu", 0, 0, false, esriCommandStyles.esriCommandStyleIconAndText);
            m_menuEditor.AddItem("esriControls.ControlsEditingVertexContextMenu", 0, 0, true, esriCommandStyles.esriCommandStyleIconAndText);

            //Set the Engine Edit Sketch
            m_EngineEditSketch = (IEngineEditSketch)m_EngineEditor;
            m_menuEditor.SetHook(axMapCtlMain.Object);
            //SetEditorMenu(m_menuEditor, axToolbarCtlMenuEditor);
                       
             #endregion

            #region  设置缺省工具，放到所有初始化完成之后
            

            if (dockMap.Selected == true)
            {
                m_controlsSynchronizer.BindControls(true);
                SetCurrentTool(axToolbarControlCommon, "esriControls.ControlsMapPanTool");
                m_controlsSynchronizer.m_mapActiveTool = axToolbarControlCommon.CurrentTool;
            }
            if (dockLayout.Selected == true)
            {
                m_controlsSynchronizer.BindControls(false);
                SetCurrentTool(axToolbarControlLayout, "esriControls.ControlsSelectTool");
                m_controlsSynchronizer.m_pageLayoutActiveTool = axToolbarControlLayout.CurrentTool;
            }
            if (dockScene.Selected == true)
            {
                m_controlsSynchronizer.BindControls(false);
                SetCurrentTool(axToolbarControlScene, "esriControls.ControlsSceneNavigateTool");
                m_controlsSynchronizer.m_scenceActiveTool = axToolbarControlLayout.CurrentTool;
            }
            #endregion

            #region 菜单栏制图按钮初始状态设定
            if (barMain.SelectedDockTab == 1)
            {
                btnAddText.Enabled = true;
                btnAddNorthArrow.Enabled = true;
                btnAddScallBar.Enabled = true;
                btnAddScallText.Enabled = true;
                btnAddGrid.Enabled = true;
                btnAddLegend.Enabled = true;
                btnExportMap.Enabled = true;
                btnPrintMap.Enabled = true;
            }
            else
            {
                btnAddText.Enabled = false;
                btnAddNorthArrow.Enabled = false;
                btnAddScallBar.Enabled = false;
                btnAddScallText.Enabled = false;
                btnAddGrid.Enabled = false;
                btnAddLegend.Enabled = false;
                btnExportMap.Enabled = false;
                btnPrintMap.Enabled = false;
            }
            #endregion

            #region 地图模板加载
            DirectoryInfo dri = new DirectoryInfo(GetParentPathofExe() + @"Resource\MapTemplate");
            foreach (FileInfo NextFile in dri.GetFiles())
            {
                if (NextFile.Extension.ToUpper().Equals(".MXD"))
                {
                    this.treeViewTemplate.Nodes.Add(NextFile.Name);
                }
            }
            #endregion

           
            
            #region 监听编辑事件
            ((IEngineEditEvents_Event)m_EngineEditor).OnDeleteFeature += new IEngineEditEvents_OnDeleteFeatureEventHandler(FrmMain_OnDeleteFeature);
            ((IEngineEditEvents_Event)m_EngineEditor).OnCreateFeature += new IEngineEditEvents_OnCreateFeatureEventHandler(FrmMain_OnCreateFeature);
            ((IEngineEditEvents_Event)m_EngineEditor).OnSaveEdits += new IEngineEditEvents_OnSaveEditsEventHandler(FrmMain_OnSaveEdits);
            ((ESRI.ArcGIS.Carto.IActiveViewEvents_Event)axMapCtlMain.Map).ItemAdded += new ESRI.ArcGIS.Carto.IActiveViewEvents_ItemAddedEventHandler(FrmMain_ItemAdded);
            ((ESRI.ArcGIS.Carto.IActiveViewEvents_Event)axMapCtlMain.Map).ItemDeleted += new ESRI.ArcGIS.Carto.IActiveViewEvents_ItemDeletedEventHandler(FrmMain_ItemDeleted);
            #endregion

            InitialAttributeTable();

            ////初始化文件类型列表框
            //ClsZfDatabase zf = new ClsZfDatabase();
            //zf.InitialZF_FileType(m_pListFielType);
            //cmbfiletype.Items.Clear();
            //for (int i = 0; i < m_pListFielType.Count;i++ )
            //{
            //    cmbfiletype.Items.Add(m_pListFielType[i].strDescription);
            //}

            #region 监听文件命令
            try
            {
                //string strDefaultPath = @"D:\CEMAP";
                //CreateFloder(strDefaultPath);
                //btnStartListen_Click(null, null);

                ////初始化数据库回调函数
                //m_call = new DatabaseCallBack(OutputSeekData);
                //Register_CallBack_Database(m_call);
                //m_callStID = new StationIDCallBack(InitStationIDList);
                //Register_CallBack_StationID(m_callStID);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            #endregion

            barDataRv.Visible = false;
        }


        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            ((IEngineEditEvents_Event)m_EngineEditor).OnDeleteFeature -= FrmMain_OnDeleteFeature;
            ((IEngineEditEvents_Event)m_EngineEditor).OnCreateFeature -= FrmMain_OnCreateFeature;
            ((IEngineEditEvents_Event)m_EngineEditor).OnSaveEdits -= FrmMain_OnSaveEdits;
            ((ICustomizeDialogEvents_Event)m_CustomizeDialog).OnStartDialog -= OnStartCustomDialog;
            ((ICustomizeDialogEvents_Event)m_CustomizeDialog).OnCloseDialog -= OnCloseCustomDialog;
            //((ESRI.ArcGIS.Carto.IActiveViewEvents_Event)axMapCtlMain.Map).ItemAdded -= FrmMain_ItemAdded;
            //((ESRI.ArcGIS.Carto.IActiveViewEvents_Event)axMapCtlMain.Map).ItemDeleted -= FrmMain_ItemDeleted;

            //停止监听
            //btnStopListen_Click(null, null);
        }
        private void FrmMain_FormClosed(object sender, FormClosedEventArgs e)//关闭窗口时终止线程
        {

            //保存界面状态和工具栏状态
            dotNetBarManagerMain.SaveLayout(GetParentPathofExe() + @"Resource\configure\Layout.cfg");
            SaveToolbarControl(axToolbarControlCustom);
            //SaveToolbarControl(axToolbarCtlMenuEditor);//暂时不用

        }

        
        //加载图层事件监听
        private void FrmMain_ItemAdded(object Item)
        {
            if (Item is IRasterLayer)
            {
                IRasterLayer rasterLayer = Item as IRasterLayer;
                if (rasterLayer == null) return;
                if (rasterLayer.Raster == null) return;
                IRaster raster = rasterLayer.Raster;
                IRaster2 raster2 = raster as IRaster2;
                IRasterDataset rasterDataset = raster2.RasterDataset;
                if (rasterDataset == null) return;
                IRasterPyramid rasterPyramid = rasterDataset as IRasterPyramid;
                //如果金字塔存在则不处理
                if (rasterPyramid.Present) return;

                //设置金字塔创建最小值
                IRasterProps rasterProps = raster2 as IRasterProps;
                if (rasterProps.Width * rasterProps.Height < 2000 * 2000) return;

                if (m_bAlwaysSame)//执行同一操作已设置
                {
                    if (m_bPyramidCreate)
                    {
                        IRasterPyramid3 rasterPyramid3 = rasterPyramid as IRasterPyramid3;
                        rasterPyramid3.Create();
                        rasterLayer.CreateFromDataset(rasterDataset);
                    }
                }
                else
                {
                    FrmRasterPyramid frm = new FrmRasterPyramid();
                    
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        m_bPyramidCreate = true;
                        IRasterPyramid3 rasterPyramid3 = rasterPyramid as IRasterPyramid3;
                        rasterPyramid3.Create();
                        rasterLayer.CreateFromDataset(rasterDataset);
                    }
                    else
                    {
                        m_bPyramidCreate = false;
                    }
                    if (frm.m_bAlwaysSame)
                    {
                        m_bAlwaysSame = true;
                    }
                }
            }
            //throw new NotImplementedException();
        }

        void FrmMain_ItemDeleted(object Item)
        {

            //throw new NotImplementedException();
        }

        void FrmMain_OnSaveEdits()
        {
            //编辑存储后ID会重新排序生成，需要刷新属性表
            IEngineEditLayers editLayers = m_EngineEditor as IEngineEditLayers;
            ILayer pLayer = ClsGDBDataCommon.GetLayerFromName(axMapCtlMain.Map, barAttributeTable.Text);
            if (pLayer !=null && barAttributeTable.Visible ==true)
            {
                m_AttributeTable.ReloadAttributeTable();
            }
            //throw new NotImplementedException();
        }
        //创建新的Feature事件
        void FrmMain_OnCreateFeature(IObject Object)
        {
            //在此刷新属性表
            IEngineEditLayers editLayers = m_EngineEditor as IEngineEditLayers;
            ILayer pLayer = ClsGDBDataCommon.GetLayerFromName(axMapCtlMain.Map, barAttributeTable.Text);
            if (pLayer == editLayers.TargetLayer)
            {
                m_AttributeTable.ReloadAttributeTable();
            }
            
            //throw new NotImplementedException();
        }

        //删除Feature事件
        void FrmMain_OnDeleteFeature(IObject Object)
        {
            //在此刷新属性表,删除对象时，实际对象还没有删除，需要存储时才删除
            //从属性表中删除对象
            IFeatureClass featureClass = Object.Class as IFeatureClass;
            ILayer pLayer = ClsGDBDataCommon.GetLayerFromName(axMapCtlMain.Map, barAttributeTable.Text);
            if (pLayer is IFeatureLayer)
            {
                IFeatureLayer featureLayer = pLayer as IFeatureLayer;
                if (featureLayer.FeatureClass == featureClass)
                {
                    DeleteRowofFID(GridTable, pLayer, Object.OID);
                }
            }
            //throw new NotImplementedException();
        }

        

        private string GetParentPathofExe()
        {
            DirectoryInfo path = new DirectoryInfo(Application.StartupPath);
            string strPath = path.ToString();
            strPath = strPath.Remove(strPath.Length - path.Name.Length);
            return strPath;
        }

        //从工具栏上动态获取命令，利用此方法可以自定义上下文菜单
        private void SetEditorMenu(IToolbarMenu toolMenu,AxToolbarControl toolBar)
        {
            toolMenu.RemoveAll();
            toolMenu.CommandPool = toolBar.CommandPool;
            for (int i = 0; i < m_menuEditor.CommandPool.Count; i++)
            {
                ICommand command = toolMenu.CommandPool.get_Command(i);
                toolMenu.AddItem(command, 0, 0, false, esriCommandStyles.esriCommandStyleIconAndText);
            }
        }


        protected override void DefWndProc(ref System.Windows.Forms.Message m)//处理收到的消息
        {
            switch (m.Msg)
            {
                //521为在线监听文件接受消息，520为数据库查询消息
                case 521:
                    StreamReader sr = new StreamReader( "D:\\filerec.txt", System.Text.Encoding.Default);
                    string strTemp = "";
                    while (sr.Peek() >= 0)
                    {
                        strTemp = sr.ReadLine();
                        string[] strArray = strTemp.Split('\0');
                        string FileFullPath = strArray[0];
                        DataRow newrow = FileRecTable.NewRow();
                        newrow[2] = FileFullPath;
                        newrow[1] = System.IO.Path.GetFileName(FileFullPath);
                        newrow[0] = dataGridRv.Rows.Count + 1;
                        System.DateTime currentTime = new System.DateTime();
                        currentTime = System.DateTime.Now;
                        newrow[3] = currentTime.ToString();
                        FileRecTable.Rows.Add(newrow);

                        //本机尚未安装ACCESS，本地数据库查询仍能执行
                        //access与excel不同，无需安装
                        OleDbSQL con = new OleDbSQL();
                        string sql = "insert into FileDB(FileName,FilePath,ReceiveTime) values('" + System.IO.Path.GetFileName(FileFullPath) + "','" + FileFullPath + "','" + currentTime.ToString() + "')";
                        con.command(sql, con.connection(con.connectionPath()));//执行插入命令
                    }
                    sr.Close();
                    break;

                case 5520:
                    try
                    {
                    //MessageBox.Show("5520");
                    StreamReader sr1 = new StreamReader("D:\\filerec.txt", System.Text.Encoding.Default);
                    string strTemp1 = "";
                    while (sr1.Peek() >= 0)
                    {
                        
                        strTemp1 = sr1.ReadLine();
                        string[] strArray = strTemp1.Split('\0');
                        string FileFullPath = strArray[0];
                        DataRow newrow = FileRecTable.NewRow();
                        newrow[2] = FileFullPath;
                        newrow[1] = System.IO.Path.GetFileName(FileFullPath);
                        newrow[0] = dataGridRv.Rows.Count + 1;
                        System.DateTime currentTime = new System.DateTime();
                        currentTime = System.DateTime.Now;
                        newrow[3] = currentTime.ToString();
                        FileRecTable.Rows.Add(newrow);

                        OleDbSQL con = new OleDbSQL();
                        string sql = "insert into FileDB(FileName,FilePath,ReceiveTime) values('" + System.IO.Path.GetFileName(FileFullPath) + "','" + FileFullPath + "','" + currentTime.ToString() + "')";
                        con.command(sql, con.connection(con.connectionPath()));//执行插入命令
                        //导出地图
                        ExportActiveView(FileFullPath);
                        //FrmExportActiveViewFig frm = new FrmExportActiveViewFig(axPageLayoutCtlMain.Object as IPageLayoutControl2, FileFullPath,m_controlsSynchronizer);
                    }
                    sr1.Close();
                    }
                    catch (System.Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                    break;
                default:

                    base.DefWndProc(ref m);//调用基类函数，一边系统处理其他消息
                    break;
            }
           
        }
        //导出地图
        private bool ExportActiveView(string strXmlFileName)
        {
            FrmExportActiveViewFig frm = new FrmExportActiveViewFig(this.axPageLayoutCtlMain.Object as IPageLayoutControl2);
            //解析XML文件
            frm.AcceptFile(strXmlFileName);

            axPageLayoutCtlMain.Update();

            //设置输出文件名
            System.DateTime curTime = new System.DateTime();
            curTime = DateTime.Now;
            string strFileName = "着陆点图" + curTime.ToShortDateString() + "-" + curTime.ToShortTimeString() + "-" + curTime.Second.ToString();
            strFileName = strFileName.Replace(@"/", @"-");
            strFileName = strFileName.Replace(@":", @"-");

            frm.ExportActiveViewParameterized(axPageLayoutCtlMain.ActiveView, 400, 1, "JPEG", @"F:\PMRS\", strFileName, false);

            frm.Dispose();

            return true;
        }

       
        public void startLis()//点击Start初始化线程
        {
            //if (m_isFirstStartRecvw)
            //{
            //    m_isFirstStartRecvw = false;
            //} 
            //else
            //{
            //    zfclose(); 
            //}
            //string fdpathIn = fdpath;
            //hwnd = User32API.GetCurrentWindowHandle();
            //int i = zfRecvw(fdpathIn,hwnd);
        }

        //自定义工具栏启动事件
        private void OnStartCustomDialog()
        {
            barCustom.Visible = true;
            axToolbarControlCustom.Customize = true;
            barContexMenuEditor.Visible = true;
            axToolbarCtlMenuEditor.Customize = true;
        }

        //自定义工具栏关闭事件
        private void OnCloseCustomDialog()
        {
            axToolbarControlCustom.Customize = false;
            //barCustom.Visible = false;
            //SetEditorMenu(m_menuEditor,axToolbarCtlMenuEditor);
            axToolbarCtlMenuEditor.Customize = false;
            barContexMenuEditor.Visible = false;
        }

        private void ElementAddedMethod(IElement pEle)
        {
            ;
        }

        private void ElementUpdatedMethod(IElement pEle)
        {
            m_NewDisplacement.OriginPoints.RemovePoints(0, m_NewDisplacement.OriginPoints.PointCount);
            m_NewDisplacement.TargetPoints.RemovePoints(0, m_NewDisplacement.TargetPoints.PointCount);
            IGraphicsContainer pGC = m_NewDisplacement.pGraphicsLayer as IGraphicsContainer;
            // pGC.DeleteElement(pEle);
            pGC.Reset();

            IElement pTmpEle = pGC.Next();
            while (pTmpEle != null)
            {
                IPolyline pLine = pTmpEle.Geometry as IPolyline;
                m_NewDisplacement.OriginPoints.AddPoint(pLine.FromPoint);
                m_NewDisplacement.TargetPoints.AddPoint(pLine.ToPoint);
                pTmpEle = pGC.Next();
            }
            //IGraphicsContainerSelect pGCS = pGC as IGraphicsContainerSelect;
            //pGCS.SelectAllElements();
            m_NewDisplacement.FrmVectorLinkTable.RefreshDataTable();
        }

        private void ElementDeletedMethod(IElement pEle)
        {
            m_NewDisplacement.OriginPoints.RemovePoints(0, m_NewDisplacement.OriginPoints.PointCount);
            m_NewDisplacement.TargetPoints.RemovePoints(0, m_NewDisplacement.TargetPoints.PointCount);
            IGraphicsContainer pGC = m_NewDisplacement.pGraphicsLayer as IGraphicsContainer;
            // pGC.DeleteElement(pEle);
            pGC.Reset();

            IElement pTmpEle = pGC.Next();
            while (pTmpEle != null)
            {
                if (pTmpEle.Equals(pEle))
                {
                    pTmpEle = pGC.Next();
                    continue;
                }
                IPolyline pLine = pTmpEle.Geometry as IPolyline;
                m_NewDisplacement.OriginPoints.AddPoint(pLine.FromPoint);
                m_NewDisplacement.TargetPoints.AddPoint(pLine.ToPoint);
                pTmpEle = pGC.Next();
            }
            IGraphicsContainerSelect pGCS = pGC as IGraphicsContainerSelect;
            //pGCS.SelectAllElements(); 
            m_NewDisplacement.FrmVectorLinkTable.RefreshDataTable();
        }

        //初始化属性表
        public void InitalizeGridSelection()
        {
            //GridPanel panel = superGridSelection.PrimaryGrid;

            // // Add 30 rows for the user to play around with

            //for (int i = 0; i < 20; i++)
            //{
            //    GridRow row = new GridRow("colLyaerName",true,true);
            //    panel.Rows.Add(row);

            //}


        }
        //从map拷贝数据到layout
        private void CopyAndOverwriteMap()
        {
            //Get IObjectCopy interface
            IObjectCopy objectCopy = new ObjectCopyClass();

            //Get IUnknown interface (map to copy)
            object toCopyMap = axMapCtlMain.Map;

            //Each Map contained within the PageLayout encapsulated by the 
            //PageLayoutControl, resides within a separate MapFrame, and therefore 
            //have their IMap::IsFramed property set to True. A Map contained within the 
            //MapControl does not reside within a MapFrame. As such before 
            //overwriting the MapControl's map, the IMap::IsFramed property must be set 
            //to False. Failure to do this will lead to corrupted map documents saved 
            //containing the contents of the MapControl.
            IMap map = toCopyMap as IMap;
            map.IsFramed = false;

            //Get IUnknown interface (copied map)
            object copiedMap = objectCopy.Copy(toCopyMap);

            //Get IUnknown interface (map to overwrite)
            object toOverwriteMap = axPageLayoutCtlMain.ActiveView.FocusMap;

            //Overwrite the MapControl's map
            objectCopy.Overwrite(copiedMap, ref toOverwriteMap);

            axPageLayoutCtlMain.DocumentFilename = axMapCtlMain.DocumentFilename;
            IActiveView pActiveView = (IActiveView)axPageLayoutCtlMain.ActiveView.FocusMap;
            pActiveView = (IActiveView)axPageLayoutCtlMain.ActiveView.FocusMap;
            IDisplayTransformation pDisplayTransformation = pActiveView.ScreenDisplay.DisplayTransformation;
            pDisplayTransformation.VisibleBounds = axMapCtlMain.ActiveView.Extent;
            pActiveView.Refresh();
        }
        //从layout拷贝数据到map
        private void CopyAndOverwriteMap_2()
        {
            //Get IObjectCopy interface
            IObjectCopy objectCopy = new ObjectCopyClass();

            //Get IUnknown interface (map to copy)
            // object toCopyMap = axMapCtlMain.Map;
            object toCopyMap = axPageLayoutCtlMain.ActiveView.FocusMap;


            //Each Map contained within the PageLayout encapsulated by the 
            //PageLayoutControl, resides within a separate MapFrame, and therefore 
            //have their IMap::IsFramed property set to True. A Map contained within the 
            //MapControl does not reside within a MapFrame. As such before 
            //overwriting the MapControl's map, the IMap::IsFramed property must be set 
            //to False. Failure to do this will lead to corrupted map documents saved 
            //containing the contents of the MapControl.
            IMap map = toCopyMap as IMap;
            map.IsFramed = false;

            //Get IUnknown interface (copied map)
            object copiedMap = objectCopy.Copy(toCopyMap);

            //Get IUnknown interface (map to overwrite)
            //object toOverwriteMap = axPageLayoutCtlMain.ActiveView.FocusMap;
            object toOverwriteMap = axMapCtlMain.ActiveView.FocusMap;

            //Overwrite the MapControl's map
            objectCopy.Overwrite(copiedMap, ref toOverwriteMap);

            //axPageLayoutCtlMain.DocumentFilename = axMapCtlMain.DocumentFilename;
            axMapCtlMain.DocumentFilename = axPageLayoutCtlMain.DocumentFilename;

            IActiveView pActiveView = (IActiveView)axPageLayoutCtlMain.ActiveView.FocusMap;

            axMapCtlMain.Extent = pActiveView.Extent;

            //IDisplayTransformation pDisplayTransformation = pActiveView.ScreenDisplay.DisplayTransformation;
            //pDisplayTransformation.VisibleBounds = axPageLayoutCtlMain.ActiveView.Extent;
            axMapCtlMain.Refresh();
        }
        
        private void bar3_DockTabChange(object sender, DevComponents.DotNetBar.DockTabChangeEventArgs e)
        {
            //表示需要从map拷贝数据到layout
            if (e.NewTab.Name =="dockLayout")
            {
                try
                {
                    axToolbarControlEdit.Enabled = false;
                    //地图窗口正在编辑时切换到地图窗口，需要FinishSketch
                    //EngineEditor engineEditor = new EngineEditorClass();
                    if (m_EngineEditor.EditState == esriEngineEditState.esriEngineStateEditing)
                    {
                        m_EngineEditSketch = (IEngineEditSketch)m_EngineEditor;
                        if (m_EngineEditSketch.LastPoint != null)//
                        {
                            m_EngineEditSketch.FinishSketch();
                        }
                    }
                }
                catch (System.Exception ex)
                {
                    ;
                }
                m_controlsSynchronizer.ActivatePageLayout();
                //设置缺省的命令
                if (m_controlsSynchronizer.m_pageLayoutActiveTool == null)
                {
                    SetCurrentTool(axToolbarControlLayout, "esriControls.ControlsSelectTool");
                    m_controlsSynchronizer.m_pageLayoutActiveTool = axToolbarControlLayout.CurrentTool;
                }
                else
                {
                    axToolbarControlLayout.CurrentTool = m_controlsSynchronizer.m_pageLayoutActiveTool;
                }

                //更新命令状态
                btnAddText.Enabled = true;
                btnAddNorthArrow.Enabled = true;
                btnAddScallBar.Enabled = true;
                btnAddScallText.Enabled = true;
                btnAddGrid.Enabled = true;
                btnAddLegend.Enabled = true;
                btnExportMap.Enabled = true;
                btnPrintMap.Enabled = true;

                //cmbSunAltOriImg.Enabled = false;
                //cmbTargetRasterLayer.Enabled = false;
                //cmBoxEffectsLayer.Enabled = false;
                //cmbAdjustMethod.Enabled = false;
                //cmbTargetTinLayer.Enabled = false;
                comboBoxExCrater.Enabled = false;
                comboBoxExNonCrater.Enabled = false;
            }
            //表示需要从layout拷贝数据到map
            if (e.NewTab.Name == "dockMap")
            {
                //axMapCtlMain.Map = axPageLayoutCtlMain.ActiveView.FocusMap;
                //m_controlsSynchronizer.ActivatePageLayout();
                axToolbarControlEdit.Enabled = true;
                m_controlsSynchronizer.ActivateMap();
                if (m_controlsSynchronizer.m_mapActiveTool == null)
                {
                    SetCurrentTool(axToolbarControlCommon, "esriControls.ControlsMapPanTool");
                    m_controlsSynchronizer.m_mapActiveTool = axToolbarControlCommon.CurrentTool;
                }
                else
                {
                    axToolbarControlCommon.CurrentTool = m_controlsSynchronizer.m_mapActiveTool;
                }

                //更新命令状态
                btnAddText.Enabled = false;
                btnAddNorthArrow.Enabled = false;
                btnAddScallBar.Enabled = false;
                btnAddScallText.Enabled = false;
                btnAddGrid.Enabled = false;
                btnAddLegend.Enabled = false;
                btnExportMap.Enabled = false;
                btnPrintMap.Enabled = false;

                //cmbSunAltOriImg.Enabled = true;
                //cmbTargetRasterLayer.Enabled = true;
                //cmBoxEffectsLayer.Enabled = true;
                //cmbAdjustMethod.Enabled = true;
                //cmbTargetTinLayer.Enabled = true;
                comboBoxExCrater.Enabled = true;
                comboBoxExNonCrater.Enabled = true;
            }
            if (e.NewTab.Name == "dockScene")
            {
                popupBar3D.Checked = true;
                m_controlsSynchronizer.ActiveSceneView();
                //axTOCCtlLayer.SetBuddyControl(axSceneCtlMain.Object);
                //记录地图和布局工具栏状态
                if (axMapCtlMain.CurrentTool != null)
                {
                    m_controlsSynchronizer.m_mapActiveTool = axMapCtlMain.CurrentTool;
                }
                if (axPageLayoutCtlMain.CurrentTool != null)
                {
                    m_controlsSynchronizer.m_pageLayoutActiveTool = axPageLayoutCtlMain.CurrentTool;
                }

                if (m_controlsSynchronizer.m_scenceActiveTool == null)
                {
                    SetCurrentTool(axToolbarControlScene, "esriControls.ControlsSceneNavigateTool");
                    m_controlsSynchronizer.m_scenceActiveTool = axToolbarControlScene.CurrentTool;
                }
                else
                {
                    axSceneCtlMain.CurrentTool = m_controlsSynchronizer.m_scenceActiveTool;
                }

                //更新命令状态
                btnAddText.Enabled = true;
                btnAddNorthArrow.Enabled = true;
                btnAddScallBar.Enabled = true;
                btnAddScallText.Enabled = true;
                btnAddGrid.Enabled = true;
                btnAddLegend.Enabled = true;
                btnExportMap.Enabled = true;
                btnPrintMap.Enabled = true;

                //cmbSunAltOriImg.Enabled = false;
                //cmbTargetRasterLayer.Enabled = false;
                //cmBoxEffectsLayer.Enabled = false;
                //cmbAdjustMethod.Enabled = false;
                //cmbTargetTinLayer.Enabled = false;
                comboBoxExCrater.Enabled = false;
                comboBoxExNonCrater.Enabled = false;
            }
        }

        //设置工具栏缺省的选中命令
        public void SetCurrentTool(AxToolbarControl toolBar, string strValue)
        {
            try
            {
                ESRI.ArcGIS.esriSystem.UIDClass uID = new ESRI.ArcGIS.esriSystem.UIDClass();
                uID.Value = strValue;
                if (toolBar.Buddy !=null)
                {
                    toolBar.CurrentTool = (ITool)toolBar.CommandPool.FindByUID(uID);
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void axTOCCtlLayer_OnMouseUp(object sender, ITOCControlEvents_OnMouseUpEvent e)
        {
            esriTOCControlItem item = esriTOCControlItem.esriTOCControlItemNone;
            IBasicMap map = null; ILayer layer = null;
            object other = null; object index = null;

            //Determine what kind of item is selected
            axTOCCtlLayer.HitTest(e.x, e.y, ref item, ref map, ref layer, ref other, ref index); 
            //记录点击选中图层所在的map
            //axMapCtlMain.Tag = map;
            //axPageLayoutCtlMain.Tag = map;

            //Ensure the item gets selected 
            if (item == esriTOCControlItem.esriTOCControlItemMap)
            {
                //axTOCCtlLayer.SelectItem(map, null);
                //ArrayList ac = new ArrayList();
                //将地图和map控件传递给layout控件，在激活地图时用到
                //ac.Add(axMapCtlMain.Object);
                //ac.Add(map);
                axMapCtlMain.CustomProperty = map;
                axPageLayoutCtlMain.CustomProperty =map;
            }

            if (item == esriTOCControlItem.esriTOCControlItemLayer)
            {
                //axTOCCtlLayer.SelectItem(layer, null);
                //Set the layer into the CustomProperty (this is used by the custom layer commands)			
                axMapCtlMain.CustomProperty = layer;
                axPageLayoutCtlMain.CustomProperty = layer;
                if (dockScene.Selected == true)
                {
                    axSceneCtlMain.CustomProperty = layer;
                    m_CmdSceneLayerOffset.pSceneCtr = axSceneCtlMain.Object as ISceneControl;
                    m_CmdSceneLayerOffset.pLayer = layer;
                }
                else
                {
                    m_CmdSceneLayerOffset.pSceneCtr = null;
                    m_CmdSceneLayerOffset.pLayer = null;
                }
            }
            if (item == esriTOCControlItem.esriTOCControlItemLegendClass)
            {
                //ILegendGroup pLegGroup = (ILegendGroup)other;
                //axTOCCtlLayer.SelectItem(pLegGroup);
                //if (layer is IFeatureLayer)
                //{
                //    ILegendClass pLegendClass = ((ILegendGroup)other).get_Class((int)index);
                //    FrmSymbolEdit frmsymboledit = new FrmSymbolEdit(pLegendClass, layer, this.axMapCtlMain, this.axTOCCtlLayer);
                //    frmsymboledit.StartPosition = FormStartPosition.CenterScreen;
                //    frmsymboledit.ShowDialog();
                //}
            }

            //右键
            if (e.button == 2)
            {
                //Popup the correct context menu
                //只有TOC绑定地图和布局时才弹出菜单
                if (axTOCCtlLayer.Buddy.Equals(axMapCtlMain.Object) || axTOCCtlLayer.Buddy.Equals(axPageLayoutCtlMain.Object))
                {
                    if (item == esriTOCControlItem.esriTOCControlItemMap)
                    {

                        m_menuFrame.PopupMenu(e.x, e.y, axTOCCtlLayer.hWnd);
                    }

                    if (item == esriTOCControlItem.esriTOCControlItemLayer)
                    {
                        m_menuLayer.PopupMenu(e.x, e.y, axTOCCtlLayer.hWnd);
                        if (this.AttributeTable !=null)
                        {
                            this.AttributeTable = m_AttributeTable.m_AttributeTable;
                        }

                        if (AttributeTable != null)
                        {
                            this.AttributeTable = m_AttributeTable.m_AttributeTable;
                        }
                    }
                }
            }
        }

        #region 属性表初始化
        private void InitialAttributeTable()
        {
            //初始化属性表界面按钮状态
            btnshowselout.Checked = true;
            btnshowselout.Checked = false;
            //初始化属性表
            if (m_AttributeTable != null)
            {
                m_AttributeTable.m_btnselras = this.btnselectras;
                m_AttributeTable.m_btnselallras = this.btnselectallras;
                m_AttributeTable.m_btnswitchras = this.btnswitchras;
                m_AttributeTable.m_btnselclear = this.btnselclear;

                m_AttributeTable.m_btnaddfield = this.btnAddField;
                m_AttributeTable.m_showall = this.btnshowall;
                m_AttributeTable.m_showsel = this.btnshowsel;

                m_AttributeTable.m_btnhide = this.buttonItemhide;
                m_AttributeTable.m_toolbarfield = this.axToolbarfield;
                m_AttributeTable.m_gridfield = this.GridTable;
                m_AttributeTable.m_datasettable = this.dataSettable;

                m_AttributeTable.m_btndelectfield = this.btndelectfield;
                m_AttributeTable.m_docktable = this.dockTable;
                
                m_AttributeTable.m_btnshowallout = this.btnshowallout;
                m_AttributeTable.m_btnshowselout = this.btnshowselout;
                m_AttributeTable.m_btntable = this.btnTable;
                m_AttributeTable.m_btnNOsel = this.btnNOsel;
            }

            //m_labeldesign.m_Layer = layer;
            m_labeldesign.m_Mapcontrol = this.axMapCtlMain;
            m_labeldesign.m_Toccontrol = this.axTOCCtlLayer;
            m_labeldesign.m_scenecontrol = this.axSceneCtlMain;

            m_joinattribute.pMapControl = this.axMapCtlMain;
            m_joinattribute.m_barTable = this.barAttributeTable;
            m_joinattribute.m_gridfield = this.GridTable;
            m_joinattribute.m_AttributeTable = this.AttributeTable;
            m_joinattribute.m_docktable = this.dockTable;
 
        }
        #endregion     

        private void axMapCtlMain_OnAfterDraw(object sender, IMapControlEvents2_OnAfterDrawEvent e)
        {
            SetMainApplictationTitle();
            

            //try
            //{
            //    IActiveView iv = axSceneCtlMain.Scene as IActiveView;
            //    iv.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
            //}
            //catch
            //{
            //}
        }

        private void DrawSunAltPoints()
        {
            if (axMapCtlMain.CurrentTool is ToolSunAltCheckState)
            {
                int linelength = 4;
                for (int i = 0; i < m_SunAltPointCollection.Count; i++)
                {
                    ClsPointInfo ptInfo = m_SunAltPointCollection[i];

                    IMapControl2 mapctrl = axMapCtlMain.Object as IMapControl2;
                    IRaster2 pRaster = m_toolSunAltCheckState.m_pLayer.Raster as IRaster2;

                    double pX = -1;
                    double pY = -1;
                    pRaster.PixelToMap(ptInfo.nImageX, ptInfo.nImageY, out pX, out pY);

                    ESRI.ArcGIS.Geometry.Point opt = new ESRI.ArcGIS.Geometry.Point();
                    opt.X = pX;
                    opt.Y = pY;
                    //mapctrl.FromMapPoint(opt,ref )      
                    int ox = -1;// (int)opt.X;
                    int oy = -1;// (int)opt.Y; 
                    mapctrl.FromMapPoint(opt, ref ox, ref oy);


                    gcs.DrawLine(penred, ox, oy - linelength, ox, oy + linelength);
                    gcs.DrawLine(penred, ox + linelength, oy, ox - linelength, oy);
                }
            }
        }

        private void DrawVectorControlPoint()
        {
            try
            {
                if (m_NewDisplacement == null || m_NewDisplacement.OriginPoints == null || m_NewDisplacement.OriginPoints == null || m_NewDisplacement.OriginPoints.PointCount != m_NewDisplacement.TargetPoints.PointCount)
                {
                    return;
                }

                //pengreen.Width = 2;
                int linelength = 4;
                for (int i = 0; i < m_NewDisplacement.OriginPoints.PointCount; i++)
                {
                    //IPoint opt = m_ToolAddControlPoints.OriginPoints.get_Point(i);
                    IPoint opt = m_NewDisplacement.OriginPoints.get_Point(i);
                    IPoint tpt = m_NewDisplacement.TargetPoints.get_Point(i);
                    IMapControl2 mapctrl = axMapCtlMain.Object as IMapControl2;

                    int ox = 0; int oy = 0; int tx = 0; int ty = 0;
                    mapctrl.FromMapPoint(opt, ref ox, ref oy);
                    mapctrl.FromMapPoint(tpt, ref tx, ref ty);
                    gcs.DrawLine(penblue, ox, oy, tx, ty);
                    gcs.DrawLine(penred, ox - linelength, oy - linelength, ox + linelength, oy + linelength);
                    gcs.DrawLine(penred, ox + linelength, oy - linelength, ox - linelength, oy + linelength);
                    gcs.DrawEllipse(penred, ox - linelength, oy - linelength, 2 * linelength, 2 * linelength);
                    gcs.DrawLine(pengreen, tx - linelength, ty - linelength, tx + linelength, ty + linelength);
                    gcs.DrawLine(pengreen, tx + linelength, ty - linelength, tx - linelength, ty + linelength);
                    gcs.DrawEllipse(pengreen, tx - linelength, ty - linelength, 2 * linelength, 2 * linelength);

                    if (i == m_NewDisplacement.FrmVectorLinkTable.SeletedIndex)
                    {
                        gcs.DrawLine(penyelow, ox, oy, tx, ty);
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

//#region 测试绘制
//        private void DrawControlPoint()
//        {
//            try
//            {
//                if (m_ToolAddControlPoints == null) return;

//                int linelength = 8;
//                IGraphicsContainer pGraphiccsContainer = axMapCtlMain.Map as IGraphicsContainer;
//                if (pGraphiccsContainer == null)
//                    return;

//                for (int i = 0; i < m_ToolAddControlPoints.TargetPoints.PointCount; i++)
//                {
//                    IPoint opt = m_ToolAddControlPoints.m_FrmLinkTableRaster.TransformedOriginPoints.get_Point(i);
//                    IPoint tpt = m_ToolAddControlPoints.m_FrmLinkTableRaster.TargetPoints.get_Point(i);
//                    IMapControl3 mapctrl = axMapCtlMain.Object as IMapControl3;
                    
//                    //判断点是否在视图范围内，如果在即绘制，不在则不绘制
//                    IEnvelope pEnvelope = mapctrl.ActiveView.Extent;

//                    int ox = 0; int oy = 0; int tx = 0; int ty = 0;
//                    mapctrl.FromMapPoint(opt, ref ox, ref oy);
//                    mapctrl.FromMapPoint(tpt, ref tx, ref ty);

//#region 连接线
//                    ILineElement pConnectLineElement = new LineElementClass();
//                    IElement pConnectElement = pConnectLineElement as IElement;

//                    //设置GEOMETRY
//                    IPolyline pLine = new PolylineClass();
//                    IPoint ptFromPoint = new PointClass();
//                    ptFromPoint.X = ox;
//                    ptFromPoint.Y = oy;
//                    pLine.FromPoint = ptFromPoint;

//                    IPoint ptToPoint = new PointClass();
//                    ptToPoint.X = ox;
//                    ptToPoint.Y = oy;
//                    pLine.ToPoint = ptToPoint;
//                    pConnectElement.Geometry = pLine;

//                    //设置颜色
//                    if (i == m_ToolAddControlPoints.m_FrmLinkTableRaster.selectedidx)
//                    {
//                        pGraphiccsContainer.AddElement(pConnectElement, 0);
//                    }
//#endregion

//#region 源点十字丝
//                    //            gcs.DrawLine(penred, ox - linelength, oy, ox + linelength, oy);
//                    //            gcs.DrawLine(penred, ox, oy - linelength, ox, oy + linelength);
//                    //            gcs.DrawLine(pengreen, tx - linelength, ty, tx + linelength, ty);
//                    //            gcs.DrawLine(pengreen, tx, ty - linelength, tx, ty + linelength);
//                    //            gcs.DrawString(i.ToString(), strFont, strBrush, ox+4, oy+4);
//                    //ILineElement pSrcLineElement = new LineElementClass();
//                    //IElement pSrcElement = pSrcLineElement as IElement;

//                    //ILine pSrcLine = new LineClass();
//                    //IPoint pSrcFromPoint = new PointClass();
//                    //pSrcFromPoint.X=ox - linelength;
//                    //pSrcFromPoint.Y = oy;

//                    //IPoint pSrcFromPoint = new PointClass();
//                    //pSrcFromPoint.X = ox - linelength;
//                    //pSrcFromPoint.Y = oy;
//#endregion

//#region 目标点十字丝
//#endregion                    

//                }

//                axMapCtlMain.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);
//            }
//            catch (System.Exception ex)
//            {

//            }
//        }
//#endregion

        //绘制栅格控制点
        private void DrawRasterRegisterControlPoint()
        {
            try
            {
                if (m_ToolAddControlPoints == null) return;
  
                int linelength = 8;
                for (int i = 0; i < m_ToolAddControlPoints.TargetPoints.PointCount; i++)
                {
                    IPoint opt = m_ToolAddControlPoints.m_FrmLinkTableRaster.TransformedOriginPoints.get_Point(i);
                    IPoint tpt = m_ToolAddControlPoints.m_FrmLinkTableRaster.TargetPoints.get_Point(i);
                    IMapControl3 mapctrl = axMapCtlMain.Object as IMapControl3;
                    //判断点是否在视图范围内，如果在即绘制，不在则不绘制
                    //IEnvelope pEnvelope = mapctrl.ActiveView.Extent;
                    
                    int ox = 0; int oy = 0; int tx = 0; int ty = 0;
                    mapctrl.FromMapPoint(opt, ref ox, ref oy);
                    mapctrl.FromMapPoint(tpt, ref tx, ref ty);
                    gcs.DrawLine(penblue, ox, oy, tx, ty);
                    gcs.DrawLine(penred, ox - linelength, oy, ox + linelength, oy);
                    gcs.DrawLine(penred, ox, oy - linelength, ox, oy + linelength);
                    gcs.DrawLine(pengreen, tx - linelength, ty, tx + linelength, ty);
                    gcs.DrawLine(pengreen, tx, ty - linelength, tx, ty + linelength);
                    gcs.DrawString(i.ToString(), strFont, strBrush, ox+4, oy+4);
                    if (i == m_ToolAddControlPoints.m_FrmLinkTableRaster.selectedidx)
                    {
                        gcs.DrawLine(penyelow, ox, oy, tx, ty);
                    }

                }
            }
            catch (System.Exception ex)
            {

            }
        }
        private void axPageLayoutCtlMain_OnAfterDraw(object sender, IPageLayoutControlEvents_OnAfterDrawEvent e)
        {
            //axTOCCtlLayer.Update();//会引起点击不中
            UpdateElements();//刷新地图要素
            RulerRefresh();//刷新标尺

            //设置自定义页面？？？
            IPage page = new PageClass();
            double dHeight = 0;
            double dWidth = 0;

            page = axPageLayoutCtlMain.PageLayout.Page;
            page.QuerySize(out dWidth, out dHeight);

            if ((dWidth == 8.5) & (dHeight == 11))
            {
                page.PutCustomSize(50, 50);
            }

        }

        private void UpdateElements()
        {
            //更新地图要素，包括比例尺、格网等
            IGraphicsContainer pGC = axPageLayoutCtlMain.ActiveView.GraphicsContainer;
            pGC.Reset();
            IElement pTmpEle = pGC.Next();
            while (pTmpEle != null)
            {
                pGC.UpdateElement(pTmpEle);
                pTmpEle = pGC.Next();
            }
        }

        //布局标尺刷新
        private void RulerRefresh()
        {
            try
            {
                IActiveView activeView = axPageLayoutCtlMain.ActiveView;
                IPageLayout pageLayout = (IPageLayout)activeView;
                //IEnvelope envelope = activeView.Extent;
                IScreenDisplay screen = activeView.ScreenDisplay;
                IDisplayTransformation disTrans = screen.DisplayTransformation;

               
                IPage page = axPageLayoutCtlMain.Page;
                
                int x1, y1, x2, y2;
                ESRI.ArcGIS.Geometry.IPoint pointPage = new PointClass();
                pointPage.PutCoords(0.0, 0.0);//页面左下角点
                disTrans.FromMapPoint(pointPage, out x1, out y1);//获取页面左下角的屏幕区坐标

                double dPageWidth=210;
                double dPageHeight=297;//页面的宽度和高度
                if (page.FormID == esriPageFormID.esriPageFormSameAsPrinter)
                {
                    ESRI.ArcGIS.Output.IPaper paper = null;
                    if (axPageLayoutCtlMain.Printer == null)
                    {
                        page.QuerySize(out dPageWidth, out dPageHeight);
                    }
                    else
                    {
                        paper = axPageLayoutCtlMain.Printer.Paper;
                        
                        paper.QueryPaperSize(out dPageWidth, out dPageHeight);
                        if (paper.Units == esriUnits.esriInches)
                        {
                            dPageWidth = dPageWidth * 2.54;
                            dPageHeight = dPageHeight * 2.54;
                        }
                    }
                }
                else
                {
                    page.QuerySize(out dPageWidth, out dPageHeight);
                }

                if (dPageWidth <= 0.0 || dPageHeight <= 0.0) return;
                
                if (axPageLayoutCtlMain.Page.Units == esriUnits.esriInches)
                {
                    dPageWidth = dPageWidth * 2.54;
                    dPageHeight = dPageHeight * 2.54;
                }
                
                pointPage.PutCoords(dPageWidth, dPageHeight);//此处得到的单位mm
                disTrans.FromMapPoint(pointPage, out x2, out y2);//获取页面右上角的屏幕区坐标

                rulerCtlHorizontal.Width = x2 - x1;//水平标尺的宽度（屏幕坐标）
                //计算屏幕坐标和实际坐标的比例系数，设置标尺的主刻度
                rulerCtlHorizontal.MajorInterval = (int)(((double)rulerCtlHorizontal.Width) / dPageWidth + 0.5) * 10;
                rulerCtlHorizontal.Divisions = 10;//设置标尺的主刻度间分辨率

                rulerCtlVertical.Height = y1 - y2;//竖起标尺的高度（屏幕坐标）
                //计算屏幕坐标和实际坐标的比例系数，设置标尺的主刻度
                rulerCtlVertical.MajorInterval = (int)(((double)rulerCtlVertical.Height) / dPageHeight + 0.5) * 10;
                rulerCtlVertical.Divisions = 10;//设置标尺的主刻度间分辨率

                //设置水平标尺和竖起标尺的位置，同页面位置一致
                rulerCtlHorizontal.Left = x1 + panelRulerVertical.Width;
                rulerCtlVertical.Top = y1 - rulerCtlVertical.Height;//屏幕坐标原点位于客户区左上角
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            GC.Collect();
        }


        //图层可选 弹出菜单项
        private void buttonItemSelect_Click(object sender, EventArgs e)
        {
            //for (int i = 0; i < superGridSelection.PrimaryGrid.SelectedRowCount; i++)
            //{
            //    GridRow row =  (GridRow)superGridSelection.GetSelectedRows()[i];
            //    if (row.Cells[1].ReadOnly == false)
            //    {
            //        row.Cells[1].Value = true;
            //    }                 
            //}
            //for (int i = 0; i < axMapCtlMain.LayerCount; i++)
            //{
            //    IFeatureLayer pLayer = axMapCtlMain.get_Layer(i) as IFeatureLayer;
            //    if (pLayer != null)
            //    {
            //        GridRow row = superGridSelection.PrimaryGrid.Rows[i] as GridRow;
            //        if (row != null)
            //        {
            //            pLayer.Selectable = (bool)row.Cells[1].Value  ;
            //        }
            //    }
            //}
        }
        //图层不可选 弹出菜单项
        private void buttonItemClearSelection_Click(object sender, EventArgs e)
        {
            //for (int i = 0; i < superGridSelection.PrimaryGrid.SelectedRowCount; i++)
            //{
            //    GridRow row = (GridRow)superGridSelection.GetSelectedRows()[i];
            //    row.Cells[1].Value = false;
            //}
            //for (int i = 0; i < axMapCtlMain.LayerCount; i++)
            //{
            //    IFeatureLayer pLayer = axMapCtlMain.get_Layer(i) as IFeatureLayer;
            //    if (pLayer != null)
            //    {
            //        GridRow row = superGridSelection.PrimaryGrid.Rows[i] as GridRow;
            //        if (row != null)
            //        {
            //            pLayer.Selectable = (bool)row.Cells[1].Value;
            //        }
            //    }
            //}
        }
        //三维不可见 弹出菜单项
        private void buttonItemVisible_Click(object sender, EventArgs e)
        {
            //for (int i = 0; i < superGridSelection.PrimaryGrid.SelectedRowCount; i++)
            //{
            //    GridRow row = (GridRow)superGridSelection.GetSelectedRows()[i];
            //    row.Cells[2].Value = true;
            //}
        }
        //三维不可见 弹出菜单项
        private void buttonItemUnVisible_Click(object sender, EventArgs e)
        {
            //    for (int i = 0; i < superGridSelection.PrimaryGrid.SelectedRowCount; i++)
            //    {
            //        GridRow row = (GridRow)superGridSelection.GetSelectedRows()[i];
            //        row.Cells[2].Value = false;
            //    }
        }

        //自定义工具栏菜单
        private void buttonItemCustomSet_Click(object sender, EventArgs e)
        {
            //FrmFeatureMerge frm = new FrmFeatureMerge();
            //frm.ShowDialog();
                m_CustomizeDialog.StartDialog(axMapCtlMain.hWnd);     
        }

        private void buttonItemAddTin_Click(object sender, EventArgs e)
        {
            //OpenFileDialog dlg = new OpenFileDialog();
            //dlg.Filter = "tin files|*.";
            //dlg.Multiselect = false;
            //if (dlg.ShowDialog() == DialogResult.OK)
            //{
            //    string FilePath = dlg.FileName;
            //} 
            try
            {
                FolderBrowserDialog fdlg = new FolderBrowserDialog();
                fdlg.SelectedPath = @"C:\Users\Administrator\Desktop\sampleAnalysis\TIN\";
                if (fdlg.ShowDialog() != DialogResult.OK)
                {
                    return;
                }
                //FrmFolderDialog dlg = new FrmFolderDialog();
                //dlg.DisplayDialog();
                if (fdlg.SelectedPath != "")
                {
                    // DirectoryInfo dir = Directory.CreateDirectory(dlg.Path);
                    DirectoryInfo dir = Directory.CreateDirectory(fdlg.SelectedPath);
                    IWorkspaceFactory pWSFact = new TinWorkspaceFactoryClass();
                    IWorkspace pWS = pWSFact.OpenFromFile(dir.Parent.FullName + @"\", 0);
                    ITinWorkspace pTinWS = pWS as ITinWorkspace;
                    ITin pTin = pTinWS.OpenTin(dir.Name);
                    //将TIN变为TIN图层
                    ITinLayer pTinLayer = new TinLayerClass();
                    pTinLayer.Dataset = pTin;
                    pTinLayer.Name = dir.Name;
                    axMapCtlMain.Map.AddLayer((ILayer)pTinLayer);
                }
            }
            catch (SystemException ee)
            {
                MessageBox.Show(ee.Message);
            }
        }

        private void buttonItemDEMToTIN_Click(object sender, EventArgs e)
        {
            FrmDEMToTin f = new FrmDEMToTin();
            f.m_pMap = axMapCtlMain.Map;
            try
            {
                if (f.ShowDialog() == DialogResult.OK)
                {
                    if (f.m_pTin != null)
                    {
                        ITinLayer player = new TinLayerClass();
                        IWorkspaceFactory pWSFact = new TinWorkspaceFactoryClass();
                        DirectoryInfo dinfo = new DirectoryInfo(f.m_TINPath);
                        IWorkspace pWS = pWSFact.OpenFromFile(dinfo.Parent.FullName, 0);
                        ITinWorkspace pTinWS = pWS as ITinWorkspace;
                        ITin pTin = pTinWS.OpenTin(dinfo.Name);
                        //将TIN变为TIN图层
                        player.Dataset = pTin;
                        player.Name = dinfo.Name;
                        axMapCtlMain.Map.AddLayer(player);
                        axMapCtlMain.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
                    }
                }
            }
            catch (SystemException ee)
            {
                MessageBox.Show(ee.Message);
            }
        }

        private void buttonItemTINToDEM_Click(object sender, EventArgs e)
        {
            FrmTinToDEM f = new FrmTinToDEM();
            f.m_pMap = axMapCtlMain.Map;
            if (f.ShowDialog() == DialogResult.OK)
            {
                if (f.m_DEMPath != "")
                {
                    IRasterLayer pRasterLayer = new RasterLayerClass();
                    pRasterLayer.CreateFromFilePath(f.m_DEMPath);
                    axMapCtlMain.AddLayer(pRasterLayer as ILayer);
                    axMapCtlMain.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
                }
            }
        }

        private void cmbTargetTinLayer_SelectedIndexChanged(object sender, EventArgs e)
        {
            string LayerName = cmbTargetTinLayer.SelectedItem.ToString();
            m_TargetTinLayer = null;
            m_EditRasterLayer = null;

            IEnumLayer pEnumLayer = axMapCtlMain.Map.get_Layers(null, true);
            pEnumLayer.Reset();
            ILayer player = pEnumLayer.Next();
            while (player != null)
            {
                if (player.Name == LayerName)
                {
                    if (dockMap.Selected == false)
                    {
                        dockMap.Selected = true;
                        axMapCtlMain.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
                    }
                    if (player is ITinLayer)
                    {
                        m_TargetTinLayer = player as ITinLayer;
                        m_cmdStartEditTinLayer.pTinLayer = m_TargetTinLayer;
                        m_cmdStopEditTinLayer.pTinLayer = m_TargetTinLayer;
                        m_cmdSaveEditTinLayer.pTinLayer = m_TargetTinLayer;
                        m_toolAddTinNode.pTinLayer = m_TargetTinLayer;
                        m_toolDeleteTINNode.pTinLayer = m_TargetTinLayer;
                        m_toolModifyTinNode.pTinLayer = m_TargetTinLayer;
                        m_toolAddTinPlane.pTinLayer = m_TargetTinLayer;
                        m_ToolDeleteNodeByArea.pTinLayer = m_TargetTinLayer;
                        m_ToolAddCraterOnTIN.pTinLayer = m_TargetTinLayer;
                        m_ToolAddCraterOnTIN.pRasterLayer = null;
                        m_ToolAddStone.pTinLayer = m_TargetTinLayer;
                        m_ToolAddStone.pRasterLayer = null;
                        m_ToolEditFeatures.pTinLayer = m_TargetTinLayer;
                        m_cmdTinSaveAs.pTinLayer = m_TargetTinLayer;
                    }
                    else if (player is IRasterLayer)
                    {
                        m_EditRasterLayer = player as IRasterLayer;
                        m_ToolAddCraterOnTIN.pTinLayer = null;
                        m_ToolAddCraterOnTIN.pRasterLayer = m_EditRasterLayer;
                        m_ToolAddStone.pTinLayer = null;
                        m_ToolAddStone.pRasterLayer = m_EditRasterLayer;
                        m_ToolExtractCraterSample.pRasterLayer = m_EditRasterLayer;
                    }

                    break;
                }
                player = pEnumLayer.Next();
            }


        }

        private void comboBoxExCrater_SelectedIndexChanged(object sender, EventArgs e)
        {
            string LayerName = comboBoxExCrater.SelectedItem.ToString();
            m_CraterLayer = null;

            IEnumLayer pEnumLayer = axMapCtlMain.Map.get_Layers(null, true);
            pEnumLayer.Reset();
            ILayer player = pEnumLayer.Next();
            while (player != null)
            {
                if (player.Name == LayerName)
                {
                    m_CraterLayer = player as IFeatureLayer;
                    break;
                }
                player = pEnumLayer.Next();
            }
            m_ToolAddCraterOnTIN.pFeatureLayer = m_CraterLayer;
        }

        private void comboBoxExNonCrater_SelectedIndexChanged(object sender, EventArgs e)
        {
            string LayerName = comboBoxExNonCrater.SelectedItem.ToString();
            m_NonCraterLayer = null;

            IEnumLayer pEnumLayer = axMapCtlMain.Map.get_Layers(null, true);
            pEnumLayer.Reset();
            ILayer player = pEnumLayer.Next();
            while (player != null)
            {
                if (player.Name == LayerName)
                {
                    m_NonCraterLayer = player as IFeatureLayer;
                    break;
                }
                player = pEnumLayer.Next();
            }
            m_ToolAddStone.pFeatureLayer = m_NonCraterLayer;
        }

        private void cmbTargetRasterLayer_SelectedIndexChanged(object sender, EventArgs e)
        {
            string LayerName = cmbTargetRasterLayer.Text;
            m_TargetRasterLayer = null;
            IEnumLayer pEnumLayer = axMapCtlMain.Map.get_Layers(null, true);
            pEnumLayer.Reset();
            ILayer player = pEnumLayer.Next();
            while (player != null)
            {
                if (player.Name == LayerName)
                {
                    m_TargetRasterLayer = player as IRasterLayer;
                    //当选择栅格图层准备处理时，界面调整到地图视图
                    if (dockMap.Selected ==false)
                    {
                        dockMap.Selected = true;
                        axMapCtlMain.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
                    }                 
                    break;
                }
                player = pEnumLayer.Next();
            }
            m_ToolRasterShift.pRasterLayer = m_TargetRasterLayer;
            m_ToolAddControlPoints.pRasterLayer = m_TargetRasterLayer;
            m_ToolAddControlPoints.m_FrmLinkTableRaster.pRasterLayer = m_TargetRasterLayer;
            m_cmdRasterShift.pRasterLayer = m_TargetRasterLayer;
            m_cmdRasterRotate.pRasterLayer = m_TargetRasterLayer;
            m_cmdRasterFlip.pRasterLayer = m_TargetRasterLayer;
            m_cmdRasterMirror.pRasterLayer = m_TargetRasterLayer;
            m_cmdRasterResample.pRasterLayer = m_TargetRasterLayer;
            m_cmdRasterTransZ.pRasterLayer = m_TargetRasterLayer;
            m_cmdRasterMosaic.pRasterLayer = m_TargetRasterLayer;
            m_cmdRasterSubset.pRasterLayer = m_TargetRasterLayer;
            m_cmdRasterNED2ENU.pRasterLayer = m_TargetRasterLayer;
            m_cmdRasterMeaAtt.pRasterLayer = m_TargetRasterLayer;
            m_cmdRasterRegister.pRasterLayer = m_TargetRasterLayer;
            m_ToolRasterEdit.m_pRasterLayer = m_TargetRasterLayer;
        }

        private void axTOCCtlLayer_OnDoubleClick(object sender, ITOCControlEvents_OnDoubleClickEvent e)
        {
            esriTOCControlItem item = esriTOCControlItem.esriTOCControlItemNone;
            IBasicMap pBMap = null;
            ILayer pLayer = null;
            object other = null;
            object index = null;

            axTOCCtlLayer.HitTest(e.x, e.y, ref item, ref pBMap, ref pLayer, ref other, ref index);
            if (pLayer == null)     return;

            //点击到Legend上
            if (pLayer is IFeatureLayer)
            {
                if (item == esriTOCControlItem.esriTOCControlItemLegendClass)
                {
                    IFeatureLayer pFeatureLayer = pLayer as IFeatureLayer;
                    if (pFeatureLayer.FeatureClass == null) return;
                    if (index == null) return;

                    ILegendClass pLegendClass = ((ILegendGroup)other).get_Class((int)index);
                    SetLegendSymbol(pLegendClass, pLayer);
                }
                else //if (item != esriTOCControlItem.esriTOCControlItemLegendClass)
                {
                    IFeatureLayer pFeatureLayer = pLayer as IFeatureLayer;
                    if (pFeatureLayer.FeatureClass == null)
                        return;

                    FrmUnique frmunique = new FrmUnique(this.axMapCtlMain, this.axTOCCtlLayer, pLayer, this.axSceneCtlMain);
                    frmunique.StartPosition = FormStartPosition.CenterScreen;
                    frmunique.ShowDialog();
                }
            }            
            else if (pLayer is IRasterLayer)//rasterlayer
            {
                IRasterLayer pRLayer = (IRasterLayer)pLayer;
                if (pRLayer.Raster == null) return;
                
                if (item == esriTOCControlItem.esriTOCControlItemLegendClass)
                {
                    IRasterRenderer rasterRenderer = pRLayer.Renderer as IRasterRenderer;
                    ILegendClass pLegendClass = ((ILegendGroup)other).get_Class((int)index);

                    if (rasterRenderer is IRasterClassifyColorRampRenderer ||
                        rasterRenderer is IRasterDiscreteColorRenderer ||
                        rasterRenderer is IRasterUniqueValueRenderer)
                    {
                        SetLegendSymbol(pLegendClass, pLayer);      
                    }
                    else if (rasterRenderer is IRasterRGBRenderer)
                    {
                        FrmSymbolRGB frmsymRGB = new FrmSymbolRGB(this.axMapCtlMain, this.axTOCCtlLayer, pRLayer, this.axSceneCtlMain);
                        frmsymRGB.StartPosition = FormStartPosition.CenterScreen;
                        frmsymRGB.ShowDialog();
                    }
                    else if (rasterRenderer is IRasterStretchColorRampRenderer)
                    {
                        LibCerMap.FrmSelectColorRamp frm = new LibCerMap.FrmSelectColorRamp(rasterRenderer, esriSymbologyStyleClass.esriStyleClassColorRamps);
                        if (frm.ShowDialog() == DialogResult.OK)
                        {
                            IRasterStretchColorRampRenderer stretch = rasterRenderer as IRasterStretchColorRampRenderer;
                            stretch.ColorRamp = frm.GetStyleGalleryItem().Item as IColorRamp;
                            RefreshView(axTOCCtlLayer, pLayer);
                        }
                    }
                    else
                    {
                        ;
                    }                   
                }
                else
                {
                    if (pRLayer.BandCount == 1)
                    {
                        FrmSymbology frmsym = new FrmSymbology(this.axMapCtlMain, this.axTOCCtlLayer, pRLayer, this.axSceneCtlMain, this.axPageLayoutCtlMain);
                        frmsym.StartPosition = FormStartPosition.CenterScreen;
                        frmsym.ShowDialog();
                    }
                    else
                    {
                        FrmSymbolRGB frmsymRGB = new FrmSymbolRGB(this.axMapCtlMain, this.axTOCCtlLayer, pRLayer, this.axSceneCtlMain);
                        frmsymRGB.StartPosition = FormStartPosition.CenterScreen;
                        frmsymRGB.ShowDialog();
                    }
                }
            }          
            else if (pLayer is ITinLayer) //tinlayer
            {
                ITinLayer pTLayer = (ITinLayer)pLayer;
                if (pTLayer.Dataset == null)
                    return;

                FrmSymboloTin frmsym = new FrmSymboloTin(this.axMapCtlMain, this.axTOCCtlLayer, this.axSceneCtlMain, pTLayer, this.barMain);
                frmsym.StartPosition = FormStartPosition.CenterScreen;
                frmsym.ShowDialog();
            }
        }

        private void SetLegendSymbol(ILegendClass pLegendClass, ILayer pLayer)
        {
            if (pLegendClass.Symbol is IMarkerSymbol)
            {
                FrmSymbol frmsb = new FrmSymbol(SymbolStyle, pLegendClass.Symbol, esriSymbologyStyleClass.esriStyleClassMarkerSymbols);
                if (frmsb.ShowDialog() == DialogResult.OK)
                {
                    pLegendClass.Symbol = frmsb.GetStyleGalleryItem().Item as ISymbol;                    
                }
            }
            else if (pLegendClass.Symbol is ILineSymbol)
            {
                FrmSymbol frmsb = new FrmSymbol(SymbolStyle, pLegendClass.Symbol, esriSymbologyStyleClass.esriStyleClassLineSymbols);
                if (frmsb.ShowDialog() == DialogResult.OK)
                {
                    pLegendClass.Symbol = frmsb.GetStyleGalleryItem().Item as ISymbol;
                }
            }
            else if (pLegendClass.Symbol is IFillSymbol)
            {
                FrmSymbol frmsb = new FrmSymbol(SymbolStyle, pLegendClass.Symbol, esriSymbologyStyleClass.esriStyleClassFillSymbols);
                if (frmsb.ShowDialog() == DialogResult.OK)
                {
                    pLegendClass.Symbol = frmsb.GetStyleGalleryItem().Item as ISymbol;
                }
            }
            else
            {

            }
            RefreshView(axTOCCtlLayer, pLayer);
        }

        private void barAttributeTable_ItemClick(object sender, EventArgs e)
        {

        }

        private void chkRandomGen_CheckedChanged(object sender, EventArgs e)
        {
            if (chkRandomGen.CheckState == CheckState.Checked)
            {
                grpRandomGen.Enabled = true;
                grpInputExisting.Enabled = false;
            }
        }

        private void chkInputExisting_CheckedChanged(object sender, EventArgs e)
        {
            if (chkInputExisting.CheckState == CheckState.Checked)
            {
                grpInputExisting.Enabled = true;
                grpRandomGen.Enabled = false;
            }
        }

        private void cmbTritype_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbTritype.SelectedItem != cmbiSubdivision)
            {
                cmbSubdivisionCount.Enabled = false;
            }
            else
            {
                cmbSubdivisionCount.Enabled = true;
            }
        }

        private void btnTextureFilename_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlgTextureFile = new OpenFileDialog();
            dlgTextureFile.Title = "打开纹理贴图路径：";
            dlgTextureFile.InitialDirectory = ".";
            //dlgTextureFile.Filter="3DS文件(*.3DS;*.3ds)|*.3DS;*.3ds|所有文件(*.*)|*.*";
            dlgTextureFile.Filter = "图像文件(*.BMP;*.PCX;*.TIFF;*.GIF;*.JPG;*.TGA;*.EXIF)|*.BMP;*.PCX;*.TIFF;*.GIF;*.JPG;*.TGA;*.EXIF|所有文件(*.*)|*.*";
            dlgTextureFile.RestoreDirectory = true;

            if (dlgTextureFile.ShowDialog() == DialogResult.OK)
            {
                txtTextureFilename.Text = dlgTextureFile.FileName;
            }
        }

        private void btnChooseExisingModel_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlgExistModelFile = new OpenFileDialog();
            dlgExistModelFile.Title = "选择已有模型：";
            dlgExistModelFile.InitialDirectory = ".";
            dlgExistModelFile.Filter = "3DS文件(*.3DS;*.3ds)|*.3DS;*.3ds|所有文件(*.*)|*.*";
            dlgExistModelFile.RestoreDirectory = true;

            if (dlgExistModelFile.ShowDialog() == DialogResult.OK)
            {
                txtExistingCrater.Text = dlgExistModelFile.FileName;
            }
        }

        private void btnOutputFilename_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlgOutputFile = new SaveFileDialog();
            dlgOutputFile.Title = "选择输出文件路径：";
            dlgOutputFile.InitialDirectory = ".";
            dlgOutputFile.Filter = "3DS文件(*.3DS;*.3ds)|*.3DS;*.3ds|所有文件(*.*)|*.*";
            dlgOutputFile.RestoreDirectory = true;
            dlgOutputFile.DefaultExt = "3ds";

            if (dlgOutputFile.ShowDialog() == DialogResult.OK)
            {
                txtOutputFilename.Text = dlgOutputFile.FileName;
            }
        }

        private void cmbModelType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbModelType.SelectedItem == cmbiCrater)
            {
                lblFirstParam.Text = "坑径(m):";
                lblSecondParam.Text = "坑深(m):";
                cmbTritype.SelectedItem = cmbiForward;
                cmbMappingType.SelectedItem = cmbiPlat;

                txtFirstParam.Value = 1000;
                txtSecondParam.Value = 300;
                txtThirdParam.Value = 2 * txtFirstParam.Value / 100;

                lblThirdParam.Visible = false;
                txtThirdParam.Visible = false;
                cmbSubdivisionCount.Enabled = false;
            }
            else if (/*cmbModelType.SelectedItem == cmbiCubic || */cmbModelType.SelectedItem == cmbiEllipse)
            {
                lblFirstParam.Text = "宽度(m):";
                lblSecondParam.Text = "长度(m):";
                lblThirdParam.Text = "高度(m):";
                cmbTritype.SelectedItem = cmbiSubdivision;
                cmbMappingType.SelectedItem = cmbiSphere;

                txtFirstParam.Value = 0.5;
                txtSecondParam.Value = 0.5;
                txtThirdParam.Value = 0.5;
                lblThirdParam.Visible = true;
                txtThirdParam.Visible = true;

                if (false)//cmbModelType.SelectedItem == cmbiCubic)
                {
                    cmbSubdivisionCount.Enabled = true;
                    cmbSubdivisionCount.SelectedIndex = 1;
                }
                else
                {
                    cmbSubdivisionCount.Enabled = false;
                    cmbSubdivisionCount.SelectedIndex = 3;
                }
            }
            else if (cmbModelType.SelectedItem == cmbiPyramid)
            {
                lblFirstParam.Text = "上底面宽(m):";
                lblSecondParam.Text = "下底面宽(m):";
                lblThirdParam.Text = "高度(m):";
                cmbTritype.SelectedItem = cmbiSubdivision;
                cmbMappingType.SelectedItem = cmbiSphere;

                txtFirstParam.Value = 0.5;
                txtSecondParam.Value = 0.5;
                txtThirdParam.Value = 0.5;
                lblThirdParam.Visible = true;
                txtThirdParam.Visible = true;

                cmbSubdivisionCount.Enabled = true;
                cmbSubdivisionCount.SelectedIndex = 1;
                //cmbSubdivisionCount.SelectedItem = cmbiSubdivisionZero;
            }
            else if (cmbModelType.SelectedItem == cmbiTetra)// || cmbModelType.SelectedItem == cmbiHemisphere)
            {
                lblFirstParam.Text = "宽度(m):";
                lblSecondParam.Text = "高度(m):";
                cmbTritype.SelectedItem = cmbiSubdivision;
                cmbMappingType.SelectedItem = cmbiSphere;

                txtFirstParam.Value = 0.5;
                txtSecondParam.Value = 0.5;
                txtThirdParam.Text = String.Empty;
                lblThirdParam.Visible = false;
                txtThirdParam.Visible = false;

                if (false)//cmbModelType.SelectedItem == cmbiHemisphere)
                {
                    cmbSubdivisionCount.Enabled = false;
                    cmbSubdivisionCount.SelectedIndex = 3;
                }
                else
                {
                    cmbSubdivisionCount.Enabled = true;
                    cmbSubdivisionCount.SelectedIndex = 1;
                }
                //cmbSubdivisionCount.SelectedItem = cmbiSubdivisionZero;
            }
            else
            {
                ;
            }
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            //检查各个输入参数是否符合要求，若不符合，直接返回
            if (!checkInputInfo())
            {
                return;
            }

            //输出的模型所在路径 
            String szModelFilename = String.Empty;
            //模型输出成功标识
            bool bFlag = false;

            //随机生成模型
            if (chkRandomGen.CheckState == CheckState.Checked)
            {
                //获取纹理贴图信息
                String szTextureFilename = txtTextureFilename.Text;
                //获取输出文件路径
                String szOutputFilename = txtOutputFilename.Text;
                //获取模型参数
                double dbFirstParam = txtFirstParam.Value;
                double dbSecondParam = txtSecondParam.Value;
                double dbThirdParam = 0;
                if (cmbModelType.SelectedItem != cmbiTetra)// && cmbModelType.SelectedItem != cmbiHemisphere)
                {
                    dbThirdParam = txtThirdParam.Value;
                }

                //获取细化次数
                int nSubdivisionCount = cmbSubdivisionCount.SelectedIndex;
                //获取三角化类型
                TriType enumTriType = TriType.TriBackward;
                if (cmbTritype.SelectedItem == cmbiForward)
                {
                    enumTriType = TriType.TriForward;
                }
                else if (cmbTritype.SelectedItem == cmbiBackword)
                {
                    enumTriType = TriType.TriBackward;
                }
                else if (cmbTritype.SelectedItem == cmbiSubdivision)
                {
                    enumTriType = TriType.TriSubdivision;
                }
                else
                {
                    ;
                }
                //获取纹理映射方式
                LibModelGen.MappingType enumMappingType = LibModelGen.MappingType.Flat;
                if (cmbMappingType.SelectedItem == cmbiPlat)
                {
                    enumMappingType = LibModelGen.MappingType.Flat;
                }
                else if (cmbMappingType.SelectedItem == cmbiSphere)
                {
                    enumMappingType = LibModelGen.MappingType.Sphere;
                }
                else
                {
                    ;
                }

                //将参数传给模型对象
                ModelBase model = null;
                if (cmbModelType.SelectedItem == cmbiCrater)
                {
                    model = new CraterGen(dbFirstParam, dbSecondParam);
                }
                else if (/*cmbModelType.SelectedItem == cmbiCubic || */cmbModelType.SelectedItem == cmbiEllipse)
                {
                    model = new CubicGen(dbFirstParam, dbSecondParam, dbThirdParam, (ushort)nSubdivisionCount);
                }
                else if (cmbModelType.SelectedItem == cmbiPyramid)
                {
                    model = new PyramidGen(dbFirstParam, dbSecondParam, dbThirdParam, (ushort)nSubdivisionCount);
                }
                else if (/*cmbModelType.SelectedItem == cmbiHemisphere ||*/ cmbModelType.SelectedItem == cmbiTetra)
                {
                    model = new TetraGen(dbFirstParam, dbSecondParam, (ushort)nSubdivisionCount);
                }
                else
                {
                    bFlag = false;
                }

                model.TexutreFilename = szTextureFilename.Substring(szTextureFilename.LastIndexOf('\\') + 1);
                model.OutputFilename = szOutputFilename;
                model.triype = enumTriType;
                model.SubDivisionCount = (ushort)nSubdivisionCount;
                model.mappingType = enumMappingType;

                //按照设置的参数生成相应的模型，并写成3DS文件，bFlag=true意味着模型生成成功
                bFlag = model.generate();
                if (bFlag)
                {
                    szModelFilename = model.OutputFilename;
                    txtOutputFilename.Text = String.Empty;
                    txtTextureFilename.Text = String.Empty;
                }
            }
            //导入已有模型
            else if (chkInputExisting.CheckState == CheckState.Checked)
            {
                if (!string.IsNullOrEmpty(txtExistingCrater.Text))
                {
                    szModelFilename = txtExistingCrater.Text;
                    txtExistingCrater.Text = String.Empty;

                    bFlag = ModelBase.readFile(szModelFilename);
                    if (bFlag)
                    {
                        m_CraterlistModels.Add(szModelFilename);
                        MessageBox.Show("已成功增加撞击坑模型！", "信息");
                    }
                    else
                        MessageBox.Show("无效的撞击坑文件！", "错误");
                }

                if (!string.IsNullOrEmpty(txtExistingNonCrater.Text))
                {
                    szModelFilename = txtExistingNonCrater.Text;
                    txtExistingNonCrater.Text = String.Empty;

                    bFlag = ModelBase.readFile(szModelFilename);
                    if (bFlag)
                    {
                        m_NonCraterlistModels.Add(szModelFilename);
                        MessageBox.Show("已成功增加石块模型！", "信息");
                    }
                    else
                        MessageBox.Show("无效的石块文件！", "错误");
                }

                bFlag = true;
            }
            //不可能执行
            else
            {
                ;
            }

            //模型生成成功，添加到模型链表中
            //if (bFlag)
            //{
            //   // m_listModels.Add(szModelFilename);
            //    MessageBox.Show("已成功增加模型！", "信息");
            //}
        }

        private bool checkInputInfo()
        {
            if (chkRandomGen.CheckState == CheckState.Checked)
            {
                if (String.IsNullOrEmpty(txtOutputFilename.Text))
                {
                    MessageBox.Show("请输入模型输出路径", "Error");
                    return false;
                }

                if (txtFirstParam.Value <= 0 || txtSecondParam.Value <= 0)
                {
                    MessageBox.Show("模型参数（高度、宽度、细化次数）不能为非正值", "错误");
                    return false;
                }

                if (cmbModelType.SelectedItem != cmbiTetra)//&& cmbModelType.SelectedItem != cmbiHemisphere)
                {
                    if (txtThirdParam.Value <= 0)
                    {
                        MessageBox.Show("模型参数（高度、宽度、细化次数）不能为非正值", "错误");
                        return false;
                    }

                    //if (txtThirdParam.Value >=250)
                    //{
                    //    MessageBox.Show("格网点不得超过250!", "错误");
                    //    return false;
                    //}
                }
            }
            else
            {
                //判断文件名是否为空
                if (String.IsNullOrEmpty(txtExistingCrater.Text) && String.IsNullOrEmpty(txtExistingNonCrater.Text))
                {
                    MessageBox.Show("请输入已有模型路径", "错误");
                    return false;
                }
                //不为空，则尝试以3DS文件读取，返回相应标识
                //else
                //{
                //    bool bFlag = ModelBase.readFile(txtExistingCrater.Text);
                //    if (!bFlag)
                //    {
                //        MessageBox.Show("无效的3DS文件！", "错误");
                //        return false;
                //    }
                //}
            }

            return true;
        }

        private void btnRandomModel_Click(object sender, EventArgs e)
        {
            if (m_TargetTinLayer == null)
            {
                MessageBox.Show("未指定目标Tin图层");
                return;
            }

            double xmax = m_TargetTinLayer.Dataset.Extent.XMax;
            double xmin = m_TargetTinLayer.Dataset.Extent.XMin;
            double ymax = m_TargetTinLayer.Dataset.Extent.YMax;
            double ymin = m_TargetTinLayer.Dataset.Extent.YMin;

            int nCraterCount = 0, nRockCount = 0;
            FrmRandomModelSetting frmSetting = new FrmRandomModelSetting();
            string LayerName = null;
            if (frmSetting.ShowDialog() == DialogResult.OK)
            {
                nCraterCount = frmSetting.CraterCount;
                nRockCount = frmSetting.RockCount;
                LayerName = frmSetting.TargetTinLayerName;
            }
            else
            {
                return;
            }
            List<string> CraterList = new List<string>();
            List<string> NonCraterList = new List<string>();
            #region 随机生成撞击坑
            int nCraterCompleteCount = 0;

            for (int i = 0; i < nCraterCount; i++)
            {
                String szOutputFilename = System.IO.Path.GetTempFileName();
                szOutputFilename = szOutputFilename.Substring(0, szOutputFilename.LastIndexOf('.')) + ".3ds";

                Random r = new Random(TerrainGen.Chaos_GetRandomSeed());
                TriType triType = TriType.TriForward;
                LibModelGen.MappingType mappingType = LibModelGen.MappingType.Flat;
                String szTextureFilename = String.Empty;

                int nSeed = (int)(r.NextDouble() * 100);
                //double nSeed = maxvalue - minvalue;
                while (nSeed == 0)
                {
                    nSeed = (int)(r.NextDouble() * 100);
                }

                double dbDiameter = nSeed * r.NextDouble();
                double dbDepth = dbDiameter * (r.NextDouble() * 0.2 + 0.2); //dbDepth/dbDiameter=[0.2,0.4]

                ModelBase model = new CraterGen(dbDiameter, dbDepth);
                model.OutputFilename = szOutputFilename;
                model.triype = triType;
                model.TexutreFilename = szTextureFilename;
                model.mappingType = mappingType;
                if (model.generate())
                {
                    //m_listModels.Add(szOutputFilename);
                    CraterList.Add(szOutputFilename);
                    nCraterCompleteCount++;
                }
            }
            #endregion
            if (m_CraterLayer == null)
            {
                MessageBox.Show("未指定随机撞击坑图层！");
            }
            else//将撞击坑添加至图层
            {
                for (int i = 0; i < CraterList.Count; i++)
                {
                    ITin pTin = m_TargetTinLayer.Dataset;
                    ISurface pSurface = ((ITinAdvanced)pTin).Surface;
                    Random r = new Random(TerrainGen.Chaos_GetRandomSeed());
                    double x = xmin + r.NextDouble() * (xmax - xmin);
                    double y = ymin + r.NextDouble() * (ymax - ymin);
                    IPoint mapPoint = new PointClass();
                    mapPoint.PutCoords(x, y);
                    IZAware za = mapPoint as IZAware;
                    za.ZAware = true;
                    double zVal;
                    zVal = pSurface.GetElevation(mapPoint);
                    if (double.IsNaN(zVal))
                    {
                        //MessageBox.Show("获取模型的高度失败！");
                        // return;
                        continue;
                    }
                    mapPoint.Z = zVal;
                    try
                    {
                        //IMultiPatch pMP = new MultiPatchClass();
                        IFeatureClass pFC = m_CraterLayer.FeatureClass;
                        IFeature pF = pFC.CreateFeature();
                        IImport3DFile pI3D = new Import3DFileClass();
                        pI3D.CreateFromFile(CraterList[i]);
                        IMultiPatch pMP = pI3D.Geometry as IMultiPatch;
                        ITransform3D pT3D = pMP as ITransform3D;
                        pT3D.Move3D(mapPoint.X, mapPoint.Y, mapPoint.Z);
                        pF.Shape = pMP as IGeometry;
                        pF.Store();
                        //if (pMapCtr != null)
                        //{
                        //    pMapCtr.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
                        //}
                    }
                    catch (SystemException ee)
                    {
                        MessageBox.Show(ee.Message);
                    }
                }
            }

            #region 随机生成石块
            int nRockCompleteCount = 0;

            for (int j = 0; j < nRockCount; j++)
            {
                String szOutputFilename = System.IO.Path.GetTempFileName();
                szOutputFilename = szOutputFilename.Substring(0, szOutputFilename.LastIndexOf('.')) + ".3ds";

                Random r = new Random(TerrainGen.Chaos_GetRandomSeed());

                //modelType=[1,3], 即除撞击坑以外的模型类型
                ModelType modelType = (ModelType)(int)Math.Round(2 * r.NextDouble() + 1);
                TriType triType = TriType.TriSubdivision;
                LibModelGen.MappingType mappingType = LibModelGen.MappingType.Sphere;

                //细化次数：0~4次
                int nSubdivisionCount = (int)Math.Round(r.NextDouble() * 3) + 1;
                String szTextureFilename = String.Empty;

                ModelBase model = null;
                int nSeed = (int)(r.NextDouble() * 100);
                while (nSeed == 0)
                {
                    nSeed = (int)(r.NextDouble() * 100);
                }
                if (modelType == ModelType.ModelCubic)
                {
                    double dbWidth = r.NextDouble() * nSeed;
                    double dbLength = r.NextDouble() * nSeed;
                    double dbHeight = r.NextDouble() * nSeed;
                    model = new CubicGen(dbWidth, dbLength, dbHeight, (ushort)nSubdivisionCount);
                }
                else if (modelType == ModelType.ModelPyramid)
                {
                    double dbUpperWidth = r.NextDouble() * nSeed;
                    double dbLowerWidth = r.NextDouble() * nSeed;
                    double dbHeight = r.NextDouble() * nSeed;
                    model = new PyramidGen(dbUpperWidth, dbLowerWidth, dbHeight, (ushort)nSubdivisionCount);
                }
                else if (modelType == ModelType.ModelTetrahedron)
                {
                    double dbWidth = r.NextDouble() * nSeed;
                    double dbHeight = r.NextDouble() * nSeed;
                    model = new TetraGen(dbWidth, dbHeight, (ushort)nSubdivisionCount);
                }
                else
                {
                    continue;
                }

                model.OutputFilename = szOutputFilename;
                model.triype = triType;
                model.TexutreFilename = szTextureFilename;
                model.mappingType = mappingType;
                if (model.generate())
                {
                    //m_listModels.Add(szOutputFilename);
                    NonCraterList.Add(szOutputFilename);
                    nRockCompleteCount++;
                }
            }
            #endregion
            if (m_NonCraterLayer == null)
            {
                MessageBox.Show("未指定石块图层!");
            }
            else
            {
                for (int i = 0; i < CraterList.Count; i++)
                {
                    ITin pTin = m_TargetTinLayer.Dataset;
                    ISurface pSurface = ((ITinAdvanced)pTin).Surface;
                    Random r = new Random(TerrainGen.Chaos_GetRandomSeed());
                    double x = xmin + r.NextDouble() * (xmax - xmin);
                    double y = ymin + r.NextDouble() * (ymax - ymin);
                    IPoint mapPoint = new PointClass();
                    mapPoint.PutCoords(x, y);
                    IZAware za = mapPoint as IZAware;
                    za.ZAware = true;
                    double zVal;
                    zVal = pSurface.GetElevation(mapPoint);
                    if (double.IsNaN(zVal))
                    {
                        // MessageBox.Show("获取模型的高度失败！");
                        // return;
                        continue;
                    }
                    mapPoint.Z = zVal;
                    try
                    {
                        //IMultiPatch pMP = new MultiPatchClass();
                        IFeatureClass pFC = m_NonCraterLayer.FeatureClass;
                        IFeature pF = pFC.CreateFeature();
                        IImport3DFile pI3D = new Import3DFileClass();
                        pI3D.CreateFromFile(NonCraterList[i]);
                        IMultiPatch pMP = pI3D.Geometry as IMultiPatch;
                        ITransform3D pT3D = pMP as ITransform3D;
                        pT3D.Move3D(mapPoint.X, mapPoint.Y, mapPoint.Z);
                        pF.Shape = pMP as IGeometry;
                        pF.Store();
                        //if (pMapCtr != null)
                        //{
                        //    pMapCtr.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
                        //}
                    }
                    catch (SystemException ee)
                    {
                        MessageBox.Show(ee.Message);
                    }
                }
            }
            axMapCtlMain.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
            //String szMessage= String.Format("已随机生成:{0}个撞击坑,{1}个石块.", nCraterCompleteCount, nRockCompleteCount);
            //MessageBox.Show(szMessage);
        }

        private void buttonItemMerge_Click(object sender, EventArgs e)
        {
            FrmMerge fm = new FrmMerge();
            fm.m_Map = axMapCtlMain.Map;
            fm.m_Scene = axSceneCtlMain.Scene;
            fm.ShowDialog();
        }


        private void btnRandomTerrain_Click(object sender, EventArgs e)
        {
            // #region 测试代码
            //           AddModelToTerrain test = new AddModelToTerrain();

            //           List<Model> listModelInfo = new List<Model>();
            //           int nCount = 10;
            //           for (int i = 0; i < nCount; i++)
            //           {
            //               Model tmpModel = new Model();
            //               tmpModel.szModelType = (i % 2 == 0) ? "Rock" : "Crater";
            //               //tmpModel.szModelType += "Crater";// +i.ToString();
            //               tmpModel.x = i * 5;
            //               tmpModel.y = i * 5;
            //               tmpModel.dbSize = 1;

            //               listModelInfo.Add(tmpModel);
            //           }

            //           bool bFlag = test.writeModelInfoToXml(@"C:\Users\Administrator\Desktop\ModelInfo.xml", listModelInfo);

            //           if (bFlag)
            //           {
            //               List<Model> testModelInfo = new List<Model>();
            //               testModelInfo = test.readModelInfoFromXml(@"C:\Users\Administrator\Desktop\ModelInfo.xml");
            //           }
            //           return;
            //#endregion
            double dbResolution = 0, dbUndulation = 0;
            Point2D[] ptRange = new Point2D[2];
            String szOutputFilename = String.Empty;

            FrmRandomTerrain randomTerrain = new FrmRandomTerrain();
            randomTerrain.m_pMap = axMapCtlMain.Map;
            if (randomTerrain.ShowDialog() == DialogResult.OK)
            {
                dbResolution = randomTerrain.Resolution;
                dbUndulation = randomTerrain.Undulation;
                ptRange = randomTerrain.PtTerrainRange;
                szOutputFilename = randomTerrain.OutputFilename;
                try
                {
                    if (File.Exists(szOutputFilename))
                    {
                        File.Delete(szOutputFilename);
                    }
                }
                catch (SystemException ee)
                {
                    MessageBox.Show(ee.Message);
                    return;
                }
            }
            else
            {
                return;
            }

            TerrainGen terrain = new TerrainGen();
            //订阅进度消息
            terrain.m_infoProgress += this.setProgressBarValue;

            //if (String.IsNullOrEmpty(szOutputFilename))
            {
                String szTmpFilename = System.IO.Path.GetTempFileName();
                szTmpFilename = szTmpFilename.Substring(0, szTmpFilename.LastIndexOf('.')) + ".tif";
                bool bGenFlag = terrain.interpolateLargeTerrain(ptRange, dbResolution, dbUndulation, szTmpFilename);
                if (!bGenFlag)
                    return;

                IRasterLayer pRasterLayer = new RasterLayerClass();
                pRasterLayer.CreateFromFilePath(szTmpFilename);
                IRaster raster = pRasterLayer.Raster;
                //IRaster raster = terrain.generateTerrain(ptRange, dbResolution, dbUndulation);
                if (raster != null)
                {
                    //if (TerrainGen.generateTerrain(ptRange, dbResolution, dbUndulation, szOutputFilename))
                    //{
                    ////地形生成，生成相应的纹理
                    ////String szHeightmapFilename = szOutputFilename;
                    ////int[] nThresholdValues = new int[3] { 90, 190, 255 };
                    ////szOutputFilename = szOutputFilename.Substring(0, szOutputFilename.LastIndexOf('.')) + "_texture.tif";

                    ////String szPath="C:\\Users\\syw\\Desktop\\texture\\lunar_textures\\";
                    ////String[] szTextureFilenames = new String[3] { szPath+"dirt.jpg",  szPath+"dirt_1.jpg", szPath+"rock.jpg" };
                    ////TextureGen.textureGen(szHeightmapFilename, szTextureFilenames, nThresholdValues, szOutputFilename);

                    //String szMessage = String.Format("已成功生成随机地形及纹理！\n{0}", szOutputFilename);
                    //MessageBox.Show(szMessage);
                    //IRasterLayer pRLayer = new RasterLayerClass();
                    //pRLayer.CreateFromRaster(raster);
                    //axMapCtlMain.Map.AddLayer(pRLayer as ILayer);
                    FrmDEMToTin frm = new FrmDEMToTin();
                    ITin tin = frm.DEMToTIN(raster);
                    ITinLayer tinlayer = new TinLayerClass();
                    tinlayer.Dataset = tin;
                    tinlayer.Name = "Mem_RandomTinLayer";
                    // tinlayer.TipText = "RandomTinLayer";                
                    //ITinEdit pTEdit = tin as ITinEdit;
                    //object ob = true;
                    //string path = Application.StartupPath + "\\" +"Temp\\";
                    //pTEdit.SaveAs(path + "Temp", ref ob);
                    //pTEdit.StopEditing(false);
                    double xmax = tinlayer.Dataset.Extent.XMax;
                    double xmin = tinlayer.Dataset.Extent.XMin;
                    double ymax = tinlayer.Dataset.Extent.YMax;
                    double ymin = tinlayer.Dataset.Extent.YMin;
                    //生成地形的地理范围
                    double dbGeoRangeX = xmax - xmin;
                    double dbGeoRangeY = ymax - ymin;

                    List<string> CraterList = new List<string>();
                    List<string> NonCraterList = new List<string>();
                    ClsGDBDataCommon GDC = new ClsGDBDataCommon();
                    IWorkspace pWS = GDC.OpenFromFileGDB(GetParentPathofExe() + @"Resource\DefaultFileGDB\MultiPatchFile.gdb");
                    IFeatureClass CraterFeatureClass = ((IFeatureWorkspace)pWS).OpenFeatureClass("Crater");
                    IFeatureClass NonCraterFeatureClass = ((IFeatureWorkspace)pWS).OpenFeatureClass("NonCrater");
                    ITable pTable = CraterFeatureClass as ITable;
                    //清空所有对象
                    IFeatureCursor pFeatureCursor = CraterFeatureClass.Search(null, false);
                    IFeature pFeature = pFeatureCursor.NextFeature();
                    while (pFeature != null)
                    {
                        pFeature.Delete();
                        pFeature = pFeatureCursor.NextFeature();
                    }
                    pFeatureCursor = NonCraterFeatureClass.Search(null, false);
                    pFeature = pFeatureCursor.NextFeature();
                    while (pFeature != null)
                    {
                        pFeature.Delete();
                        pFeature = pFeatureCursor.NextFeature();
                    }
                    FrmRandomModelSetting frmRMS = new FrmRandomModelSetting();
                    #region 随机生成撞击坑
                    int nCraterCompleteCount = 0;
                    int nCraterCount = randomTerrain.CraterNum;
                    int nRockCount = randomTerrain.StoneNum;

                    for (int i = 0; i < nCraterCount; i++)
                    {
                        String szModelOutputFilename = System.IO.Path.GetTempFileName();
                        szModelOutputFilename = szModelOutputFilename.Substring(0, szModelOutputFilename.LastIndexOf('.')) + ".3ds";

                        Random r = new Random(TerrainGen.Chaos_GetRandomSeed());
                        TriType triType = TriType.TriForward;
                        LibModelGen.MappingType mappingType = LibModelGen.MappingType.Flat;
                        String szTextureFilename = String.Empty;

                        //int nSeed = (int)(r.NextDouble() * 100);
                        ////double nSeed = maxvalue - minvalue;
                        //while (nSeed == 0)
                        //{
                        //    nSeed = (int)(r.NextDouble() * 100);
                        //}

                        //随机撞击坑的大小随机值
                        double dbSeed = r.NextDouble() * 0.15 + 0.05;   //[0.05,0.2]

                        //double dbDiameter = nSeed * r.NextDouble();
                        //double dbDepth = dbDiameter * (r.NextDouble() * 0.2 + 0.2); //dbDepth/dbDiameter=[0.2,0.4]
                        double dbDiameter = dbSeed * Math.Min(dbGeoRangeX, dbGeoRangeY);
                        double dbDepth = dbDiameter * (r.NextDouble() * 0.2 + 0.2); //dbDepth/dbDiameter=[0.2,0.4]

                        ModelBase model = new CraterGen(dbDiameter, dbDepth);
                        model.OutputFilename = szModelOutputFilename;
                        model.triype = triType;
                        model.TexutreFilename = szTextureFilename;
                        model.mappingType = mappingType;
                        if (model.generate())
                        {
                            //m_listModels.Add(szOutputFilename);
                            CraterList.Add(szModelOutputFilename);
                            nCraterCompleteCount++;
                        }
                    }
                    #endregion
                    #region 添加撞击坑至图层
                    for (int i = 0; i < CraterList.Count; i++)
                    {
                        ITin pTin = tinlayer.Dataset;
                        ISurface pSurface = ((ITinAdvanced)pTin).Surface;
                        Random r = new Random(TerrainGen.Chaos_GetRandomSeed());
                        double x = xmin + r.NextDouble() * (xmax - xmin);
                        double y = ymin + r.NextDouble() * (ymax - ymin);
                        IPoint mapPoint = new PointClass();
                        mapPoint.PutCoords(x, y);
                        IZAware za = mapPoint as IZAware;
                        za.ZAware = true;
                        double zVal;
                        zVal = pSurface.GetElevation(mapPoint);
                        if (double.IsNaN(zVal))
                        {
                            MessageBox.Show("获取模型的高度失败！");
                            // return;
                        }
                        mapPoint.Z = zVal;
                        try
                        {
                            //IMultiPatch pMP = new MultiPatchClass();
                            // IFeatureClass pFC = m_CraterLayer.FeatureClass;
                            IFeatureClass pFC = CraterFeatureClass;
                            IFeature pF = pFC.CreateFeature();
                            IImport3DFile pI3D = new Import3DFileClass();
                            pI3D.CreateFromFile(CraterList[i]);
                            IMultiPatch pMP = pI3D.Geometry as IMultiPatch;
                            ITransform3D pT3D = pMP as ITransform3D;
                            pT3D.Move3D(mapPoint.X, mapPoint.Y, mapPoint.Z);

                            pF.Shape = pMP as IGeometry;
                            pF.Store();
                            //if (pMapCtr != null)
                            //{
                            //    pMapCtr.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
                            //}
                        }
                        catch (SystemException ee)
                        {
                            MessageBox.Show(ee.Message);
                        }
                    }
                    #endregion
                    #region 随机生成石块
                    int nRockCompleteCount = 0;

                    //获得当前文件路径下的石头模型文件
                    String szPathName = GetParentPathofExe() + @"Resource\RockModel";
                    String[] szFileList = Directory.GetFiles(szPathName);
                    if (szFileList.Length == 0)
                    {
                        MessageBox.Show("找不到石块模型库文件！");
                        return;
                    }

                    for (int j = 0; j < nRockCount; j++)
                    {
                        //随机获得石块模型名字，从模型库中读取
                        Random r = new Random(TerrainGen.Chaos_GetRandomSeed());
                        int nRandomPos = r.Next(szFileList.Length);
                        String szModelOutputFilename = szFileList[nRandomPos];
                        //szModelOutputFilename = szModelOutputFilename.Substring(0, szModelOutputFilename.LastIndexOf('.')) + ".3ds";
                        NonCraterList.Add(szModelOutputFilename);
                        nRockCompleteCount++;

                        //Random r = new Random(TerrainGen.Chaos_GetRandomSeed());

                        ////modelType=[1,3], 即除撞击坑以外的模型类型
                        //ModelType modelType = (ModelType)(int)Math.Round(2 * r.NextDouble() + 1);
                        //TriType triType = TriType.TriSubdivision;
                        //LibModelGen.MappingType mappingType = LibModelGen.MappingType.Sphere;

                        ////细化次数：0~4次
                        //int nSubdivisionCount = (int)Math.Round(r.NextDouble() * 3) + 1;
                        //String szTextureFilename = String.Empty;

                        //ModelBase model = null;
                        //int nSeed = (int)(r.NextDouble() * 100);
                        //while (nSeed == 0)
                        //{
                        //    nSeed = (int)(r.NextDouble() * 100);
                        //}
                        //if (modelType == ModelType.ModelCubic)
                        //{
                        //    double dbWidth = r.NextDouble() * nSeed;
                        //    double dbLength = r.NextDouble() * nSeed;
                        //    double dbHeight = r.NextDouble() * nSeed;
                        //    model = new CubicGen(dbWidth, dbLength, dbHeight, (ushort)nSubdivisionCount);
                        //}
                        //else if (modelType == ModelType.ModelPyramid)
                        //{
                        //    double dbUpperWidth = r.NextDouble() * nSeed;
                        //    double dbLowerWidth = r.NextDouble() * nSeed;
                        //    double dbHeight = r.NextDouble() * nSeed;
                        //    model = new PyramidGen(dbUpperWidth, dbLowerWidth, dbHeight, (ushort)nSubdivisionCount);
                        //}
                        //else if (modelType == ModelType.ModelTetrahedron)
                        //{
                        //    double dbWidth = r.NextDouble() * nSeed;
                        //    double dbHeight = r.NextDouble() * nSeed;
                        //    model = new TetraGen(dbWidth, dbHeight, (ushort)nSubdivisionCount);
                        //}
                        //else
                        //{
                        //    continue;
                        //}

                        //model.OutputFilename = szModelOutputFilename;
                        //model.triype = triType;
                        //model.TexutreFilename = szTextureFilename;
                        //model.mappingType = mappingType;
                        //if (model.generate())
                        //{
                        //    //m_listModels.Add(szOutputFilename);
                        //    NonCraterList.Add(szModelOutputFilename);
                        //    nRockCompleteCount++;
                        //}
                    }
                    #endregion
                    #region 添加石块至图层
                    for (int i = 0; i < NonCraterList.Count; i++)
                    {
                        ITin pTin = tinlayer.Dataset;
                        ISurface pSurface = ((ITinAdvanced)pTin).Surface;
                        Random r = new Random(TerrainGen.Chaos_GetRandomSeed());
                        double x = xmin + r.NextDouble() * (xmax - xmin);
                        double y = ymin + r.NextDouble() * (ymax - ymin);
                        IPoint mapPoint = new PointClass();
                        mapPoint.PutCoords(x, y);
                        IZAware za = mapPoint as IZAware;
                        za.ZAware = true;
                        double zVal;
                        zVal = pSurface.GetElevation(mapPoint);
                        if (double.IsNaN(zVal))
                        {
                            MessageBox.Show("获取模型的高度失败！");
                            // return;
                        }
                        mapPoint.Z = zVal;
                        try
                        {
                            //IMultiPatch pMP = new MultiPatchClass();
                            //IFeatureClass pFC = m_NonCraterLayer.FeatureClass;
                            IFeatureClass pFC = NonCraterFeatureClass;
                            IFeature pF = pFC.CreateFeature();
                            IImport3DFile pI3D = new Import3DFileClass();
                            pI3D.CreateFromFile(NonCraterList[i]);
                            IMultiPatch pMP = pI3D.Geometry as IMultiPatch;
                            ITransform3D pT3D = pMP as ITransform3D;
                            pT3D.Move3D(mapPoint.X, mapPoint.Y, mapPoint.Z);
                            double dbModelRangeX = pMP.Envelope.Width;
                            double dbModelRangeY = pMP.Envelope.Height;
                            double dbModelRangeZ = pMP.Envelope.ZMax - pMP.Envelope.ZMin;

                            //根据地形大小改变石块大小
                            double dbRandomSize = Math.Min(dbGeoRangeX, dbGeoRangeY) * (r.NextDouble() * 0.03 + 0.03); //[0.03 0.06]
                            //double dbRandomSizeY = dbGeoRangeY * (r.NextDouble() * 0.09 + 0.01); //[0.01 0.1]
                            //double dbRandomSizeZ = (dbRandomSizeX + dbRandomSizeY) / 2;//dbGeoRangeX * (r.NextDouble() * 0.05 + 0.15); //[0.05 0.2]
                            double dbScale = dbRandomSize / Math.Max(dbModelRangeX, dbModelRangeY);
                            //double dbScaleY = dbRandomSizeY / dbModelRangeY;
                            //double dbScaleZ = (dbScaleX + dbScaleY) / 2;
                            pT3D.Scale3D(mapPoint, dbScale, dbScale, dbScale);

                            pF.Shape = pMP as IGeometry;
                            pF.Store();
                            //if (pMapCtr != null)
                            //{
                            //    pMapCtr.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
                            //}
                        }
                        catch (SystemException ee)
                        {
                            MessageBox.Show(ee.Message);
                        }
                    }
                    #endregion

                    FrmMerge Fmerge = new FrmMerge();
                    IFeatureLayer craterlayer = new FeatureLayerClass();
                    craterlayer.FeatureClass = CraterFeatureClass;
                    IFeatureLayer NonCraterlayer = new FeatureLayerClass();
                    NonCraterlayer.FeatureClass = NonCraterFeatureClass;
                    ITinEdit pTinEdit = tin as ITinEdit;
                    try
                    {
                        object vtMissing = true;
                        //pTinEdit = tin as ITinEdit;
                        pTinEdit.SaveAs(GetParentPathofExe() + @"Resource\DefaultFileGDB\TempTin", ref vtMissing);
                        pTinEdit.StopEditing(false);
                    }
                    catch (SystemException ee)
                    {
                        if (ee.Message == "The specified file or dataset already exists.")
                        {
                            pTinEdit.Save();
                            pTinEdit.StopEditing(true);
                        }
                        else
                        {
                            MessageBox.Show(ee.Message);
                        }
                    }
                    //将撞击坑与石块融入地形
                    Fmerge.MergeNonCraterToMemoryTin(tinlayer, NonCraterlayer);
                    Fmerge.MergeCraterToMemoryTin(tinlayer, craterlayer);

                    //tin转DEM
                    try
                    {
                        FrmTinToDEM FTintoDEM = new FrmTinToDEM();
                        ITinAdvanced ptina = tin as ITinAdvanced;
                        DirectoryInfo dir = new DirectoryInfo(szOutputFilename);
                        String sdir = dir.Parent.FullName;
                        String name = dir.Name;
                        IRasterDataset pRD = FTintoDEM.TinToRaster_new(ptina, esriRasterizationType.esriElevationAsRaster, sdir, name, rstPixelType.PT_DOUBLE, randomTerrain.Resolution, tin.Extent, true);
                        IRasterLayer prl = new RasterLayerClass();
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(pRD);
                        // prl.CreateFromDataset(pRD);
                        prl.CreateFromFilePath(szOutputFilename);
                        axMapCtlMain.Map.AddLayer(prl as ILayer);
                        //axMapCtlMain.Map.AddLayer(tinlayer as ILayer);
                        MessageBox.Show("随机生成地形成功！");
                        GC.Collect();
                    }
                    catch (System.Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    // MessageBox.Show("转换成功！");
                    // axMapCtlMain.Map.AddLayer(tinlayer as ILayer);
                    axMapCtlMain.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
                }
                else
                {
                    MessageBox.Show("随机生成地形失败！");
                }

                //try
                //{
                //    Directory.Delete(szTmpFilename);
                //}
                //catch (System.Exception ex)
                //{
                //    MessageBox.Show(ex.Message);
                //}
            }

            // else
            {
                //int nSizeX = (int)(Math.Abs(ptRange[0].X - ptRange[1].X) / dbResolution);
                //int nSizeY = (int)(Math.Abs(ptRange[0].Y - ptRange[1].Y) / dbResolution);

                //bool bFlag = false;
                //if (Math.Max(nSizeX, nSizeY) > terrain.SubBlockSize * 4)
                //{
                //    //bFlag = terrain.generateLargeTerrain(ptRange, dbResolution, dbUndulation, szOutputFilename);
                //    bFlag = terrain.interpolateLargeTerrain(ptRange, dbResolution, dbUndulation, szOutputFilename);
                //}
                //else
                //{
                //    bFlag = terrain.generateTerrain(ptRange, dbResolution, dbUndulation, szOutputFilename);
                //}

                //if (bFlag)
                //{
                //    ////地形生成，生成相应的纹理
                //    ////String szHeightmapFilename = szOutputFilename;
                //    ////int[] nThresholdValues = new int[3] { 90, 190, 255 };
                //    ////szOutputFilename = szOutputFilename.Substring(0, szOutputFilename.LastIndexOf('.')) + "_texture.tif";

                //    ////String szPath="C:\\Users\\syw\\Desktop\\texture\\lunar_textures\\";
                //    ////String[] szTextureFilenames = new String[3] { szPath+"dirt.jpg",  szPath+"dirt_1.jpg", szPath+"rock.jpg" };
                //    ////TextureGen.textureGen(szHeightmapFilename, szTextureFilenames, nThresholdValues, szOutputFilename);

                //    String szMessage = String.Format("已成功生成随机地形及纹理！\n{0}", szOutputFilename);
                //    MessageBox.Show(szMessage);
                //    IRasterLayer pRLayer = new RasterLayerClass();
                //    pRLayer.CreateFromFilePath(szOutputFilename);
                //    axMapCtlMain.Map.AddLayer(pRLayer as ILayer);
                //    axMapCtlMain.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
                //}
                //else
                //{
                //    MessageBox.Show("随机生成地形失败！");
                //}
            }

            //取消订阅
            terrain.m_infoProgress -= this.setProgressBarValue;
        }

        private void setProgressBarValue(int nValue)
        {
            this.progressBarXMain.Value = nValue;

            if (nValue == progressBarXMain.Maximum)
            {
                progressBarXMain.Value = 0;
            }
        }

        private void btnRandomTexture_Click(object sender, EventArgs e)
        {
            FrmRandomTexture randomTexture = new FrmRandomTexture();
            randomTexture.m_pMap = axMapCtlMain.Map;
            if (randomTexture.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    String szOutputFilename = randomTexture.OutputFilename;
                    String[] szTextureFilenames = randomTexture.TextureNames;
                    int[] nThresholdValues = randomTexture.ThresholdValues;
                    IRaster pRaster = randomTexture.pRaster;

                    if (TextureGen.textureGen(pRaster, szTextureFilenames, nThresholdValues, szOutputFilename))
                    {
                        String msg = String.Format("已成功生成随机纹理{0}！", szOutputFilename);
                        MessageBox.Show(msg);
                    }
                    else
                    {
                        MessageBox.Show("随机生成纹理失败！");
                        return;
                    }


                    IRasterLayer prasterlayer = new RasterLayerClass();
                    prasterlayer.CreateFromFilePath(szOutputFilename);
                    axMapCtlMain.AddLayer(prasterlayer);
                    axMapCtlMain.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
                }
                catch (SystemException ee)
                {
                    MessageBox.Show(ee.Message);
                }
            }
        }

        //*********************************************************************************************************************
        // 布局窗体点击设计
        private void axPageLayoutCtlMain_OnMouseDown(object sender, IPageLayoutControlEvents_OnMouseDownEvent e)
        {
            
        }

        private void axPageLayoutCtlMain_OnMouseUp(object sender, IPageLayoutControlEvents_OnMouseUpEvent e)
        {
            //右键
            if (e.button == 2)
            {
                IGraphicsContainer pGraphicsContainer = axPageLayoutCtlMain.ActiveView.GraphicsContainer;
                IGraphicsContainerSelect pGraphicsContainerSelect = pGraphicsContainer as IGraphicsContainerSelect;
                int SelectElementCount = pGraphicsContainerSelect.ElementSelectionCount;
                IEnumElement pEnumElement = pGraphicsContainerSelect.SelectedElements;
                pEnumElement.Reset();
                IElement pElement = pEnumElement.Next();
                IElementProperties pElementProperties = pElement as IElementProperties;

                //只有当布局界面选择的要素个数不为零，而且右键点击时才出现右键菜单
                if (SelectElementCount != 0)
                {
                    m_menuElement.PopupMenu(e.x, e.y, axPageLayoutCtlMain.hWnd);
                }
                //else
                //{
                //    IMap pMap = axPageLayoutCtlMain.ActiveView.FocusMap;
                //    if (m_EngineEditor.Map == pMap && m_EngineEditor.EditState == esriEngineEditState.esriEngineStateEditing)
                //    {
                //        IEngineEditTask pTask = m_EngineEditor.CurrentTask;
                //        if (pTask.UniqueName == "ControlToolsEditing_ModifyFeatureTask" ||
                //             pTask.UniqueName == "ControlToolsEditing_CreateNewFeatureTask")
                //        {
                //            m_EngineEditSketch.SetEditLocation(e.x, e.y);//必须设置此属性Insert Vertex才能用
                //            m_menuEditor.PopupMenu(e.x, e.y, axMapCtlMain.hWnd);//编辑草图状态下右键菜单
                //        }
                //    }
                //}
            }
        }

        //布局界面双击
        private void axPageLayoutCtlMain_OnDoubleClick(object sender, IPageLayoutControlEvents_OnDoubleClickEvent e)
        {
            //需要编辑的图层

        }
        //数据界面双击
        private void axMapCtlMain_OnDoubleClick(object sender, IMapControlEvents2_OnDoubleClickEvent e)
        {

        }
        //*********************************************************************************************************************
        private void comboBoxExNonCrater_TextChanged(object sender, EventArgs e)
        {

        }

        private void axMapCtlMain_OnMapReplaced(object sender, IMapControlEvents2_OnMapReplacedEvent e)
        {
            //if (e != null)
            //{
            //  IPageLayoutControl2 pLayeoutctr = (m_hookHelper.Hook) as IPageLayoutControl2;
            // if (pLayeoutctr != null && pLayeoutctr.CustomProperty != null)

            //ArrayList al = pLayeoutctr.CustomProperty as ArrayList;
            // IMap pMap = al[1] as IMap;
            // IMapControl2 pmapctr = al[0] as IMapControl2;

            //IMap pMap = axPageLayoutCtlMain.ActiveView.FocusMap;
            //axPageLayoutCtlMain.PageLayout.FocusNextMapFrame();
            //bool containnewmap = false;
            //if (pMap.Equals(e.newMap))
            //{
            //    containnewmap = true;
            //}
            //while (!axPageLayoutCtlMain.ActiveView.FocusMap.Equals(pMap))
            //{
            //    if (axPageLayoutCtlMain.ActiveView.FocusMap.Equals(e.newMap))
            //    {
            //        containnewmap = true;
            //        break;
            //    }
            //    axPageLayoutCtlMain.PageLayout.FocusNextMapFrame();
            //}

            //如果layout控件的地图中包含map控件的新map，就指定layout控件的新map，否则，替换整个maps
            //if (containnewmap == true)
            //{
            //    axPageLayoutCtlMain.ActiveView.FocusMap = e.newMap as IMap;
            //}
            //else
            //{
            //    IMaps maps = new Maps();

            //    while (maps.Count != 0)
            //    {
            //        maps.RemoveAt(0);
            //    }

            //    //add the new map to the Maps collection
            //    maps.Add(e.newMap as IMap);                    

            //    axPageLayoutCtlMain.PageLayout.ReplaceMaps(maps);

            //}
            //}

            //sunyiwei
            //ICompositeLayer cgl = (ICompositeLayer)axMapCtlMain.Map.BasicGraphicsLayer;
            //if (cgl.Count == 0)
            //{
            //    m_NewDisplacement.pGraphicsLayer = ((ICompositeGraphicsLayer)axMapCtlMain.Map.BasicGraphicsLayer).AddLayer("AdjustLayer", null);

            //    IGraphicsContainerEvents_Event pGCE = axMapCtlMain.Map.BasicGraphicsLayer as IGraphicsContainerEvents_Event;
            //    pGCE.ElementAdded += new ESRI.ArcGIS.Carto.IGraphicsContainerEvents_ElementAddedEventHandler(ElementAddedMethod);
            //    pGCE.ElementAdded += new ESRI.ArcGIS.Carto.IGraphicsContainerEvents_ElementAddedEventHandler(ElementAddedMethod);

            //    pGCE.ElementUpdated += new ESRI.ArcGIS.Carto.IGraphicsContainerEvents_ElementUpdatedEventHandler(ElementUpdatedMethod);
            //    pGCE.ElementDeleted += new ESRI.ArcGIS.Carto.IGraphicsContainerEvents_ElementDeletedEventHandler(ElementDeletedMethod);
            //}

            axMapCtlMain.Refresh();
            axPageLayoutCtlMain.Refresh();
        }

        private void axPageLayoutCtlMain_OnPageLayoutReplaced(object sender, IPageLayoutControlEvents_OnPageLayoutReplacedEvent e)
        {
            // ICompositeLayer cgl = (ICompositeLayer)axMapCtlMain.Map.BasicGraphicsLayer;
            //if (cgl.Count == 0)
            //{
            //    m_NewDisplacement.pGraphicsLayer = ((ICompositeGraphicsLayer)axMapCtlMain.Map.BasicGraphicsLayer).AddLayer("AdjustLayer", null);

            //    IGraphicsContainerEvents_Event pGCE = axMapCtlMain.Map.BasicGraphicsLayer as IGraphicsContainerEvents_Event;
            //    pGCE.ElementAdded += new ESRI.ArcGIS.Carto.IGraphicsContainerEvents_ElementAddedEventHandler(ElementAddedMethod);
            //    pGCE.ElementUpdated += new ESRI.ArcGIS.Carto.IGraphicsContainerEvents_ElementUpdatedEventHandler(ElementUpdatedMethod);
            //    pGCE.ElementDeleted += new ESRI.ArcGIS.Carto.IGraphicsContainerEvents_ElementDeletedEventHandler(ElementDeletedMethod);
            //}
        }
  
        private void axMapCtlMain_OnViewRefreshed(object sender, IMapControlEvents2_OnViewRefreshedEvent e)
        {
            ////  superGridSelection.PrimaryGrid.Rows.Clear();
            //cmbTargetTinLayer.Items.Clear();
            //comboBoxExCrater.Items.Clear();
            //comboBoxExNonCrater.Items.Clear();
            //IMap pMap = axMapCtlMain.Map; 
            //for (int i = 0; i < pMap.LayerCount; i++)
            //{
            //    //更新选择gridview
            //    ILayer pLayer = pMap.get_Layer(i);
            //    //bool bCanSelect = false;
            //    //bool bVisible = true;
            //    //if (pLayer is IFeatureLayer)
            //    //{
            //    //    bCanSelect = ((IFeatureLayer)pLayer).Selectable;
            //    //    GridPanel panel = superGridSelection.PrimaryGrid;
            //    //    GridRow row = new GridRow(pLayer.Name, bCanSelect, bVisible);
            //    //    panel.Rows.Add(row);
            //    //}
            //    //else
            //    //{
            //    //    GridPanel panel = superGridSelection.PrimaryGrid;                    
            //    //    GridRow row = new GridRow(pLayer.Name, bCanSelect, bVisible);
            //    //    row.Cells[1].ReadOnly = true;
            //    //    panel.Rows.Add(row);
            //    //}
            //    //更新tin编辑目标图层

            //    if (pLayer is ITinLayer)
            //    {
            //        //ITinLayer pTLayer = (ITinLayer)pLayer ;
            //        //ITinRenderer pRenderNew = new TinFaceRenderer() as ITinRenderer;
            //        //ITinSingleSymbolRenderer pUVRenderer = pRenderNew as ITinSingleSymbolRenderer;
            //        //ISimpleFillSymbol pSymbol = new SimpleFillSymbolClass();
            //        //pSymbol.Color = ColorToIColor(System.Drawing.Color.Green);
            //        //pUVRenderer.Symbol = pSymbol as ISymbol;
            //        //pTLayer.ClearRenderers();
            //        //pTLayer.InsertRenderer(pRenderNew, 0);

            //        cmbTargetTinLayer.Items.Add(pLayer.Name);
            //        if (cmbTargetTinLayer.SelectedIndex == -1 && cmbTargetTinLayer.Items.Count > 0)
            //        {
            //            cmbTargetTinLayer.SelectedIndex = 0;                       
            //        }
            //    }
            //    if (pLayer is IFeatureLayer)
            //    {
            //        IFeatureLayer pfLayer = pLayer as IFeatureLayer;
            //        if (pfLayer.FeatureClass != null)
            //        {
            //            if (pfLayer.FeatureClass.ShapeType == esriGeometryType.esriGeometryMultiPatch)
            //            {
            //                comboBoxExCrater.Items.Add(pfLayer.Name);
            //                comboBoxExNonCrater.Items.Add(pfLayer.Name);
            //            }
            //        }
            //    }
            //}
            //for (int i = 0; i < cmbTargetTinLayer.Items.Count; i++)
            //{
            //    string layername = cmbTargetTinLayer.Items[i].ToString();
            //    //List<ILayer> layerlist = GetLayersByName(layername); 
            //    if (m_TargetTinLayer.Name == layername)
            //    {
            //        cmbTargetTinLayer.SelectedIndex = i;
            //    }
            //}
            //for (int i = 0; i < comboBoxExCrater.Items.Count; i++)
            //{
            //    string layername = comboBoxExCrater.Items[i].ToString();
            //    if (m_CraterLayer == null)
            //    {
            //        comboBoxExCrater.SelectedIndex = -1;
            //        break;
            //    }
            //    if (m_CraterLayer.Name == layername)
            //    {
            //        comboBoxExCrater.SelectedIndex = i;
            //    }
            //}
            //for (int i = 0; i < comboBoxExNonCrater.Items.Count; i++)
            //{
            //    string layername = comboBoxExNonCrater.Items[i].ToString();
            //    if (m_NonCraterLayer == null)
            //    {
            //        comboBoxExNonCrater.SelectedIndex = -1;
            //        break;
            //    }
            //    if (m_NonCraterLayer.Name == layername)
            //    {
            //        comboBoxExNonCrater.SelectedIndex = i;
            //    }
            //}
            //for (int i = 0; i < cmbTargetRasterLayer.Items.Count; i++)
            //{
            //    string layername = cmbTargetRasterLayer.Items[i].ToString();
            //    //List<ILayer> layerlist = GetLayersByName(layername); 
            //    if (m_TargetRasterLayer == null)
            //    {
            //        break;
            //    }
            //    if (m_TargetRasterLayer.Name == layername)
            //    {
            //        cmbTargetRasterLayer.SelectedIndex = i;
            //    }

            //}

            //try
            //{
            //    IActiveView iv = axSceneCtlMain.Scene as IActiveView;
            //    iv.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
            //}
            //catch
            //{
            //}
        }

  
        //创建3D目标图层
        private void buttonItemNewFeature_Click(object sender, EventArgs e)
        {

            //        SaveFileDialog dlg = new SaveFileDialog();
            //        dlg.Filter = "shapefile|.shp";
            //        try
            //        {
            //            if (dlg.ShowDialog() == DialogResult.OK)
            //            {
            //                ClsGDBDataCommon comm = new ClsGDBDataCommon();
            //                DirectoryInfo di = new DirectoryInfo(dlg.FileName);
            //                IWorkspace inmemWor = comm.OpenFromShapefile(di.Parent.FullName);

            //                IFields pFields = new FieldsClass();
            //                IFieldsEdit pFieldsEdit = (IFieldsEdit)pFields;

            //                //设置字段   
            //                IField pField = new FieldClass();
            //                IFieldEdit pFieldEdit = (IFieldEdit)pField;

            //                //创建类型为几何类型的字段   
            //                pFieldEdit.Name_2 = "shape";
            //                pFieldEdit.Type_2 = esriFieldType.esriFieldTypeGeometry;
            //                /*
            //                //不定义空间参考会报异常
            //                //为esriFieldTypeGeometry类型的字段创建几何定义，包括类型和空间参照    
            //                IGeometryDef pGeoDef = new GeometryDefClass();     //The geometry definition for the field if IsGeometry is TRUE.   
            //                IGeometryDefEdit pGeoDefEdit = (IGeometryDefEdit)pGeoDef;
            //                pGeoDefEdit.GeometryType_2 = esriGeometryType.esriGeometryPolygon;
            //                pGeoDefEdit.SpatialReference_2 = new UnknownCoordinateSystemClass();
            //                pGeoDefEdit.SpatialReference.SetDomain(-200, 200, -200, 200);//没有这句就抛异常来自HRESULT：0x8004120E。
            //               // pGeoDefEdit.SpatialReference_2 = pSPF;
            //                pFieldEdit.GeometryDef_2 = pGeoDef;
            //                pFieldsEdit.AddField(pField);
            //                */

            //                IGeometryDef pGeoDef = new GeometryDefClass();
            //                IGeometryDefEdit pGeoDefEdit = (IGeometryDefEdit)pGeoDef;
            //                pGeoDefEdit.AvgNumPoints_2 = 5;
            //                pGeoDefEdit.GeometryType_2 = esriGeometryType.esriGeometryMultiPatch; ;
            //                pGeoDefEdit.GridCount_2 = 1;
            //                pGeoDefEdit.HasM_2 = false;
            //                pGeoDefEdit.HasZ_2 = false;
            //                //pGeoDefEdit.SpatialReference_2 = SpatialRef;
            //                pGeoDefEdit.SpatialReference_2 = new UnknownCoordinateSystemClass();//没有这一句就报错，说尝试读取或写入受保护的内存。
            //                pGeoDefEdit.SpatialReference.SetDomain(-8000000, 8000000, -800000, 8000000);//没有这句就抛异常来自HRESULT：0x8004120E。

            //                pFieldEdit.Name_2 = "SHAPE";
            //                pFieldEdit.Type_2 = esriFieldType.esriFieldTypeGeometry;
            //                pFieldEdit.GeometryDef_2 = pGeoDef;
            //                pFieldEdit.IsNullable_2 = true;
            //                pFieldEdit.Required_2 = true;
            //                pFieldsEdit.AddField(pField);    

            //                // ifeatureworkspacee
            //                IFeatureWorkspace pFeatureWorkspace = inmemWor as IFeatureWorkspace;
            //                IFeatureClass fc = pFeatureWorkspace.CreateFeatureClass(di.Name, pFields, null, null, esriFeatureType.esriFTSimple, "shape", "");
            //                //System.Runtime.InteropServices.Marshal.ReleaseComObject(pname);
            //                System.Runtime.InteropServices.Marshal.ReleaseComObject(pFeatureWorkspace);
            //                System.Runtime.InteropServices.Marshal.ReleaseComObject(fc);
            //                System.Runtime.InteropServices.Marshal.ReleaseComObject(inmemWor);
            //            }
            //        }
            //        catch (SystemException ee)
            //        {
            //            MessageBox.Show(ee.Message);
            //        }
            //
            try
            {
                SaveFileDialog saveFileDlg = new SaveFileDialog();
                saveFileDlg.DefaultExt = "gdb";
                saveFileDlg.Filter = "(文件型数据库)*.gdb|*.gdb";
                if (saveFileDlg.ShowDialog(this) == DialogResult.OK)
                {
                    string strFileGDB = saveFileDlg.FileName;

                    string strPath = System.IO.Path.GetDirectoryName(strFileGDB);
                    string strName = System.IO.Path.GetFileName(strFileGDB);

                    ClsGDBDataCommon com = new ClsGDBDataCommon();
                    com.Create_FileGDB(strPath, strName);
                }
            }
            catch (SystemException ee)
            {
                MessageBox.Show(ee.Message);
            }
        }

        private void axMapCtlMain_OnMouseMove(object sender, IMapControlEvents2_OnMouseMoveEvent e)
        {

#region 废弃

            //if (axMapCtlMain.Map.DistanceUnits == esriUnits.esriDecimalDegrees)
            //{
            //    cmdMapUnit.pDegreeMode = 1;
            //}

            //switch (cmdMapUnit.pDegreeMode)
            //{
            //    case 0:
            //    case 1:
            //        string x = e.mapX.ToString();
            //        string y = e.mapY.ToString();
            //        //string unit = axMapCtlMain.Map.MapUnits.ToString();
            //        string unit = axMapCtlMain.Map.DistanceUnits.ToString();
            //        string unit2 = unit.Substring(4, unit.Length - 4);
            //        lblInfo.Text = x + " " + y + " " + unit2;
            //        break;
            //    case 2:
            //        int dDegX = Convert.ToInt32(Math.Truncate(e.mapX));
            //        int dMinX = Convert.ToInt32(Math.Truncate((e.mapX - Math.Truncate(e.mapX)) * 60));
            //        int dSecX = Convert.ToInt32(Math.Round(((e.mapX - Math.Truncate(e.mapX)) * 60 - Math.Truncate((e.mapX - Math.Truncate(e.mapX)) * 60)) * 60));
            //        int dDegY = Convert.ToInt32(Math.Truncate(e.mapY));
            //        int dMinY = Convert.ToInt32(Math.Truncate((e.mapY - Math.Truncate(e.mapY)) * 60));
            //        int dSecY = Convert.ToInt32(Math.Round(((e.mapY - Math.Truncate(e.mapY)) * 60 - Math.Truncate((e.mapY - Math.Truncate(e.mapY)) * 60)) * 60));
            //        string ux = dDegX.ToString() + "°" + dMinX.ToString() + "'" + dSecX.ToString() + '"';
            //        string uy = dDegY.ToString() + "°" + dMinY.ToString() + "'" + dSecY.ToString() + '"';
            //        lblInfo.Text = ux + "   " + uy;
            //        break;
            //    default:
            //        break;
            //}
#endregion

            if (cmdMapUnit.IsModeSet)
            {
                switch (cmdMapUnit.pDegreeMode)
                {
                    /////////////////////////////////////////////////////////
                    //0:显示方式不为度
                    //1：显示方式为十进制度
                    //2：显示方式为度分秒
                    ////////////////////////////////////////////////////////
                    case 0:
                        if (axMapCtlMain.Map.SpatialReference != null)
                        {
                            string x = e.mapX.ToString();
                            string y = e.mapY.ToString();
                            //string unit = axMapCtlMain.Map.MapUnits.ToString();
                            string unit = axMapCtlMain.Map.DistanceUnits.ToString();
                            string unit2 = unit.Substring(4, unit.Length - 4);
                            lblInfo.Text = x + " " + y + " " + unit2;
                        }
                        break;
                    case 1:
                        if (axMapCtlMain.Map.SpatialReference != null)
                        {
                            ISpatialReferenceFactory pSRF = new SpatialReferenceEnvironmentClass();
                            IPoint PointInMap = new PointClass();
                            PointInMap.PutCoords(e.mapX, e.mapY);
                            PointInMap.SpatialReference = pSRF.CreateProjectedCoordinateSystem(axMapCtlMain.Map.SpatialReference.FactoryCode);
                            PointInMap.Project(pSRF.CreateGeographicCoordinateSystem((int)esriSRGeoCS3Type.esriSRGeoCS_TheMoon));//104903
                            string unit = axMapCtlMain.Map.DistanceUnits.ToString();
                            string unit2 = unit.Substring(4, unit.Length - 4);
                            lblInfo.Text = PointInMap.X + " " + PointInMap.Y + " " + unit2;
                        }
                        break;
                    case 2:
                        if (axMapCtlMain.Map.SpatialReference != null)
                        {
                            string ux = "";
                            string uy = "";
                            ISpatialReferenceFactory pSRF = new SpatialReferenceEnvironmentClass();
                            IPoint PointInMap = new PointClass();
                            PointInMap.PutCoords(e.mapX, e.mapY);
                            PointInMap.SpatialReference = pSRF.CreateProjectedCoordinateSystem(axMapCtlMain.Map.SpatialReference.FactoryCode);
                            PointInMap.Project(pSRF.CreateGeographicCoordinateSystem((int)esriSRGeoCS3Type.esriSRGeoCS_TheMoon));//104903                        

                            int dDegX = Convert.ToInt32(Math.Truncate(PointInMap.X));
                            int dMinX = Convert.ToInt32(Math.Truncate((PointInMap.X - Math.Truncate(PointInMap.X)) * 60));
                            int dSecX = Convert.ToInt32(Math.Round(((PointInMap.X - Math.Truncate(PointInMap.X)) * 60 - Math.Truncate((PointInMap.X - Math.Truncate(PointInMap.X)) * 60)) * 60));
                            int dDegY = Convert.ToInt32(Math.Truncate(PointInMap.Y));
                            int dMinY = Convert.ToInt32(Math.Truncate((PointInMap.Y - Math.Truncate(PointInMap.Y)) * 60));
                            int dSecY = Convert.ToInt32(Math.Round(((PointInMap.Y - Math.Truncate(PointInMap.Y)) * 60 - Math.Truncate((PointInMap.Y - Math.Truncate(PointInMap.Y)) * 60)) * 60));
                            if (dDegX > 0)
                            {
                                ux = dDegX.ToString() + "°" + dMinX.ToString() + "'" + dSecX.ToString() + '"' + "E";
                            }
                            if (dDegY > 0)
                            {
                                uy = dDegY.ToString() + "°" + dMinY.ToString() + "'" + dSecY.ToString() + '"' + "N";
                            }
                            if (dDegX < 0)
                            {
                                ux = Math.Abs(dDegX).ToString() + "°" + Math.Abs(dMinX).ToString() + "'" + Math.Abs(dSecX).ToString() + '"' + "W";
                            }
                            if (dDegY < 0)
                            {
                                uy = Math.Abs(dDegY).ToString() + "°" + Math.Abs(dMinY).ToString() + "'" + Math.Abs(dSecY).ToString() + '"' + "S";
                            }

                            lblInfo.Text = ux + "   " + uy;
                        }

                        break;
                    default:
                        break;
                }
            }
            else
            {

                string x = e.mapX.ToString();
                string y = e.mapY.ToString();
                string unit = axMapCtlMain.Map.MapUnits.ToString();
                string unit2 = unit.Substring(4, unit.Length - 4);
                lblInfo.Text = x + " " + y + " " + unit2;
            }
            
            if (isMeaAttStart)
            {
                IPolygonElement PolygonElement = new PolygonElementClass();
                IElement pElement = PolygonElement as IElement;
                pElement.Geometry = pPolygonMeaAtt;
                //pGraphiccsContainer = axMapCtlMain.Map as IGraphicsContainer;
                pGraphiccsContainer.AddElement((IElement)PolygonElement, 0);
                axMapCtlMain.ActiveView.Refresh();
                ClsRasterMeaAttribute clsMeaAtt = new ClsRasterMeaAttribute();
                double pixelmax=0.0;
                double pixelmin=0.0;
                if (m_TargetRasterLayer!=null)
                {
                    clsMeaAtt.RasterMeaAtt(m_TargetRasterLayer.Raster, pPolygonMeaAtt,ref pixelmax,ref pixelmin);
                    if (MessageBox.Show("最大值：" + pixelmax.ToString() + "\n" + "最小值：" + pixelmin.ToString(), "计算信息", MessageBoxButtons.OK) == DialogResult.OK)
                    {
                        pGraphiccsContainer.DeleteAllElements();
                        axMapCtlMain.ActiveView.Refresh();
                    }
                }                
                isMeaAttStart = false;
                
                //SetCurrentTool(axToolbarControlCommon, "esriControls.ControlsMapPanTool");
            }
        }

        private void buttonItemnewFeatureLayer_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDlg = new SaveFileDialog();
            saveFileDlg.Filter = "";
            //if (saveFileDlg.ShowDialog(this) == DialogResult.OK)
            //{
            //    string strFileGDB = saveFileDlg.FileName;

            //    string strPath = System.IO.Path.GetDirectoryName(strFileGDB);
            //    string strName = System.IO.Path.GetFileName(strFileGDB);

            //}
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "";
            try
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    ClsGDBDataCommon comm = new ClsGDBDataCommon();
                    DirectoryInfo di = new DirectoryInfo(dlg.FileName);
                    IWorkspace inmemWor = comm.OpenFromFileGDB(di.Parent.FullName);

                    IFields pFields = new FieldsClass();
                    IFieldsEdit pFieldsEdit = (IFieldsEdit)pFields;

                    //设置字段   
                    IField pField = new FieldClass();
                    IFieldEdit pFieldEdit = (IFieldEdit)pField;

                    //创建类型为几何类型的字段   
                    pFieldEdit.Name_2 = "shape";
                    pFieldEdit.Type_2 = esriFieldType.esriFieldTypeGeometry;
                    /*
                    //不定义空间参考会报异常
                    //为esriFieldTypeGeometry类型的字段创建几何定义，包括类型和空间参照    
                    IGeometryDef pGeoDef = new GeometryDefClass();     //The geometry definition for the field if IsGeometry is TRUE.   
                    IGeometryDefEdit pGeoDefEdit = (IGeometryDefEdit)pGeoDef;
                    pGeoDefEdit.GeometryType_2 = esriGeometryType.esriGeometryPolygon;
                    pGeoDefEdit.SpatialReference_2 = new UnknownCoordinateSystemClass();
                    pGeoDefEdit.SpatialReference.SetDomain(-200, 200, -200, 200);//没有这句就抛异常来自HRESULT：0x8004120E。
                   // pGeoDefEdit.SpatialReference_2 = pSPF;
                    pFieldEdit.GeometryDef_2 = pGeoDef;
                    pFieldsEdit.AddField(pField);
                    */

                    IGeometryDef pGeoDef = new GeometryDefClass();
                    IGeometryDefEdit pGeoDefEdit = (IGeometryDefEdit)pGeoDef;
                    //pGeoDefEdit.AvgNumPoints_2 = 5;
                    pGeoDefEdit.GeometryType_2 = esriGeometryType.esriGeometryMultiPatch; ;
                    //pGeoDefEdit.GridCount_2 = 1;
                    // pGeoDefEdit.HasM_2 = false;
                    pGeoDefEdit.HasZ_2 = true;
                    //pGeoDefEdit.SpatialReference_2 = SpatialRef;
                    pGeoDefEdit.SpatialReference_2 = new UnknownCoordinateSystemClass();//没有这一句就报错，说尝试读取或写入受保护的内存。
                    pGeoDefEdit.SpatialReference.SetDomain(-8000000, 8000000, -8000000, 8000000);//没有这句就抛异常来自HRESULT：0x8004120E。
                    pGeoDefEdit.SpatialReference.SetZDomain(-8000000, 8000000);

                    pFieldEdit.Name_2 = "SHAPE";
                    pFieldEdit.Type_2 = esriFieldType.esriFieldTypeGeometry;
                    pFieldEdit.GeometryDef_2 = pGeoDef;
                    pFieldEdit.IsNullable_2 = true;
                    pFieldEdit.Required_2 = true;
                    pFieldsEdit.AddField(pField);

                    // ifeatureworkspacee
                    IFeatureWorkspace pFeatureWorkspace = inmemWor as IFeatureWorkspace;
                    IFeatureClass fc = pFeatureWorkspace.CreateFeatureClass(di.Name, pFields, null, null, esriFeatureType.esriFTSimple, "shape", "");
                    //System.Runtime.InteropServices.Marshal.ReleaseComObject(pname);
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(pFeatureWorkspace);
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(fc);
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(inmemWor);
                }
            }
            catch (SystemException ee)
            {
                MessageBox.Show(ee.Message);
            }

        }

        private void buttonItemNewDoc_Click_Click(object sender, EventArgs e)
        {
            //execute New Document command
            DialogResult res = MessageBox.Show("是否保存当前工作空间?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (res == DialogResult.Yes)
            {
                buttonItemSaveDoc_Click(null, null);
            }

            //craete a new Map
            IMaps pMaps = new Maps();
            IMap map = new MapClass();
            map.Name = "Map";
            pMaps.Add(map);

            IPageLayout2 pPagelayout = axPageLayoutCtlMain.PageLayout as IPageLayout2;
            pPagelayout.ClearContent();
            pPagelayout.ReplaceMaps(pMaps);
            axPageLayoutCtlMain.DocumentFilename = null;

            dockLayout.Selected = true;
            
            m_controlsSynchronizer.ActivatePageLayout();
            //同时清理map和scene
            //axMapCtlMain.Map = axPageLayoutCtlMain.ActiveView.FocusMap;
            m_controlsSynchronizer.ReplaceMap(axPageLayoutCtlMain.ActiveView.FocusMap);
            //axMapCtlMain.ActiveView.Refresh();
            axSceneCtlMain.Scene.ClearLayers();
            //清理属性表
            if (barAttributeTable.Visible == true)
            {
                if (AttributeTable!=null)
                {
                    AttributeTable.Clear();
                    AttributeTable.Columns.Clear();
                    barAttributeTable.Visible = false;
                }
            }
        }

        //打开地图文档
        private void buttonItemOpenDoc_Click(object sender, EventArgs e)
        {
            dockLayout.Selected = true;
            ICommand command = new LibEngineCmd.CmdOpenMxdDoc(m_controlsSynchronizer,barAttributeTable, AttributeTable,null);
            command.OnCreate(axPageLayoutCtlMain.Object);
           
            command.OnClick();
            SetMainApplictationTitle();
            SetCurrentTool(axToolbarControlLayout, "esriControls.ControlsSelectTool");
        }

        private void buttonItemSaveDoc_Click(object sender, EventArgs e)
        {
            dockLayout.Selected = true;
            ICommand command = new LibEngineCmd.CmdSaveMxdDoc();
            command.OnCreate(axPageLayoutCtlMain.Object);
            command.OnClick();
        }

        private void buttonItemSaveAs_Click(object sender, EventArgs e)
        {
            //SaveAs文档无法得到保存的文档名称，需要自己写保存文档，以便得到保存的文件名
            dockLayout.Selected = true;
            //ICommand cmd = new ControlsSaveAsDocCommandClass();
            ICommand cmd = new LibEngineCmd.CmdSaveAsMxdDoc();
            cmd.OnCreate(axPageLayoutCtlMain.Object);
            cmd.OnClick();
            SetMainApplictationTitle();
        }

        private void buttonItemExit_Click(object sender, EventArgs e)
        {
            //exit the application
            Application.Exit();
        }


        #region 属性表
        private bool m_bSelectChangeMap = false;
        /// <summary>
        /// 当地图窗口的地物选择发生变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void axMapCtlMain_OnSelectionChanged(object sender, EventArgs e)
        {
            ILayer pLayer = ClsGDBDataCommon.GetLayerFromName(axMapCtlMain.Map, barAttributeTable.Text);
            if (pLayer == null) return;          
            SetGridTableSelection(GridTable, pLayer);
            RefreshGridTable(GridTable);
        }

        #region 属性表操作

        //列表中选择改变
        private void GridTable_SelectionChanged(object sender, EventArgs e)
        {

        }
        
        private void GridTable_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
 
        }

        
        private void TableSelectionChange()
        {
            //列表选择改变时要分为是显示全部还是显示选择集状态
            //显示全部时要清除图层原有选择集，显示选择集时不清除图层选择集，而是增加新的对象的选中显示
            ILayer layer = ClsGDBDataCommon.GetLayerFromName(axMapCtlMain.Map, barAttributeTable.Text);
           
            DataGridViewSelectedRowCollection coll = GridTable.SelectedRows;
            for (int i = 0; i < coll.Count; i++)
            {
                DataGridViewRow row = coll[i];
                row.Selected = true;
                //得到ID字段所在表中的列索引
                int nIndexID = GetFIDorObjectIDIndexInTabel(layer,GridTable);
                //得到FID或ObjectID
                int nID = GetFeatureFIDorObjecIDofRow(layer,GridTable, row);
                //得到图形要素
                if (layer is IFeatureLayer)
                {
                    IFeatureLayer featurelayer = layer as IFeatureLayer;
                    IFeatureSelection selection = featurelayer as IFeatureSelection;
                    IFeature feature = GetFeatureFromFID(featurelayer, nID);
                    //设置图层要素选中
                    if (feature != null)
                    {
                        selection.Add(feature);
                        selection.SelectionChanged();
                    }
                }
                RefreshSelectedRecordNum(GridTable);
            }
            axMapCtlMain.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, layer, null);
        }
        //删除指定FID的行
        private bool DeleteRowofFID(DataGridViewX gridTable, ILayer layer, int nFID)
        {
            try
            {
                //从属性表中删除对对象
                //得到ID字段所在表中的列索引
                int nColIndex = GetFIDorObjectIDIndexInTabel(layer, GridTable);
                int nRowIndex = GetRowIndexFromValue(GridTable, nColIndex, nFID);
                GridTable.Rows.RemoveAt(nRowIndex);
            }
            catch (System.Exception ex)
            {
                return false;
            }
            return true;
        }

        //刷新列表显示
        private void RefreshGridTable(DataGridViewX gridTable)
        {
            try
            {
                for (int i = 0; i < gridTable.Rows.Count; i++)
                {
                    if (btnshowallout.Checked)
                    {
                        gridTable.Rows[i].Visible = true;
                    }
                    else
                    {
                        if (gridTable.Rows[i].Selected == true)
                        {
                            gridTable.Rows[i].Visible = true;
                        }
                        else
                        {
                            gridTable.Rows[i].Visible = false;
                        }
                    }
                }

                RefreshSelectedRecordNum(gridTable);
                gridTable.Update();
            }
            catch (System.Exception ex)
            {

            }
        }

        private void RefreshSelectedRecordNum(DataGridViewX gridTable)
        {
            DataGridViewSelectedRowCollection rows = GridTable.SelectedRows;
            btnNOsel.Text = "{" + rows.Count + "} out of {" + gridTable.Rows.Count + "} is selected";
        }

        //设置表格中的选择属性
        private void SetGridTableSelection(DataGridViewX gridTable, ILayer layer)
        {
            if (!(layer is IFeatureLayer)) return;

            //清除表格选特性
            //ClearGridTableSelection(gridTable);
            gridTable.ClearSelection();

            IFeatureLayer featureLayer = (IFeatureLayer)layer;
            IFeatureSelection featureSelection = (IFeatureSelection)featureLayer;
            ISelectionSet selectionSet = featureSelection.SelectionSet;
            if (selectionSet.Count > 0)
            {
                ICursor cursor;
                selectionSet.Search(null, true, out cursor);
                IFeatureCursor featureCursor = cursor as IFeatureCursor;
                IFeature feature = null;
                //得到ID字段所在表中的列索引
                int nIndexID = GetFIDorObjectIDIndexInTabel(layer,GridTable);
                int nID = -1;
                int nRowSelected = -1;
                while ((feature = featureCursor.NextFeature()) != null)
                {
                    //得到FID或ObjectID
                    nID = GetFIDorObjectIDofFeature(feature);
                    if (nID >= 0)
                    {
                        //得到表中对应ID的行
                        nRowSelected = GetRowIndexFromValue(GridTable, nIndexID, nID);
                        if (nRowSelected >= 0)
                        {
                            DataGridViewRow row = GridTable.Rows[nRowSelected];
                            row.Selected = true; 
                        }
                    }
                }
            }
            
        }

        //清除表格选特性
        private void ClearGridTableSelection(DataGridViewX gridTable)
        {
            for (int i = 0; i < gridTable.Rows.Count;i++ )
            {
                gridTable.Rows[i].Tag = 0;
            }
        }

        //得到指定Feature的ID
        private int GetFIDorObjectIDofFeature(IFeature feature)
        {
            int nID =-1;
            if (feature.HasOID)
            {
                nID =int.Parse(feature.OID.ToString());
            }
           
            return nID;
            
        }

        //得到指定图层中ID的要素
        private IFeature GetFeatureFromFID(ILayer layer, int nID)
        {
            IFeature feature = null;
            try
            {
            if (layer is IFeatureLayer)
            {
                IFeatureLayer featureLayer = layer as IFeatureLayer;
                if (featureLayer.FeatureClass != null)
                {
                    IFeatureClass featureClass = featureLayer.FeatureClass;
                    feature = featureClass.GetFeature(nID);
                } 
            }
            }
            catch (System.Exception ex)
            {

            }
            return feature;
        }

        //得到表中指定Feature ID的列索引
        private int GetFIDorObjectIDIndexInTabel(ILayer layer,DataGridViewX gridTable)
        {
            int nIndex = -1;
            //得到FeatureClass的OID
            if (layer is IFeatureLayer)
            {
                IFeatureLayer featureLayer = layer as IFeatureLayer;
                if (featureLayer.FeatureClass != null)
                {
                    IFeatureClass featureClass = featureLayer.FeatureClass;
                    string strObjFieldName = featureClass.OIDFieldName;

                    for (int i = 0; i < GridTable.Columns.Count; i++)
                    {
                        if (GridTable.Columns[i].Name == strObjFieldName)
                        {
                            nIndex = i;
                            break;
                        }
                    }
                }
                     
            }
           
            return nIndex;
        }

        //得到指定行的图形的FID
        private int GetFeatureFIDorObjecIDofRow(ILayer layer ,DataGridViewX gridTable,DataGridViewRow row)
        {
            try
            {
                int nFID = -1;
                int nIndexCol = GetFIDorObjectIDIndexInTabel(layer, gridTable);
                return Convert.ToInt32(row.Cells[nIndexCol].Value);
            }
            catch (System.Exception ex)
            {

            }
            return -1;
        }
        //得到指定列值所在的行
        private int GetRowIndexFromValue(DataGridViewX gridTable, int nCol, int nID)
        {
            int nRow = -1;
            if (nCol >=0 && nCol <gridTable.ColumnCount)
            {
                for (int i = 0; i < gridTable.Rows.Count;i++ )
                {
                    int nValue = Convert.ToInt32(gridTable.Rows[i].Cells[nCol].Value);
                    if (nValue == nID)
                    {
                        nRow = i;
                        break;
                    }
                }
            }
            return nRow;
        }

        private void GridTable_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void GridTable_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            //在此判断此字段值是否可以编辑
            ILayer layer = ClsGDBDataCommon.GetLayerFromName(axMapCtlMain.Map, barAttributeTable.Text);
            //判断图层是否处于编辑状态
            if (layer is IFeatureLayer)
            {
                IFeatureLayer featureLayer = layer as IFeatureLayer;
                IEngineEditLayers editLayers = m_EngineEditor as IEngineEditLayers;
                if (editLayers.IsEditable(featureLayer))
                {
                    //判断点击的字段是否可以编辑
                    string strFieldName = GridTable.Columns[e.ColumnIndex].Name;
                    if (!IsEditable(featureLayer,strFieldName))
                    {
                        e.Cancel = true;
                    }
                }
                else
                {
                    e.Cancel = true;
                    //MessageBox.Show("请先设置编辑图层！");
                }
            }
            else
            {
                e.Cancel = true;
            }
            
        }

        //判断字段是否可以编辑
        private bool IsEditable(IFeatureLayer layer,string strFieldName)
        {
            bool bEdit = true;
            if (layer.FeatureClass !=null)
            {
                IFields fields = layer.FeatureClass.Fields;
                IField field = fields.get_Field(fields.FindField(strFieldName));
                
                if (field.Type == esriFieldType.esriFieldTypeBlob ||
                    field.Type == esriFieldType.esriFieldTypeGlobalID ||
                    field.Type == esriFieldType.esriFieldTypeGUID ||
                    field.Type == esriFieldType.esriFieldTypeOID ||
                    field.Type == esriFieldType.esriFieldTypeRaster ||
                    field.Type == esriFieldType.esriFieldTypeGeometry)
                {
                    bEdit = false;
                }
            }

            return bEdit;
        }

        
        private void GridTable_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                //在此更新字段值
                if (e.ColumnIndex < 0 || e.RowIndex < 0) return;

                string strFieldName = GridTable.Columns[e.ColumnIndex].Name;
                string strNewValue = GridTable.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                //得到对应图形的FID
                ILayer layer = ClsGDBDataCommon.GetLayerFromName(axMapCtlMain.Map, barAttributeTable.Text);
                int nFid = GetFeatureFIDorObjecIDofRow(layer, GridTable, GridTable.Rows[e.RowIndex]);


                //判断值是否改变，改变时才编辑回
                if (layer is IFeatureLayer)
                {
                    IFeatureLayer featureLayer = layer as IFeatureLayer;
                    if (featureLayer.FeatureClass == null) return;
                    IFeatureClass featureClass = featureLayer.FeatureClass;
                    //得到对应的Feature
                    IFeature feature = featureClass.GetFeature(nFid);
                    if (feature == null) return;
                    //得到编辑的字段
                    int nField = featureClass.FindField(strFieldName);
                    IField field = featureClass.Fields.get_Field(nField);
                    //检查值是否匹配
                    object obj = feature.get_Value(nField);
                    if (/*!field.CheckValue((object)strNewValue) ||*/ (!field.Editable)  )
                    {
                        //恢复原有值
                        GridTable.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = Convert.ToString(obj);
                        return;
                    }
                    //如果和原来的值相同，返回
                    if (Convert.ToString(obj) == strNewValue) return;
                    try
                    {
                        feature.set_Value(nField, (object)strNewValue);
                        feature.Store();
                    }
                    catch (System.Exception ex)
                    {
                        //恢复原有值
                        GridTable.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = Convert.ToString(obj);
                    }

                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
                 
            
        }

        private void GridTable_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        /// <summary>
        /// 改变字段值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GridTable_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {

        }

        /// <summary>
        /// 添加字段
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddField_Click(object sender, EventArgs e)
        {
            ILayer layer = ClsGDBDataCommon.GetLayerFromName(axMapCtlMain.Map, barAttributeTable.Text);
            if (layer is IFeatureLayer)
            {               
                IFeatureLayer featureLayer = layer as IFeatureLayer;
                IEngineEditLayers editLayers = m_EngineEditor as IEngineEditLayers;
                if (editLayers.IsEditable(featureLayer))
                {
                    MessageBox.Show("请先停止编辑图层");
                    return;
                }

                FrmAddField frm = new FrmAddField(featureLayer);
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    if (frm.AddField(frm.m_strFieldName) >=0)
                    {
                        m_AttributeTable.ReloadAttributeTable();
                    } 
                }
            }
        }

        /// <summary>
        /// 删除字段
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btndelectfield_Click(object sender, EventArgs e)
        {
            ILayer layer = ClsGDBDataCommon.GetLayerFromName(axMapCtlMain.Map, barAttributeTable.Text);
            if (layer is IFeatureLayer)
            {
                IFeatureLayer featureLayer = layer as IFeatureLayer;
                IEngineEditLayers editLayers = m_EngineEditor as IEngineEditLayers;
                if (editLayers.IsEditable(featureLayer))
                {
                    MessageBox.Show("请先停止编辑图层");
                    return;
                }

                FrmDelField frm = new FrmDelField(featureLayer);
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    if (frm.DeleteField())
                    {
                        m_AttributeTable.ReloadAttributeTable();
                    }
                }
            }
            #endregion
        }


        private void GridTable_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (btnshowallout.Checked)
            {
                if (e.RowIndex >= 0)
                {
                    //if (!m_bShiftOrCtrl)
                    //{
                        //清除表格选择特性
                        ClearGridTableSelection(GridTable);
                        //清除图层选择集
                        axMapCtlMain.Map.ClearSelection();
                    //}
                    DataGridViewRow row = GridTable.Rows[e.RowIndex];
                    row.Selected = true;
                }
            }
        }
        /// <summary>
        /// gridview选中行发生改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GridTable_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (btnshowallout.Checked)
            {
                TableSelectionChange();
            }

        }

        /// <summary>
        /// 显示所有记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnshowall_Click(object sender, EventArgs e)
        {
            btnshowallout_Click(null, null);
        }

        /// <summary>
        /// 显示选中记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnshowsel_Click(object sender, EventArgs e)
        {
            btnshowselout_Click(null, null);

        }

        /// <summary>
        /// 显示所有记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnshowallout_Click(object sender, EventArgs e)
        {
            btnshowallout.Checked = true;
            btnshowselout.Checked = false;

            //RefreshGridTable(GridTable);
            ILayer pLayer = ClsGDBDataCommon.GetLayerFromName(axMapCtlMain.Map, barAttributeTable.Text);
            if (pLayer == null) return;
            SetGridTableSelection(GridTable, pLayer);
            RefreshGridTable(GridTable);
        }

        /// <summary>
        /// 显示选中记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnshowselout_Click(object sender, EventArgs e)
        {
            btnshowallout.Checked = false;
            btnshowselout.Checked = true;

            RefreshGridTable(GridTable);
        }

        private void barAttributeTable_VisibleChanged(object sender, EventArgs e)
        {

        }

       

#endregion

        /// <summary>
        /// 导出属性表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExport_Click(object sender, EventArgs e)
        {
            ExportToExcel exportexcel = new ExportToExcel();
            exportexcel.ExportDataGridViewToExcel(this.GridTable);
        }

        /// <summary>
        /// 根据属性选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnselectras_Click(object sender, EventArgs e)
        {
             ILayer pLayer = ClsGDBDataCommon.GetLayerFromName(axMapCtlMain.Map, barAttributeTable.Text);
            if (pLayer == null) return;

            if (pLayer is IFeatureLayer)
            {
                //string strquery = "";
                FrmSelByAttribute frm = new FrmSelByAttribute(axMapCtlMain,pLayer);
                frm.ShowDialog();
            }
            else if (pLayer is IRasterLayer)
            {
            }
        }

        /// <summary>
        /// 字段计算
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFieldCalculator_Click(object sender, EventArgs e)
        {
            ILayer pLayer = ClsGDBDataCommon.GetLayerFromName(axMapCtlMain.Map, barAttributeTable.Text);
            if (pLayer != null)
            {
                axTOCCtlLayer.Update();
                axTOCCtlLayer.SelectItem(pLayer, 0);
            }

            if (pLayer is IFeatureLayer)
            {
                FrmFiledCalculator frm = new FrmFiledCalculator(pLayer);
                frm.ShowDialog();
                m_AttributeTable.ReloadAttributeTable();
            }
        }

        private void buttonItemCreateShapeFile_Click(object sender, EventArgs e)
        {
            FrmCreateShapeFile fsf = new FrmCreateShapeFile((IMapControl3)axMapCtlMain.Object, (IMapControl3)axMapControlHide.Object);
            fsf.ShowDialog();
        }



        private void cmbAdjustMethod_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dockMap.Selected ==false)
            {
                dockMap.Selected = true;
                axMapCtlMain.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
            }
            if (cmbAdjustMethod.SelectedItem == cbiAffine)
            {
                m_Adjust.m_AdjustMehod = CmdAdjust.AdjustMethod.AdjustMethod_Affine;
            }

            if (cmbAdjustMethod.SelectedItem == cbiProjective)
            {
                m_Adjust.m_AdjustMehod = CmdAdjust.AdjustMethod.AdjustMethod_Projective;
            }

            if (cmbAdjustMethod.SelectedItem == cbiSimilarity)
            {
                m_Adjust.m_AdjustMehod = CmdAdjust.AdjustMethod.AdjustMethod_Similarity;
            }

            return;
        }

        private void cmbAdjustMethod_DropDown(object sender, EventArgs e)
        {

        }

        #region 三维分析

        private void btn_surfaceop_Click(object sender, EventArgs e)
        {
            //FrmSurfaceOp frm = new FrmSurfaceOp((IMapControl3)axMapCtlMain.Object);
            //frm.ShowDialog();
        }

        private void btnillum_Click(object sender, EventArgs e)//光照
        {
            if (barMain.SelectedDockTab == 2 && this.axSceneCtlMain.Scene != null)
            {
                FrmIllumination illum = new FrmIllumination(this.axSceneCtlMain);
                illum.StartPosition = FormStartPosition.CenterScreen;
                illum.ShowDialog();
            }
            else
            {
                MessageBox.Show("请生成三维场景并切换到三维视图", "提示", MessageBoxButtons.OK);
            }
        }

        private void btnprofile_Click(object sender, EventArgs e)//剖面
        {

            if (dockMap.Selected == true)
            {
                //ISelection pSelection = this.axMapCtlMain.Map.FeatureSelection;

                //int i = 0;
                //IEnumFeature pEnumFeature = pSelection as IEnumFeature;
                //IEnumFeatureSetup m_EnumFeatureSetup = pEnumFeature as IEnumFeatureSetup;
                //m_EnumFeatureSetup.AllFields = true;
                //pEnumFeature.Reset();
                //IFeature pFeature = pEnumFeature.Next();
                //if (pFeature != null)
                //{
                //    while (pFeature != null)
                //    {
                //        i++;
                //        pFeature = pEnumFeature.Next();
                //    }
                //    if (i == 1)
                //    {
                //        pEnumFeature.Reset();
                //        IFeature mFeature = pEnumFeature.Next();
                //        Frmprofile frmprofile = new Frmprofile(mFeature, (IMapControl3)axMapCtlMain.Object);
                //        frmprofile.ShowInTaskbar = false;
                //        frmprofile.StartPosition = FormStartPosition.CenterScreen;
                //        frmprofile.ShowDialog();
                //    }
                //    else
                //    {
                //        MessageBox.Show("只能选中一条线路进行剖面分析", "提示", MessageBoxButtons.OK);
                //    }
                //}
                //else
                //{
                //    MessageBox.Show("请先选中一条路线", "提示", MessageBoxButtons.OK);
                //}
            }
            else
            {
                MessageBox.Show("请切换到地图窗口", "提示", MessageBoxButtons.OK);
            }
        }

        #endregion

        private void dotNetBarManagerMain_ItemClick(object sender, EventArgs e)
        {

        }

        #region 视图工具

        private void btnbarleft_CheckedChanged(object sender, EventArgs e)
        {
            if (btnbarleft.Checked == true)
            {
                barLeft.Show();
                int nItemCount = barLeft.Items.Count;
                for (int i = 0; i < nItemCount; i++)
                {
                    barLeft.Items[i].Visible = true;
                }
                barLeft.Refresh();
            }
            else
            {
                barLeft.Visible = false;
            }
        }

        private void btnbarright_CheckedChanged(object sender, EventArgs e)
        {
            //if (btnbarright.Checked == true)
            //{
            //    barRight.Show();
            //    int nItemCount = barRight.Items.Count;
            //    for (int i = 0; i < nItemCount; i++)
            //    {
            //        barRight.Items[i].Visible = true;
            //    }
            //    barRight.Refresh();
            //}
            //else
            //{
            //    barRight.Visible = false;
            //}
        }

        private void btnmap_CheckedChanged(object sender, EventArgs e)
        {
            if (btnmap.Checked == true)
            {
                //barMain.SelectedDockTab = 0;
                barMain.SelectedDockContainerItem = this.dockMap;
                btnlayout.Checked = false;
                btn3d.Checked = false;
            }
        }

        private void btnlayout_CheckedChanged(object sender, EventArgs e)
        {
            if (btnlayout.Checked == true)
            {
                //barMain.SelectedDockTab = 1;
                barMain.SelectedDockContainerItem = this.dockLayout;
                btnmap.Checked = false;
                btn3d.Checked = false;
            }
        }

        private void btn3d_CheckedChanged(object sender, EventArgs e)
        {
            if (btn3d.Checked == true)
            {
                //barMain.SelectedDockTab = 2;
                barMain.SelectedDockContainerItem = this.dockScene;
                btnmap.Checked = false;
                btnlayout.Checked = false;
            }
        }

        private void barLeft_VisibleChanged(object sender, EventArgs e)
        {
            if (barLeft.Visible == false)
            {
                btnbarleft.Checked = false;
            }
        }

        private void barRight_VisibleChanged(object sender, EventArgs e)
        {
            if (barRight.Visible == false)
            {
                btnbarright.Checked = false;
            }
        }


        #endregion

        #region 三维快捷

        private void axSceneCtlMain_OnKeyDown(object sender, ISceneControlEvents_OnKeyDownEvent e)
        {
            if ((System.Windows.Forms.Control.ModifierKeys & Keys.Control) == Keys.Control)//Ctrl健被按下; 
            {
                if (e.keyCode == 38)
                {
                    ICamera pCamera = axSceneCtlMain.Camera;
                    pCamera.RollAngle = rolldegree;
                    rolldegree += 15;
                    axSceneCtlMain.Refresh();
                }

                else if (e.keyCode == 40)//下
                {
                    ICamera pCamera = axSceneCtlMain.Camera;
                    pCamera.RollAngle = rolldegree;
                    rolldegree += -15;
                    axSceneCtlMain.Refresh();
                }
                else if (e.keyCode == 37)//左
                {
                    ICamera pCamera = axSceneCtlMain.Camera;
                    pCamera.Rotate(15);
                    axSceneCtlMain.Refresh();
                }
                else if (e.keyCode == 39)//右
                {
                    ICamera pCamera = axSceneCtlMain.Camera;
                    pCamera.Rotate(-15);
                    axSceneCtlMain.Refresh();
                }
            }
            ////else if ((System.Windows.Forms.Control.ModifierKeys & Keys.Shift) == Keys.Shift)//Shift键被按下
            ////{
            ////    if (e.keyCode == 38)//上
            ////    {
            ////        ICamera pCamera = axSceneCtlMain.Camera;
            ////        pCamera.Move(esriCameraMovementType.esriCameraMoveUp, 0.005);
            ////        axSceneCtlMain.Refresh();
            ////    }
            ////    else if (e.keyCode == 40)//下
            ////    {
            ////    }
            ////    else if (e.keyCode == 37)//左
            ////    {
            ////    }
            ////    else if (e.keyCode == 39)//右
            ////    {
            ////    }
            ////}
            else
            {
                if (e.keyCode == 38)//上
                {
                    //ICamera pCamera = axSceneCtlMain.Camera;
                    //IPoint pSPoint = pCamera.Observer;
                    //IPoint pEPoint = new PointClass();
                    ////IZAware pZaware = pEPoint as IZAware;
                    ////pZaware.ZAware = true;
                    ////pEPoint.Z = pSPoint.Z + pSPoint.Z * 0.05;
                    //pEPoint.Y = pSPoint.Y+5;
                    //pEPoint.X = pSPoint.X+5;
                    //pCamera.Pan(pSPoint, pEPoint);
                    //axSceneCtlMain.Refresh();
                    ICamera pCamera = axSceneCtlMain.Camera;
                    pCamera.Move(esriCameraMovementType.esriCameraMoveUp, 0.005);
                    axSceneCtlMain.Refresh();
                }
                else if (e.keyCode == 40)//下
                {
                    ICamera pCamera = axSceneCtlMain.Camera;
                    pCamera.Move(esriCameraMovementType.esriCameraMoveDown, 0.005);
                    axSceneCtlMain.Refresh();
                }
                else if (e.keyCode == 37)//左
                {
                    ICamera pCamera = axSceneCtlMain.Camera;
                    pCamera.Move(esriCameraMovementType.esriCameraMoveLeft, 0.005);
                    axSceneCtlMain.Refresh();
                }
                else if (e.keyCode == 39)//右
                {
                    ICamera pCamera = axSceneCtlMain.Camera;
                    pCamera.Move(esriCameraMovementType.esriCameraMoveRight, 0.005);
                    axSceneCtlMain.Refresh();
                }
            }
            //}
        }

        private void axSceneCtlMain_OnKeyUp(object sender, ISceneControlEvents_OnKeyUpEvent e)
        {
            //if ((System.Windows.Forms.Control.ModifierKeys & Keys.Control) != Keys.Control)//Ctrl健被按下; 
            //{
            //    if (e.keyCode == 38)
            //    {
            //        ICamera pCamera = axSceneCtlMain.Camera;
            //        pCamera.RollAngle = rolldegree;
            //        rolldegree += 15;
            //        axSceneCtlMain.Refresh();
            //    }

            //    else if (e.keyCode == 40)//下
            //    {
            //        ICamera pCamera = axSceneCtlMain.Camera;
            //        pCamera.RollAngle = rolldegree;
            //        rolldegree += -15;
            //        axSceneCtlMain.Refresh();
            //    }
            //    else if (e.keyCode == 37)//左
            //    {
            //        ICamera pCamera = axSceneCtlMain.Camera;
            //        pCamera.Rotate(15);
            //        axSceneCtlMain.Refresh();
            //    }
            //    else if (e.keyCode == 39)//右
            //    {
            //        ICamera pCamera = axSceneCtlMain.Camera;
            //        pCamera.Rotate(-15);
            //        axSceneCtlMain.Refresh();
            //    }
            //}
            //else if ((System.Windows.Forms.Control.ModifierKeys & Keys.Shift) == Keys.Shift)
            //{
            //}
        }


        //点转线
        private void buttonItemPointToLine_Click(object sender, EventArgs e)
        {
            IMapControl3 pMapcontrol = axMapCtlMain.Object as IMapControl3;
            LibCerMap.FrmPointToLine frm = new FrmPointToLine(pMapcontrol);
            frm.ShowDialog();
        }

        //private void panmap( double ratioy)
        //{
        //    IScene pScene = axSceneCtlMain.Scene;
        //    IEnvelope pEnvelop = pScene.Extent;
        //    double height = pEnvelop.Height;
        //    double width = pEnvelop.Width;
        //    ////pEnvelop.Offset(width * ratiox, height * ratioy);
        //    //pEnvelop.XMin = pEnvelop.XMin + width * ratiox;
        //    //pEnvelop.ZMin = pEnvelop.YMin + height * ratioy;
        //    //pEnvelop.XMax = pEnvelop.XMax + width * ratiox;
        //    //pEnvelop.ZMin = pEnvelop.YMax + height * ratioy;
        //    //axSceneCtlMain.Refresh();

        //    ICamera pCamera = axSceneCtlMain.Camera;
        //    //IPoint pPoint = pCamera.Observer;
        //    //IPoint nPoint = new PointClass();
        //    //nPoint.X = pPoint.X + width * ratiox;
        //    //nPoint.Y = pPoint.Y + height * ratioy;
        //    //pCamera.Observer = nPoint;
        //    //axSceneCtlMain.Refresh();

        //    pCamera.Move(esriCameraMovementType.esriCameraMoveUp, ratioy);
        //    axSceneCtlMain.Refresh();
        //}

        #endregion

        private void dockScene_GotFocus(object sender, EventArgs e)
        {
            axTOCCtlLayer.SetBuddyControl(axSceneCtlMain.Object);
        }

        private void dockScene_VisibleChanged(object sender, EventArgs e)
        {
            ;
        }

        private void btn_RasterClip_Click(object sender, EventArgs e)
        {
            FrmRasterSubset f = new FrmRasterSubset((IMapControl3)axMapCtlMain.Object);
            f.ShowDialog();
        }

        private bool calcAzimuAndElev(double dImgPtX, double dImgPtY, ref ClsPointInfo ptInfo)
        {
            if (ptInfo == null || ptInfo.cameraPara == null)
                return false;

            double dPtX, dPtY;
            dPtX = dImgPtX - ptInfo.cameraPara.dPx;
            dPtY = ptInfo.cameraPara.dPy - dImgPtY;

            OriAngle oriAngle = new OriAngle();
            oriAngle.kap = ptInfo.cameraPara.dKappa;
            oriAngle.omg = ptInfo.cameraPara.dOmg;
            oriAngle.phi = ptInfo.cameraPara.dPhi;

            Matrix pRMat = new Matrix(3, 3);
            ClsGetCameraView.OPK2RMat(oriAngle, ref pRMat);

            double dVecX, dVecY, dVecZ;
            dVecX = pRMat.getNum(0, 0) * dPtX + pRMat.getNum(0, 1) * dPtY - pRMat.getNum(0, 2) * ptInfo.cameraPara.dFocus;
            dVecY = pRMat.getNum(1, 0) * dPtX + pRMat.getNum(1, 1) * dPtY - pRMat.getNum(1, 2) * ptInfo.cameraPara.dFocus;
            dVecZ = pRMat.getNum(2, 0) * dPtX + pRMat.getNum(2, 1) * dPtY - pRMat.getNum(2, 2) * ptInfo.cameraPara.dFocus;

            double dS = Math.Sqrt(dVecX * dVecX + dVecY * dVecY + dVecZ * dVecZ);

            ptInfo.dbAngleHor = Math.Atan2(dVecY, dVecX);
            if (ptInfo.dbAngleHor < 0)
            {
                ptInfo.dbAngleHor += 2 * Math.PI;
            }

            ptInfo.dbAngleHor = ptInfo.dbAngleHor / Math.PI * 180;
            ptInfo.dbAngleVer = -1.0 * Math.Asin(dVecZ / dS);
            ptInfo.dbAngleVer = ptInfo.dbAngleVer / Math.PI * 180;

            return true;
        }

        private void axToolbarControlLayout_OnItemClick(object sender, IToolbarControlEvents_OnItemClickEvent e)
        {
            //当打开或保存地图文档时需要将地图文档置前
            IToolbarItem item = ((AxToolbarControl)sender).GetItem(e.index);
            if (item.Command.Name == "CustomCE.CmdOpenMxdDoc" || item.Command.Name == "CustomCE.CmdSaveMxdDoc")
            {
                dockLayout.Selected = true;
                SetCurrentTool(axToolbarControlLayout, "esriControls.ControlsSelectTool");
                m_controlsSynchronizer.m_pageLayoutActiveTool = axToolbarControlLayout.CurrentTool;
            }
        }

        #region 地图控件右键菜单

        private void axMapCtlMain_OnMouseDown(object sender, IMapControlEvents2_OnMouseDownEvent e)
        {
            ///进入太阳高度角量测模式
            if (this.axMapCtlMain.CurrentTool is ToolSunAltCheckState)
            {
                IMapControl2 mapctrl = axMapCtlMain.Object as IMapControl2;
                IPoint point = mapctrl.ToMapPoint(e.x, e.y);
                IRaster2 pRaster2 = m_toolSunAltCheckState.m_pLayer.Raster as IRaster2;

                int nColumn = -1;
                int nRow = -1;
                pRaster2.MapToPixel(point.X, point.Y, out nColumn, out nRow);

                IRasterProps pProps = m_toolSunAltCheckState.m_pLayer.Raster as IRasterProps;
                IEnvelope pEnv = pProps.Extent;
                if (e.mapX >= pEnv.XMin && e.mapX <= pEnv.XMax && e.mapY >= pEnv.YMin && e.mapY <= pEnv.YMax)
                {
                    ClsPointInfo ptInfo = new ClsPointInfo();
                    ptInfo.nPointID = m_SunAltPointCollection.Count + 1;
                    ptInfo.nImageX = nColumn;
                    ptInfo.nImageY = nRow;

                    ptInfo.cameraPara = m_cameraPara;
                    if (calcAzimuAndElev(nColumn, nRow, ref ptInfo))//ptInfo.nImageX, ptInfo.nImageY, ref ptInfo))
                    {
                        m_SunAltPointCollection.Add(ptInfo);

                        DrawSunAltPoints();

                        //m_SunAltPointCollection[m_SunAltPointCollection.Count-1].nImageX=nColumn;
                        //m_SunAltPointCollection[m_SunAltPointCollection.Count-1].nImageY=nRow;
                        this.m_cmdSunAltProperty.m_frmSunAltitude.updateInfoList();
                    }
                }
            }

            if (m_cmdRasterMeaAtt.isMeaAtt)
            {
                //SetCurrentTool(axToolbarControlCommon, "esriControls.ControlsNoneTool");
                pGraphiccsContainer = axMapCtlMain.Map as IGraphicsContainer;
                pGraphiccsContainer.DeleteAllElements();
                pPolygonMeaAtt = axMapCtlMain.TrackPolygon();
                isMeaAttStart = true;
                m_cmdRasterMeaAtt.isMeaAtt = false;

            }
           
        }

        private void axMapCtlMain_OnMouseUp(object sender, IMapControlEvents2_OnMouseUpEvent e)
        {
            //右键
            if (e.button == 2)
            {
                IMap pMap = axMapCtlMain.Map;
                if (m_EngineEditor.Map == pMap && m_EngineEditor.EditState == esriEngineEditState.esriEngineStateEditing)
                {
                    IEngineEditTask pTask = m_EngineEditor.CurrentTask;
                    if (pTask.UniqueName == "ControlToolsEditing_ModifyFeatureTask" ||
                         pTask.UniqueName == "ControlToolsEditing_CreateNewFeatureTask")
                    {
                        m_EngineEditSketch.SetEditLocation(e.x, e.y);//必须设置此属性Insert Vertex才能用
                        m_menuEditor.PopupMenu(e.x, e.y, axMapCtlMain.hWnd);//编辑草图状态下右键菜单
                    }
                }
                else
                {
                    System.Drawing.Point p = new System.Drawing.Point();
                    p.X = e.x;
                    p.Y = e.y;
                    if (axMapCtlMain.CurrentTool  is ToolRasterEdit )
                    {
                        return;
                    }
                    contextMenuMap.Show(axMapCtlMain, p);//常规地图菜单
                }
            }


        }

        private void toolzoomin_Click(object sender, EventArgs e)//放大
        {
            SetCurrentTool(axToolbarControlCommon, "esriControls.ControlsMapZoomInTool");
        }

        private void toolzoomout_Click(object sender, EventArgs e)//缩小
        {
            SetCurrentTool(axToolbarControlCommon, "esriControls.ControlsMapZoomOutTool");
        }

        private void menuMapPopupSelect_Click(object sender, EventArgs e)//选择
        {
            SetCurrentTool(axToolbarControlCommon, "esriControls.ControlsSelectTool");
        }

        private void toolpan_Click(object sender, EventArgs e)//漫游
        {
            SetCurrentTool(axToolbarControlCommon, "esriControls.ControlsMapPanTool");
        }

        private void toolextent_Click(object sender, EventArgs e)//全图
        {
            ITool TempTool = this.axMapCtlMain.CurrentTool;
            ESRI.ArcGIS.SystemUI.ICommand pCommand = new ESRI.ArcGIS.Controls.ControlsMapFullExtentCommandClass();
            pCommand.OnCreate(this.axMapCtlMain.Object);
            pCommand.OnClick();
            double size = this.axMapCtlMain.MapScale;

            this.axMapCtlMain.CurrentTool = TempTool;
        }

        private void toolzoominfixed_Click(object sender, EventArgs e)//中心放大
        {
            ITool TempTool = this.axMapCtlMain.CurrentTool;

            ESRI.ArcGIS.SystemUI.ICommand pCommand = new ESRI.ArcGIS.Controls.ControlsMapZoomInFixedCommandClass();
            pCommand.OnCreate(this.axMapCtlMain.Object);
            pCommand.OnClick();

            this.axMapCtlMain.CurrentTool = TempTool;
        }

        private void toolzoomoutfixed_Click(object sender, EventArgs e)//中心缩小
        {
            ITool TempTool = this.axMapCtlMain.CurrentTool;

            ESRI.ArcGIS.SystemUI.ICommand pCommand = new ESRI.ArcGIS.Controls.ControlsMapZoomOutFixedCommandClass();
            pCommand.OnCreate(this.axMapCtlMain.Object);
            pCommand.OnClick();

            this.axMapCtlMain.CurrentTool = TempTool;
        }

        private void toolpreview_Click(object sender, EventArgs e)//前一视图
        {
            IExtentStack viewStack = null;
            viewStack = axMapCtlMain.ActiveView.ExtentStack;
            if (viewStack.CanUndo())
            {
                viewStack.Undo();
                axMapCtlMain.Refresh();
                axMapCtlMain.MousePointer = esriControlsMousePointer.esriPointerDefault;
            }
            else
            {
                DevComponents.DotNetBar.MessageBoxEx.Show("这已是最前视图");
            }
        }

        private void toolnextview_Click(object sender, EventArgs e)//后一视图
        {
            IExtentStack viewStack = null;
            viewStack = axMapCtlMain.ActiveView.ExtentStack;
            if (viewStack.CanRedo())
            {
                viewStack.Redo();
                axMapCtlMain.Refresh();
                axMapCtlMain.MousePointer = esriControlsMousePointer.esriPointerDefault;
            }
            else
            {
                DevComponents.DotNetBar.MessageBoxEx.Show("这已是最后视图");
            }
        }

        private void toolselset_Click(object sender, EventArgs e)//选择集
        {
            List<string> LayerName = new List<string>();
            ISelection pSelection = axMapCtlMain.Map.FeatureSelection;

            IEnumFeature pEnumFeature = pSelection as IEnumFeature;
            IEnumFeatureSetup m_EnumFeatureSetup = pEnumFeature as IEnumFeatureSetup;
            m_EnumFeatureSetup.AllFields = true;
            pEnumFeature.Reset();
            IFeature pFeature = pEnumFeature.Next();
            if (pFeature != null)
            {
                while (pFeature != null)
                {
                    string strlayerName = pFeature.Class.AliasName;
                    if (LayerName.Contains(strlayerName) == false)
                    {
                        LayerName.Add(strlayerName);
                    }
                    pFeature = pEnumFeature.Next();
                }
                FrmSelectSet frmselset = new FrmSelectSet(LayerName, this.axMapCtlMain);
                frmselset.StartPosition = FormStartPosition.CenterScreen;
                frmselset.ShowDialog();
            }
            else
            {
                DevComponents.DotNetBar.MessageBoxEx.Show("没有任何选中要素");
            }
        }

        #endregion



        private void btn_CreateFromXML_Click(object sender, EventArgs e)
        {
            //FrmFromXML f = new FrmFromXML();
            //f.m_pMapCtl = axMapCtlMain;
            //f.m_pTOCCtl = axTOCCtlLayer;
            //f.ShowDialog();
        }

        #region 位置矢量====地球矢量、太阳矢量

        private void btnvector_Click(object sender, EventArgs e)
        {
            IMapControl2 pMapcontrol = (IMapControl2)axMapCtlMain.Object;

            IPageLayoutControl3 pPageLayoutControl = (IPageLayoutControl3)axPageLayoutCtlMain.Object;
            ISceneControl pSceneControl = (ISceneControl)axSceneCtlMain.Object;


            FrmVertor frmVertor = new FrmVertor(pPageLayoutControl, pSceneControl);
            frmVertor.ShowDialog();
        }

        #endregion


        //路径单元规划XML解析
        private void btn_CreateLabel_Click(object sender, EventArgs e)
        {
            //FrmCreateLabel f = new FrmCreateLabel();
            //f.m_pMapCtl = axMapCtlMain;
            //f.ShowDialog();
        }

        private void axToolbarControlScene_OnMouseDown(object sender, IToolbarControlEvents_OnMouseDownEvent e)
        {

        }

        private void btntoolmatrix_Click(object sender, EventArgs e)
        {
            if (axMapCtlMain.Map.LayerCount == 0)
            {
                MessageBox.Show("目前没有可转换图层，请先打开一张矢量或栅格图层", "提示", MessageBoxButtons.OK);
            }
            else
            {
                FrmMatrix frmMatrix = new FrmMatrix((IMapControl3)axMapCtlMain.Object);
                frmMatrix.ShowDialog();
            }
        }

        //地平线高度角测量
        private void btnMenuAmizuth_Click(object sender, EventArgs e)
        {
            LibCerMap.FrmSunAltitude frm = new FrmSunAltitude((IMapControl3)axMapCtlMain.Object);
            frm.Show();
        }

        //private void buttonItem4_Click(object sender, EventArgs e)
        //{
        //    ClsGetCameraView getCameraView = new ClsGetCameraView();

        //    String szFilename = "";
        //    OpenFileDialog ofDialog=new OpenFileDialog();
        //    if (DialogResult.OK == ofDialog.ShowDialog())
        //    {
        //        szFilename = ofDialog.FileName;
        //    }            

        //    //IRasterWorkspace2 rw=getCameraView.OpenRasterWorkspace(System.IO.Path.GetDirectoryName(szFilename));
        //    //IRasterDataset pDataset=rw.OpenRasterDataset(System.IO.Path.GetFileName(szFilename));
        //    IRasterLayer pLayer=new RasterLayerClass();
        //    pLayer.CreateFromFilePath(szFilename);

        //    IRaster pSrcRaster=pLayer.Raster;
        //    IRaster pDstRaster=null;
        //    ExOriPara pExOriPara=new ExOriPara();
        //    pExOriPara.ori.omg = -1.88163966;
        //    pExOriPara.ori.phi = -0.1480308175;
        //    pExOriPara.ori.kap = 6.207224487;
        //    pExOriPara.pos.X = 161.32276;
        //    pExOriPara.pos.Y = 168.909783;
        //    pExOriPara.pos.Z = -1.349185;

        //    IRasterProps pProps = pSrcRaster as IRasterProps;
        //    int nWidth = 1024;// pProps.Width;
        //    int nHeight = 1024;// pProps.Height;

        //    int fx=nWidth/2;
        //    int fy=nHeight/2;
        //    double dbFocus = 1226.23;

        //    Point2D[,] ptResult = null;
        //    if (getCameraView.ImageReprojectionRange(pSrcRaster, out ptResult, pExOriPara, dbFocus, fx, fy, nWidth, nHeight, 50))
        //    {
        //        StreamWriter sw = new StreamWriter("C:\\Users\\syw\\Desktop\\testPoints.txt");
        //        for (int i = 0; i < ptResult.GetLength(0); i++)
        //        {
        //            for (int j = 0; j < ptResult.GetLength(1); j++)
        //            {
        //                if(ptResult[i,j]!=null)
        //                    sw.Write(ptResult[i, j].X + "," + ptResult[i, j].Y + "\n");
        //            }                    
        //        }

        //        sw.Close();
        //        MessageBox.Show("Ok");
        //    }
        //}

        private void axPageLayoutCtlMain_OnFocusMapChanged(object sender, EventArgs e)
        {            
            try
            {
                //axMapCtlMain.Map = axPageLayoutCtlMain.ActiveView.FocusMap;
                IMap map = axPageLayoutCtlMain.ActiveView.FocusMap;
                m_controlsSynchronizer.ReplaceMap(map);

                m_controlsSynchronizer.ActivatePageLayout();
                SetCurrentTool(axToolbarControlLayout, "esriControls.ControlsSelectTool");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cmbSunAltOriImg_SelectedIndexChanged(object sender, EventArgs e)
        {
            string szLayerName = this.cmbSunAltOriImg.SelectedItem.ToString();
            if (!string.IsNullOrEmpty(szLayerName))
            {
                List<ILayer> pLayerList = ClsGDBDataCommon.GetLayersByName(axMapCtlMain.Map,szLayerName);
                for (int i = 0; i < pLayerList.Count; i++)
                {
                    if (pLayerList[i] is IRasterLayer)
                    {
                        if (m_toolSunAltCheckState.m_pLayer == null || !m_toolSunAltCheckState.m_pLayer.Equals(pLayerList[i] as IRasterLayer))
                        {
                            if (dockMap.Selected == false)
                            {
                                dockMap.Selected = true;
                                axMapCtlMain.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
                            }
                            //m_cmdSunAltProperty.m_frmSunAltitude.m_sunAltInfoList.Clear();
                            //axMapCtlMain.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
                            m_toolSunAltCheckState.m_pLayer = pLayerList[i] as IRasterLayer;

                            IRasterDataset pDataset = ((IRaster2)(m_toolSunAltCheckState.m_pLayer.Raster)).RasterDataset;
                            //IRasterDataset pDataset = m_toolSunAltCheckState.m_pLayer.Raster as IRasterDataset;

                            String szFilename = pDataset.CompleteName;
                            String szXmlFilenameLower = System.IO.Path.GetDirectoryName(szFilename) + "\\" + System.IO.Path.GetFileNameWithoutExtension(szFilename) + ".xml";
                            String szXmlFilenameUpper = System.IO.Path.GetDirectoryName(szFilename) + "\\" + System.IO.Path.GetFileNameWithoutExtension(szFilename) + ".XML";

                            bool bFlag = false;
                            m_cameraPara = ParseXmlFileToGetPara(szXmlFilenameLower);
                            if (m_cameraPara != null)
                            {
                                this.txtSunAltXmlFile.Text = szXmlFilenameLower;
                                bFlag = true;
                            }

                            if (!bFlag)
                            {
                                m_cameraPara = ParseXmlFileToGetPara(szXmlFilenameUpper);
                                if (m_cameraPara != null)
                                {
                                    this.txtSunAltXmlFile.Text = szXmlFilenameUpper;
                                    bFlag = true;
                                }
                            }

                            if (!bFlag)
                            {
                                this.txtSunAltXmlFile.Text = string.Empty;
                            }

                            //m_cmdSunAltProperty.m_frmSunAltitude.m_clsCameraPara = m_cameraPara;
                        }

                        break;
                        //axMapCtlMain.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
                    }
                }
            }
        }

        public ClsCameraPara ParseXmlFileToGetPara(string szFilename)
        {
            try
            {
                ClsCameraPara result = new ClsCameraPara();
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(szFilename);

                XmlNode cs = xmlDoc.SelectSingleNode(@"CameraPara/ExtPara/dX");
                if (cs != null)
                {
                    result.dX = double.Parse(cs.InnerText);
                }

                cs = xmlDoc.SelectSingleNode(@"CameraPara/ExtPara/dY");
                if (cs != null)
                {
                    result.dY = double.Parse(cs.InnerText);
                }

                cs = xmlDoc.SelectSingleNode(@"CameraPara/ExtPara/dZ");
                if (cs != null)
                {
                    result.dZ = double.Parse(cs.InnerText);
                }

                cs = xmlDoc.SelectSingleNode(@"CameraPara/ExtPara/dPhi");
                if (cs != null)
                {
                    result.dPhi = double.Parse(cs.InnerText);
                }

                cs = xmlDoc.SelectSingleNode(@"CameraPara/ExtPara/dOmg");
                if (cs != null)
                {
                    result.dOmg = double.Parse(cs.InnerText);
                }

                cs = xmlDoc.SelectSingleNode(@"CameraPara/ExtPara/dKappa");
                if (cs != null)
                {
                    result.dKappa = double.Parse(cs.InnerText);
                }

                cs = xmlDoc.SelectSingleNode(@"CameraPara/InePara/dFocus");
                if (cs != null)
                {
                    result.dFocus = double.Parse(cs.InnerText);
                }

                cs = xmlDoc.SelectSingleNode(@"CameraPara/InePara/dPx");
                if (cs != null)
                {
                    result.dPx = double.Parse(cs.InnerText);
                }

                cs = xmlDoc.SelectSingleNode(@"CameraPara/InePara/dPy");
                if (cs != null)
                {
                    result.dPy = double.Parse(cs.InnerText);
                }

                cs = xmlDoc.SelectSingleNode(@"CameraPara/InePara/nW");
                if (cs != null)
                {
                    result.nW = int.Parse(cs.InnerText);
                }

                cs = xmlDoc.SelectSingleNode(@"CameraPara/InePara/nH");
                if (cs != null)
                {
                    result.nH = int.Parse(cs.InnerText);
                }

                cs = xmlDoc.SelectSingleNode(@"CameraPara/InePara/szImageName");
                if (cs != null)
                {
                    result.szImageName = cs.InnerText;
                }

                return result;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }

            return null;
        }

        private void txtSunAltXmlFile_TextChanged(object sender, EventArgs e)
        {
            m_toolSunAltCheckState.m_szXmlName = this.txtSunAltXmlFile.Text;
            m_cameraPara = ParseXmlFileToGetPara(this.txtSunAltXmlFile.Text);
            //if(m_cameraPara!=null)
            //    m_cmdSunAltProperty.m_frmSunAltitude.m_clsCameraPara = m_cameraPara;
        }

        private void btnSetXmlFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofDialog = new OpenFileDialog();
            ofDialog.Filter = "XML文件(*.xml)|*.XML;*.xml|所有文件(*.*)|*.*";

            if (DialogResult.OK == ofDialog.ShowDialog())
            {
                if (System.IO.Path.GetExtension(ofDialog.FileName).ToUpper() == ".XML")
                {
                    this.txtSunAltXmlFile.Text = ofDialog.FileName;
                    m_cameraPara = ParseXmlFileToGetPara(this.txtSunAltXmlFile.Text);
                    //if (m_cameraPara != null)
                    //    m_cmdSunAltProperty.m_frmSunAltitude.m_clsCameraPara = m_cameraPara;
                }
            }
        }
        private void axToolbarControlScene_OnItemClick(object sender, IToolbarControlEvents_OnItemClickEvent e)
        {
            IToolbarItem item = ((AxToolbarControl)sender).GetItem(e.index);
            if (item.Command.Name == "CustomCE.CmdCreateScene")
            {
                dockScene.Selected = true;
            }
        }

        private void cmbSunAltOriImg_DropDown(object sender, EventArgs e)
        {

            //object tmpObj = cmbSunAltOriImg.SelectedItem;
            //cmbSunAltOriImg.Items.Clear();

            //IMap pMap = axMapCtlMain.Map;
            //for (int i = 0; i < pMap.LayerCount; i++)
            //{
            //    ILayer pLayer = pMap.get_Layer(i);
            //    if (pLayer is IRasterLayer)
            //    {
            //        //CmbSunAlt
            //        cmbSunAltOriImg.Items.Add(pLayer.Name);
            //    }
            //}

            //bool bFlag = false;
            //for (int i = 0; i < cmbSunAltOriImg.Items.Count; i++)
            //{
            //    if (cmbSunAltOriImg.Items[i] == tmpObj)
            //    {
            //        cmbSunAltOriImg.SelectedItem = tmpObj;
            //        bFlag = true;
            //        break;
            //    }
            //}

            //if (!bFlag)
            //    if (cmbSunAltOriImg.Items.Count > 0)
            //        cmbSunAltOriImg.SelectedIndex = 0;
        }
        #region 图层效果
        private void sliderItemTransparency_ValueChanged(object sender, EventArgs e)
        {

            if (pLayerEffects is IFeatureLayer)
            {
                ILayerEffects pLEffects = new FeatureLayerClass();
                IFeatureLayer pFLayer = pLayerEffects as IFeatureLayer;
                pLEffects = pFLayer as ILayerEffects;
                pLEffects.SupportsInteractive = true;
                pLEffects.Transparency = (short)sliderItemTransparency.Value;    
            }
            else if (pLayerEffects is IRasterLayer)
            {
                ILayerEffects pLEffects = new RasterLayerClass();
                IRasterLayer pRLayer = pLayerEffects as IRasterLayer;
                pLEffects = pRLayer as ILayerEffects;
                pLEffects.SupportsInteractive = true;
                pLEffects.Transparency = (short)sliderItemTransparency.Value;
            }

            if (dockMap.Selected == true)
            {
                axMapCtlMain.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
            }
            if (dockLayout.Selected == true)
            {
                axPageLayoutCtlMain.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
            }
        }

        private void sliderItemContrast_ValueChanged(object sender, EventArgs e)
        {
            if (pLayerEffects is IRasterLayer)
            {
                ILayerEffects pLEffects = new RasterLayerClass();
                IRasterLayer pRLayer = pLayerEffects as IRasterLayer;
                pLEffects = pRLayer as ILayerEffects;
                pLEffects.SupportsInteractive = true;
                pLEffects.Contrast = (short)sliderItemContrast.Value;
                if (dockMap.Selected == true)
                {
                    axMapCtlMain.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
                }
                if (dockLayout.Selected == true)
                {
                    axPageLayoutCtlMain.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
                }
            }
        }

        private void sliderItemBrightness_ValueChanged(object sender, EventArgs e)
        {
            if (pLayerEffects is IRasterLayer)
            {
                ILayerEffects pLEffects = new RasterLayerClass();
                IRasterLayer pRLayer = pLayerEffects as IRasterLayer;
                pLEffects = pRLayer as ILayerEffects;
                pLEffects.SupportsInteractive = true;
                pLEffects.Brightness = (short)sliderItemBrightness.Value;

                if (dockMap.Selected == true)
                {
                    axMapCtlMain.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
                }
                if (dockLayout.Selected == true)
                {
                    axPageLayoutCtlMain.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
                }
            }
        }


        #endregion

        private void treeViewTemplate_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            DirectoryInfo dri = new DirectoryInfo(GetParentPathofExe() + @"Resource\MapTemplate");
            try
            {
                string filepath = dri.FullName + @"\" + e.Node.Text;

                dockLayout.Selected = true;
                ICommand command = new LibEngineCmd.CmdOpenMxdDoc(m_controlsSynchronizer, barAttributeTable, AttributeTable,filepath);
                command.OnCreate(axPageLayoutCtlMain.Object);

                command.OnClick();
                SetMainApplictationTitle();
                SetCurrentTool(axToolbarControlLayout, "esriControls.ControlsSelectTool");
              }
            catch (Exception ex)
            {
                //MessageBox.Show("打开地图文档失败", ex.Message);
            }
        }

        private void btn_CreateImagePath_Click(object sender, EventArgs e)
        {
            //FrmCreateImagePath f = new FrmCreateImagePath();
            //f.m_pMapCtl = axMapCtlMain;
            //f.ShowDialog();
        }

        private void axMapCtlMain_OnKeyUp(object sender, IMapControlEvents2_OnKeyUpEvent e)
        {

        }

        private void axSceneCtlMain_OnDoubleClick(object sender, ISceneControlEvents_OnDoubleClickEvent e)
        {
            FrmFullScreenScene frmscene = new FrmFullScreenScene(this.axSceneCtlMain);
            frmscene.ShowDialog();
        }

        private void btn_CreatParkPoint_Click(object sender, EventArgs e)//根据巡视器XML生成停泊点图层
        {
            FrmImportNeijiance FrmCPaPoint = new FrmImportNeijiance(this.axMapCtlMain, this.axTOCCtlLayer);
            FrmCPaPoint.ShowDialog();
        }

        //根据点图层字段生成太阳地球矢量
        private void menuToolVector_Click(object sender, EventArgs e)
        {
            LibCerMap.FrmOritationVector frm = new LibCerMap.FrmOritationVector((IMapControl3)axMapCtlMain.Object);
            frm.ShowDialog();
        }

        private void menuShpFromFile_Click(object sender, EventArgs e)
        {
            LibCerMap.FrmCreateShpFromFile frm = new FrmCreateShpFromFile((IMapControl3)axMapCtlMain.Object);
            frm.ShowDialog();
        }

        private void btnCreatGmpPoint_Click(object sender, EventArgs e)
        {
            FrmCreateGmpPoint frm = new FrmCreateGmpPoint(this.axMapCtlMain,this.axTOCCtlLayer);

            frm.ShowDialog();
        }

        #region  从模板地图导入符号

        private void btnMapSymbol_Click(object sender, EventArgs e)//从模板地图导入图层符号
        {
            try
            {

                if (axMapCtlMain.LayerCount != 0)
                {
                    string FilePath = "";
                    OpenFileDialog dlgMapSymbol = new OpenFileDialog();
                    dlgMapSymbol.Title = "选择单元规划模板地图";
                    dlgMapSymbol.InitialDirectory = ".";
                    dlgMapSymbol.Filter = "mxd文件(*.mxd)|*.mxd";
                    dlgMapSymbol.RestoreDirectory = true;
                    if (dlgMapSymbol.ShowDialog() == DialogResult.OK)
                    {
                        FilePath = dlgMapSymbol.FileName;
                    }
                    else
                    {
                        return;
                    }
                    IMapDocument pMapDocument = new MapDocumentClass();
                    pMapDocument.Open(FilePath, null);

                    IMap pMap = pMapDocument.Map[0];
                     //初始化图层列表
                    IEnumLayer pEnumLayer = axMapCtlMain.Map.get_Layers(null, true);
                    pEnumLayer.Reset();
                    ILayer pLayerWait = null;
                    while ((pLayerWait = pEnumLayer.Next()) != null)
                    {
                        if (pLayerWait is IFeatureLayer && pLayerWait.Name.Contains("_passpoint"))
                        {
                            for (int j = 0; j < pMap.LayerCount; j++)
                            {
                                ILayer pLayerSymbol = pMap.get_Layer(j);
                                if (pLayerSymbol is IFeatureLayer && pLayerSymbol.Name.Contains("_passpoint"))
                                {
                                    IFeatureLayer pFLayerSymbol = pLayerSymbol as IFeatureLayer;
                                    IGeoFeatureLayer pGFLayerSymbol = pFLayerSymbol as IGeoFeatureLayer;
                                    IFeatureLayer pFLayerWait = pLayerWait as IFeatureLayer;
                                    IGeoFeatureLayer pGFLayerWait = pFLayerWait as IGeoFeatureLayer;
                                    if (pGFLayerSymbol != null)
                                    {
                                        pGFLayerWait.Renderer = pGFLayerSymbol.Renderer;

                                        IAnnotateLayerProperties pAnnoLayerP = new LabelEngineLayerPropertiesClass();//符号图层标注
                                        IElementCollection PELECOLL;//queryitem的参数，用不到
                                        IElementCollection pelecoll;//同上
                                        pGFLayerSymbol.AnnotationProperties.QueryItem(0, out pAnnoLayerP, out  PELECOLL, out pelecoll);
                                        ILabelEngineLayerProperties pLabelEenLayPro = pAnnoLayerP as ILabelEngineLayerProperties;

                                        IFontDisp pFont = new StdFontClass() as IFontDisp;
                                        ITextSymbol pTextSymbol = new TextSymbolClass();
                                        pTextSymbol.Color = pLabelEenLayPro.Symbol.Color;
                                        pFont.Bold = pLabelEenLayPro.Symbol.Font.Bold;
                                        pFont.Italic = pLabelEenLayPro.Symbol.Font.Italic;
                                        pFont.Strikethrough = pLabelEenLayPro.Symbol.Font.Strikethrough;
                                        pFont.Underline = pLabelEenLayPro.Symbol.Font.Underline;
                                        pFont.Name = pLabelEenLayPro.Symbol.Font.Name;
                                        pFont.Size = pLabelEenLayPro.Symbol.Font.Size;
                                        pTextSymbol.Font = pFont;

                                        pGFLayerWait.AnnotationProperties.Clear();
                                        ILabelEngineLayerProperties pLabelWaitLayPro = new LabelEngineLayerPropertiesClass();//设置待渲染图层的标注
                                        pLabelWaitLayPro.Expression = pLabelEenLayPro.Expression;
                                        pLabelWaitLayPro.Symbol = pTextSymbol;

                                        IAnnotateLayerProperties pAnnLayProWait = pLabelWaitLayPro as IAnnotateLayerProperties;//新生成的符号图层设置标注
                                        pAnnLayProWait.DisplayAnnotation = true;
                                        pAnnLayProWait.FeatureLayer = pGFLayerWait;
                                        pAnnLayProWait.LabelWhichFeatures = esriLabelWhichFeatures.esriVisibleFeatures;
                                        pAnnLayProWait.WhereClause = "";
                                        pGFLayerWait.AnnotationProperties.Add(pAnnLayProWait);
                                        pGFLayerWait.DisplayAnnotation = true;
                                    }
                                }
                                //break;
                            }
                        }
                        else if (pLayerWait is IFeatureLayer && pLayerWait.Name.Contains("_passline"))
                        {
                            for (int j = 0; j < pMap.LayerCount; j++)
                            {
                                ILayer pLayerSymbol = pMap.get_Layer(j);
                                if (pLayerSymbol is IFeatureLayer && pLayerSymbol.Name.Contains("_passline"))
                                {
                                    IFeatureLayer pFLayerSymbol = pLayerSymbol as IFeatureLayer;
                                    IGeoFeatureLayer pGFLayerSymbol = pFLayerSymbol as IGeoFeatureLayer;
                                    IFeatureLayer pFLayerWait = pLayerWait as IFeatureLayer;
                                    IGeoFeatureLayer pGFLayerWait = pFLayerWait as IGeoFeatureLayer;
                                    if (pGFLayerSymbol != null)
                                    {
                                        pGFLayerWait.Renderer = pGFLayerSymbol.Renderer;

                                        IAnnotateLayerProperties pAnnoLayerP = new LabelEngineLayerPropertiesClass();//符号图层标注
                                        IElementCollection PELECOLL;//queryitem的参数，用不到
                                        IElementCollection pelecoll;//同上
                                        pGFLayerSymbol.AnnotationProperties.QueryItem(0, out pAnnoLayerP, out  PELECOLL, out pelecoll);
                                        ILabelEngineLayerProperties pLabelEenLayPro = pAnnoLayerP as ILabelEngineLayerProperties;

                                        IFontDisp pFont = new StdFontClass() as IFontDisp;
                                        ITextSymbol pTextSymbol = new TextSymbolClass();
                                        pTextSymbol.Color = pLabelEenLayPro.Symbol.Color;
                                        pFont.Bold = pLabelEenLayPro.Symbol.Font.Bold;
                                        pFont.Italic = pLabelEenLayPro.Symbol.Font.Italic;
                                        pFont.Strikethrough = pLabelEenLayPro.Symbol.Font.Strikethrough;
                                        pFont.Underline = pLabelEenLayPro.Symbol.Font.Underline;
                                        pFont.Name = pLabelEenLayPro.Symbol.Font.Name;
                                        pFont.Size = pLabelEenLayPro.Symbol.Font.Size;
                                        pTextSymbol.Font = pFont;

                                        pGFLayerWait.AnnotationProperties.Clear();
                                        ILabelEngineLayerProperties pLabelWaitLayPro = new LabelEngineLayerPropertiesClass();//设置待渲染图层的标注
                                        pLabelWaitLayPro.Expression = pLabelEenLayPro.Expression;
                                        pLabelWaitLayPro.Symbol = pTextSymbol;

                                        IAnnotateLayerProperties pAnnLayProWait = pLabelWaitLayPro as IAnnotateLayerProperties;//新生成的符号图层设置标注
                                        pAnnLayProWait.DisplayAnnotation = true;
                                        pAnnLayProWait.FeatureLayer = pGFLayerWait;
                                        pAnnLayProWait.LabelWhichFeatures = esriLabelWhichFeatures.esriVisibleFeatures;
                                        pAnnLayProWait.WhereClause = "";
                                        pGFLayerWait.AnnotationProperties.Add(pAnnLayProWait);
                                        pGFLayerWait.DisplayAnnotation = true;
                                    }
                                }
                                //break;
                            }
                        }
                        else if (pLayerWait is IFeatureLayer && pLayerWait.Name.Contains("_polylinelabel"))
                        {
                            for (int j = 0; j < pMap.LayerCount; j++)
                            {
                                ILayer pLayerSymbol = pMap.get_Layer(j);
                                if (pLayerSymbol is IFeatureLayer && pLayerSymbol.Name.Contains("_polylinelabel"))
                                {
                                    IFeatureLayer pFLayerSymbol = pLayerSymbol as IFeatureLayer;
                                    IGeoFeatureLayer pGFLayerSymbol = pFLayerSymbol as IGeoFeatureLayer;
                                    IFeatureLayer pFLayerWait = pLayerWait as IFeatureLayer;
                                    IGeoFeatureLayer pGFLayerWait = pFLayerWait as IGeoFeatureLayer;
                                    if (pGFLayerSymbol != null)
                                    {
                                        pGFLayerWait.Renderer = pGFLayerSymbol.Renderer;

                                        IAnnotateLayerProperties pAnnoLayerP = new LabelEngineLayerPropertiesClass();//符号图层标注
                                        IElementCollection PELECOLL;//queryitem的参数，用不到
                                        IElementCollection pelecoll;//同上
                                        pGFLayerSymbol.AnnotationProperties.QueryItem(0, out pAnnoLayerP, out  PELECOLL, out pelecoll);
                                        ILabelEngineLayerProperties pLabelEenLayPro = pAnnoLayerP as ILabelEngineLayerProperties;

                                        IFontDisp pFont = new StdFontClass() as IFontDisp;
                                        ITextSymbol pTextSymbol = new TextSymbolClass();
                                        pTextSymbol.Color = pLabelEenLayPro.Symbol.Color;
                                        pFont.Bold = pLabelEenLayPro.Symbol.Font.Bold;
                                        pFont.Italic = pLabelEenLayPro.Symbol.Font.Italic;
                                        pFont.Strikethrough = pLabelEenLayPro.Symbol.Font.Strikethrough;
                                        pFont.Underline = pLabelEenLayPro.Symbol.Font.Underline;
                                        pFont.Name = pLabelEenLayPro.Symbol.Font.Name;
                                        pFont.Size = pLabelEenLayPro.Symbol.Font.Size;
                                        pTextSymbol.Font = pFont;

                                        pGFLayerWait.AnnotationProperties.Clear();
                                        ILabelEngineLayerProperties pLabelWaitLayPro = new LabelEngineLayerPropertiesClass();//设置待渲染图层的标注
                                        pLabelWaitLayPro.Expression = pLabelEenLayPro.Expression;
                                        pLabelWaitLayPro.Symbol = pTextSymbol;

                                        IAnnotateLayerProperties pAnnLayProWait = pLabelWaitLayPro as IAnnotateLayerProperties;//新生成的符号图层设置标注
                                        pAnnLayProWait.DisplayAnnotation = true;
                                        pAnnLayProWait.FeatureLayer = pGFLayerWait;
                                        pAnnLayProWait.LabelWhichFeatures = esriLabelWhichFeatures.esriVisibleFeatures;
                                        pAnnLayProWait.WhereClause = "";
                                        pGFLayerWait.AnnotationProperties.Add(pAnnLayProWait);
                                        pGFLayerWait.DisplayAnnotation = true;
                                    }
                                }
                                //break;
                            }
                        }
                        else
                        {

                        }
                    }
                    //出现问题：设置符号后，TOC不刷新，但是当显示在axPageLayoutCtlMain的时候TOC进行了刷新，测试后
                    //TOC开始的时候没有和几个船体控件进行绑定
                    if (axTOCCtlLayer.Buddy.Equals(axMapCtlMain) == false && barMain.SelectedDockTab == 0)
                    {
                        axTOCCtlLayer.SetBuddyControl(axMapCtlMain);
                    }
                    axMapCtlMain.ActiveView.Refresh();
                    axTOCCtlLayer.ActiveView.Refresh();
                }
                else
                {
                    MessageBox.Show("请先导入需要设置符号的图层", "提示", MessageBoxButtons.OK);
                }
            }
            catch (System.Exception ex)
            {

            }
        }

        private void btnMapParkSymbol_Click(object sender, EventArgs e)
        {
            FrmImportWaijiance frmwjc = new FrmImportWaijiance(this.axMapCtlMain, this.axTOCCtlLayer);
            frmwjc.ShowDialog();
        }

        private void btnINMapSymbol_Click(object sender, EventArgs e)
        {
            if (axMapCtlMain.LayerCount != 0)
            {
                string FilePath = "";
                OpenFileDialog dlgMapSymbol = new OpenFileDialog();
                dlgMapSymbol.Title = "选择整体规划模板地图";
                dlgMapSymbol.InitialDirectory = ".";
                dlgMapSymbol.Filter = "mxd文件(*.mxd)|*.mxd";
                dlgMapSymbol.RestoreDirectory = true;
                if (dlgMapSymbol.ShowDialog() == DialogResult.OK)
                {
                    FilePath = dlgMapSymbol.FileName;
                }
                else
                {
                    return;
                }

                IMapDocument pMapDocument = new MapDocumentClass();
                pMapDocument.Open(FilePath, null);
                IMap pMap = pMapDocument.Map[0];

                //初始化图层列表
                IEnumLayer pEnumLayer = axMapCtlMain.Map.get_Layers(null, true);
                pEnumLayer.Reset();
                ILayer pLayerWait = null;
                while ((pLayerWait = pEnumLayer.Next()) != null)
                {
                    if (pLayerWait is IFeatureLayer && pLayerWait.Name.Contains("_Gmppoint"))
                    {
                        for (int j = 0; j < pMap.LayerCount; j++)
                        {
                            ILayer pLayerSymbol = pMap.get_Layer(j);
                            if (pLayerSymbol is IFeatureLayer && pLayerSymbol.Name.Contains("_Gmppoint"))
                            {
                                IFeatureLayer pFLayerSymbol = pLayerSymbol as IFeatureLayer;
                                IGeoFeatureLayer pGFLayerSymbol = pFLayerSymbol as IGeoFeatureLayer;
                                IFeatureLayer pFLayerWait = pLayerWait as IFeatureLayer;
                                IGeoFeatureLayer pGFLayerWait = pFLayerWait as IGeoFeatureLayer;
                                if (pGFLayerSymbol != null)
                                {
                                    pGFLayerWait.Renderer = pGFLayerSymbol.Renderer;

                                    IAnnotateLayerProperties pAnnoLayerP = new LabelEngineLayerPropertiesClass();//符号图层标注
                                    IElementCollection PELECOLL;//queryitem的参数，用不到
                                    IElementCollection pelecoll;//同上
                                    pGFLayerSymbol.AnnotationProperties.QueryItem(0, out pAnnoLayerP, out  PELECOLL, out pelecoll);
                                    ILabelEngineLayerProperties pLabelEenLayPro = pAnnoLayerP as ILabelEngineLayerProperties;

                                    IFontDisp pFont = new StdFontClass() as IFontDisp;
                                    ITextSymbol pTextSymbol = new TextSymbolClass();
                                    pTextSymbol.Color = pLabelEenLayPro.Symbol.Color;
                                    pFont.Bold = pLabelEenLayPro.Symbol.Font.Bold;
                                    pFont.Italic = pLabelEenLayPro.Symbol.Font.Italic;
                                    pFont.Strikethrough = pLabelEenLayPro.Symbol.Font.Strikethrough;
                                    pFont.Underline = pLabelEenLayPro.Symbol.Font.Underline;
                                    pFont.Name = pLabelEenLayPro.Symbol.Font.Name;
                                    pFont.Size = pLabelEenLayPro.Symbol.Font.Size;
                                    pTextSymbol.Font = pFont;

                                    pGFLayerWait.AnnotationProperties.Clear();
                                    ILabelEngineLayerProperties pLabelWaitLayPro = new LabelEngineLayerPropertiesClass();//设置待渲染图层的标注
                                    pLabelWaitLayPro.Expression = pLabelEenLayPro.Expression;
                                    pLabelWaitLayPro.Symbol = pTextSymbol;

                                    IAnnotateLayerProperties pAnnLayProWait = pLabelWaitLayPro as IAnnotateLayerProperties;//新生成的符号图层设置标注
                                    pAnnLayProWait.DisplayAnnotation = true;
                                    pAnnLayProWait.FeatureLayer = pGFLayerWait;
                                    pAnnLayProWait.LabelWhichFeatures = esriLabelWhichFeatures.esriVisibleFeatures;
                                    pAnnLayProWait.WhereClause = "";
                                    pGFLayerWait.AnnotationProperties.Add(pAnnLayProWait);
                                    pGFLayerWait.DisplayAnnotation = true;
                                }                              
                            }
                            //break;
                        }
                    }

                    else if (pLayerWait is IFeatureLayer && pLayerWait.Name.Contains("_GmpLine"))
                    {
                        for (int j = 0; j < pMap.LayerCount; j++)
                        {
                            ILayer pLayerSymbol = pMap.get_Layer(j);
                            if (pLayerSymbol is IFeatureLayer && pLayerSymbol.Name.Contains("_GmpLine"))
                            {
                                IFeatureLayer pFLayerSymbol = pLayerSymbol as IFeatureLayer;
                                IGeoFeatureLayer pGFLayerSymbol = pFLayerSymbol as IGeoFeatureLayer;
                                IFeatureLayer pFLayerWait = pLayerWait as IFeatureLayer;
                                IGeoFeatureLayer pGFLayerWait = pFLayerWait as IGeoFeatureLayer;
                                if (pGFLayerSymbol != null)
                                {
                                    pGFLayerWait.Renderer = pGFLayerSymbol.Renderer;

                                    IAnnotateLayerProperties pAnnoLayerP = new LabelEngineLayerPropertiesClass();//符号图层标注
                                    IElementCollection PELECOLL;//queryitem的参数，用不到
                                    IElementCollection pelecoll;//同上
                                    pGFLayerSymbol.AnnotationProperties.QueryItem(0, out pAnnoLayerP, out  PELECOLL, out pelecoll);
                                    ILabelEngineLayerProperties pLabelEenLayPro = pAnnoLayerP as ILabelEngineLayerProperties;

                                    IFontDisp pFont = new StdFontClass() as IFontDisp;
                                    ITextSymbol pTextSymbol = new TextSymbolClass();
                                    pTextSymbol.Color = pLabelEenLayPro.Symbol.Color;
                                    pFont.Bold = pLabelEenLayPro.Symbol.Font.Bold;
                                    pFont.Italic = pLabelEenLayPro.Symbol.Font.Italic;
                                    pFont.Strikethrough = pLabelEenLayPro.Symbol.Font.Strikethrough;
                                    pFont.Underline = pLabelEenLayPro.Symbol.Font.Underline;
                                    pFont.Name = pLabelEenLayPro.Symbol.Font.Name;
                                    pFont.Size = pLabelEenLayPro.Symbol.Font.Size;
                                    pTextSymbol.Font = pFont;

                                    pGFLayerWait.AnnotationProperties.Clear();
                                    ILabelEngineLayerProperties pLabelWaitLayPro = new LabelEngineLayerPropertiesClass();//设置待渲染图层的标注
                                    pLabelWaitLayPro.Expression = pLabelEenLayPro.Expression;
                                    pLabelWaitLayPro.Symbol = pTextSymbol;

                                    IAnnotateLayerProperties pAnnLayProWait = pLabelWaitLayPro as IAnnotateLayerProperties;//新生成的符号图层设置标注
                                    pAnnLayProWait.DisplayAnnotation = true;
                                    pAnnLayProWait.FeatureLayer = pGFLayerWait;
                                    pAnnLayProWait.LabelWhichFeatures = esriLabelWhichFeatures.esriVisibleFeatures;
                                    pAnnLayProWait.WhereClause = "";
                                    pGFLayerWait.AnnotationProperties.Add(pAnnLayProWait);
                                    pGFLayerWait.DisplayAnnotation = true;
                                }                               
                            }
                            //break;
                        }
                    }
                }
                //出现问题：设置符号后，TOC不刷新，但是当显示在axPageLayoutCtlMain的时候TOC进行了刷新，测试后
                //TOC开始的时候没有和几个船体控件进行绑定
                if (axTOCCtlLayer.Buddy.Equals(axMapCtlMain) == false && barMain.SelectedDockTab == 0)
                {
                    axTOCCtlLayer.SetBuddyControl(axMapCtlMain);
                }
                axMapCtlMain.ActiveView.Refresh();
                axTOCCtlLayer.ActiveView.Refresh();
            }
            else
            {
                MessageBox.Show("请先导入需要设置符号的图层", "提示", MessageBoxButtons.OK);
            }
        }

        #endregion

        #region 制图菜单

        private void btnAddText_Click(object sender, EventArgs e)
        {
            cmdAddText.OnClick();
        }

        private void btnAddNorthArrow_Click(object sender, EventArgs e)
        {
            cmdNorthArrow.OnClick(); 
        }

        private void btnAddLegend_Click(object sender, EventArgs e)
        {
            cmdLeg.OnClick();
        }

        private void btnAddGrid_Click(object sender, EventArgs e)
        {
            cmdMapGrid.OnClick();
        }

        private void btnAddScallBar_Click(object sender, EventArgs e)
        {
            cmdScaleBar.OnClick();
        }

        private void btnAddScallText_Click(object sender, EventArgs e)
        {
            cmdScaleText.OnClick();
        }

        private void btnExportMap_Click(object sender, EventArgs e)
        {
            cmdExpActFig.OnClick();
        }

        private void btnPrintMap_Click(object sender, EventArgs e)
        {
            cmdPrintMap.OnClick();
        }

        #endregion

        #region 菜单栏——数据转换——栅格处理

        private void menuBtnRasterPan_Click(object sender, EventArgs e)
        {
            if (m_cmdRasterShift.Enabled)
            {
                m_cmdRasterShift.OnClick();
            }
        }

        private void menuBtnRasterRotate_Click(object sender, EventArgs e)
        {
            if (m_cmdRasterRotate.Enabled)
            {
                m_cmdRasterRotate.OnClick();
            }
        }

        private void menuBtnRasterMirror_Click(object sender, EventArgs e)
        {
            if (m_cmdRasterMirror.Enabled)
            {
                m_cmdRasterMirror.OnClick();
            }
        }

        private void menuBtnFlip_Click(object sender, EventArgs e)
        {
            if (m_cmdRasterFlip.Enabled)
            {
                m_cmdRasterFlip.OnClick();
            }
        }

        private void menuBtnRasterZ_Click(object sender, EventArgs e)
        {
            if (m_cmdRasterTransZ.Enabled)
            {
                m_cmdRasterTransZ.OnClick();
            }
        }

        private void menuBtnRasterResample_Click(object sender, EventArgs e)
        {
            if (m_cmdRasterResample.Enabled)
            {
                m_cmdRasterResample.OnClick();
            }
        }

        #endregion

        /// <summary>
        /// 将北东地坐标系转换成东北天坐标系
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNEDtoENU_Click(object sender, EventArgs e)
        {

#region 废弃代码
            //FrmNEDtoENU frmN2E = new FrmNEDtoENU(axMapCtlMain.Object as IMapControl3, null);
            //if (DialogResult.OK == frmN2E.ShowDialog())
            //{
            //    ClsRasterOp pRasterOp = new ClsRasterOp();

            //    //获取RASTERLAYER
            //    IMapControl3 pMapControl = axMapCtlMain.Object as IMapControl3;
            //    IRasterLayer pRasterLayer = ClsGDBDataCommon.GetLayerFromName(pMapControl.Map, frmN2E.szSrcName) as IRasterLayer;

            //    if (pRasterLayer == null)
            //        return;

            //    //实际的处理
            //    string szDstName = frmN2E.szDstName;
            //    if (pRasterOp.NorthEastToEastNorth(pRasterLayer, szDstName))
            //    {
            //        RasterLayerClass rasterlayer = new RasterLayerClass();
            //        rasterlayer.CreateFromFilePath(szDstName);

            //        pMapControl.AddLayer(rasterlayer as ILayer);
            //    }
            //}
#endregion
            FrmNEDtoENU frmN2E = new FrmNEDtoENU(axMapCtlMain.Object as IMapControl3, null);
            if (DialogResult.OK == frmN2E.ShowDialog())
            {
                ClsRasterOp pRasterOp = new ClsRasterOp();

                //获取RASTERLAYER
                IMapControl3 pMapControl = axMapCtlMain.Object as IMapControl3;
                IRasterLayer pRasterLayer = ClsGDBDataCommon.GetLayerFromName(pMapControl.Map, frmN2E.szSrcName) as IRasterLayer;

                if (pRasterLayer == null)
                    return;

                pRasterOp.NorthEastToEastNorth3(pRasterLayer);
                pMapControl.ActiveView.PartialRefresh( esriViewDrawPhase.esriViewGeography, null, null);
            }
        }

        //save toolbar status,在主窗口关闭时候调用
        private void SaveToolbarStatusToFile(StreamWriter SW,DevComponents.DotNetBar.Bar barTool)
        {
            //StreamWriter FSC = new StreamWriter(GetParentPathofExe() + @"Resource\configure\ToolBar.cfg", true);
            SW.WriteLine(barTool.Name);
            if (barTool.Visible == true)
            {
                SW.WriteLine("on");
            }
            else
            {
                SW.WriteLine("off");
            }
        }

        //保存自定义工具栏
        private void SaveToolbarControl(AxToolbarControl toolbar)
        {
            try
            {
                string filePath = GetParentPathofExe() + @"Resource\configure\" + toolbar.Name + @".cfg";
                //Create a MemoryBlobStream. 
                IBlobStream blobStream = new MemoryBlobStreamClass();
                //Get the IStream interface.
                IStream stream = (IStream)blobStream;
                //Get the IToolbarControl2 interface.
                IToolbarControl2 toolbarControl = (IToolbarControl2)toolbar.Object;
                //Save the ToolbarControl into the stream.
                toolbarControl.SaveItems(stream);
                //Save the stream to a file.
                blobStream.SaveToFile(filePath);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //读取自定义工具栏
        private void LoadToolbarControl(AxToolbarControl toolbar)
        {
            try
            {
                string filePath = GetParentPathofExe() + @"Resource\configure\" + toolbar.Name + @".cfg";
                if (System.IO.File.Exists(filePath) == true)
                {
                    //Create a MemoryBlobStream.
                    IBlobStream blobStream = new MemoryBlobStreamClass();
                    //Load the stream from the file.
                    blobStream.LoadFromFile(filePath);
                    //Get the IStream interface.
                    IStream stream = (IStream)blobStream;
                    //Get the IToolbarControl2 interface.
                    IToolbarControl2 toolbarControl = (IToolbarControl2)toolbar.Object;
                    //Load the stream into the ToolbarControl.
                    toolbarControl.LoadItems(stream);
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
 
        }



        #region 环拍范围 太阳高度角 参数获取

        public struct tagInstallMatrix
        {
            //public double[] dOriMatrix;
            //public double[] dPosMatrix;
            public unsafe fixed double dOriMatrix[9];
            public unsafe fixed double dPosMatrix[3];
        }

        public double Deg2Rad(double d)  //!<角度转弧度
        {
            return d / 180.0 * Math.PI;
        }

        public void att321(double x, double y, double z, ref double[,] lbw)
        {
            double sx, cx, sy, cy, sz, cz;
            lbw = new double[3, 3];

            sx = Math.Sin(x);
            cx = Math.Cos(x);
            sy = Math.Sin(y);
            cy = Math.Cos(y);
            sz = Math.Sin(z);
            cz = Math.Cos(z);

            lbw[0, 0] = cy * cz;
            lbw[0, 1] = cy * sz;
            lbw[0, 2] = -sy;
            lbw[1, 0] = sx * sy * cz - cx * sz;
            lbw[1, 1] = sx * sy * sz + cx * cz;
            lbw[1, 2] = sx * cy;
            lbw[2, 0] = cx * sy * cz + sx * sz;
            lbw[2, 1] = cx * sy * sz - sx * cz;
            lbw[2, 2] = cx * cy;
        }

        public void att321_IRSAVer(double x, double y, double z, ref double[,] lbw)
        {
            double sx, cx, sy, cy, sz, cz;
            lbw = new double[3, 3];

            sx = Math.Sin(x);
            cx = Math.Cos(x);
            sy = Math.Sin(y);
            cy = Math.Cos(y);
            sz = Math.Sin(z);
            cz = Math.Cos(z);

            lbw[0, 0] = cy * cz;
            lbw[1, 0] = cy * sz;
            lbw[2, 0] = -sy;
            lbw[0, 1] = sx * sy * cz - cx * sz;
            lbw[1, 1] = sx * sy * sz + cx * cz;
            lbw[2, 1] = sx * cy;
            lbw[0, 2] = cx * sy * cz + sx * sz;
            lbw[1, 2] = cx * sy * sz - sx * cz;
            lbw[2, 2] = cx * cy;
        }

        private void menuBtnVisibility_Click(object sender, EventArgs e)//环拍范围分析
        {
            FrmCamOriInWorld frmCam = new FrmCamOriInWorld(this.axMapCtlMain);
            frmCam.ShowDialog();
            if (frmCam.isResultOK)
            {

                //double[,] lbw = new double[3, 3];
                //tagInstallMatrix matBase = new tagInstallMatrix();
                //tagInstallMatrix matMastExp2Body = new tagInstallMatrix();
                //tagInstallMatrix matMastYaw2Exp = new tagInstallMatrix();
                //tagInstallMatrix matMastPitch2Yaw = new tagInstallMatrix();
                //tagInstallMatrix matLCamBase2Pitch = new tagInstallMatrix();
                //tagInstallMatrix matRCamBase2LCamBase = new tagInstallMatrix();
                //tagInstallMatrix matLCamCap2Base = new tagInstallMatrix();
                //tagInstallMatrix matRCamCap2Base = new tagInstallMatrix();
                //Ex_OriPara exOriCamL = new Ex_OriPara();
                //exOriCamL.ori.kap = 0;
                //exOriCamL.ori.omg = 0;
                //exOriCamL.ori.phi = 0;
                //exOriCamL.pos.X = 0;
                //exOriCamL.pos.Y = 0;
                //exOriCamL.pos.Z = 0;

                //Ex_OriPara exOriCamR = new Ex_OriPara();
                //exOriCamR.ori.kap = 0;
                //exOriCamR.ori.omg = 0;
                //exOriCamR.ori.phi = 0;
                //exOriCamR.pos.X = 0;
                //exOriCamR.pos.Y = 0;
                //exOriCamR.pos.Z = 0;


                //att321_IRSAVer(Deg2Rad(frmCam.dOriOmg), Deg2Rad(frmCam.dOriPhi), Deg2Rad(frmCam.dOriKap), ref lbw);
                //unsafe
                //{
                //    matBase.dPosMatrix[0] = frmCam.dPosX;
                //    matBase.dPosMatrix[1] = frmCam.dPosY;
                //    matBase.dPosMatrix[2] = frmCam.dPosZ;
                //    matBase.dOriMatrix[0] = lbw[0, 0];
                //    matBase.dOriMatrix[1] = lbw[0, 1];
                //    matBase.dOriMatrix[2] = lbw[0, 2];
                //    matBase.dOriMatrix[3] = lbw[1, 0];
                //    matBase.dOriMatrix[4] = lbw[1, 1];
                //    matBase.dOriMatrix[5] = lbw[1, 2];
                //    matBase.dOriMatrix[6] = lbw[2, 0];
                //    matBase.dOriMatrix[7] = lbw[2, 1];
                //    matBase.dOriMatrix[8] = lbw[2, 2];

                //    matMastExp2Body.dPosMatrix[0] = 533.204;
                //    matMastExp2Body.dPosMatrix[1] = -1.5296;
                //    matMastExp2Body.dPosMatrix[2] = -570.7493;
                //    matMastExp2Body.dOriMatrix[0] = 0.1704;
                //    matMastExp2Body.dOriMatrix[1] = 89.8822;
                //    matMastExp2Body.dOriMatrix[2] = 89.8769;
                //    matMastExp2Body.dOriMatrix[3] = 90.1175;
                //    matMastExp2Body.dOriMatrix[4] = 0.1934;
                //    matMastExp2Body.dOriMatrix[5] = 90.1535;
                //    matMastExp2Body.dOriMatrix[6] = 90.1234;
                //    matMastExp2Body.dOriMatrix[7] = 89.8467;
                //    matMastExp2Body.dOriMatrix[8] = 0.1968;

                //    matMastYaw2Exp.dPosMatrix[0] = -85.0554;
                //    matMastYaw2Exp.dPosMatrix[1] = -47.9819;
                //    matMastYaw2Exp.dPosMatrix[2] = 0;
                //    matMastYaw2Exp.dOriMatrix[0] = 0;
                //    matMastYaw2Exp.dOriMatrix[1] = 90;
                //    matMastYaw2Exp.dOriMatrix[2] = 90;
                //    matMastYaw2Exp.dOriMatrix[3] = 90;
                //    matMastYaw2Exp.dOriMatrix[4] = 0.051;
                //    matMastYaw2Exp.dOriMatrix[5] = 89.9490;
                //    matMastYaw2Exp.dOriMatrix[6] = 90;
                //    matMastYaw2Exp.dOriMatrix[7] = 90.0510;
                //    matMastYaw2Exp.dOriMatrix[8] = 0.0510;

                //    matMastPitch2Yaw.dPosMatrix[0] = 0.3225;
                //    matMastPitch2Yaw.dPosMatrix[1] = -0.0005;
                //    matMastPitch2Yaw.dPosMatrix[2] = -649.3588;
                //    matMastPitch2Yaw.dOriMatrix[0] = 0.093;
                //    matMastPitch2Yaw.dOriMatrix[1] = 90.0930;
                //    matMastPitch2Yaw.dOriMatrix[2] = 90.0001;
                //    matMastPitch2Yaw.dOriMatrix[3] = 89.9070;
                //    matMastPitch2Yaw.dOriMatrix[4] = 0.1554;
                //    matMastPitch2Yaw.dOriMatrix[5] = 89.8755;
                //    matMastPitch2Yaw.dOriMatrix[6] = 90.0002;
                //    matMastPitch2Yaw.dOriMatrix[7] = 90.1245;
                //    matMastPitch2Yaw.dOriMatrix[8] = 0.1245;

                //    matLCamBase2Pitch.dPosMatrix[0] = 69.8878;
                //    matLCamBase2Pitch.dPosMatrix[1] = -109.0252;
                //    matLCamBase2Pitch.dPosMatrix[2] = -102.9593;
                //    matLCamBase2Pitch.dOriMatrix[0] = 90.1032;
                //    matLCamBase2Pitch.dOriMatrix[1] = 0.2726;
                //    matLCamBase2Pitch.dOriMatrix[2] = 90.2523;
                //    matLCamBase2Pitch.dOriMatrix[3] = 90.3158;
                //    matLCamBase2Pitch.dOriMatrix[4] = 89.7483;
                //    matLCamBase2Pitch.dOriMatrix[5] = 0.4038;
                //    matLCamBase2Pitch.dOriMatrix[6] = 0.3357;
                //    matLCamBase2Pitch.dOriMatrix[7] = 89.8850;
                //    matLCamBase2Pitch.dOriMatrix[8] = 89.6846;

                //    matRCamBase2LCamBase.dPosMatrix[0] = 269.91;
                //    matRCamBase2LCamBase.dPosMatrix[1] = -0.17;
                //    matRCamBase2LCamBase.dPosMatrix[2] = -0.45;
                //    matRCamBase2LCamBase.dOriMatrix[0] = 0.2988;
                //    matRCamBase2LCamBase.dOriMatrix[1] = 90.0116;
                //    matRCamBase2LCamBase.dOriMatrix[2] = 89.7014;
                //    matRCamBase2LCamBase.dOriMatrix[3] = 89.9886;
                //    matRCamBase2LCamBase.dOriMatrix[4] = 0.0312;
                //    matRCamBase2LCamBase.dOriMatrix[5] = 89.9710;
                //    matRCamBase2LCamBase.dOriMatrix[6] = 90.2944;
                //    matRCamBase2LCamBase.dOriMatrix[7] = 90.0289;
                //    matRCamBase2LCamBase.dOriMatrix[8] = 0.2959;

                //    matLCamCap2Base.dPosMatrix[0] = 24.13066375;
                //    matLCamCap2Base.dPosMatrix[1] = 22.869995;
                //    matLCamCap2Base.dPosMatrix[2] = -2.82183125;
                //    matLCamCap2Base.dOriMatrix[0] = 0;
                //    matLCamCap2Base.dOriMatrix[1] = 90;
                //    matLCamCap2Base.dOriMatrix[2] = 90;
                //    matLCamCap2Base.dOriMatrix[3] = 90;
                //    matLCamCap2Base.dOriMatrix[4] = 0;
                //    matLCamCap2Base.dOriMatrix[5] = 90;
                //    matLCamCap2Base.dOriMatrix[6] = 90;
                //    matLCamCap2Base.dOriMatrix[7] = 90;
                //    matLCamCap2Base.dOriMatrix[8] = 0;

                //    matRCamCap2Base.dPosMatrix[0] = 22.863;
                //    matRCamCap2Base.dPosMatrix[1] = 30.159;
                //    matRCamCap2Base.dPosMatrix[2] = -6.573;
                //    matRCamCap2Base.dOriMatrix[0] = 0;
                //    matRCamCap2Base.dOriMatrix[1] = 90;
                //    matRCamCap2Base.dOriMatrix[2] = 90;
                //    matRCamCap2Base.dOriMatrix[3] = 90;
                //    matRCamCap2Base.dOriMatrix[4] = 0;
                //    matRCamCap2Base.dOriMatrix[5] = 90;
                //    matRCamCap2Base.dOriMatrix[6] = 90;
                //    matRCamCap2Base.dOriMatrix[7] = 90;
                //    matRCamCap2Base.dOriMatrix[8] = 0;
                //}
                //bool it = false;
                for (int i = 0; i < frmCam.dYawAngle.Count; i++)
                {
                    //it = mlCalcCamOriInWorldByGivenInsMatPosX(matBase, frmCam.dExpAngle, frmCam.dYawAngle[i], frmCam.dPitchAngle,
                    //                                             matMastExp2Body, matMastYaw2Exp, matMastPitch2Yaw,
                    //                                              matLCamBase2Pitch, matRCamBase2LCamBase,
                    //                                             matLCamCap2Base, matRCamCap2Base,
                    //                                             out exOriCamL, out exOriCamR);
                    Ex_OriPara exOriCamL = SkyandCamInWorldAnalyst(frmCam.dOriOmg, frmCam.dOriPhi, frmCam.dOriKap, frmCam.dPosX, frmCam.dPosY,
                                                                 frmCam.dPosZ, frmCam.dExpAngle, frmCam.dYawAngle[i], frmCam.dPitchAngle);
                    frmCam.GetCamOri(exOriCamL, frmCam.pFeatureClass, i);
                }


                IFeatureLayer ppFeatureLayer = new FeatureLayerClass();
                ppFeatureLayer.FeatureClass = frmCam.pFeatureClass;
                ppFeatureLayer.Name = "环拍结果";
                axMapCtlMain.AddLayer(ppFeatureLayer as ILayer);
                axMapCtlMain.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
            }

        }

        #endregion

//        #region 电文模块
        
//        private void btnBrowse_Click(object sender, EventArgs e)
//        {
//            FolderBrowserDialog fdlg = new FolderBrowserDialog();
//            fdlg.SelectedPath = @"D:\CE3Map";
//            if (fdlg.ShowDialog() != DialogResult.OK)
//            {
//                return;
//            }

//            if (fdlg.SelectedPath != "")
//            {
//                //DirectoryInfo dir = Directory.CreateDirectory(fdlg.SelectedPath);
//                fdpath= fdlg.SelectedPath;
//                txtFolder.Text = fdpath;
//            }
//        }

//        private void btnStartListen_Click(object sender, EventArgs e)
//        {
//            //try
//            //{
//            //    //创建文件接受目录
//            //    bool bResult = false;
//            //    if (txtFolder.Text != "")
//            //    {
//            //        bResult = CreateFloder(txtFolder.Text);
//            //    }
//            //    else
//            //    {
//            //        string fdpathIn = "D:\\CE3Map";
//            //        bResult =  CreateFloder(fdpathIn);
//            //    }
//            //    if (!bResult)
//            //    {
//            //        MessageBox.Show("创建文件接收文件夹失败！");
//            //        return;
//            //    }

//            //    //启动监听线程
//            //    thdLis = new Thread(new ThreadStart(startLis));
//            //    if (thdLis !=null)
//            //    {
//            //        thdLis.Start();
//            //    }
//            //    else
//            //    {
//            //        MessageBox.Show("文件接收服务启动失败！");
//            //        return;
//            //    }              

//            //    btnStartListen.Checked = true;
//            //    btnStopListen.Checked = false;
//            //}
//            //catch (System.Exception ex)
//            //{
//            //    MessageBox.Show("文件接收服务启动失败");
//            //}
//        }

//        public bool CreateFloder(string Floder)
//        {
//            bool bResult = false;
//            try
//            {
            
//            System.DateTime currentTime = new System.DateTime();
//            currentTime = System.DateTime.Today;
//            //string pathFloder = txtFolder.Text + "\\" + currentTime.ToShortDateString().ToString();
//            string strdata = currentTime.Year + "." + currentTime.Month + "." + currentTime.Day;
//            string pathFloder = Floder + "\\" + strdata;

//            if (Directory.Exists(pathFloder))
//            {
//                fdpath = pathFloder;
//            }
//            else
//            {
//                Directory.CreateDirectory(pathFloder);
//                fdpath = pathFloder;
//                for (int i = 0; i < m_pListFielType.Count;i++ )
//                {
//                    _ZF_FILETYPE fileType = m_pListFielType[i];
//                    Directory.CreateDirectory(pathFloder + "\\" + fileType.strFileType + " " + fileType.nCode); 
//                }
//                //Directory.CreateDirectory(pathFloder + "\\" + "IFLI_YSDHX 5000");
//                //Directory.CreateDirectory(pathFloder + "\\" + "IFLI_YSQJX 5001");
//                //Directory.CreateDirectory(pathFloder + "\\" + "IFLI_YSBZX 5002");
//                //Directory.CreateDirectory(pathFloder + "\\" + "IFLI_ZS 5016");
//                //Directory.CreateDirectory(pathFloder + "\\" + "IFLI_FYQJX 5026");
//                //Directory.CreateDirectory(pathFloder + "\\" + "IFLI_GC_100CM 5027");
//                //Directory.CreateDirectory(pathFloder + "\\" + "IFLI_GC_10CM 5028");
//                //Directory.CreateDirectory(pathFloder + "\\" + "IFLI_GC_1CM 5029");
//                //Directory.CreateDirectory(pathFloder + "\\" + "IFLI_GC_HIGH 5030");
//                //Directory.CreateDirectory(pathFloder + "\\" + "IFLI_RNAVIP 5051");
//                //Directory.CreateDirectory(pathFloder + "\\" + "IFLI_RPACPA 5054");
//                //Directory.CreateDirectory(pathFloder + "\\" + "IFLI_RZLLAND 5520");
//            }
//            strSaveFileFloder = pathFloder;
//            bResult = true;
//            }
//            catch (System.Exception ex)
//            {
//                bResult = false;
//            }

//            return bResult;
//        }

//        private void btnStopListen_Click(object sender, EventArgs e)
//        {
//            //try
//            //{
//            //    btnStartListen.Checked = false;
//            //    btnStopListen.Checked = true;
//            //    int nErrCode = zfclose();
//            //    if (nErrCode !=0)
//            //    {
//            //        //MessageBox.Show("关闭文件接收服务失败！" + nErrCode.ToString());
//            //    }
//            //    if (thdLis != null)
//            //    {
//            //        thdLis.Abort();
//            //    }
//            //    m_isFirstStartRecvw = true;
//            //}
//            //catch (System.Exception ex)
//            //{
//            //    MessageBox.Show("关闭文件接收服务失败！"); 
//            //}
//        }

//        private void dataGridRv_CellDoubleClick(object sender, DataGridViewCellEventArgs e)//双击接收到的文件，将文件添加到地图控件中
//        {
//            string FilePath = dataGridRv.Rows[e.RowIndex].Cells[2].Value.ToString();
//            string fileName = dataGridRv.Rows[e.RowIndex].Cells[1].Value.ToString();
//            if (fileName.Contains(".tif"))
//            {
//                IMapControl3 pMapControlAdd = axMapCtlMain.Object as IMapControl3;
//                IRasterLayer pRasterFileRv=new RasterLayerClass();
//                pRasterFileRv.CreateFromFilePath(FilePath);
//                pMapControlAdd.AddLayer(pRasterFileRv as ILayer);
                
//            }
            
//        }

//        private void btnSetSQL_Click(object sender, EventArgs e)
//        {
//            FrmSetSQL frmsetsql = new FrmSetSQL();
//            frmsetsql.ShowDialog();
//            RichTBoxSQL.Clear();
//            RichTBoxSQL.Text = "select * from FileDB where " + frmsetsql.strSQL;
//        }

//        private void RadioBoxLocal_CheckedChanged(object sender, EventArgs e)
//        {
//            this.panelLocal.Visible = RadioBoxLocal.Checked;
//            this.panelRemote.Visible = RadioBoxService.Checked;
//            datagridLocal.Visible = RadioBoxLocal.Checked;
//            datagridzdFile.Visible = RadioBoxService.Checked;
//         }

//        private void RadioBoxService_CheckedChanged(object sender, EventArgs e)
//        {
//            this.panelLocal.Visible = RadioBoxLocal.Checked;
//            this.panelRemote.Visible = RadioBoxService.Checked;
//            datagridLocal.Visible = RadioBoxLocal.Checked;
//            datagridzdFile.Visible = RadioBoxService.Checked;
//        }
//        //查询站点记录数量
//        private void btnSeekSite_Click(object sender, EventArgs e)
//        {
//            panelRecords.Visible = true;
//            gridRecords.Visible = true;
//            gridRecords.PrimaryGrid.Rows.Clear();

//            hwnd = User32API.GetCurrentWindowHandle();
//            for (int i = 0; i < cmbStationID.Items.Count;i++ )
//            {
//                //string strID =  cmbStationID.Items[i].ToString();
//                cmbStationID.SelectedIndex = i;
//                cmbStationID.Refresh();
//                RichTBoxSQL.Refresh();
//                string strID = cmbStationID.Text;
//                if (int.Parse(strID) < 1312140001) continue;

//                int nIndex = ClsZfDatabase.GetZF_FileTypeIndex(m_pListFielType, cmbfiletype.Text);
//                if (nIndex < 0) return;
//                int filetype = m_pListFielType[nIndex].nCode;
//                int nCount = zdFileCount(filetype, RichTBoxSQL.Text, hwnd);
//                if (nCount < 1) continue;
//                GridRow row = new GridRow(strID, nCount.ToString());
//                gridRecords.PrimaryGrid.Rows.Add(row);
                
//                gridRecords.Refresh();
//                Thread.Sleep(500);
//            }
//            gridRecords.Refresh();
//            MessageBox.Show("查询完毕！");
//        }

//        //双击查询当前行的数据
//        private void gridRecords_RowDoubleClick(object sender, GridRowDoubleClickEventArgs e)
//        {
//            if (e.GridRow.GridIndex >=0)
//            {
//                cmbStationID.Text = gridRecords.GetCell(e.GridRow.GridIndex, 0).Value.ToString();
//                btnSearch_Click(null, null);
//            }
//        }

//        private void btnSearch_Click(object sender, EventArgs e)
//        {
//            if (RadioBoxLocal.Checked == true)//查询本地
//            {
//                if (RichTBoxSQL.Text != "")
//                {
//                    OleDbSQL cnn = new OleDbSQL();
//                    DataSet ds = cnn.dataAdapter(RichTBoxSQL.Text, cnn.connection(cnn.connectionPath()));
//                    if (ds != null)
//                    {
//                        DataTable dt = ds.Tables[0];
//                        datagridLocal.DataSource = dt;
//                        datagridLocal.Update();
//                    }                   
//                }
//                else
//                {
//                    string strSQL = "select * from FileDB";
//                    OleDbSQL cnn = new OleDbSQL();
//                    DataSet ds = cnn.dataAdapter(strSQL, cnn.connection(cnn.connectionPath()));
//                    if (ds != null)
//                    {
//                        DataTable dt = ds.Tables[0];
//                        datagridLocal.DataSource = dt;
//                        datagridLocal.Update();
//                    }                   
//                }
//            }
//            else if (RadioBoxService.Checked==true)
//            {
//                zdFileTable.Clear();
//                datagridzdFile.Update();
//                if (RichTBoxSQL.Text!=null)//查询网络数据库
//                {
//                    hwnd = User32API.GetCurrentWindowHandle();
//#region 

//                    //int filetype = 0;
//                    //if (cmbfiletype.SelectedIndex==0)
//                    //{
//                    //    filetype = 5051;
//                    //}
//                    //else if (cmbfiletype.SelectedIndex==1)
//                    //{
//                    //    filetype = 5054;
//                    //}
//                    //else if (cmbfiletype.SelectedIndex == 2)
//                    //{
//                    //    filetype = 5000;
//                    //}
//                    //else if (cmbfiletype.SelectedIndex == 3)
//                    //{
//                    //    filetype = 5001;
//                    //}
//                    //else if (cmbfiletype.SelectedIndex == 4)
//                    //{
//                    //    filetype = 5002;
//                    //}
//                    //else if (cmbfiletype.SelectedIndex == 5)
//                    //{
//                    //    filetype = 5016;
//                    //}
//                    //else if (cmbfiletype.SelectedIndex == 6)
//                    //{
//                    //    filetype = 5026;
//                    //}
//                    //else if (cmbfiletype.SelectedIndex == 7)
//                    //{
//                    //    filetype = 5027;
//                    //}
//                    //else if (cmbfiletype.SelectedIndex == 8)
//                    //{
//                    //    filetype = 5028;
//                    //}
//                    //else if (cmbfiletype.SelectedIndex == 9)
//                    //{
//                    //    filetype = 5029;
//                    //}
//                    //else if (cmbfiletype.SelectedIndex == 10)
//                    //{
//                    //    filetype = 5030;
//                    //}
//                    //else if (cmbfiletype.SelectedIndex == 11)
//                    //{
//                    //    filetype = 5520;
//                    //}
//#endregion

//                    int nIndex = ClsZfDatabase.GetZF_FileTypeIndex(m_pListFielType, cmbfiletype.Text);
//                    if (nIndex < 0) return;
//                    int filetype = m_pListFielType[nIndex].nCode;
//                    m_nQueryFileNum = 0;
//                    this.txtRecorders.Text = m_nQueryFileNum.ToString();
//                    int i=zdFile(filetype,RichTBoxSQL.Text, hwnd);
//                    this.txtRecorders.Text = m_nQueryFileNum.ToString();
//                    i += 1;
//                }
//            }
//        }


//        private void cmbfiletype_SelectedIndexChanged(object sender, EventArgs e)
//        {
//            int nIndex = ClsZfDatabase.GetZF_FileTypeIndex(m_pListFielType, cmbfiletype.Text);
//            if(nIndex <0) return;
//            _ZF_FILETYPE fileType= m_pListFielType[nIndex];
//            txtSaveFloder.Text = strSaveFileFloder + "\\" + fileType.strFileType + @" " + fileType.nCode;
//            zdStationID();
//        }

//        private void cmbStationID_SelectedIndexChanged(object sender, EventArgs e)
//        {
//            RichTBoxSQL.Clear();
//            RichTBoxSQL.Text = "uiStandID = " + cmbStationID.Text;
//        }

      
//        private void dataGridViewX1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
//        {
//            DataDownLoad(datagridzdFile, e.RowIndex,true);             
//        }
//        //数据下载
//        public bool DataDownLoad(DataGridViewX dataGrid,int rowIndex,bool bAddToMap)
//        {
//            try
//            {
//                DirectoryInfo path = new DirectoryInfo(txtSaveFloder.Text);
//                if (path == null) return false;
//                if (!path.Exists) path.Create();
               
//                string szfileName;
//                string szfilenames;
//                int FileTypes;
//                int TaskCode;
//                int ObjCode;
//                szfileName = txtSaveFloder.Text + "\\" + datagridzdFile.Rows[rowIndex].Cells[1].Value.ToString();
//                szfilenames = datagridzdFile.Rows[rowIndex].Cells[2].Value.ToString();
//                FileTypes = int.Parse(datagridzdFile.Rows[rowIndex].Cells[5].Value.ToString());
//                TaskCode = int.Parse(datagridzdFile.Rows[rowIndex].Cells[3].Value.ToString());
//                ObjCode = int.Parse(datagridzdFile.Rows[rowIndex].Cells[4].Value.ToString());
//                int i = zfget(szfileName, szfilenames, FileTypes, TaskCode, ObjCode);
//                if (i != 1)
//                {
//                    return false;
//                }
//                System.DateTime currentTime = new System.DateTime();
//                currentTime = System.DateTime.Now;
//                OleDbSQL con = new OleDbSQL();
//                string sql = "insert into FileDB(FileName,FilePath,ReceiveTime) values('" + System.IO.Path.GetFileName(szfileName) + "','" + szfileName + "','" + currentTime.ToString() + "')";
//                con.command(sql, con.connection(con.connectionPath()));//执行插入命令
//                if (szfileName.Contains(".tif") && bAddToMap)
//                {
//                    IRasterLayer pRasterFile = new RasterLayerClass();
//                    pRasterFile.CreateFromFilePath(szfileName);
//                    axMapCtlMain.AddLayer(pRasterFile as ILayer);
//                }
//                //MessageBox.Show("下载完毕", "提示", MessageBoxButtons.OK);
//            }
//            catch (System.Exception ex)
//            {
//                MessageBox.Show(ex.Message);
//            }
//            return true;
//        }

//        //下载查询到的所有数据
//        private void btnDownLoadAll_Click(object sender, EventArgs e)
//        {
//            int nCount = datagridzdFile.Rows.Count;
//            for (int i = 0; i < nCount;i++ )
//            {
//                datagridzdFile.Rows[i].Selected = true;
//                datagridzdFile.CurrentCell = datagridzdFile.Rows[i].Cells[0];
//                datagridzdFile.Refresh();
//                if (DataDownLoad(datagridzdFile, i, false))
//                {
//                    txtRecorders.Text = i.ToString() + @"/" + m_nQueryFileNum.ToString();
//                }
//                else
//                {
//                    txtRecorders.Text = i.ToString() + @"/" + m_nQueryFileNum.ToString() + "失败！";
//                }
//                txtRecorders.Refresh();

//                Thread.Sleep(1000);
//            }
//            MessageBox.Show("下载完毕");
//        }

//        private void btnSaveFloder_Click(object sender, EventArgs e)
//        {
//            FolderBrowserDialog fdlg = new FolderBrowserDialog();
//            fdlg.SelectedPath = @"D:\CE3Map";
//            if (fdlg.ShowDialog() != DialogResult.OK)
//            {
//                return;
//            }

//            if (fdlg.SelectedPath != "")
//            {
//                txtSaveFloder.Text = fdlg.SelectedPath;
//            }
//        }

//        private void toolopenfile_Click(object sender, EventArgs e)//右键菜单
//        {
//            if (barDataRv.SelectedDockTab==0)
//            {
//                string fileFullPath = "";
//                fileFullPath = dataGridRv.Rows[dataGridRvselectIndex].Cells[2].Value.ToString();
//                if (fileFullPath.Contains(".tif"))
//                {
//                    IMapControl3 pMapControlAdd = axMapCtlMain.Object as IMapControl3;
//                    IRasterLayer pRasterFileRv = new RasterLayerClass();
//                    pRasterFileRv.CreateFromFilePath(fileFullPath);
//                    pMapControlAdd.AddLayer(pRasterFileRv as ILayer);
//                }
//                else if (fileFullPath.Contains(".xml"))
//                {
//                }
//            }
//            else if (barDataRv.SelectedDockTab==1)
//            {
//                string fileFullPath = "";
//                fileFullPath = datagridLocal.Rows[dataGridLocalSelectIndex].Cells[2].Value.ToString();
//                if (fileFullPath.Contains(".tif"))
//                {
//                    IMapControl3 pMapControlAdd = axMapCtlMain.Object as IMapControl3;
//                    IRasterLayer pRasterFileRv = new RasterLayerClass();
//                    pRasterFileRv.CreateFromFilePath(fileFullPath);
//                    pMapControlAdd.AddLayer(pRasterFileRv as ILayer);
//                }
//                else if (fileFullPath.Contains(".xml"))
//                {
//                }
//            }
//        }

//        private void tooltransandopen_Click(object sender, EventArgs e)
//        {
//            string fileFullPath = "";

//            if (barDataRv.SelectedDockTab == 0)
//            {
//                //  string fileFullPath = "";
//                fileFullPath = dataGridRv.Rows[dataGridRvselectIndex].Cells[2].Value.ToString();
//            }
//            else if (barDataRv.SelectedDockTab == 1)
//            {
//                fileFullPath = datagridLocal.Rows[dataGridLocalSelectIndex].Cells[2].Value.ToString();
//            }
//            else
//                fileFullPath = string.Empty;

//            if (string.IsNullOrEmpty(fileFullPath))
//                return;

//            if (fileFullPath.Contains(".tif"))
//            {
//                FrmNEDtoENU frmN2E = new FrmNEDtoENU(axMapCtlMain.Object as IMapControl3, fileFullPath);

//                if (frmN2E.ShowDialog() == DialogResult.OK)
//                {
//                    //首先进行坐标系x,y变换
//                    //string fileExpTran;//进行x,y做表转换后输出的tiff文件存储路径，用这一文件在进行后期的Z转换
//                    //fileExpTran = System.IO.Path.GetDirectoryName(LayerExpName) +"\\"+ System.IO.Path.GetFileNameWithoutExtension(LayerExpName)+"XY.tif";
//                    try
//                    {
//                        string szSrcFilename = frmN2E.szSrcName;
//                        string szDstFilename = frmN2E.szDstName;

//                        ClsRasterOp pRasterOp = new ClsRasterOp();
//                        if (pRasterOp.NorthEastToEastNorth(szSrcFilename, szDstFilename))
//                        {
//                            RasterLayerClass rasterlayer = new RasterLayerClass();
//                            rasterlayer.CreateFromFilePath(szDstFilename);
//                            //IRaster2 pRaster2 = rasterlayer.Raster as IRaster2;
//                            //IRasterDataset2 pRasterDataset = pRaster2.RasterDataset as IRasterDataset2;
//                            //ChangeRasterValue(pRasterDataset, -1, 0);

//                            IMapControl3 pMapControl = axMapCtlMain.Object as IMapControl3;
//                            pMapControl.AddLayer(rasterlayer as ILayer);
//                            //this.Close();
//                        }

//                        ////LayerExpName = txtLayerExp.Text;
//                        //if (WorkMode == 0)
//                        //{

//                        //}
//                        //else
//                        //{
//                        //    if (pRasterOp.NorthEastToEastNorth(pRasterLayerMode1, LayerExpName))
//                        //    {
//                        //        RasterLayerClass rasterlayer = new RasterLayerClass();
//                        //        rasterlayer.CreateFromFilePath(LayerExpName);
//                        //        //IRaster2 pRaster2 = rasterlayer.Raster as IRaster2;
//                        //        //IRasterDataset2 pRasterDataset = pRaster2.RasterDataset as IRasterDataset2;
//                        //        //ChangeRasterValue(pRasterDataset, -1, 0);
//                        //        pMapControl.AddLayer(rasterlayer as ILayer);
//                        //        this.Close();
//                        //    }
//                        //}
//                    }
//                    catch (System.Exception ex)
//                    {
//                        MessageBox.Show(ex.Message);
//                    }
//                }
//            }
//            else if (fileFullPath.Contains(".xml"))
//            {
//            }
            
//            //else if()
//            //{
//            //    string fileFullPath = "";
//            //    fileFullPath = datagridLocal.Rows[dataGridLocalSelectIndex].Cells[2].Value.ToString();
//            //    if (fileFullPath.Contains(".tif"))
//            //    {
//            //        FrmNEDtoENU frmN2E = new FrmNEDtoENU(axMapCtlMain.Object as IMapControl3,fileFullPath);
//            //        frmN2E.ShowDialog();
//            //    }
//            //    else if (fileFullPath.Contains(".xml"))
//            //    {
//            //    }
//            //}
//        }

//        private void datagridLocal_CellContextMenuStripNeeded(object sender, DataGridViewCellContextMenuStripNeededEventArgs e)
//        {
//            datagridLocal.ClearSelection();
//            datagridLocal.Rows[e.RowIndex].Selected = true;
//            dataGridLocalSelectIndex = e.RowIndex;
//        }

//        private void dataGridRv_CellContextMenuStripNeeded(object sender, DataGridViewCellContextMenuStripNeededEventArgs e)
//        {
//            dataGridRv.ClearSelection();
//            dataGridRv.Rows[e.RowIndex].Selected = true;
//            dataGridRvselectIndex = e.RowIndex;
//        }
//#endregion
        

        /// <summary>
        /// 将mxd文件中所有图层的数据源文件和mxd工程文件打包，便于异地使用
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnmxdRAR_Click(object sender, EventArgs e)
        {
            FrmMxdRAR frmrar = new FrmMxdRAR();
            frmrar.ShowDialog();
        }

        /// <summary>
        /// 统计栅格区域中最大最小值，用于量测坑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMeaAttribute_Click(object sender, EventArgs e)
        {
            
        }

        private void btnProBackWard_Click(object sender, EventArgs e)
        {
            FrmProjectBackward frm = new FrmProjectBackward(axMapCtlMain.Object as IMapControl3);
            frm.ShowDialog();
        }

        private void btnJSCamLabel_Click(object sender, EventArgs e)
        {
            FrmJSCameraLabel frm = new FrmJSCameraLabel(this.axMapCtlMain.Object as IMapControl3);
            frm.ShowDialog();
        }

        private void axToolbarControlTinEdit_OnMouseDown(object sender, IToolbarControlEvents_OnMouseDownEvent e)
        {
           
        }

        private void btnSkyLinePara_Click(object sender, EventArgs e)
        {
            FrmSkylinePara frm = new FrmSkylinePara();
           
            if ( frm.ShowDialog()==DialogResult.OK)
            {
                Ex_OriPara exOriCamL = SkyandCamInWorldAnalyst(frm.dOriOmg, frm.dOriPhi, frm.dOriKap, frm.dPosX, frm.dPosY,
                                        frm.dPosZ, frm.dExpAngle, frm.dYawAngle, frm.dPitchAngle);
                frm.matlabCalc(exOriCamL);
            }
        }
       
        private Ex_OriPara SkyandCamInWorldAnalyst(double dOriOmg, double dOriPhi, double dOriKap, double dPosX, double dPosY,
                                                   double dPosZ,double dExpAngle, double dYawAngle, double dPitchAngle)
        {
            double[,] lbw = new double[3, 3];
            tagInstallMatrix matBase = new tagInstallMatrix();
            tagInstallMatrix matMastExp2Body = new tagInstallMatrix();
            tagInstallMatrix matMastYaw2Exp = new tagInstallMatrix();
            tagInstallMatrix matMastPitch2Yaw = new tagInstallMatrix();
            tagInstallMatrix matLCamBase2Pitch = new tagInstallMatrix();
            tagInstallMatrix matRCamBase2LCamBase = new tagInstallMatrix();
            tagInstallMatrix matLCamCap2Base = new tagInstallMatrix();
            tagInstallMatrix matRCamCap2Base = new tagInstallMatrix();
            Ex_OriPara exOriCamL = new Ex_OriPara();
            exOriCamL.ori.kap = 0;
            exOriCamL.ori.omg = 0;
            exOriCamL.ori.phi = 0;
            exOriCamL.pos.X = 0;
            exOriCamL.pos.Y = 0;
            exOriCamL.pos.Z = 0;

            Ex_OriPara exOriCamR = new Ex_OriPara();
            exOriCamR.ori.kap = 0;
            exOriCamR.ori.omg = 0;
            exOriCamR.ori.phi = 0;
            exOriCamR.pos.X = 0;
            exOriCamR.pos.Y = 0;
            exOriCamR.pos.Z = 0;

            //需要改到文件中
            att321_IRSAVer(Deg2Rad(dOriOmg), Deg2Rad(dOriPhi), Deg2Rad(dOriKap), ref lbw);
            unsafe
            {
                matBase.dPosMatrix[0] = dPosX;
                matBase.dPosMatrix[1] = dPosY;
                matBase.dPosMatrix[2] = dPosZ;
                matBase.dOriMatrix[0] = lbw[0, 0];
                matBase.dOriMatrix[1] = lbw[0, 1];
                matBase.dOriMatrix[2] = lbw[0, 2];
                matBase.dOriMatrix[3] = lbw[1, 0];
                matBase.dOriMatrix[4] = lbw[1, 1];
                matBase.dOriMatrix[5] = lbw[1, 2];
                matBase.dOriMatrix[6] = lbw[2, 0];
                matBase.dOriMatrix[7] = lbw[2, 1];
                matBase.dOriMatrix[8] = lbw[2, 2];

                matMastExp2Body.dPosMatrix[0] = 535.329;
                matMastExp2Body.dPosMatrix[1] = -49.147;
                matMastExp2Body.dPosMatrix[2] = -554.806;
                matMastExp2Body.dOriMatrix[0] = 0.2912;
                matMastExp2Body.dOriMatrix[1] = 90.0410;
                matMastExp2Body.dOriMatrix[2] = 90.2883;
                matMastExp2Body.dOriMatrix[3] = 89.9582;
                matMastExp2Body.dOriMatrix[4] = 0.1624;
                matMastExp2Body.dOriMatrix[5] = 89.8431;
                matMastExp2Body.dOriMatrix[6] = 89.7118;
                matMastExp2Body.dOriMatrix[7] = 90.1571;
                matMastExp2Body.dOriMatrix[8] = 0.3283;

                matMastYaw2Exp.dPosMatrix[0] = -85.259;
                matMastYaw2Exp.dPosMatrix[1] = 0.00000;
                matMastYaw2Exp.dPosMatrix[2] = 0.000;
                matMastYaw2Exp.dOriMatrix[0] = 0;
                matMastYaw2Exp.dOriMatrix[1] = 90;
                matMastYaw2Exp.dOriMatrix[2] = 90;
                matMastYaw2Exp.dOriMatrix[3] = 90;
                matMastYaw2Exp.dOriMatrix[4] = 0.0737;
                matMastYaw2Exp.dOriMatrix[5] = 90.0737;
                matMastYaw2Exp.dOriMatrix[6] = 90;
                matMastYaw2Exp.dOriMatrix[7] = 89.9263;
                matMastYaw2Exp.dOriMatrix[8] = 0.0737;

                matMastPitch2Yaw.dPosMatrix[0] = -0.236;
                matMastPitch2Yaw.dPosMatrix[1] = -0.0010;
                matMastPitch2Yaw.dPosMatrix[2] = -647.459;
                matMastPitch2Yaw.dOriMatrix[0] = 0.14630;
                matMastPitch2Yaw.dOriMatrix[1] = 89.8537;
                matMastPitch2Yaw.dOriMatrix[2] = 90.0000;
                matMastPitch2Yaw.dOriMatrix[3] = 90.1462;
                matMastPitch2Yaw.dOriMatrix[4] = 0.3219;
                matMastPitch2Yaw.dOriMatrix[5] = 90.2867;
                matMastPitch2Yaw.dOriMatrix[6] = 90.0007;
                matMastPitch2Yaw.dOriMatrix[7] = 89.7133;
                matMastPitch2Yaw.dOriMatrix[8] = 0.2867;

                matLCamBase2Pitch.dPosMatrix[0] = 72.246;
                matLCamBase2Pitch.dPosMatrix[1] = -109.188;
                matLCamBase2Pitch.dPosMatrix[2] = -124.709;
                matLCamBase2Pitch.dOriMatrix[0] = 90.0692;
                matLCamBase2Pitch.dOriMatrix[1] = 0.1372;
                matLCamBase2Pitch.dOriMatrix[2] = 89.8815;
                matLCamBase2Pitch.dOriMatrix[3] = 90.2061;
                matLCamBase2Pitch.dOriMatrix[4] = 90.1187;
                matLCamBase2Pitch.dOriMatrix[5] = 0.2379;
                matLCamBase2Pitch.dOriMatrix[6] = 0.2174;
                matLCamBase2Pitch.dOriMatrix[7] = 89.9312;
                matLCamBase2Pitch.dOriMatrix[8] = 89.7937;

                matRCamBase2LCamBase.dPosMatrix[0] = 269.62;
                matRCamBase2LCamBase.dPosMatrix[1] = 0.51;
                matRCamBase2LCamBase.dPosMatrix[2] = -0.79;
                matRCamBase2LCamBase.dOriMatrix[0] = 0.3172;
                matRCamBase2LCamBase.dOriMatrix[1] = 89.9953;
                matRCamBase2LCamBase.dOriMatrix[2] = 89.6829;
                matRCamBase2LCamBase.dOriMatrix[3] = 90.0047;
                matRCamBase2LCamBase.dOriMatrix[4] = 0.0051;
                matRCamBase2LCamBase.dOriMatrix[5] = 89.9981;
                matRCamBase2LCamBase.dOriMatrix[6] = 90.3146;
                matRCamBase2LCamBase.dOriMatrix[7] = 90.0019;
                matRCamBase2LCamBase.dOriMatrix[8] = 0.3146;

                matLCamCap2Base.dPosMatrix[0] = 22.3469;
                matLCamCap2Base.dPosMatrix[1] = 27.65682;
                matLCamCap2Base.dPosMatrix[2] = -5.33906;
                matLCamCap2Base.dOriMatrix[0] = 0;
                matLCamCap2Base.dOriMatrix[1] = 90;
                matLCamCap2Base.dOriMatrix[2] = 90;
                matLCamCap2Base.dOriMatrix[3] = 90;
                matLCamCap2Base.dOriMatrix[4] = 0;
                matLCamCap2Base.dOriMatrix[5] = 90;
                matLCamCap2Base.dOriMatrix[6] = 90;
                matLCamCap2Base.dOriMatrix[7] = 90;
                matLCamCap2Base.dOriMatrix[8] = 0;

                matRCamCap2Base.dPosMatrix[0] = 22.863;
                matRCamCap2Base.dPosMatrix[1] = 30.159;
                matRCamCap2Base.dPosMatrix[2] = -6.573;
                matRCamCap2Base.dOriMatrix[0] = 0;
                matRCamCap2Base.dOriMatrix[1] = 90;
                matRCamCap2Base.dOriMatrix[2] = 90;
                matRCamCap2Base.dOriMatrix[3] = 90;
                matRCamCap2Base.dOriMatrix[4] = 0;
                matRCamCap2Base.dOriMatrix[5] = 90;
                matRCamCap2Base.dOriMatrix[6] = 90;
                matRCamCap2Base.dOriMatrix[7] = 90;
                matRCamCap2Base.dOriMatrix[8] = 0;

                //matMastExp2Body.dPosMatrix[0] = 533.204;
                //matMastExp2Body.dPosMatrix[1] = -1.5296;
                //matMastExp2Body.dPosMatrix[2] = -570.7493;
                //matMastExp2Body.dOriMatrix[0] = 0.1704;
                //matMastExp2Body.dOriMatrix[1] = 89.8822;
                //matMastExp2Body.dOriMatrix[2] = 89.8769;
                //matMastExp2Body.dOriMatrix[3] = 90.1175;
                //matMastExp2Body.dOriMatrix[4] = 0.1934;
                //matMastExp2Body.dOriMatrix[5] = 90.1535;
                //matMastExp2Body.dOriMatrix[6] = 90.1234;
                //matMastExp2Body.dOriMatrix[7] = 89.8467;
                //matMastExp2Body.dOriMatrix[8] = 0.1968;

                //matMastYaw2Exp.dPosMatrix[0] = -85.0554;
                //matMastYaw2Exp.dPosMatrix[1] = -47.9819;
                //matMastYaw2Exp.dPosMatrix[2] = 0;
                //matMastYaw2Exp.dOriMatrix[0] = 0;
                //matMastYaw2Exp.dOriMatrix[1] = 90;
                //matMastYaw2Exp.dOriMatrix[2] = 90;
                //matMastYaw2Exp.dOriMatrix[3] = 90;
                //matMastYaw2Exp.dOriMatrix[4] = 0.051;
                //matMastYaw2Exp.dOriMatrix[5] = 89.9490;
                //matMastYaw2Exp.dOriMatrix[6] = 90;
                //matMastYaw2Exp.dOriMatrix[7] = 90.0510;
                //matMastYaw2Exp.dOriMatrix[8] = 0.0510;

                //matMastPitch2Yaw.dPosMatrix[0] = 0.3225;
                //matMastPitch2Yaw.dPosMatrix[1] = -0.0005;
                //matMastPitch2Yaw.dPosMatrix[2] = -649.3588;
                //matMastPitch2Yaw.dOriMatrix[0] = 0.093;
                //matMastPitch2Yaw.dOriMatrix[1] = 90.0930;
                //matMastPitch2Yaw.dOriMatrix[2] = 90.0001;
                //matMastPitch2Yaw.dOriMatrix[3] = 89.9070;
                //matMastPitch2Yaw.dOriMatrix[4] = 0.1554;
                //matMastPitch2Yaw.dOriMatrix[5] = 89.8755;
                //matMastPitch2Yaw.dOriMatrix[6] = 90.0002;
                //matMastPitch2Yaw.dOriMatrix[7] = 90.1245;
                //matMastPitch2Yaw.dOriMatrix[8] = 0.1245;

                //matLCamBase2Pitch.dPosMatrix[0] = 69.8878;
                //matLCamBase2Pitch.dPosMatrix[1] = -109.0252;
                //matLCamBase2Pitch.dPosMatrix[2] = -102.9593;
                //matLCamBase2Pitch.dOriMatrix[0] = 90.1032;
                //matLCamBase2Pitch.dOriMatrix[1] = 0.2726;
                //matLCamBase2Pitch.dOriMatrix[2] = 90.2523;
                //matLCamBase2Pitch.dOriMatrix[3] = 90.3158;
                //matLCamBase2Pitch.dOriMatrix[4] = 89.7483;
                //matLCamBase2Pitch.dOriMatrix[5] = 0.4038;
                //matLCamBase2Pitch.dOriMatrix[6] = 0.3357;
                //matLCamBase2Pitch.dOriMatrix[7] = 89.8850;
                //matLCamBase2Pitch.dOriMatrix[8] = 89.6846;

                //matRCamBase2LCamBase.dPosMatrix[0] = 269.91;
                //matRCamBase2LCamBase.dPosMatrix[1] = -0.17;
                //matRCamBase2LCamBase.dPosMatrix[2] = -0.45;
                //matRCamBase2LCamBase.dOriMatrix[0] = 0.2988;
                //matRCamBase2LCamBase.dOriMatrix[1] = 90.0116;
                //matRCamBase2LCamBase.dOriMatrix[2] = 89.7014;
                //matRCamBase2LCamBase.dOriMatrix[3] = 89.9886;
                //matRCamBase2LCamBase.dOriMatrix[4] = 0.0312;
                //matRCamBase2LCamBase.dOriMatrix[5] = 89.9710;
                //matRCamBase2LCamBase.dOriMatrix[6] = 90.2944;
                //matRCamBase2LCamBase.dOriMatrix[7] = 90.0289;
                //matRCamBase2LCamBase.dOriMatrix[8] = 0.2959;

                //matLCamCap2Base.dPosMatrix[0] = 24.13066375;
                //matLCamCap2Base.dPosMatrix[1] = 22.869995;
                //matLCamCap2Base.dPosMatrix[2] = -2.82183125;
                //matLCamCap2Base.dOriMatrix[0] = 0;
                //matLCamCap2Base.dOriMatrix[1] = 90;
                //matLCamCap2Base.dOriMatrix[2] = 90;
                //matLCamCap2Base.dOriMatrix[3] = 90;
                //matLCamCap2Base.dOriMatrix[4] = 0;
                //matLCamCap2Base.dOriMatrix[5] = 90;
                //matLCamCap2Base.dOriMatrix[6] = 90;
                //matLCamCap2Base.dOriMatrix[7] = 90;
                //matLCamCap2Base.dOriMatrix[8] = 0;

                //matRCamCap2Base.dPosMatrix[0] = 22.863;
                //matRCamCap2Base.dPosMatrix[1] = 30.159;
                //matRCamCap2Base.dPosMatrix[2] = -6.573;
                //matRCamCap2Base.dOriMatrix[0] = 0;
                //matRCamCap2Base.dOriMatrix[1] = 90;
                //matRCamCap2Base.dOriMatrix[2] = 90;
                //matRCamCap2Base.dOriMatrix[3] = 90;
                //matRCamCap2Base.dOriMatrix[4] = 0;
                //matRCamCap2Base.dOriMatrix[5] = 90;
                //matRCamCap2Base.dOriMatrix[6] = 90;
                //matRCamCap2Base.dOriMatrix[7] = 90;
                //matRCamCap2Base.dOriMatrix[8] = 0;
            }
             mlCalcCamOriInWorldByGivenInsMatPosX(matBase, dExpAngle, dYawAngle, dPitchAngle,
                                                                 matMastExp2Body, matMastYaw2Exp, matMastPitch2Yaw,
                                                                  matLCamBase2Pitch, matRCamBase2LCamBase,
                                                                 matLCamCap2Base, matRCamCap2Base,
                                                                 out exOriCamL, out exOriCamR);
             return exOriCamL;
        }
        
       
        private void btnAddModelFromXmlFile_Click(object sender, EventArgs e)
        {
#region 必须有DOM的时候才能执行的代码 
            //FrmAddModelToTerrain f = new FrmAddModelToTerrain(axMapCtlMain.Map);
            //try
            //{
            //    if (f.ShowDialog() == DialogResult.OK)
            //    {
            //        //得到RASTER的路径
            //        string rasterfilepath = m_EditRasterLayer.FilePath;//选中图层的文件路径
            //        string rasterfiledir = System.IO.Path.GetDirectoryName(rasterfilepath);//文件位置
            //        string rasterfilename = System.IO.Path.GetFileNameWithoutExtension(rasterfilepath);//文件名称

            //        //得到纹理影像
            //        string domFilename = rasterfilename + "_dom" + System.IO.Path.GetExtension(rasterfilepath);
            //        string domFilePath = rasterfiledir + "\\" + domFilename;

            //        //得到撞击坑样本影像
            //        string domClipFilename = rasterfilename + "_dom_clip" + System.IO.Path.GetExtension(rasterfilepath);
            //        string domClipFilePath = rasterfiledir + "\\" + domClipFilename;

            //        string mergedemfilename = "";
            //        string mergedemfilepath = "";
            //        try
            //        {
            //            DateTime dt = DateTime.Now;
            //            string szAppendix = "_" + dt.Year.ToString() + dt.Month.ToString() + dt.Day.ToString() + "_" + dt.Hour.ToString() + dt.Minute.ToString() + dt.Second.ToString();

            //            mergedemfilename = rasterfilename + szAppendix + System.IO.Path.GetExtension(rasterfilepath);
            //            mergedemfilepath = rasterfiledir + "\\" + mergedemfilename;
            //            File.Copy(rasterfilepath, mergedemfilepath, false);

            //            string mergeDomfilename = rasterfilename + szAppendix + "_dom" + System.IO.Path.GetExtension(rasterfilepath);
            //            string mergeDomfilepath = rasterfiledir + "\\" + mergeDomfilename;
            //            File.Copy(domFilePath, mergeDomfilepath);

            //            string mergeDomClipfilename = rasterfilename + szAppendix + "_dom_clip" + System.IO.Path.GetExtension(rasterfilepath);
            //            string mergeDomClipfilepath = rasterfiledir + "\\" + mergeDomClipfilename;
            //            File.Copy(domClipFilePath, mergeDomClipfilepath);

            //            //for (int i = 1; ; i++)
            //            //{
            //            //    mergedemfilename = rasterfilename + "_merge" + i.ToString() + System.IO.Path.GetExtension(rasterfilepath);
            //            //    mergedemfilepath = rasterfiledir + "\\" + mergedemfilename;
            //            //    if (File.Exists(mergedemfilepath) == false)
            //            //    {
            //            //        File.Copy(rasterfilepath, mergedemfilepath, false);
            //            //        string mergeDomfilename = rasterfilename + "_merge" + i.ToString() + "_dom" + System.IO.Path.GetExtension(rasterfilepath);
            //            //        string mergeDomfilepath = rasterfiledir + "\\" + mergeDomfilename;
            //            //        File.Copy(domFilePath, mergeDomfilepath);
            //            //        break;
            //            //    }

            //            //}
            //        }
            //        catch (System.Exception ex)
            //        {
            //            MessageBox.Show(ex.Message);
            //            return;
            //        }
            //        IRasterLayer pMergeLayer = new RasterLayerClass();
            //        pMergeLayer.CreateFromFilePath(mergedemfilepath);
            //        IRaster pRaster = pMergeLayer.Raster;
            //        if (pRaster == null)
            //            return;

            //        ClsAddModelToTerrain pAddModel = new ClsAddModelToTerrain();
            //        pAddModel.pProgressbar = progressBarXMain;
            //        pAddModel.listModelInfo = m_manualAddModels;
            //        if (pAddModel.addModelToTerrain(f.m_pXmlFilename, pMergeLayer))
            //            MessageBox.Show(mergedemfilepath + "图层更新成功！");
            //        else
            //            MessageBox.Show("图层更新失败！");

            //        //更新图层
            //        axMapCtlMain.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
            //    }
            //}
            //catch (SystemException ee)
            //{
            //    MessageBox.Show(ee.Message);
            //}
#endregion

#region 只有DEM的时候就能执行的代码
            FrmAddModelToTerrain f = new FrmAddModelToTerrain(axMapCtlMain.Map);
            try
            {
                if (f.ShowDialog() == DialogResult.OK)
                {
                    //得到RASTER的路径
                    string rasterfilepath = m_EditRasterLayer.FilePath;//选中图层的文件路径
                    //string rasterfiledir = System.IO.Path.GetDirectoryName(rasterfilepath);//文件位置
                    //string rasterfilename = System.IO.Path.GetFileNameWithoutExtension(rasterfilepath);//文件名称
                    //mergedemfilepath = rasterfiledir + "\\" + mergedemfilename;
                    //////得到纹理影像
                    ////string domFilename = rasterfilename + "_dom" + System.IO.Path.GetExtension(rasterfilepath);
                    ////string domFilePath = rasterfiledir + "\\" + domFilename;

                    //////得到撞击坑样本影像
                    ////string domClipFilename = rasterfilename + "_dom_clip" + System.IO.Path.GetExtension(rasterfilepath);
                    ////string domClipFilePath = rasterfiledir + "\\" + domClipFilename;

                    //string mergedemfilename = "";
                    //string mergedemfilepath = "";
                    //try
                    //{
                    //    DateTime dt = DateTime.Now;
                    //    string szAppendix = "_" + dt.Year.ToString() + dt.Month.ToString() + dt.Day.ToString() + "_" + dt.Hour.ToString() + dt.Minute.ToString() + dt.Second.ToString();

                    //    mergedemfilename = rasterfilename + szAppendix + System.IO.Path.GetExtension(rasterfilepath);
                    //    mergedemfilepath = rasterfiledir + "\\" + mergedemfilename;
                    //    File.Copy(rasterfilepath, mergedemfilepath, false);

                    //    //string mergeDomfilename = rasterfilename + szAppendix + "_dom" + System.IO.Path.GetExtension(rasterfilepath);
                    //    //string mergeDomfilepath = rasterfiledir + "\\" + mergeDomfilename;
                    //    //File.Copy(domFilePath, mergeDomfilepath);

                    //    //string mergeDomClipfilename = rasterfilename + szAppendix + "_dom_clip" + System.IO.Path.GetExtension(rasterfilepath);
                    //    //string mergeDomClipfilepath = rasterfiledir + "\\" + mergeDomClipfilename;
                    //    //File.Copy(domClipFilePath, mergeDomClipfilepath);

                    //    //for (int i = 1; ; i++)
                    //    //{
                    //    //    mergedemfilename = rasterfilename + "_merge" + i.ToString() + System.IO.Path.GetExtension(rasterfilepath);
                    //    //    mergedemfilepath = rasterfiledir + "\\" + mergedemfilename;
                    //    //    if (File.Exists(mergedemfilepath) == false)
                    //    //    {
                    //    //        File.Copy(rasterfilepath, mergedemfilepath, false);
                    //    //        string mergeDomfilename = rasterfilename + "_merge" + i.ToString() + "_dom" + System.IO.Path.GetExtension(rasterfilepath);
                    //    //        string mergeDomfilepath = rasterfiledir + "\\" + mergeDomfilename;
                    //    //        File.Copy(domFilePath, mergeDomfilepath);
                    //    //        break;
                    //    //    }

                    //    //}
                    //}
                    //catch (System.Exception ex)
                    //{
                    //    MessageBox.Show(ex.Message);
                    //    return;
                    //}
                    IRasterLayer pMergeLayer = new RasterLayerClass();
                    pMergeLayer.CreateFromFilePath(rasterfilepath);
                    IRaster pRaster = pMergeLayer.Raster;
                    if (pRaster == null)
                        return;

                    ClsAddModelToTerrain pAddModel = new ClsAddModelToTerrain();
                    pAddModel.pProgressbar = progressBarXMain;
                    pAddModel.listModelInfo = m_manualAddModels;
                    if (pAddModel.addModelToTerrain(f.m_pXmlFilename, pMergeLayer))
                        MessageBox.Show(rasterfilepath + "图层更新成功！");
                    else
                        MessageBox.Show("图层更新失败！");

                    //加载图层
                    ClsGDBDataCommon GDC = new ClsGDBDataCommon();
                    IWorkspace pWS = GDC.OpenFromFileGDB(GetParentPathofExe() + @"Resource\DefaultFileGDB\MultiPatchFile.gdb");
                    IFeatureClass CraterFeatureClass = ((IFeatureWorkspace)pWS).OpenFeatureClass("Crater");
                    IFeatureClass NonCraterFeatureClass = ((IFeatureWorkspace)pWS).OpenFeatureClass("NonCrater");
                                 
                    for (int i = 0; i < axMapCtlMain.Map.LayerCount; i++)
                    {
                        ILayer player = axMapCtlMain.Map.get_Layer(i);
                        if (player.Name == "Crater" || player.Name == "NonCrater")
                        {
                            axMapCtlMain.DeleteLayer(i);
                            i--;
                        }
                    }

                    IFeatureLayer pfeaturelayer = new FeatureLayerClass();
                    pfeaturelayer.FeatureClass = CraterFeatureClass;
                    pfeaturelayer.Name = "Crater";
                    axMapCtlMain.Map.AddLayer(pfeaturelayer as ILayer);
                    pfeaturelayer = new FeatureLayerClass();
                    pfeaturelayer.FeatureClass = NonCraterFeatureClass;
                    pfeaturelayer.Name = "NonCrater";
                    axMapCtlMain.Map.AddLayer(pfeaturelayer as ILayer);

                    //更新图层
                    axMapCtlMain.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
                    axMapCtlMain_OnAfterDraw(null, null);
                }
            }
            catch (SystemException ee)
            {
                MessageBox.Show(ee.Message);
            }
#endregion
        }
        private void buttonItemAddMultiPatchLayer_Click(object sender, EventArgs e)
        {
            if (m_EditRasterLayer == null)
            {
                MessageBox.Show("请先指定目标栅格图层!");
                return;
            }
            ClsGDBDataCommon GDC = new ClsGDBDataCommon();
            IWorkspace pWS = GDC.OpenFromFileGDB(GetParentPathofExe() + @"Resource\DefaultFileGDB\MultiPatchFile.gdb");
            IFeatureClass CraterFeatureClass = ((IFeatureWorkspace)pWS).OpenFeatureClass("Crater");
            IFeatureClass NonCraterFeatureClass = ((IFeatureWorkspace)pWS).OpenFeatureClass("NonCrater");
            ITable pTable = CraterFeatureClass as ITable;
            ITable pTable2 = NonCraterFeatureClass as ITable;
            if (pTable.RowCount(null) > 0 || pTable2.RowCount(null) > 0)
            {
                if (MessageBox.Show("当前图层已有数据，是否需要清空？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2)
                    == DialogResult.Yes)
                {
                    //清空当前记录的所有模型参数信息
                    m_manualAddModels.Clear();

                    //清空所有对象
                    IFeatureCursor pFeatureCursor = CraterFeatureClass.Search(null, false);
                    IFeature pFeature = pFeatureCursor.NextFeature();
                    while (pFeature != null)
                    {
                        pFeature.Delete();
                        pFeature = pFeatureCursor.NextFeature();
                    }
                    pFeatureCursor = NonCraterFeatureClass.Search(null, false);
                    pFeature = pFeatureCursor.NextFeature();
                    while (pFeature != null)
                    {
                        pFeature.Delete();
                        pFeature = pFeatureCursor.NextFeature();
                    }
                }
            }
            for (int i = 0; i < axMapCtlMain.Map.LayerCount; i++)
            {
                ILayer player = axMapCtlMain.Map.get_Layer(i);
                if (player.Name == "Crater" || player.Name == "NonCrater")
                {
                    axMapCtlMain.DeleteLayer(i);
                    i--;
                }
            }
            IFeatureLayer pfeaturelayer = new FeatureLayerClass();
            pfeaturelayer.FeatureClass = CraterFeatureClass;
            pfeaturelayer.Name = "Crater";
            axMapCtlMain.Map.AddLayer(pfeaturelayer as ILayer);
            pfeaturelayer = new FeatureLayerClass();
            pfeaturelayer.FeatureClass = NonCraterFeatureClass;
            pfeaturelayer.Name = "NonCrater";
            axMapCtlMain.Map.AddLayer(pfeaturelayer as ILayer);
            axMapCtlMain.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
            axMapCtlMain_OnAfterDraw(null, null);

            for (int i = 0; i < comboBoxExCrater.Items.Count; i++)
            {
                if (comboBoxExCrater.Items[i].ToString() == "Crater")
                {
                    comboBoxExCrater.SelectedIndex = i;
                    break;
                }
            }
            for (int i = 0; i < comboBoxExNonCrater.Items.Count; i++)
            {
                if (comboBoxExNonCrater.Items[i].ToString() == "NonCrater")
                {
                    comboBoxExNonCrater.SelectedIndex = i;
                    break;
                }
            }
        }

        private void btnMerge_Click(object sender, EventArgs e)
        {
            ClsAddModelToTerrain addModel = new ClsAddModelToTerrain();
            addModel.pProgressbar = progressBarXMain;
            //更新MultiPatch
            //if (!addModel.UpdateMultiPatchFromModel(listModelInfo, pRaster))
            //    return;
            if (m_EditRasterLayer == null)
            {
                MessageBox.Show("未指定地形图层！");
                return;
            }

            IFeatureClass nonCraterClass = null;
            IFeatureClass craterClass = null;
            try
            {
                ClsGDBDataCommon GDC = new ClsGDBDataCommon();
                IWorkspace pWS = GDC.OpenFromFileGDB(GetParentPathofExe() + @"Resource\DefaultFileGDB\MultiPatchFile.gdb");
                IFeatureClass CraterFeatureClass = ((IFeatureWorkspace)pWS).OpenFeatureClass("Crater");
                IFeatureClass NonCraterFeatureClass = ((IFeatureWorkspace)pWS).OpenFeatureClass("NonCrater");
                
                if (m_NonCraterLayer != null)
                    nonCraterClass = m_NonCraterLayer.FeatureClass;
                else
                    nonCraterClass = NonCraterFeatureClass;

                if (m_CraterLayer != null)
                    craterClass = m_CraterLayer.FeatureClass;
                else
                    craterClass = CraterFeatureClass;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }         

            //得到RASTER的路径
            string rasterfilepath = m_EditRasterLayer.FilePath;//选中图层的文件路径
            string rasterfiledir = System.IO.Path.GetDirectoryName(rasterfilepath);//文件位置
            string rasterfilename = System.IO.Path.GetFileNameWithoutExtension(rasterfilepath);//文件名称

            //得到纹理影像
            string domFilename = rasterfilename + "_dom" + System.IO.Path.GetExtension(rasterfilepath);
            string domFilePath = rasterfiledir + "\\" + domFilename;

            //得到撞击坑样本影像
            string domClipFilename = rasterfilename + "_dom_clip" + System.IO.Path.GetExtension(rasterfilepath);
            string domClipFilePath = rasterfiledir + "\\" + domClipFilename;

            string mergedemfilename = "";
            string mergedemfilepath = "";
            try
            {
                DateTime dt = DateTime.Now;
                string szAppendix = "_" + dt.Year.ToString() + dt.Month.ToString() + dt.Day.ToString() + "_" + dt.Hour.ToString() + dt.Minute.ToString() + dt.Second.ToString();

                mergedemfilename = rasterfilename + szAppendix + System.IO.Path.GetExtension(rasterfilepath);
                mergedemfilepath = rasterfiledir + "\\" + mergedemfilename;
                File.Copy(rasterfilepath, mergedemfilepath, false);

                if (File.Exists(domFilePath))
                {
                    string mergeDomfilename = rasterfilename + szAppendix + "_dom" + System.IO.Path.GetExtension(rasterfilepath);
                    string mergeDomfilepath = rasterfiledir + "\\" + mergeDomfilename;
                    File.Copy(domFilePath, mergeDomfilepath);
                }

                if (File.Exists(domClipFilePath))
                {
                    string mergeDomClipfilename = rasterfilename + szAppendix + "_dom_clip" + System.IO.Path.GetExtension(rasterfilepath);
                    string mergeDomClipfilepath = rasterfiledir + "\\" + mergeDomClipfilename;
                    File.Copy(domClipFilePath, mergeDomClipfilepath);
                }

                //for (int i = 1; ; i++)
                //{
                //    mergedemfilename = rasterfilename + "_merge" + i.ToString() + System.IO.Path.GetExtension(rasterfilepath);
                //    mergedemfilepath = rasterfiledir + "\\" + mergedemfilename;
                //    if (File.Exists(mergedemfilepath) == false)
                //    {
                //        File.Copy(rasterfilepath, mergedemfilepath, false);
                //        string mergeDomfilename = rasterfilename + "_merge" + i.ToString() + "_dom" + System.IO.Path.GetExtension(rasterfilepath);
                //        string mergeDomfilepath = rasterfiledir + "\\" + mergeDomfilename;
                //        File.Copy(domFilePath, mergeDomfilepath);
                //        break;
                //    }

                //}
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            IRasterLayer pMergeLayer = new RasterLayerClass();
            pMergeLayer.CreateFromFilePath(mergedemfilepath);
            IRaster pRaster = pMergeLayer.Raster;
            if (pRaster == null)
                return;

            //融合
            if (!addModel.MergeWithRaster(pMergeLayer, craterClass, nonCraterClass))
                return;
            IRasterBandCollection pRC = pRaster as IRasterBandCollection;
            IRasterBand pBand = pRC.Item(0);
            pBand.ComputeStatsAndHist();
            IRasterEdit pre = pRaster as IRasterEdit;
            pre.Refresh();
            MessageBox.Show(mergedemfilepath + "融合成功！");
        }

        private void btnChooseNonCrater_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlgExistModelFile = new OpenFileDialog();
            dlgExistModelFile.Title = "选择已有模型：";
            dlgExistModelFile.InitialDirectory = ".";
            dlgExistModelFile.Filter = "3DS文件(*.3DS;*.3ds)|*.3DS;*.3ds|所有文件(*.*)|*.*";
            dlgExistModelFile.RestoreDirectory = true;

            if (dlgExistModelFile.ShowDialog() == DialogResult.OK)
            {
                txtExistingNonCrater.Text = dlgExistModelFile.FileName;
            }
        }

        private void btnProjectBackward_Click(object sender, EventArgs e)
        {
            FrmProjectBackward frmProjectBackward = new FrmProjectBackward(axMapCtlMain.Object as IMapControl3);
            if (DialogResult.OK == frmProjectBackward.ShowDialog())
            {

            }
        }

        private void btnXmlEditor_Click(object sender, EventArgs e)
        {
            FrmXmlEditor pXmlEditor = new FrmXmlEditor(m_EditRasterLayer);
            
            //添加当前添加的模型
            pXmlEditor.ListModels = m_manualAddModels;
            if (DialogResult.OK == pXmlEditor.ShowDialog())
            {
                axMapCtlMain.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
            }
        }

        private void axToolbarControlTinEdit_OnItemClick(object sender, IToolbarControlEvents_OnItemClickEvent e)
        {
            //if (e.index == 12)
            {
                axMapCtlMain.Focus();
            }
        }    
        private void axToolbarControlCommon_OnItemClick(object sender, IToolbarControlEvents_OnItemClickEvent e)
        {

        }

        private void btnbarright_Click(object sender, EventArgs e)
        {
            barRight.Visible = btnbarright.Checked;
        }


        private void menuView_Click(object sender, EventArgs e)
        {

        }

        private void contextMenuStripToolbar_Opening(object sender, CancelEventArgs e)
        {
            
        }

        private void popupBarCommon_Click(object sender, EventArgs e)
        {
            barCommon.Visible = popupBarCommon.Checked;
        }

        private void popupBarLayout_Click(object sender, EventArgs e)
        {
            barLayout.Visible = popupBarLayout.Checked;
        }

        private void popupBar3D_Click(object sender, EventArgs e)
        {
            bar3D.Visible = popupBar3D.Checked;
        }

        private void popupBarEditor_Click(object sender, EventArgs e)
        {
            barEditor.Visible = popupBarEditor.Checked;
        }

        private void popupBarTinEditor_Click(object sender, EventArgs e)
        {
            barTinEditor.Visible = popupBarTinEditor.Checked;
        }

        private void popupBarGeoAdjust_Click(object sender, EventArgs e)
        {
            barGeoAdjust.Visible = popupBarGeoAdjust.Checked;
        }

        private void popupBarSunalt_Click(object sender, EventArgs e)
        {
            barSunAlt.Visible = popupBarSunalt.Checked;
        }

        private void popupBarEffects_Click(object sender, EventArgs e)
        {
            barEffects.Visible = popupBarEffects.Checked;
        }

        private void popupBarCustom_Click(object sender, EventArgs e)
        {
            barCustom.Visible = popupBarCustom.Checked;
        }
        private void popupBarGeoReference_Click(object sender, EventArgs e)
        {
            barGeoReference.Visible = popupBarGeoReference.Checked;
        }
        private void contextMenubar_Opening(object sender, CancelEventArgs e)
        {
            popupBarCustom.Checked = barCustom.Visible;
            popupBarCommon.Checked = barCommon.Visible;
            popupBarLayout.Checked = barLayout.Visible;
            popupBar3D.Checked = bar3D.Visible;
            popupBarSunalt.Checked = barSunAlt.Visible;
            popupBarTinEditor.Checked = barTinEditor.Visible;
            popupBarEditor.Checked = barEditor.Visible;
            popupBarGeoAdjust.Checked = barGeoAdjust.Visible;
            popupBarEffects.Checked = barEffects.Visible;
            popupBarGeoReference.Checked = barGeoReference.Visible;
        }

        private void RefreshView(AxTOCControl pTocControl,ILayer pLayer)
        {
            if (pTocControl.Buddy is IMapControl3)//.Equals(axMapCtlMain.Object)
            {
                IMapControl3 pMapControl = (IMapControl3)pTocControl.Buddy;
                pMapControl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
                pTocControl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
            }
            else if (axTOCCtlLayer.Buddy is ISceneControl)
            {
                ISceneControl pSceneControl = (ISceneControl)pTocControl.Buddy;
                IActiveView pActiveView = pSceneControl.Scene as IActiveView;
                pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, pLayer, null);
                pTocControl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
            }
            else if (axTOCCtlLayer.Buddy is IPageLayoutControl)
            {
                IPageLayoutControl pPageLayoutControl = (IPageLayoutControl)pTocControl.Buddy;
                pTocControl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
                pPageLayoutControl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);              
            }
        }

        private void cmbTargetRasterLayer_Click(object sender, EventArgs e)
        {
            
        }

        private void cmBoxEffectsLayer_Click(object sender, EventArgs e)
        {
            
        }

        private void cmBoxEffectsLayer_SelectedIndexChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < axMapCtlMain.LayerCount; i++)
            {
                if (cmBoxEffectsLayer.Text == axMapCtlMain.get_Layer(i).Name)
                {
                    pLayerEffects = axMapCtlMain.get_Layer(i);
                    if (dockMap.Selected == false)
                    {
                        dockMap.Selected = true;
                        axMapCtlMain.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
                    }
                    break;
                }
            }

            if (pLayerEffects == null)
            {
                sliderItemTransparency.Enabled = false;
                sliderItemContrast.Enabled = false;
                sliderItemBrightness.Enabled = false;
                return;
            }

            if (pLayerEffects is IFeatureLayer)
            {
                sliderItemTransparency.Enabled = true;
                sliderItemContrast.Enabled = false;
                sliderItemBrightness.Enabled = false;
                ILayerEffects pLEffects = new FeatureLayerClass();
                IFeatureLayer pFLayer = pLayerEffects as IFeatureLayer;
                pLEffects = pFLayer as ILayerEffects;
                pLEffects.SupportsInteractive = true;
                sliderItemTransparency.Value = pLEffects.Transparency;
            }
            else if (pLayerEffects is IRasterLayer)
            {
                sliderItemTransparency.Enabled = true;
                sliderItemContrast.Enabled = true;
                sliderItemBrightness.Enabled = true;
                ILayerEffects pLEffects = new RasterLayerClass();
                IRasterLayer pRLayer = pLayerEffects as IRasterLayer;
                pLEffects = pRLayer as ILayerEffects;
                pLEffects.SupportsInteractive = true;
                sliderItemTransparency.Value = pLEffects.Transparency;
                sliderItemContrast.Value = pLEffects.Contrast;
                sliderItemBrightness.Value = pLEffects.Brightness;
            }
        }

        private void cmBoxEffectsLayer_Enter(object sender, EventArgs e)
        {
            //更新内容，不要放到Click事件上，Click会引起SelectedChanged
            cmBoxEffectsLayer.Items.Clear();
            
            IEnumLayer pEnumLayer = axMapCtlMain.Map.get_Layers(null, true);
            pEnumLayer.Reset();
            ILayer pLayer = pEnumLayer.Next();
            while (pLayer != null)
            {
                if (pLayer is IFeatureLayer)
                {
                    IFeatureLayer featureLayer = pLayer as IFeatureLayer;
                    if (featureLayer.FeatureClass != null)
                    {
                        cmBoxEffectsLayer.Items.Add(pLayer.Name);
                    }
                }
                if (pLayer is IRasterLayer)
                {
                    IRasterLayer rasterLayer = pLayer as IRasterLayer;
                    if (rasterLayer.Raster != null)
                    {
                        cmBoxEffectsLayer.Items.Add(pLayer.Name);
                    }
                }
                pLayer = pEnumLayer.Next();
            }
        }

        private void cmbTargetRasterLayer_Enter(object sender, EventArgs e)
        {
            //更新内容，不要放到Click事件上，Click会引起SelectedChanged
            string strName = cmbTargetRasterLayer.Text;
            cmbTargetRasterLayer.Items.Clear();
            IEnumLayer pEnumLayer = axMapCtlMain.Map.get_Layers(null, true);
            pEnumLayer.Reset();
            ILayer pLayer = pEnumLayer.Next();
            while (pLayer != null)
            {
                if (pLayer is IRasterLayer)
                {
                    IRasterLayer rasterLayer = pLayer as IRasterLayer;
                    if (rasterLayer.Raster != null)
                    {
                        cmbTargetRasterLayer.Items.Add(pLayer.Name);
                    }
                }
                pLayer = pEnumLayer.Next();
            }
            cmbTargetRasterLayer.Text = strName; 
        }

        private void cmbTargetTinLayer_Enter(object sender, EventArgs e)
        {
            cmbTargetTinLayer.Items.Clear();
            comboBoxExCrater.Items.Clear();
            comboBoxExNonCrater.Items.Clear();

            IEnumLayer pEnumLayer = axMapCtlMain.Map.get_Layers(null, true);
            pEnumLayer.Reset();
            ILayer pLayer = pEnumLayer.Next();
            while (pLayer != null)
            {
                //更新tin编辑目标图层
                if (pLayer is ITinLayer || pLayer is IRasterLayer)
                {
                    cmbTargetTinLayer.Items.Add(pLayer.Name);
                    if (cmbTargetTinLayer.SelectedIndex == -1 && cmbTargetTinLayer.Items.Count > 0)
                    {
                        cmbTargetTinLayer.SelectedIndex = 0;
                    }
                }

                if (pLayer is IFeatureLayer)
                {
                    IFeatureLayer pfLayer = pLayer as IFeatureLayer;
                    if (pfLayer.FeatureClass != null)
                    {
                        if (pfLayer.FeatureClass.ShapeType == esriGeometryType.esriGeometryMultiPatch)
                        {
                            comboBoxExCrater.Items.Add(pfLayer.Name);
                            comboBoxExNonCrater.Items.Add(pfLayer.Name);
                        }
                    }
                }
                pLayer = pEnumLayer.Next();
            }

            for (int i = 0; i < cmbTargetTinLayer.Items.Count; i++)
            {
                string layername = cmbTargetTinLayer.Items[i].ToString();
                //List<ILayer> layerlist = GetLayersByName(layername); 
                if (m_TargetTinLayer != null)
                {
                    if (m_TargetTinLayer.Name == layername)
                    {
                        cmbTargetTinLayer.SelectedIndex = i;
                    }
                }
                if (m_EditRasterLayer != null)
                {
                    if (m_EditRasterLayer.Name == layername)
                    {
                        cmbTargetTinLayer.SelectedIndex = i;
                    }
                }

            }
            for (int i = 0; i < comboBoxExCrater.Items.Count; i++)
            {
                string layername = comboBoxExCrater.Items[i].ToString();
                if (m_CraterLayer == null)
                {
                    comboBoxExCrater.SelectedIndex = -1;
                    break;
                }
                if (m_CraterLayer.Name == layername)
                {
                    comboBoxExCrater.SelectedIndex = i;
                }
            }
            for (int i = 0; i < comboBoxExNonCrater.Items.Count; i++)
            {
                string layername = comboBoxExNonCrater.Items[i].ToString();
                if (m_NonCraterLayer == null)
                {
                    comboBoxExNonCrater.SelectedIndex = -1;
                    break;
                }
                if (m_NonCraterLayer.Name == layername)
                {
                    comboBoxExNonCrater.SelectedIndex = i;
                }
            }
        }

        private void axPageLayoutCtlMain_OnExtentUpdated(object sender, IPageLayoutControlEvents_OnExtentUpdatedEvent e)
        {
            //IGraphicsContainer pGC = axPageLayoutCtlMain.ActiveView.GraphicsContainer;
            // pGC.Reset();

            //IElement pTmpEle = pGC.Next();
            //while (pTmpEle != null)
            //{
            //    pGC.UpdateElement(pTmpEle);
            //    pTmpEle = pGC.Next();
            //}
        }

        private void btnCheckUpdate_Click(object sender, EventArgs e)
        {
            LibCheckUpdate.UpdateMe frm= new LibCheckUpdate.UpdateMe();
            frm.ShowDialog();

        }

        private void btnRasterExportBatch_Click(object sender, EventArgs e)
        {
            FrmExportRasterBatch frm = new FrmExportRasterBatch(this.axMapCtlMain.Object as IMapControl3);
            frm.ShowDialog();
        }

        private void btnNEDtoENUShape_Click(object sender, EventArgs e)
        {
            FrmShapeFileNEDtoENU dlg = new FrmShapeFileNEDtoENU(this.axMapCtlMain.Object as IMapControl3);
            dlg.ShowDialog();
        }

        private void cmbAdjustMethod_Enter(object sender, EventArgs e)
        {
           
        }

        private void cmbSunAltOriImg_Enter(object sender, EventArgs e)
        {
            string strName = cmbSunAltOriImg.Text;
            cmbSunAltOriImg.Items.Clear();
            IEnumLayer pEnumLayer = axMapCtlMain.Map.get_Layers(null, true);
            pEnumLayer.Reset();
            ILayer pLayer = pEnumLayer.Next();
            while (pLayer != null)
            {
                if (pLayer is IRasterLayer)
                {
                    IRasterLayer rasterLayer = pLayer as IRasterLayer;
                    if (rasterLayer.Raster != null)
                    {
                        cmbSunAltOriImg.Items.Add(pLayer.Name);
                    }
                }
                pLayer = pEnumLayer.Next();
            }
            cmbSunAltOriImg.Text = strName;
        }

        private void ToolLandPointEXPMap_Click(object sender, EventArgs e)
        {
            string fileFullPath = "";

            if (barDataRv.SelectedDockTab == 0)
            {
                //  string fileFullPath = "";
                //fileFullPath = dataGridRv.Rows[dataGridRvselectIndex].Cells[2].Value.ToString();
            }

            else
                fileFullPath = string.Empty;

            if (string.IsNullOrEmpty(fileFullPath))
                return;

            if (fileFullPath.Contains(".xml"))
            {

            }
        }


        private void axMapCtlMain_OnAfterScreenDraw(object sender, IMapControlEvents2_OnAfterScreenDrawEvent e)
        {
            
            //绘制栅格影像配准控制点（不能放到AfterDraw事件中，因为每渲染一个图层都会激发，造成多次绘制）
            if (m_ToolAddControlPoints != null && m_ToolAddControlPoints.m_FrmLinkTableRaster != null)
            {
                if (m_ToolAddControlPoints.m_FrmLinkTableRaster.Visible)
                {
                    DrawRasterRegisterControlPoint();
                }
            }

            //绘制矢量校正控制点
            DrawVectorControlPoint();
            //太阳高度角量测点
            DrawSunAltPoints();

            try
            {
                if (dock3D.Selected)
                {
                    if (axSceneCtlMain.Scene == null) return;
                    if (axSceneCtlMain.Scene.LayerCount < 1) return;
                    IActiveView iv = axSceneCtlMain.Scene as IActiveView;
                    iv.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
                }
            }
            catch
            {
            }
        }

        private void btnLandExpMap_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "XML文件(*.xml)|*.xml;|All files (*.*)|*.*";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                //FrmExportActiveViewFig frm = new FrmExportActiveViewFig(this.axPageLayoutCtlMain.Object as IPageLayoutControl2, dlg.FileName,m_controlsSynchronizer);
                ExportActiveView(dlg.FileName);
            }
           
        }

        
        private void SetMainApplictationTitle()
        {
            //设置主窗体文档名称 
            if (string.IsNullOrEmpty(axPageLayoutCtlMain.DocumentFilename))
            {
                this.Text = m_strApplicationName;
            }
            else
            {
                this.Text = m_strApplicationName + axPageLayoutCtlMain.DocumentFilename;
            }
            
        }

        

        
        private void FrmMain_KeyDown(object sender, KeyEventArgs e)
        {
           
        }

        private void FrmMain_KeyUp(object sender, KeyEventArgs e)
        {
            
        }

        private void GridTable_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.Control || e.Shift)
            //{
            //    m_bShiftOrCtrl = true;
            //}
           
        }

        private void GridTable_KeyUp(object sender, KeyEventArgs e)
        {
            //m_bShiftOrCtrl = false;
        }

        private void btnTable_Click(object sender, EventArgs e)
        {
           
        }

        private void btnTable_PopupOpen(object sender, DevComponents.DotNetBar.PopupOpenEventArgs e)
        {
            ILayer layer = ClsGDBDataCommon.GetLayerFromName(axMapCtlMain.Map, barAttributeTable.Text);
            if (layer is IFeatureLayer)
            {
                IFeatureLayer featureLayer = layer as IFeatureLayer;
                IEngineEditLayers editLayers = m_EngineEditor as IEngineEditLayers;
                if (editLayers.IsEditable(featureLayer))
                {
                    btnAddField.Enabled = false;
                    btndelectfield.Enabled = false;
                }
                else
                {
                    btnAddField.Enabled = true;
                    btndelectfield.Enabled = true;
                }
            }
        }

        //将本MXD文档中的所有文件整体导出到一个目录中
        private void btnItemExportMxd_Click(object sender, EventArgs e)
        {
            FrmExportMxd frm = new FrmExportMxd((IPageLayoutControl3)axPageLayoutCtlMain.Object);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                ICommand command = new LibEngineCmd.CmdOpenMxdDoc(m_controlsSynchronizer, barAttributeTable, AttributeTable, frm.m_strDocNameNew);
                command.OnCreate(axPageLayoutCtlMain.Object);

                command.OnClick();
                SetMainApplictationTitle();
                SetCurrentTool(axToolbarControlLayout, "esriControls.ControlsSelectTool");                
            }
        }

        //添加XY字段并更新坐标值
        private void btnAddXYField_Click(object sender, EventArgs e)
        {
            try
            {

                ILayer layer = ClsGDBDataCommon.GetLayerFromName(axMapCtlMain.Map, barAttributeTable.Text);
                if (layer is IFeatureLayer)
                {
                    IFeatureLayer featureLayer = layer as IFeatureLayer;
                    IEngineEditLayers editLayers = m_EngineEditor as IEngineEditLayers;
                    if (editLayers.IsEditable(featureLayer))
                    {
                        MessageBox.Show("请先停止编辑图层");
                        return;
                    }
                    string strFieldX = "POINT_X";
                    string strFieldY = "POINT_Y";
                    //测试字段是否已存在 
                    IFeatureClass featureClass = featureLayer.FeatureClass;

                    IFields fields = featureClass.Fields;
                    int nIndexX = fields.FindField(strFieldX);
                    FrmAddField frm = new FrmAddField(featureLayer);
                    if (nIndexX < 0)
                    {
                        nIndexX = frm.AddField(strFieldX, esriFieldType.esriFieldTypeDouble);
                    }
                    int nIndexY = fields.FindField(strFieldY);
                    if (nIndexY <= 0)
                    {
                        nIndexY = frm.AddField(strFieldY, esriFieldType.esriFieldTypeDouble);
                    }

                    if (nIndexX >= 0 && nIndexY >= 0)
                    {
                        //更新字段值
                        IFeatureCursor featureCursor = featureClass.Update(null, false);
                        IFeature feature = null;
                        esriGeometryType geometryType = featureClass.ShapeType;
                        double x, y;
                        while ((feature = featureCursor.NextFeature()) != null)
                        {
                            if (geometryType == esriGeometryType.esriGeometryPoint)
                            {
                                IPoint point = feature.Shape as IPoint;
                                x = point.X;
                                y = point.Y;
                            }
                            else// if(geometryType == esriGeometryType.esriGeometryPolygon)
                            {
                                IEnvelope envelope = feature.Shape.Envelope;
                                x = envelope.XMin + envelope.Width / 2.0;
                                y = envelope.YMin + envelope.Height / 2.0;
                            }

                            feature.set_Value(nIndexX, (object)x.ToString("f8"));
                            feature.set_Value(nIndexY, (object)y.ToString("f8"));
                            feature.Store();
                        }
                        featureCursor.Flush();
                    }
                    m_AttributeTable.ReloadAttributeTable();
                }
            }
            catch (System.Exception ex)
            {

            }
        }

        private void buttonItem54_Click(object sender, EventArgs e)
        {
            FrmImportZhongxian frmzx = new FrmImportZhongxian(this.axMapCtlMain, this.axTOCCtlLayer);
            frmzx.ShowDialog();
        }

        private void buttonItem24_Click(object sender, EventArgs e)
        {
            FrmDuiqiNeiZhongIMU frmdqnzimu = new FrmDuiqiNeiZhongIMU(this.axMapCtlMain, this.axTOCCtlLayer);
            frmdqnzimu.ShowDialog();
        }

        private void buttonItem18_Click(object sender, EventArgs e)
        {
            FrmDuiqi2Waijian frm2neijian = new FrmDuiqi2Waijian(this.axMapCtlMain, this.axTOCCtlLayer);
            frm2neijian.ShowDialog();
        }

        private void buttonItem28_Click(object sender, EventArgs e)
        {
            FrmDuiqi2Waijian frm2neijian = new FrmDuiqi2Waijian(this.axMapCtlMain, this.axTOCCtlLayer);
            frm2neijian.ShowDialog();
        }

        private void buttonItem13_Click(object sender, EventArgs e)
        {
            FrmDuiqiWaiZhong frmwaizhong = new FrmDuiqiWaiZhong(this.axMapCtlMain, this.axTOCCtlLayer);
            frmwaizhong.ShowDialog();
        }

        private void buttonItem27_Click(object sender, EventArgs e)
        {
            FrmDuiqiWaiZhong frmwaizhong = new FrmDuiqiWaiZhong(this.axMapCtlMain, this.axTOCCtlLayer);
            frmwaizhong.ShowDialog();
        }

        private void buttonItem26_Click(object sender, EventArgs e)
        {
            //FrmDuiqiNeijian frmduiqineijian = new FrmDuiqiNeijian(this.axMapCtlMain, this.axTOCCtlLayer);
            //frmduiqineijian.ShowDialog();
            IMapControl3 pMapcontrol = axMapCtlMain.Object as IMapControl3;
            FrmInsideToInsideAlignment frm = new FrmInsideToInsideAlignment(pMapcontrol);
            frm.ShowDialog();
        }

        private void buttonItemCreateCenterline_Click(object sender, EventArgs e)
        {
            IMapControl3 pMapcontrol = axMapCtlMain.Object as IMapControl3;
            LibCerMap.FrmControlPointToCenterLine frm = new FrmControlPointToCenterLine(pMapcontrol);
            frm.ShowDialog();
        }

        private void buttonItemCenterlineInsideReport_Click(object sender, EventArgs e)
        {
            IMapControl3 pMapcontrol = axMapCtlMain.Object as IMapControl3;
            LibCerMap.FrmCenterLineInsideReport frm = new FrmCenterLineInsideReport(pMapcontrol);
            frm.ShowDialog();
        }

        private void buttonItem25_Click(object sender, EventArgs e)
        {
            IMapControl3 pMapcontrol = axMapCtlMain.Object as IMapControl3;
            FrmCenterLineInsideAlignment frm = new FrmCenterLineInsideAlignment(pMapcontrol);
            frm.ShowDialog();
        }       

   


    }//ClassFrmMain
}//NameSpace
