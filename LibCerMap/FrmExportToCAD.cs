using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geoprocessing;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
 
namespace LibCerMap
{
    public partial class FrmExportToCAD : OfficeForm
    {
        IMapControl3 pMapcontrol;
        IPageLayoutControl pPageLayoutControl;

        public FrmExportToCAD(IMapControl3 mapcontrol, IPageLayoutControl pagelayoutcontrol)
        {
            InitializeComponent();
            pMapcontrol = mapcontrol;
            pPageLayoutControl = pagelayoutcontrol;
            if (comboBox1.Items.Count > 0)
            {
                comboBox1.SelectedIndex = 0;
            }
               
        }

        private void buttonXExport_Click(object sender, EventArgs e)
        {
            try
            {
                IVariantArray parameters = new VarArrayClass();
                string layers = "";
                for (int i = 0; i < listBox2.Items.Count; i++)
                {
                    layers = layers + listBox2.Items[i].ToString() + ";";
                }
                layers = layers.TrimEnd(';');

                // parameters.Add(@"D:\mydb.gdb\myFeatuerClass1;D:\mydb.gdb\myFeatuerClass2");
                //parameters.Add("DWG_R2007");
                //parameters.Add(@"C:\temp\ExportCAD.DWG");
                parameters.Add(layers);
                parameters.Add(comboBox1.SelectedItem.ToString());
                parameters.Add(textBox1.Text);
                GeoProcessor gp = new GeoProcessor();
                bool response = RunTool("ExportCAD_conversion", parameters, null, false);
                MessageBox.Show("导出完成");
            }
            catch
            {
            }
            
        }

       

public  bool RunTool(string toolName,IVariantArray parameters , ITrackCancel TC, bool showResultDialog)
{
    GeoProcessor gp = new GeoProcessor();
    IGeoProcessorResult result = null;
    // Execute the tool            
    try
    {
        result = (IGeoProcessorResult)gp.Execute(toolName, parameters, TC);
        string re = result.GetOutput(0).GetAsText(); 
        if (showResultDialog)
            ReturnMessages(result,"");
        if (result.MaxSeverity == 2) //error
            return false;
        else
            return true;
    } 
    catch (COMException err)
    {
        MessageBox.Show(err.Message + " in RunTool");
        ReturnMessages(result, "");
    }
    catch (Exception err)
    {
        MessageBox.Show(err.Message + " in RunTool");
        ReturnMessages(result, "");
    }
        return false;
}

         private void ReturnMessages(  ESRI.ArcGIS.Geoprocessing.IGeoProcessorResult pResult ,  String Title = "" )  
         {
              String ErrorMessage ="";
  
        if (pResult.MessageCount > 0 )   
             {
            for(int Count = 0; Count < pResult.MessageCount -1; Count++)
            {
                  ErrorMessage += pResult.GetMessage(Count)  ;
            }  
        }
        System.Windows.Forms.MessageBox.Show(ErrorMessage, Title);
  
         }

         private void FrmExportToCAD_Load(object sender, EventArgs e)
         {
             for (int i = 0; i < pMapcontrol.LayerCount; i++)
             {
                 ILayer pLayer = null;
                 if (pMapcontrol.get_Layer(i) is IFeatureLayer)
                 {
                     pLayer = pMapcontrol.get_Layer(i);
                     IFeatureLayer pFeatureLayer = pLayer as IFeatureLayer;
                     if (pFeatureLayer == null)
                     {
                         continue;
                     }
                     IFeatureClass pFeatureClass = pFeatureLayer.FeatureClass;
                     if (pFeatureClass == null)
                     {
                         continue;
                     }
                     IDataset ds = pFeatureClass as IDataset;

                     if (ds.Category == "Shapefile Feature Class")
                     {
                         listBox1.Items.Add(ds.Workspace.PathName + @"\" + ds.Name + ".shp");
                     }
                     else
                     {
                         listBox1.Items.Add(ds.Workspace.PathName + @"\" + ds.Name );
                     }
                 }
             }
         }

         private void buttonXadd_Click(object sender, EventArgs e)
         {
             if (listBox1.SelectedIndex < 0)
                 return;
             else
             {
                 int idx = listBox1.SelectedIndex;
                 listBox2.Items.Add(listBox1.Items[idx].ToString());
                 listBox1.Items.RemoveAt(idx);
             }
         }

         private void buttonXRemove_Click(object sender, EventArgs e)
         {
             if (listBox2.SelectedIndex < 0)
             {
                 return;
             }
             else
             {
                 int idx = listBox2.SelectedIndex;
                 listBox1.Items.Add(listBox2.Items[idx].ToString());
                 listBox2.Items.RemoveAt(idx);
             }
         }

         private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
         {
             buttonXadd_Click(null, null);
         }

         private void listBox2_MouseDoubleClick(object sender, MouseEventArgs e)
         {
             buttonXRemove_Click(null, null);
         }

         private void buttonXSelect_Click(object sender, EventArgs e)
         {
             string filter = "";
             if (comboBox1.SelectedItem.ToString().ToUpper().Contains("DWG"))
             {
                 filter =  "| *.DWG";
             }
             else if (comboBox1.SelectedItem.ToString().ToUpper().Contains("DXF"))
             {
                 filter = "| *.DXF";
             }
             else if (comboBox1.SelectedItem.ToString().ToUpper().Contains("DGN"))
             {
                 filter = "| *.DGN";
             }
             SaveFileDialog dlg = new SaveFileDialog();
             dlg.RestoreDirectory = true;
             dlg.Filter = filter;
             if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
             {
                 string filename = dlg.FileName;
                 textBox1.Text = filename;
             }
         }

    }
}
