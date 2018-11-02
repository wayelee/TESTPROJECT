using System;
using System.Drawing;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Controls;
using System.Windows .Forms ;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Display;

namespace LibEngineCmd
{
    /// <summary>
    /// Summary description for CmdAddText.
    /// </summary>
    [Guid("f425f664-4e1f-4199-86d3-fbe7fff7a052")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("CustomCE.CmdAddText")]
    public sealed class CmdAddText : BaseCommand
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

        public CmdAddText()
        {
            //
            // TODO: Define values for the public properties
            //
            base.m_category = "CustomCE"; //localizable text
            base.m_caption = "文本";  //localizable text
            base.m_message = "添加文本";  //localizable text 
            base.m_toolTip = "文本";  //localizable text 
            base.m_name = "CustomCE.CmdAddText";   //unique id, non-localizable (e.g. "MyCategory_MyCommand")

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
            // TODO: Add CmdAddText.OnClick implementation
            IPageLayoutControl3 pPageLayoutControl = ClsGlobal.GetPageLayoutControl(m_hookHelper);
            if (pPageLayoutControl == null) return;
            //if (m_hookHelper.Hook is IToolbarControl)
            //{
            //    if (((IToolbarControl)m_hookHelper.Hook).Buddy is IPageLayoutControl3)
            //    {
            //        pPageLayoutControl = (IPageLayoutControl3)((IToolbarControl)m_hookHelper.Hook).Buddy;
            //    }
 
            //}
            ////In case the container is MapControl
            //else if (m_hookHelper.Hook is IPageLayoutControl3)
            //{
            //    pPageLayoutControl = (IPageLayoutControl3)m_hookHelper.Hook;
            //}
            //else
            //{
            //    MessageBox.Show("当前界面必须是PageLayoutControl界面!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //    return;
            //}

            try
            {
                ITextElement pTextElement = new TextElementClass();
                IElement pElement = (IElement)pTextElement;

                //添加文本
                pTextElement.Text = "Text";
                pTextElement.ScaleText = false;

                ITextSymbol pTextSymbol = pTextElement.Symbol;
                pTextSymbol.Size = 48;
                pTextElement.Symbol = pTextSymbol;

                IGraphicsContainer pGraphicContainer = m_hookHelper.ActiveView.GraphicsContainer;               
                IActiveView pActiveView = m_hookHelper.ActiveView;
                IPageLayout pPageLayout = (IPageLayout)pActiveView;
                IPage pPage = pPageLayout.Page;

                IEnvelope pPrintPageEnvolope=pPage.PrintableBounds;
                double dbCenterX=(pPrintPageEnvolope.XMax+ pPrintPageEnvolope.XMin)/2;
                double dbCenterY=(pPrintPageEnvolope.YMax+ pPrintPageEnvolope.YMin)/2;
                double pWidth = 20;
                double pHeigth = 20;
                double dbXmin = dbCenterX - pWidth / 2;
                double dbYmin = dbCenterY - pHeigth / 2;
                double dbXmax = dbCenterX + pWidth / 2;
                double dbYmax = dbCenterY + pHeigth / 2;

                IEnvelope pEnvelope = new EnvelopeClass();
                pEnvelope.PutCoords(dbXmin, dbYmin, dbXmax, dbYmax);
                pElement.Geometry = pEnvelope as IGeometry;
                pGraphicContainer.AddElement(pElement, 0);

                //刷新
                m_hookHelper.ActiveView.Refresh();

                //LibCerMap.FrmAddText FrmAddtext = new LibCerMap.FrmAddText(null);
                //if (FrmAddtext.ShowDialog() == DialogResult.OK)
                //{
                    
                //}
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
    }
}
