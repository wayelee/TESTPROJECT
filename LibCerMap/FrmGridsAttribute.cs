/**
* @copyright Copyright(C), 2013-2013, PMRS Lab, IRSA, CAS
* @file FrmGridsAttribute.cs
* @date 2013.03.16
* @author Ge Xizhi
* @brief 格网属性设计
* @version 1.0
* @par 修改历史：
* <作者>  <修改日期>  <版本号>  <详细描述>\n
*
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.DataSourcesFile;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.SystemUI;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Display;
using System.Drawing.Text;
using stdole;
using DevComponents.DotNetBar;
namespace LibCerMap
{
    public partial class FrmGridsAttribute : OfficeForm
    {
        //外部只需要通过获取该变量，就可以获得界面上所设置的参数
        public IMapGrid m_pMapGrid=null;

        ////用于保存和加载对话框中的参数
        //public ClsGridAttibute m_gridAttibute = new ClsGridAttibute();

        string[] SymbolStyle;

        //从界面获取相应参数
        private void updateFromUI()
        {
            if (m_pMapGrid == null)
                return;

            #region 长轴
            //显示位置
            bool[] bShowPos = new bool[4];// m_gridAttibute.longIndexPos;
            bShowPos[0]=this.cBoxDivisionLeft.Checked;
            bShowPos[1]=this.cBoxDivisionTop.Checked;
            bShowPos[2]=this.cBoxDivisionRight.Checked  ;
            bShowPos[3]=this.cBoxDivisionBottom.Checked ;
            m_pMapGrid.SetTickVisibility(bShowPos[0], bShowPos[1], bShowPos[2], bShowPos[3]);

            //样式，已在事件更新，略            

            //显示位置:内部或者外部
            if (cBoxDivisionOutside.Checked)
            {
                m_pMapGrid.TickLength = Math.Abs(DivisionTickSize.Value);
                //m_gridAttibute.longIndexOutside = true;
                //m_gridAttibute.longIndexLength = Math.Abs(DivisionTickSize.Value);
            }
            else
            {
                m_pMapGrid.TickLength = -1 * Math.Abs(DivisionTickSize.Value);
                //m_gridAttibute.longIndexOutside = false;
                //m_gridAttibute.longIndexLength = -1*Math.Abs(DivisionTickSize.Value);
            }
            #endregion

            #region 短轴
            //显示位置
            //bShowPos = m_gridAttibute.shortIndexPos;
            bShowPos[0]=this.cBoxSubDivisionLeft.Checked ;
            bShowPos[1]=this.cBoxSubDivisionTop.Checked  ;
             bShowPos[2]=this.cBoxSubDivisionRight.Checked ;
            bShowPos[3]=this.cBoxSubDivisionBottom.Checked ;
            m_pMapGrid.SetSubTickVisibility(bShowPos[0], bShowPos[1], bShowPos[2], bShowPos[3]);

            //样式，在事件响应中已经更新，略
            
            //显示位置：内部或者外部
            if (cBoxSubDivisionOutside.Checked)
            {
                m_pMapGrid.SubTickLength = Math.Abs(SubDivisionTickSize.Value);
                //m_gridAttibute.shortIndexOutside = true;
                //m_gridAttibute.shortIndexLength = Math.Abs(this.SubDivisionTickSize.Value);
            }
            else
            {
                m_pMapGrid.SubTickLength = -1*Math.Abs(SubDivisionTickSize.Value);
                //m_gridAttibute.shortIndexOutside = false;
                //m_gridAttibute.shortIndexLength = -1*Math.Abs(this.SubDivisionTickSize.Value);
            }

            //显示大小和数目            
            //m_gridAttibute.nShortIndexCount=this.SubDivisionNumber.Value;
            m_pMapGrid.SubTickCount = Convert.ToInt16(SubDivisionNumber.Value);
            #endregion

            #region 边框
            //btBorder_Click(null, null);
            #endregion

            #region 标注
            
            //显示位置
            bShowPos[0]=this.cBoxLabelLeft.Checked ;
            bShowPos[1]=this.cBoxLabelTop.Checked ;
            bShowPos[2]=this.cBoxLabelRight.Checked ;
            bShowPos[3]=this.cBoxLabelBottom.Checked ;
            m_pMapGrid.SetLabelVisibility(bShowPos[0], bShowPos[1], bShowPos[2], bShowPos[3]);

            #region 标注风格:字体名称、大小、颜色、间隔等

            //加载字体名称            
            IFontDisp pFont = new StdFontClass() as IFontDisp;
            pFont.Name = this.cbBoxLabelFont.Text;
            pFont.Size = Convert.ToDecimal(this.cbBoxLabelSize.Text);
            
            //m_pMapGrid.LabelFormat.Font.Name = this.cbBoxLabelFont.Text;
            //m_pMapGrid.LabelFormat.Font.Size = Convert.ToDecimal(this.cbBoxLabelSize.Text);
            
            //字体颜色
            if(LabelColor.SelectedColor!=null)
            {
                m_pMapGrid.LabelFormat.Color = ClsGDBDataCommon.ColorToIColor(LabelColor.SelectedColor);
            }
            

            //字体与边框间隔
            m_pMapGrid.LabelFormat.LabelOffset=this.txtLabelOffset.Value;

            //字体风格
            if (this.toolBtnBold.Checked)
            {
                pFont.Bold = true;
            }
            else
            {
                pFont.Bold = false;
            }

            if (this.toolBtnIntend.Checked)
            {
                pFont.Italic = true;
            }
            else
            {
                pFont.Italic = false;
            }
            if (this.toolBtnStrikethrough.Checked)
            {
                pFont.Strikethrough = true;
            }
            else
            {
                pFont.Strikethrough = false;
            }

            if (this.toolBtnUnderline.Checked)
            {
                pFont.Underline = true;
            }
            else
            {
                pFont.Underline = false;
            }

            m_pMapGrid.LabelFormat.Font = pFont;
            #endregion

            #region 是否垂直显示
            m_pMapGrid.LabelFormat.set_LabelAlignment(esriGridAxisEnum.esriGridAxisBottom, !this.cBoxLabelVBottom.Checked);
            m_pMapGrid.LabelFormat.set_LabelAlignment(esriGridAxisEnum.esriGridAxisLeft, !this.cBoxLabelVLeft.Checked);
            m_pMapGrid.LabelFormat.set_LabelAlignment(esriGridAxisEnum.esriGridAxisRight, !this.cBoxLabelVRight.Checked);
            m_pMapGrid.LabelFormat.set_LabelAlignment(esriGridAxisEnum.esriGridAxisTop, !this.cBoxLabelVTop.Checked);
            #endregion

            #region 小数位数
            if (m_pMapGrid.LabelFormat is IFormattedGridLabel || m_pMapGrid.LabelFormat is IMixedFontGridLabel)
            {
                intergerInputDigitNum.Enabled = true;

                IFormattedGridLabel pFormattedGridLabel = m_pMapGrid.LabelFormat as IFormattedGridLabel;
                INumericFormat pNumbericFormat = new NumericFormatClass();
                INumberFormat pNumberFormat = pNumbericFormat as INumberFormat;
                
                pNumbericFormat.RoundingValue = intergerInputDigitNum.Value;
                pNumbericFormat.RoundingOption = esriRoundingOptionEnum.esriRoundNumberOfDecimals;
                pNumbericFormat.ZeroPad = true;

                pFormattedGridLabel.Format = pNumbericFormat as INumberFormat;
            }
            else if (m_pMapGrid.LabelFormat is IDMSGridLabel)
            {
                intergerInputDigitNum.Enabled = false;
                IDMSGridLabel3 pDMSGridLabel = m_pMapGrid.LabelFormat as IDMSGridLabel3;
 
                switch (cbBoxLabelFormat.Text)
                {
                    case "Standard":
                        pDMSGridLabel.LabelType = esriDMSGridLabelType.esriDMSGridLabelStandard;
                        break;
                    case "Stacked":
                        pDMSGridLabel.LabelType = esriDMSGridLabelType.esriDMSGridLabelStacked;
                        break;
                    case "Degrees":
                        pDMSGridLabel.LabelType = esriDMSGridLabelType.esriDMSGridLabelDD;
                        break;
                    case "Degrees Minutes":
                        pDMSGridLabel.LabelType = esriDMSGridLabelType.esriDMSGridLabelDM;
                        break;
                    case "Degrees Minutes Seconds":
                        pDMSGridLabel.LabelType = esriDMSGridLabelType.esriDMSGridLabelStandard;
                        break;
                    default:
                        pDMSGridLabel.LabelType = esriDMSGridLabelType.esriDMSGridLabelStandard;
                        break;
                }

                if (chkShowMinusSign.Checked)
                {
                    pDMSGridLabel.LatLonFormat.ShowDirections = false;
                    pDMSGridLabel.ShowMinusSign = true;
                }
                else
                {
                    pDMSGridLabel.LatLonFormat.ShowDirections = true;
                    pDMSGridLabel.ShowMinusSign = false;
                }

            }
            //try
            //{
            //    IFormattedGridLabel pFormattedGridLabel = m_pMapGrid.LabelFormat as IFormattedGridLabel;
            //    INumericFormat pNumbericFormat  = new NumericFormatClass();
            //    pNumbericFormat.RoundingValue = intergerInputDigitNum.Value;
            //    pNumbericFormat.RoundingOption = esriRoundingOptionEnum.esriRoundNumberOfDecimals;
            //    pNumbericFormat.ZeroPad = true;

            //    pFormattedGridLabel.Format = pNumbericFormat as INumberFormat;

            //    this.lblDigitNum.Visible = true;
            //    this.intergerInputDigitNum.Visible = true;
            //}
            //catch (System.Exception ex)
            //{
            //    this.lblDigitNum.Visible = false;
            //    this.intergerInputDigitNum.Visible = false;
            //}
            #endregion
            #endregion

            #region 风格线和刻度
            if (this.cBoxLineGridNull.Checked)
            {
                m_pMapGrid.LineSymbol=null;
                m_pMapGrid.TickMarkSymbol=null;
            }

            if(cBoxLineGridLine.Checked)
            {
                //m_gridAttibute.annotationMarkerSymbol=null;
                m_pMapGrid.TickMarkSymbol = null;
            }

            if( cBoxLineGridTick.Checked)
            {
                //m_gridAttibute.annotationLineSymbol=null;
                m_pMapGrid.LineSymbol = null;
            }
            #endregion

            #region 根据不同类型的风格显示不同的间隔或索引
            if (m_pMapGrid is IMeasuredGrid)
            {
                #region 公里格网

                IMeasuredGrid pMeasureGrid = m_pMapGrid as IMeasuredGrid;
                //单位设置
                esriUnits pUnits = pMeasureGrid.Units;
                if (this.cboBoxMIntervalUnit.Text == "Centimeters")
                {
                    pUnits = esriUnits.esriCentimeters;
                }
                else if (this.cboBoxMIntervalUnit.Text == "DecimalDegrees")
                {
                    pUnits = esriUnits.esriDecimalDegrees;
                }
                else if (this.cboBoxMIntervalUnit.Text == "Decimeters")
                {
                    pUnits = esriUnits.esriDecimeters;
                }
                else if (this.cboBoxMIntervalUnit.Text == "Feet")
                {
                    pUnits = esriUnits.esriFeet;
                }
                else if (this.cboBoxMIntervalUnit.Text == "Inches")
                {
                    pUnits = esriUnits.esriInches;
                }
                else if (this.cboBoxMIntervalUnit.Text == "Kilometers")
                {
                    pUnits = esriUnits.esriKilometers;
                }
                else if (this.cboBoxMIntervalUnit.Text == "Meters")
                {
                    pUnits = esriUnits.esriMeters;
                }
                else if (this.cboBoxMIntervalUnit.Text == "Miles")
                {
                    pUnits = esriUnits.esriMiles;
                }
                else if (this.cboBoxMIntervalUnit.Text == "Millimeters")
                {
                    pUnits = esriUnits.esriMillimeters;
                }
                else if (this.cboBoxMIntervalUnit.Text == "NauticalMiles")
                {
                    pUnits = esriUnits.esriNauticalMiles;
                }
                else if (this.cboBoxMIntervalUnit.Text == "Points")
                {
                    pUnits = esriUnits.esriPoints;
                }
                else if (this.cboBoxMIntervalUnit.Text == "UnknownUnits")
                {
                    pUnits = esriUnits.esriUnknownUnits;
                }
                else if (this.cboBoxMIntervalUnit.Text == "Yards")
                {
                    pUnits = esriUnits.esriYards;
                }
                else
                    pUnits=esriUnits.esriMeters;


                //间隔
                pMeasureGrid.XIntervalSize = Convert.ToDouble(this.txtMIntervalX.Text);
                pMeasureGrid.YIntervalSize = Convert.ToDouble(this.txtMIntervalY.Text);
                pMeasureGrid.Units = pUnits;

                if (this.cBoxMOriginDefine.Checked) //固定原点
                {
                    pMeasureGrid.FixedOrigin=true;
                    pMeasureGrid.XOrigin=Convert.ToDouble(this.txtMXOrigin.Text);
                    pMeasureGrid.YOrigin=Convert.ToDouble(this.txtMYOrigin.Text);
                }
                else
                {
                    //m_gridAttibute.bMeasureGridIsFixOrigin = false;
                    pMeasureGrid.FixedOrigin = false;
                }
                #endregion
            }
            else if (m_pMapGrid is IGraticule/*.bIsGraticuleGrid*/) //经纬格网
            {
                #region 经纬格网
                IMeasuredGrid pMeasureGrid = m_pMapGrid as IMeasuredGrid;

                //单位设置
                esriUnits pUnits = pMeasureGrid.Units;
                if (this.cboBoxMIntervalUnit.Text == "Centimeters")
                {
                    pUnits = esriUnits.esriCentimeters;
                }
                else if (this.cboBoxMIntervalUnit.Text == "DecimalDegrees")
                {
                    pUnits = esriUnits.esriDecimalDegrees;
                }
                else if (this.cboBoxMIntervalUnit.Text == "Decimeters")
                {
                    pUnits = esriUnits.esriDecimeters;
                }
                else if (this.cboBoxMIntervalUnit.Text == "Feet")
                {
                    pUnits = esriUnits.esriFeet;
                }
                else if (this.cboBoxMIntervalUnit.Text == "Inches")
                {
                    pUnits = esriUnits.esriInches;
                }
                else if (this.cboBoxMIntervalUnit.Text == "Kilometers")
                {
                    pUnits = esriUnits.esriKilometers;
                }
                else if (this.cboBoxMIntervalUnit.Text == "Meters")
                {
                    pUnits = esriUnits.esriMeters;
                }
                else if (this.cboBoxMIntervalUnit.Text == "Miles")
                {
                    pUnits = esriUnits.esriMiles;
                }
                else if (this.cboBoxMIntervalUnit.Text == "Millimeters")
                {
                    pUnits = esriUnits.esriMillimeters;
                }
                else if (this.cboBoxMIntervalUnit.Text == "NauticalMiles")
                {
                    pUnits = esriUnits.esriNauticalMiles;
                }
                else if (this.cboBoxMIntervalUnit.Text == "Points")
                {
                    pUnits = esriUnits.esriPoints;
                }
                else if (this.cboBoxMIntervalUnit.Text == "UnknownUnits")
                {
                    pUnits = esriUnits.esriUnknownUnits;
                }
                else if (this.cboBoxMIntervalUnit.Text == "Yards")
                {
                    pUnits = esriUnits.esriYards;
                }
                else
                    pUnits = esriUnits.esriMeters;

                pMeasureGrid.XIntervalSize = double.Parse(this.txtGXIntervalD.Text) + double.Parse(this.txtGXIntervalF.Text) / 60 + double.Parse(this.txtGXIntervalM.Text) / 3600;
                pMeasureGrid.YIntervalSize = double.Parse(this.txtGYIntervalD.Text) + double.Parse(this.txtGYIntervalF.Text) / 60 + double.Parse(this.txtGYIntervalM.Text) / 3600;
                pMeasureGrid.Units = pUnits;

                if (this.cBoxGOriginDefine.Checked == true)
                {
                    pMeasureGrid.FixedOrigin = true;
                    pMeasureGrid.XOrigin = double.Parse(this.txtGXOriginD.Text) + double.Parse(this.txtGXOriginF.Text) / 60 + double.Parse(this.txtGXOriginM.Text) / 3600;
                    pMeasureGrid.YOrigin = double.Parse(this.txtGYOriginD.Text) + double.Parse(this.txtGYOriginF.Text) / 60 + double.Parse(this.txtGYOriginM.Text) / 3600;
                }
                else
                {
                    pMeasureGrid.FixedOrigin = false;
                    //m_gridAttibute.bGraticuleGridIsFixOrigin = false;
                }
                #endregion
            }
            else if (m_pMapGrid is IIndexGrid/*.bIsIndexGrid*/) //索引格网
            {
                #region 索引格网
                
                //设置索引标注
                IIndexGrid pIndexGrid = m_pMapGrid as IIndexGrid;
                pIndexGrid.ColumnCount = IndexColumnNumber.Value;
                pIndexGrid.RowCount = IndexRowColumn.Value;

                string[] szStringList = txtColumnHeadings.Text.Split('\n');
                for (int i = 0; i < szStringList.Length; i++)
                    pIndexGrid.set_XLabel(i, szStringList[i]);
                    //if (szStringList.Length == IndexColumnNumber.Value)
                    //    m_gridAttibute.szIndexGridColumn = szStringList;

                szStringList = txtRowHeading.Text.Split('\n');
                for (int i = 0; i < szStringList.Length; i++)
                    pIndexGrid.set_YLabel(i, szStringList[i]);
                //if (szStringList.Length == IndexRowColumn.Value)
                //    m_gridAttibute.szIndexGridRow = szStringList;
                #endregion
            }
            else
                ;
            #endregion
        }

        //用参数更新相应界面
        private void updateUI()
        {
            if (m_pMapGrid == null)
                return;

#region 长轴
            bool[] bShowPos = new bool[4];
            m_pMapGrid.QueryTickVisibility(ref bShowPos[0], ref bShowPos[1], ref bShowPos[2], ref bShowPos[3]);

            //显示位置
            this.cBoxDivisionLeft.Checked = bShowPos[0];
            this.cBoxDivisionTop.Checked = bShowPos[1];
            this.cBoxDivisionRight.Checked = bShowPos[2];
            this.cBoxDivisionBottom.Checked = bShowPos[3];

            //样式
            ILineSymbol pLineSymbol=m_pMapGrid.TickLineSymbol;
            if ( pLineSymbol!= null)
            {
                //FrmSymbol FrmSymbols = new FrmSymbol(SymbolStyle, (ISymbol)pLineSymbol, esriSymbologyStyleClass.esriStyleClassLineSymbols);
                if (btDivisionSymbol.Image != null)
                    btDivisionSymbol.Image.Dispose();
                //this.btDivisionSymbol.Image = FrmSymbols.GetImageByGiveSymbolBeforeSelectItem(esriSymbologyStyleClass.esriStyleClassLineSymbols, (ISymbol)pLineSymbol, btDivisionSymbol.Width, btDivisionSymbol.Height);
                this.btDivisionSymbol.Image = CommonProcess.getImageFromSymbol((ISymbol)pLineSymbol, btDivisionSymbol.Width, btDivisionSymbol.Height);
            }

            //显示位置:内部或者外部
            if (m_pMapGrid.TickLength>=0)
            {
                cBoxDivisionOutside.Checked = true;
                cBoxDivisionInside.Checked = false;
            }
            else
            {
                cBoxDivisionOutside.Checked = false;
                cBoxDivisionInside.Checked = true;
            }

            DivisionTickSize.Value = Math.Abs(m_pMapGrid.TickLength);
#endregion

#region 短轴
            //显示位置
            //bShowPos = m_gridAttibute.shortIndexPos;
            m_pMapGrid.QuerySubTickVisibility(ref bShowPos[0], ref bShowPos[1], ref bShowPos[2], ref bShowPos[3]);
            this.cBoxSubDivisionLeft.Checked = bShowPos[0];
            this.cBoxSubDivisionTop.Checked = bShowPos[1];
            this.cBoxSubDivisionRight.Checked = bShowPos[2];
            this.cBoxSubDivisionBottom.Checked = bShowPos[3];
            
            //样式
            pLineSymbol = m_pMapGrid.SubTickLineSymbol;//.shortIndexLineSymbol;
            if (pLineSymbol != null)
            {
                //FrmSymbol FrmSymbolss = new FrmSymbol(SymbolStyle, (ISymbol)pLineSymbol, esriSymbologyStyleClass.esriStyleClassLineSymbols);

                if (btSubDivisionSymbol.Image != null)
                    btSubDivisionSymbol.Image.Dispose();
                //this.btSubDivisionSymbol.Image = FrmSymbolss.GetImageByGiveSymbolBeforeSelectItem(esriSymbologyStyleClass.esriStyleClassLineSymbols, (ISymbol)pLineSymbol, btSubDivisionSymbol.Width, btSubDivisionSymbol.Height);
                this.btSubDivisionSymbol.Image = CommonProcess.getImageFromSymbol((ISymbol)pLineSymbol, btSubDivisionSymbol.Width, btSubDivisionSymbol.Height);
            }

            //显示位置：内部或者外部
            if (m_pMapGrid.SubTickLength>=0)
            {
                cBoxSubDivisionOutside.Checked = true;
                cBoxSubDivisionInside.Checked = false;
            }
            else
            {
                cBoxSubDivisionOutside.Checked = false;
                cBoxSubDivisionInside.Checked = true;
            }

            //显示大小和数目
            this.SubDivisionTickSize.Value = Math.Abs(m_pMapGrid.SubTickLength);//.shortIndexLength;
            this.SubDivisionNumber.Value = m_pMapGrid.SubTickCount;//.nShortIndexCount;
#endregion

#region 边框
            if (m_pMapGrid.Border is ISimpleMapGridBorder) //简单边框
                cbBoxBoder.SelectedItem = cbItemSimple;
            else
                cbBoxBoder.SelectedItem = cbItemCalibrate;  //整饰边框
#endregion

#region 标注

            //设置界面上标注样式
            cbBoxLabelFormat.Items.Clear();
            if (m_pMapGrid is IGraticule)
            {
                cbBoxLabelFormat.Items.Add("Standard");
                cbBoxLabelFormat.Items.Add("Stacked");
                cbBoxLabelFormat.Items.Add("Degrees");
                cbBoxLabelFormat.Items.Add("Degrees Minutes");
                cbBoxLabelFormat.Items.Add("Degrees Minutes Seconds");
            }
            else
            {
                cbBoxLabelFormat.Items.Add("Formatted");
                cbBoxLabelFormat.Items.Add("MixedFont");
            }
            //选中当前样式
            if (m_pMapGrid.LabelFormat is IFormattedGridLabel)
            {
                cbBoxLabelFormat.Text = "Formatted";
                //获取小数位数
                IFormattedGridLabel pFormattedGridLabel = m_pMapGrid.LabelFormat as IFormattedGridLabel;
                INumericFormat pNumericFormat  = pFormattedGridLabel.Format as INumericFormat;
                if (pNumericFormat !=null)
                {
                    intergerInputDigitNum.Text = pNumericFormat.RoundingValue.ToString();
                }          
            }
            else if (m_pMapGrid.LabelFormat is IMixedFontGridLabel)
            {
                cbBoxLabelFormat.Text = "MixedFont";
                //获取小数位数
                IFormattedGridLabel pFormattedGridLabel = m_pMapGrid.LabelFormat as IFormattedGridLabel;
                INumericFormat pNumericFormat = pFormattedGridLabel.Format as INumericFormat;
                 if (pNumericFormat !=null)
                {
                    intergerInputDigitNum.Text = pNumericFormat.RoundingValue.ToString();
                }          
            }
            else if (m_pMapGrid.LabelFormat is IDMSGridLabel)
            {
                IDMSGridLabel pDMSGridLabel = m_pMapGrid.LabelFormat as IDMSGridLabel;
                switch (pDMSGridLabel.LabelType)
                {
                    case esriDMSGridLabelType.esriDMSGridLabelStandard:
                        cbBoxLabelFormat.Text = "Standard";
                        break;
                    case esriDMSGridLabelType.esriDMSGridLabelStacked:
                        cbBoxLabelFormat.Text = "Stacked";
                        break;
                    case esriDMSGridLabelType.esriDMSGridLabelDD:
                        cbBoxLabelFormat.Text = "Degrees";
                        break;
                    case esriDMSGridLabelType.esriDMSGridLabelDM:
                        cbBoxLabelFormat.Text = "Degrees Minutes";
                        break;
                    case esriDMSGridLabelType.esriDMSGridLabelDS:
                        cbBoxLabelFormat.Text = "Degrees Minutes Seconds";
                        break;
                    default:
                        break;
                }
            }
            
            m_pMapGrid.QueryLabelVisibility(ref bShowPos[0], ref bShowPos[1], ref bShowPos[2], ref bShowPos[3]);//.annotationPos;

            //显示位置
            this.cBoxLabelLeft.Checked = bShowPos[0];
            this.cBoxLabelTop.Checked = bShowPos[1];
            this.cBoxLabelRight.Checked = bShowPos[2];
            this.cBoxLabelBottom.Checked = bShowPos[3];

            #region 标注风格:字体名称、大小、颜色、间隔等

            //加载字体名称
            InstalledFontCollection pFontCollection = new InstalledFontCollection();
            FontFamily[] pFontFamily = pFontCollection.Families;
            for (int i = 0; i < pFontFamily.Length; i++)
            {
                string pFontName = pFontFamily[i].Name;
                this.cbBoxLabelFont.Items.Add(pFontName);
            }
            this.cbBoxLabelFont.SelectedText = m_pMapGrid.LabelFormat.Font.Name;

            //字体大小
            for (int i = 3; i <= 100; i++)
            {
                this.cbBoxLabelSize.Items.Add(i.ToString());
            }
            this.cbBoxLabelSize.Text = m_pMapGrid.LabelFormat.Font.Size.ToString();

            //字体颜色
            IColor pTextColor = m_pMapGrid.LabelFormat.Color;
            Color pColor = ColorTranslator.FromOle(pTextColor.RGB);
            LabelColor.SelectedColor = pColor;

            //字体与边框间隔
            this.txtLabelOffset.Text = m_pMapGrid.LabelFormat.LabelOffset.ToString();
            
            //字体风格
            if (m_pMapGrid.LabelFormat.Font.Bold)
            {
                this.toolBtnBold.Checked = true;
            }
            else
            {
                this.toolBtnBold.Checked = false;
            }

            if (m_pMapGrid.LabelFormat.Font.Italic)
            {
                this.toolBtnIntend.Checked = true;
            }
            else
            {
                this.toolBtnIntend.Checked = false;
            }
            if (m_pMapGrid.LabelFormat.Font.Strikethrough)
            {
                this.toolBtnStrikethrough.Checked = true;
            }
            else
            {
                this.toolBtnStrikethrough.Checked = false;
            }

            if (m_pMapGrid.LabelFormat.Font.Underline)
            {
                this.toolBtnUnderline.Checked = true;
            }
            else
            {
                this.toolBtnUnderline.Checked = false;
            }
            #endregion

            #region 是否垂直显示
            if (m_pMapGrid.LabelFormat.get_LabelAlignment(esriGridAxisEnum.esriGridAxisBottom))
            {
                this.cBoxLabelVBottom.Checked = false;
            }
            else
            {
                this.cBoxLabelBottom.Checked = true;
            }
            if (m_pMapGrid.LabelFormat.get_LabelAlignment(esriGridAxisEnum.esriGridAxisLeft))
            {
                this.cBoxLabelVLeft.Checked = false;
            }
            else
            {
                this.cBoxLabelVLeft.Checked = true;
            }
            if (m_pMapGrid.LabelFormat.get_LabelAlignment(esriGridAxisEnum.esriGridAxisRight))
            {
                this.cBoxLabelVRight.Checked = false;
            }
            else
            {
                this.cBoxLabelVRight.Checked = true;
            }
            if (m_pMapGrid.LabelFormat.get_LabelAlignment(esriGridAxisEnum.esriGridAxisTop))
            {
                this.cBoxLabelVTop.Checked = false;
            }
            else
            {
                this.cBoxLabelVTop.Checked = true;
            }
            #endregion            

#region 小数位数
            try
            {
                IFormattedGridLabel pFormattedGridLabel = m_pMapGrid.LabelFormat as IFormattedGridLabel;
                INumericFormat pNumbericFormat = pFormattedGridLabel.Format as INumericFormat;
                if (pNumbericFormat != null)
                    this.intergerInputDigitNum.Value = Convert.ToInt32(pNumbericFormat.RoundingValue);
                else
                    this.intergerInputDigitNum.Value = 6;

                this.intergerInputDigitNum.Visible = true;
                this.lblDigitNum.Visible = true;
            }
            catch (System.Exception ex)
            {
                this.intergerInputDigitNum.Visible = false;
                this.lblDigitNum.Visible = false;
            }
#endregion
            #endregion

            #region 显示风格线和刻度
            pLineSymbol =m_pMapGrid.LineSymbol;//.annotationLineSymbol;
            IMarkerSymbol pMarkerSymbol=m_pMapGrid.TickMarkSymbol;//.annotationMarkerSymbol ;
            if (pLineSymbol == null && pMarkerSymbol == null)
            {
                this.cBoxLineGridNull.Checked = true;
                this.cBoxLineGridLine.Checked = false;
                this.cBoxLineGridTick.Checked = false;

                //更新图像按钮
                this.btLineSymbol.Image = null;
                this.btLineSymbol.Enabled = false;
            }
            else if (pMarkerSymbol != null)
            {
                this.cBoxLineGridNull.Checked = false;
                this.cBoxLineGridLine.Checked = false;
                this.cBoxLineGridTick.Checked = true;
                //pMarkerSymbol = pMapGrid.TickMarkSymbol as IMarkerSymbol;

                //更新图像按钮
                this.btLineSymbol.Enabled = true;
                //FrmSymbol FrmSymbolss = new FrmSymbol(SymbolStyle, (ISymbol)pMarkerSymbol, esriSymbologyStyleClass.esriStyleClassMarkerSymbols);

                if (btLineSymbol.Image != null)
                    btLineSymbol.Image.Dispose();

//                this.btLineSymbol.Image = FrmSymbolss.GetImageByGiveSymbolBeforeSelectItem(esriSymbologyStyleClass.esriStyleClassMarkerSymbols, (ISymbol)pMarkerSymbol, btLineSymbol.Width, btLineSymbol.Height);
                this.btLineSymbol.Image = CommonProcess.getImageFromSymbol((ISymbol)pMarkerSymbol, btLineSymbol.Width, btLineSymbol.Height);
            }
            else if (pLineSymbol != null)
            {
                this.cBoxLineGridNull.Checked = false;
                this.cBoxLineGridLine.Checked = true;
                this.cBoxLineGridTick.Checked = false;
                //pLineSymbol = pMapGrid.LineSymbol as ILineSymbol;

                this.btLineSymbol.Enabled = true;
                //FrmSymbol FrmSymbolss = new FrmSymbol(SymbolStyle, (ISymbol)pLineSymbol, esriSymbologyStyleClass.esriStyleClassLineSymbols);

                if (btLineSymbol.Image != null)
                    btLineSymbol.Image.Dispose();
                //this.btLineSymbol.Image = FrmSymbolss.GetImageByGiveSymbolBeforeSelectItem(esriSymbologyStyleClass.esriStyleClassLineSymbols, (ISymbol)pLineSymbol, btLineSymbol.Width, btLineSymbol.Height);
                this.btLineSymbol.Image = CommonProcess.getImageFromSymbol((ISymbol)pLineSymbol, btLineSymbol.Width, btLineSymbol.Height);
            }
            else 
                ;
#endregion

#region 根据不同类型的风格显示不同的间隔或索引
            if (m_pMapGrid is IMeasuredGrid)
            {
                #region 公里格网
                //显示选项卡
                this.tabItemMInterval.Visible = true;
                this.tabItemGInterval.Visible = false;
                this.tabItemIndex.Visible = false;

                //单位
                this.cboBoxMIntervalUnit.Items.Add("Centimeters");
                this.cboBoxMIntervalUnit.Items.Add("DecimalDegrees");
                this.cboBoxMIntervalUnit.Items.Add("Decimeters");
                this.cboBoxMIntervalUnit.Items.Add("Feet");
                this.cboBoxMIntervalUnit.Items.Add("Inches");
                this.cboBoxMIntervalUnit.Items.Add("Kilometers");
                this.cboBoxMIntervalUnit.Items.Add("Meters");
                this.cboBoxMIntervalUnit.Items.Add("Miles");
                this.cboBoxMIntervalUnit.Items.Add("Millimeters");
                this.cboBoxMIntervalUnit.Items.Add("NauticalMiles");
                this.cboBoxMIntervalUnit.Items.Add("Points");
                this.cboBoxMIntervalUnit.Items.Add("UnknownUnits");
                this.cboBoxMIntervalUnit.Items.Add("Yards");

                IMeasuredGrid pMeasureGrid=m_pMapGrid as IMeasuredGrid;
                if (pMeasureGrid.Units == esriUnits.esriCentimeters)
                {
                    this.cboBoxMIntervalUnit.Text = "Centimeters";
                }
                else if (pMeasureGrid.Units == esriUnits.esriDecimalDegrees)
                {
                    this.cboBoxMIntervalUnit.Text = "DecimalDegrees";
                }
                else if (pMeasureGrid.Units == esriUnits.esriDecimeters)
                {
                    this.cboBoxMIntervalUnit.Text = "Decimeters";
                }
                else if (pMeasureGrid.Units == esriUnits.esriFeet)
                {
                    this.cboBoxMIntervalUnit.Text = "Feet";
                }
                else if (pMeasureGrid.Units == esriUnits.esriInches)
                {
                    this.cboBoxMIntervalUnit.Text = "Inches";
                }
                else if (pMeasureGrid.Units == esriUnits.esriKilometers)
                {
                    this.cboBoxMIntervalUnit.Text = "Kilometers";
                }
                else if (pMeasureGrid.Units == esriUnits.esriMeters)
                {
                    this.cboBoxMIntervalUnit.Text = "Meters";
                }
                else if (pMeasureGrid.Units == esriUnits.esriMiles)
                {
                    this.cboBoxMIntervalUnit.Text = "Miles";
                }
                else if (pMeasureGrid.Units == esriUnits.esriMillimeters)
                {
                    this.cboBoxMIntervalUnit.Text = "Millimeters";
                }
                else if (pMeasureGrid.Units == esriUnits.esriNauticalMiles)
                {
                    this.cboBoxMIntervalUnit.Text = "NauticalMiles";
                }
                else if (pMeasureGrid.Units == esriUnits.esriPoints)
                {
                    this.cboBoxMIntervalUnit.Text = "Points";
                }
                else if (pMeasureGrid.Units == esriUnits.esriUnknownUnits)
                {
                    this.cboBoxMIntervalUnit.Text = "UnknownUnits";
                }
                else if (pMeasureGrid.Units == esriUnits.esriYards)
                {
                    this.cboBoxMIntervalUnit.Text = "Yards";
                }
                else
                    ;

                //间隔
                this.txtMIntervalX.Text = pMeasureGrid.XIntervalSize.ToString();
                this.txtMIntervalY.Text = pMeasureGrid.YIntervalSize.ToString();

                if (pMeasureGrid.FixedOrigin) //固定原点
                {
                    this.cBoxMOriginDefine.Checked = true;
                    this.cBoxMOriginFrom.Checked = false;
                    this.txtMXOrigin.Text = pMeasureGrid.XOrigin.ToString();
                    this.txtMYOrigin.Text = pMeasureGrid.YOrigin.ToString();
                    
                }
                else
                {
                    this.cBoxMOriginFrom.Checked = true;
                    this.cBoxMOriginDefine.Checked = false;
                    this.txtMXOrigin.Enabled = false;
                    this.txtMYOrigin.Enabled = false;
                }
                #endregion
            }
            else if (m_pMapGrid is IGraticule) //经纬格网
            {
                #region 经纬格网
                //显示选项卡
                this.tabItemMInterval.Visible = false;
                this.tabItemGInterval.Visible = true;
                this.tabItemIndex.Visible = false;

                //间隔
                //计算度分秒
                IMeasuredGrid pMeasureGrid = m_pMapGrid as IMeasuredGrid;
                //单位
                this.cboBoxMIntervalUnit.Items.Add("Centimeters");
                this.cboBoxMIntervalUnit.Items.Add("DecimalDegrees");
                this.cboBoxMIntervalUnit.Items.Add("Decimeters");
                this.cboBoxMIntervalUnit.Items.Add("Feet");
                this.cboBoxMIntervalUnit.Items.Add("Inches");
                this.cboBoxMIntervalUnit.Items.Add("Kilometers");
                this.cboBoxMIntervalUnit.Items.Add("Meters");
                this.cboBoxMIntervalUnit.Items.Add("Miles");
                this.cboBoxMIntervalUnit.Items.Add("Millimeters");
                this.cboBoxMIntervalUnit.Items.Add("NauticalMiles");
                this.cboBoxMIntervalUnit.Items.Add("Points");
                this.cboBoxMIntervalUnit.Items.Add("UnknownUnits");
                this.cboBoxMIntervalUnit.Items.Add("Yards");

                if (pMeasureGrid.Units == esriUnits.esriCentimeters)
                {
                    this.cboBoxMIntervalUnit.Text = "Centimeters";
                }
                else if (pMeasureGrid.Units == esriUnits.esriDecimalDegrees)
                {
                    this.cboBoxMIntervalUnit.Text = "DecimalDegrees";
                }
                else if (pMeasureGrid.Units == esriUnits.esriDecimeters)
                {
                    this.cboBoxMIntervalUnit.Text = "Decimeters";
                }
                else if (pMeasureGrid.Units == esriUnits.esriFeet)
                {
                    this.cboBoxMIntervalUnit.Text = "Feet";
                }
                else if (pMeasureGrid.Units == esriUnits.esriInches)
                {
                    this.cboBoxMIntervalUnit.Text = "Inches";
                }
                else if (pMeasureGrid.Units == esriUnits.esriKilometers)
                {
                    this.cboBoxMIntervalUnit.Text = "Kilometers";
                }
                else if (pMeasureGrid.Units == esriUnits.esriMeters)
                {
                    this.cboBoxMIntervalUnit.Text = "Meters";
                }
                else if (pMeasureGrid.Units == esriUnits.esriMiles)
                {
                    this.cboBoxMIntervalUnit.Text = "Miles";
                }
                else if (pMeasureGrid.Units == esriUnits.esriMillimeters)
                {
                    this.cboBoxMIntervalUnit.Text = "Millimeters";
                }
                else if (pMeasureGrid.Units == esriUnits.esriNauticalMiles)
                {
                    this.cboBoxMIntervalUnit.Text = "NauticalMiles";
                }
                else if (pMeasureGrid.Units == esriUnits.esriPoints)
                {
                    this.cboBoxMIntervalUnit.Text = "Points";
                }
                else if (pMeasureGrid.Units == esriUnits.esriUnknownUnits)
                {
                    this.cboBoxMIntervalUnit.Text = "UnknownUnits";
                }
                else if (pMeasureGrid.Units == esriUnits.esriYards)
                {
                    this.cboBoxMIntervalUnit.Text = "Yards";
                }
                else
                    ;

                double pXInterval = pMeasureGrid.XIntervalSize;
                double pYInterval = pMeasureGrid.YIntervalSize;
                int pXD = 0;
                int pXF = 0;
                double pXM = 0;
                int pYD = 0;
                int pYF = 0;
                double pYM = 0;
                pXD = (int)pXInterval;
                pXF = (int)((pXInterval - pXD) * 60);
                pXM = (double)((pXInterval - pXD - (double)pXF / 60) * 3600);
                this.txtGXIntervalD.Text = pXD.ToString();
                this.txtGXIntervalF.Text = pXF.ToString();
                this.txtGXIntervalM.Text = pXM.ToString();

                pYD = (int)pYInterval;
                pYF = (int)((pYInterval - pYD) * 60);
                pYM = (double)((pYInterval - pYD - (double)pYF / 60) * 3600);
                this.txtGYIntervalD.Text = pYD.ToString();
                this.txtGYIntervalF.Text = pYF.ToString();
                this.txtGYIntervalM.Text = pYM.ToString();

                //原点设计
                if (pMeasureGrid.FixedOrigin) //固定原点
                {
                    this.cBoxGOriginDefine.Checked = true;
                    this.cBoxGOriginFrom.Checked = false;

                    //设置原点
                    pXInterval = pMeasureGrid.XOrigin;
                    pYInterval = pMeasureGrid.YOrigin;//.dbGraticuleOriginX;

                    pXD = (int)pXInterval;
                    pXF = (int)((pXInterval - pXD) * 60);
                    pXM = (double)((pXInterval - pXD - (double)pXF / 60) * 3600);
                    this.txtGXOriginD.Text = pXD.ToString();
                    this.txtGXOriginF.Text = pXF.ToString();
                    this.txtGXOriginM.Text = pXM.ToString();

                    pYD = (int)pYInterval;
                    pYF = (int)((pYInterval - pYD) * 60);
                    pYM = (double)((pYInterval - pYD - (double)pYF / 60) * 3600);
                    this.txtGYOriginD.Text = pYD.ToString();
                    this.txtGYOriginF.Text = pYF.ToString();
                    this.txtGYOriginM.Text = pYM.ToString();
                }
                else
                {
                    this.cBoxGOriginDefine.Checked = false;
                    this.cBoxGOriginFrom.Checked = true;
                    this.txtGXOriginD.Enabled = false;
                    this.txtGXOriginF.Enabled = false;
                    this.txtGXOriginM.Enabled = false;
                    this.txtGYOriginD.Enabled = false;
                    this.txtGYOriginF.Enabled = false;
                    this.txtGYOriginM.Enabled = false;
                }
                #endregion
            }
            else if (m_pMapGrid is IIndexGrid) //索引格网
            {
                #region 索引格网
                //选项卡设置
                this.tabItemMInterval.Visible = false;
                this.tabItemGInterval.Visible = false;
                this.tabItemIndex.Visible = true;

                txtColumnHeadings.Text=string.Empty;
                txtRowHeading.Text=string.Empty;

                IIndexGrid pIndexGrid = m_pMapGrid as IIndexGrid;
                txtColumnHeadings.Text = string.Empty;
                for (int i = 0; i < pIndexGrid.ColumnCount; i++)
                {
                    string szCurrentString = pIndexGrid.get_XLabel(i);
                    this.txtColumnHeadings.Text += (szCurrentString + "\n");
                }

                //string[] szRow = m_gridAttibute.szIndexGridRow;
                txtRowHeading.Text = string.Empty;
                for (int i = 0; i < pIndexGrid.RowCount; i++)
                {
                    string szCurrentString = pIndexGrid.get_YLabel(i);
                    this.txtRowHeading.Text += (szCurrentString + "\n");
                }
                    
                this.IndexColumnNumber.Value = pIndexGrid.ColumnCount;//.nIndexGridColCount;
                this.IndexRowColumn.Value = pIndexGrid.RowCount;//.nIndexGridRowCount;
                #endregion
            }
            else
                ;
#endregion
        }

        //读取地图格网的相应属性，并保存
        private void getParaFromMapGrid(IMapGrid pMapGrid)
        {
            if (pMapGrid == null)
                return;

            IClone pSrcClone = pMapGrid as IClone;
            IClone pDstClone = pSrcClone.Clone();
            m_pMapGrid = pDstClone as IMapGrid;

            //bool[] bShowPos=m_gridAttibute.longIndexPos;

            ////长轴
            //pMapGrid.QueryTickVisibility(ref bShowPos[0], ref bShowPos[1], ref bShowPos[2], ref bShowPos[3]);
            //m_gridAttibute.longIndexLineSymbol=pMapGrid.TickLineSymbol as ILineSymbol;
            //m_gridAttibute.longIndexLength = Math.Abs(pMapGrid.TickLength);
            //if (pMapGrid.TickLength > 0)
            //    m_gridAttibute.longIndexOutside = true;
            //else
            //    m_gridAttibute.longIndexOutside = false;
            
            ////短轴
            //bShowPos = m_gridAttibute.shortIndexPos;
            //pMapGrid.QuerySubTickVisibility(ref bShowPos[0], ref bShowPos[1], ref bShowPos[2], ref bShowPos[3]);
            //m_gridAttibute.shortIndexLineSymbol = pMapGrid.SubTickLineSymbol as ILineSymbol;
            //m_gridAttibute.shortIndexLength = Math.Abs(pMapGrid.SubTickLength);
            //if (pMapGrid.SubTickLength > 0)
            //    m_gridAttibute.shortIndexOutside = true;
            //else
            //    m_gridAttibute.shortIndexOutside = false;
            //m_gridAttibute.nShortIndexCount = pMapGrid.SubTickCount;

            ////边框
            //m_gridAttibute.mapGridBorder = pMapGrid.Border;

            ////标注
            //bShowPos = m_gridAttibute.annotationPos;
            //pMapGrid.QueryLabelVisibility(ref bShowPos[0], ref bShowPos[1], ref bShowPos[2], ref bShowPos[3]);
            //m_gridAttibute.annotationGridLabel = pMapGrid.LabelFormat;
            //m_gridAttibute.annotationLineSymbol = pMapGrid.LineSymbol;
            //m_gridAttibute.annotationMarkerSymbol = pMapGrid.TickMarkSymbol;
            
            ////线
            //m_gridAttibute.annotationLineSymbol=pMapGrid.LineSymbol;
            //m_gridAttibute.annotationMarkerSymbol = pMapGrid.TickMarkSymbol;

            ////公里格网的间隔
            //if (pMapGrid.Name.Substring(0, 2) == "公里")
            //{
            //    IMeasuredGrid pMeasuredGrid=pMapGrid as IMeasuredGrid;
            //    m_gridAttibute.bIsMeasureGrid = true;
            //    m_gridAttibute.bIsGraticuleGrid = false;
            //    m_gridAttibute.bIsIndexGrid = false;

            //    m_gridAttibute.dbMeasureGridIntervalX = pMeasuredGrid.XIntervalSize;
            //    m_gridAttibute.dbMeasureGridIntervalY = pMeasuredGrid.YIntervalSize;
            //    m_gridAttibute.unitsMeasureGrid = pMeasuredGrid.Units;

            //    m_gridAttibute.bMeasureGridIsFixOrigin = pMeasuredGrid.FixedOrigin;
            //    m_gridAttibute.dbMeasureGridOriginX = pMeasuredGrid.XOrigin;
            //    m_gridAttibute.dbMeasureGridOriginY = pMeasuredGrid.YOrigin;                
            //}
            //else if (pMapGrid.Name.Substring(0, 2) == "经纬")
            //{
            //    IGraticule pGraticule = pMapGrid as IGraticule;
            //    IMeasuredGrid pMeasuredGrid = pGraticule as IMeasuredGrid;

            //    m_gridAttibute.bIsMeasureGrid = false;
            //    m_gridAttibute.bIsGraticuleGrid = true;
            //    m_gridAttibute.bIsIndexGrid = false;

            //    m_gridAttibute.dbGraticuleIntervalX = pMeasuredGrid.XIntervalSize;
            //    m_gridAttibute.dbGraticuleIntervalY = pMeasuredGrid.YIntervalSize;

            //    m_gridAttibute.bGraticuleGridIsFixOrigin = pMeasuredGrid.FixedOrigin;
            //    m_gridAttibute.dbGraticuleOriginX = pMeasuredGrid.XOrigin;
            //    m_gridAttibute.dbGraticuleOriginY = pMeasuredGrid.YOrigin;
            //}
            //else if (pMapGrid.Name.Substring(0, 2) == "索引")
            //{
            //     IIndexGrid  pIndexGrid =pMapGrid as IIndexGrid ;

            //     m_gridAttibute.bIsMeasureGrid = false;
            //     m_gridAttibute.bIsGraticuleGrid = false;
            //     m_gridAttibute.bIsIndexGrid = true;

            //    m_gridAttibute.nIndexGridRowCount = pIndexGrid.RowCount;
            //    m_gridAttibute.nIndexGridColCount = pIndexGrid.ColumnCount;

            //    m_gridAttibute.szIndexGridRow=new string[m_gridAttibute.nIndexGridRowCount];
            //    m_gridAttibute.szIndexGridColumn = new string[m_gridAttibute.nIndexGridColCount];
            //    for (int i = 0; i < m_gridAttibute.nIndexGridRowCount; i++)
            //    {
            //        m_gridAttibute.szIndexGridRow[i] = pIndexGrid.get_YLabel(i);
            //    }

            //    for (int i = 0; i < m_gridAttibute.nIndexGridColCount; i++)
            //    {
            //        m_gridAttibute.szIndexGridColumn[i] = pIndexGrid.get_XLabel(i);
            //    }                    
            //}
            //else
            //    ;

            //更新界面
            updateUI();
        }

        //利用现成的参数生成相应的格网类，并返回
//        private void setParaToMapGrid()
//        {
//            //生成相应的实例对象
//            if (m_gridAttibute.bIsMeasureGrid) //公里格网
//            {
//                m_pMapGrid=new MeasuredGridClass() as IMapGrid;
//            }
//            else if (m_gridAttibute.bIsGraticuleGrid) //经纬格网
//            {
//                m_pMapGrid = new GraticuleClass() as IMapGrid;
//            }
//            else if (m_gridAttibute.bIsIndexGrid) //索引格网
//            {
//                m_pMapGrid = new IndexGridClass() as IMapGrid;
//            }
//            else
//            {
//                m_pMapGrid = null;
//                return;
//            }

//            //给实例对象赋值
//#region 长轴
//            bool[] bShowPos=m_gridAttibute.longIndexPos;
//            m_pMapGrid.SetTickVisibility(bShowPos[0],bShowPos[1],bShowPos[2],bShowPos[3]);
//            m_pMapGrid.TickLength=m_gridAttibute.longIndexLength;
//            m_pMapGrid.TickLineSymbol=m_gridAttibute.longIndexLineSymbol;
//#endregion

//#region 短轴
//            bShowPos=m_gridAttibute.shortIndexPos;
//            m_pMapGrid.SetSubTickVisibility(bShowPos[0],bShowPos[1],bShowPos[2],bShowPos[3]);
//            m_pMapGrid.SubTickLength=m_gridAttibute.shortIndexLength;
//            m_pMapGrid.SubTickLineSymbol=m_gridAttibute.shortIndexLineSymbol;
//            m_pMapGrid.SubTickCount = Convert.ToInt16(m_gridAttibute.nShortIndexCount);            
//#endregion

//#region 边框
//            m_pMapGrid.Border = m_gridAttibute.mapGridBorder;
//#endregion

//#region 标注
//            m_pMapGrid.LabelFormat = m_gridAttibute.annotationGridLabel;
//            m_pMapGrid.LineSymbol = m_gridAttibute.annotationLineSymbol;
//            m_pMapGrid.TickMarkSymbol = m_gridAttibute.annotationMarkerSymbol;
//#endregion

//#region 不同类型的格网有不同的属性参数
//            if (m_gridAttibute.bIsMeasureGrid)
//            {
//                IMeasuredGrid pMeasureGrid = m_pMapGrid as IMeasuredGrid;

//                pMeasureGrid.XIntervalSize=m_gridAttibute.dbMeasureGridIntervalX;
//                pMeasureGrid.YIntervalSize=m_gridAttibute.dbMeasureGridIntervalY;
//                pMeasureGrid.XOrigin=m_gridAttibute.dbMeasureGridOriginX;
//                pMeasureGrid.YOrigin=m_gridAttibute.dbMeasureGridOriginY;
//                pMeasureGrid.Units=m_gridAttibute.unitsMeasureGrid;
//                pMeasureGrid.FixedOrigin=m_gridAttibute.bMeasureGridIsFixOrigin;
//            }
//            else if (m_gridAttibute.bIsGraticuleGrid)
//            {
//                IGraticule pGraticule=m_pMapGrid as IGraticule;
//                IMeasuredGrid pMeasureGrid = pGraticule as IMeasuredGrid;

//                pMeasureGrid.XIntervalSize=m_gridAttibute.dbGraticuleIntervalX;
//                pMeasureGrid.YIntervalSize=m_gridAttibute.dbGraticuleIntervalY;
//                pMeasureGrid.XOrigin=m_gridAttibute.dbGraticuleOriginX;
//                pMeasureGrid.YOrigin=m_gridAttibute.dbGraticuleOriginY;
//                pMeasureGrid.FixedOrigin=m_gridAttibute.bGraticuleGridIsFixOrigin;
//            }
//            else if (m_gridAttibute.bIsIndexGrid)
//            {
//                IIndexGrid pIndexGrid=m_pMapGrid as IIndexGrid;
//                pIndexGrid.ColumnCount=m_gridAttibute.nIndexGridColCount;
//                pIndexGrid.RowCount=m_gridAttibute.nIndexGridRowCount;
                
//                for(int i=0;i<pIndexGrid.ColumnCount;i++)
//                    pIndexGrid.set_XLabel(i, m_gridAttibute.szIndexGridColumn[i]);

//                for(int i=0;i<pIndexGrid.RowCount;i++)
//                    pIndexGrid.set_YLabel(i,m_gridAttibute.szIndexGridRow[i]);
//            }
//            else
//                ;
//#endregion

//        }
    
        public FrmGridsAttribute(string[] symbolstyle,IMapGrid mapgrid)
        {
            InitializeComponent();
            this.EnableGlass = false;
            SymbolStyle = symbolstyle;
            //pMapGrid = mapgrid;

            //将传进来的MAPGRID相应参数保存到clsgridAttribute类中
            getParaFromMapGrid(mapgrid);
        }

        private void FrmGridsAttribute_Load(object sender, EventArgs e)
        {
            if (m_pMapGrid != null)
                updateUI();
        }
       
        //确定按钮
        private void btOK_Click(object sender, EventArgs e)
        {
            try
            {
                updateFromUI();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.StackTrace);
            }
            finally
            {
                GC.Collect();
                this.Close();
            }
            //setParaToMapGrid();
        }
     
        //取消按钮
        private void btCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

         //长轴样式
        private void btDivisionSymbol_Click(object sender, EventArgs e)
        {
            ILineSymbol pLineSymbol = m_pMapGrid.TickLineSymbol;//.longIndexLineSymbol;
            FrmSymbol LongAxeSymbol = new FrmSymbol(SymbolStyle, (ISymbol)pLineSymbol, esriSymbologyStyleClass.esriStyleClassLineSymbols);
            if (LongAxeSymbol.ShowDialog() == DialogResult.OK)
            {
                IStyleGalleryItem pStyleGalleryItem = LongAxeSymbol.GetStyleGalleryItem();
                if (pStyleGalleryItem != null)
                {
                    m_pMapGrid.TickLineSymbol = pStyleGalleryItem.Item as ILineSymbol;
                    this.btDivisionSymbol.Image = LongAxeSymbol.GetImageByGiveSymbolAfterSelectItem(btDivisionSymbol.Width, btDivisionSymbol.Height);
                }
            }
        }
      
        //短轴样式
        private void btSubDivisionSymbol_Click(object sender, EventArgs e)
        {
            ILineSymbol pLineSymbol = m_pMapGrid.SubTickLineSymbol;//.shortIndexLineSymbol;
            FrmSymbol ShortAxeSymbol = new FrmSymbol(SymbolStyle, (ISymbol)pLineSymbol, esriSymbologyStyleClass.esriStyleClassLineSymbols);

            if (ShortAxeSymbol.ShowDialog() == DialogResult.OK)
            {
                IStyleGalleryItem pStyleGalleryItem = ShortAxeSymbol.GetStyleGalleryItem();
                if (pStyleGalleryItem != null)
                {
                    m_pMapGrid.SubTickLineSymbol = pStyleGalleryItem.Item as ILineSymbol;
                    this.btSubDivisionSymbol.Image = ShortAxeSymbol.GetImageByGiveSymbolAfterSelectItem(btSubDivisionSymbol.Width, btSubDivisionSymbol.Height);
                }
            }
        }
     
        //边框样式
        private void btBorder_Click(object sender, EventArgs e)
        {
            if (this.cbBoxBoder.Text == "简单边框")
            {
                ILineSymbol pLineSymbol=null;
                ISimpleMapGridBorder pSimpleMapGridBorder=m_pMapGrid.Border as ISimpleMapGridBorder;
                if (pSimpleMapGridBorder != null)
                    pLineSymbol = pSimpleMapGridBorder.LineSymbol;
                else
                {
                    IGraphicProperties m_GraphicProperties = new CommandsEnvironmentClass();
                    pLineSymbol = m_GraphicProperties.LineSymbol;
                }                
                
                FrmSymbol pSimpleLine = new FrmSymbol(SymbolStyle, (ISymbol)pLineSymbol, esriSymbologyStyleClass.esriStyleClassLineSymbols);
                if (pSimpleLine.ShowDialog() == DialogResult.OK)
                {
                    IStyleGalleryItem pStyleGalleryItem = pSimpleLine.GetStyleGalleryItem();
                    if (pStyleGalleryItem != null)
                    {
                        m_pMapGrid.Border = new SimpleMapGridBorderClass();
                        pSimpleMapGridBorder = m_pMapGrid.Border as ISimpleMapGridBorder;
                        pSimpleMapGridBorder.LineSymbol = pStyleGalleryItem.Item as ILineSymbol;
                    }
                }
            }
            else if (this.cbBoxBoder.Text == "整饰边框")
            {
                FrmCalibGridBorder FrmCali = new FrmCalibGridBorder();
                if (FrmCali.ShowDialog() == DialogResult.OK)
                {
                    m_pMapGrid.Border = FrmCali.GetGridBorder() as IMapGridBorder;
                }
            }
        }

        //内部线样式 LineSymbol
        private void btLineSymbol_Click(object sender, EventArgs e)
        {
            if ( this.cBoxLineGridTick.Checked == true)
            {
                //刻度样式选择
                IMarkerSymbol pMarkerSymbol = m_pMapGrid.TickMarkSymbol;//.annotationMarkerSymbol;
                FrmSymbol TickSymbol = new FrmSymbol(SymbolStyle, (ISymbol)pMarkerSymbol, esriSymbologyStyleClass.esriStyleClassMarkerSymbols);
                if (TickSymbol.ShowDialog() == DialogResult.OK)
                {
                    IStyleGalleryItem pStyleGalleryItem = TickSymbol.GetStyleGalleryItem();
                    if (pStyleGalleryItem != null)
                    {
                        m_pMapGrid.TickMarkSymbol = pStyleGalleryItem.Item as IMarkerSymbol;
                        btLineSymbol.Image = TickSymbol.GetImageByGiveSymbolAfterSelectItem(btLineSymbol.Width, btLineSymbol.Height);
                    }
                }
            }
            else if (this.cBoxLineGridLine.Checked == true)
            {
                //经纬网样式选择
                ILineSymbol pLineSymbol = m_pMapGrid.LineSymbol;//.annotationLineSymbol;
                FrmSymbol LineSymbol = new FrmSymbol(SymbolStyle, (ISymbol)pLineSymbol, esriSymbologyStyleClass.esriStyleClassLineSymbols);
                if (LineSymbol.ShowDialog() == DialogResult.OK)
                {
                    IStyleGalleryItem pStyleGalleryItem = LineSymbol.GetStyleGalleryItem();
                    if (pStyleGalleryItem != null)
                    {
                        m_pMapGrid.LineSymbol = pStyleGalleryItem.Item as ILineSymbol;
                        btLineSymbol.Image = LineSymbol.GetImageByGiveSymbolAfterSelectItem(btLineSymbol.Width, btLineSymbol.Height);
                    }
                }
            }
        }

        ////索引格网 Tab 样式设计
        //private void btIndexTabSymbol_Click(object sender, EventArgs e)
        //{
        //    FrmIndexTabStyle Frm=new FrmIndexTabStyle();
        //    Frm.ShowDialog();
        //    if (Frm.DialogResult == DialogResult.OK)
        //    {
        //        pForegroundColor = Frm.pForeground; ;
        //        pOutlineColor = Frm.pOutline ;
        //        pTickness = Frm.pTick;
        //    }
        //}

        //索引格网行列设计


        //1：A 2：B …………26：Z
        

        private void IndexColumnNumber_ValueChanged(object sender, EventArgs e)
        {
            int nColumnCount = this.IndexColumnNumber.Value;
            if (nColumnCount <= 0)
                return;

            //比当前列数少
            IIndexGrid pIndexGrid = m_pMapGrid as IIndexGrid;
            if (nColumnCount <= pIndexGrid.ColumnCount)
            {
                pIndexGrid.ColumnCount = nColumnCount;
                //string[] szStringList = new string[nColumnCount];

                ////复制前nRowCount个标签
                //for (int i = 0; i < nColumnCount; i++)
                //    szStringList[i] = pIndexGrid.get_XLabel(i);//.szIndexGridColumn[i];

                //m_gridAttibute.szIndexGridColumn = szStringList;
            }
            else //比当前行数多
            {
                //string[] szStringList = new string[nColumnCount];

                //复制原来的标签
                //for (int i = 0; i < pIndexGrid.ColumnCount; i++)
                //    szStringList[i] = m_gridAttibute.szIndexGridColumn[i];

                //增加新的标签
                for (int i = pIndexGrid.ColumnCount; i < nColumnCount; i++)
                    pIndexGrid.set_XLabel(i,CommonProcess.indexToString(i+1));// (Convert.ToChar('A' + i)).ToString();

                //m_gridAttibute.szIndexGridColumn = szStringList;
                pIndexGrid.ColumnCount=nColumnCount;
            }

            //string[] szColumn = m_gridAttibute.szIndexGridColumn;
            txtColumnHeadings.Text=string.Empty;
            for (int i = 0; i < pIndexGrid.ColumnCount; i++)
            {
                this.txtColumnHeadings.Text += (pIndexGrid.get_XLabel(i) + "\n");
            }         

        }

        private void IndexRowColumn_ValueChanged(object sender, EventArgs e)
        {
            int nRowCount = this.IndexRowColumn.Value;
            if (nRowCount <= 0)
                return;
            
            //比当前行数少
            IIndexGrid pIndexGrid = m_pMapGrid as IIndexGrid;
            if (nRowCount <= pIndexGrid.RowCount/*.nIndexGridRowCount*/)
            {
                pIndexGrid.RowCount = nRowCount;
                //string[] szStringList = new string[nRowCount];
                
                ////复制前nRowCount个标签
                //for (int i = 0; i < nRowCount; i++)
                //    szStringList[i] = m_gridAttibute.szIndexGridRow[i];

                //m_gridAttibute.szIndexGridRow = szStringList;
            }
            else //比当前行数多
            {
                string[] szStringList = new string[nRowCount];

                ////复制原来的标签
                //for (int i = 0; i < m_gridAttibute.nIndexGridRowCount; i++)
                //    szStringList[i] = m_gridAttibute.szIndexGridRow[i];

                //增加新的标签
                for (int i = pIndexGrid.RowCount; i < nRowCount; i++)
                    pIndexGrid.set_YLabel(i,(i+1).ToString());

                // m_gridAttibute.szIndexGridRow = szStringList;
                pIndexGrid.RowCount = nRowCount;
            }

            //string[] szRow = m_gridAttibute.szIndexGridRow;
            txtRowHeading.Text = string.Empty;
            for (int i = 0; i < pIndexGrid.RowCount/*.nIndexGridRowCount*/; i++)
            {
                this.txtRowHeading.Text += (pIndexGrid.get_YLabel(i) + "\n");
            }
        }

        //经纬网和公里格网点线设计
        private void cBoxLineGridLine_CheckedChanged(object sender, EventArgs e)
        {
            this.btLineSymbol.Enabled = true;

            ILineSymbol pLineSymbol = m_pMapGrid.LineSymbol;//.annotationLineSymbol;
            if (pLineSymbol != null)
            {
                //FrmSymbol FrmSymbolss = new FrmSymbol(SymbolStyle, (ISymbol)pLineSymbol, esriSymbologyStyleClass.esriStyleClassLineSymbols);

                if (btLineSymbol.Image != null)
                    btLineSymbol.Image.Dispose();

                //this.btLineSymbol.Image = FrmSymbolss.GetImageByGiveSymbolBeforeSelectItem(esriSymbologyStyleClass.esriStyleClassLineSymbols, (ISymbol)pLineSymbol, btLineSymbol.Width, btLineSymbol.Height);
                this.btLineSymbol.Image = CommonProcess.getImageFromSymbol((ISymbol)pLineSymbol, btLineSymbol.Width, btLineSymbol.Height);
            }
            else
            {
                IGraphicProperties m_GraphicProperties = new CommandsEnvironmentClass();
                //FrmSymbol FrmSymbolss = new FrmSymbol(SymbolStyle, (ISymbol)m_GraphicProperties.LineSymbol, esriSymbologyStyleClass.esriStyleClassLineSymbols);

                if (btLineSymbol.Image != null)
                    btLineSymbol.Image.Dispose();


                //this.btLineSymbol.Image = FrmSymbolss.GetImageByGiveSymbolBeforeSelectItem(esriSymbologyStyleClass.esriStyleClassLineSymbols, (ISymbol)m_GraphicProperties.LineSymbol, btLineSymbol.Width, btLineSymbol.Height);
                this.btLineSymbol.Image = CommonProcess.getImageFromSymbol((ISymbol)m_GraphicProperties.LineSymbol, btLineSymbol.Width, btLineSymbol.Height);
             //   pLineSymbol = m_GraphicProperties.LineSymbol as ILineSymbol;
            }
        }

        //显示格网刻度
        private void cBoxLineGridTick_CheckedChanged(object sender, EventArgs e)
        {
            this.btLineSymbol.Enabled = true;

            IMarkerSymbol pMarkerSymbol = m_pMapGrid.TickMarkSymbol;//.annotationMarkerSymbol;
            if (pMarkerSymbol != null)
            {
               // FrmSymbol FrmSymbolss = new FrmSymbol(SymbolStyle, (ISymbol)pMarkerSymbol, esriSymbologyStyleClass.esriStyleClassMarkerSymbols);

                if (btLineSymbol.Image != null)
                    btLineSymbol.Image.Dispose();


                //this.btLineSymbol.Image = FrmSymbolss.GetImageByGiveSymbolBeforeSelectItem(esriSymbologyStyleClass.esriStyleClassMarkerSymbols, (ISymbol)pMarkerSymbol, btLineSymbol.Width, btLineSymbol.Height);
                this.btLineSymbol.Image = CommonProcess.getImageFromSymbol((ISymbol)pMarkerSymbol, btLineSymbol.Width, btLineSymbol.Height);
            }
            else
            {
                IGraphicProperties m_GraphicProperties = new CommandsEnvironmentClass();
                //FrmSymbol FrmSymbolss = new FrmSymbol(SymbolStyle, (ISymbol)m_GraphicProperties.MarkerSymbol, esriSymbologyStyleClass.esriStyleClassMarkerSymbols);

                if (btLineSymbol.Image != null)
                    btLineSymbol.Image.Dispose();

                //this.btLineSymbol.Image = FrmSymbolss.GetImageByGiveSymbolBeforeSelectItem(esriSymbologyStyleClass.esriStyleClassMarkerSymbols, (ISymbol)m_GraphicProperties.MarkerSymbol, btLineSymbol.Width, btLineSymbol.Height);
                this.btLineSymbol.Image = CommonProcess.getImageFromSymbol((ISymbol)m_GraphicProperties.MarkerSymbol, btLineSymbol.Width, btLineSymbol.Height);
                //m_gridAttibute.annotationMarkerSymbol = m_GraphicProperties.MarkerSymbol as IMarkerSymbol;
            }
        }

        private void cBoxLineGridNull_CheckedChanged(object sender, EventArgs e)
        {
            this.btLineSymbol.Image = null;
            this.btLineSymbol.Enabled = false;
        }

        //经纬网和公里格网原点设计选择
        private void cBoxGOriginFrom_CheckedChanged(object sender, EventArgs e)
        {
            if (cBoxGOriginFrom.Checked == true)
            {
                this.cBoxGOriginFrom.Checked =true;
                this.cBoxGOriginDefine.Checked = false;
                this.cBoxGOriginFrom.Checked = true;
                this.txtGXOriginD.Enabled = false;
                this.txtGXOriginF.Enabled = false;
                this.txtGXOriginM.Enabled = false;
                this.txtGYOriginD.Enabled = false;
                this.txtGYOriginF.Enabled = false;
                this.txtGYOriginM.Enabled = false;   
            }
        }

        private void cBoxGOriginDefine_CheckedChanged(object sender, EventArgs e)
        {
            if (cBoxGOriginDefine.Checked == true)
            {
                this.cBoxGOriginDefine.Checked = true;
                this.cBoxGOriginFrom.Checked = false;
                this.txtGXOriginD.Enabled = true;
                this.txtGXOriginF.Enabled = true;
                this.txtGXOriginM.Enabled = true;
                this.txtGYOriginD.Enabled = true;
                this.txtGYOriginF.Enabled = true;
                this.txtGYOriginM.Enabled = true;   
            }
        }

        private void cBoxMOriginFrom_CheckedChanged(object sender, EventArgs e)
        {
            if (cBoxMOriginFrom.Checked == true)
            {
                this.cBoxMOriginFrom.Checked = true;
                this.cBoxMOriginDefine.Checked = false;
                this.txtMXOrigin.Enabled = false;
                this.txtMYOrigin.Enabled = false; 
            }
        }

        private void cBoxMOriginDefine_CheckedChanged(object sender, EventArgs e)
        {
            if (cBoxMOriginDefine.Checked == true)
            {
                this.cBoxMOriginFrom.Checked = false;
                this.cBoxMOriginDefine.Checked = true;
                this.txtMXOrigin.Enabled = true;
                this.txtMYOrigin.Enabled = true;
            }
        }

        private void gPanelBorder_Click(object sender, EventArgs e)
        {

        }

        private void cbBoxLabelFormat_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        private void chkShowMinusSign_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        //private void cbBoxBoder_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if(cbBoxBoder.SelectedItem==cbItemSimple)
        //    {

        //    }
        //    else if (cbBoxBoder.SelectedItem == cbItemCalibrate)
        //    {

        //    }
        //    else
        //        ;
        //}

    }


    public class CommonProcess
    {
        static public Image getImage(IStyleGalleryClass pStyleGalleryClass, ISymbol pSymbol, int nWidth, int nHeight)
        {
            Image pImage = new Bitmap(nWidth, nHeight);
            Graphics gc = Graphics.FromImage(pImage);
            IntPtr hdc = gc.GetHdc();

            var rect = new tagRECT();
            rect.left = 0;
            rect.top = 0;
            rect.right = nWidth;
            rect.bottom = nHeight;

            pStyleGalleryClass.Preview(pSymbol, hdc.ToInt32(), ref rect);
            gc.ReleaseHdc(hdc);
            gc.Dispose();

            return pImage;
        }

        //从SYMBOL获得预览图
        static public Image getImageFromSymbol(ISymbol pSymbol, int nWidth, int nHeight)
        {
            if (pSymbol == null)
                return null;

            IStyleGalleryClass pStyleGalleryClass = null;
            if (pSymbol is IMarkerSymbol)
            {
                pStyleGalleryClass = new MarkerSymbolStyleGalleryClass();
            }
            else if (pSymbol is ILineSymbol)
            {
                pStyleGalleryClass = new LineSymbolStyleGalleryClass();
            }
            else if (pSymbol is IFillSymbol)
            {
                pStyleGalleryClass = new FillSymbolStyleGalleryClassClass();
            }
            else
            {
                pStyleGalleryClass = null;
            }

            if (pStyleGalleryClass != null)
            {
                return getImage(pStyleGalleryClass, pSymbol, nWidth, nHeight);
            }

            return null;
        }

        static public char indexToChar(int nIndex)
        {
            if (nIndex < 0 || nIndex > 26)
                return Convert.ToChar(string.Empty);

            return Convert.ToChar('A' + nIndex - 1);
        }

        static public string indexToString(int nIndex)
        {
            if (nIndex <= 0)
                return string.Empty;

            string szResult = string.Empty;
            while (nIndex > 26)
            {
                int nCurrentDigit = nIndex % 26;
                nIndex /= 26;

                if (nCurrentDigit == 0)
                {
                    nCurrentDigit += 26;
                    nIndex -= 1;
                }
                szResult = indexToChar(nCurrentDigit) + szResult;
            }

            szResult = indexToChar(nIndex) + szResult;
            return szResult;
        }
    }
    //public class ClsGridAttibute
    //{
    //    ///刻度轴
    //    //长轴刻度
    //    public bool[] longIndexPos = new bool[4]; //显示位置, 左上右下
    //    public bool longIndexOutside = true;    //在外部显示刻度
    //    public double longIndexLength = 5;      //刻度长度
    //    public ILineSymbol longIndexLineSymbol = null;  //刻度样式

    //    //短轴刻度
    //    public bool[] shortIndexPos = new bool[4]; //显示位置
    //    public bool shortIndexOutside = true;    //在外部显示刻度
    //    public double shortIndexLength = 5;      //刻度长度
    //    public ILineSymbol shortIndexLineSymbol = null;  //刻度样式
    //    public int nShortIndexCount = 5;            //短轴数目

    //    //边框
    //    public IMapGridBorder mapGridBorder = null;

    //    ///标注
    //    public bool[] annotationPos = new bool[4]; //显示位置
    //    public IGridLabel annotationGridLabel = null;    //标注格式
    //    //public IIndexGridTabStyle annnotationIndexGridTabType = null; //索引样式，只在索引格网中可用

    //    //格网线和格网刻度属性
    //    public IMarkerSymbol annotationMarkerSymbol = null;
    //    public ILineSymbol annotationLineSymbol = null;

    //    //公里格网间隔, 只在公里格网时可用
    //    public bool bIsMeasureGrid = false;
    //    public double dbMeasureGridIntervalX = double.MinValue;  //以规定的单位为准
    //    public double dbMeasureGridIntervalY = double.MinValue;
    //    public esriUnits unitsMeasureGrid = esriUnits.esriMeters;
    //    public double dbMeasureGridOriginX = double.MinValue;
    //    public double dbMeasureGridOriginY = double.MinValue;
    //    public bool bMeasureGridIsFixOrigin = false;    //是否固定原点

    //    //经纬格网间隔, 只在经纬格网时可用
    //    public bool bIsGraticuleGrid = false;
    //    public double dbGraticuleIntervalX = double.MinValue;  //以度为单位
    //    public double dbGraticuleIntervalY = double.MinValue;
    //    public double dbGraticuleOriginX = double.MinValue;
    //    public double dbGraticuleOriginY = double.MinValue;
    //    public bool bGraticuleGridIsFixOrigin = false;    //是否固定原点

    //    //索引格网，只在索引格网时可用
    //    public bool bIsIndexGrid = false;
    //    public int nIndexGridRowCount = 0;
    //    public int nIndexGridColCount = 0;
    //    public string[] szIndexGridColumn = null;
    //    public string[] szIndexGridRow = null;
    //}
}
