/**
* @copyright Copyright(C), 2013, PMRS Lab, IRSA, CAS
* @file FrmSymbol.cs
* @date 2013.03.03
* @author Ge Xizhi
* @brief 面、点、线设计窗口
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
using System.IO;

namespace LibCerMap
{
    public partial class FrmSymbolStyle : OfficeForm
    {
       
        //定义字符串数组，用于存在样式的路径
        //string[] AllStylePath;

        //定义一个已经选择样式的列表
        public string[] SelectStylePath;
       
        public FrmSymbolStyle(string[] symbolstyle)
        {
            InitializeComponent();
            this.EnableGlass = false;
            SelectStylePath = symbolstyle;
        }
        private void FrmSymbolStyle_Load(object sender, EventArgs e)
        {
            //遍历文件
            DirectoryInfo theFolder = new DirectoryInfo(ClsGDBDataCommon.GetParentPathofExe() + @"Resource\Style\");
            FileInfo[] fileInfo = theFolder.GetFiles();
            //样式数量
            int StyleNumber = 0;
            foreach (FileInfo NextFile in fileInfo)  //遍历文件获取数量
            {
                StyleNumber++;
            }
            DevComponents.DotNetBar.BaseItem[] BaseItem = new DevComponents.DotNetBar.BaseItem[StyleNumber];
            int N = 0;
            foreach (FileInfo NextFile in fileInfo)  //遍历文件加载名称
            {
                DevComponents.DotNetBar.CheckBoxItem CheckBox = new DevComponents.DotNetBar.CheckBoxItem();
                CheckBox.Name = NextFile.Name;
                CheckBox.Text = NextFile.Name;
                for (int i = 0; i < SelectStylePath.Length; i++)
                {
                    string FileName = System.IO.Path.GetFileName(SelectStylePath[i]);
                    if (FileName == CheckBox.Text && FileName !=null)
                    {
                        CheckBox.Checked = true;
                    }
                }
                BaseItem[N] = CheckBox;
                N++;
            }
            this.itemPanelStyleList.Items.AddRange(BaseItem);
            this.itemPanelStyleList.Refresh();
        }

        private void btOK_Click(object sender, EventArgs e)
        {
            int SelectItemNumber = itemPanelStyleList.SelectedItems.Count;
            //SelectStylePath = new string[SelectItemNumber];
            for (int i = 0; i < SelectItemNumber; i++)
            {
                SelectStylePath[i] = ClsGDBDataCommon.GetParentPathofExe() + @"Resource\Style\" + itemPanelStyleList.SelectedItems[i].Text;
            }
            this.Hide();
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void itemPanelStyleList_ItemClick(object sender, EventArgs e)
        {

        }


    }
}
