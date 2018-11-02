using System;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Geodatabase;

namespace LibEngineCmd
{
    /// <summary>
    /// Summary description for CmdPictureElement.
    /// </summary>
    [Guid("484fa4e6-21fd-411e-804d-27584d4db72a")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("CustomCE.CmdPictureElement")]
    public sealed class CmdPictureElement : BaseCommand
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
        private IPictureElement4 pPictureElement; 

        public CmdPictureElement()
        {
            //
            // TODO: Define values for the public properties
            //
            base.m_category = "CustomCE"; //localizable text
            base.m_caption = "≤Â»ÎÕº∆¨";  //localizable text
            base.m_message = "≤Â»ÎÕº∆¨";  //localizable text 
            base.m_toolTip = "≤Â»ÎÕº∆¨";  //localizable text 
            base.m_name = "CustomCE.CmdPictureElement";   //unique id, non-localizable (e.g. "MyCategory_MyCommand")

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
            // TODO: Add CmdPictureElement.OnClick implementation
            IPageLayoutControl3 pPageLayoutControl = null;

            if (m_hookHelper.Hook is IToolbarControl)
            {
                if (((IToolbarControl)m_hookHelper.Hook).Buddy is IPageLayoutControl3)
                {
                    pPageLayoutControl = (IPageLayoutControl3)((IToolbarControl)m_hookHelper.Hook).Buddy;
                }

            }
            //In case the container is MapControl
            else if (m_hookHelper.Hook is IPageLayoutControl3)
            {
                pPageLayoutControl = (IPageLayoutControl3)m_hookHelper.Hook;
            }

            try
            {
                OpenFileDialog ODialog = new OpenFileDialog();
                ODialog.Title = "«Î—°‘ÒÕº∆¨";
                ODialog.Filter = "bmpÕº∆¨|*.bmp|pngÕº∆¨|*.png|JPEGÕº∆¨|*.jpg";
                ODialog.RestoreDirectory = true;
                if (ODialog.ShowDialog()==DialogResult.OK)
                {
                    string picturePath = ODialog.FileName;
                    if (System.IO.Path.GetFileName(picturePath).Contains(".bmp") || System.IO.Path.GetFileName(picturePath).Contains(".BMP"))
                    {
                        pPictureElement = new BmpPictureElementClass();                       
                    }
                    else if (System.IO.Path.GetFileName(picturePath).Contains(".png") || System.IO.Path.GetFileName(picturePath).Contains(".PNG"))
                    {
                        pPictureElement = new PngPictureElementClass();
                    }
                    else if (System.IO.Path.GetFileName(picturePath).Contains(".jpg") || System.IO.Path.GetFileName(picturePath).Contains(".JPG"))
                    {
                        pPictureElement = new JpgPictureElementClass();
                    }
                    pPictureElement.ImportPictureFromFile(picturePath);
                    pPictureElement.SavePictureInDocument = true;

                    IGraphicsContainer pGraphicContainer = m_hookHelper.ActiveView.GraphicsContainer;
                    IActiveView pActiveView = m_hookHelper.ActiveView;
                    IPageLayout pPageLayout = (IPageLayout)pActiveView;
                    IPage pPage = pPageLayout.Page;

                    IRasterLayer pRLayer = new RasterLayerClass();
                    pRLayer.CreateFromFilePath(picturePath);
                    double pictureX = pRLayer.ColumnCount;
                    double pictureY = pRLayer.RowCount;

                    IEnvelope pPrintPageEnvolope = pPage.PrintableBounds;
                    double dbCenterX = (pPrintPageEnvolope.XMax + pPrintPageEnvolope.XMin) / 2;
                    double dbCenterY = (pPrintPageEnvolope.YMax + pPrintPageEnvolope.YMin) / 2;
                    double pWidth = 5;
                    double pHeigth = 5*(pictureY/pictureX);
                    double dbXmin = dbCenterX - pWidth/2 ;
                    double dbYmin = dbCenterY - pHeigth/2 ;
                    double dbXmax = dbCenterX + pWidth/2 ;
                    double dbYmax = dbCenterY + pHeigth/2 ;

                    IElement pElement = (IElement)pPictureElement;
                    IEnvelope pEnvelope = new EnvelopeClass();
                    pEnvelope.PutCoords(dbXmin, dbYmin, dbXmax, dbYmax);
                    pElement.Geometry = pEnvelope as IGeometry;
                    pGraphicContainer.AddElement(pElement, 0);

                    m_hookHelper.ActiveView.Refresh();
                }
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
