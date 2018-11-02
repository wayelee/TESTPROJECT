using System;
using System.Drawing;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Controls;
using LibCerMap;
using System.Windows.Forms;
using System.Diagnostics;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.DataSourcesRaster;

namespace LibEngineCmd
{
    /// <summary>
    /// Summary description for CmdRasterRegister.
    /// </summary>
    [Guid("a051dcae-7f7a-4290-82e6-7902c7be26ee")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("CustomCE.CmdRasterRegister")]
    public sealed class CmdRasterRegister : BaseCommand
    {
        #region COM Registration Function(s)
        [ComRegisterFunction()]
        [ComVisible(false)]
        static void RegisterFunction(Type registerType)
        {
            // Required for ArcGIS Component Category Registrar support
            ArcGISCategoryRegistration(registerType);

            //
            // TODO: Add any COM registration code here
            //
        }

        [ComUnregisterFunction()]
        [ComVisible(false)]
        static void UnregisterFunction(Type registerType)
        {
            // Required for ArcGIS Component Category Registrar support
            ArcGISCategoryUnregistration(registerType);

            //
            // TODO: Add any COM unregistration code here
            //
        }

        #region ArcGIS Component Category Registrar generated code
        /// <summary>
        /// Required method for ArcGIS Component Category registration -
        /// Do not modify the contents of this method with the code editor.
        /// </summary>
        private static void ArcGISCategoryRegistration(Type registerType)
        {
            string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
            ControlsCommands.Register(regKey);

        }
        /// <summary>
        /// Required method for ArcGIS Component Category unregistration -
        /// Do not modify the contents of this method with the code editor.
        /// </summary>
        private static void ArcGISCategoryUnregistration(Type registerType)
        {
            string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
            ControlsCommands.Unregister(regKey);

        }

        #endregion
        #endregion

        private IHookHelper m_hookHelper;
        private FrmLinkTableRaster m_FrmLink = null;
        public IRasterLayer pRasterLayer;

        public CmdRasterRegister(FrmLinkTableRaster frmLink)
        {
            //
            // TODO: Define values for the public properties
            //
            base.m_category = "CustomCE"; //localizable text
            base.m_caption = "栅格影像配准";  //localizable text
            base.m_message = "栅格影像配准";  //localizable text 
            base.m_toolTip = "栅格影像配准";  //localizable text 
            base.m_name = "CustomCE.CmdRasterRegister";   //unique id, non-localizable (e.g. "MyCategory_MyCommand")

            m_FrmLink = frmLink;
            try
            {
                //
                // TODO: change bitmap name if necessary
                //
                string bitmapResourceName = GetType().Name + ".bmp";
                base.m_bitmap = new Bitmap(GetType(), bitmapResourceName);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message, "Invalid Bitmap");
            }
        }

        #region Overridden Class Methods

        /// <summary>
        /// Occurs when this command is created
        /// </summary>
        /// <param name="hook">Instance of the application</param>
        public override void OnCreate(object hook)
        {
            if (hook == null)
                return;

            if (m_hookHelper == null)
                m_hookHelper = new HookHelperClass();

            m_hookHelper.Hook = hook;

            // TODO:  Add other initialization code
        }

        /// <summary>
        /// Occurs when this command is clicked
        /// </summary>
        public override void OnClick()
        {
            // TODO: Add CmdRasterRegister.OnClick implementation
            IMapControl3 pMapCtrl = null;
            if (m_hookHelper.Hook is IToolbarControl)
            {
                if (((IToolbarControl)m_hookHelper.Hook).Buddy is IMapControl3)
                {
                    pMapCtrl = (IMapControl3)((IToolbarControl)m_hookHelper.Hook).Buddy;
                }

            }
            //In case the container is MapControl
            else if (m_hookHelper.Hook is IMapControl3)
            {
                pMapCtrl = (IMapControl3)m_hookHelper.Hook;
            }
            else
                return;

            System.GC.Collect();
            FrmSiftMatching frmSiftMatching = new FrmSiftMatching(pMapCtrl,pRasterLayer);
            if (frmSiftMatching.ShowDialog() == DialogResult.OK)
            {
                ClsSiftMatching pSiftMatching = new ClsSiftMatching();
                double[] dbMatchPts;
                int nCount = 0;
                if (pSiftMatching.siftMatching(frmSiftMatching.m_pSiftMatchPara, out dbMatchPts, out nCount))
                {
                    IRaster2 pLeftRaster = frmSiftMatching.m_pRasterLeft;
                    IRasterProps pLeftProps = pLeftRaster as IRasterProps;
                    IPoint pLeftLowerLeft = pLeftProps.Extent.LowerLeft;

                    IRaster2 pRightRaster = frmSiftMatching.m_pRasterRight;
                    IRasterProps pRightProps = pRightRaster as IRasterProps;
                    IPoint pRightLowerLeft = pRightProps.Extent.LowerLeft;

                    //SIFT匹配出来的都是影像坐标，应该先转换成MAPCTRL单位为准
                    for (int i = 0; i < nCount; i++)
                    {
                        int x = (int)dbMatchPts[4 * i];
                        int y = (int)(dbMatchPts[4 * i + 1]);
                        //y = (int)(-1*dbMatchPts[4 * i + 1]);
                        pLeftRaster.PixelToMap(x, y, out dbMatchPts[4 * i], out dbMatchPts[4 * i + 1]);

                        x = (int)dbMatchPts[4 * i + 2];
                        y = (int)(dbMatchPts[4 * i + 3]);
                        //y = (int)(-1*dbMatchPts[4 * i + 3]);
                        pRightRaster.PixelToMap(x, y, out dbMatchPts[4 * i + 2], out dbMatchPts[4 * i + 3]);

#region 废弃代码
                        //int x = (int)dbMatchPts[4 * i];
                        //int y = (int)(dbMatchPts[4 * i + 1]);
                        //dbMatchPts[4 * i] = x * pLeftProps.MeanCellSize().X + pLeftLowerLeft.X;
                        //dbMatchPts[4 * i + 1] = y * pLeftProps.MeanCellSize().Y + pLeftLowerLeft.Y;

                        //x = (int)dbMatchPts[4 * i + 2];
                        //y = (int)(dbMatchPts[4 * i + 3]);
                        //dbMatchPts[4 * i + 2] = x * pRightProps.MeanCellSize().X + pRightLowerLeft.X;
                        //dbMatchPts[4 * i + 3] = y * pRightProps.MeanCellSize().Y + pRightLowerLeft.Y;
                        //IPoint ptFrom = pMapCtrl.ToMapPoint(Convert.ToInt32(dbMatchPts[4 * i + 0]), Convert.ToInt32(dbMatchPts[4 * i + 1]));
                        //IPoint ptTo = pMapCtrl.ToMapPoint(Convert.ToInt32(dbMatchPts[4 * i + 2]), Convert.ToInt32(dbMatchPts[4 * i + 3]));

                        //dbMatchPts[4 * i + 0] = ptFrom.X;
                        //dbMatchPts[4 * i + 1] = ptFrom.Y;
                        //dbMatchPts[4 * i + 2] = ptTo.X;
                        //dbMatchPts[4 * i + 3] = ptTo.Y;
#endregion
                        
                    }
                    pSiftMatching.outputMatchPointsToFile("d:\\b.txt", dbMatchPts, nCount);

                    //添加到控制点中
                    m_FrmLink.DelAllPoints();                  
                    for (int i =0;i<nCount;i++)
                    {
                        IPoint ptOrg = new ESRI.ArcGIS.Geometry.PointClass();
                        ptOrg.PutCoords(dbMatchPts[4 * i], dbMatchPts[4 * i + 1]);
                        m_FrmLink.OriginPoints.AddPoint(ptOrg);

                        IPoint ptTarget= new ESRI.ArcGIS.Geometry.PointClass();
                        ptTarget.PutCoords(dbMatchPts[4 * i + 2], dbMatchPts[4 * i + 3]);
                        m_FrmLink.TargetPoints.AddPoint(ptTarget);
                    }
                    m_FrmLink.RefreshControlAllPoints();
                    m_FrmLink.Show();
                    pMapCtrl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
                    //MessageBox.Show("匹配成功！");
                }
                else
                {
                    MessageBox.Show("匹配失败！");
                }

                //把临时创建的文件删除
                try
                {
                    string szLeftFilename = frmSiftMatching.m_pSiftMatchPara.szLeftFilename;
                    string szRightFilename = frmSiftMatching.m_pSiftMatchPara.szRightFilename;
                    if (System.IO.File.Exists(szLeftFilename))
                    {
                        System.IO.File.Delete(szLeftFilename);
                    }

                    if (System.IO.File.Exists(szRightFilename))
                    {
                        System.IO.File.Delete(szRightFilename);
                    }
                }
                catch (System.Exception ex)
                {
                    return;
                }
            }
        }


        public override bool Enabled
        {
            get
            {
                if ((m_hookHelper.ActiveView is IMap)&& pRasterLayer != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        #endregion
    }
}
