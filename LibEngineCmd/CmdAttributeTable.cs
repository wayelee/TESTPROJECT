using System;
using System.Drawing;
using System.Windows.Forms;
using System.Data;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.DataSourcesRaster;
using ESRI.ArcGIS.Geodatabase;


using DevComponents.DotNetBar;
using ESRI.ArcGIS.Geometry;

namespace LibEngineCmd
{
    /// <summary>
    /// Summary description for CmdAttributeTable.
    /// </summary>
    [Guid("5e7ff06b-04d1-4272-9d22-36e59476c586")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("CustomCE.CmdAttributeTable")]
    public sealed class CmdAttributeTable : BaseCommand
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
        //ILayer m_pLayer = null;

        DevComponents.DotNetBar.Bar m_barTable;
       
        public DevComponents.DotNetBar.Controls.DataGridViewX m_gridfield;
        public DevComponents.DotNetBar.ButtonItem m_btnselras;
        public DevComponents.DotNetBar.ButtonItem m_btnselallras;
        public DevComponents.DotNetBar.ButtonItem m_btnswitchras;
        public DevComponents.DotNetBar.ButtonItem m_btnselclear;
        public DevComponents.DotNetBar.ButtonItem m_btnhide;
 
        public DevComponents.DotNetBar.ButtonItem m_showall;
        public DevComponents.DotNetBar.ButtonItem m_showsel;
        public DevComponents.DotNetBar.ButtonItem m_btndelectfield;
        public DevComponents.DotNetBar.ButtonItem m_btnaddfield;
  
        public DevComponents.DotNetBar.DockContainerItem m_docktable;
        public DevComponents.DotNetBar.ButtonItem m_btnshowallout;
        public DevComponents.DotNetBar.ButtonItem m_btnshowselout;
        public DevComponents.DotNetBar.ButtonItem m_btntable;
        public DevComponents.DotNetBar.LabelItem m_btnNOsel;
  
        //public ILayer m_Layer;
        public System.Data.DataSet m_datasettable;
        public System.Data.DataTable m_AttributeTable;
  
        public AxToolbarControl m_toolbarfield;
        
        public CmdAttributeTable(DevComponents.DotNetBar.Bar barTable)
        {
            //
            m_barTable = barTable;
            
            // TODO: Define values for the public properties
            //
            base.m_category = "CustomCE"; //localizable text
            base.m_caption = "打开属性表";  //localizable text
            base.m_message = "打开属性表";  //localizable text 
            base.m_toolTip = "打开属性表";  //localizable text 
            base.m_name = "CustomCE.CmdAttributeTable";   //unique id, non-localizable (e.g. "MyCategory_MyCommand")

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
            ReloadAttributeTable();
        }

        //重新打开属性表
        public bool ReloadAttributeTable()
        {
            try
            {
                m_barTable.Show();
                ILayer m_Layer = ClsGlobal.GetSelectedLayer(m_hookHelper);
                m_AttributeTable = new System.Data.DataTable();
                if (m_Layer == null) return false;

                if (m_Layer is IFeatureLayer)
                {
                    ReLoadFeatureLayerTable(m_Layer);
                }
                else if (m_Layer is IRasterLayer)
                {
                    ReLoadRasterLayerTable(m_Layer);
                }
                else
                {
                    return false;
                }

                UpdateUI(m_Layer);
                m_gridfield.DataSource = m_AttributeTable;
                m_gridfield.CurrentCell = null;
            }
            catch (System.Exception ex)
            {
                return false;
            }
            return true;
        }

        private void ReLoadFeatureLayerTable(ILayer layer)
        {
            if (layer is IFeatureLayer)
            {
                IFeatureLayer featureLayer = layer as IFeatureLayer;
                if (featureLayer.FeatureClass == null) return;
                //IFeatureClass featureClass = featureLayer as IFeatureClass;
                ITable table = featureLayer as ITable;
                IFields fields = table.Fields;
                //添加字段列
                for (int i = 0; i < fields.FieldCount; i++)
                {
                    m_AttributeTable.Columns.Add();
                    m_AttributeTable.Columns[i].ColumnName = fields.Field[i].Name;
                }

                ICursor cursor = table.Search(null, false);
                IRow row = null;
                while ((row = cursor.NextRow()) != null)
                {
                    DataRow dataRow = m_AttributeTable.NewRow();
                    for (int i = 0; i < fields.FieldCount; i++)
                    {
                        if (fields.get_Field(i).Type == esriFieldType.esriFieldTypeGeometry)
                        {
                            dataRow[i] = featureLayer.FeatureClass.ShapeType.ToString();//fields.get_Field(i).Type.ToString();
                        }
                        else
                        {
                            dataRow[i] = row.get_Value(i);
                        }

                    }
                    m_AttributeTable.Rows.Add(dataRow);
                }
            }
            
        }
        private void ReLoadRasterLayerTable(ILayer layer)
        {
            if(!(layer is IRasterLayer)) return;

            IRasterLayer rasterLayer = layer as IRasterLayer;
            if (rasterLayer.BandCount !=1)
            {
                MessageBox.Show("只有单波段短整型栅格数据才具有属性表", "提示", MessageBoxButtons.OK);
                return;
            }

            ITable table = LibCerMap.ClsGDBDataCommon.GetTableofLayer(rasterLayer);
            if (table == null) return;

            IFields fields = table.Fields;
            //添加字段列
            for (int i = 0; i < fields.FieldCount; i++)
            {
                m_AttributeTable.Columns.Add();
                m_AttributeTable.Columns[i].ColumnName = fields.Field[i].Name;
            }
            ICursor cursor = table.Search(null, false);
            IRow row = null;
            while ((row = cursor.NextRow()) != null)
            {
                DataRow dataRow = m_AttributeTable.NewRow();
                for (int i = 0; i < fields.FieldCount; i++)
                {
                    dataRow[i] = row.get_Value(i);
                }
                m_AttributeTable.Rows.Add(dataRow);
            }
         }
        private void UpdateUI(ILayer m_Layer)
        {
            m_gridfield.AutoGenerateColumns = true;
            m_barTable.Show();
            int nItemCount = m_barTable.Items.Count;
            for (int i = 0; i < nItemCount; i++)
            {
                m_barTable.Items[i].Visible = true;
            }
            m_barTable.Invalidate();
            m_btnshowallout.Visible = true;
            m_btnshowselout.Visible = true;
            m_btntable.Visible = true;
            m_btnselras.Visible = true;
            m_btnselallras.Visible = false;
            m_btnaddfield.Visible = true;
            m_btnswitchras.Visible = false;
            m_btnselclear.Visible = false;
            m_toolbarfield.Visible = true;
            m_btnhide.Visible = true;
            m_showall.Visible = true;
            m_showsel.Visible = true;
            m_btndelectfield.Visible = true;
            m_gridfield.CurrentCell = null;
            m_barTable.Text = m_Layer.Name;
            m_docktable.Text = m_Layer.Name;
        }
        public override bool Enabled
        {
            get
            {
                if (m_hookHelper.Hook is IMapControl3 || m_hookHelper.Hook is IPageLayoutControl3)
                {
                    ILayer layer = ClsGlobal.GetSelectedLayer(m_hookHelper);
                    if (layer == null) { return false; }

                    if (layer is IFeatureLayer)
                    {
                        if (((IFeatureLayer)layer).FeatureClass != null)
                        {
                            return true;
                        }
                    }
                    else if (layer is IRasterLayer)
                    {
                        IRasterLayer rasterLayer = layer as IRasterLayer;
                        if (rasterLayer.BandCount == 1 || rasterLayer.Raster != null)
                        {
                            IRasterProps pRProps = rasterLayer.Raster as IRasterProps;
                            if (pRProps.PixelType == rstPixelType.PT_CHAR || pRProps.PixelType == rstPixelType.PT_UCHAR)
                            {
                                return true;
                            }                          
                        } 

                    }
                    return false;
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
