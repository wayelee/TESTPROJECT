/**
* @copyright Copyright(C), 2013, PMRS Lab, IRSA, CAS
* @file CmdNorthArrow.cs
* @date 2013.03.03
* @author Ge Xizhi
* @brief 调用指北针窗口
* @version 1.0
* @par 修改历史：
* <作者>  <修改日期>  <版本号>  <详细描述>\n
*张三  2013.03.03  1.0 
*/

using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.IO;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;
using LibCerMap;

namespace LibEngineCmd
{
    /// <summary>
    /// Summary description for CmdNorthArrow.
    /// </summary>
    [Guid("3e78e99c-48f0-4695-8421-2ad9147a2318")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("CustomCE.CmdNorthArrow")]
    public sealed class CmdNorthArrow : BaseCommand
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

        public CmdNorthArrow()
        {
            //
            // TODO: Define values for the public properties
            //
            base.m_category = "CustomCE"; //localizable text
            base.m_caption = "指北针";  //localizable text
            base.m_message = "";  //localizable text 
            base.m_toolTip = "指北针";  //localizable text 
            base.m_name = "CustomCE.CmdNorthArrow";   //unique id, non-localizable (e.g. "MyCategory_MyCommand")

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
            {
                return;
            }

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
            // TODO: Add CmdNorthArrow.OnClick implementation
            IPageLayoutControl3 pPageLayoutControl = null;
          
            if (m_hookHelper.Hook is IToolbarControl)
            {
                pPageLayoutControl = (IPageLayoutControl3)((IToolbarControl)m_hookHelper.Hook).Buddy;
            }
            //In case the container is MapControl
            else if (m_hookHelper.Hook is IPageLayoutControl3)
            {
                pPageLayoutControl = (IPageLayoutControl3)m_hookHelper.Hook;
            }
            else
            {
                MessageBox.Show("Active control must be PageLayoutControl!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            try
            {
                CreateNorhtArrow();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }            
        }

        public override bool Enabled
        {
            get
            {
                if (m_hookHelper.ActiveView is IPageLayout)
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

        private void CreateNorhtArrow()
        {
            //Create the form with the SymbologyControl
            FrmNorthArrow symbolForm = new FrmNorthArrow(null);
            //Get the IStyleGalleryItem
            if (symbolForm.ShowDialog() != DialogResult.OK) return;
           
            IStyleGalleryItem styleGalleryItem = symbolForm.GetItem();
            //Release the form
            symbolForm.Dispose();
            if (styleGalleryItem == null) return;

            IGraphicsContainer pGraphicsContainer = m_hookHelper.ActiveView.GraphicsContainer;
            IMapFrame pMapFrame = pGraphicsContainer.FindFrame(m_hookHelper.ActiveView.FocusMap) as IMapFrame;

            if (pMapFrame == null)  return;
            
             //Create a map surround frame
            IMapSurroundFrame pMapSurroundFrame = new MapSurroundFrameClass();
            //Set its map frame and map surround
            pMapSurroundFrame.MapFrame = pMapFrame;
            pMapSurroundFrame.MapSurround = (IMapSurround)styleGalleryItem.Item; 

           //显示结果大小
            IEnvelope pEnv = new EnvelopeClass();
            IActiveView pActiveView = m_hookHelper.ActiveView;
            IPageLayout pPageLayout = (IPageLayout)pActiveView;
            IPage pPage = pPageLayout.Page;
            double pWidth = pPage.PrintableBounds.XMax - 5;
            double pHeigth = pPage.PrintableBounds.YMax - 5;
            pEnv.PutCoords(pWidth, pHeigth, pWidth + 20, pHeigth + 20);

            IElement pElement = (IElement)pMapSurroundFrame;            
            pElement.Geometry = (IGeometry)pEnv;

            //Add the element to the graphics container
            pGraphicsContainer.AddElement((IElement)pMapSurroundFrame, 0);

            //Refresh
            m_hookHelper.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, pMapSurroundFrame, null);
        }
    }
}
