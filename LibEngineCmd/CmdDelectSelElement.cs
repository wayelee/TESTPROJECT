using System;
using System.Drawing;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Controls;
using System.IO;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.DataSourcesFile;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.SystemUI;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Display;
using System.Drawing.Text;
using stdole;

namespace LibEngineCmd
{
    /// <summary>
    /// Summary description for CmdDelectSelElement.
    /// </summary>
    [Guid("7e58fd56-265d-4793-ad20-3cf7441d2dc9")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("CustomCE.CmdDelectSelElement")]
    public sealed class CmdDelectSelElement : BaseCommand
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

        public CmdDelectSelElement()
        {
            //
            // TODO: Define values for the public properties
            //
            base.m_category = "CustomCE"; //localizable text
            base.m_caption = "É¾³ýÒªËØ";  //localizable text
            base.m_message = "É¾³ýÒªËØ";  //localizable text 
            base.m_toolTip = "É¾³ýÒªËØ";  //localizable text 
            base.m_name = "CustomCE.CmdDelectSelElement";   //unique id, non-localizable (e.g. "MyCategory_MyCommand")

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
            // TODO: Add CmdDelectSelElement.OnClick implementation

            IGraphicsContainer pGraphicsContainer = m_hookHelper.ActiveView.GraphicsContainer;
            IGraphicsContainerSelect pGraphicsContainerSelect = pGraphicsContainer as IGraphicsContainerSelect;
            int SelectElementCount = pGraphicsContainerSelect.ElementSelectionCount;
            IEnumElement pEnumElement = pGraphicsContainerSelect.SelectedElements;
            pEnumElement.Reset();
            IElement pElement = pEnumElement.Next();
            for (int i = 0; i < SelectElementCount; i++)
            {
                pGraphicsContainer.DeleteElement(pElement);
                pElement = pEnumElement.Next();
                m_hookHelper.ActiveView.Refresh();
            }
        }

        #endregion
    }
}
