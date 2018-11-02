using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.DataSourcesFile;

using DevComponents.DotNetBar;

namespace LibCerMap
{
    public partial class FrmJoinAttribute : OfficeForm
    {
        AxMapControl pMapControl = null;
        ILayer pLayer = null;
        IFeatureLayer pJoinLayer = new FeatureLayerClass();
        List<string> PathIn = new List<string>();
        List<string> FileIN = new List<string>();
        List<string> NameIn = new List<string>();
        public DevComponents.DotNetBar.Bar m_barTable;
        public System.Data.DataTable m_AttributeTable;
        public DevComponents.DotNetBar.Controls.DataGridViewX m_gridfield;
        public DevComponents.DotNetBar.DockContainerItem m_docktable;
        public FrmJoinAttribute(AxMapControl mapcontrol, ILayer layer, DevComponents.DotNetBar.Bar barTable, DataTable AttributeTable, DevComponents.DotNetBar.Controls.DataGridViewX gridfield, DockContainerItem docktable)
        {
            InitializeComponent();
            this.EnableGlass = false;
            pMapControl = mapcontrol;
            pLayer = layer;
            m_barTable = barTable;
            m_AttributeTable = AttributeTable;
            m_gridfield = gridfield;
            m_docktable = docktable;
        }

        private void FrmJoinAttribute_Load(object sender, EventArgs e)
        {
            ITable pTable = (ITable)pLayer;
            IFields pFields = pTable.Fields;
            for (int i = 0; i < pFields.FieldCount;i++ )
            {
                cmbtable.Items.Add(pFields.Field[i].Name);
            }
            for (int j = 0; j < pMapControl.LayerCount;j++ )
            {
                if (pMapControl.get_Layer(j) is IFeatureLayer)
                {
                    if (pMapControl.get_Layer(j).Name!=pLayer.Name)
                    {
                        cmblayer.Items.Add(pMapControl.get_Layer(j).Name);
                    }
                }
            }
            cmbtable.SelectedIndex = 0;
            cmblayer.SelectedIndex = 0;
        }

        private void chkall_CheckedChanged(object sender, EventArgs e)
        {
            chkmatch.Checked = !chkall.Checked;
        }

        private void chkmatch_CheckedChanged(object sender, EventArgs e)
        {
            chkall.Checked = !chkmatch.Checked;
        }

        private void btnInputShp_Click(object sender, EventArgs e)
        {
            OpenFileDialog openfile = new OpenFileDialog();
            openfile.Filter = "*.shp|*.shp";
            if (openfile.ShowDialog()==DialogResult.OK)
            {

                if (cmblayer.Items.Contains(System.IO.Path.GetFileNameWithoutExtension(openfile.FileName)))
                {
                    MessageBox.Show("该图层已加入列表","提示",  MessageBoxButtons.OK);
                    return;
                }
                else
                {
                    PathIn.Add(System.IO.Path.GetDirectoryName(openfile.FileName));
                    FileIN.Add(System.IO.Path.GetFileName(openfile.FileName));
                    NameIn.Add(System.IO.Path.GetFileNameWithoutExtension(openfile.FileName));
                    cmblayer.Items.Insert(0,System.IO.Path.GetFileNameWithoutExtension(openfile.FileName));
                    cmblayer.SelectedIndex = 0;
                }
            }
        }

        private void cmblayer_TextChanged(object sender, EventArgs e)
        {
            cmbjointable.Items.Clear();
            if (NameIn.Contains(cmblayer.SelectedItem.ToString()))
            {
                int nindex = 0;
                for (int i = 0; i < NameIn.Count;i++ )
                {
                    if (cmblayer.SelectedText==NameIn[i])
                    {
                        nindex = i;
                        break;
                    }
                }
                IWorkspaceFactory pWFInput = new ShapefileWorkspaceFactoryClass();
                IFeatureWorkspace pWSInput = (IFeatureWorkspace)pWFInput.OpenFromFile(PathIn[nindex], 0);
                IFeatureClass pFtClassIn = pWSInput.OpenFeatureClass(FileIN[nindex]);
                pJoinLayer.FeatureClass = pFtClassIn;
            }
            else 
            {
                for (int i = 0; i < pMapControl.LayerCount;i++ )
                {
                    if (pMapControl.get_Layer(i).Name == cmblayer.SelectedItem.ToString())
                    {
                        IFeatureLayer pflayer=new FeatureLayerClass();
                        pflayer = pMapControl.get_Layer(i) as IFeatureLayer;
                        pJoinLayer.FeatureClass = pflayer.FeatureClass;
                        break;
                    }
                }
            }
            ITable pTableIn = (ITable)pJoinLayer;
            for (int j = 0; j < pTableIn.Fields.FieldCount; j++)
            {
                cmbjointable.Items.Add(pTableIn.Fields.get_Field(j).Name);
            }
            cmbjointable.SelectedIndex = 0;
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            ITable pTableR = (ITable)pLayer;
            ITable pTableJ = (ITable)pJoinLayer;
            IField pField = new FieldClass();
            m_AttributeTable = new System.Data.DataTable();
            for (int i = 0; i < pTableR.Fields.FieldCount;i++ )
            {
                m_AttributeTable.Columns.Add();
                m_AttributeTable.Columns[i].ColumnName = pTableR.Fields.Field[i].Name;
            }
            for (int k = 0; k < pTableJ.Fields.FieldCount; k++)
            {
                //pField = pTableJ.Fields.Field[k];
                //pTableR.AddField(pField);
                m_AttributeTable.Columns.Add();            
                m_AttributeTable.Columns[pTableR.Fields.FieldCount+k].ColumnName = pTableJ.Fields.Field[k].Name+"*";
                         
            }
            
            if (chkall.Checked==true)
            {
                IRow row = null;
                ICursor cursor = pTableR.Search(null, false);
                row = cursor.NextRow();
                while (row != null)
                {
                    DataRow drow = m_AttributeTable.NewRow();
                    for (int k = 0; k < pTableR.Fields.FieldCount; k++)
                    {
                        drow[k] = row.get_Value(k);
                    }

                    IRow jrow = null;
                    ICursor jcursor=pTableJ.Search(null,false);
                    jrow=jcursor.NextRow();
                    while(jrow!=null)
                    {
                        if (row.get_Value(cmbtable.SelectedIndex).ToString() == jrow.get_Value(cmbjointable.SelectedIndex).ToString()) 
                        {
                            for (int m = 0; m < pTableJ.Fields.FieldCount; m++)
                            {
                                drow[pTableR.Fields.FieldCount + m] = jrow.get_Value(m);
                            }
                            break;
                        }
                        jrow = jcursor.NextRow();
                    }
                    m_AttributeTable.Rows.Add(drow);                  
                    row = cursor.NextRow();
                }
            }
            else
            {
                IRow row = null;
                ICursor cursor = pTableR.Search(null, false);
                row = cursor.NextRow();
                while (row != null)
                {
                    DataRow drow = m_AttributeTable.NewRow();
                    IRow jrow = null;
                    ICursor jcursor = pTableJ.Search(null, false);
                    jrow = jcursor.NextRow();
                    while (jrow != null)
                    {
                        if (row.get_Value(cmbtable.SelectedIndex).ToString() == jrow.get_Value(cmbjointable.SelectedIndex).ToString())
                        {
                            for (int k = 0; k < pTableR.Fields.FieldCount; k++)
                            {
                                drow[k] = row.get_Value(k);
                            }

                            for (int m = 0; m < pTableJ.Fields.FieldCount; m++)
                            {
                                drow[pTableR.Fields.FieldCount + m] = jrow.get_Value(m);
                            }
                            break;
                        }
                        jrow = jcursor.NextRow();
                    }
                  
                    m_AttributeTable.Rows.Add(drow);
                    row = cursor.NextRow();
                }
            }



            m_gridfield.AutoGenerateColumns = true;
            m_barTable.Show();
            int nItemCount = m_barTable.Items.Count;
            for (int i = 0; i < nItemCount; i++)
            {
                 m_barTable.Items[i].Visible = true;
            }
            m_barTable.Invalidate();
            m_barTable.Text = pLayer.Name;
            m_docktable.Text = pLayer.Name;
            m_gridfield.DataSource = m_AttributeTable;
            m_gridfield.CurrentCell = null;

            this.Close();
        }

      
    }
}
