/**
* @copyright Copyright(C), 2013-2013, PMRS Lab, IRSA, CAS
* @file FrmIndexTabStyle.cs
* @date 2013.03.16
* @author Ge Xizhi
* @brief 索引格网选项卡设计
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
    public partial class FrmIndexTabStyle : OfficeForm
    {
        public IRgbColor pForeground;
        public IRgbColor pOutline;

        public double pTick;
       
        public FrmIndexTabStyle()
        {
            InitializeComponent();
            this.EnableGlass = false;
        }

        private void FrmIndexTabStyle_Load(object sender, EventArgs e)
        {
            pTick = double.Parse(this.TabWith.Text);
            ForegroundColor.SelectedColor = Color.White;
            OutlineColor.SelectedColor = Color.Black;
        }
      
        //确定按钮
        private void btOK_Click(object sender, EventArgs e)
        {
            if (ForegroundColor.SelectedColor != null)
            {
                pForeground = new RgbColorClass();
                pForeground.Red = this.ForegroundColor.SelectedColor.R;
                pForeground.Green = this.ForegroundColor.SelectedColor.G;
                pForeground.Blue = this.ForegroundColor.SelectedColor.B;
            }

            if (OutlineColor.SelectedColor != null)
            {
                pOutline = new RgbColorClass();
                pOutline.Red = this.OutlineColor.SelectedColor.R;
                pOutline.Green = this.OutlineColor.SelectedColor.G;
                pOutline.Blue = this.OutlineColor.SelectedColor.B;
            }
            pTick = double.Parse(this.TabWith.Text);

            this.Close();
        }

        //取消按钮
        private void btCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
