/**
* @copyright Copyright(C), 2013, PMRS Lab, IRSA, CAS
* @file FrmNavigationGrids.cs
* @date 2013.03.03
* @author Ge Xizhi
* @brief 地图网格添加导航
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
    public partial class FrmNavigationGrids : OfficeForm
    {
        private IHookHelper m_hookHelper;
        public IMapGrids MapGrids;
        string[] SymbolStyle;
        IMapGrid pTempMapGrid = null;

        public FrmNavigationGrids(string[] symbolstyle, IHookHelper hookHelper)
        {
            InitializeComponent();
            this.EnableGlass = false;
            SymbolStyle = symbolstyle;
            m_hookHelper = hookHelper;
        }

        private void FrmGrids_Load(object sender, EventArgs e)
        {
            try
            {
                IActiveView pActiveView = m_hookHelper.PageLayout as IActiveView;
                IMap pMap = pActiveView.FocusMap;
                IGraphicsContainer pGraphicsContainer = pActiveView as IGraphicsContainer;
                IMapFrame pMapFrame = pGraphicsContainer.FindFrame(pMap) as IMapFrame;
                MapGrids = pMapFrame as IMapGrids;

                UpdateGridsListBox();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
       
        //更新格网列表
        private void UpdateGridsListBox()
        {
            try
            {
                if (MapGrids != null)
                {
                    if (itemPanelGrids.Items.Count == 0)
                    {
                        DevComponents.DotNetBar.BaseItem[] BaseItem = new DevComponents.DotNetBar.BaseItem[MapGrids.MapGridCount];
                        for (int i = 0; i < MapGrids.MapGridCount; i++)
                        {
                            DevComponents.DotNetBar.CheckBoxItem CheckBox = new DevComponents.DotNetBar.CheckBoxItem();
                            CheckBox.Name = MapGrids.get_MapGrid(i).Name;
                            CheckBox.Text = MapGrids.get_MapGrid(i).Name;
                            if (MapGrids.get_MapGrid(i).Visible == true)
                            {
                                CheckBox.Checked = true;
                            }
                            else
                            {
                                CheckBox.Checked = false;
                            }
                            //MapGrids.get_MapGrid(i).Visible = false;
                            BaseItem[i] = CheckBox;
                        }
                        this.itemPanelGrids.Items.AddRange(BaseItem);
                        this.itemPanelGrids.Refresh();
                    }
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
     
        //新建格网 
        private void btAddNewGrids_Click(object sender, EventArgs e)
        {
            try
            {
                //新建格网
                IMapGrid pMapGrid=null;
                FrmNewGrids NewGrids = new FrmNewGrids(m_hookHelper.ActiveView,SymbolStyle);
                if(NewGrids.ShowDialog() == DialogResult.OK)
                    pMapGrid = NewGrids.NewMapGrid;

                if (pMapGrid != null)
                {
                    MapGrids.AddMapGrid(pMapGrid);

                    DevComponents.DotNetBar.CheckBoxItem checkBoxItem = new DevComponents.DotNetBar.CheckBoxItem();
                    checkBoxItem.Name = MapGrids.get_MapGrid(MapGrids.MapGridCount - 1).Name;
                    checkBoxItem.Text = MapGrids.get_MapGrid(MapGrids.MapGridCount - 1).Name;
                    checkBoxItem.Checked = true;
                    MapGrids.get_MapGrid(MapGrids.MapGridCount - 1).Visible = false;
                    itemPanelGrids.Items.Add((DevComponents.DotNetBar.BaseItem)checkBoxItem, itemPanelGrids.Items.Count - 1);
                    itemPanelGrids.Refresh();
                }
                else
                {
                    return;
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
      
        //格网属性
        private void btGridsAttribute_Click(object sender, EventArgs e)
        {
            try
            {
                if (itemPanelGrids.SelectedItems.Count != 1)
                {
                    MessageBox.Show("选择的格网数量不正确，请选择一个格网！", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                else
                {
                    for (int i = 0; i < MapGrids.MapGridCount; i++)
                    {
                        IMapGrid pMapGrid = MapGrids.get_MapGrid(i);
                        if (pMapGrid == null) continue;
                        
                        if (pMapGrid.Name == itemPanelGrids.SelectedItems[0].Name)
                        {
                            FrmGridsAttribute FrmGridAtt = new FrmGridsAttribute(SymbolStyle, pMapGrid);
                            if (FrmGridAtt.ShowDialog() == DialogResult.OK)
                            {
                                pTempMapGrid = FrmGridAtt.m_pMapGrid;
                                pTempMapGrid.Name = pMapGrid.Name;                              
                            }
                            break;
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.StackTrace);                
            }
            finally
            {
                GC.Collect();
            }
            return;
        }
    
        //删除格网 
        private void btDeleteGrids_Click(object sender, EventArgs e)
        {
            try
            {
                if (itemPanelGrids.SelectedItems.Count == MapGrids.MapGridCount)
                {
                    MapGrids.ClearMapGrids();
                    itemPanelGrids.Items.Clear();
                    itemPanelGrids.Refresh();
                }
                if (itemPanelGrids.SelectedItems.Count < MapGrids.MapGridCount)
                {
                    while (itemPanelGrids.SelectedItems.Count != 0)
                    {
                        for (int j = 0; j < MapGrids.MapGridCount; j++)
                        {
                            if (MapGrids.get_MapGrid(j).Name == itemPanelGrids.SelectedItems[0].Name)
                            {
                                //删除格网
                                MapGrids.DeleteMapGrid(MapGrids.get_MapGrid(j));
                                //删除列表中，格网不存在的项
                                itemPanelGrids.Items.Remove(itemPanelGrids.SelectedItems[0].Name);
                                itemPanelGrids.Refresh();
                                break;
                            }
                        }
                    }
                }
                m_hookHelper.ActiveView.Refresh();  
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
     
        //确定按钮 
        private void btOK_Click(object sender, EventArgs e)
        {
            try
            {
                if (pTempMapGrid != null)
                {
                    for (int i = 0; i < MapGrids.MapGridCount; i++)
                    {
                        IMapGrid pMapGrid = MapGrids.get_MapGrid(i);
                        if (pMapGrid == null) continue;

                        if (pMapGrid.Name == pTempMapGrid.Name)
                        {
                            MapGrids.DeleteMapGrid(pMapGrid);
                            break;
                        }
                    }

                    MapGrids.AddMapGrid(pTempMapGrid);
                    pTempMapGrid = null;
                }

                //应用格网
                if (itemPanelGrids.Items.Count != 0)
                {
                    for (int i = 0; i < MapGrids.MapGridCount; i++)
                    {
                        MapGrids.get_MapGrid(i).Visible = false;
                        int pIndex = -1;
                        for (int j = 0; j < itemPanelGrids.SelectedItems.Count; j++)
                        {
                            if (MapGrids.get_MapGrid(i).Name == itemPanelGrids.SelectedItems[j].Name)
                            {
                                pIndex = j;
                            }
                        }
                        if (pIndex != -1)
                        {
                            MapGrids.get_MapGrid(i).Visible = true;
                        }
                    }
                }
                //if (itemPanelGrids.SelectedItems.Count == 0)
                //{
                //    for (int i = 0; i < MapGrids.MapGridCount; i++)
                //    {
                //        MapGrids.get_MapGrid(i).Visible = false;
                //    }
                //}
                m_hookHelper.ActiveView.Refresh();
                this.Hide();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally { GC.Collect(); }
        }
  
        //取消按钮 
        private void btCancel_Click(object sender, EventArgs e)
        {
            if (itemPanelGrids.Items.Count == 0)
            {
                this.Close();
            }
            else
            {
                this.Hide();
            }
        }
   
        //应用按钮 
        private void btApply_Click(object sender, EventArgs e)
        {
            try
            {
                if (pTempMapGrid != null)
                {
                    for (int i = 0; i < MapGrids.MapGridCount; i++)
                    {
                        IMapGrid pMapGrid = MapGrids.get_MapGrid(i);
                        if (pMapGrid == null) continue;

                        if (pMapGrid.Name == pTempMapGrid.Name)
                        {
                            MapGrids.DeleteMapGrid(pMapGrid);
                            break;
                        }
                    }
                
                    MapGrids.AddMapGrid(pTempMapGrid);
                    pTempMapGrid = null;
                }

                if (itemPanelGrids.Items.Count != 0)
                {
                    for (int i = 0; i < MapGrids.MapGridCount; i++)
                    {
                        MapGrids.get_MapGrid(i).Visible = false;
                        int pIndex = -1;
                        for (int j = 0; j < itemPanelGrids.SelectedItems.Count; j++)
                        {
                            if (MapGrids.get_MapGrid(i).Name == itemPanelGrids.SelectedItems[j].Name)
                            {
                                pIndex = j;
                                break;
                            }
                        }
                        if (pIndex != -1)
                        {
                            MapGrids.get_MapGrid(i).Visible = true;
                        }
                    }
                }
                m_hookHelper.ActiveView.Refresh();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                GC.Collect();
            }

            return;
        }
    }
}
