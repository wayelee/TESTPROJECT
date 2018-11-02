using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Geodatabase;

using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Analyst3D;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.DataSourcesFile;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Display;
using DevComponents.DotNetBar;
namespace LibCerMap
{
    public partial class FrmAddTinPlane : OfficeForm
    {
        public FrmAddTinPlane()
        {
            InitializeComponent();
            this.EnableGlass = false;
        }
        public double dHeight;
        public IGeometry pGeometry;
        public ITinEdit pTinEdit;
        public IMapControl2 pMapCtr;
        private void FrmAddTinPlane_Load(object sender, EventArgs e)
        {
            comboBoxExHeightSrc.SelectedIndex = 0;
        }

        public void setDoubleInputValue(double val)
        {
            doubleInput1.Value = val;
        }

        private void doubleInput1_ValueChanged(object sender, EventArgs e)
        {
            dHeight = doubleInput1.Value;
        }

        public void AddShapeToTin()
        {
            object Missing = Type.Missing;
            IPointCollection ptc = pGeometry as IPointCollection;
            if (ptc == null)
            {
                return;
            }
            //for (int i = 0; i < ptc.PointCount; i++)
            //{
            //    IPoint pt = ptc.get_Point(i);
            //    IZAware pza = pt as IZAware;
            //    pza.ZAware = true;
            //    pt.Z = dHeight;
            //}
            IZAware pza = pGeometry as IZAware;
            pza.ZAware = true;
            IZ pz = pGeometry as IZ;
            pz.SetConstantZ(dHeight);
            try
            {
                pTinEdit.AddShapeZ(pGeometry, esriTinSurfaceType.esriTinHardReplace, 0, ref Missing);
            }
            catch(SystemException e )
            {
                
            }
            if (pMapCtr != null)
            {
                pMapCtr.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
            }
        }

        private void comboBoxExHeightSrc_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void FrmAddTinPlane_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason != CloseReason.FormOwnerClosing)
            {
                e.Cancel = true;
            }
            Hide();
        }
        public void setButtonOkEnable(bool val)
        {
            buttonXOK.Enabled = val;
        }
        private void buttonX1_Click(object sender, EventArgs e)
        {
            buttonXOK.Enabled = false;
            AddShapeToTin();
        }
        public void CalPlaneHeight()
        {
            if (comboBoxExHeightSrc.SelectedIndex == 0)
            {
                ITinSelection pTinS = pTinEdit as ITinSelection;
                pTinS.SelectByArea(esriTinElementType.esriTinNode, (IPolygon)pGeometry, true, true, esriTinSelectionType.esriTinSelectionAdd);
                
                IEnumTinElement ienumtinnodes = pTinS.GetSelection(esriTinElementType.esriTinNode);
                ITinNode pNode = ienumtinnodes.Next() as ITinNode;
                double maxHeight = pNode.Z;
                while (pNode != null)
                {   
                    if (pNode.Z > maxHeight)
                    {
                        maxHeight = pNode.Z;
                    }           
                    pNode = ienumtinnodes.Next() as ITinNode;
                 
                }
                setDoubleInputValue(maxHeight);
            }
            else
                if (comboBoxExHeightSrc.SelectedIndex == 0)
                {
                    ITinSelection pTinS = pTinEdit as ITinSelection;
                    pTinS.SelectByArea(esriTinElementType.esriTinNode, (IPolygon)pGeometry, true, true, esriTinSelectionType.esriTinSelectionAdd);

                    IEnumTinElement ienumtinnodes = pTinS.GetSelection(esriTinElementType.esriTinNode);
                    ITinNode pNode = ienumtinnodes.Next() as ITinNode;
                    double minHeight = pNode.Z;
                    while (pNode != null)
                    {
                        if (pNode.Z < minHeight)
                        {
                            minHeight = pNode.Z;
                        }
                        pNode = ienumtinnodes.Next() as ITinNode;
                        
                    }
                    setDoubleInputValue(minHeight);
                }
        }
    }
}
