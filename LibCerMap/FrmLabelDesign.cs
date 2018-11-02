using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Drawing.Text;
using stdole;

using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;

using DevComponents.DotNetBar;

namespace LibCerMap
{
    public partial class FrmLabelDesign : OfficeForm
    {
        AxMapControl pMapControl = null;
        AxTOCControl pTocControl = null;
        AxSceneControl pSceneControl = null;
        ILayer pLayer = null;
        ITextSymbol pTextSymbol = new TextSymbolClass();

        #region Color类

  
  
        #endregion

        public FrmLabelDesign(AxMapControl mapControl, AxTOCControl toccontrol, ILayer layer, AxSceneControl sceneControl)
        {
            InitializeComponent();
            this.EnableGlass = false;
            pMapControl = mapControl;
            pTocControl = toccontrol;
            pLayer = layer;
            pSceneControl = sceneControl;
        }

        private void FrmLabelDesign_Load(object sender, EventArgs e)
        {
            IGeoFeatureLayer geoFeatureLayer = (IGeoFeatureLayer)pLayer;
            IAnnotateLayerPropertiesCollection pAnnColl = geoFeatureLayer.AnnotationProperties;
            IAnnotateLayerPropertiesCollection2 pAnnColl2 = (IAnnotateLayerPropertiesCollection2)pAnnColl;
            IAnnotateLayerProperties pAnnProp = pAnnColl2.get_Properties(0);
            ILabelEngineLayerProperties pLable = (ILabelEngineLayerProperties)pAnnProp;
            IBasicOverposterLayerProperties4 pBasic = (IBasicOverposterLayerProperties4)pLable.BasicOverposterLayerProperties;
            //IPointPlacementPriorities pPlace = pBasic.PointPlacementPriorities;
            
            chklabel.Checked = pAnnProp.DisplayAnnotation;
            ITable pTable = (ITable)pLayer;
            IFields pFields = pTable.Fields;
            for (int i = 0; i < pFields.FieldCount;i++ )
            {
                if (pFields.Field[i].Name!="Shape")
                {
                     cmbfields.Items.Add(pFields.Field[i].Name);
                }              
            }
            //if (chklabel.Checked)
            //{
            //    cmbfields.Text = pLable.Expression;
            //} 
            //else
            //{
            //    cmbfields.SelectedIndex = 0;
            //}
            cmbfields.Text = pLable.Expression;

            InstalledFontCollection pFontCollection = new InstalledFontCollection();
            FontFamily[] pFontFamily = pFontCollection.Families;
            for (int i = 0; i < pFontFamily.Length; i++)
            {
                string pFontName = pFontFamily[i].Name;
                this.cmbfont.Items.Add(pFontName);
            }

            //标注方向
            ILineLabelPosition pLineLabelPosition=pBasic.LineLabelPosition;
            if (pLineLabelPosition != null)
            {
                if (pLineLabelPosition.Horizontal)  //水平
                {
                    radioBtnHorizontal.Checked = true;
                    radioBtnParallel.Checked = false;
                    radioBtnPerpendicular.Checked = false;
                }
                if (pLineLabelPosition.Perpendicular)//垂直
                {
                    radioBtnHorizontal.Checked = false;
                    radioBtnParallel.Checked = false;
                    radioBtnPerpendicular.Checked = true;
                }
                if (pLineLabelPosition.Parallel)//平行
                {
                    radioBtnHorizontal.Checked = false;
                    radioBtnParallel.Checked = true;
                    radioBtnPerpendicular.Checked = false;
                }
            }            

            if (pLable.Symbol!=null)
            {
                pTextSymbol = pLable.Symbol;
                stdole.IFontDisp pFont = pTextSymbol.Font;
                symbolcolor.SelectedColor = ClsGDBDataCommon.IColorToColor(pTextSymbol.Color);
                //cmbfont.Text = pTextSymbol.Font.Name;
                for (int i = 0; i < cmbfont.Items.Count;i++ )
                {
                    if (cmbfont.Items[i].ToString()==pTextSymbol.Font.Name)
                    {
                        cmbfont.SelectedIndex = i;
                    }
                }
                cmbsize.Text = pTextSymbol.Size.ToString();
                if (pFont.Bold==true)
                {
                    toolBtnBold.Checked = true;
                }
                else
                {
                    toolBtnBold.Checked = false;
                }
                if (pFont.Italic == true)
                {
                    toolBtnIntend.Checked = true;
                }
                else
                {
                    toolBtnIntend.Checked = false;
                }
                if (pFont.Underline == true)
                {
                    toolBtnUnderline.Checked = true;
                }
                else
                {
                    toolBtnUnderline.Checked = false;
                }
                if (pFont.Strikethrough == true)
                {
                    toolBtnStrikethrough.Checked = true;
                }
                else
                {
                    toolBtnStrikethrough.Checked = false;
                }

            } 
            else
            {
                symbolcolor.SelectedColor = Color.Black;
                cmbfont.SelectedIndex =cmbfont.Items.Count-9;
                cmbsize.Text = "8";
                toolBtnBold.Checked = false;
                toolBtnIntend.Checked = false;
                toolBtnUnderline.Checked = false;
                toolBtnStrikethrough.Checked = false;
            }
        }

        private void btnuse_Click(object sender, EventArgs e)
        {
            try
            {

                IFontDisp pFont = new StdFontClass() as IFontDisp;
                pTextSymbol.Color = ClsGDBDataCommon.ColorToIColor(symbolcolor.SelectedColor);
                pFont.Bold = toolBtnBold.Checked;
                pFont.Italic = toolBtnIntend.Checked;
                pFont.Strikethrough = toolBtnStrikethrough.Checked;
                pFont.Underline = toolBtnUnderline.Checked;
                pFont.Name = cmbfont.Text;
                pFont.Size = decimal.Parse(cmbsize.Text);
                pTextSymbol.Font = pFont;

                IGeoFeatureLayer pGeoLayer = (IGeoFeatureLayer)pLayer;
                pGeoLayer.AnnotationProperties.Clear();
                ILabelEngineLayerProperties pLabelEngine = new LabelEngineLayerPropertiesClass();
                pLabelEngine.Symbol = pTextSymbol;

                //pLabelEngine2.BasicOverposterLayerProperties = pBasicProperties as IBasicOverposterLayerProperties;

                if (cmbfields.Text.Contains("[") && cmbfields.Text.Contains("]"))
                {
                    pLabelEngine.Expression = cmbfields.Text;
                }
                else
                {
                    string Exptext = "[" + cmbfields.Text + "]";
                    pLabelEngine.Expression = Exptext;
                }

                IBasicOverposterLayerProperties4 pBasic = new BasicOverposterLayerPropertiesClass();
                //pBasic.PolygonPlacementMethod = esriOverposterPolygonPlacementMethod.esriAlwaysHorizontal;
                if (chkOverLap.Checked)
                {
                    pBasic.GenerateUnplacedLabels = true;
                }
                if (pLayer is IFeatureLayer)
                {
                    IFeatureLayer featureLayer = pLayer as FeatureLayer;
                    if (featureLayer.FeatureClass == null) return;

                    if (featureLayer.FeatureClass.ShapeType == esriGeometryType.esriGeometryPoint)
                    {
                        IPointPlacementPriorities pPointPlace = new PointPlacementPrioritiesClass();
                        pBasic.PointPlacementMethod = esriOverposterPointPlacementMethod.esriAroundPoint;
                        pBasic.PointPlacementOnTop = radioCenterTop.Checked;

                        SetPointPlacementPriorities(pPointPlace);
                        pBasic.PointPlacementPriorities = pPointPlace;
                    }
                    else if (featureLayer.FeatureClass.ShapeType == esriGeometryType.esriGeometryPolyline)
                    {
                        //更改角度
                         //= new LineLabelPositionClass();
                        IClone pSrcClone = pBasic.LineLabelPosition as IClone;
                        IClone pDstClone = pSrcClone.Clone();
                        ILineLabelPosition pLineLabelPosition = pDstClone as ILineLabelPosition;
                        if (pLineLabelPosition != null)
                        {
                            if (radioBtnHorizontal.Checked)
                            {
                                pLineLabelPosition.Horizontal = true;
                                pLineLabelPosition.Parallel = false;
                                pLineLabelPosition.Perpendicular = false;
                            }

                            if (radioBtnPerpendicular.Checked)
                            {
                                pLineLabelPosition.Horizontal = false;
                                pLineLabelPosition.Parallel = false;
                                pLineLabelPosition.Perpendicular = true;
                            }

                            if (radioBtnParallel.Checked)
                            {
                                pLineLabelPosition.Horizontal = false;
                                pLineLabelPosition.Parallel = true;
                                pLineLabelPosition.Perpendicular = false;
                            }
                        }                        
                        
                        pBasic.LineLabelPosition = pLineLabelPosition;
                        //pBasic.PerpendicularToAngle = true;
                    }
                    else if (featureLayer.FeatureClass.ShapeType == esriGeometryType.esriGeometryPolygon)
                    {
                        pBasic.PolygonPlacementMethod = esriOverposterPolygonPlacementMethod.esriAlwaysHorizontal;
                    }
                }

                //赋给labelEngine
                pLabelEngine.BasicOverposterLayerProperties = pBasic as IBasicOverposterLayerProperties;


                IAnnotateLayerProperties pAnnoLayerP = (IAnnotateLayerProperties)pLabelEngine;
                pAnnoLayerP.DisplayAnnotation = chklabel.Checked;
                pAnnoLayerP.FeatureLayer = pGeoLayer;
                pAnnoLayerP.LabelWhichFeatures = esriLabelWhichFeatures.esriVisibleFeatures;
                pAnnoLayerP.WhereClause = "";

                pGeoLayer.AnnotationProperties.Add(pAnnoLayerP);
                pGeoLayer.DisplayAnnotation = chklabel.Checked;


                pMapControl.ActiveView.Refresh();
                //if (pTocControl.Buddy == pMapControl.Object)
                //{
                //    pTocControl.SetBuddyControl(pMapControl);
                //    pMapControl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
                //    pTocControl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
                //}
                //else if (pTocControl.Buddy.Equals(pSceneControl.Object))
                //{
                //    pTocControl.SetBuddyControl(pSceneControl);
                //    IActiveView pActiveView = pSceneControl.Scene as IActiveView;
                //    pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, pLayer, null);
                //    pTocControl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
                //} 
            }
            catch (System.Exception ex)
            {

            }
        }

        private void SetPointPlacementPriorities(IPointPlacementPriorities pointPlace)
        {
            InitialPointPlacementPriorities(pointPlace);
            if (radioAboveCenter.Checked)
            {
                pointPlace.AboveCenter =1;
            }
            else if (radioAboveLeft.Checked)
            {
                pointPlace.AboveLeft = 1;
            }
            else if (radioAboveRight.Checked)
            {
                pointPlace.AboveRight = 1;
            }
            else if (radioCenterLeft.Checked)
            {
                pointPlace.CenterLeft = 1;
            }
            else if (radioCenterRight.Checked)
            {
                pointPlace.CenterRight = 1;
            }
            else if (radioBelowLeft.Checked)
            {
                pointPlace.BelowLeft = 1;
            }
            else if (radioBelowCenter.Checked)
            {
                pointPlace.BelowCenter = 1;
            }
            else if (radioBelowRight.Checked)
            {
                pointPlace.BelowRight = 1;
            } 
            
        }
        private void InitialPointPlacementPriorities(IPointPlacementPriorities pointPlace)
        {
            pointPlace.AboveLeft = 0;
            pointPlace.AboveCenter = 0;
            pointPlace.AboveRight = 0;
            pointPlace.CenterLeft = 0;
            pointPlace.CenterRight = 0;
            pointPlace.BelowLeft = 0;
            pointPlace.BelowCenter = 0;
            pointPlace.BelowRight = 0;
        }
        private void btnok_Click(object sender, EventArgs e)
        {
            btnuse_Click(sender, e);
            this.Close();
        }

        /// <summary>
        /// 组合标注
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnexpression_Click(object sender, EventArgs e)
        {
            FrmLabelExpression frmexpress = new FrmLabelExpression(this.cmbfields,pLayer);
            frmexpress.ShowDialog();
            frmexpress.StartPosition = FormStartPosition.CenterScreen;
            frmexpress.ShowInTaskbar = false;
        }

        /// <summary>
        /// 标注符号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnsymbol_Click(object sender, EventArgs e)
        {
            FrmTextSymbol frmtext = new FrmTextSymbol();
            frmtext.StartPosition = FormStartPosition.CenterScreen;
            frmtext.ShowInTaskbar = false;
            frmtext.ShowDialog();           
            pTextSymbol = frmtext.GetTextSymbol();
            if (pTextSymbol != null)
            {                
                stdole.IFontDisp pFont = pTextSymbol.Font;
                symbolcolor.SelectedColor = ClsGDBDataCommon.IColorToColor(pTextSymbol.Color);
                //cmbfont.Text = pTextSymbol.Font.Name;
                for (int i = 0; i < cmbfont.Items.Count; i++)
                {
                    if (cmbfont.Items[i].ToString() == pTextSymbol.Font.Name)
                    {
                        cmbfont.SelectedIndex = i;
                    }
                }
                cmbsize.Text = pTextSymbol.Size.ToString();
                if (pFont.Bold == true)
                {
                    toolBtnBold.Checked = true;
                }
                else
                {
                    toolBtnBold.Checked = false;
                }
                if (pFont.Italic == true)
                {
                    toolBtnIntend.Checked = true;
                }
                else
                {
                    toolBtnIntend.Checked = false;
                }
                if (pFont.Underline == true)
                {
                    toolBtnUnderline.Checked = true;
                }
                else
                {
                    toolBtnUnderline.Checked = false;
                }
                if (pFont.Strikethrough == true)
                {
                    toolBtnStrikethrough.Checked = true;
                }
                else
                {
                    toolBtnStrikethrough.Checked = false;
                }

            }
            else
            {
                IFontDisp pFont = new StdFontClass() as IFontDisp;
                pTextSymbol.Color = ClsGDBDataCommon.ColorToIColor(symbolcolor.SelectedColor);
                pFont.Bold = toolBtnBold.Checked;
                pFont.Italic = toolBtnIntend.Checked;
                pFont.Strikethrough = toolBtnStrikethrough.Checked;
                pFont.Underline = toolBtnUnderline.Checked;
                pFont.Name = cmbfont.Text;
                pFont.Size = decimal.Parse(cmbsize.Text);
                pTextSymbol.Font = pFont;

            }
        }

        private void chklabel_CheckedChanged(object sender, EventArgs e)
        {
            if (chklabel.Checked==true)
            {
                btnConvetAnno.Enabled = true;
            } 
            else
            {
                btnConvetAnno.Enabled = false;
            }
        }

        private void btnConvetAnno_Click(object sender, EventArgs e)
        {
#region 

            //IFeatureLayer pFeatureLayer = pLayer as IFeatureLayer;
            //IFeatureClass pFeatureClass = pFeatureLayer.FeatureClass;
            //IFeatureDataset pFdataSet = pFeatureClass.FeatureDataset;
            //IDataset pDataSet = pFeatureClass as IDataset;
            //IWorkspace pWorkspace = pDataSet.Workspace;
            //IFeatureWorkspaceAnno pAnnoWorkSpace = pWorkspace as IFeatureWorkspaceAnno;
            //IObjectClassDescription pOCDes = new AnnotationFeatureClassDescriptionClass();
            //IFeatureClassDescription fcDescription = (IFeatureClassDescription)pOCDes;

            //IGeoFeatureLayer geoFeatureLayer = (IGeoFeatureLayer)pLayer;
            //IAnnotateLayerPropertiesCollection pAnnColl = geoFeatureLayer.AnnotationProperties;
            //IAnnotateLayerPropertiesCollection2 pAnnColl2 = (IAnnotateLayerPropertiesCollection2)pAnnColl;
            //IAnnotateLayerProperties pAnnProp = pAnnColl2.get_Properties(0);
            //pAnnProp.Class="Annotation Class 1";
            //ILabelEngineLayerProperties pLable = (ILabelEngineLayerProperties)pAnnProp;
            //IAnnotateLayerPropertiesCollection pAnnPropColl=new AnnotateLayerPropertiesCollectionClass();
           
            //ITextSymbol annotationTextSymbol = pLable.Symbol;
            //ISymbol annotationSymbol = (ISymbol)annotationTextSymbol;
            //ISymbolCollection symbolCollection = new SymbolCollectionClass();
            //ISymbolCollection2 symbolCollection2 = (ISymbolCollection2)symbolCollection;
            //ISymbolIdentifier2 symbolIdentifier2 = null;
            //symbolCollection2.AddSymbol(annotationSymbol, "Annotation Class 1", out symbolIdentifier2);
            //pLable.SymbolID = symbolIdentifier2.ID;

            //pAnnPropColl.Add(pAnnProp);

            //IGraphicsLayerScale graphicsLayerScale = new GraphicsLayerScaleClass();
            //graphicsLayerScale.ReferenceScale = pAnnProp.AnnotationMinimumScale;
            //graphicsLayerScale.Units = esriUnits.esriFeet;

            //IOverposterProperties overposterProperties = new BasicOverposterPropertiesClass();
            //IFields requiredFields = pOCDes.RequiredFields;
            //int shapeFieldIndex = requiredFields.FindField(fcDescription.ShapeFieldName);
            //IField shapeField = requiredFields.get_Field(shapeFieldIndex);
            //IGeometryDef geometryDef = shapeField.GeometryDef;
            //IGeometryDefEdit geometryDefEdit = (IGeometryDefEdit)geometryDef;
            //geometryDefEdit.SpatialReference_2 = geometryDef.SpatialReference;
            //IAnnotationLayerFactory annotationLayerFactory = new FDOGraphicsLayerFactoryClass();

            ////IFeatureClass pclass=pAnnoWorkSpace.CreateAnnotationClass(pLayer.Name + "注记图层", pOCDes.RequiredFields, pOCDes.InstanceCLSID, pOCDes.ClassExtensionCLSID, pLayer.Name, "", pFdataSet, pFeatureClass, pAnnPropColl, graphicsLayerScale.ReferenceScale, symbolCollection, true);
            //ClsGDBDataCommon cgc = new ClsGDBDataCommon();
            //IFeatureWorkspace wk = cgc.OpenFromFileGDB(@"C:\Users\zhaoqiang\Documents\ArcGIS\Default.gdb") as IFeatureWorkspace;
            ////IAnnotationLayer annotationLayer = annotationLayerFactory.CreateAnnotationLayer
            ////                                   ((IFeatureWorkspace)pWorkspace, pFdataSet, pAnnProp.Class, geometryDef, null,
            ////                                    pAnnColl, graphicsLayerScale, symbolCollection, false,
            ////                                    false, false, true, overposterProperties, "default");

            //IAnnotationLayer annotationLayer = annotationLayerFactory.CreateAnnotationLayer
            //                                  ((IFeatureWorkspace)wk, null, pAnnProp.Class, geometryDef, null,
            //                                   pAnnColl, graphicsLayerScale, symbolCollection, true,
            //                                   false, false, true, overposterProperties, "default");
            //pMapControl.AddLayer(annotationLayer as ILayer);
            //pMapControl.Refresh();
            //pTocControl.Refresh();
   
    //// Get the feature class from the feature layer.
    //IFeatureLayer featureLayer = (IFeatureLayer)annotationLayer;
    //IFeatureClass featureClass = featureLayer.FeatureClass;

    //return featureClass;
            //int index = 0;
            //for (int i = 0; i<pMapControl.Map.LayerCount;i++ )
            //{
            //    if (pMapControl.Map.Layer[i].Name==pLayer.Name)
            //    {
            //        i = index;
            //        break;
            //    }
            //}

#endregion
            ConvertLabelsToGDBAnnotationSingleLayer(pMapControl.Map, false);//(pMapControl.Map, index, false)可能会存在两个同名图层，这样会出问题

        }

        //public IFeatureDataset CreateFeatureDataset_Example(IWorkspace workspace, string fdsName, ISpatialReference fdsSR)
        //{
        //    IFeatureWorkspace featureWorkspace = (IFeatureWorkspace)workspace;
        //    return featureWorkspace.CreateFeatureDataset(fdsName, fdsSR);
        //}

        public void ConvertLabelsToGDBAnnotationSingleLayer(IMap pMap, bool   featureLinked)//(IMap pMap, int layerIndex, bool   featureLinked)
        {
            try
            {
                IConvertLabelsToAnnotation pConvertLabelsToAnnotation = new
                ConvertLabelsToAnnotationClass();
                ITrackCancel pTrackCancel = new CancelTrackerClass();
                //Change global level options for the conversion by sending in different parameters to the next line.
                pConvertLabelsToAnnotation.Initialize(pMap,
                    esriAnnotationStorageType.esriDatabaseAnnotation,
                    esriLabelWhichFeatures.esriAllFeatures, true, pTrackCancel, null);

                //ILayer pLayer = pMap.get_Layer(layerIndex);
                IGeoFeatureLayer pGeoFeatureLayer = pLayer as IGeoFeatureLayer;
                if (pGeoFeatureLayer != null)
                {
                    IFeatureClass pFeatureClass = pGeoFeatureLayer.FeatureClass;
                    IDataset pDataset = pFeatureClass as IDataset;

                    IFeatureWorkspace pFeatureWorkspace = pDataset.Workspace as
                        IFeatureWorkspace;
                    //Add the layer information to the converter object. Specify the parameters of the output annotation feature class here as well.
                    //if (pFeatureClass.FeatureDataset != null)
                    //{
                    //    pConvertLabelsToAnnotation.AddFeatureLayer(pGeoFeatureLayer,
                    //    pGeoFeatureLayer.Name + "_Anno", pFeatureWorkspace,
                    //    pFeatureClass.FeatureDataset, featureLinked, false, false, true, true, "");
                    //} 
                    //else
                    //{
                    //必须指定路径，默认存放到原有GDB中会造成无法第二次创建的问题
                    if (MessageBox.Show("请指定一个位置生成GeoDatabase存放注记图层", "提示", MessageBoxButtons.OKCancel) == DialogResult.OK)
                    {
                        SaveFileDialog saveFileDlg = new SaveFileDialog();
                        saveFileDlg.DefaultExt = "gdb";
                        saveFileDlg.Filter = "(文件型数据库)*.gdb|*.gdb";
                        if (saveFileDlg.ShowDialog() == DialogResult.OK)
                        {
                            string strFileGDB = saveFileDlg.FileName;

                            string strPath = System.IO.Path.GetDirectoryName(strFileGDB);
                            string strName = System.IO.Path.GetFileName(strFileGDB);

                            ClsGDBDataCommon com = new ClsGDBDataCommon();
                            com.Create_FileGDB(strPath, strName);
                            string strfile = strPath + "\\" + strName;
                            IWorkspace pws = com.OpenFromFileGDB(strfile);
                            IFeatureDataset pdataset = creatdataset(pws, pLayer.Name + "Anno", pMap.SpatialReference);
                            pConvertLabelsToAnnotation.AddFeatureLayer(pGeoFeatureLayer,
                            pGeoFeatureLayer.Name + "_Anno", (IFeatureWorkspace)pws,
                            pdataset, featureLinked, false, false, true, true, "");
                        }
                    }
                    //}                 
                    //Do the conversion.
                    pConvertLabelsToAnnotation.ConvertLabels();
                    IEnumLayer pEnumLayer = pConvertLabelsToAnnotation.AnnoLayers;
                    //Turn off labeling for the layer converted.
                    pGeoFeatureLayer.DisplayAnnotation = false;

                    //Add the result annotation layer to the map.
                    pMap.AddLayers(pEnumLayer, true);

                    //Refresh the map to update the display.
                    IActiveView pActiveView = pMap as IActiveView;
                    pActiveView.Refresh();
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("该图层已存在注记图层","提示",MessageBoxButtons.OK);
            }
           
        }

        public IFeatureDataset creatdataset(IWorkspace workspace, string fdsName, ISpatialReference fdsSR)
        {
            IFeatureWorkspace featureWorkspace = (IFeatureWorkspace)workspace;
            return featureWorkspace.CreateFeatureDataset(fdsName, fdsSR);
        }

        private void btncancle_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmbsize_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void lblsize_Click(object sender, EventArgs e)
        {

        }

       
      
    }
}
