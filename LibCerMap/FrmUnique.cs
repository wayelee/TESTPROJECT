using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;

using DevComponents.DotNetBar;

namespace LibCerMap
{
    public partial class FrmUnique : Form
    {
        ILayer pLayer = null;
        AxMapControl pMapControl = null;
        AxTOCControl pTocControl = null;
        AxSceneControl pSceneControl = null;

        IFeatureLayer pFLayer = null;      
        IFields pFields = null;
        IField pField = null;
        IFeatureClass pFClass = null;
        IGeoFeatureLayer pGeoFeatureLayer = null;
        ISymbologyStyleClass pSymbolClass = null;
        IDisplayTable pDisplayTable = null;
        IRandomColorRamp pRandomRamp = new RandomColorRampClass();
        public IUniqueValueRenderer pUniqueRender = new UniqueValueRendererClass();
        public IUniqueValueRenderer pUniquerendermu = new UniqueValueRendererClass();

        ISimpleRenderer pRenderer = new SimpleRendererClass();
        

        int fieldIndex = 0;//选中的图层索引号
        int fieldIndex1 = 0;//多字段下选中的图层索引号
        int fieldIndex2 = 0;
        int fieldIndex3 = 0;
        int rowindex = 0;//用于记录要移除的值所在的行号
        int rowindexmu = 0;
        ArrayList rmvx = new ArrayList();//用于记录移除的值的label
        ArrayList revxmu = new ArrayList();
        List<IColor> coloru = new List<IColor>(); //记录移除值的颜色
        List<IColor> colorumu = new List<IColor>();
        List<long> countu = new List<long>();//记录移除值对应的对象数目
        List<long> countumu = new List<long>();
        List<long> cuntu=new List<long>();
        List<long> cuntumu=new List<long>();

        IFeatureLayer pFLayerIn = new FeatureLayerClass();//导入符号的图层

        public FrmUnique(AxMapControl mmapcontrol, AxTOCControl mtoccontrol, ILayer layer, AxSceneControl sceneControl)
        {
            InitializeComponent();
            pMapControl = mmapcontrol;
            pTocControl = mtoccontrol;
            pLayer = layer;
            pSceneControl = sceneControl;
        }

        private void FrmUnique_Load(object sender, EventArgs e)
        {
            try
            {
                treeshow.SelectedIndex = 0;
                pFLayer = (IFeatureLayer)pLayer;
                pGeoFeatureLayer = (IGeoFeatureLayer)pFLayer;
                pDisplayTable = pGeoFeatureLayer as IDisplayTable;
                pFClass = pFLayer.FeatureClass;
                if (pFClass == null)
                { 
                    this.Close();
                    return;
                }

                pFields = pFClass.Fields;
                string sInstall = ClsGDBDataCommon.GetParentPathofExe() + @"Resource\Style\ESRI.ServerStyle";
                axSymbologyControl1.LoadStyleFile(sInstall);
                axSymbologyControl1.StyleClass = esriSymbologyStyleClass.esriStyleClassColorRamps;
                pSymbolClass = axSymbologyControl1.GetStyleClass(esriSymbologyStyleClass.esriStyleClassColorRamps);

                cmbfield2.Items.Add("None");
                cmbfield3.Items.Add("None");

                for (int i = 0; i < pFields.FieldCount; i++)
                {
                    cmbfield.Items.Add(pFields.get_Field(i).Name);
                    cmbfield1.Items.Add(pFields.get_Field(i).Name);
                    cmbfield2.Items.Add(pFields.get_Field(i).Name);
                    cmbfield3.Items.Add(pFields.get_Field(i).Name);
                }
                cmbfield.SelectedIndex = 0;
                cmbfield1.SelectedIndex = 0;
                cmbfield2.SelectedIndex = 0;
                cmbfield3.SelectedIndex = 0;

                pRandomRamp.MaxSaturation = 100;
                pRandomRamp.MinSaturation = 0;
                pRandomRamp.StartHue = 0;
                pRandomRamp.EndHue = 360;
                pRandomRamp.MaxValue = 100;
                pRandomRamp.MinValue = 0;
                pRandomRamp.Size = 200;
                bool bok;
                pRandomRamp.CreateRamp(out bok);
                if (bok)
                {
                    initcmbbox();
                }
                initgrid();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void treeshow_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (treeshow.SelectedIndex==0)
            {
                grPanelfield.Visible = true;
                grpanelfieldmu.Visible = false;
                grpanelcolor.Visible = true;
                grpanelcolormu.Visible = false;
                gridviewu.Visible = true;
                gridviewmu.Visible = false;
            } 
            else
            {
                grPanelfield.Visible = false;
                grpanelfieldmu.Visible = true;
                grpanelcolor.Visible = false;
                grpanelcolormu.Visible =true;
                gridviewu.Visible = false;
                gridviewmu.Visible = true;
            }
        }

        /// <summary>
        /// 初始化datagridview
        /// </summary>
        private void initgrid()
        {
            IUniqueValueRenderer pUniquerend = new UniqueValueRendererClass();
            if (pGeoFeatureLayer.Renderer is IUniqueValueRenderer)
            {
                pUniquerend=pGeoFeatureLayer.Renderer as IUniqueValueRenderer;

                if (pUniquerend.FieldCount==1)
                {
                   
                    treeshow.SelectedIndex = 0;
                    cmbfield.Text = pUniquerend.get_Field(0);
                    DataRow headrow = dataTable2.NewRow();
                    headrow[1] = "<Heading>";
                    headrow[2] = pUniquerend.get_Field(0);
                    dataTable2.Rows.Add(headrow);

                    for (int i = 0; i < pUniquerend.ValueCount; i++)
                    {
                        DataRow pointrow = dataTable2.NewRow();
                        pointrow[1] = pUniquerend.get_Value(i);
                        pointrow[2] = pointrow[1];
                        dataTable2.Rows.Add(pointrow);
                    }
                    if (pFClass.ShapeType == esriGeometryType.esriGeometryLine || pFClass.ShapeType == esriGeometryType.esriGeometryPolyline)
                    {
                        for (int j = 0; j <= pUniquerend.ValueCount - 1; j++)
                        {
                            string xv;
                            xv = pUniquerend.get_Value(j);
                            if (xv != "")
                            {
                                //ISimpleLineSymbol pNextSymbol = new SimpleLineSymbolClass();
                                //pNextSymbol=pUniquerend.get_Symbol(xv) as ISimpleLineSymbol;
                                ILineSymbol pNextSymbol = pUniquerend.get_Symbol(xv) as ILineSymbol;
                                gridviewu.Rows[j + 1].Cells[0].Style.BackColor = ClsGDBDataCommon.IColorToColor(pNextSymbol.Color);
                            }
                        }
                        gridviewu.CurrentCell = null;
                    }
                    else if (pFClass.ShapeType == esriGeometryType.esriGeometryMultipoint || pFClass.ShapeType == esriGeometryType.esriGeometryPoint)
                    {
                        for (int j = 0; j <= pUniquerend.ValueCount - 1; j++)
                        {
                            string xv;
                            xv = pUniquerend.get_Value(j);
                            if (xv != "")
                            {
                                //ISimpleMarkerSymbol pNextSymbol = new SimpleMarkerSymbolClass();
                                //pNextSymbol = pUniquerend.get_Symbol(xv) as ISimpleMarkerSymbol;
                                IMarkerSymbol pNextSymbol = pUniquerend.get_Symbol(xv) as IMarkerSymbol;
                                gridviewu.Rows[j + 1].Cells[0].Style.BackColor = ClsGDBDataCommon.IColorToColor(pNextSymbol.Color);
                            }
                        }
                        gridviewu.CurrentCell = null;
                       
                    }
                    else if (pFClass.ShapeType == esriGeometryType.esriGeometryPolygon )
                    {
                        for (int j = 0; j <= pUniquerend.ValueCount - 1; j++)
                        {
                            string xv;
                            xv = pUniquerend.get_Value(j);
                            if (xv != "")
                            {
                                //ISimpleFillSymbol pNextSymbol = new SimpleFillSymbolClass();
                                //pNextSymbol = pUniquerend.get_Symbol(xv) as ISimpleFillSymbol;
                                IFillSymbol pNextSymbol = pUniquerend.get_Symbol(xv) as IFillSymbol;
                                gridviewu.Rows[j + 1].Cells[0].Style.BackColor = ClsGDBDataCommon.IColorToColor(pNextSymbol.Color);
                            }
                        }
                        gridviewu.CurrentCell = null;
                    }
                    else
                    {

                    }
                    pUniqueRender = pUniquerend;
                } 
                else
                {
                    

                    string strname = pUniquerend.get_Field(0);
                    cmbfield1.Text = pUniquerend.get_Field(0);
                    if (pUniquerend.FieldCount > 1)
                    {
                        for (int i = 1; i < pUniquerend.FieldCount; i++)
                        {
                            strname = "|" + pUniquerend.get_Field(i);
                        }
                    }
                    if (pUniquerend.FieldCount == 2)
                    {
                        cmbfield2.Text = pUniquerend.get_Field(1);
                    }
                    else if (pUniquerend.FieldCount == 3)
                    {
                        cmbfield2.Text = pUniquerend.get_Field(1);
                        cmbfield3.Text = pUniquerend.get_Field(2);
                    }
                    treeshow.SelectedIndex = 1;
                    DataRow headrow = dataTable3.NewRow();
                    headrow[1] = "<Heading>";
                    headrow[2] = strname;
                    dataTable3.Rows.Add(headrow);

                    for (int i = 0; i < pUniquerend.ValueCount; i++)
                    {
                        DataRow pointrow = dataTable3.NewRow();
                        pointrow[1] = pUniquerend.get_Value(i);
                        pointrow[2] = pointrow[1];
                        dataTable3.Rows.Add(pointrow);
                    }
                    if (pFClass.ShapeType == esriGeometryType.esriGeometryLine || pFClass.ShapeType == esriGeometryType.esriGeometryPolyline)
                    {
                        for (int j = 0; j <= pUniquerend.ValueCount - 1; j++)
                        {
                            string xv;
                            xv = pUniquerend.get_Value(j);
                            if (xv != "")
                            {
                                //ISimpleLineSymbol pNextSymbol = new SimpleLineSymbolClass();
                                //pNextSymbol = pUniquerend.get_Symbol(xv) as ISimpleLineSymbol;
                                ILineSymbol pNextSymbol = pUniquerend.get_Symbol(xv) as ILineSymbol;
                                gridviewmu.Rows[j + 1].Cells[0].Style.BackColor = ClsGDBDataCommon.IColorToColor(pNextSymbol.Color);
                            }
                        }
                        gridviewmu.CurrentCell = null;
                    }
                    else if (pFClass.ShapeType == esriGeometryType.esriGeometryMultipoint || pFClass.ShapeType == esriGeometryType.esriGeometryPoint)
                    {
                        for (int j = 0; j <= pUniquerend.ValueCount - 1; j++)
                        {
                            string xv;
                            xv = pUniquerend.get_Value(j);
                            if (xv != "")
                            {
                                //ISimpleMarkerSymbol pNextSymbol = new SimpleMarkerSymbolClass();
                                //pNextSymbol = pUniquerend.get_Symbol(xv) as ISimpleMarkerSymbol;
                                IMarkerSymbol pNextSymbol = pUniquerend.get_Symbol(xv) as IMarkerSymbol;
                                gridviewmu.Rows[j + 1].Cells[0].Style.BackColor = ClsGDBDataCommon.IColorToColor(pNextSymbol.Color);
                            }
                        }
                        gridviewmu.CurrentCell = null;
                    }
                    else if (pFClass.ShapeType == esriGeometryType.esriGeometryPolygon)
                    {
                        for (int j = 0; j <= pUniquerend.ValueCount - 1; j++)
                        {
                            string xv;
                            xv = pUniquerend.get_Value(j);
                            if (xv != "")
                            {
                                //ISimpleFillSymbol pNextSymbol = new SimpleFillSymbolClass();
                                //pNextSymbol = pUniquerend.get_Symbol(xv) as ISimpleFillSymbol;
                                IFillSymbol pNextSymbol = pUniquerend.get_Symbol(xv) as IFillSymbol;
                                gridviewmu.Rows[j + 1].Cells[0].Style.BackColor = ClsGDBDataCommon.IColorToColor(pNextSymbol.Color);
                            }
                        }
                        gridviewmu.CurrentCell = null;
                    }
                    else
                    {

                    }
                    pUniquerendermu = pUniquerend;
                }
            } 
            
        }
       
        /// <summary>
        /// 初始化颜色条
        /// </summary>
        private void initcmbbox()
        {

            IStyleGalleryItem pStyleGalleryItem = new ServerStyleGalleryItem();
            pStyleGalleryItem.Item = pRandomRamp;
            stdole.IPictureDisp mPicture = pSymbolClass.PreviewItem(pStyleGalleryItem, cmbcolor.Width, cmbcolor.Height);
            Image mimage = Image.FromHbitmap(new System.IntPtr(mPicture.Handle));
            cmbcolor.Items.Add(mimage);
            cmbcolormu.Items.Add(mimage);
           

            for (int i = 53; i < 79; i++)
            {
                stdole.IPictureDisp pPicture = pSymbolClass.PreviewItem(pSymbolClass.GetItem(i), cmbcolor.Width, cmbcolor.Height);
                Image image = Image.FromHbitmap(new System.IntPtr(pPicture.Handle));
                cmbcolor.Items.Add(image);
                cmbcolormu.Items.Add(image);
            }
          
            if (cmbcolor.Items.Count > 0)
            {
                cmbcolor.SelectedIndex = 0;
                cmbcolormu.SelectedIndex = 0;
            }
        }

        /****重写combox的类，使得combox能够显示图片****/
        public partial class ComboboxSymbol : ComboBox
        {
            public ComboboxSymbol()
            {
                DrawMode = DrawMode.OwnerDrawFixed;
                DropDownStyle = ComboBoxStyle.DropDownList;
                Cursor = System.Windows.Forms.Cursors.Arrow;
            }

            protected override void OnDrawItem(DrawItemEventArgs e)
            {
                e.DrawBackground();
                e.DrawFocusRectangle();
                try
                {
                    //显示图片
                    Image image = (Image)Items[e.Index];
                    System.Drawing.Rectangle rect = e.Bounds;
                    e.Graphics.DrawImage(image, rect);
                }

                catch
                {
                }

                finally
                {
                    base.OnDrawItem(e);
                }
            }
        }

  
        /// <summary>
        /// 添加所有值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnaddall_Click(object sender, EventArgs e)
        {
            if (treeshow.SelectedIndex == 0)//独立值单波段
            {
                if (gridviewu.Rows.Count <=1)
                {
                    string fieldName = cmbfield.Text;

                    ISimpleLineSymbol pLineSymbol = new SimpleLineSymbolClass();
                    pLineSymbol.Style = esriSimpleLineStyle.esriSLSSolid;
                    pLineSymbol.Width = 1;

                    ISimpleFillSymbol pSimpleFillSymbol = new SimpleFillSymbolClass();
                    pSimpleFillSymbol.Style = esriSimpleFillStyle.esriSFSSolid;
                    pSimpleFillSymbol.Outline.Width = 0.4;

                    ISimpleMarkerSymbol pPointSymbol = new SimpleMarkerSymbolClass();
                    pPointSymbol.Style = esriSimpleMarkerStyle.esriSMSCircle;
                    pPointSymbol.Size = 6;

                    //pDisplayTable = pGeoFeatureLayer as IDisplayTable;
                    //线图层独立值渲染
                    if (pFClass.ShapeType == esriGeometryType.esriGeometryLine || pFClass.ShapeType == esriGeometryType.esriGeometryPolyline)
                    {
                        pUniqueRender.RemoveAllValues();
                        int ncount = 0;//记录要素总个数
                        //These properties should be set prior to adding values.
                        pUniqueRender.FieldCount = 1;
                        pUniqueRender.set_Field(0, fieldName);
                        pUniqueRender.DefaultSymbol = pLineSymbol as ISymbol;
                        pUniqueRender.UseDefaultSymbol = true;


                        IFeatureCursor pFeatureCursor = pDisplayTable.SearchDisplayTable(null, false) as IFeatureCursor;
                        IFeatureCursor pFeatureCursor1 = pDisplayTable.SearchDisplayTable(null, false) as IFeatureCursor;
                        IFeature pFeature = pFeatureCursor.NextFeature();
                        IFeature mFeature = pFeatureCursor1.NextFeature();

                        bool ValFound;

                        IFields pFields = pFeatureCursor.Fields;
                        fieldIndex = pFields.FindField(fieldName);
                        IField pField = pFields.get_Field(fieldIndex);
 
                        IsNumberic IsNum = new IsNumberic();
                        while (pFeature != null)
                        {
                            object classValue;
                            classValue = pFeature.get_Value(fieldIndex);

                            //Test to see if this value was added to the renderer. If not, add it.
                            ValFound = false;

                            //if (pField.Type == esriFieldType.esriFieldTypeDouble ||
                            //    pField.Type == esriFieldType.esriFieldTypeSingle ||
                            //    pField.Type == esriFieldType.esriFieldTypeInteger)
                            //{
                            //    for (int i = 0; i <= pUniqueRender.ValueCount - 1; i++)
                            //    {
                            //        if (Math.Abs(System.Convert.ToDouble(pUniqueRender.get_Value(i))-System.Convert.ToDouble((classValue))) < 0.00001)
                            //        {
                            //            ValFound = true;
                            //            break; //Exit the loop if the value was found.
                            //        }
                            //    }
                            //} 
                            //else
                            //{
                                for (int i = 0; i <= pUniqueRender.ValueCount - 1; i++)
                                {
                                    if (pUniqueRender.get_Value(i) == classValue.ToString() )
                                    {
                                        ValFound = true;
                                        break; //Exit the loop if the value was found.
                                    }
                                }
                            //}
                            //If the value was not found, it's new and will be added.
                            if (ValFound == false)
                            {
                                //if (pField.Type == esriFieldType.esriFieldTypeDouble ||
                                //pField.Type == esriFieldType.esriFieldTypeSingle ||
                                //pField.Type == esriFieldType.esriFieldTypeInteger)
                                //{
                                //    pUniqueRender.AddValue(Convert.ToDouble(classValue).ToString(), fieldName, pLineSymbol as ISymbol);
                                //}
                                //else
                                //{
                                    pUniqueRender.AddValue(classValue.ToString(), fieldName, pLineSymbol as ISymbol);
                                    pUniqueRender.set_Label(classValue.ToString(), classValue.ToString());
                                    pUniqueRender.set_Symbol(classValue.ToString(), pLineSymbol as ISymbol);
                                //}
                                
                            }
                            pFeature = pFeatureCursor.NextFeature();
                            ncount += 1;
                        }

                        long[] mcount = new long[pUniqueRender.ValueCount];//记录每一类别的要素数量
                        while (mFeature != null)
                        {
                            for (int k = 0; k < pUniqueRender.ValueCount; k++)
                            {
                                if (mFeature.get_Value(fieldIndex).ToString() == pUniqueRender.get_Value(k))
                                {
                                    mcount[k]++;
                                    break;
                                }
                            }
                            mFeature = pFeatureCursor1.NextFeature();
                        }

                        for (int g = 0; g < mcount.Length; g++)
                        {
                            countu.Add(mcount[g]);
                        }

                        DataRow headrow = dataTable2.NewRow();
                        headrow[1] = "<Heading>";
                        headrow[2] = fieldName;
                        headrow[3] = ncount;
                        dataTable2.Rows.Add(headrow);


                        for (int i = 0; i < pUniqueRender.ValueCount; i++)
                        {
                            DataRow pointrow = dataTable2.NewRow();
                            pointrow[1] = pUniqueRender.get_Value(i);
                            pointrow[2] = pointrow[1];
                            pointrow[3] = mcount[i];
                            dataTable2.Rows.Add(pointrow);
                        }

                        //Since the number of unique values is known, the color ramp can be sized and the colors assigned.
                        pRandomRamp.Size = pUniqueRender.ValueCount;
                        bool bOK;
                        pRandomRamp.CreateRamp(out bOK);
                        IEnumColors pEnumColors = pRandomRamp.Colors;
                        pEnumColors.Reset();

                        int n = pFClass.FeatureCount(null);

                        IFeatureCursor pFeatureCursor2 = pDisplayTable.SearchDisplayTable(null, false) as IFeatureCursor;
                        for (int i = 0; i < n; i++)
                        {
                            IFeature mpFeature = pFeatureCursor2.NextFeature();
                            IClone pSourceClone = pLineSymbol as IClone;
                            ISimpleLineSymbol pSimpleFillSymb = pSourceClone.Clone() as ISimpleLineSymbol;
                            string pFeatureValue = mpFeature.get_Value(mpFeature.Fields.FindField(fieldName)).ToString();
                            pUniqueRender.AddValue(pFeatureValue, "唯一值渲染", (ISymbol)pSimpleFillSymb);
                        }



                        for (int j = 0; j <= pUniqueRender.ValueCount - 1; j++)
                        {
                            string xv;
                            xv = pUniqueRender.get_Value(j);
                            if (xv != "")
                            {
                                ISimpleLineSymbol pNextSymbol = pUniqueRender.get_Symbol(xv) as ISimpleLineSymbol;
                                pNextSymbol.Color = pEnumColors.Next();
                                pUniqueRender.set_Symbol(xv, (ISymbol)pNextSymbol);
                                gridviewu.Rows[j + 1].Cells[0].Style.BackColor = ClsGDBDataCommon.IColorToColor(pNextSymbol.Color);
                            }
                        }
                        gridviewu.CurrentCell = null;
                    }

                    else if (pFClass.ShapeType == esriGeometryType.esriGeometryMultipoint || pFClass.ShapeType == esriGeometryType.esriGeometryPoint)
                    {
                        pUniqueRender.RemoveAllValues();
                        int ncount = 0;//记录要素总个数
                        //These properties should be set prior to adding values.
                        pUniqueRender.FieldCount = 1;
                        pUniqueRender.set_Field(0, fieldName);
                        pUniqueRender.DefaultSymbol = pPointSymbol as ISymbol;
                        pUniqueRender.UseDefaultSymbol = true;


                        IFeatureCursor pFeatureCursor = pDisplayTable.SearchDisplayTable(null, false) as IFeatureCursor;
                        IFeatureCursor pFeatureCursor1 = pDisplayTable.SearchDisplayTable(null, false) as IFeatureCursor;
                        IFeature pFeature = pFeatureCursor.NextFeature();
                        IFeature mFeature = pFeatureCursor1.NextFeature();

                        bool ValFound;

                        IFields pFields = pFeatureCursor.Fields;
                        fieldIndex = pFields.FindField(fieldName);
                        while (pFeature != null)
                        {
                            object classValue;
                            classValue = pFeature.get_Value(fieldIndex);

                            //Test to see if this value was added to the renderer. If not, add it.
                            ValFound = false;
                            for (int i = 0; i <= pUniqueRender.ValueCount - 1; i++)
                            {
                                if (pUniqueRender.get_Value(i) == classValue.ToString())
                                {
                                    ValFound = true;
                                    break; //Exit the loop if the value was found.
                                }
                            }
                            //If the value was not found, it's new and will be added.
                            if (ValFound == false)
                            {
                                pUniqueRender.AddValue(classValue.ToString(), fieldName, pPointSymbol as ISymbol);
                                pUniqueRender.set_Label(classValue.ToString(), classValue.ToString());
                                pUniqueRender.set_Symbol(classValue.ToString(), pPointSymbol as ISymbol);
                            }
                            pFeature = pFeatureCursor.NextFeature();
                            ncount += 1;
                        }

                        long[] mcount = new long[pUniqueRender.ValueCount];//记录每一类别的要素数量
                        while (mFeature != null)
                        {
                            for (int k = 0; k < pUniqueRender.ValueCount; k++)
                            {
                                if (mFeature.get_Value(fieldIndex).ToString() == pUniqueRender.get_Value(k))
                                {
                                    mcount[k]++;
                                    break;
                                }
                            }
                            mFeature = pFeatureCursor1.NextFeature();
                        }

                        for (int g = 0; g < mcount.Length; g++)
                        {
                            countu.Add(mcount[g]);
                        }

                        DataRow headrow = dataTable2.NewRow();
                        headrow[1] = "<Heading>";
                        headrow[2] = fieldName;
                        headrow[3] = ncount;
                        dataTable2.Rows.Add(headrow);


                        for (int i = 0; i < pUniqueRender.ValueCount; i++)
                        {
                            DataRow pointrow = dataTable2.NewRow();
                            pointrow[1] = pUniqueRender.get_Value(i);
                            pointrow[2] = pointrow[1];
                            pointrow[3] = mcount[i];
                            dataTable2.Rows.Add(pointrow);
                        }

                        //Since the number of unique values is known, the color ramp can be sized and the colors assigned.
                        pRandomRamp.Size = pUniqueRender.ValueCount;
                        bool bOK;
                        pRandomRamp.CreateRamp(out bOK);
                        IEnumColors pEnumColors = pRandomRamp.Colors;
                        pEnumColors.Reset();

                        int n = pFClass.FeatureCount(null);

                        IFeatureCursor pFeatureCursor2 = pDisplayTable.SearchDisplayTable(null, false) as IFeatureCursor;
                        for (int i = 0; i < n; i++)
                        {
                            IFeature mpFeature = pFeatureCursor2.NextFeature();
                            IClone pSourceClone = pPointSymbol as IClone;
                            ISimpleMarkerSymbol pSimpleFillSymb = pSourceClone.Clone() as ISimpleMarkerSymbol;
                            string pFeatureValue = mpFeature.get_Value(mpFeature.Fields.FindField(fieldName)).ToString();
                            pUniqueRender.AddValue(pFeatureValue, "唯一值渲染", (ISymbol)pSimpleFillSymb);
                        }



                        for (int j = 0; j <= pUniqueRender.ValueCount - 1; j++)
                        {
                            string xv;
                            xv = pUniqueRender.get_Value(j);
                            if (xv != "")
                            {
                                ISimpleMarkerSymbol pNextSymbol = pUniqueRender.get_Symbol(xv) as ISimpleMarkerSymbol;
                                pNextSymbol.Color = pEnumColors.Next();
                                pUniqueRender.set_Symbol(xv, (ISymbol)pNextSymbol);
                                gridviewu.Rows[j + 1].Cells[0].Style.BackColor = ClsGDBDataCommon.IColorToColor(pNextSymbol.Color);
                            }
                        }
                        gridviewu.CurrentCell = null;
                    }

                    else if (pFClass.ShapeType == esriGeometryType.esriGeometryPolygon)
                    {
                        pUniqueRender.RemoveAllValues();
                        int ncount = 0;//记录要素总个数
                        //These properties should be set prior to adding values.
                        pUniqueRender.FieldCount = 1;
                        pUniqueRender.set_Field(0, fieldName);
                        pUniqueRender.DefaultSymbol = pSimpleFillSymbol as ISymbol;
                        pUniqueRender.UseDefaultSymbol = true;


                        IFeatureCursor pFeatureCursor = pDisplayTable.SearchDisplayTable(null, false) as IFeatureCursor;
                        IFeatureCursor pFeatureCursor1 = pDisplayTable.SearchDisplayTable(null, false) as IFeatureCursor;
                        IFeature pFeature = pFeatureCursor.NextFeature();
                        IFeature mFeature = pFeatureCursor1.NextFeature();

                        bool ValFound;

                        IFields pFields = pFeatureCursor.Fields;
                        fieldIndex = pFields.FindField(fieldName);
                        while (pFeature != null)
                        {
                            object classValue;
                            classValue = pFeature.get_Value(fieldIndex);

                            //Test to see if this value was added to the renderer. If not, add it.
                            ValFound = false;
                            for (int i = 0; i <= pUniqueRender.ValueCount - 1; i++)
                            {
                                if (pUniqueRender.get_Value(i) == classValue.ToString())
                                {
                                    ValFound = true;
                                    break; //Exit the loop if the value was found.
                                }
                            }
                            //If the value was not found, it's new and will be added.
                            if (ValFound == false)
                            {
                                pUniqueRender.AddValue(classValue.ToString(), fieldName, pSimpleFillSymbol as ISymbol);
                                pUniqueRender.set_Label(classValue.ToString(), classValue.ToString());
                                pUniqueRender.set_Symbol(classValue.ToString(), pSimpleFillSymbol as ISymbol);
                            }
                            pFeature = pFeatureCursor.NextFeature();
                            ncount += 1;
                        }

                        long[] mcount = new long[pUniqueRender.ValueCount];//记录每一类别的要素数量
                        while (mFeature != null)
                        {
                            for (int k = 0; k < pUniqueRender.ValueCount; k++)
                            {
                                if (mFeature.get_Value(fieldIndex).ToString() == pUniqueRender.get_Value(k))
                                {
                                    mcount[k]++;
                                    break;
                                }
                            }
                            mFeature = pFeatureCursor1.NextFeature();
                        }

                        for (int g = 0; g < mcount.Length; g++)
                        {
                            countu.Add(mcount[g]);
                        }

                        DataRow headrow = dataTable2.NewRow();
                        headrow[1] = "<Heading>";
                        headrow[2] = fieldName;
                        headrow[3] = ncount;
                        dataTable2.Rows.Add(headrow);


                        for (int i = 0; i < pUniqueRender.ValueCount; i++)
                        {
                            DataRow pointrow = dataTable2.NewRow();
                            pointrow[1] = pUniqueRender.get_Value(i);
                            pointrow[2] = pointrow[1];
                            pointrow[3] = mcount[i];
                            dataTable2.Rows.Add(pointrow);
                        }

                        //Since the number of unique values is known, the color ramp can be sized and the colors assigned.
                        pRandomRamp.Size = pUniqueRender.ValueCount;
                        bool bOK;
                        pRandomRamp.CreateRamp(out bOK);
                        IEnumColors pEnumColors = pRandomRamp.Colors;
                        pEnumColors.Reset();

                        int n = pFClass.FeatureCount(null);

                        IFeatureCursor pFeatureCursor2 = pDisplayTable.SearchDisplayTable(null, false) as IFeatureCursor;
                        for (int i = 0; i < n; i++)
                        {
                            IFeature mpFeature = pFeatureCursor2.NextFeature();
                            IClone pSourceClone = pSimpleFillSymbol as IClone;
                            ISimpleFillSymbol pSimpleFillSymb = pSourceClone.Clone() as ISimpleFillSymbol;
                            string pFeatureValue = mpFeature.get_Value(mpFeature.Fields.FindField(fieldName)).ToString();
                            pUniqueRender.AddValue(pFeatureValue, "唯一值渲染", (ISymbol)pSimpleFillSymb);
                        }



                        for (int j = 0; j <= pUniqueRender.ValueCount - 1; j++)
                        {
                            string xv;
                            xv = pUniqueRender.get_Value(j);
                            if (xv != "")
                            {
                                ISimpleFillSymbol pNextSymbol = pUniqueRender.get_Symbol(xv) as ISimpleFillSymbol;
                                pNextSymbol.Color = pEnumColors.Next();
                                pUniqueRender.set_Symbol(xv, (ISymbol)pNextSymbol);
                                gridviewu.Rows[j + 1].Cells[0].Style.BackColor = ClsGDBDataCommon.IColorToColor(pNextSymbol.Color);
                            }
                        }
                        gridviewu.CurrentCell = null;
                    }
                    else
                    {
                        MessageBox.Show("该类型图层不能进行唯一值渲染", "提示", MessageBoxButtons.OK);
                    }


                } 
                
            }
            else
            {
                if (gridviewmu.Rows.Count<=1)
                {
                    ISimpleLineSymbol pLineSymbol = new SimpleLineSymbolClass();
                    pLineSymbol.Style = esriSimpleLineStyle.esriSLSSolid;
                    pLineSymbol.Width = 1;

                    ISimpleFillSymbol pSimpleFillSymbol = new SimpleFillSymbolClass();
                    pSimpleFillSymbol.Style = esriSimpleFillStyle.esriSFSSolid;
                    pSimpleFillSymbol.Outline.Width = 0.4;

                    ISimpleMarkerSymbol pPointSymbol = new SimpleMarkerSymbolClass();
                    pPointSymbol.Style = esriSimpleMarkerStyle.esriSMSCircle;
                    pPointSymbol.Size = 6;

                    pDisplayTable = pGeoFeatureLayer as IDisplayTable;

                    if (pFClass.ShapeType == esriGeometryType.esriGeometryLine || pFClass.ShapeType == esriGeometryType.esriGeometryPolyline)
                    {
                        if (cmbfield2.SelectedIndex == 0 && cmbfield3.SelectedIndex == 0)
                        {
                            MessageBox.Show("请选择两个或两个以上的字段进行多字段独立值渲染", "提示", MessageBoxButtons.OK);
                        }

                        else if (cmbfield2.SelectedIndex == 0 || cmbfield3.SelectedIndex == 0)
                        {
                            string field1Name = cmbfield1.Text;
                            string field2Name = cmbfield2.Text;
                            string field3Name = cmbfield3.Text;
                            string fieldname = "";

                            string fieldName2 = null;//用于保存第二个字段的值
                            if (cmbfield2.SelectedIndex == 0)
                            {
                                fieldName2 = cmbfield3.Text;
                            }
                            else
                            {
                                fieldName2 = cmbfield2.Text;
                            }


                            pUniquerendermu.RemoveAllValues();
                            int ncount = 0;//记录要素总个数
                            pUniquerendermu.FieldCount = 2;
                            pUniquerendermu.set_Field(0, field1Name);
                            pUniquerendermu.set_Field(1, fieldName2);
                            pUniquerendermu.FieldDelimiter = " | ";
                            pUniquerendermu.DefaultSymbol = pLineSymbol as ISymbol;
                            pUniquerendermu.UseDefaultSymbol = true;

                            fieldname = field1Name + pUniquerendermu.FieldDelimiter + fieldName2;

                            IFeatureCursor pFeatureCursor = pDisplayTable.SearchDisplayTable(null, false) as IFeatureCursor;
                            IFeatureCursor pFeatureCursor1 = pDisplayTable.SearchDisplayTable(null, false) as IFeatureCursor;
                            IFeature pFeature = pFeatureCursor.NextFeature();
                            IFeature mFeature = pFeatureCursor1.NextFeature();

                            bool ValFound;

                            IFields pFields = pFeatureCursor.Fields;
                            fieldIndex1 = pFields.FindField(field1Name);
                            fieldIndex2 = pFields.FindField(fieldName2);

                            while (pFeature != null)
                            {
                                object classValue;
                                object classvalue1;
                                object classvalue2;
                                classvalue1 = pFeature.get_Value(fieldIndex1);
                                classvalue2 = pFeature.get_Value(fieldIndex2);
                                classValue = classvalue1 + pUniquerendermu.FieldDelimiter + classvalue2;


                                //Test to see if this value was added to the renderer. If not, add it.
                                ValFound = false;
                                for (int i = 0; i <= pUniquerendermu.ValueCount - 1; i++)
                                {
                                    if (pUniquerendermu.get_Value(i) == classValue.ToString())
                                    {
                                        ValFound = true;
                                        break; //Exit the loop if the value was found.
                                    }
                                }
                                //If the value was not found, it's new and will be added.
                                if (ValFound == false)
                                {
                                    pUniquerendermu.AddValue(classValue.ToString(), fieldname, pLineSymbol as ISymbol);
                                    pUniquerendermu.set_Label(classValue.ToString(), classValue.ToString());
                                    pUniquerendermu.set_Symbol(classValue.ToString(), pLineSymbol as ISymbol);
                                }
                                pFeature = pFeatureCursor.NextFeature();
                                ncount += 1;
                            }

                            long[] mcount = new long[pUniquerendermu.ValueCount];//记录每一类别的要素数量
                            while (mFeature != null)
                            {
                                for (int k = 0; k < pUniquerendermu.ValueCount; k++)
                                {
                                    if (mFeature.get_Value(fieldIndex1).ToString() + pUniquerendermu.FieldDelimiter + mFeature.get_Value(fieldIndex2)
                                        == pUniquerendermu.get_Value(k))
                                    {
                                        mcount[k]++;
                                        break;
                                    }
                                }
                                mFeature = pFeatureCursor1.NextFeature();
                            }

                            for (int g = 0; g < mcount.Length; g++)
                            {
                                countumu.Add(mcount[g]);
                            }

                            DataRow headrow = dataTable3.NewRow();
                            headrow[1] = "<Heading>";
                            headrow[2] = fieldname;
                            headrow[3] = ncount;
                            dataTable3.Rows.Add(headrow);

                            for (int i = 0; i < pUniquerendermu.ValueCount; i++)
                            {
                                DataRow pointrow = dataTable3.NewRow();
                                pointrow[1] = pUniquerendermu.get_Value(i);
                                pointrow[2] = pointrow[1];
                                pointrow[3] = mcount[i];
                                dataTable3.Rows.Add(pointrow);
                            }

                            pRandomRamp.Size = pUniquerendermu.ValueCount;
                            bool bOK;
                            pRandomRamp.CreateRamp(out bOK);
                            IEnumColors pEnumColors = pRandomRamp.Colors;
                            pEnumColors.Reset();

                            int n = pFClass.FeatureCount(null);

                            IFeatureCursor pFeatureCursor2 = pDisplayTable.SearchDisplayTable(null, false) as IFeatureCursor;
                            for (int i = 0; i < n; i++)
                            {
                                IFeature mpFeature = pFeatureCursor2.NextFeature();
                                IClone pSourceClone = pLineSymbol as IClone;
                                ISimpleLineSymbol pSimpleFillSymb = pSourceClone.Clone() as ISimpleLineSymbol;
                                string pFeatureValue = mpFeature.get_Value(mpFeature.Fields.FindField(field1Name)).ToString() + pUniquerendermu.FieldDelimiter
                                                       + mpFeature.get_Value(mpFeature.Fields.FindField(fieldName2)).ToString();
                                pUniquerendermu.AddValue(pFeatureValue, "唯一值渲染", (ISymbol)pSimpleFillSymb);
                            }



                            for (int j = 0; j <= pUniquerendermu.ValueCount - 1; j++)
                            {
                                string xv;
                                xv = pUniquerendermu.get_Value(j);
                                if (xv != "")
                                {
                                    ISimpleLineSymbol pNextSymbol = pUniquerendermu.get_Symbol(xv) as ISimpleLineSymbol;
                                    pNextSymbol.Color = pEnumColors.Next();
                                    pUniquerendermu.set_Symbol(xv, (ISymbol)pNextSymbol);
                                    gridviewmu.Rows[j + 1].Cells[0].Style.BackColor = ClsGDBDataCommon.IColorToColor(pNextSymbol.Color);
                                }
                            }
                            gridviewmu.CurrentCell = null;
                        }

                        else
                        {
                            string field1Name = cmbfield1.Text;
                            string field2Name = cmbfield2.Text;
                            string field3Name = cmbfield3.Text;
                            string fieldname = "";

                            pUniquerendermu.RemoveAllValues();
                            int ncount = 0;//记录要素总个数
                            pUniquerendermu.FieldCount = 3;
                            pUniquerendermu.set_Field(0, field1Name);
                            pUniquerendermu.set_Field(1, field2Name);
                            pUniquerendermu.set_Field(2, field3Name);
                            pUniquerendermu.FieldDelimiter = " | ";
                            pUniquerendermu.DefaultSymbol = pPointSymbol as ISymbol;
                            pUniquerendermu.UseDefaultSymbol = true;

                            fieldname = field1Name + pUniquerendermu.FieldDelimiter + field2Name + pUniquerendermu.FieldDelimiter + field3Name;

                            IFeatureCursor pFeatureCursor = pDisplayTable.SearchDisplayTable(null, false) as IFeatureCursor;
                            IFeatureCursor pFeatureCursor1 = pDisplayTable.SearchDisplayTable(null, false) as IFeatureCursor;
                            IFeature pFeature = pFeatureCursor.NextFeature();
                            IFeature mFeature = pFeatureCursor1.NextFeature();

                            bool ValFound;

                            IFields pFields = pFeatureCursor.Fields;
                            fieldIndex1 = pFields.FindField(field1Name);
                            fieldIndex2 = pFields.FindField(field2Name);
                            fieldIndex3 = pFields.FindField(field3Name);

                            while (pFeature != null)
                            {
                                object classValue;
                                object classvalue1;
                                object classvalue2;
                                object classvalue3;
                                classvalue1 = pFeature.get_Value(fieldIndex1);
                                classvalue2 = pFeature.get_Value(fieldIndex2);
                                classvalue3 = pFeature.get_Value(fieldIndex3);
                                classValue = classvalue1 + pUniquerendermu.FieldDelimiter + classvalue2 + pUniquerendermu.FieldDelimiter + classvalue3;


                                //Test to see if this value was added to the renderer. If not, add it.
                                ValFound = false;
                                for (int i = 0; i <= pUniquerendermu.ValueCount - 1; i++)
                                {
                                    if (pUniquerendermu.get_Value(i) == classValue.ToString())
                                    {
                                        ValFound = true;
                                        break; //Exit the loop if the value was found.
                                    }
                                }
                                //If the value was not found, it's new and will be added.
                                if (ValFound == false)
                                {
                                    pUniquerendermu.AddValue(classValue.ToString(), fieldname, pPointSymbol as ISymbol);
                                    pUniquerendermu.set_Label(classValue.ToString(), classValue.ToString());
                                    pUniquerendermu.set_Symbol(classValue.ToString(), pPointSymbol as ISymbol);
                                }
                                pFeature = pFeatureCursor.NextFeature();
                                ncount += 1;
                            }

                            long[] mcount = new long[pUniquerendermu.ValueCount];//记录每一类别的要素数量
                            while (mFeature != null)
                            {
                                for (int k = 0; k < pUniquerendermu.ValueCount; k++)
                                {
                                    if (mFeature.get_Value(fieldIndex1).ToString() + pUniquerendermu.FieldDelimiter + mFeature.get_Value(fieldIndex2)
                                        + pUniquerendermu.FieldDelimiter + mFeature.get_Value(fieldIndex3) == pUniquerendermu.get_Value(k))
                                    {
                                        mcount[k]++;
                                        break;
                                    }
                                }
                                mFeature = pFeatureCursor1.NextFeature();
                            }

                            for (int g = 0; g < mcount.Length; g++)
                            {
                                countumu.Add(mcount[g]);
                            }

                            DataRow headrow = dataTable3.NewRow();
                            headrow[1] = "<Heading>";
                            headrow[2] = fieldname;
                            headrow[3] = ncount;
                            dataTable3.Rows.Add(headrow);

                            for (int i = 0; i < pUniquerendermu.ValueCount; i++)
                            {
                                DataRow pointrow = dataTable3.NewRow();
                                pointrow[1] = pUniquerendermu.get_Value(i);
                                pointrow[2] = pointrow[1];
                                pointrow[3] = mcount[i];
                                dataTable3.Rows.Add(pointrow);
                            }

                            pRandomRamp.Size = pUniquerendermu.ValueCount;
                            bool bOK;
                            pRandomRamp.CreateRamp(out bOK);
                            IEnumColors pEnumColors = pRandomRamp.Colors;
                            pEnumColors.Reset();

                            int n = pFClass.FeatureCount(null);

                            IFeatureCursor pFeatureCursor2 = pDisplayTable.SearchDisplayTable(null, false) as IFeatureCursor;
                            for (int i = 0; i < n; i++)
                            {
                                IFeature mpFeature = pFeatureCursor2.NextFeature();
                                IClone pSourceClone = pLineSymbol as IClone;
                                ISimpleLineSymbol pSimpleFillSymb = pSourceClone.Clone() as ISimpleLineSymbol;
                                string pFeatureValue = mpFeature.get_Value(mpFeature.Fields.FindField(field1Name)).ToString() + pUniquerendermu.FieldDelimiter
                                                       + mpFeature.get_Value(mpFeature.Fields.FindField(field2Name)).ToString() + pUniquerendermu.FieldDelimiter
                                                       + mpFeature.get_Value(mpFeature.Fields.FindField(field3Name)).ToString();
                                pUniquerendermu.AddValue(pFeatureValue, "唯一值渲染", (ISymbol)pSimpleFillSymb);
                            }



                            for (int j = 0; j <= pUniquerendermu.ValueCount - 1; j++)
                            {
                                string xv;
                                xv = pUniquerendermu.get_Value(j);
                                if (xv != "")
                                {
                                    ISimpleLineSymbol pNextSymbol = pUniquerendermu.get_Symbol(xv) as ISimpleLineSymbol;
                                    pNextSymbol.Color = pEnumColors.Next();
                                    pUniquerendermu.set_Symbol(xv, (ISymbol)pNextSymbol);
                                    gridviewmu.Rows[j + 1].Cells[0].Style.BackColor = ClsGDBDataCommon.IColorToColor(pNextSymbol.Color);
                                }
                            }
                            gridviewmu.CurrentCell = null;
                        }

                    }

                    else if (pFClass.ShapeType == esriGeometryType.esriGeometryMultipoint || pFClass.ShapeType == esriGeometryType.esriGeometryPoint)
                    {
                        if (cmbfield2.SelectedIndex == 0 && cmbfield3.SelectedIndex == 0)
                        {
                            MessageBox.Show("请选择两个或两个以上的字段进行多字段独立值渲染", "提示", MessageBoxButtons.OK);
                        }

                        else if (cmbfield2.SelectedIndex == 0 || cmbfield3.SelectedIndex == 0)
                        {
                            string field1Name = cmbfield1.Text;
                            string field2Name = cmbfield2.Text;
                            string field3Name = cmbfield3.Text;
                            string fieldname = "";

                            string fieldName2 = null;//用于保存第二个字段的值
                            if (cmbfield2.SelectedIndex == 0)
                            {
                                fieldName2 = cmbfield3.Text;
                            }
                            else
                            {
                                fieldName2 = cmbfield2.Text;
                            }


                            pUniquerendermu.RemoveAllValues();
                            int ncount = 0;//记录要素总个数
                            pUniquerendermu.FieldCount = 2;
                            pUniquerendermu.set_Field(0, field1Name);
                            pUniquerendermu.set_Field(1, fieldName2);
                            pUniquerendermu.FieldDelimiter = " | ";
                            pUniquerendermu.DefaultSymbol = pPointSymbol as ISymbol;
                            pUniquerendermu.UseDefaultSymbol = true;

                            fieldname = field1Name + pUniquerendermu.FieldDelimiter + fieldName2;

                            IFeatureCursor pFeatureCursor = pDisplayTable.SearchDisplayTable(null, false) as IFeatureCursor;
                            IFeatureCursor pFeatureCursor1 = pDisplayTable.SearchDisplayTable(null, false) as IFeatureCursor;
                            IFeature pFeature = pFeatureCursor.NextFeature();
                            IFeature mFeature = pFeatureCursor1.NextFeature();

                            bool ValFound;

                            IFields pFields = pFeatureCursor.Fields;
                            fieldIndex1 = pFields.FindField(field1Name);
                            fieldIndex2 = pFields.FindField(fieldName2);

                            while (pFeature != null)
                            {
                                object classValue;
                                object classvalue1;
                                object classvalue2;
                                classvalue1 = pFeature.get_Value(fieldIndex1);
                                classvalue2 = pFeature.get_Value(fieldIndex2);
                                classValue = classvalue1 + pUniquerendermu.FieldDelimiter + classvalue2;


                                //Test to see if this value was added to the renderer. If not, add it.
                                ValFound = false;
                                for (int i = 0; i <= pUniquerendermu.ValueCount - 1; i++)
                                {
                                    if (pUniquerendermu.get_Value(i) == classValue.ToString())
                                    {
                                        ValFound = true;
                                        break; //Exit the loop if the value was found.
                                    }
                                }
                                //If the value was not found, it's new and will be added.
                                if (ValFound == false)
                                {
                                    pUniquerendermu.AddValue(classValue.ToString(), fieldname, pPointSymbol as ISymbol);
                                    pUniquerendermu.set_Label(classValue.ToString(), classValue.ToString());
                                    pUniquerendermu.set_Symbol(classValue.ToString(), pPointSymbol as ISymbol);
                                }
                                pFeature = pFeatureCursor.NextFeature();
                                ncount += 1;
                            }

                            long[] mcount = new long[pUniquerendermu.ValueCount];//记录每一类别的要素数量
                            while (mFeature != null)
                            {
                                for (int k = 0; k < pUniquerendermu.ValueCount; k++)
                                {
                                    if (mFeature.get_Value(fieldIndex1).ToString() + pUniquerendermu.FieldDelimiter + mFeature.get_Value(fieldIndex2)
                                        == pUniquerendermu.get_Value(k))
                                    {
                                        mcount[k]++;
                                        break;
                                    }
                                }
                                mFeature = pFeatureCursor1.NextFeature();
                            }

                            for (int g = 0; g < mcount.Length; g++)
                            {
                                countumu.Add(mcount[g]);
                            }

                            DataRow headrow = dataTable3.NewRow();
                            headrow[1] = "<Heading>";
                            headrow[2] = fieldname;
                            headrow[3] = ncount;
                            dataTable3.Rows.Add(headrow);

                            for (int i = 0; i < pUniquerendermu.ValueCount; i++)
                            {
                                DataRow pointrow = dataTable3.NewRow();
                                pointrow[1] = pUniquerendermu.get_Value(i);
                                pointrow[2] = pointrow[1];
                                pointrow[3] = mcount[i];
                                dataTable3.Rows.Add(pointrow);
                            }

                            pRandomRamp.Size = pUniquerendermu.ValueCount;
                            bool bOK;
                            pRandomRamp.CreateRamp(out bOK);
                            IEnumColors pEnumColors = pRandomRamp.Colors;
                            pEnumColors.Reset();

                            int n = pFClass.FeatureCount(null);

                            IFeatureCursor pFeatureCursor2 = pDisplayTable.SearchDisplayTable(null, false) as IFeatureCursor;
                            for (int i = 0; i < n; i++)
                            {
                                IFeature mpFeature = pFeatureCursor2.NextFeature();
                                IClone pSourceClone = pPointSymbol as IClone;
                                ISimpleMarkerSymbol pSimpleFillSymb = pSourceClone.Clone() as ISimpleMarkerSymbol;
                                string pFeatureValue = mpFeature.get_Value(mpFeature.Fields.FindField(field1Name)).ToString() + pUniquerendermu.FieldDelimiter
                                                       + mpFeature.get_Value(mpFeature.Fields.FindField(fieldName2)).ToString();
                                pUniquerendermu.AddValue(pFeatureValue, "唯一值渲染", (ISymbol)pSimpleFillSymb);
                            }



                            for (int j = 0; j <= pUniquerendermu.ValueCount - 1; j++)
                            {
                                string xv;
                                xv = pUniquerendermu.get_Value(j);
                                if (xv != "")
                                {
                                    ISimpleMarkerSymbol pNextSymbol = pUniquerendermu.get_Symbol(xv) as ISimpleMarkerSymbol;
                                    pNextSymbol.Color = pEnumColors.Next();
                                    pUniquerendermu.set_Symbol(xv, (ISymbol)pNextSymbol);
                                    gridviewmu.Rows[j + 1].Cells[0].Style.BackColor = ClsGDBDataCommon.IColorToColor(pNextSymbol.Color);
                                }
                            }
                            gridviewmu.CurrentCell = null;
                        }

                        else
                        {
                            string field1Name = cmbfield1.Text;
                            string field2Name = cmbfield2.Text;
                            string field3Name = cmbfield3.Text;
                            string fieldname = "";

                            pUniquerendermu.RemoveAllValues();
                            int ncount = 0;//记录要素总个数
                            pUniquerendermu.FieldCount = 3;
                            pUniquerendermu.set_Field(0, field1Name);
                            pUniquerendermu.set_Field(1, field2Name);
                            pUniquerendermu.set_Field(2, field3Name);
                            pUniquerendermu.FieldDelimiter = " | ";
                            pUniquerendermu.DefaultSymbol = pPointSymbol as ISymbol;
                            pUniquerendermu.UseDefaultSymbol = true;

                            fieldname = field1Name + pUniquerendermu.FieldDelimiter + field2Name + pUniquerendermu.FieldDelimiter + field3Name;

                            IFeatureCursor pFeatureCursor = pDisplayTable.SearchDisplayTable(null, false) as IFeatureCursor;
                            IFeatureCursor pFeatureCursor1 = pDisplayTable.SearchDisplayTable(null, false) as IFeatureCursor;
                            IFeature pFeature = pFeatureCursor.NextFeature();
                            IFeature mFeature = pFeatureCursor1.NextFeature();

                            bool ValFound;

                            IFields pFields = pFeatureCursor.Fields;
                            fieldIndex1 = pFields.FindField(field1Name);
                            fieldIndex2 = pFields.FindField(field2Name);
                            fieldIndex3 = pFields.FindField(field3Name);

                            while (pFeature != null)
                            {
                                object classValue;
                                object classvalue1;
                                object classvalue2;
                                object classvalue3;
                                classvalue1 = pFeature.get_Value(fieldIndex1);
                                classvalue2 = pFeature.get_Value(fieldIndex2);
                                classvalue3 = pFeature.get_Value(fieldIndex3);
                                classValue = classvalue1 + pUniquerendermu.FieldDelimiter + classvalue2 + pUniquerendermu.FieldDelimiter + classvalue3;


                                //Test to see if this value was added to the renderer. If not, add it.
                                ValFound = false;
                                for (int i = 0; i <= pUniquerendermu.ValueCount - 1; i++)
                                {
                                    if (pUniquerendermu.get_Value(i) == classValue.ToString())
                                    {
                                        ValFound = true;
                                        break; //Exit the loop if the value was found.
                                    }
                                }
                                //If the value was not found, it's new and will be added.
                                if (ValFound == false)
                                {
                                    pUniquerendermu.AddValue(classValue.ToString(), fieldname, pPointSymbol as ISymbol);
                                    pUniquerendermu.set_Label(classValue.ToString(), classValue.ToString());
                                    pUniquerendermu.set_Symbol(classValue.ToString(), pPointSymbol as ISymbol);
                                }
                                pFeature = pFeatureCursor.NextFeature();
                                ncount += 1;
                            }

                            long[] mcount = new long[pUniquerendermu.ValueCount];//记录每一类别的要素数量
                            while (mFeature != null)
                            {
                                for (int k = 0; k < pUniquerendermu.ValueCount; k++)
                                {
                                    if (mFeature.get_Value(fieldIndex1).ToString() + pUniquerendermu.FieldDelimiter + mFeature.get_Value(fieldIndex2)
                                        + pUniquerendermu.FieldDelimiter + mFeature.get_Value(fieldIndex3) == pUniquerendermu.get_Value(k))
                                    {
                                        mcount[k]++;
                                        break;
                                    }
                                }
                                mFeature = pFeatureCursor1.NextFeature();
                            }

                            for (int g = 0; g < mcount.Length; g++)
                            {
                                countumu.Add(mcount[g]);
                            }

                            DataRow headrow = dataTable3.NewRow();
                            headrow[1] = "<Heading>";
                            headrow[2] = fieldname;
                            headrow[3] = ncount;
                            dataTable3.Rows.Add(headrow);

                            for (int i = 0; i < pUniquerendermu.ValueCount; i++)
                            {
                                DataRow pointrow = dataTable3.NewRow();
                                pointrow[1] = pUniquerendermu.get_Value(i);
                                pointrow[2] = pointrow[1];
                                pointrow[3] = mcount[i];
                                dataTable3.Rows.Add(pointrow);
                            }

                            pRandomRamp.Size = pUniquerendermu.ValueCount;
                            bool bOK;
                            pRandomRamp.CreateRamp(out bOK);
                            IEnumColors pEnumColors = pRandomRamp.Colors;
                            pEnumColors.Reset();

                            int n = pFClass.FeatureCount(null);

                            IFeatureCursor pFeatureCursor2 = pDisplayTable.SearchDisplayTable(null, false) as IFeatureCursor;
                            for (int i = 0; i < n; i++)
                            {
                                IFeature mpFeature = pFeatureCursor2.NextFeature();
                                IClone pSourceClone = pPointSymbol as IClone;
                                ISimpleMarkerSymbol pSimpleFillSymb = pSourceClone.Clone() as ISimpleMarkerSymbol;
                                string pFeatureValue = mpFeature.get_Value(mpFeature.Fields.FindField(field1Name)).ToString() + pUniquerendermu.FieldDelimiter
                                                       + mpFeature.get_Value(mpFeature.Fields.FindField(field2Name)).ToString() + pUniquerendermu.FieldDelimiter
                                                       + mpFeature.get_Value(mpFeature.Fields.FindField(field3Name)).ToString();
                                pUniquerendermu.AddValue(pFeatureValue, "唯一值渲染", (ISymbol)pSimpleFillSymb);
                            }



                            for (int j = 0; j <= pUniquerendermu.ValueCount - 1; j++)
                            {
                                string xv;
                                xv = pUniquerendermu.get_Value(j);
                                if (xv != "")
                                {
                                    ISimpleMarkerSymbol pNextSymbol = pUniquerendermu.get_Symbol(xv) as ISimpleMarkerSymbol;
                                    pNextSymbol.Color = pEnumColors.Next();
                                    pUniquerendermu.set_Symbol(xv, (ISymbol)pNextSymbol);
                                    gridviewmu.Rows[j + 1].Cells[0].Style.BackColor = ClsGDBDataCommon.IColorToColor(pNextSymbol.Color);
                                }
                            }
                            gridviewmu.CurrentCell = null;
                        }
                    }

                    else if (pFClass.ShapeType == esriGeometryType.esriGeometryPolygon)
                    {
                        if (cmbfield2.SelectedIndex == 0 && cmbfield3.SelectedIndex == 0)
                        {
                            MessageBox.Show("请选择两个或两个以上的字段进行多字段独立值渲染", "提示", MessageBoxButtons.OK);
                        }

                        else if (cmbfield2.SelectedIndex == 0 || cmbfield3.SelectedIndex == 0)
                        {
                            string field1Name = cmbfield1.Text;
                            string field2Name = cmbfield2.Text;
                            string field3Name = cmbfield3.Text;
                            string fieldname = "";

                            string fieldName2 = null;//用于保存第二个字段的值
                            if (cmbfield2.SelectedIndex == 0)
                            {
                                fieldName2 = cmbfield3.Text;
                            }
                            else
                            {
                                fieldName2 = cmbfield2.Text;
                            }


                            pUniquerendermu.RemoveAllValues();
                            int ncount = 0;//记录要素总个数
                            pUniquerendermu.FieldCount = 2;
                            pUniquerendermu.set_Field(0, field1Name);
                            pUniquerendermu.set_Field(1, fieldName2);
                            pUniquerendermu.FieldDelimiter = " | ";
                            pUniquerendermu.DefaultSymbol = pSimpleFillSymbol as ISymbol;
                            pUniquerendermu.UseDefaultSymbol = true;

                            fieldname = field1Name + pUniquerendermu.FieldDelimiter + fieldName2;

                            IFeatureCursor pFeatureCursor = pDisplayTable.SearchDisplayTable(null, false) as IFeatureCursor;
                            IFeatureCursor pFeatureCursor1 = pDisplayTable.SearchDisplayTable(null, false) as IFeatureCursor;
                            IFeature pFeature = pFeatureCursor.NextFeature();
                            IFeature mFeature = pFeatureCursor1.NextFeature();

                            bool ValFound;

                            IFields pFields = pFeatureCursor.Fields;
                            fieldIndex1 = pFields.FindField(field1Name);
                            fieldIndex2 = pFields.FindField(fieldName2);

                            while (pFeature != null)
                            {
                                object classValue;
                                object classvalue1;
                                object classvalue2;
                                classvalue1 = pFeature.get_Value(fieldIndex1);
                                classvalue2 = pFeature.get_Value(fieldIndex2);
                                classValue = classvalue1 + pUniquerendermu.FieldDelimiter + classvalue2;


                                //Test to see if this value was added to the renderer. If not, add it.
                                ValFound = false;
                                for (int i = 0; i <= pUniquerendermu.ValueCount - 1; i++)
                                {
                                    if (pUniquerendermu.get_Value(i) == classValue.ToString())
                                    {
                                        ValFound = true;
                                        break; //Exit the loop if the value was found.
                                    }
                                }
                                //If the value was not found, it's new and will be added.
                                if (ValFound == false)
                                {
                                    pUniquerendermu.AddValue(classValue.ToString(), fieldname, pSimpleFillSymbol as ISymbol);
                                    pUniquerendermu.set_Label(classValue.ToString(), classValue.ToString());
                                    pUniquerendermu.set_Symbol(classValue.ToString(), pSimpleFillSymbol as ISymbol);
                                }
                                pFeature = pFeatureCursor.NextFeature();
                                ncount += 1;
                            }

                            long[] mcount = new long[pUniquerendermu.ValueCount];//记录每一类别的要素数量
                            while (mFeature != null)
                            {
                                for (int k = 0; k < pUniquerendermu.ValueCount; k++)
                                {
                                    if (mFeature.get_Value(fieldIndex1).ToString() + pUniquerendermu.FieldDelimiter + mFeature.get_Value(fieldIndex2)
                                        == pUniquerendermu.get_Value(k))
                                    {
                                        mcount[k]++;
                                        break;
                                    }
                                }
                                mFeature = pFeatureCursor1.NextFeature();
                            }

                            for (int g = 0; g < mcount.Length; g++)
                            {
                                countumu.Add(mcount[g]);
                            }

                            DataRow headrow = dataTable3.NewRow();
                            headrow[1] = "<Heading>";
                            headrow[2] = fieldname;
                            headrow[3] = ncount;
                            dataTable3.Rows.Add(headrow);

                            for (int i = 0; i < pUniquerendermu.ValueCount; i++)
                            {
                                DataRow pointrow = dataTable3.NewRow();
                                pointrow[1] = pUniquerendermu.get_Value(i);
                                pointrow[2] = pointrow[1];
                                pointrow[3] = mcount[i];
                                dataTable3.Rows.Add(pointrow);
                            }

                            pRandomRamp.Size = pUniquerendermu.ValueCount;
                            bool bOK;
                            pRandomRamp.CreateRamp(out bOK);
                            IEnumColors pEnumColors = pRandomRamp.Colors;
                            pEnumColors.Reset();

                            int n = pFClass.FeatureCount(null);

                            IFeatureCursor pFeatureCursor2 = pDisplayTable.SearchDisplayTable(null, false) as IFeatureCursor;
                            for (int i = 0; i < n; i++)
                            {
                                IFeature mpFeature = pFeatureCursor2.NextFeature();
                                IClone pSourceClone = pSimpleFillSymbol as IClone;
                                ISimpleFillSymbol pSimpleFillSymb = pSourceClone.Clone() as ISimpleFillSymbol;
                                string pFeatureValue = mpFeature.get_Value(mpFeature.Fields.FindField(field1Name)).ToString() + pUniquerendermu.FieldDelimiter
                                                       + mpFeature.get_Value(mpFeature.Fields.FindField(fieldName2)).ToString();
                                pUniquerendermu.AddValue(pFeatureValue, "唯一值渲染", (ISymbol)pSimpleFillSymb);
                            }



                            for (int j = 0; j <= pUniquerendermu.ValueCount - 1; j++)
                            {
                                string xv;
                                xv = pUniquerendermu.get_Value(j);
                                if (xv != "")
                                {
                                    ISimpleFillSymbol pNextSymbol = pUniquerendermu.get_Symbol(xv) as ISimpleFillSymbol;
                                    pNextSymbol.Color = pEnumColors.Next();
                                    pUniquerendermu.set_Symbol(xv, (ISymbol)pNextSymbol);
                                    gridviewmu.Rows[j + 1].Cells[0].Style.BackColor = ClsGDBDataCommon.IColorToColor(pNextSymbol.Color);
                                }
                            }
                            gridviewmu.CurrentCell = null;
                        }

                        else
                        {
                            string field1Name = cmbfield1.Text;
                            string field2Name = cmbfield2.Text;
                            string field3Name = cmbfield3.Text;
                            string fieldname = "";

                            pUniquerendermu.RemoveAllValues();
                            int ncount = 0;//记录要素总个数
                            pUniquerendermu.FieldCount = 3;
                            pUniquerendermu.set_Field(0, field1Name);
                            pUniquerendermu.set_Field(1, field2Name);
                            pUniquerendermu.set_Field(2, field3Name);
                            pUniquerendermu.FieldDelimiter = " | ";
                            pUniquerendermu.DefaultSymbol = pSimpleFillSymbol as ISymbol;
                            pUniquerendermu.UseDefaultSymbol = true;

                            fieldname = field1Name + pUniquerendermu.FieldDelimiter + field2Name + pUniquerendermu.FieldDelimiter + field3Name;

                            IFeatureCursor pFeatureCursor = pDisplayTable.SearchDisplayTable(null, false) as IFeatureCursor;
                            IFeatureCursor pFeatureCursor1 = pDisplayTable.SearchDisplayTable(null, false) as IFeatureCursor;
                            IFeature pFeature = pFeatureCursor.NextFeature();
                            IFeature mFeature = pFeatureCursor1.NextFeature();

                            bool ValFound;

                            IFields pFields = pFeatureCursor.Fields;
                            fieldIndex1 = pFields.FindField(field1Name);
                            fieldIndex2 = pFields.FindField(field2Name);
                            fieldIndex3 = pFields.FindField(field3Name);

                            while (pFeature != null)
                            {
                                object classValue;
                                object classvalue1;
                                object classvalue2;
                                object classvalue3;
                                classvalue1 = pFeature.get_Value(fieldIndex1);
                                classvalue2 = pFeature.get_Value(fieldIndex2);
                                classvalue3 = pFeature.get_Value(fieldIndex3);
                                classValue = classvalue1 + pUniquerendermu.FieldDelimiter + classvalue2 + pUniquerendermu.FieldDelimiter + classvalue3;


                                //Test to see if this value was added to the renderer. If not, add it.
                                ValFound = false;
                                for (int i = 0; i <= pUniquerendermu.ValueCount - 1; i++)
                                {
                                    if (pUniquerendermu.get_Value(i) == classValue.ToString())
                                    {
                                        ValFound = true;
                                        break; //Exit the loop if the value was found.
                                    }
                                }
                                //If the value was not found, it's new and will be added.
                                if (ValFound == false)
                                {
                                    pUniquerendermu.AddValue(classValue.ToString(), fieldname, pSimpleFillSymbol as ISymbol);
                                    pUniquerendermu.set_Label(classValue.ToString(), classValue.ToString());
                                    pUniquerendermu.set_Symbol(classValue.ToString(), pSimpleFillSymbol as ISymbol);
                                }
                                pFeature = pFeatureCursor.NextFeature();
                                ncount += 1;
                            }

                            long[] mcount = new long[pUniquerendermu.ValueCount];//记录每一类别的要素数量
                            while (mFeature != null)
                            {
                                for (int k = 0; k < pUniquerendermu.ValueCount; k++)
                                {
                                    if (mFeature.get_Value(fieldIndex1).ToString() + pUniquerendermu.FieldDelimiter + mFeature.get_Value(fieldIndex2)
                                        + pUniquerendermu.FieldDelimiter + mFeature.get_Value(fieldIndex3) == pUniquerendermu.get_Value(k))
                                    {
                                        mcount[k]++;
                                        break;
                                    }
                                }
                                mFeature = pFeatureCursor1.NextFeature();
                            }

                            for (int g = 0; g < mcount.Length; g++)
                            {
                                countumu.Add(mcount[g]);
                            }

                            DataRow headrow = dataTable3.NewRow();
                            headrow[1] = "<Heading>";
                            headrow[2] = fieldname;
                            headrow[3] = ncount;
                            dataTable3.Rows.Add(headrow);

                            for (int i = 0; i < pUniquerendermu.ValueCount; i++)
                            {
                                DataRow pointrow = dataTable3.NewRow();
                                pointrow[1] = pUniquerendermu.get_Value(i);
                                pointrow[2] = pointrow[1];
                                pointrow[3] = mcount[i];
                                dataTable3.Rows.Add(pointrow);
                            }

                            pRandomRamp.Size = pUniquerendermu.ValueCount;
                            bool bOK;
                            pRandomRamp.CreateRamp(out bOK);
                            IEnumColors pEnumColors = pRandomRamp.Colors;
                            pEnumColors.Reset();

                            int n = pFClass.FeatureCount(null);

                            IFeatureCursor pFeatureCursor2 = pDisplayTable.SearchDisplayTable(null, false) as IFeatureCursor;
                            for (int i = 0; i < n; i++)
                            {
                                IFeature mpFeature = pFeatureCursor2.NextFeature();
                                IClone pSourceClone = pSimpleFillSymbol as IClone;
                                ISimpleFillSymbol pSimpleFillSymb = pSourceClone.Clone() as ISimpleFillSymbol;
                                string pFeatureValue = mpFeature.get_Value(mpFeature.Fields.FindField(field1Name)).ToString() + pUniquerendermu.FieldDelimiter
                                                       + mpFeature.get_Value(mpFeature.Fields.FindField(field2Name)).ToString() + pUniquerendermu.FieldDelimiter
                                                       + mpFeature.get_Value(mpFeature.Fields.FindField(field3Name)).ToString();
                                pUniquerendermu.AddValue(pFeatureValue, "唯一值渲染", (ISymbol)pSimpleFillSymb);
                            }



                            for (int j = 0; j <= pUniquerendermu.ValueCount - 1; j++)
                            {
                                string xv;
                                xv = pUniquerendermu.get_Value(j);
                                if (xv != "")
                                {
                                    ISimpleFillSymbol pNextSymbol = pUniquerendermu.get_Symbol(xv) as ISimpleFillSymbol;
                                    pNextSymbol.Color = pEnumColors.Next();
                                    pUniquerendermu.set_Symbol(xv, (ISymbol)pNextSymbol);
                                    gridviewmu.Rows[j + 1].Cells[0].Style.BackColor = ClsGDBDataCommon.IColorToColor(pNextSymbol.Color);
                                }
                            }
                            gridviewmu.CurrentCell = null;
                        }
                    }

                    else
                    {
                        MessageBox.Show("该类型图层不能进行唯一值渲染", "提示", MessageBoxButtons.OK);
                    }
                }
                
            }
        }

        /// <summary>
        /// 移除所有值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
         private void btnremoveall_Click(object sender, EventArgs e)
        {
            if (treeshow.SelectedIndex==0)
            {
                pUniqueRender.RemoveAllValues();
                for (int ndr = dataTable2.Rows.Count - 1; ndr >= 0; ndr--)
                {
                    DataRow dr = dataTable2.Rows[ndr];
                    dataTable2.Rows.Remove(dr);
                }
                countu.Clear();
            } 
            else
            {
                pUniquerendermu.RemoveAllValues();
                for (int ndr = dataTable3.Rows.Count - 1; ndr >= 0; ndr--)
                {
                    DataRow dr = dataTable3.Rows[ndr];
                    dataTable3.Rows.Remove(dr);
                }
                countumu.Clear();
            }
           
        }


        /*********************************
         * 移除值
         * *******************************/
         private void btnremove_Click(object sender, EventArgs e)
         {
             if (treeshow.SelectedIndex==0)
             {
                 if (gridviewu.Rows.Count > 1)
                 {
                     if (rowindex != 0 || rowindex != gridviewmu.Rows.Count - 1)
                     {
                         string rvx = pUniqueRender.get_Value(rowindex - 1);
                         IColor clu = null;
                         rmvx.Add(rvx);
                         clu = ClsGDBDataCommon.ColorToIColor(gridviewu.Rows[rowindex].Cells[0].Style.BackColor);
                         coloru.Add(clu);
                         cuntu.Add(countu[rowindex - 1]);
                         countu.Remove(countu[rowindex - 1]);
                         dataTable2.Rows[rowindex].Delete();
                         pUniqueRender.RemoveValue(rvx);
                         gridviewu.CurrentCell = null;
                     }
                 }                
             }
             else
             {
                 if (gridviewmu.Rows.Count > 1)
                 {
                     if (rowindex != 0 || rowindex != gridviewmu.Rows.Count - 1)
                     {
                         string rvxmu = pUniquerendermu.get_Value(rowindexmu - 1);
                         IColor clumu = null;
                         revxmu.Add(rvxmu);
                         clumu = ClsGDBDataCommon.ColorToIColor(gridviewmu.Rows[rowindexmu].Cells[0].Style.BackColor);
                         colorumu.Add(clumu);
                         cuntumu.Add(countumu[rowindexmu - 1]);
                         countumu.Remove(countumu[rowindexmu - 1]);
                         dataTable3.Rows[rowindexmu].Delete();
                         pUniquerendermu.RemoveValue(rvxmu);
                         gridviewmu.CurrentCell = null;
                     }
                 }
             }
         }

         /***********************************
          * 添加值
          ***********************************/
         private void btnadd_Click(object sender, EventArgs e)
         {
             FrmAddUValue frmaddvalue = new FrmAddUValue(pUniqueRender,pUniquerendermu,pFClass,coloru,colorumu,gridviewu,gridviewmu
                                                         ,treeshow,dataTable2,dataTable3,rmvx,revxmu,cuntu,cuntumu,countu,countumu);
             frmaddvalue.ShowDialog();
             frmaddvalue.StartPosition = FormStartPosition.CenterScreen;
             frmaddvalue.ShowInTaskbar = false;
         }

#region  选择的渲染字段发生变化

         private void cmbfield_SelectedIndexChanged(object sender, EventArgs e)
         {
             pUniqueRender.RemoveAllValues();
             for (int ndr = dataTable2.Rows.Count - 1; ndr >= 0; ndr--)
             {
                 DataRow dr = dataTable2.Rows[ndr];
                 dataTable2.Rows.Remove(dr);
             }
             countu.Clear();
         }

         private void cmbfield1_SelectedIndexChanged(object sender, EventArgs e)
         {
             pUniquerendermu.RemoveAllValues();
             for (int ndr = dataTable3.Rows.Count - 1; ndr >= 0; ndr--)
             {
                 DataRow dr = dataTable3.Rows[ndr];
                 dataTable3.Rows.Remove(dr);
             }
             countumu.Clear();
         }

         private void cmbfield2_SelectedIndexChanged(object sender, EventArgs e)
         {
             pUniquerendermu.RemoveAllValues();
             for (int ndr = dataTable3.Rows.Count - 1; ndr >= 0; ndr--)
             {
                 DataRow dr = dataTable3.Rows[ndr];
                 dataTable3.Rows.Remove(dr);
             }
             countumu.Clear();
         }

         private void cmbfield3_SelectedIndexChanged(object sender, EventArgs e)
         {
             pUniquerendermu.RemoveAllValues();
             for (int ndr = dataTable3.Rows.Count - 1; ndr >= 0; ndr--)
             {
                 DataRow dr = dataTable3.Rows[ndr];
                 dataTable3.Rows.Remove(dr);
             }
             countumu.Clear();
         }

#endregion

#region 单击某一类别，进行删除添加操作

         private void gridviewu_CellClick(object sender, DataGridViewCellEventArgs e)
         {
             if (e.ColumnIndex==0)
             {
                 return;
             } 
             else
             {
                 if (e.RowIndex != 0 || e.RowIndex != gridviewu.Rows.Count - 1)
                 {
                     rowindex = e.RowIndex;
                     btnremove.Enabled = true;
                     btnadd.Enabled = true;
                 }
             }
         }

         private void gridviewmu_CellClick(object sender, DataGridViewCellEventArgs e)
         {
             if (e.ColumnIndex == 0)
             {
                 return;
             }
             else
             {
                 if (e.RowIndex != 0 || e.RowIndex != gridviewmu.Rows.Count - 1)
                 {
                     rowindexmu = e.RowIndex;
                     btnremove.Enabled = true;
                     btnadd.Enabled = true;
                 }
             }
         }

#endregion


#region 双击datagridview颜色列，改变某一类渲染颜色

         private void gridviewmu_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
         {
             if (e.ColumnIndex == 0 && e.RowIndex != 0 && e.RowIndex != gridviewmu.Rows.Count - 1)
             {
                 ColorDialog colordig = new ColorDialog();
                 if (colordig.ShowDialog() == DialogResult.OK)
                 {
                     gridviewmu.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = colordig.Color;
                     gridviewmu.CurrentCell = null;

                     string xv;
                     xv = pUniquerendermu.get_Value(e.RowIndex - 1);
                     if (xv != "")
                     {
                         if (pFClass.ShapeType == esriGeometryType.esriGeometryPolygon)
                         {
                             //ISimpleFillSymbol pNextSymbol = pUniquerendermu.get_Symbol(xv) as ISimpleFillSymbol;
                             //pNextSymbol.Color = ClsGDBDataCommon.ColorToIColor(gridviewmu.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor);
                             IFillSymbol pNextSymbol = pUniquerendermu.get_Symbol(xv) as IFillSymbol;
                             pNextSymbol.Color = ClsGDBDataCommon.ColorToIColor(gridviewmu.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor);
                             pUniquerendermu.set_Symbol(xv, (ISymbol)pNextSymbol);
                         }
                         else if (pFClass.ShapeType == esriGeometryType.esriGeometryMultipoint || pFClass.ShapeType == esriGeometryType.esriGeometryPoint)
                         {
                             //ISimpleMarkerSymbol pNextSymbol = pUniquerendermu.get_Symbol(xv) as ISimpleMarkerSymbol;
                             //pNextSymbol.Color = ClsGDBDataCommon.ColorToIColor(gridviewmu.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor);
                             IMarkerSymbol pNextSymbol = pUniquerendermu.get_Symbol(xv) as IMarkerSymbol;
                             pNextSymbol.Color = ClsGDBDataCommon.ColorToIColor(gridviewmu.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor);
                             pUniquerendermu.set_Symbol(xv, (ISymbol)pNextSymbol);
                         }
                         else
                         {
                             //ISimpleLineSymbol pNextSymbol = pUniquerendermu.get_Symbol(xv) as ISimpleLineSymbol;
                             //pNextSymbol.Color = ClsGDBDataCommon.ColorToIColor(gridviewmu.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor);
                             ILineSymbol pNextSymbol = pUniquerendermu.get_Symbol(xv) as ILineSymbol;
                             pNextSymbol.Color = ClsGDBDataCommon.ColorToIColor(gridviewmu.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor);
                             pUniquerendermu.set_Symbol(xv, (ISymbol)pNextSymbol);
                         }
                     }                  
                 }
             }
         }

         private void gridviewu_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
         {
             if (e.ColumnIndex == 0 && e.RowIndex != 0 && e.RowIndex != gridviewu.Rows.Count - 1)
             {
                 ColorDialog colordig = new ColorDialog();
                 if (colordig.ShowDialog() == DialogResult.OK)
                 {
                     gridviewu.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = colordig.Color;
                     gridviewu.CurrentCell = null;

                     string xv;
                     xv = pUniqueRender.get_Value(e.RowIndex - 1);
                     if (xv != "")
                     {
                         if (pFClass.ShapeType == esriGeometryType.esriGeometryPolygon)
                         {
                             //ISimpleFillSymbol pNextSymbol = pUniqueRender.get_Symbol(xv) as ISimpleFillSymbol;
                             //pNextSymbol.Color = ClsGDBDataCommon.ColorToIColor(gridviewu.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor);
                             IFillSymbol pNextSymbol = pUniqueRender.get_Symbol(xv) as IFillSymbol;
                             pNextSymbol.Color = ClsGDBDataCommon.ColorToIColor(gridviewu.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor);
                             pUniqueRender.set_Symbol(xv, (ISymbol)pNextSymbol);
                         }
                         else if (pFClass.ShapeType == esriGeometryType.esriGeometryMultipoint || pFClass.ShapeType == esriGeometryType.esriGeometryPoint)
                         {
                             //ISimpleMarkerSymbol pNextSymbol = pUniqueRender.get_Symbol(xv) as ISimpleMarkerSymbol;
                             IMarkerSymbol pNextSymbol = pUniqueRender.get_Symbol(xv) as IMarkerSymbol;
                             pNextSymbol.Color = ClsGDBDataCommon.ColorToIColor(gridviewu.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor);
                             pUniqueRender.set_Symbol(xv, (ISymbol)pNextSymbol);
                         }
                         else
                         {
                             //ISimpleLineSymbol pNextSymbol = pUniqueRender.get_Symbol(xv) as ISimpleLineSymbol;
                             ILineSymbol pNextSymbol = pUniqueRender.get_Symbol(xv) as ILineSymbol;
                             pNextSymbol.Color = ClsGDBDataCommon.ColorToIColor(gridviewu.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor);
                             pUniqueRender.set_Symbol(xv, (ISymbol)pNextSymbol);
                         }
                     }
                 }
             }
         }

#endregion

         private void btnok_Click(object sender, EventArgs e)
         {
             if (treeshow.SelectedIndex == 0)//独立值单波段
             {
                 if (gridviewu.Rows.Count > 1)//已设置渲染值
                 {
                     pUniqueRender.DefaultLabel = null;
                     pUniqueRender.UseDefaultSymbol = false;
                     pUniqueRender.ColorScheme = "Custom";

                     ITable pTable = pDisplayTable as ITable;
                     //bool isString = pTable.Fields.get_Field(fieldIndex).Type == esriFieldType.esriFieldTypeString;
                     pUniqueRender.set_FieldType(0, true);

                     pGeoFeatureLayer.Renderer = pUniqueRender as IFeatureRenderer;
                     IUID pUID = new UIDClass();
                     pUID.Value = "{683C994E-A17B-11D1-8816-080009EC732A}";
                     pGeoFeatureLayer.RendererPropertyPageClassID = pUID as UIDClass;

                     //pTocControl.SetBuddyControl(pMapControl);
                     //pMapControl.Refresh();
                     //pTocControl.Refresh();
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
                 else//如果没有设置渲染值，则恢复原来统一渲染
                 {
                     pUniqueRender.RemoveAllValues();
                     if (pFClass.ShapeType == esriGeometryType.esriGeometryLine || pFClass.ShapeType == esriGeometryType.esriGeometryPolyline)
                     {
                         SimpleLineSymbol pNextSymbol = new SimpleLineSymbolClass();
                         pNextSymbol.Color = ClsGDBDataCommon.ColorToIColor(Color.Green);
                         pRenderer.Symbol = pNextSymbol as ISymbol;
                     }
                     else if (pFClass.ShapeType == esriGeometryType.esriGeometryMultipoint || pFClass.ShapeType == esriGeometryType.esriGeometryPoint)
                     {
                         ISimpleMarkerSymbol pNextSymbol = new SimpleMarkerSymbolClass();
                         pNextSymbol.Color = ClsGDBDataCommon.ColorToIColor(Color.Green);
                         pRenderer.Symbol = pNextSymbol as ISymbol;
                     }
                     else if (pFClass.ShapeType == esriGeometryType.esriGeometryPolygon)
                     {
                         ISimpleFillSymbol pNextSymbol = new SimpleFillSymbolClass();
                         pNextSymbol.Color = ClsGDBDataCommon.ColorToIColor(Color.Green);
                         pRenderer.Symbol = pNextSymbol as ISymbol;
                     }
                     pGeoFeatureLayer.Renderer = pRenderer as IFeatureRenderer;
                     //pTocControl.SetBuddyControl(pMapControl);
                     //pMapControl.Refresh();
                     //pTocControl.Refresh();
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
                
             }
             else
             {
                 if (gridviewmu.Rows.Count>1)
                 {
                     pUniquerendermu.DefaultLabel = null;
                     pUniquerendermu.UseDefaultSymbol = false;
                     pUniquerendermu.ColorScheme = "Custom";
                     ITable pTable = pDisplayTable as ITable;
                     bool isString = pTable.Fields.get_Field(fieldIndex).Type == esriFieldType.esriFieldTypeString;
                     pUniquerendermu.set_FieldType(0, isString);
                     pGeoFeatureLayer.Renderer = pUniquerendermu as IFeatureRenderer;
                     IUID pUID = new UIDClass();
                     pUID.Value = "{683C994E-A17B-11D1-8816-080009EC732A}";
                     pGeoFeatureLayer.RendererPropertyPageClassID = pUID as UIDClass;

                     //pTocControl.SetBuddyControl(pMapControl);
                     //pMapControl.Refresh();
                     //pTocControl.Refresh();
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
                 else
                 {
                     pUniquerendermu.RemoveAllValues();
                     if (pFClass.ShapeType == esriGeometryType.esriGeometryLine || pFClass.ShapeType == esriGeometryType.esriGeometryPolyline)
                     {
                         SimpleLineSymbol pNextSymbol = new SimpleLineSymbolClass();
                         pNextSymbol.Color = ClsGDBDataCommon.ColorToIColor(Color.Green);
                         pRenderer.Symbol = pNextSymbol as ISymbol;
                     }
                     else if (pFClass.ShapeType == esriGeometryType.esriGeometryMultipoint || pFClass.ShapeType == esriGeometryType.esriGeometryPoint)
                     {
                         ISimpleMarkerSymbol pNextSymbol = new SimpleMarkerSymbolClass();
                         pNextSymbol.Color = ClsGDBDataCommon.ColorToIColor(Color.Green);
                         pRenderer.Symbol = pNextSymbol as ISymbol;
                     }
                     else if (pFClass.ShapeType == esriGeometryType.esriGeometryPolygon)
                     {
                         ISimpleFillSymbol pNextSymbol = new SimpleFillSymbolClass();
                         pNextSymbol.Color = ClsGDBDataCommon.ColorToIColor(Color.Green);
                         pRenderer.Symbol = pNextSymbol as ISymbol;
                     }
                     pGeoFeatureLayer.Renderer = pRenderer as IFeatureRenderer;
                     //pTocControl.SetBuddyControl(pMapControl);
                     //pMapControl.Refresh();
                     //pTocControl.Refresh();
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
                

             }
             this.Close();
         }
        private void btnuse_Click(object sender, EventArgs e)
        {

            if (treeshow.SelectedIndex == 0)
            {
                if (gridviewu.Rows.Count > 1)
                {
                    pUniqueRender.DefaultLabel = null;
                    pUniqueRender.UseDefaultSymbol = false;
                    pUniqueRender.ColorScheme = "Custom";
                    ITable pTable = pDisplayTable as ITable;
                    bool isString = pTable.Fields.get_Field(fieldIndex).Type == esriFieldType.esriFieldTypeString;
                    pUniqueRender.set_FieldType(0, isString);
                    pGeoFeatureLayer.Renderer = pUniqueRender as IFeatureRenderer;
                    IUID pUID = new UIDClass();
                    pUID.Value = "{683C994E-A17B-11D1-8816-080009EC732A}";
                    pGeoFeatureLayer.RendererPropertyPageClassID = pUID as UIDClass;

                    //pTocControl.SetBuddyControl(pMapControl);
                    //pMapControl.Refresh();
                    //pTocControl.Refresh();
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
                else
                {
                    pUniqueRender.RemoveAllValues();
                    if (pFClass.ShapeType == esriGeometryType.esriGeometryLine || pFClass.ShapeType == esriGeometryType.esriGeometryPolyline)
                    {
                        SimpleLineSymbol pNextSymbol = new SimpleLineSymbolClass();
                        pNextSymbol.Color = ClsGDBDataCommon.ColorToIColor(Color.Green);
                        pRenderer.Symbol = pNextSymbol as ISymbol;
                    }
                    else if (pFClass.ShapeType == esriGeometryType.esriGeometryMultipoint || pFClass.ShapeType == esriGeometryType.esriGeometryPoint)
                    {
                        ISimpleMarkerSymbol pNextSymbol = new SimpleMarkerSymbolClass();
                        pNextSymbol.Color = ClsGDBDataCommon.ColorToIColor(Color.Green);
                        pRenderer.Symbol = pNextSymbol as ISymbol;
                    }
                    else if (pFClass.ShapeType == esriGeometryType.esriGeometryPolygon)
                    {
                        ISimpleFillSymbol pNextSymbol = new SimpleFillSymbolClass();
                        pNextSymbol.Color = ClsGDBDataCommon.ColorToIColor(Color.Green);
                        pRenderer.Symbol = pNextSymbol as ISymbol;
                    }
                    pGeoFeatureLayer.Renderer = pRenderer as IFeatureRenderer;
                    //pTocControl.SetBuddyControl(pMapControl);
                    //pMapControl.Refresh();
                    //pTocControl.Refresh();
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

            }
            else
            {
                if (gridviewmu.Rows.Count > 1)
                {
                    pUniquerendermu.DefaultLabel = null;
                    pUniquerendermu.UseDefaultSymbol = false;
                    pUniquerendermu.ColorScheme = "Custom";
                    ITable pTable = pDisplayTable as ITable;
                    bool isString = pTable.Fields.get_Field(fieldIndex).Type == esriFieldType.esriFieldTypeString;
                    pUniquerendermu.set_FieldType(0, isString);
                    pGeoFeatureLayer.Renderer = pUniquerendermu as IFeatureRenderer;
                    IUID pUID = new UIDClass();
                    pUID.Value = "{683C994E-A17B-11D1-8816-080009EC732A}";
                    pGeoFeatureLayer.RendererPropertyPageClassID = pUID as UIDClass;

                    //pTocControl.SetBuddyControl(pMapControl);
                    //pMapControl.Refresh();
                    //pTocControl.Refresh();
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
                else
                {
                    pUniquerendermu.RemoveAllValues();
                    if (pFClass.ShapeType == esriGeometryType.esriGeometryLine || pFClass.ShapeType == esriGeometryType.esriGeometryPolyline)
                    {
                        SimpleLineSymbol pNextSymbol = new SimpleLineSymbolClass();
                        pNextSymbol.Color = ClsGDBDataCommon.ColorToIColor(Color.Green);
                        pRenderer.Symbol = pNextSymbol as ISymbol;
                    }
                    else if (pFClass.ShapeType == esriGeometryType.esriGeometryMultipoint || pFClass.ShapeType == esriGeometryType.esriGeometryPoint)
                    {
                        ISimpleMarkerSymbol pNextSymbol = new SimpleMarkerSymbolClass();
                        pNextSymbol.Color = ClsGDBDataCommon.ColorToIColor(Color.Green);
                        pRenderer.Symbol = pNextSymbol as ISymbol;
                    }
                    else if (pFClass.ShapeType == esriGeometryType.esriGeometryPolygon)
                    {
                        ISimpleFillSymbol pNextSymbol = new SimpleFillSymbolClass();
                        pNextSymbol.Color = ClsGDBDataCommon.ColorToIColor(Color.Green);
                        pRenderer.Symbol = pNextSymbol as ISymbol;
                    }
                    pGeoFeatureLayer.Renderer = pRenderer as IFeatureRenderer;
                    //pTocControl.SetBuddyControl(pMapControl);
                    //pMapControl.Refresh();
                    //pTocControl.Refresh();
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
            }
        }

        private void btncancle_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmbcolormu_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (gridviewmu.Rows.Count > 1)
            {
                if (cmbcolormu.SelectedIndex != 0)
                {
                    int symbolindex = cmbcolormu.SelectedIndex;//获取选择的序号
                    IColorRamp PColorramp = null;
                    IStyleGalleryItem mpStyleGalleryItem = pSymbolClass.GetItem(symbolindex + 52);
                    PColorramp = (IColorRamp)mpStyleGalleryItem.Item;//获取选择的符号 
                    for (int i = 0; i <= pUniquerendermu.ValueCount - 1; i++)
                    {
                        gridviewmu.Rows[1 + i].Cells[0].Style.BackColor = ClsGDBDataCommon.IColorToColor(PColorramp.get_Color(i));
                        string xv;
                        xv = pUniquerendermu.get_Value(i);
                        if (xv != "")
                        {
                            if (pFClass.ShapeType == esriGeometryType.esriGeometryLine || pFClass.ShapeType == esriGeometryType.esriGeometryPolyline)
                            {
                                SimpleLineSymbol pNextSymbol = new SimpleLineSymbolClass();
                                pNextSymbol.Color = ClsGDBDataCommon.ColorToIColor(gridviewmu.Rows[1 + i].Cells[0].Style.BackColor);
                                pUniquerendermu.set_Symbol(xv, pNextSymbol as ISymbol);
                            }
                            else if (pFClass.ShapeType == esriGeometryType.esriGeometryMultipoint || pFClass.ShapeType == esriGeometryType.esriGeometryPoint)
                            {
                                ISimpleMarkerSymbol pNextSymbol = new SimpleMarkerSymbolClass();
                                pNextSymbol.Color = ClsGDBDataCommon.ColorToIColor(gridviewmu.Rows[1 + i].Cells[0].Style.BackColor);
                                pUniquerendermu.set_Symbol(xv, pNextSymbol as ISymbol);
                            }
                            else if (pFClass.ShapeType == esriGeometryType.esriGeometryPolygon)
                            {
                                ISimpleFillSymbol pNextSymbol = new SimpleFillSymbolClass();
                                pNextSymbol.Color = ClsGDBDataCommon.ColorToIColor(gridviewmu.Rows[1 + i].Cells[0].Style.BackColor);
                                pUniquerendermu.set_Symbol(xv, pNextSymbol as ISymbol);
                            }                  
                        }
                        
                    }
                }
                else
                {
                    pRandomRamp.Size = pUniquerendermu.ValueCount;
                    bool bOK;
                    IEnumColors pEnumColors = pRandomRamp.Colors;
                    pEnumColors.Reset();
                    for (int i = 0; i <= pUniquerendermu.ValueCount - 1; i++)
                    {
                        gridviewmu.Rows[1 + i].Cells[0].Style.BackColor = ClsGDBDataCommon.IColorToColor(pEnumColors.Next());
                        string xv;
                        xv = pUniquerendermu.get_Value(i);
                        if (xv != "")
                        {
                            if (pFClass.ShapeType == esriGeometryType.esriGeometryLine || pFClass.ShapeType == esriGeometryType.esriGeometryPolyline)
                            {
                                SimpleLineSymbol pNextSymbol = new SimpleLineSymbolClass();
                                pNextSymbol.Color = ClsGDBDataCommon.ColorToIColor(gridviewmu.Rows[1 + i].Cells[0].Style.BackColor);
                                pUniquerendermu.set_Symbol(xv, pNextSymbol as ISymbol);
                            }
                            else if (pFClass.ShapeType == esriGeometryType.esriGeometryMultipoint || pFClass.ShapeType == esriGeometryType.esriGeometryPoint)
                            {
                                ISimpleMarkerSymbol pNextSymbol = new SimpleMarkerSymbolClass();
                                pNextSymbol.Color = ClsGDBDataCommon.ColorToIColor(gridviewmu.Rows[1 + i].Cells[0].Style.BackColor);
                                pUniquerendermu.set_Symbol(xv, pNextSymbol as ISymbol);
                            }
                            else if (pFClass.ShapeType == esriGeometryType.esriGeometryPolygon)
                            {
                                ISimpleFillSymbol pNextSymbol = new SimpleFillSymbolClass();
                                pNextSymbol.Color = ClsGDBDataCommon.ColorToIColor(gridviewmu.Rows[1 + i].Cells[0].Style.BackColor);
                                pUniquerendermu.set_Symbol(xv, pNextSymbol as ISymbol);
                            }
                        }
                    }
                }
            }
           
        }

        private void cmbcolor_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (gridviewu.Rows.Count > 1)
            {
                if (cmbcolor.SelectedIndex != 0)
                {
                    int symbolindex = cmbcolor.SelectedIndex;//获取选择的序号
                    IColorRamp PColorramp = null;
                    IStyleGalleryItem mpStyleGalleryItem = pSymbolClass.GetItem(symbolindex + 52);
                    PColorramp = (IColorRamp)mpStyleGalleryItem.Item;//获取选择的符号 
                    for (int i = 0; i <= pUniqueRender.ValueCount - 1; i++)
                    {
                        gridviewu.Rows[1 + i].Cells[0].Style.BackColor = ClsGDBDataCommon.IColorToColor(PColorramp.get_Color(i));
                        string xv;
                        xv = pUniqueRender.get_Value(i);
                        if (xv != "")
                        {
                            if (pFClass.ShapeType == esriGeometryType.esriGeometryLine || pFClass.ShapeType == esriGeometryType.esriGeometryPolyline)
                            {
                                SimpleLineSymbol pNextSymbol = new SimpleLineSymbolClass();
                                pNextSymbol.Color = ClsGDBDataCommon.ColorToIColor(gridviewu.Rows[1 + i].Cells[0].Style.BackColor);
                                pUniqueRender.set_Symbol(xv, pNextSymbol as ISymbol);
                            }
                            else if (pFClass.ShapeType == esriGeometryType.esriGeometryMultipoint || pFClass.ShapeType == esriGeometryType.esriGeometryPoint)
                            {
                                ISimpleMarkerSymbol pNextSymbol = new SimpleMarkerSymbolClass();
                                pNextSymbol.Color = ClsGDBDataCommon.ColorToIColor(gridviewu.Rows[1 + i].Cells[0].Style.BackColor);
                                pUniqueRender.set_Symbol(xv, pNextSymbol as ISymbol);
                            }
                            else if (pFClass.ShapeType == esriGeometryType.esriGeometryPolygon)
                            {
                                ISimpleFillSymbol pNextSymbol = new SimpleFillSymbolClass();
                                pNextSymbol.Color = ClsGDBDataCommon.ColorToIColor(gridviewu.Rows[1 + i].Cells[0].Style.BackColor);
                                pUniqueRender.set_Symbol(xv, pNextSymbol as ISymbol);
                            }
                        }
                    }
                }
                else
                {
                    pRandomRamp.Size = pUniqueRender.ValueCount;
                    bool bOK;
                    IEnumColors pEnumColors = pRandomRamp.Colors;
                    pEnumColors.Reset();
                    for (int i = 0; i <= pUniqueRender.ValueCount - 1; i++)
                    {
                        gridviewu.Rows[1 + i].Cells[0].Style.BackColor = ClsGDBDataCommon.IColorToColor(pEnumColors.Next());
                        string xv;
                        xv = pUniqueRender.get_Value(i);
                        if (xv != "")
                        {
                            if (pFClass.ShapeType == esriGeometryType.esriGeometryLine || pFClass.ShapeType == esriGeometryType.esriGeometryPolyline)
                            {
                                SimpleLineSymbol pNextSymbol = new SimpleLineSymbolClass();
                                pNextSymbol.Color = ClsGDBDataCommon.ColorToIColor(gridviewu.Rows[1 + i].Cells[0].Style.BackColor);
                                pUniqueRender.set_Symbol(xv, pNextSymbol as ISymbol);
                            }
                            else if (pFClass.ShapeType == esriGeometryType.esriGeometryMultipoint || pFClass.ShapeType == esriGeometryType.esriGeometryPoint)
                            {
                                ISimpleMarkerSymbol pNextSymbol = new SimpleMarkerSymbolClass();
                                pNextSymbol.Color = ClsGDBDataCommon.ColorToIColor(gridviewu.Rows[1 + i].Cells[0].Style.BackColor);
                                pUniqueRender.set_Symbol(xv, pNextSymbol as ISymbol);
                            }
                            else if (pFClass.ShapeType == esriGeometryType.esriGeometryPolygon)
                            {
                                ISimpleFillSymbol pNextSymbol = new SimpleFillSymbolClass();
                                pNextSymbol.Color = ClsGDBDataCommon.ColorToIColor(gridviewu.Rows[1 + i].Cells[0].Style.BackColor);
                                pUniqueRender.set_Symbol(xv, pNextSymbol as ISymbol);
                            }
                        }
                    }
                }
            }
        }

        private void btnInlayer_Click(object sender, EventArgs e)
        {
            
            txtInlayer.Enabled = true;
            FrmUniqueInLayer frminlayer = new FrmUniqueInLayer(pLayer,pMapControl,pTocControl,this,pSceneControl);
            frminlayer.ShowDialog();
        }
      
    }
}
