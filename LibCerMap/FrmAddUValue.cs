using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.esriSystem;

using DevComponents.DotNetBar;

namespace LibCerMap
{
    public partial class FrmAddUValue : OfficeForm
    {
        ArrayList addvx = new ArrayList();
        ArrayList addvxmu = new ArrayList();
        List<IColor> coloru = new List<IColor>();
        List<IColor> colorumu = new List<IColor>();
        List<long> cuntu = new List<long>();
        List<long> cuntumu = new List<long>();
        List<long> countu = new List<long>();
        List<long> countumu = new List<long>();
        IUniqueValueRenderer pUniquerender = null;
        IUniqueValueRenderer pUniquerendermu = null;
        DevComponents.AdvTree.AdvTree treeshow = null;
        DevComponents.DotNetBar.Controls.DataGridViewX gridviewu = null;
        DevComponents.DotNetBar.Controls.DataGridViewX gridviewmu = null;
        DataTable dtable2 = null;
        DataTable dtable3 = null;

        ISimpleLineSymbol pLineSymbol = new SimpleLineSymbolClass();
        ISimpleFillSymbol pSimpleFillSymbol = new SimpleFillSymbolClass();
        ISimpleMarkerSymbol pPointSymbol = new SimpleMarkerSymbolClass();
        IFeatureClass pFClass = null;


        public FrmAddUValue(IUniqueValueRenderer ure, IUniqueValueRenderer uremu, IFeatureClass fc, List<IColor> cu, List<IColor> cumu
                           , DevComponents.DotNetBar.Controls.DataGridViewX grid, DevComponents.DotNetBar.Controls.DataGridViewX gridmu
                           , DevComponents.AdvTree.AdvTree tree,DataTable dt,DataTable dtmu, ArrayList avx, ArrayList avxmu
                           ,List<long>couu,List<long>couumu,List<long>conuu,List<long>conuumu)
        {
            InitializeComponent();
            this.EnableGlass = false;
            pUniquerender = ure;
            pUniquerendermu = uremu;
            coloru = cu;
            colorumu = cumu;
            treeshow = tree;
            addvx = avx;
            addvxmu = avxmu;
            pFClass = fc;
            gridviewu = grid;
            gridviewmu = gridmu;
            dtable2 = dt;
            dtable3 = dtmu;
            cuntu = couu;
            cuntumu = couumu;
            countu = conuu;
            countumu = conuumu;
        }

        private void FrmAddUValue_Load(object sender, EventArgs e)
        {
            pLineSymbol.Style = esriSimpleLineStyle.esriSLSSolid;
            pLineSymbol.Width = 1;
            pSimpleFillSymbol.Style = esriSimpleFillStyle.esriSFSSolid;
            pSimpleFillSymbol.Outline.Width = 0.4;
            pPointSymbol.Style = esriSimpleMarkerStyle.esriSMSCircle;
            pPointSymbol.Size = 6;
            if (treeshow.SelectedIndex == 0)
            {
                for (int i = 0; i < addvx.Count; i++)
                {
                    DevComponents.AdvTree.Node nodeu = new DevComponents.AdvTree.Node();
                    nodeu.Text = addvx[i].ToString();
                    treeadd.Nodes.Add(nodeu);
                }
            }
            else
            {
                for (int i = 0; i < addvxmu.Count; i++)
                {
                    DevComponents.AdvTree.Node nodeu = new DevComponents.AdvTree.Node();
                    nodeu.Text = addvxmu[i].ToString();
                    treeadd.Nodes.Add(nodeu);
                }
            }
        }

        private void FrmAddUValue_FormClosed(object sender, FormClosedEventArgs e)
        {
            treeadd.Nodes.Clear();
        }

        private void btnok_Click(object sender, EventArgs e)
        {
            if (treeshow.SelectedIndex == 0)
            {
                if (treeadd.SelectedNode==null)
                {
                    MessageBox.Show("请选择要添加的值", "提示", MessageBoxButtons.OK);
                }
                else
                {
                    string strnode="";
                    strnode = treeadd.SelectedNode.Text;
                    if (pFClass.ShapeType == esriGeometryType.esriGeometryPolygon)
                    {
                        IClone pSourceClone = pSimpleFillSymbol as IClone;
                        ISimpleFillSymbol pSimpleFillSymb = pSourceClone.Clone() as ISimpleFillSymbol;
                        pUniquerender.AddValue(strnode, "唯一值渲染", (ISymbol)pSimpleFillSymb);

                        ISimpleFillSymbol pNextSymbol = pUniquerender.get_Symbol(strnode) as ISimpleFillSymbol;
                        pNextSymbol.Color = coloru[treeadd.SelectedIndex];
                        pUniquerender.set_Symbol(strnode, (ISymbol)pNextSymbol);
                        DataRow rowu = dtable2.NewRow();
                        rowu[1] = strnode;
                        rowu[2] = strnode;
                        rowu[3] = cuntu[treeadd.SelectedIndex];
                        dtable2.Rows.Add(rowu);
                        gridviewu.Rows[gridviewu.Rows.Count - 2].Cells[0].Style.BackColor = ClsGDBDataCommon.IColorToColor(pNextSymbol.Color);
                    }
                    else if (pFClass.ShapeType == esriGeometryType.esriGeometryLine || pFClass.ShapeType == esriGeometryType.esriGeometryPolyline)
                    {
                        IClone pSourceClone = pLineSymbol as IClone;
                        ISimpleLineSymbol pSimpleFillSymb = pSourceClone.Clone() as ISimpleLineSymbol;
                        pUniquerender.AddValue(strnode, "唯一值渲染", (ISymbol)pSimpleFillSymb);

                        ISimpleLineSymbol pNextSymbol = pUniquerender.get_Symbol(strnode) as ISimpleLineSymbol;
                        pNextSymbol.Color = coloru[treeadd.SelectedIndex];
                        pUniquerender.set_Symbol(strnode, (ISymbol)pNextSymbol);
                        DataRow rowu = dtable2.NewRow();
                        rowu[1] = strnode;
                        rowu[2] = strnode;
                        rowu[3] = cuntu[treeadd.SelectedIndex];
                        dtable2.Rows.Add(rowu);
                        gridviewu.Rows[gridviewu.Rows.Count - 2].Cells[0].Style.BackColor = ClsGDBDataCommon.IColorToColor(pNextSymbol.Color);
                    } 
                    else
                    {
                        IClone pSourceClone = pPointSymbol as IClone;
                        ISimpleMarkerSymbol pSimpleFillSymb = pSourceClone.Clone() as ISimpleMarkerSymbol;
                        pUniquerender.AddValue(strnode, "唯一值渲染", (ISymbol)pSimpleFillSymb);

                        ISimpleMarkerSymbol pNextSymbol = pUniquerender.get_Symbol(strnode) as ISimpleMarkerSymbol;
                        pNextSymbol.Color = coloru[treeadd.SelectedIndex];
                        pUniquerender.set_Symbol(strnode, (ISymbol)pNextSymbol);
                        DataRow rowu = dtable2.NewRow();
                        rowu[1] = strnode;
                        rowu[2] = strnode;
                        rowu[3] = cuntu[treeadd.SelectedIndex];
                        dtable2.Rows.Add(rowu);
                        gridviewu.Rows[gridviewu.Rows.Count - 2].Cells[0].Style.BackColor = ClsGDBDataCommon.IColorToColor(pNextSymbol.Color);
                    }
                    addvx.Remove(addvx[treeadd.SelectedIndex]);
                    coloru.Remove(coloru[treeadd.SelectedIndex]);
                    countu.Add(cuntu[treeadd.SelectedIndex]);
                    cuntu.Remove(cuntu[treeadd.SelectedIndex]);
                    treeadd.Nodes[treeadd.SelectedIndex].Remove();
                    treeadd.SelectedNode = null;
                    gridviewu.CurrentCell = null;
                }
               
            }
            else
            {
                if (treeadd.SelectedNode == null)
                {
                    MessageBox.Show("请选择要添加的值", "提示", MessageBoxButtons.OK);
                }
                else
                {
                    string strnode = "";
                    strnode = treeadd.SelectedNode.Text;
                    if (pFClass.ShapeType == esriGeometryType.esriGeometryPolygon)
                    {
                        IClone pSourceClone = pSimpleFillSymbol as IClone;
                        ISimpleFillSymbol pSimpleFillSymb = pSourceClone.Clone() as ISimpleFillSymbol;
                        pUniquerendermu.AddValue(strnode, "唯一值渲染", (ISymbol)pSimpleFillSymb);

                        ISimpleFillSymbol pNextSymbol = pUniquerendermu.get_Symbol(strnode) as ISimpleFillSymbol;
                        pNextSymbol.Color = colorumu[treeadd.SelectedIndex];
                        pUniquerendermu.set_Symbol(strnode, (ISymbol)pNextSymbol);
                        DataRow rowu = dtable3.NewRow();
                        rowu[1] = strnode;
                        rowu[2] = strnode;
                        rowu[3] = cuntumu[treeadd.SelectedIndex];
                        dtable3.Rows.Add(rowu);
                        gridviewmu.Rows[gridviewmu.Rows.Count - 2].Cells[0].Style.BackColor = ClsGDBDataCommon.IColorToColor(pNextSymbol.Color);
                    }
                    else if (pFClass.ShapeType == esriGeometryType.esriGeometryLine || pFClass.ShapeType == esriGeometryType.esriGeometryPolyline)
                    {
                        IClone pSourceClone = pLineSymbol as IClone;
                        ISimpleLineSymbol pSimpleFillSymb = pSourceClone.Clone() as ISimpleLineSymbol;
                        pUniquerendermu.AddValue(strnode, "唯一值渲染", (ISymbol)pSimpleFillSymb);

                        ISimpleLineSymbol pNextSymbol = pUniquerendermu.get_Symbol(strnode) as ISimpleLineSymbol;
                        pNextSymbol.Color = colorumu[treeadd.SelectedIndex];
                        pUniquerendermu.set_Symbol(strnode, (ISymbol)pNextSymbol);
                        DataRow rowu = dtable3.NewRow();
                        rowu[1] = strnode;
                        rowu[2] = strnode;
                        rowu[3] = cuntumu[treeadd.SelectedIndex];
                        dtable3.Rows.Add(rowu);
                        gridviewmu.Rows[gridviewmu.Rows.Count - 2].Cells[0].Style.BackColor = ClsGDBDataCommon.IColorToColor(pNextSymbol.Color);
                    }
                    else
                    {
                        IClone pSourceClone = pPointSymbol as IClone;
                        ISimpleMarkerSymbol pSimpleFillSymb = pSourceClone.Clone() as ISimpleMarkerSymbol;
                        pUniquerendermu.AddValue(strnode, "唯一值渲染", (ISymbol)pSimpleFillSymb);

                        ISimpleMarkerSymbol pNextSymbol = pUniquerendermu.get_Symbol(strnode) as ISimpleMarkerSymbol;
                        pNextSymbol.Color = colorumu[treeadd.SelectedIndex];
                        pUniquerendermu.set_Symbol(strnode, (ISymbol)pNextSymbol);
                        DataRow rowu = dtable3.NewRow();
                        rowu[1] = strnode;
                        rowu[2] = strnode;
                        rowu[3] = cuntumu[treeadd.SelectedIndex];
                        dtable3.Rows.Add(rowu);
                        gridviewmu.Rows[gridviewmu.Rows.Count - 2].Cells[0].Style.BackColor = ClsGDBDataCommon.IColorToColor(pNextSymbol.Color);
                    }
                    addvxmu.Remove(addvxmu[treeadd.SelectedIndex]);
                    colorumu.Remove(colorumu[treeadd.SelectedIndex]);
                    countumu.Add(cuntumu[treeadd.SelectedIndex]);
                    cuntumu.Remove(cuntumu[treeadd.SelectedIndex]);
                    treeadd.Nodes[treeadd.SelectedIndex].Remove();
                    treeadd.SelectedNode = null;
                    gridviewmu.CurrentCell = null;
                }
               
            }
        }

        private void btncancle_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
