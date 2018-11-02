/**
* @copyright Copyright(C), 2013-2013, PMRS Lab, IRSA, CAS
* @file FrmCalibGridBorder.cs
* @date 2013.03.16
* @author Ge Xizhi
* @brief 整饰边框设计
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
using DevComponents.DotNetBar;

namespace LibCerMap
{
    public partial class FrmCalibGridBorder : OfficeForm
    {
        ICalibratedMapGridBorder CalibratedMapGridBorder;

        public FrmCalibGridBorder()
        {
            InitializeComponent();
            this.EnableGlass = false;
        }
        private void FrmCalibGridBorder_Load(object sender, EventArgs e)
        {
            BackgroundColor.SelectedColor = Color.Black;
            ForegroundColor.SelectedColor = Color.White;
        }
        private void btOK_Click(object sender, EventArgs e)
        {
            CreateCalibratedMapGridBorder();
            this.Close();
        }

/**
* @fn  CreateCalibratedMapGridBorder
* @date 2013.03.16
* @author Ge Xizhi
* @brief 创建整饰边框
* @param 零个参数
* @retval 无返回值
*/
        private void CreateCalibratedMapGridBorder()
        {
            CalibratedMapGridBorder = new CalibratedMapGridBorderClass();

            //Set ICalibratedMapGridBorder properties
           
            CalibratedMapGridBorder.BackgroundColor = ClsGDBDataCommon.ColorToIColor(BackgroundColor.SelectedColor);
            CalibratedMapGridBorder.ForegroundColor = ClsGDBDataCommon.ColorToIColor(ForegroundColor.SelectedColor); ;

            CalibratedMapGridBorder.BorderWidth = double.Parse(BorderWith.Text);
            CalibratedMapGridBorder.Interval = double.Parse(BorderInterval .Text);

            if (ckBoxAlternating.Checked == true)
            {
                CalibratedMapGridBorder.Alternating = true;
            }
            else
            {
                CalibratedMapGridBorder.Alternating = false;
            }
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //调用该窗口后返回整饰边框
        public ICalibratedMapGridBorder GetGridBorder()
        {
            return CalibratedMapGridBorder;
        }
    }
}
