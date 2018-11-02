using System;
using System.Drawing;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Controls;
using LibCerMap;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Carto;
using System.Windows;
using System.Windows.Forms;

namespace LibEngineCmd
{
    /// <summary>
    /// Summary description for CmdAdjust.
    /// </summary>
    [Guid("4a9d9736-12da-47a7-b47b-09760159e790")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("CustomCE.CmdAdjust")]
    public sealed class CmdAdjust : BaseCommand
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

        public enum AdjustMethod
        {
            AdjustMethod_Affine=0,
            AdjustMethod_Projective,
            AdjustMethod_Similarity,
            AdjustMethod_EdgeSnap,
            AdjustMethod_RubberSheet
        }

        private IHookHelper m_hookHelper;
        public ToolNewDisplacement m_NewDisplacement;
        public CmdSetAdjustData m_CmdSetAdjustData;
        public AdjustMethod m_AdjustMehod;
        public IMapControl2 m_pControl;

        public CmdAdjust()
        {
            //
            // TODO: Define values for the public properties
            //
            base.m_category = "CustomCE"; //localizable text
            base.m_caption = "空间校正";  //localizable text
            base.m_message = "空间校正";  //localizable text 
            base.m_toolTip = "空间校正";  //localizable text 
            base.m_name = "CustomCE.CmdAdjust";   //unique id, non-localizable (e.g. "MyCategory_MyCommand")

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
        /// Enabled
        /// </summary>
        public override bool Enabled
        {
            get
            {
                if (m_hookHelper.ActiveView is IMap)
                {
                    if (m_NewDisplacement !=null && m_CmdSetAdjustData !=null)
                    {
                        if (m_NewDisplacement.ControlPointsCount>0 && m_CmdSetAdjustData.ListLayers.Count >0)
                        {
                            return IsControlPointsEnough();
                        }
                    }
                }
                return false; ;
            }
        }

        /// <summary>
        /// Occurs when this command is clicked
        /// </summary>
        public override void OnClick()
        {
            // TODO: Add CmdAdjust.OnClick implementation
            if (m_CmdSetAdjustData.ListLayers == null)
            {
                return;
            }

            bool bFlag=false;
            bFlag=IsControlPointsEnough();
            if(!bFlag)
            {
                MessageBox.Show("控制点数不够！\n仿射变换：3\n投影变换：4\n相似变换：2");
                return;
            }

            if (m_CmdSetAdjustData.ListLayers.Count == 0)
            {
                MessageBox.Show("未设置校正数据！");
                return;
            }

            if (m_CmdSetAdjustData.ListLayers.Count != 0 && bFlag)
            {
                if (m_AdjustMehod == AdjustMethod.AdjustMethod_Affine)
                {
                    AffineAdjust();
                }

                if (m_AdjustMehod == AdjustMethod.AdjustMethod_Projective)
                {
                    ProjectiveAdjust();
                }

                if (m_AdjustMehod == AdjustMethod.AdjustMethod_Similarity)
                {
                    SimilarityAdjust();
                }
            }
        }
        #endregion

        #region 方法

        private bool IsControlPointsEnough()
        {
            int nControlPointsCount=m_NewDisplacement.ControlPointsCount;
            if (m_AdjustMehod == AdjustMethod.AdjustMethod_Affine && nControlPointsCount >= 3)
            {
                return true;
            }

            if (m_AdjustMehod == AdjustMethod.AdjustMethod_Projective && nControlPointsCount >= 4)
            {
                return true;
            }

            if (m_AdjustMehod == AdjustMethod.AdjustMethod_Similarity && nControlPointsCount >= 2)
            {
                return true;
            }

            return false;
        }

        //仿射变换
        private void AffineAdjust()
        {
            AffineTransformation2DClass affineTrans = new AffineTransformation2DClass();

            int nCount=m_NewDisplacement.ControlPointsCount;
            IPoint[] fromPoints = new IPoint[nCount];
            IPoint[] toPoints = new IPoint[nCount];
            for (int i = 0; i < nCount; i++)
            {
                fromPoints[i] = m_NewDisplacement.OriginPoints.get_Point(i);
                toPoints[i] = m_NewDisplacement.TargetPoints.get_Point(i);
            }

            affineTrans.DefineFromControlPoints(fromPoints, toPoints);
            execAdjust(affineTrans);
        }

        private ILayer findLayer(String szName)
        {
            if (m_pControl == null)
            {
                return null;
            }

            for (int i = 0; i < m_pControl.Map.LayerCount;i++ )
            {
                if (m_pControl.Map.get_Layer(i).Name == szName)
                {
                    return m_pControl.Map.get_Layer(i);
                }
            }

            return null;
        }

        //投影变换
        private void ProjectiveAdjust()
        {
            ProjectiveTransformation2DClass projectiveAdjust = new ProjectiveTransformation2DClass();

            int nCount = m_NewDisplacement.ControlPointsCount;
            IPoint[] fromPoints = new IPoint[nCount];
            IPoint[] toPoints = new IPoint[nCount];
            for (int i = 0; i < nCount; i++)
            {
                fromPoints[i] = m_NewDisplacement.OriginPoints.get_Point(i);
                toPoints[i] = m_NewDisplacement.TargetPoints.get_Point(i);
            }

            projectiveAdjust.DefineFromControlPoints(fromPoints, toPoints);
            execAdjust(projectiveAdjust);
        }

        //相似变换
        private void SimilarityAdjust()
        {
            //ProjectiveTransformation2DClass projectiveAdjust = new ProjectiveTransformation2DClass();

            //int nCount = m_NewDisplacement.ControlPointsCount;
            //IPoint[] fromPoints = new IPoint[nCount];
            //IPoint[] toPoints = new IPoint[nCount];
            //for (int i = 0; i < nCount; i++)
            //{
            //    fromPoints[i] = m_NewDisplacement.OriginPoints.get_Point(i);
            //    toPoints[i] = m_NewDisplacement.TargetPoints.get_Point(i);
            //}

            //projectiveAdjust.DefineFromControlPoints(fromPoints, toPoints);
            //execAdjust(projectiveAdjust);
        }

        private void execAdjust(ITransformation trans)
        {
            if (DialogResult.Yes != MessageBox.Show("是否确定执行转换？", "", MessageBoxButtons.YesNo))
            {
                return;
            }
            
            for (int i = 0; i < m_CmdSetAdjustData.ListLayers.Count; i++)
            {
                //获得layer
                IFeatureClass pFC = ((IFeatureLayer)findLayer(m_CmdSetAdjustData.ListLayers[i])).FeatureClass;
                IFeatureCursor pFCursor = pFC.Update(null, false);
                IFeature pF = pFCursor.NextFeature();
                while (pF != null)
                {
                    ITransform2D pTrans = pF.Shape as ITransform2D;
                    pTrans.Transform(esriTransformDirection.esriTransformForward, trans);
                    pF.Store();
                    pF = pFCursor.NextFeature();
                }
            }

            m_NewDisplacement.OriginPoints.RemovePoints(0, m_NewDisplacement.OriginPoints.PointCount);
            m_NewDisplacement.TargetPoints.RemovePoints(0, m_NewDisplacement.TargetPoints.PointCount);
            m_NewDisplacement.VectorLinkTable.RefreshDataTable();
            m_NewDisplacement.RefreshLayer();
            m_pControl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewAll, null, null);
        }

        #endregion
    }
}
