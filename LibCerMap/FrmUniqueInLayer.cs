using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;

using DevComponents.DotNetBar;

namespace LibCerMap
{
    public partial class FrmUniqueInLayer : OfficeForm
    {
        ILayer pLayer = null;
        ILayer pInLayer = null;
        IFeatureLayer pFLayer = null;
        IFeatureLayer pInFLayer = null;
        IGeoFeatureLayer pInGeoFLayer = null;
        AxMapControl pMapControl = null;
        AxTOCControl pTocControl = null;
        AxSceneControl pSceneControl = null;
        FrmUnique FrmUnique = null;
        IUniqueValueRenderer pUniquerend = new UniqueValueRendererClass();
        public FrmUniqueInLayer(ILayer layer, AxMapControl MapControl,AxTOCControl toccontrol, FrmUnique Frm,AxSceneControl scenecontrol)
        {
            InitializeComponent();
            this.EnableGlass = false;
            pLayer = layer;
            pMapControl = MapControl;
            pTocControl = toccontrol;
            FrmUnique = Frm;
            pSceneControl = scenecontrol;
        }

        private void FrmUniqueInLayer_Load(object sender, EventArgs e)
        {
            pFLayer = (IFeatureLayer)pLayer;
            //初始化图层列表
            IEnumLayer pEnumLayer = pMapControl.Map.get_Layers(null, true);
            pEnumLayer.Reset();
            ILayer pInlayer = null;

            if (pFLayer.FeatureClass.ShapeType == esriGeometryType.esriGeometryMultipoint || pFLayer.FeatureClass.ShapeType == esriGeometryType.esriGeometryPoint)
            {
                pEnumLayer.Reset();
                while ((pInlayer = pEnumLayer.Next()) != null)
                {
                    if (pInlayer is IFeatureLayer)
                    {
                        IFeatureLayer pInFlayer = (IFeatureLayer)pInlayer;
                        IGeoFeatureLayer pGeoFeaLayer = (IGeoFeatureLayer)pInFlayer;
                        if (pInFlayer.FeatureClass.ShapeType == esriGeometryType.esriGeometryMultipoint || pInFlayer.FeatureClass.ShapeType == esriGeometryType.esriGeometryPoint)
                        {
                            if (pGeoFeaLayer.Renderer is IUniqueValueRenderer)
                            {
                                cmbInlayer.Items.Add(pInFlayer.Name);
                            }
                        }
                    }
                }
            }
            else if (pFLayer.FeatureClass.ShapeType == esriGeometryType.esriGeometryPolyline || pFLayer.FeatureClass.ShapeType == esriGeometryType.esriGeometryLine)
            {
                pEnumLayer.Reset();
                while ((pInlayer = pEnumLayer.Next()) != null)
                {
                    if (pInlayer is IFeatureLayer)
                    {
                        IFeatureLayer pInFlayer = (IFeatureLayer)pInlayer;
                        IGeoFeatureLayer pGeoFeaLayer = (IGeoFeatureLayer)pInFlayer;
                        if (pInFlayer.FeatureClass.ShapeType == esriGeometryType.esriGeometryPolyline || pInFlayer.FeatureClass.ShapeType == esriGeometryType.esriGeometryLine)
                        {
                            if (pGeoFeaLayer.Renderer is IUniqueValueRenderer)
                            {
                                cmbInlayer.Items.Add(pInFlayer.Name);
                            }
                        }
                    }
                }
            }
            else if (pFLayer.FeatureClass.ShapeType == esriGeometryType.esriGeometryPolygon)
            {
                 pEnumLayer.Reset();
                while ((pInlayer = pEnumLayer.Next()) != null)
                {
                    if (pInlayer is IFeatureLayer)
                    {
                        IFeatureLayer pInFlayer = (IFeatureLayer)pInlayer;
                        IGeoFeatureLayer pGeoFeaLayer = (IGeoFeatureLayer)pInFlayer;
                        if (pInFlayer.FeatureClass.ShapeType == esriGeometryType.esriGeometryPolygon)
                        {
                            if (pGeoFeaLayer.Renderer is IUniqueValueRenderer)
                            {
                                cmbInlayer.Items.Add(pInFlayer.Name);
                            }
                        }
                    }
                }
            }
            if (cmbInlayer.Items.Count==0)
            {
                MessageBox.Show("当前地图中没有合适的图层", "提示", MessageBoxButtons.OK);
                this.Close();
                return;
            }
            if (cmbInlayer.Items.Count > 0)
            {
                cmbInlayer.SelectedIndex = 0;
            }
        }

        private void cmbInlayer_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbfield1.Items.Clear();
            ILayer playermap = ClsGDBDataCommon.GetLayerFromName(pMapControl.Map, cmbInlayer.Text);
            if (playermap is IFeatureLayer)
            {
                IFeatureLayer pfeaturelayer = (IFeatureLayer)playermap;
                IGeoFeatureLayer pGeofeaturelayer = (IGeoFeatureLayer)pfeaturelayer;
                if (pGeofeaturelayer.Renderer is IUniqueValueRenderer)
                {
                    pInLayer = playermap;
                    pInFLayer = (IFeatureLayer)pInLayer;
                    pInGeoFLayer = (IGeoFeatureLayer)pInFLayer;
                    pUniquerend = pInGeoFLayer.Renderer as IUniqueValueRenderer;
                }
            }                    
                
            if (pUniquerend.FieldCount==1)
            {
                cmbfield2.Enabled = false;
                cmbfield3.Enabled = false;
                ITable pTbale = pLayer as ITable;
                IFields pFields = pTbale.Fields;
                for (int i=2; i < pFields.FieldCount;i++ )
                {
                    cmbfield1.Items.Add(pFields.get_Field(i).Name);
                }
                string uniquefield = pUniquerend.get_Field(0);
                if (cmbfield1.Items.Contains(uniquefield))
                {
                    cmbfield1.Text = uniquefield;
                } 
                else
                {
                    MessageBox.Show("当前图层中不包含导入符号图层的独立值字段", "提示", MessageBoxButtons.OK);
                    //this.Close();
                }
            }

        }

        private void btnok_Click(object sender, EventArgs e)
        {            
            if (pUniquerend.FieldCount==1)
            {
                IGeoFeatureLayer pGEOFeaturelayer = pFLayer as IGeoFeatureLayer;
                IDisplayTable pDisTable = pGEOFeaturelayer as IDisplayTable;
                ISimpleLineSymbol pLineSymbol = new SimpleLineSymbolClass();
                pLineSymbol.Style = esriSimpleLineStyle.esriSLSSolid;
                pLineSymbol.Width = 1;

                ISimpleFillSymbol pSimpleFillSymbol = new SimpleFillSymbolClass();
                pSimpleFillSymbol.Style = esriSimpleFillStyle.esriSFSSolid;
                pSimpleFillSymbol.Outline.Width = 0.4;

                ISimpleMarkerSymbol pPointSymbol = new SimpleMarkerSymbolClass();
                pPointSymbol.Style = esriSimpleMarkerStyle.esriSMSCircle;
                pPointSymbol.Size = 6;

                FrmUnique.pUniqueRender.RemoveAllValues();
                FrmUnique.pUniqueRender.FieldCount = 1;
                FrmUnique.pUniqueRender.set_Field(0, cmbfield1.Text);
                if (pFLayer.FeatureClass.ShapeType == esriGeometryType.esriGeometryMultipoint || pFLayer.FeatureClass.ShapeType == esriGeometryType.esriGeometryPoint)
                {
                    FrmUnique.pUniqueRender.DefaultSymbol = pPointSymbol as ISymbol;
                    FrmUnique.pUniqueRender.UseDefaultSymbol = true;
                }
                else if (pFLayer.FeatureClass.ShapeType == esriGeometryType.esriGeometryPolyline || pFLayer.FeatureClass.ShapeType == esriGeometryType.esriGeometryLine)
                {
                    FrmUnique.pUniqueRender.DefaultSymbol = pLineSymbol as ISymbol;
                    FrmUnique.pUniqueRender.UseDefaultSymbol = true;
                }
                else if (pFLayer.FeatureClass.ShapeType == esriGeometryType.esriGeometryPolygon)
                {
                    FrmUnique.pUniqueRender.DefaultSymbol = pSimpleFillSymbol as ISymbol;
                    FrmUnique.pUniqueRender.UseDefaultSymbol = true;
                }
                
                IFeatureCursor pFeatureCursor = pDisTable.SearchDisplayTable(null, false) as IFeatureCursor;
                IFeatureCursor pFeatureCursor1 = pDisTable.SearchDisplayTable(null, false) as IFeatureCursor;
                IFeature pFeature = pFeatureCursor.NextFeature();
                IFeature mFeature = pFeatureCursor1.NextFeature();

                bool ValFound;
                IFields pFields = pFeatureCursor.Fields;
                int fieldIndex = pFields.FindField(cmbfield1.Text);
                while (pFeature !=null)
                {
                    object classValue;
                    classValue = pFeature.get_Value(fieldIndex);
                    ValFound = false;
                    for (int i = 0; i <= FrmUnique.pUniqueRender.ValueCount - 1; i++)
                    {
                        if (FrmUnique.pUniqueRender.get_Value(i) == classValue.ToString())
                        {
                            ValFound = true;
                            break; //Exit the loop if the value was found.
                        }
                    }
                    if (ValFound == false)
                    {
                        bool valfudIn = false;
                        for (int i=0;i<pUniquerend.ValueCount;i++)
                        {
                            if (pUniquerend.get_Value(i)==classValue.ToString())
                            {
                                valfudIn = true;
                                FrmUnique.pUniqueRender.AddValue(classValue.ToString(),cmbfield1.Text,pUniquerend.get_Symbol(classValue.ToString()));
                                FrmUnique.pUniqueRender.set_Label(classValue.ToString(), classValue.ToString());
                                FrmUnique.pUniqueRender.set_Symbol(classValue.ToString(), pUniquerend.get_Symbol(classValue.ToString()));
                                break;
                            }
                        }
                        if (valfudIn==false)
                        {
                            if (pFLayer.FeatureClass.ShapeType == esriGeometryType.esriGeometryMultipoint || pFLayer.FeatureClass.ShapeType == esriGeometryType.esriGeometryPoint)
                            {
                                FrmUnique.pUniqueRender.AddValue(classValue.ToString(), cmbfield1.Text, pPointSymbol as ISymbol);
                                FrmUnique.pUniqueRender.set_Label(classValue.ToString(), classValue.ToString());
                                FrmUnique.pUniqueRender.set_Symbol(classValue.ToString(), pPointSymbol as ISymbol);
                            }
                            else if (pFLayer.FeatureClass.ShapeType == esriGeometryType.esriGeometryPolyline || pFLayer.FeatureClass.ShapeType == esriGeometryType.esriGeometryLine)
                            {
                                FrmUnique.pUniqueRender.AddValue(classValue.ToString(), cmbfield1.Text, pLineSymbol as ISymbol);
                                FrmUnique.pUniqueRender.set_Label(classValue.ToString(), classValue.ToString());
                                FrmUnique.pUniqueRender.set_Symbol(classValue.ToString(), pLineSymbol as ISymbol);
                            }
                            else if (pFLayer.FeatureClass.ShapeType == esriGeometryType.esriGeometryPolygon)
                            {
                                FrmUnique.pUniqueRender.AddValue(classValue.ToString(), cmbfield1.Text, pSimpleFillSymbol as ISymbol);
                                FrmUnique.pUniqueRender.set_Label(classValue.ToString(), classValue.ToString());
                                FrmUnique.pUniqueRender.set_Symbol(classValue.ToString(), pSimpleFillSymbol as ISymbol);
                            }
                        }
                                              
                    }
                    pFeature = pFeatureCursor.NextFeature();
                }
                FrmUnique.pUniqueRender.DefaultLabel = null;
                FrmUnique.pUniqueRender.UseDefaultSymbol = false;
                FrmUnique.pUniqueRender.ColorScheme = "Custom";
                ITable pTable = pDisTable as ITable;
                bool isString = pTable.Fields.get_Field(fieldIndex).Type == esriFieldType.esriFieldTypeString;
                FrmUnique.pUniqueRender.set_FieldType(0, isString);
                pGEOFeaturelayer.Renderer = FrmUnique.pUniqueRender as IFeatureRenderer;
                IUID pUID = new UIDClass();
                pUID.Value = "{683C994E-A17B-11D1-8816-080009EC732A}";
                pGEOFeaturelayer.RendererPropertyPageClassID = pUID as UIDClass;
                if (pTocControl.Buddy == pMapControl.Object)
                {
                    pTocControl.SetBuddyControl(pMapControl);
                    pMapControl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
                    pTocControl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
                }
                else if (pTocControl.Buddy.Equals(pSceneControl.Object))
                {
                    pTocControl.SetBuddyControl(pSceneControl);
                    IActiveView pActiveView = pSceneControl.Scene as IActiveView;
                    pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, pLayer, null);
                    pTocControl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
                } 
            }
            this.Close();
            FrmUnique.Close();
        }

        private void btncancle_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
