using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using ESRI.ArcGIS.ADF.BaseClasses;
using System.Collections.Generic;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using LibCerMap;
using ESRI.ArcGIS.Analyst3D;
using ESRI.ArcGIS.DataSourcesRaster;
using LibModelGen;
using System.IO;

namespace LibEngineCmd
{
    /// <summary>
    /// Summary description for ToolAddStone.
    /// </summary>
    [Guid("b8f4ddad-4d89-4609-98f9-d10d25c74b51")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("CustomCE.ToolAddStone")]
    public sealed class ToolAddStone : BaseTool
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
        //撞击坑图层
        public IFeatureLayer pFeatureLayer;
        public ITinLayer pTinLayer;
        public IRasterLayer pRasterLayer;
        public IMap pMap;
        //模型链表
        public List<String> m_listModels;
        public List<Model> m_manualAddModels;
        public ToolAddStone()
        {
            //
            // TODO: Define values for the public properties
            //
            base.m_category = "CustomCE"; //localizable text 
            base.m_caption = "添加石块";  //localizable text 
            base.m_message = "添加石块";  //localizable text
            base.m_toolTip = "添加石块";  //localizable text
            base.m_name = "CustomCE.ToolAddStone";   //unique id, non-localizable (e.g. "MyCategory_MyTool")
            try
            {
                //
                // TODO: change resource name if necessary
                //
                string bitmapResourceName = GetType().Name + ".bmp";
                base.m_bitmap = new Bitmap(GetType(), bitmapResourceName);
                base.m_cursor = new System.Windows.Forms.Cursor(GetType(), GetType().Name + ".cur");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message, "Invalid Bitmap");
            }
        }
        public override bool Enabled
        {
            get
            {
                IMapControl2 pMapCtr = (((IToolbarControl)m_hookHelper.Hook).Buddy) as IMapControl2;
                if (pMapCtr == null)
                {
                    return false;
                }
                // TODO: Add CmdNorthArrow.OnClick implementation   
                if (pTinLayer == null && pRasterLayer == null)
                {
                    return false;
                }
                //else
                //{
                //    ITin pTin = pTinLayer.Dataset;
                //    ITinEdit pTinEdit = pTin as ITinEdit;
                //    return pTinEdit.IsInEditMode;
                //}
                if (pFeatureLayer == null)
                {
                    return false;
                }
                return true;
            }
        }

        private Model m_pModel = null;
        #region Overridden Class Methods

        /// <summary>
        /// Occurs when this tool is created
        /// </summary>
        /// <param name="hook">Instance of the application</param>
        public override void OnCreate(object hook)
        {
            if (m_hookHelper == null)
                m_hookHelper = new HookHelperClass();

            m_hookHelper.Hook = hook;

            // TODO:  Add ToolAddStone.OnCreate implementation
        }

        /// <summary>
        /// Occurs when this tool is clicked
        /// </summary>
        public override void OnClick()
        {
            // TODO: Add ToolAddStone.OnClick implementation
            //FrmManualSetModelPara manualSetModelPara = new FrmManualSetModelPara();
            //manualSetModelPara.Show();
            //manualSetModelPara.Owner =
            //      System.Windows.Forms.Form.FromChildHandle(User32API.GetCurrentWindowHandle()) as System.Windows.Forms.Form;
        }

        public override void OnMouseDown(int Button, int Shift, int X, int Y)
        {
            // TODO:  Add ToolAddStone.OnMouseDown implementation
            if (pTinLayer != null)
            {
                ITin pTin = pTinLayer.Dataset;
                ISurface pSurface = ((ITinAdvanced)pTin).Surface;
                IMapControl2 pMapCtr = (((IToolbarControl)m_hookHelper.Hook).Buddy) as IMapControl2;
                //在mapctr操作
                if (pMapCtr != null)
                {
                    IPoint mapPoint = pMapCtr.ToMapPoint(X, Y);
                    IZAware za = mapPoint as IZAware;
                    za.ZAware = true;
                    double zVal;
                    zVal = pSurface.GetElevation(mapPoint);
                    if (double.IsNaN(zVal))
                    {
                        MessageBox.Show("获取模型的高度失败！");
                        return;
                    }
                    if (m_listModels.Count == 0)
                    {
                        MessageBox.Show("请先生成模型！");
                        return;
                    }
                    mapPoint.Z = zVal;
                    IFeature pfeature = null;
                    try
                    {
                        //IMultiPatch pMP = new MultiPatchClass();
                        IFeatureClass pFC = pFeatureLayer.FeatureClass;
                        IFeature pF = pFC.CreateFeature();
                        IImport3DFile pI3D = new Import3DFileClass();
                        pI3D.CreateFromFile(m_listModels[m_listModels.Count - 1]);
                        IMultiPatch pMP = pI3D.Geometry as IMultiPatch;
                        ITransform3D pT3D = pMP as ITransform3D;
                        pT3D.Move3D(mapPoint.X, mapPoint.Y, mapPoint.Z);
                        pF.Shape = pMP as IGeometry;
                        pfeature = pF as IFeature;
                        pF.Store();
                        if (pMapCtr != null)
                        {
                            pMapCtr.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
                        }

                    }
                    catch (SystemException e)
                    {
                        if (e.Message == "The spatial index grid size is invalid.")
                        {
                            IFeatureClassLoad pFCL = pFeatureLayer.FeatureClass as IFeatureClassLoad;
                            pFCL.LoadOnlyMode = true;
                            pfeature.Store();
                            pFCL.LoadOnlyMode = false;
                        }
                        else
                        {
                            MessageBox.Show(e.Message);
                        }
                    }



                    //IMultiPatch pMP = new MultiPatchClass();
                    //for (int i = 0; i < 5; i++)
                    //{
                    //    IFeature pF = pFC.CreateFeature();
                    //    IImport3DFile pI3D = new Import3DFileClass();
                    //    pI3D.CreateFromFile(@"C:\Users\Administrator\Desktop\sampleAnalysis\1002.3ds");
                    //    IMultiPatch pMP = pI3D.Geometry as IMultiPatch;
                    //   // ITransform3D pT3D = pMP as ITransform3D;
                    //    pF.Shape = pMP as IGeometry;
                    //    ITransform3D pT3D = pF.Shape as ITransform3D;
                    //    pT3D.Move3D(5293 + 1600, -20427 , 800 + i*400);
                    //    // pF.Shape = pI3D.Geometry;
                }
            }
            if (pRasterLayer !=null)
            {
                //ITin pTin = pTinLayer.Dataset;
                //ISurface pSurface = ((ITinAdvanced)pTin).Surface;
                IMapControl2 pMapCtr = (((IToolbarControl)m_hookHelper.Hook).Buddy) as IMapControl2;
                //在mapctr操作
                if (pMapCtr != null)
                {
                    IPoint mapPoint = pMapCtr.ToMapPoint(X, Y);

                    //添加手工编辑窗口
                    FrmManualSetModelPara pManualSetPara = null;
                    if (m_pModel == null)
                    {
                        Pt2d ptCurrent = new Pt2d();
                        ptCurrent.X = mapPoint.X;
                        ptCurrent.Y = mapPoint.Y;
                        pManualSetPara = new FrmManualSetModelPara(ptCurrent, "Rock");
                    }
                    else
                    {
                        m_pModel.x = mapPoint.X;
                        m_pModel.y = mapPoint.Y;
                        pManualSetPara = new FrmManualSetModelPara(m_pModel);
                    }

                    if (pManualSetPara.ShowDialog() == DialogResult.OK)
                    {
                        if (m_pModel == null)
                            m_pModel = new Model();

                        //bool bFlag = m_pModel.compareModelPara(pManualSetPara.m_pModel);
                        //if (!bFlag)//模型参数已经被修改，需要重新生成新的模型文件
                        //{
                        //    m_pModel.copyFromModel(pManualSetPara.m_pModel);

                        //    //获得当前文件路径下的石头模型文件,并随机获得石块模型
                        //    String szAppPathName = GetParentPathofExe() + @"Resource\RockModel";
                        //    String[] szFileList = Directory.GetFiles(szAppPathName);
                        //    if (szFileList.Length > 0)
                        //    {
                        //        Random r = new Random(TerrainGen.Chaos_GetRandomSeed());
                        //        int nRandomPos = r.Next(szFileList.Length);
                        //        String szModelOutputFilename = szFileList[nRandomPos];
                        //        m_listModels.Add(szModelOutputFilename);
                        //    }
                        //}

                            m_pModel.copyFromModel(pManualSetPara.m_pModel);

                            //获得当前文件路径下的石头模型文件,并随机获得石块模型
                            String szAppPathName = ClsGlobal.GetParentPathofExe() + @"Resource\RockModel";
                            String[] szFileList = Directory.GetFiles(szAppPathName);
                            if (szFileList.Length > 0)
                            {
                                Random r = new Random(TerrainGen.Chaos_GetRandomSeed());
                                int nRandomPos = r.Next(szFileList.Length);
                                String szModelOutputFilename = szFileList[nRandomPos];
                                m_listModels.Add(szModelOutputFilename);
                            }

                        //保存当前添加的模型参数
                        Model pTmpModel = new Model();
                        pTmpModel.copyFromModel(pManualSetPara.m_pModel);
                        m_manualAddModels.Add(pTmpModel);
                    }
                    else
                        return;

                    IZAware za = mapPoint as IZAware;
                    za.ZAware = true;
                    double zVal;
                    IRaster2 pRaster2 = pRasterLayer.Raster as IRaster2;
                    int col, row;
                    pRaster2.MapToPixel(mapPoint.X, mapPoint.Y, out col, out row);
                    zVal = Convert.ToDouble(pRaster2.GetPixelValue(0, col, row));
                    if (double.IsNaN(zVal))
                    {
                        MessageBox.Show("获取模型的高度失败！");
                        return;
                    }
                    if (m_listModels.Count == 0)
                    {
                        MessageBox.Show("请先生成模型！");
                        return;
                    }
                    mapPoint.Z = zVal;
                    try
                    {
                        //IMultiPatch pMP = new MultiPatchClass();
                        IFeatureClass pFC = pFeatureLayer.FeatureClass;
                        IFeature pF = pFC.CreateFeature();
                        IImport3DFile pI3D = new Import3DFileClass();
                        pI3D.CreateFromFile(m_listModels[m_listModels.Count - 1]);
                        IMultiPatch pMP = pI3D.Geometry as IMultiPatch;
                        ITransform3D pT3D = pMP as ITransform3D;
                        pT3D.Move3D(mapPoint.X, mapPoint.Y, mapPoint.Z);
                        IRasterProps pRasterProps = pRasterLayer.Raster as IRasterProps;
                        double xmax = pRasterProps.Extent.XMax;
                        double xmin = pRasterProps.Extent.XMin;
                        double ymax = pRasterProps.Extent.YMax;
                        double ymin = pRasterProps.Extent.YMin;
                        //生成地形的地理范围
                        double dbGeoRangeX = xmax - xmin;
                        double dbGeoRangeY = ymax - ymin;

                        double dbModelRangeX = pMP.Envelope.Width;
                        double dbModelRangeY = pMP.Envelope.Height;
                        double dbModelRangeZ = pMP.Envelope.ZMax - pMP.Envelope.ZMin;

                        //根据地形大小改变石块大小
                        //double dbRandomSize = Math.Min(dbGeoRangeX, dbGeoRangeY) * (0.03 ); //[0.03 0.06]
                        //double dbScale = dbRandomSize / Math.Max(dbModelRangeX, dbModelRangeY);
                        double dbScale = m_pModel.dbSize / Math.Max(dbModelRangeX, Math.Max(dbModelRangeY, dbModelRangeZ));
                        pT3D.Scale3D(mapPoint, dbScale, dbScale, dbScale);


                        pF.Shape = pMP as IGeometry;
                        pF.Store();
                        if (pMapCtr != null)
                        {
                            pMapCtr.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
                        }

                    }
                    catch (SystemException e)
                    {
                        MessageBox.Show(e.Message);
                    }
                }

            }

        }

        public override void OnMouseMove(int Button, int Shift, int X, int Y)
        {
            // TODO:  Add ToolAddStone.OnMouseMove implementation
        }

        public override void OnMouseUp(int Button, int Shift, int X, int Y)
        {
            // TODO:  Add ToolAddStone.OnMouseUp implementation
        }
        #endregion
    }
}
