using System;
using System.Drawing;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using LibCerMap;

namespace LibEngineCmd
{
    /// <summary>
    /// Summary description for CmdChangeAnnotationSymbol.
    /// </summary>
    [Guid("1a424e5e-a24e-4a46-b751-2b48bd48bce2")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("CustomCE.CmdChangeAnnotationSymbol")]
    public sealed class CmdChangeAnnotationSymbol : BaseCommand
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
        IEngineSnapEnvironment snapEnvironment;
        IEngineFeatureSnapAgent featureSnapAgent = null;

        private IEngineEditor m_engineEditor;
     //   private IEngineEditLayers m_editLayer;

        //修改注记颜色
        public CmdChangeAnnotationSymbol()
        {
            //
            // TODO: Define values for the public properties
            //
            base.m_category = "CustomCE"; //localizable text
            base.m_caption = "修改注记样式";  //localizable text
            base.m_message = "修改注记样式";  //localizable text 
            base.m_toolTip = "修改注记样式";  //localizable text 
            base.m_name = "CustomCE.CmdChangeAnnotationSymbol";   //unique id, non-localizable (e.g. "MyCategory_MyCommand")

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
            try
            {
                m_hookHelper = new HookHelperClass();
                m_hookHelper.Hook = hook;
                m_engineEditor = new EngineEditorClass(); //this class is a singleton
               // m_editLayer = m_engineEditor as IEngineEditLayers;

            }
            catch
            {
                m_hookHelper = null;
            }

            // TODO:  Add other initialization code
        }

        /// <summary>
        /// Occurs when this command is clicked
        /// </summary>
        public override void OnClick()
        {
            // TODO: Add CmdChangeAnnotationSymbol.OnClick implementation
            IMapControl3 pMapCtr = ClsGlobal.GetMapControl(m_hookHelper);
            if (pMapCtr == null) return;

            ISelection pSelection = pMapCtr.Map.FeatureSelection;
             

            FrmAddText pFrmAddText = null;
            IEnumFeature pEnumFeature = pSelection as IEnumFeature;

            int selectcount = 0;
            pEnumFeature.Reset();
            IFeature pFeature = null;
            while ((pFeature = pEnumFeature.Next()) != null)
            {
                selectcount++;
            }


            pEnumFeature.Reset();
            pFeature =null ;
            while ((pFeature =pEnumFeature.Next()) !=null )
            {
                IAnnotationFeature  pAFeature = pFeature as IAnnotationFeature ;
                if (pAFeature !=null)
                {
                    ITextElement pTextElement = pAFeature.Annotation as ITextElement;
                    if (pFrmAddText == null)
                    {
                        pFrmAddText = new FrmAddText(pTextElement, m_hookHelper);
                        if (pFrmAddText.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                        {
                            return;
                        }                        
                    }
              //    ( (ITextElement) pAFeature.Annotation ).Symbol= pFrmAddText.pTextElement.Symbol;

                    //ITextElement pTextElement2 = pAFeature.Annotation as ITextElement;
                    //pTextElement2.Symbol = pFrmAddText.pTextElement.Symbol;
                    //pTextElement2.Text = pFrmAddText.pTextElement.Text;
                    //pAFeature.Annotation = pTextElement2 as IElement;
                    
                    ITextElement pTextElement2 = pAFeature.Annotation as ITextElement;
                    pTextElement2.Symbol = pFrmAddText.pTextElement.Symbol;
                    if (selectcount == 1)
                    {                      
                        pTextElement2.Text = pFrmAddText.pTextElement.Text;
                    }                   
                    pAFeature.Annotation = pTextElement2 as IElement;
                    pFeature.Store();
                }
            }
            
        }

        public override bool Enabled
        {
            get
            {
                //check whether Editing 
                if (m_engineEditor.EditState == esriEngineEditState.esriEngineStateNotEditing)
                {
                    return false;
                }

                //check that only one feature is currently selected
                //IMapControl3 pMapCtr = (((IToolbarControl)m_hookHelper.Hook).Buddy) as IMapControl3;

                IMapControl3 pMapCtr = ClsGlobal.GetMapControl(m_hookHelper);
                if (pMapCtr ==null)
                {
                    return false;
                }

                ISelection pSelection = pMapCtr.Map.FeatureSelection;
                //IFeatureSelection featureSelection = pSelection as IFeatureSelection;
                //ISelectionSet selectionSet = featureSelection.SelectionSet;
                //if (selectionSet.Count < 1)
                //{
                //    return false;
                //}

                IEnumFeature pEnumFeature = pSelection as IEnumFeature;
                pEnumFeature.Reset();
                IFeature pFeature = pEnumFeature.Next();
                while (pFeature != null)
                {
                    if (pFeature.FeatureType == esriFeatureType.esriFTAnnotation)
                    {
                        return true;
                    }
                    pFeature = pEnumFeature.Next();
                }
                return false;
               // return true;
            }
        }


        #endregion
    }
}
