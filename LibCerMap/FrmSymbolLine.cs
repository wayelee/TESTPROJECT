using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;
using System.IO;

using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Analyst3D;

using DevComponents.DotNetBar;
using DevComponents.DotNetBar.SuperGrid;

namespace LibCerMap
{
    public partial class FrmSymbolLine : OfficeForm
    {
        //当前listbox选择项索引
        int listBoxSelectIndex = -1;
        //符号库文件名
        public string fileName = ClsGDBDataCommon.GetParentPathofExe() + @"Resource\Style\ESRI.ServerStyle";
        //当前样式类索引
        public int CurrentStyleGalleryClassIndex;
        //缩放比例
        float Radio = 1;

        List<double> rewidth = new List<double>();
        List<string> istype = new List<string>();

        //制图线\哈希线的CAP\JOIN选择
        string curCatCap = "Butt";
        string curCatJoin = "Round";
        string curHashCap = "Butt";
        string curHashJoin = "Round";

        List<Image> imagelist = new List<Image>();
        List<ISymbol> symbollist = new List<ISymbol>();
        List<ISimpleLineSymbol> simplelinelist = new List<ISimpleLineSymbol>();
        List<ICartographicLineSymbol> graphiclinelist = new List<ICartographicLineSymbol>();
        List<IHashLineSymbol> hashlinelist = new List<IHashLineSymbol>();
        List<ISimpleLine3DSymbol> sim3dlinelist = new List<ISimpleLine3DSymbol>();
        object temp = null;
        ISymbologyStyleClass psymbologyStyleClass = null;
        IMultiLayerLineSymbol pMultiLineSymbol = new MultiLayerLineSymbolClass();

        ISymbol pSymbolOK = null;

        public FrmSymbolLine(ISymbologyStyleClass symbologyStyleClass)
        {
            InitializeComponent();
            this.EnableGlass = false;
            psymbologyStyleClass = symbologyStyleClass;
        }

        private void FrmSymbolLine_Load(object sender, EventArgs e)
        {
            cmbstyle.SelectedIndex = 0;
            //psymbologyStyleClass = pSymbologyControl.GetStyleClass(pSymbologyControl.StyleClass); 
        }

        private void btnadd_Click(object sender, EventArgs e)
        {         
            if(superTabControl1.SelectedTabIndex==0)//简单线
            {
                ISimpleLineSymbol simpleLineSymbol = createSimpleLine(colorsimple.SelectedColor, cbSimpleStyle.Text, double.Parse(widthsimple.Text.ToString()));
                addListBoxItem(simpleLineSymbol as ISymbol);
            }

            else if (superTabControl1.SelectedTabIndex==1)
            {
                checkRadioButton();
                ICartographicLineSymbol cartographicLineSymbol = createCartographicLine(colorgraphic.SelectedColor, widthgraphic.Value, offsetgraphic.Value, curCatCap, curCatJoin);
                addListBoxItem(cartographicLineSymbol as ISymbol);
            }

            else if(superTabControl1.SelectedTabIndex==2)
            {
                checkRadioButton();
                IHashLineSymbol hashLineSymbol = createHashLine(colorhash.SelectedColor, widthhash.Value, anglehash.Value, offsethash.Value, curHashCap, curHashJoin);
                addListBoxItem(hashLineSymbol as ISymbol);
            }
            else if (superTabControl1.SelectedTabIndex == 3)
            {
                ISimpleLine3DSymbol simline3dsymbol = new SimpleLine3DSymbolClass();
                if (cmbstyle.SelectedIndex==0)
                {
                    simline3dsymbol=creatline3Dsymbol(color3d.SelectedColor, esriSimple3DLineStyle.esriS3DLSTube, width3d.Value, sliderquality.Value / 100);
                } 
                else if(cmbstyle.SelectedIndex==1)
                {
                    simline3dsymbol=creatline3Dsymbol(color3d.SelectedColor, esriSimple3DLineStyle.esriS3DLSStrip, width3d.Value, sliderquality.Value / 100);
                }
                else
                {
                    simline3dsymbol=creatline3Dsymbol(color3d.SelectedColor, esriSimple3DLineStyle.esriS3DLSWall, width3d.Value, sliderquality.Value / 100);
                }
                addListBoxItem(simline3dsymbol as ISymbol);
            }


            RePainPictureBox(Radio);

        }

        //添加GLISTBOX项
        private void addListBoxItem(ISymbol symbol)
        {

            IStyleGallery styleGallery;
            IStyleGalleryItem styleGalleryItem;
            IStyleGalleryStorage styleGalleryStorge;

            styleGalleryItem = new ServerStyleGalleryItemClass();
            styleGalleryItem.Name = this.txtSymbolName.Text;
            styleGalleryItem.Category = "default";
            object objSymbol = symbol;
            styleGalleryItem.Item = objSymbol;

            styleGallery = new ServerStyleGalleryClass();
            styleGalleryStorge = styleGallery as IStyleGalleryStorage;
            styleGalleryStorge.TargetFile = fileName;

            //IStyleGalleryClass styleGalleryClass = styleGallery.get_Class(CurrentStyleGalleryClassIndex);
            ISymbologyStyleClass pSymbolClass = psymbologyStyleClass;
            pSymbolClass.AddItem(styleGalleryItem, 0);
            pSymbolClass.SelectItem(0);

            stdole.IPictureDisp pPicture = pSymbolClass.PreviewItem(styleGalleryItem, 140, 15);
            Image image = Image.FromHbitmap(new System.IntPtr(pPicture.Handle));

            GridPanel panel = supergrid.PrimaryGrid;
            GridRow row = new GridRow(image);
            panel.Rows.Add(row);
            psymbologyStyleClass.RemoveItem(0);
            imagelist.Add(image);
            symbollist.Add(symbol);
           
        }

#region 创建简单线

        private ISimpleLineSymbol createSimpleLine(Color color, string style, double width)
        {
            ISimpleLineSymbol simpleLineSymbol = new SimpleLineSymbolClass();
            //设置子样式类型
            switch (style)
            {
                case "Solid":
                case "esriSLSSolid":
                    simpleLineSymbol.Style = esriSimpleLineStyle.esriSLSSolid;
                    break;
                case "Dashed":
                case "esriSLSDash":
                    simpleLineSymbol.Style = esriSimpleLineStyle.esriSLSDash;
                    break;
                case "Dotted":
                case "esriSLSDot":
                    simpleLineSymbol.Style = esriSimpleLineStyle.esriSLSDot;
                    break;
                case "Dash-Dot":
                case "esriSLSDashDot":
                    simpleLineSymbol.Style = esriSimpleLineStyle.esriSLSDashDot;

                    break;
                case "Dash-Dot-Dot":
                case "esriSLSDashDotDot":
                    simpleLineSymbol.Style = esriSimpleLineStyle.esriSLSDashDotDot;
                    break;
                case "null":
                    simpleLineSymbol.Style = esriSimpleLineStyle.esriSLSNull;
                    break;
            }
            simpleLineSymbol.Width = width;
            //IRgbColor rgbColor = getRGB(.R, color.G, color.B);
            simpleLineSymbol.Color = ClsGDBDataCommon.ColorToIColor(color);
            simplelinelist.Add(simpleLineSymbol);
            string typesimple = "issimplesymbol";
            istype.Add(typesimple);
            return simpleLineSymbol;
        }

#endregion

#region 创建制图线

        private ICartographicLineSymbol createCartographicLine(Color color, double width, double offset, string cap, string join)
        {
            ICartographicLineSymbol cartographicLineSymbol = new CartographicLineSymbolClass();
            switch (cap)
            {
                case "Butt":
                    cartographicLineSymbol.Cap = esriLineCapStyle.esriLCSButt;
                    break;
                case "Round":
                    cartographicLineSymbol.Cap = esriLineCapStyle.esriLCSRound;
                    break;
                case "Square":
                    cartographicLineSymbol.Cap = esriLineCapStyle.esriLCSSquare;
                    break;
            }

            switch (join)
            {
                case "Mitre":
                    cartographicLineSymbol.Join = esriLineJoinStyle.esriLJSMitre;
                    break;
                case "Round":
                    cartographicLineSymbol.Join = esriLineJoinStyle.esriLJSRound;
                    break;
                case "Bevel":
                    cartographicLineSymbol.Join = esriLineJoinStyle.esriLJSBevel;
                    break;
            }
            cartographicLineSymbol.Width = width;
            cartographicLineSymbol.MiterLimit = 4;
            ILineProperties lineProperties;
            lineProperties = cartographicLineSymbol as ILineProperties;
            lineProperties.Offset = offset;
            double[] dob = new double[6];
            dob[0] = 0;
            dob[1] = 1;
            dob[2] = 2;
            dob[3] = 3;
            dob[4] = 4;
            dob[5] = 5;
            ITemplate template = new TemplateClass();
            template.Interval = 1;
            for (int i = 0; i < dob.Length; i += 2)
            {
                template.AddPatternElement(dob[i], dob[i + 1]);
            }
            lineProperties.Template = template;
            //IRgbColor rgbColor = getRGB(.R, color.G, color.B);
            cartographicLineSymbol.Color = ClsGDBDataCommon.ColorToIColor(color);
            graphiclinelist.Add(cartographicLineSymbol);
            string typesimple = "isgraphicsymbol";
            istype.Add(typesimple);
            return cartographicLineSymbol;
        }

#endregion

#region 创建哈希线

        private IHashLineSymbol createHashLine(Color color, double width, double angle, double offset, string cap, string join)
        {
            IHashLineSymbol hashLineSymbol = new HashLineSymbolClass();
            ILineProperties lineProperties = hashLineSymbol as ILineProperties;
            lineProperties.Offset = offset;
            double[] dob = new double[6];
            dob[0] = 0;
            dob[1] = 1;
            dob[2] = 2;
            dob[3] = 3;
            dob[4] = 4;
            dob[5] = 5;
            ITemplate template = new TemplateClass();
            //间隔
            template.Interval = 1;
            for (int i = 0; i < dob.Length; i += 2)
            {
                template.AddPatternElement(dob[i], dob[i + 1]);
            }
            lineProperties.Template = template;
            hashLineSymbol.Width = width;
            hashLineSymbol.Angle = angle;
            //hashLineSymbol.HashSymbol = createCartographicLine(color, width, offset, cap, join);
            hashLineSymbol.HashSymbol = createSimpleLine_new(color, "esriSLSDot", width);
            //IRgbColor hashColor = new RgbColor();
            //hashColor = getRGB(.R, color.G, color.B);
            hashLineSymbol.Color = ClsGDBDataCommon.ColorToIColor(color);
            hashlinelist.Add(hashLineSymbol);
            string typesimple = "ishashsymbol";
            istype.Add(typesimple);
            return hashLineSymbol;
        }

#endregion

#region 创建三维线

        private ISimpleLine3DSymbol creatline3Dsymbol(Color color,esriSimple3DLineStyle style,double Width,double quality)
        {
            ISimpleLine3DSymbol pSim3DlineSymbol = new SimpleLine3DSymbolClass();
            ILineSymbol plinesymbol = new SimpleLine3DSymbolClass();
            //IRgbColor rgbColor = getRGB(.R, color.G, color.B);
            plinesymbol.Color = ClsGDBDataCommon.ColorToIColor(color);
            plinesymbol.Width = Width;
            pSim3DlineSymbol = plinesymbol as ISimpleLine3DSymbol;
            pSim3DlineSymbol.ResolutionQuality = quality;
            pSim3DlineSymbol.Style = style;
            sim3dlinelist.Add(pSim3DlineSymbol);
            string typesimple = "is3dlinesymbol";
            istype.Add(typesimple);
            return pSim3DlineSymbol;
        }

#endregion
        
#region 线删除移动等操作

        private void btnremove_Click(object sender, EventArgs e)
        {
            if (supergrid.PrimaryGrid.Rows.Count > 0)
            {
                SelectedElementCollection items = supergrid.PrimaryGrid.GetSelectedRows();
                GridRow rurow = null;
                foreach (GridRow item in items)
                {
                    rurow = item;
                    int n = rurow.Index;
                    supergrid.PrimaryGrid.Rows.Remove(item);
                    if (istype[n] == "issimplesymbol")
                     {
                         int j = 0;
                         for (int i = 0; i < n+1;i++ )
                         {
                             if (istype[i] == "issimplesymbol")
                             {
                                 j++;
                             }       
                         }
                         imagelist.Remove(imagelist[n]);
                         simplelinelist.Remove(simplelinelist[j-1]);
                         istype.Remove(istype[n]);
                         symbollist.Remove(symbollist[n]);
                     }
                    else if (istype[n] == "isgraphicsymbol")
                    {
                        int j=0;
                        for (int i = 0; i < n + 1; i++)
                        {
                            if (istype[i] == "isgraphicsymbol")
                            {
                                j++;
                            }
                        }
                        imagelist.Remove(imagelist[n]);
                        graphiclinelist.Remove(graphiclinelist[j-1]);
                        istype.Remove(istype[n]);
                        symbollist.Remove(symbollist[n]);
                    }
                    else if (istype[n] == "ishashsymbol")
                    {
                        int j = 0;
                        for (int i = 0; i < n + 1; i++)
                        {
                            if (istype[i] == "ishashsymbol")
                            {
                                j++;
                            }
                        }
                        imagelist.Remove(imagelist[n]);
                        hashlinelist.Remove(hashlinelist[j-1]);
                        istype.Remove(istype[n]);
                        symbollist.Remove(symbollist[n]);
                    }

                    else
                    {
                        int j = 0;
                        for (int i = 0; i < n + 1; i++)
                        {
                            if (istype[i] == "is3dlinesymbol")
                            {
                                j++;
                            }
                        }
                        imagelist.Remove(imagelist[n]);
                        sim3dlinelist.Remove(sim3dlinelist[j - 1]);
                        istype.Remove(istype[n]);
                        symbollist.Remove(symbollist[n]);
                    }

                    RePainPictureBox(Radio);
                }
            }
        }

        private void btnup_Click(object sender, EventArgs e)
        {
            if (supergrid.PrimaryGrid.SelectedRowCount > 0)
            {
                if (supergrid.PrimaryGrid.Rows.Count > 1)
                {
                    SelectedElementCollection items = supergrid.PrimaryGrid.GetSelectedRows();
                    GridRow rerow = null;
                    foreach (GridRow item in items)
                    {
                        rerow = item;
                        int n = rerow.Index;
                        if (n != 0)
                        {
                            supergrid.PrimaryGrid.Rows.Remove(item);
                            supergrid.PrimaryGrid.Rows.Insert(n - 1, rerow);
                            Image imagetemp = null ;
                            ISymbol symboltemp = null;
                            ISimpleLineSymbol SLinetemp = new SimpleLineSymbolClass();
                            ICartographicLineSymbol GLinetemp = new CartographicLineSymbolClass();
                            IHashLineSymbol HLinetemp = new HashLineSymbolClass();
                            ISimpleLine3DSymbol TDLinetemp=new SimpleLine3DSymbolClass();
                            string typetemp = null;
                            if (istype[n]==istype[n-1])
                            {
                                if (istype[n] == "issimplesymbol")
                                {
                                    int j = 0;
                                    for (int i = 0; i < n + 1; i++)
                                    {
                                        if (istype[i] == "issimplesymbol")
                                        {
                                            j++;
                                        }
                                    }
                                    SLinetemp = simplelinelist[j-1];
                                    simplelinelist[j-1] = simplelinelist[j - 2];
                                    simplelinelist[j - 2] = SLinetemp;
                                }
                                else if (istype[n] == "isgraphicsymbol")
                                {
                                    int j = 0;
                                    for (int i = 0; i < n + 1; i++)
                                    {
                                        if (istype[i] == "isgraphicsymbol")
                                        {
                                            j++;
                                        }
                                    }
                                    GLinetemp = graphiclinelist[j-1];
                                    graphiclinelist[j-1] = graphiclinelist[j - 2];
                                    graphiclinelist[j -2] = GLinetemp;
                                }
                                else if (istype[n] == "ishashsymbol")
                                {
                                    int j = 0;
                                    for (int i = 0; i < n + 1; i++)
                                    {
                                        if (istype[i] == "ishashsymbol")
                                        {
                                            j++;
                                        }
                                    }
                                    HLinetemp = hashlinelist[j-1];
                                    hashlinelist[j-1] = hashlinelist[j - 2];
                                    hashlinelist[j - 2] = HLinetemp;
                                }
                                else
                                {
                                    int j = 0;
                                    for (int i = 0; i < n + 1; i++)
                                    {
                                        if (istype[i] == "is3dlinesymbol")
                                        {
                                            j++;
                                        }
                                    }
                                    TDLinetemp = sim3dlinelist[j - 1];
                                    sim3dlinelist[j - 1] = sim3dlinelist[j - 2];
                                    sim3dlinelist[j - 2] = TDLinetemp;
                                }
                            }
                            else
                            {
                                typetemp=istype[n];
                                istype[n] = istype[n - 1];
                                istype[n - 1] = typetemp;
                            }
                            imagetemp = imagelist[n];
                            imagelist[n]=imagelist[n - 1];
                            imagelist[n - 1] = imagetemp;
                            symboltemp = symbollist[n];
                            symbollist[n] = symbollist[n - 1];
                            symbollist[n - 1] = symboltemp;
                        }

                        RePainPictureBox(Radio);
                    }
                }
            }
        }

        private void btndown_Click(object sender, EventArgs e)
        {
            if (supergrid.PrimaryGrid.SelectedRowCount > 0)
            {
                if (supergrid.PrimaryGrid.Rows.Count > 1)
                {
                    SelectedElementCollection items = supergrid.PrimaryGrid.GetSelectedRows();
                    GridRow rerow = null;
                    foreach (GridRow item in items)
                    {
                        rerow = item;
                        int n = rerow.Index;
                        if (n != supergrid.PrimaryGrid.Rows.Count - 1)
                        {
                            supergrid.PrimaryGrid.Rows.Remove(item);
                            supergrid.PrimaryGrid.Rows.Insert(n + 1, rerow);
                            Image imagetemp = null;
                            ISymbol symboltemp = null;
                            ISimpleLineSymbol SLinetemp = new SimpleLineSymbolClass();
                            ICartographicLineSymbol GLinetemp = new CartographicLineSymbolClass();
                            IHashLineSymbol HLinetemp = new HashLineSymbolClass();
                            ISimpleLine3DSymbol TDLinetemp = new SimpleLine3DSymbolClass();
                            string typetemp = null;

                            if (istype[n]==istype[n+1])
                            {

                                if (istype[n] == "issimplesymbol")
                                {
                                    int j = 0;
                                    for (int i = 0; i < n + 1; i++)
                                    {
                                        if (istype[i] == "issimplesymbol")
                                        {
                                            j++;
                                        }
                                    }
                                    SLinetemp = simplelinelist[j-1];
                                    simplelinelist[j-1] = simplelinelist[j ];
                                    simplelinelist[j] = SLinetemp;
                                }
                                else if (istype[n] == "isgraphicsymbol")
                                {
                                    int j = 0;
                                    for (int i = 0; i < n + 1; i++)
                                    {
                                        if (istype[i] == "isgraphicsymbol")
                                        {
                                            j++;
                                        }
                                    }
                                    GLinetemp = graphiclinelist[j-1];
                                    graphiclinelist[j-1] = graphiclinelist[j ];
                                    graphiclinelist[j] = GLinetemp;
                                }
                                else if (istype[n] == "ishashsymbol")
                                {
                                    int j = 0;
                                    for (int i = 0; i < n + 1; i++)
                                    {
                                        if (istype[i] == "ishashsymbol")
                                        {
                                            j++;
                                        }
                                    }
                                    HLinetemp = hashlinelist[j-1];
                                    hashlinelist[j-1] = hashlinelist[j ];
                                    hashlinelist[j ] = HLinetemp;
                                }
                                else
                                {
                                    int j = 0;
                                    for (int i = 0; i < n + 1; i++)
                                    {
                                        if (istype[i] == "is3dlinesymbol")
                                        {
                                            j++;
                                        }
                                    }
                                    TDLinetemp = sim3dlinelist[j - 1];
                                    sim3dlinelist[j - 1] = sim3dlinelist[j];
                                    sim3dlinelist[j] = TDLinetemp;
                                }
                            }
                            else
                            {
                                typetemp=istype[n];
                                istype[n] = istype[n + 1];
                                istype[n + 1] = typetemp;
                            }
                            imagetemp = imagelist[n];
                            imagelist[n]=imagelist[n + 1];
                            imagelist[n + 1] = imagetemp;
                            symboltemp = symbollist[n];
                            symbollist[n] = symbollist[n + 1];
                            symbollist[n + 1] = symboltemp;
                        }

                        RePainPictureBox(Radio);
                    }
                }
            }
        }       
#endregion

#region 预览图像操作

        private void RePainPictureBox(float ratio)
        {
            //在pictureBox 中预浏览图象
            Bitmap bitmap = new Bitmap(picturepre.Width, picturepre.Height);
            Graphics graphics = Graphics.FromImage(bitmap);
            System.Drawing.Color color = System.Drawing.Color.FromArgb(0, 0, 0);

            Bitmap image;
            int startX;
            int startY;
            for (int i = 0; i < supergrid.PrimaryGrid.Rows.Count; i++)
            {
                image = (Bitmap)imagelist[i];
                startX = picturepre.Width / 2 - (int)(imagelist[i].Size.Width * ratio) / 2;
                startY = picturepre.Height / 2 - (int)(imagelist[i].Size.Height * ratio) / 2;

                image.MakeTransparent(Color.Transparent);
                System.Drawing.Rectangle rectangle = new Rectangle(startX, startY, (int)(imagelist[i].Size.Width * ratio), (int)(imagelist[i].Size.Height * ratio));
                graphics.DrawImage(imagelist[i], rectangle);
                graphics.Save();
            }
            graphics.Dispose();
            picturepre.Image = bitmap;
        }

        //选择缩放比率
        private void cmbPercent_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strRadio = this.cmbPercent.Text;
            float fradio = float.Parse(strRadio.Substring(0, strRadio.Length - 1));
            Radio = fradio / 100;
            RePainPictureBox(Radio);
        }

#endregion

#region 简单线相关操作

        //改变已建简单线的颜色
        private void colorsimple_SelectedColorChanged(object sender, EventArgs e)
        {
            Color cColor = colorsimple.SelectedColor;
            if (supergrid.PrimaryGrid.SelectedRowCount > 0)
            {
                SelectedElementCollection items = supergrid.PrimaryGrid.GetSelectedRows();
                GridRow crow = null;
                foreach (GridRow item in items)
                {
                    crow = item;
                    int n = crow.Index;
                    if (istype[n] == "issimplesymbol")
                    {
                        int j = 0;
                        for (int i = 0; i < n + 1; i++)
                        {
                            if (istype[i] == "issimplesymbol")
                            {
                                j++;
                            }
                        }

                        simplelinelist[j - 1].Color = ClsGDBDataCommon.ColorToIColor(cColor);
                        ISimpleLineSymbol psimpleLineSymbol = createSimpleLine_new(cColor, simplelinelist[j - 1].Style.ToString(), simplelinelist[j - 1].Width);
                        IStyleGallery styleGallery;
                        IStyleGalleryItem styleGalleryItem;
                        IStyleGalleryStorage styleGalleryStorge;

                        styleGalleryItem = new ServerStyleGalleryItemClass();
                        styleGalleryItem.Name = this.txtSymbolName.Text;
                        styleGalleryItem.Category = "default";
                        object objSymbol = psimpleLineSymbol as ISymbol;
                        styleGalleryItem.Item = objSymbol;

                        styleGallery = new ServerStyleGalleryClass();
                        styleGalleryStorge = styleGallery as IStyleGalleryStorage;
                        styleGalleryStorge.TargetFile = fileName;

                        //IStyleGalleryClass styleGalleryClass = styleGallery.get_Class(CurrentStyleGalleryClassIndex);
                        ISymbologyStyleClass pSymbolClass = psymbologyStyleClass;
                        pSymbolClass.AddItem(styleGalleryItem, 0);
                        pSymbolClass.SelectItem(0);

                        stdole.IPictureDisp pPicture = pSymbolClass.PreviewItem(styleGalleryItem, 140, 15);
                        Image image = Image.FromHbitmap(new System.IntPtr(pPicture.Handle));
                        GridPanel panel = supergrid.PrimaryGrid;
                        GridRow row = new GridRow(image);
                        supergrid.PrimaryGrid.Rows.Remove(item);
                        supergrid.PrimaryGrid.Rows.Insert(n, row);
                        psymbologyStyleClass.RemoveItem(0);
                        imagelist.RemoveAt(n);
                        imagelist.Insert(n, image);
                        symbollist.RemoveAt(n);
                        symbollist.Insert(n, psimpleLineSymbol as ISymbol);


                        RePainPictureBox(Radio);
                    }
                }
            }
        }

        //改变已建线的宽度
        private void widthsimple_ValueChanged(object sender, EventArgs e)
        {
            double mwidth = widthsimple.Value;
            if (supergrid.PrimaryGrid.SelectedRowCount > 0)
            {
                SelectedElementCollection items = supergrid.PrimaryGrid.GetSelectedRows();
                GridRow crow = null;
                foreach (GridRow item in items)
                {
                    crow = item;
                    int n = crow.Index;
                    if (istype[n] == "issimplesymbol")
                    {
                        int j = 0;
                        for (int i = 0; i < n + 1; i++)
                        {
                            if (istype[i] == "issimplesymbol")
                            {
                                j++;
                            }
                        }

                        simplelinelist[j - 1].Width = mwidth;
                        ISimpleLineSymbol psimpleLineSymbol = createSimpleLine_new(ClsGDBDataCommon.IColorToColor(simplelinelist[j - 1].Color), simplelinelist[j - 1].Style.ToString(), mwidth);
                        IStyleGallery styleGallery;
                        IStyleGalleryItem styleGalleryItem;
                        IStyleGalleryStorage styleGalleryStorge;

                        styleGalleryItem = new ServerStyleGalleryItemClass();
                        styleGalleryItem.Name = this.txtSymbolName.Text;
                        styleGalleryItem.Category = "default";
                        object objSymbol = psimpleLineSymbol as ISymbol;
                        styleGalleryItem.Item = objSymbol;

                        styleGallery = new ServerStyleGalleryClass();
                        styleGalleryStorge = styleGallery as IStyleGalleryStorage;
                        styleGalleryStorge.TargetFile = fileName;

                        //IStyleGalleryClass styleGalleryClass = styleGallery.get_Class(CurrentStyleGalleryClassIndex);
                        ISymbologyStyleClass pSymbolClass = psymbologyStyleClass;
                        pSymbolClass.AddItem(styleGalleryItem, 0);
                        pSymbolClass.SelectItem(0);

                        stdole.IPictureDisp pPicture = pSymbolClass.PreviewItem(styleGalleryItem, 140, 15);
                        Image image = Image.FromHbitmap(new System.IntPtr(pPicture.Handle));
                        GridPanel panel = supergrid.PrimaryGrid;
                        GridRow row = new GridRow(image);
                        supergrid.PrimaryGrid.Rows.Remove(item);
                        supergrid.PrimaryGrid.Rows.Insert(n, row);
                        psymbologyStyleClass.RemoveItem(0);

                        imagelist.RemoveAt(n);
                        imagelist.Insert(n, image);
                        symbollist.RemoveAt(n);
                        symbollist.Insert(n, psimpleLineSymbol as ISymbol);

                        RePainPictureBox(Radio);
                    }
                }

            }
        }

        private void cbSimpleStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbSimpleStyle.SelectedIndex == 0)
            {
                widthsimple.Enabled = true;
            }
            else
            {
                widthsimple.Enabled = false;
                widthsimple.Value = 1.00;
            }
        }

#endregion

#region 制图线相关操作

        //改变线颜色
        private void colorgraphic_SelectedColorChanged(object sender, EventArgs e)
        {
            Color cColor = colorgraphic.SelectedColor;
            if (supergrid.PrimaryGrid.SelectedRowCount > 0)
            {
                SelectedElementCollection items = supergrid.PrimaryGrid.GetSelectedRows();
                GridRow crow = null;
                foreach (GridRow item in items)
                {
                    crow = item;
                    int n = crow.Index;
                    if (istype[n] == "isgraphicsymbol")
                    {
                        int j = 0;
                        for (int i = 0; i < n + 1; i++)
                        {
                            if (istype[i] == "isgraphicsymbol")
                            {
                                j++;
                            }
                        }

                        graphiclinelist[j - 1].Color = ClsGDBDataCommon.ColorToIColor(cColor);
                        ILineProperties lineProperties;
                        lineProperties = graphiclinelist[j - 1] as ILineProperties;

                        ICartographicLineSymbol cartographicLineSymbol = createCartographicLine_new(colorgraphic.SelectedColor, graphiclinelist[j - 1].Width, lineProperties.Offset, graphiclinelist[j - 1].Cap.ToString(), graphiclinelist[j - 1].Join.ToString());

                        IStyleGallery styleGallery;
                        IStyleGalleryItem styleGalleryItem;
                        IStyleGalleryStorage styleGalleryStorge;

                        styleGalleryItem = new ServerStyleGalleryItemClass();
                        styleGalleryItem.Name = this.txtSymbolName.Text;
                        styleGalleryItem.Category = "default";
                        object objSymbol = cartographicLineSymbol as ISymbol;
                        styleGalleryItem.Item = objSymbol;

                        styleGallery = new ServerStyleGalleryClass();
                        styleGalleryStorge = styleGallery as IStyleGalleryStorage;
                        styleGalleryStorge.TargetFile = fileName;

                        //IStyleGalleryClass styleGalleryClass = styleGallery.get_Class(CurrentStyleGalleryClassIndex);
                        ISymbologyStyleClass pSymbolClass = psymbologyStyleClass;
                        pSymbolClass.AddItem(styleGalleryItem, 0);
                        pSymbolClass.SelectItem(0);

                        stdole.IPictureDisp pPicture = pSymbolClass.PreviewItem(styleGalleryItem, 140, 15);
                        Image image = Image.FromHbitmap(new System.IntPtr(pPicture.Handle));
                        GridPanel panel = supergrid.PrimaryGrid;
                        GridRow row = new GridRow(image);
                        supergrid.PrimaryGrid.Rows.Remove(item);
                        supergrid.PrimaryGrid.Rows.Insert(n, row);
                        psymbologyStyleClass.RemoveItem(0);

                        imagelist.RemoveAt(n);
                        imagelist.Insert(n, image);
                        symbollist.RemoveAt(n);
                        symbollist.Insert(n, cartographicLineSymbol as ISymbol);

                        RePainPictureBox(Radio);
                    }
                }

            }
        }

        //改变线宽度
        private void widthgraphic_ValueChanged(object sender, EventArgs e)
        {
            double width = widthgraphic.Value;
            if (supergrid.PrimaryGrid.SelectedRowCount > 0)
            {
                SelectedElementCollection items = supergrid.PrimaryGrid.GetSelectedRows();
                GridRow crow = null;
                foreach (GridRow item in items)
                {
                    crow = item;
                    int n = crow.Index;
                    if (istype[n] == "isgraphicsymbol")
                    {
                        int j = 0;
                        for (int i = 0; i < n + 1; i++)
                        {
                            if (istype[i] == "isgraphicsymbol")
                            {
                                j++;
                            }
                        }

                        graphiclinelist[j - 1].Width = width;
                        ILineProperties lineProperties;
                        lineProperties = graphiclinelist[j - 1] as ILineProperties;

                        ICartographicLineSymbol cartographicLineSymbol = createCartographicLine_new(ClsGDBDataCommon.IColorToColor(graphiclinelist[j - 1].Color), width, lineProperties.Offset, graphiclinelist[j - 1].Cap.ToString(), graphiclinelist[j - 1].Join.ToString());

                        IStyleGallery styleGallery;
                        IStyleGalleryItem styleGalleryItem;
                        IStyleGalleryStorage styleGalleryStorge;

                        styleGalleryItem = new ServerStyleGalleryItemClass();
                        styleGalleryItem.Name = this.txtSymbolName.Text;
                        styleGalleryItem.Category = "default";
                        object objSymbol = cartographicLineSymbol as ISymbol;
                        styleGalleryItem.Item = objSymbol;

                        styleGallery = new ServerStyleGalleryClass();
                        styleGalleryStorge = styleGallery as IStyleGalleryStorage;
                        styleGalleryStorge.TargetFile = fileName;

                        //IStyleGalleryClass styleGalleryClass = styleGallery.get_Class(CurrentStyleGalleryClassIndex);
                        ISymbologyStyleClass pSymbolClass = psymbologyStyleClass;
                        pSymbolClass.AddItem(styleGalleryItem, 0);
                        pSymbolClass.SelectItem(0);

                        stdole.IPictureDisp pPicture = pSymbolClass.PreviewItem(styleGalleryItem, 140, 15);
                        Image image = Image.FromHbitmap(new System.IntPtr(pPicture.Handle));
                        GridPanel panel = supergrid.PrimaryGrid;
                        GridRow row = new GridRow(image);
                        supergrid.PrimaryGrid.Rows.Remove(item);
                        supergrid.PrimaryGrid.Rows.Insert(n, row);
                        psymbologyStyleClass.RemoveItem(0);

                        imagelist.RemoveAt(n);
                        imagelist.Insert(n, image);
                        symbollist.RemoveAt(n);
                        symbollist.Insert(n, cartographicLineSymbol as ISymbol);

                        RePainPictureBox(Radio);
                    }
                }
            }
        }

        //改变线偏移量
        private void offsetgraphic_ValueChanged(object sender, EventArgs e)
        {
            double offsetg = offsetgraphic.Value;
            if (supergrid.PrimaryGrid.SelectedRowCount > 0)
            {
                SelectedElementCollection items = supergrid.PrimaryGrid.GetSelectedRows();
                GridRow crow = null;
                foreach (GridRow item in items)
                {
                    crow = item;
                    int n = crow.Index;
                    if (istype[n] == "isgraphicsymbol")
                    {
                        int j = 0;
                        for (int i = 0; i < n + 1; i++)
                        {
                            if (istype[i] == "isgraphicsymbol")
                            {
                                j++;
                            }
                        }

                        ILineProperties lineProperties;
                        lineProperties = graphiclinelist[j - 1] as ILineProperties;
                        lineProperties.Offset = offsetg;

                        ICartographicLineSymbol cartographicLineSymbol = createCartographicLine_new(ClsGDBDataCommon.IColorToColor(graphiclinelist[j - 1].Color), graphiclinelist[j - 1].Width, offsetg, graphiclinelist[j - 1].Cap.ToString(), graphiclinelist[j - 1].Join.ToString());

                        IStyleGallery styleGallery;
                        IStyleGalleryItem styleGalleryItem;
                        IStyleGalleryStorage styleGalleryStorge;

                        styleGalleryItem = new ServerStyleGalleryItemClass();
                        styleGalleryItem.Name = this.txtSymbolName.Text;
                        styleGalleryItem.Category = "default";
                        object objSymbol = cartographicLineSymbol as ISymbol;
                        styleGalleryItem.Item = objSymbol;

                        styleGallery = new ServerStyleGalleryClass();
                        styleGalleryStorge = styleGallery as IStyleGalleryStorage;
                        styleGalleryStorge.TargetFile = fileName;

                        //IStyleGalleryClass styleGalleryClass = styleGallery.get_Class(CurrentStyleGalleryClassIndex);
                        ISymbologyStyleClass pSymbolClass = psymbologyStyleClass;
                        pSymbolClass.AddItem(styleGalleryItem, 0);
                        pSymbolClass.SelectItem(0);

                        stdole.IPictureDisp pPicture = pSymbolClass.PreviewItem(styleGalleryItem, 140, 15);
                        Image image = Image.FromHbitmap(new System.IntPtr(pPicture.Handle));
                        GridPanel panel = supergrid.PrimaryGrid;
                        GridRow row = new GridRow(image);
                        supergrid.PrimaryGrid.Rows.Remove(item);
                        supergrid.PrimaryGrid.Rows.Insert(n, row);
                        psymbologyStyleClass.RemoveItem(0);

                        imagelist.RemoveAt(n);
                        imagelist.Insert(n, image);
                        symbollist.RemoveAt(n);
                        symbollist.Insert(n, cartographicLineSymbol as ISymbol);

                        RePainPictureBox(Radio);
                    }

                }
            }
        }



#endregion

#region 哈希线相关操作

        //改变线颜色
        private void colorhash_SelectedColorChanged(object sender, EventArgs e)
        {
            Color cColor = colorhash.SelectedColor;
            if (supergrid.PrimaryGrid.SelectedRowCount > 0)
            {
                SelectedElementCollection items = supergrid.PrimaryGrid.GetSelectedRows();
                GridRow crow = null;
                foreach (GridRow item in items)
                {
                    crow = item;
                    int n = crow.Index;
                    if (istype[n] == "ishashsymbol")
                    {
                        int j = 0;
                        for (int i = 0; i < n + 1; i++)
                        {
                            if (istype[i] == "ishashsymbol")
                            {
                                j++;
                            }
                        }

                        hashlinelist[j - 1].Color = ClsGDBDataCommon.ColorToIColor(cColor);
                        ILineProperties lineProperties;
                        lineProperties = hashlinelist[j - 1] as ILineProperties;

                        IHashLineSymbol hashLineSymbol = createHashLine_new(cColor, hashlinelist[j - 1].Width, hashlinelist[j - 1].Angle, lineProperties.Offset);

                        IStyleGallery styleGallery;
                        IStyleGalleryItem styleGalleryItem;
                        IStyleGalleryStorage styleGalleryStorge;

                        styleGalleryItem = new ServerStyleGalleryItemClass();
                        styleGalleryItem.Name = this.txtSymbolName.Text;
                        styleGalleryItem.Category = "default";
                        object objSymbol = hashLineSymbol as ISymbol;
                        styleGalleryItem.Item = objSymbol;

                        styleGallery = new ServerStyleGalleryClass();
                        styleGalleryStorge = styleGallery as IStyleGalleryStorage;
                        styleGalleryStorge.TargetFile = fileName;

                        //IStyleGalleryClass styleGalleryClass = styleGallery.get_Class(CurrentStyleGalleryClassIndex);
                        ISymbologyStyleClass pSymbolClass = psymbologyStyleClass;
                        pSymbolClass.AddItem(styleGalleryItem, 0);
                        pSymbolClass.SelectItem(0);

                        stdole.IPictureDisp pPicture = pSymbolClass.PreviewItem(styleGalleryItem, 140, 15);
                        Image image = Image.FromHbitmap(new System.IntPtr(pPicture.Handle));
                        GridPanel panel = supergrid.PrimaryGrid;
                        GridRow row = new GridRow(image);
                        supergrid.PrimaryGrid.Rows.Remove(item);
                        supergrid.PrimaryGrid.Rows.Insert(n, row);
                        psymbologyStyleClass.RemoveItem(0);

                        imagelist.RemoveAt(n);
                        imagelist.Insert(n, image);
                        symbollist.RemoveAt(n);
                        symbollist.Insert(n, hashLineSymbol as ISymbol);

                        RePainPictureBox(Radio);
                    }

                }
            }
        }

        //改变线宽度
        private void widthhash_ValueChanged(object sender, EventArgs e)
        {
            double width = widthhash.Value;
            if (supergrid.PrimaryGrid.SelectedRowCount > 0)
            {
                SelectedElementCollection items = supergrid.PrimaryGrid.GetSelectedRows();
                GridRow crow = null;
                foreach (GridRow item in items)
                {
                    crow = item;
                    int n = crow.Index;
                    if (istype[n] == "ishashsymbol")
                    {
                        int j = 0;
                        for (int i = 0; i < n + 1; i++)
                        {
                            if (istype[i] == "ishashsymbol")
                            {
                                j++;
                            }
                        }

                        hashlinelist[j - 1].Width = width;
                        ILineProperties lineProperties;
                        lineProperties = hashlinelist[j - 1] as ILineProperties;

                        IHashLineSymbol hashLineSymbol = createHashLine_new(ClsGDBDataCommon.IColorToColor(hashlinelist[j - 1].Color), width, hashlinelist[j - 1].Angle, lineProperties.Offset);

                        IStyleGallery styleGallery;
                        IStyleGalleryItem styleGalleryItem;
                        IStyleGalleryStorage styleGalleryStorge;

                        styleGalleryItem = new ServerStyleGalleryItemClass();
                        styleGalleryItem.Name = this.txtSymbolName.Text;
                        styleGalleryItem.Category = "default";
                        object objSymbol = hashLineSymbol as ISymbol;
                        styleGalleryItem.Item = objSymbol;

                        styleGallery = new ServerStyleGalleryClass();
                        styleGalleryStorge = styleGallery as IStyleGalleryStorage;
                        styleGalleryStorge.TargetFile = fileName;

                        //IStyleGalleryClass styleGalleryClass = styleGallery.get_Class(CurrentStyleGalleryClassIndex);
                        ISymbologyStyleClass pSymbolClass = psymbologyStyleClass;
                        pSymbolClass.AddItem(styleGalleryItem, 0);
                        pSymbolClass.SelectItem(0);

                        stdole.IPictureDisp pPicture = pSymbolClass.PreviewItem(styleGalleryItem, 140, 15);
                        Image image = Image.FromHbitmap(new System.IntPtr(pPicture.Handle));
                        GridPanel panel = supergrid.PrimaryGrid;
                        GridRow row = new GridRow(image);
                        supergrid.PrimaryGrid.Rows.Remove(item);
                        supergrid.PrimaryGrid.Rows.Insert(n, row);
                        psymbologyStyleClass.RemoveItem(0);

                        imagelist.RemoveAt(n);
                        imagelist.Insert(n, image);
                        symbollist.RemoveAt(n);
                        symbollist.Insert(n, hashLineSymbol as ISymbol);

                        RePainPictureBox(Radio);
                    }

                }
            }
        }

        //改变先角度
        private void anglehash_ValueChanged(object sender, EventArgs e)
        {
            double angle = anglehash.Value;
            if (supergrid.PrimaryGrid.SelectedRowCount > 0)
            {
                SelectedElementCollection items = supergrid.PrimaryGrid.GetSelectedRows();
                GridRow crow = null;
                foreach (GridRow item in items)
                {
                    crow = item;
                    int n = crow.Index;
                    if (istype[n] == "ishashsymbol")
                    {
                        int j = 0;
                        for (int i = 0; i < n + 1; i++)
                        {
                            if (istype[i] == "ishashsymbol")
                            {
                                j++;
                            }
                        }

                        hashlinelist[j - 1].Angle = angle;
                        ILineProperties lineProperties;
                        lineProperties = hashlinelist[j - 1] as ILineProperties;

                        IHashLineSymbol hashLineSymbol = createHashLine_new(ClsGDBDataCommon.IColorToColor(hashlinelist[j - 1].Color), hashlinelist[j - 1].Width, angle, lineProperties.Offset);

                        IStyleGallery styleGallery;
                        IStyleGalleryItem styleGalleryItem;
                        IStyleGalleryStorage styleGalleryStorge;

                        styleGalleryItem = new ServerStyleGalleryItemClass();
                        styleGalleryItem.Name = this.txtSymbolName.Text;
                        styleGalleryItem.Category = "default";
                        object objSymbol = hashLineSymbol as ISymbol;
                        styleGalleryItem.Item = objSymbol;

                        styleGallery = new ServerStyleGalleryClass();
                        styleGalleryStorge = styleGallery as IStyleGalleryStorage;
                        styleGalleryStorge.TargetFile = fileName;

                        //IStyleGalleryClass styleGalleryClass = styleGallery.get_Class(CurrentStyleGalleryClassIndex);
                        ISymbologyStyleClass pSymbolClass = psymbologyStyleClass;
                        pSymbolClass.AddItem(styleGalleryItem, 0);
                        pSymbolClass.SelectItem(0);

                        stdole.IPictureDisp pPicture = pSymbolClass.PreviewItem(styleGalleryItem, 140, 15);
                        Image image = Image.FromHbitmap(new System.IntPtr(pPicture.Handle));
                        GridPanel panel = supergrid.PrimaryGrid;
                        GridRow row = new GridRow(image);
                        supergrid.PrimaryGrid.Rows.Remove(item);
                        supergrid.PrimaryGrid.Rows.Insert(n, row);
                        psymbologyStyleClass.RemoveItem(0);

                        imagelist.RemoveAt(n);
                        imagelist.Insert(n, image);
                        symbollist.RemoveAt(n);
                        symbollist.Insert(n, hashLineSymbol as ISymbol);

                        RePainPictureBox(Radio);
                    }

                }
            }
        }

        //改变线偏移量
        private void offsethash_ValueChanged(object sender, EventArgs e)
        {
            double offseth = offsethash.Value;
            if (supergrid.PrimaryGrid.SelectedRowCount > 0)
            {
                SelectedElementCollection items = supergrid.PrimaryGrid.GetSelectedRows();
                GridRow crow = null;
                foreach (GridRow item in items)
                {
                    crow = item;
                    int n = crow.Index;
                    if (istype[n] == "ishashsymbol")
                    {
                        int j = 0;
                        for (int i = 0; i < n + 1; i++)
                        {
                            if (istype[i] == "ishashsymbol")
                            {
                                j++;
                            }
                        }

                        ILineProperties lineProperties;
                        lineProperties = hashlinelist[j - 1] as ILineProperties;
                        lineProperties.Offset = offseth;

                        IHashLineSymbol hashLineSymbol = createHashLine_new(ClsGDBDataCommon.IColorToColor(hashlinelist[j - 1].Color), hashlinelist[j - 1].Width, hashlinelist[j - 1].Angle, offseth);

                        IStyleGallery styleGallery;
                        IStyleGalleryItem styleGalleryItem;
                        IStyleGalleryStorage styleGalleryStorge;

                        styleGalleryItem = new ServerStyleGalleryItemClass();
                        styleGalleryItem.Name = this.txtSymbolName.Text;
                        styleGalleryItem.Category = "default";
                        object objSymbol = hashLineSymbol as ISymbol;
                        styleGalleryItem.Item = objSymbol;

                        styleGallery = new ServerStyleGalleryClass();
                        styleGalleryStorge = styleGallery as IStyleGalleryStorage;
                        styleGalleryStorge.TargetFile = fileName;

                        //IStyleGalleryClass styleGalleryClass = styleGallery.get_Class(CurrentStyleGalleryClassIndex);
                        ISymbologyStyleClass pSymbolClass = psymbologyStyleClass;
                        pSymbolClass.AddItem(styleGalleryItem, 0);
                        pSymbolClass.SelectItem(0);

                        stdole.IPictureDisp pPicture = pSymbolClass.PreviewItem(styleGalleryItem, 140, 15);
                        Image image = Image.FromHbitmap(new System.IntPtr(pPicture.Handle));
                        GridPanel panel = supergrid.PrimaryGrid;
                        GridRow row = new GridRow(image);
                        supergrid.PrimaryGrid.Rows.Remove(item);
                        supergrid.PrimaryGrid.Rows.Insert(n, row);
                        psymbologyStyleClass.RemoveItem(0);

                        imagelist.RemoveAt(n);
                        imagelist.Insert(n, image);
                        symbollist.RemoveAt(n);
                        symbollist.Insert(n, hashLineSymbol as ISymbol);

                        RePainPictureBox(Radio);
                    }
                   
                }
            }
        }

    

#endregion

#region 创建各种线类型，用于更改线风格操作


        private ISimpleLineSymbol createSimpleLine_new(Color color, string style, double width)
        {
            ISimpleLineSymbol simpleLineSymbol = new SimpleLineSymbolClass();
            //设置子样式类型
            switch (style)
            {
                case "Solid":
                case "esriSLSSolid":
                    simpleLineSymbol.Style = esriSimpleLineStyle.esriSLSSolid;
                    break;
                case "Dashed":
                case "esriSLSDash":
                    simpleLineSymbol.Style = esriSimpleLineStyle.esriSLSDash;
                    break;
                case "Dotted":
                case "esriSLSDot":
                    simpleLineSymbol.Style = esriSimpleLineStyle.esriSLSDot;
                    break;
                case "Dash-Dot":
                case "esriSLSDashDot":
                    simpleLineSymbol.Style = esriSimpleLineStyle.esriSLSDashDot;

                    break;
                case "Dash-Dot-Dot":
                case "esriSLSDashDotDot":
                    simpleLineSymbol.Style = esriSimpleLineStyle.esriSLSDashDotDot;
                    break;
                case "null":
                    simpleLineSymbol.Style = esriSimpleLineStyle.esriSLSNull;
                    break;
            }
            simpleLineSymbol.Width = width;
            simpleLineSymbol.Color = ClsGDBDataCommon.ColorToIColor(color); ;
            return simpleLineSymbol;
        }

        private ICartographicLineSymbol createCartographicLine_new(Color color, double width, double offset, string cap, string join)
        {
            ICartographicLineSymbol cartographicLineSymbol = new CartographicLineSymbolClass();
            switch (cap)
            {
                case "Butt":
                    cartographicLineSymbol.Cap = esriLineCapStyle.esriLCSButt;
                    break;
                case "Round":
                    cartographicLineSymbol.Cap = esriLineCapStyle.esriLCSRound;
                    break;
                case "Square":
                    cartographicLineSymbol.Cap = esriLineCapStyle.esriLCSSquare;
                    break;
            }

            switch (join)
            {
                case "Mitre":
                    cartographicLineSymbol.Join = esriLineJoinStyle.esriLJSMitre;
                    break;
                case "Round":
                    cartographicLineSymbol.Join = esriLineJoinStyle.esriLJSRound;
                    break;
                case "Bevel":
                    cartographicLineSymbol.Join = esriLineJoinStyle.esriLJSBevel;
                    break;
            }
            cartographicLineSymbol.Width = width;
            cartographicLineSymbol.MiterLimit = 4;
            ILineProperties lineProperties;
            lineProperties = cartographicLineSymbol as ILineProperties;
            lineProperties.Offset = offset;
            double[] dob = new double[6];
            dob[0] = 0;
            dob[1] = 1;
            dob[2] = 2;
            dob[3] = 3;
            dob[4] = 4;
            dob[5] = 5;
            ITemplate template = new TemplateClass();
            template.Interval = 1;
            for (int i = 0; i < dob.Length; i += 2)
            {
                template.AddPatternElement(dob[i], dob[i + 1]);
            }
            lineProperties.Template = template;
            
            cartographicLineSymbol.Color = ClsGDBDataCommon.ColorToIColor(color); ;          
            return cartographicLineSymbol;
        }

        private IHashLineSymbol createHashLine_new(Color color, double width, double angle, double offset)
        {
            IHashLineSymbol hashLineSymbol = new HashLineSymbolClass();
            ILineProperties lineProperties = hashLineSymbol as ILineProperties;
            lineProperties.Offset = offset;
            double[] dob = new double[6];
            dob[0] = 0;
            dob[1] = 1;
            dob[2] = 2;
            dob[3] = 3;
            dob[4] = 4;
            dob[5] = 5;
            ITemplate template = new TemplateClass();
            //间隔
            template.Interval = 1;
            for (int i = 0; i < dob.Length; i += 2)
            {
                template.AddPatternElement(dob[i], dob[i + 1]);
            }
            lineProperties.Template = template;
            hashLineSymbol.Width = width;
            hashLineSymbol.Angle = angle;
            //hashLineSymbol.HashSymbol = createCartographicLine(color, width, offset, cap, join);
            hashLineSymbol.HashSymbol = createSimpleLine_new(color, "esriSLSDot", width);
            //IRgbColor hashColor = new RgbColor();
            //hashColor = getRGB(color.R, color.G, color.B);
            hashLineSymbol.Color = ClsGDBDataCommon.ColorToIColor(color);
            return hashLineSymbol;
        }

#endregion

       
        //获取ＣＡＰ＼ＪＯＩＮ的选择
        private void checkRadioButton()
        {
            if (rbCatButt.Checked == true)
            {
                curCatCap = "Butt";
            }
            else if (rbCatRoundC.Checked == true)
            {
                curCatCap = "Round";
            }
            else if (rbCatSquare.Checked == true)
            {
                curCatCap = "Square";
            }

            if (rbCatMitre.Checked == true)
            {
                curCatJoin = "Mitre";
            }
            else if (rbCatRoundJ.Checked == true)
            {
                curCatJoin = "Round";
            }
            else if (rbCatBevel.Checked == true)
            {
                curCatJoin = "Bevel";
            }

            if (rbHashButt.Checked == true)
            {
                curHashCap = "Butt";
            }
            else if (rbHashRoundC.Checked == true)
            {
                curHashCap = "Round";
            }
            else if (rbHashSquare.Checked == true)
            {
                curHashCap = "Square";
            }

            if (rbHashMitre.Checked == true)
            {
                curHashJoin = "Mitre";
            }
            else if (rbHashRoundJ.Checked == true)
            {
                curHashJoin = "Round";
            }
            else if (rbHashRoundJ.Checked == true)
            {
                curHashJoin = "Bevel";
            }
        }

        private void supergrid_RowClick(object sender, GridRowClickEventArgs e)
        {
            //if (supergrid.PrimaryGrid.SelectedRowCount > 0)
            //{
            //    SelectedElementCollection items = supergrid.PrimaryGrid.GetSelectedRows();
            //    GridRow rerow = null;
            //    foreach (GridRow item in items)
            //    {
            //        rerow = item;
            //        int n = rerow.Index;

            //        if (istype[n] == "issimplesymbol")
            //        {
            //            superTabControl1.SelectedTabIndex = 0;
            //            int j = 0;
            //            for (int i = 0; i < n + 1; i++)
            //            {
            //                if (istype[i] == "issimplesymbol")
            //                {
            //                    j++;
            //                }
            //            }

            //            colorsimple.SelectedColor = ClsGDBDataCommon.IColorToColor(simplelinelist[j - 1].Color);
            //            string style = simplelinelist[j - 1].Style.ToString();
            //            switch (style)
            //            {
            //                case "esriSLSSolid":
            //                    cbSimpleStyle.SelectedIndex = 0;
            //                    widthsimple.Value = simplelinelist[j - 1].Width;
            //                    break;
            //                case "esriSLSDash":
            //                    cbSimpleStyle.SelectedIndex = 1;
            //                    break;
            //                case "esriSLSDot":
            //                    cbSimpleStyle.SelectedIndex = 2;
            //                    break;
            //                case "esriSLSDashDot":
            //                    cbSimpleStyle.SelectedIndex = 3;

            //                    break;
            //                case "esriSLSDashDotDot":
            //                    cbSimpleStyle.SelectedIndex = 4;
            //                    break;
            //                case "null":
            //                    cbSimpleStyle.SelectedIndex = 5;
            //                    break;
            //            }



            //        }
            //        else if (istype[n] == "isgraphicsymbol")
            //        {
            //            superTabControl1.SelectedTabIndex = 1;
            //        }
            //        else
            //        {
            //            superTabControl1.SelectedTabIndex = 2;
            //        }
            //    }
            //    GridCell recell = rerow.Cells[0];
            //    supergrid.Focus();
            //    recell.IsSelected = true;
            //    rerow.IsSelected = true;
            //}
        }

        private void btncancle_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnok_Click(object sender, EventArgs e)
        {
           //// //指定临时图片保存位置
           // FileInfo fileInfo = new FileInfo(fileName);
           // string path = fileInfo.DirectoryName;
           // string bitmapFileName = path + @"\" + this.txtSymbolName.Text + ".bmp";
           // //创建新的画图，并将底色清为白色
           // Bitmap bitmap = new Bitmap(listBoxImage.ImageSize.Width - 100, listBoxImage.ImageSize.Height);
           // Graphics graphics = Graphics.FromImage(bitmap);
           // System.Drawing.Color color = System.Drawing.Color.FromArgb(0, 0, 0);
           // graphics.Clear(Color.White);

           // Bitmap image;
           // int startX;
           // int startY;
           // for (int i = 0; i < supergrid.PrimaryGrid.Rows.Count; i++)
           // {
           //     image = (Bitmap)imagelist[i];
           //     startX = picturepre.Width / 2 - imagelist[i].Size.Width / 2;
           //     startY = picturepre.Height / 2 - imagelist[i].Size.Height / 2;

           //     image.MakeTransparent(Color.Transparent);
           //     System.Drawing.Rectangle rectangle = new Rectangle(startX - 50, startY - 55, imagelist[i].Size.Width, imagelist[i].Size.Height);
           //     graphics.DrawImage(imagelist[i], rectangle);
           //     graphics.Save();
           // }
           // graphics.Dispose();

           // //保存画图
           // bitmap.Save(bitmapFileName, System.Drawing.Imaging.ImageFormat.Bmp);

           // IPictureLineSymbol pictureLineSymbol = new PictureLineSymbolClass();
           // pictureLineSymbol.CreateLineSymbolFromFile(esriIPictureType.esriIPictureBitmap, bitmapFileName);

           // pictureLineSymbol.Offset = 0;
           // pictureLineSymbol.Width = 10;
           // pictureLineSymbol.Rotate = true;

           // IHashLineSymbol phline = new HashLineSymbolClass();
           // phline.HashSymbol = pictureLineSymbol;
           // phline.Angle = 0;
            for (int i = 0; i < symbollist.Count; i++)
            {
                pMultiLineSymbol.AddLayer(symbollist[i] as ILineSymbol);
            }

            IStyleGallery styleGallery;
            IStyleGalleryItem styleGalleryItem;
            IStyleGalleryStorage styleGalleryStorge;

            styleGalleryItem = new ServerStyleGalleryItemClass();
            styleGalleryItem.Name = this.txtSymbolName.Text;
            styleGalleryItem.Category = "default";
            object objSymbol = pMultiLineSymbol;
            styleGalleryItem.Item = objSymbol;

            styleGallery = new ServerStyleGalleryClass();
            styleGalleryStorge = styleGallery as IStyleGalleryStorage;
            styleGalleryStorge.TargetFile = fileName;

            //IStyleGalleryClass styleGalleryClass = styleGallery.get_Class(CurrentStyleGalleryClassIndex);
            ISymbologyStyleClass pSymbolClass = psymbologyStyleClass;
            pSymbolClass.AddItem(styleGalleryItem, 0);
            pSymbolClass.SelectItem(0);

            //保存新样式
            //styleGallery.SaveStyle(fileName, fileInfo.Name, styleGallery.get_Class(15).Name);//"Line Symbols");

            this.Close();
        }
    }
}