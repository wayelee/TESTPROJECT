/**
* @copyright Copyright(C), 2013, PMRS Lab, IRSA, CAS
* @file FrmNorthArrow.cs
* @date 2013.03.03
* @author Ge Xizhi
* @brief 添加指北针
* @version 1.0
* @par 修改历史：
* <作者>  <修改日期>  <版本号>  <详细描述>\n
*张三  2013.03.03  1.0  
*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.DataSourcesFile;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.SystemUI;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Display;
using DevComponents.DotNetBar;

namespace LibCerMap
{
    public partial class FrmNorthArrow : OfficeForm
    {
        private IStyleGalleryItem m_pStyleGalleryItem=null;
        private INorthArrow m_pNorthArrow = null;
        private INorthArrow m_pNorthArrowOrg =null;

        public FrmNorthArrow(INorthArrow northArrow)
        {
            InitializeComponent();
            this.EnableGlass = false;
            m_pNorthArrow = northArrow;     
        }
        private void FrmNorthArrow_Load(object sender, EventArgs e)
        {
            try
            {
                //加载指北针样式
                string EsriStylePath = ClsGDBDataCommon.GetParentPathofExe() + @"Resource\Style\ESRI.ServerStyle";
                axSymbologyControl.LoadStyleFile(EsriStylePath);
                axSymbologyControl.StyleClass = esriSymbologyStyleClass.esriStyleClassNorthArrows;
                ISymbologyStyleClass symbologyStyleClass = axSymbologyControl.GetStyleClass(axSymbologyControl.StyleClass);
                if (m_pNorthArrow != null)
                {
                    m_pStyleGalleryItem = new ServerStyleGalleryItem();
                    m_pStyleGalleryItem.Item = m_pNorthArrow;
                    symbologyStyleClass.AddItem(m_pStyleGalleryItem, 0);
                   
                }
                 symbologyStyleClass.SelectItem(0);
                ////选择第一个指北针样式为预定样式
                //SetFeatureClassStyle(esriSymbologyStyleClass.esriStyleClassNorthArrows);
                ////默认设置
                //角度设置
                this.NorthArrowAngle.Value = 0;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
      
        //选择样式
        private void axSymbologyControl_OnItemSelected(object sender, ISymbologyControlEvents_OnItemSelectedEvent e)
        {
            //选择样式
            m_pStyleGalleryItem = (IStyleGalleryItem)e.styleGalleryItem;
            if (m_pStyleGalleryItem != null)
            {
               INorthArrow pNorthArrow = m_pStyleGalleryItem.Item as INorthArrow;
               NorthArrowAngle.Text = pNorthArrow.Angle.ToString();
               this.NorthArrowSize.Value = pNorthArrow.Size;
               IColor pNorthArrowColor = pNorthArrow.Color;
                Color pColor = ColorTranslator.FromOle(pNorthArrowColor.RGB );
                colorNorthArrow.SelectedColor = pColor;
            }
            PreviewImage();
        }

        //首次创建指北针选择样式为第一个
        private void SetFeatureClassStyle(esriSymbologyStyleClass symbologyStyleClass)
        {
            this.axSymbologyControl.StyleClass = symbologyStyleClass;
            ISymbologyStyleClass pSymbologyStyleClass = this.axSymbologyControl.GetStyleClass(symbologyStyleClass);
            pSymbologyStyleClass.SelectItem(0);
        }
        //预览
        private void PreviewImage()
        {
            try
            {
                //Get and set the style class 
                ISymbologyStyleClass symbologyStyleClass = axSymbologyControl.GetStyleClass(axSymbologyControl.StyleClass);

                //Preview an image of the symbol
                stdole.IPictureDisp pPicpture = symbologyStyleClass.PreviewItem(m_pStyleGalleryItem, ImagePreview.Width, ImagePreview.Height);
                System.Drawing.Image pImage = System.Drawing.Image.FromHbitmap(new System.IntPtr(pPicpture.Handle));

                ImagePreview.Image = pImage;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
      
        //颜色设置
        private void colorNorthArrow_SelectedColorChanged(object sender, EventArgs e)
        {
            if (m_pStyleGalleryItem == null)
            {
                MessageBox.Show("请选择指北针样式", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            
            INorthArrow pNorthArrow = m_pStyleGalleryItem.Item as INorthArrow;
            pNorthArrow.Color = ClsGDBDataCommon.ColorToIColor(colorNorthArrow.SelectedColor);
            PreviewImage();
        }
     
        //设置角度
        private void NorthArrowAngle_ValueChanged(object sender, EventArgs e)
        {
            if (m_pStyleGalleryItem != null && !string.IsNullOrEmpty(NorthArrowAngle.Text))
            {
                INorthArrow pNorthArrow = m_pStyleGalleryItem.Item as INorthArrow;
                pNorthArrow.CalibrationAngle = double.Parse(this.NorthArrowAngle.Text); //这个角度
                PreviewImage();
            }
        }
        
        //大小设置
        private void NorthArrowSize_ValueChanged(object sender, EventArgs e)
        {
            if (m_pStyleGalleryItem != null  && !string.IsNullOrEmpty(NorthArrowSize.Text))
            {
                INorthArrow pNorthArrow = m_pStyleGalleryItem.Item as INorthArrow;
                pNorthArrow.Size = double.Parse(this.NorthArrowSize.Text);
                PreviewImage();
            }
        }
       
        //确定按钮
        private void btOK_Click(object sender, EventArgs e)
        {
            //INorthArrow pNorthArrow = m_pStyleGalleryItem.Item as INorthArrow;
            //IClone pSrcClone = pNorthArrow as IClone;
            //IClone pDstClone = pSrcClone.Clone();
            //INorthArrow pDstNorthArrow = pDstClone as INorthArrow;

            //m_pNorthArrow.CalibrationAngle = pDstNorthArrow.CalibrationAngle;
            //m_pNorthArrow.Color = pDstNorthArrow.Color;
            //m_pNorthArrow.Size = pDstNorthArrow.Size;

            this.Close();
            this.DialogResult = DialogResult.OK;
        }
     
        //取消按钮
        private void btCancel_Click(object sender, EventArgs e)
        {
            this.Close();
            DialogResult = DialogResult.Cancel;
        }      

        //调用该窗口后返回指北针样式
        public IStyleGalleryItem GetItem()
        {
            if (m_pStyleGalleryItem == null)
            {
                MessageBox.Show("请选择一个指北针样式！");
                return null;
            }
            else
            {
                return m_pStyleGalleryItem;
                //m_pNorthArrow = m_pStyleGalleryItem.Item as INorthArrow;
                ////名称
                //m_pNorthArrow.Name = "North Arrow";
                ////角度
                //m_pNorthArrow.CalibrationAngle = double.Parse(this.NorthArrowAngle.Text);
                ////大小
                //string ptxtSize = double.Parse(NorthArrowSize.Text).ToString();
                //m_pNorthArrow.Size = double.Parse(ptxtSize);

                //return m_pNorthArrow;
            }
        }

        //双击样式窗口
        private void axSymbologyControl_OnDoubleClick(object sender, ISymbologyControlEvents_OnDoubleClickEvent e)
        {
            btOK_Click(null,null);
        }
    }
}
