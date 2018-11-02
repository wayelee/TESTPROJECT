// Copyright 2012 ESRI
// 
// All rights reserved under the copyright laws of the United States
// and applicable international laws, treaties, and conventions.
// 
// You may freely redistribute and use this sample code, with or
// without modification, provided you include the original copyright
// notice and use restrictions.
// 
// See the use restrictions at <your ArcGIS install location>/DeveloperKit10.1/userestrictions.txt.
// 

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;

using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.SystemUI;
using System.Collections.Generic;

namespace LibCerMap
{
    /// <summary>
    /// This class is used to synchronize a given PageLayoutControl and a MapControl.
    /// When initialized, the user must pass the reference of these control to the class, bind
    /// the control together by calling 'BindControls' which in turn sets a joined Map referenced
    /// by both control; and set all the buddy controls joined between these two controls.
    /// When alternating between the MapControl and PageLayoutControl, you should activate the visible control 
    /// and deactivate the other by calling ActivateXXX.
    /// This class is limited to a situation where the controls are not simultaneously visible. 
    /// </summary>
    public class ControlsSynchronizer
    {
        //public delegate void MapReplaceEvent(object sender, IMapControlEvents2_OnMapReplacedEvent e);
        //public event MapReplaceEvent m_mapReplaceEvent;//(object sender, IMapControlEvents2_OnMapReplacedEvent e);

        #region class members
        public ITool m_mapActiveTool = null;
        public ITool m_pageLayoutActiveTool = null;
        public ITool m_scenceActiveTool = null;

        private IMapControl3 m_mapControl = null;
        private IPageLayoutControl2 m_pageLayoutControl = null;
        private ISceneControl m_sceneControl = null;

        private bool m_IsMapCtrlactive = true;

        private ArrayList m_frameworkControls = null;
        #endregion

        #region constructor

        /// <summary>
        /// default constructor
        /// </summary>
        public ControlsSynchronizer()
        {
            //initialize the underlying ArrayList
            m_frameworkControls = new ArrayList();
        }

        /// <summary>
        /// class constructor
        /// </summary>
        /// <param name="mapControl"></param>
        /// <param name="pageLayoutControl"></param>
        public ControlsSynchronizer(IMapControl3 mapControl, IPageLayoutControl2 pageLayoutControl, ISceneControl sceneControl)
            : this()
        {
            //assign the class members
            m_mapControl = mapControl;
            m_pageLayoutControl = pageLayoutControl;
            m_sceneControl = sceneControl;
        }
        #endregion

        #region properties
        /// <summary>
        /// Gets or sets the MapControl
        /// </summary>
        public IMapControl3 MapControl
        {
            get { return m_mapControl; }
            set { m_mapControl = value; }
        }

        /// <summary>
        /// Gets or sets the PageLayoutControl
        /// </summary>
        public IPageLayoutControl2 PageLayoutControl
        {
            get { return m_pageLayoutControl; }
            set { m_pageLayoutControl = value; }
        }

        /// <summary>
        /// Get an indication of the type of the currently active view
        /// </summary>
        public string ActiveViewType
        {
            get
            {
                if (m_IsMapCtrlactive)
                    return "MapControl";
                else
                    return "PageLayoutControl";
            }
        }

        /// <summary>
        /// get the active control
        /// </summary>
        public object ActiveControl
        {
            get
            {
                if (m_mapControl == null || m_pageLayoutControl == null)
                    throw new Exception("ControlsSynchronizer::ActiveControl:\r\nEither MapControl or PageLayoutControl are not initialized!");

                if (m_IsMapCtrlactive)
                    return m_mapControl.Object;
                else
                    return m_pageLayoutControl.Object;
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Activate the MapControl and deactivate the PagleLayoutControl
        /// </summary>
        public void ActivateMap()
        {
            try
            {
                if (m_pageLayoutControl == null || m_mapControl == null)
                    throw new Exception("ControlsSynchronizer::ActivateMap:\r\nEither MapControl or PageLayoutControl are not initialized!");

                //cache the current tool of the PageLayoutControl
                if (m_pageLayoutControl.CurrentTool != null)
                {
                    m_pageLayoutActiveTool = m_pageLayoutControl.CurrentTool;
                }
                if (m_sceneControl.CurrentTool != null)
                {
                    m_scenceActiveTool = m_sceneControl.CurrentTool;
                }

                //deactivate the PagleLayout
                m_pageLayoutControl.ActiveView.Deactivate();

                //activate the MapControl
                m_mapControl.ActiveView.Activate(m_mapControl.hWnd);
                
                //assign the last active tool that has been used on the MapControl back as the active tool
                if (m_mapActiveTool != null)
                {
                    m_mapControl.CurrentTool = m_mapActiveTool;
                }
                m_IsMapCtrlactive = true;

                //on each of the framework controls, set the Buddy control to the MapControl
                this.SetBuddies(m_mapControl.Object);

            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("ControlsSynchronizer::ActivateMap:\r\n{0}", ex.Message));
            }
        }

        /// <summary>
        /// Activate the PagleLayoutControl and deactivate the MapCotrol
        /// </summary>
        public void ActivatePageLayout()
        {
            try
            {
                if (m_pageLayoutControl == null || m_mapControl == null)
                    throw new Exception("ControlsSynchronizer::ActivatePageLayout:\r\nEither MapControl or PageLayoutControl are not initialized!");

                //cache the current tool of the MapControl
                if (m_mapControl.CurrentTool != null)
                {
                    m_mapActiveTool = m_mapControl.CurrentTool;
                }
                if (m_sceneControl.CurrentTool != null)
                {
                    m_scenceActiveTool = m_sceneControl.CurrentTool;
                }

                //deactivate the MapControl
                m_mapControl.ActiveView.Deactivate();

                //activate the PageLayoutControl
                m_pageLayoutControl.ActiveView.Activate(m_pageLayoutControl.hWnd);
                
                //assign the last active tool that has been used on the PageLayoutControl back as the active tool
                if (m_pageLayoutActiveTool != null)
                {
                    m_pageLayoutControl.CurrentTool = m_pageLayoutActiveTool;
                }

                m_IsMapCtrlactive = false;

                //on each of the framework controls, set the Buddy control to the PageLayoutControl
                this.SetBuddies(m_pageLayoutControl.Object);

            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("ControlsSynchronizer::ActivatePageLayout:\r\n{0}", ex.Message));
            }
        }

        public void ActiveSceneView()
        {
            this.SetBuddies(m_sceneControl.Object);
        }
        /// <summary>
        /// given a new map, replaces the PageLayoutControl and the MapControl's focus map
        /// </summary>
        /// <param name="newMap"></param>
        public void ReplaceMap(IMap newMap)
        {
            if (newMap == null)
                throw new Exception("ControlsSynchronizer::ReplaceMap:\r\nNew map for replacement is not initialized!");

            if (m_pageLayoutControl == null || m_mapControl == null)
                throw new Exception("ControlsSynchronizer::ReplaceMap:\r\nEither MapControl or PageLayoutControl are not initialized!");

            bool bIsMapActive = m_IsMapCtrlactive;

            #region 地图重置前记录所有图例的items
           //////////////////////////////////////////////////////////////////////////
            //ReplaceMap后会引起Legend的Map为null，重置map后会引起Item变化
            ArrayList pMaps = new ArrayList();//记录Legend对应的地图
            ArrayList pCount = new ArrayList();//记录每个Legend内部Items的数量
            ArrayList pLegendItems = new ArrayList();//记录items

            IGraphicsContainer pGC = m_pageLayoutControl.ActiveView.GraphicsContainer;
            pGC.Reset();
            IElement pElement= pGC.Next();
            ILegend pLegend = null;
           
            while (pElement != null)
            {
                IElementProperties pElementProperties = pElement as IElementProperties;
                if (pElementProperties == null) continue;
                if (pElementProperties.Name == "Legend" || pElementProperties.Name == "图例")
                {
                    IMapSurroundFrame pMapSurroundFrame = pElement as IMapSurroundFrame;
                    pLegend = pMapSurroundFrame.MapSurround as ILegend;
                    if (pLegend != null)
                    {
                        for (int i = 0; i < pLegend.ItemCount;i++ )
                        {
                            pLegendItems.Add(pLegend.get_Item(i));
                        }
                        pCount.Add(pLegend.ItemCount);
                        pMaps.Add(pLegend.Map);
                    }                         
                }
                pElement = pGC.Next();
            }

            #endregion
            //////////////////////////////////////////////////////////////////////////
            //call replace map on the PageLayout in order to replace the focus map
            //we must call ActivatePageLayout, since it is the control we call 'ReplaceMaps'
            this.ActivatePageLayout();
            //assign the new map to the MapControl
            m_mapControl.Map = newMap;

            #region 重置后恢复图例的items
            pGC.Reset();
            int nIndex = 0;
            int nItemStart =0;
            pElement = pGC.Next();
            while (pElement != null)
            {
                IElementProperties pElementProperties = pElement as IElementProperties;
                if (pElementProperties == null) continue;
                if (pElementProperties.Name == "Legend" || pElementProperties.Name == "图例")
                {
                    IMapSurroundFrame pMapSurroundFrame = pElement as IMapSurroundFrame;
                    pLegend = pMapSurroundFrame.MapSurround as ILegend;
                    if (pLegend != null)
                    {
                        if (pLegend.Map == null)
                        {
                            pLegend.Map = pMaps[nIndex] as IMap;
                            if (pLegend.ItemCount != (int)pCount[nIndex])
                            {
                                for (int i = pLegend.ItemCount-1; i >= 0; i--)
                                {
                                    if (!FindItem(pLegendItems, nItemStart, (int)nItemStart + (int)pCount[nIndex], pLegend.get_Item(i)))//如果原来没有则删除
                                    {
                                        pLegend.RemoveItem(i);
                                    }
                                }
                            }
                        }
                        nItemStart += (int)pCount[nIndex];
                        nIndex++;
                    }
                }
                pElement = pGC.Next();
            }
            #endregion

            //reset the active tools
            m_pageLayoutActiveTool = null;
            m_mapActiveTool = null;

            //make sure that the last active control is activated
            if (bIsMapActive)
            {
                this.ActivateMap();
                m_mapControl.ActiveView.Refresh();
            }
            else
            {
                this.ActivatePageLayout();
                m_pageLayoutControl.ActiveView.Refresh();
            }
        }

        private bool FindItem(ArrayList list,int nStart,int nEnd, ILegendItem item)
        {
            for (int i = nStart; i < nEnd; i++)
            {
                if (((ILegendItem)list[i]).Layer == item.Layer)
                {
                    return true;
                }
            }
            return false;
        }
        //尝试复制Legend
        private void DeepCopyLegend(ILegend SrcLegend, ILegend DstLegend)
        {
            IClone pSrc = SrcLegend as IClone;
            IClone pDest = pSrc.Clone();
            DstLegend = pDest as ILegend;
        }



         //<summary>
         //given a new map, replaces the PageLayoutControl and the MapControl's focus map
         //</summary>
         //<param name="newMap"></param>
        public void ReplaceNewMap(IMap newMap)
        {
            if (newMap == null)
                throw new Exception("ControlsSynchronizer::ReplaceMap:\r\nNew map for replacement is not initialized!");

            if (m_pageLayoutControl == null || m_mapControl == null)
                throw new Exception("ControlsSynchronizer::ReplaceMap:\r\nEither MapControl or PageLayoutControl are not initialized!");

            //create a new instance of IMaps collection which is needed by the PageLayout
            IMaps maps = new Maps();
            //add the new map to the Maps collection
            maps.Add(newMap);

            bool bIsMapActive = m_IsMapCtrlactive;

            //call replace map on the PageLayout in order to replace the focus map
            //we must call ActivatePageLayout, since it is the control we call 'ReplaceMaps'
            this.ActivatePageLayout();
            m_pageLayoutControl.PageLayout.ReplaceMaps(maps);;

            //assign the new map to the MapControl
            m_mapControl.Map = newMap;

            //reset the active tools
            m_pageLayoutActiveTool = null;
            m_mapActiveTool = null;

            //make sure that the last active control is activated
            if (bIsMapActive)
            {
                this.ActivateMap();
                m_mapControl.ActiveView.Refresh();
            }
            else
            {
                this.ActivatePageLayout();
                m_pageLayoutControl.ActiveView.Refresh();
            }
        }

        public void AddNewMaps(IMaps newMaps)
        {
            if (newMaps == null)
                throw new Exception("ControlsSynchronizer::ReplaceMap:\r\nNew map for replacement is not initialized!");

            if (m_pageLayoutControl == null || m_mapControl == null)
                throw new Exception("ControlsSynchronizer::ReplaceMap:\r\nEither MapControl or PageLayoutControl are not initialized!");

           bool bIsMapActive = m_IsMapCtrlactive;

            //call replace map on the PageLayout in order to replace the focus map
            //we must call ActivatePageLayout, since it is the control we call 'ReplaceMaps'
            this.ActivatePageLayout();
            m_pageLayoutControl.PageLayout.ReplaceMaps(newMaps); ;
            IMap map = newMaps.get_Item(0);
            //assign the new map to the MapControl
            m_mapControl.Map = map;

            //reset the active tools
            //m_pageLayoutActiveTool = null;
            //m_mapActiveTool = null;

            //make sure that the last active control is activated
            if (bIsMapActive)
            {
                this.ActivateMap();
                m_mapControl.ActiveView.Refresh();
            }
            else
            {
                this.ActivatePageLayout();
                m_pageLayoutControl.ActiveView.Refresh();
            }
        }
        /// <summary>
        /// bind the MapControl and PageLayoutControl together by assigning a new joint focus map
        /// </summary>
        /// <param name="mapControl"></param>
        /// <param name="pageLayoutControl"></param>
        /// <param name="activateMapFirst">true if the MapControl supposed to be activated first</param>
        public void BindControls(IMapControl3 mapControl, IPageLayoutControl2 pageLayoutControl, bool activateMapFirst)
        {
            if (mapControl == null || pageLayoutControl == null)
                throw new Exception("ControlsSynchronizer::BindControls:\r\nEither MapControl or PageLayoutControl are not initialized!");

            m_mapControl = MapControl;
            m_pageLayoutControl = pageLayoutControl;

            this.BindControls(activateMapFirst);
        }

        /// <summary>
        /// bind the MapControl and PageLayoutControl together by assigning a new joint focus map 
        /// </summary>
        /// <param name="activateMapFirst">true if the MapControl supposed to be activated first</param>
        public void BindControls(bool activateMapFirst)
        {
            if (m_pageLayoutControl == null || m_mapControl == null)
                throw new Exception("ControlsSynchronizer::BindControls:\r\nEither MapControl or PageLayoutControl are not initialized!");

            //create a new instance of IMap
            IMap newMap = new MapClass();
            newMap.Name = "Map";

            //create a new instance of IMaps collection which is needed by the PageLayout
            IMaps maps = new Maps();
            //add the new Map instance to the Maps collection
            maps.Add(newMap);

            //call replace map on the PageLayout in order to replace the focus map
            m_pageLayoutControl.PageLayout.ReplaceMaps(maps);
            //assign the new map to the MapControl
            m_mapControl.Map = newMap;

            //if (m_mapReplaceEvent != null)
            //{
            //    m_mapReplaceEvent(m_mapControl, null);
            //}

            //reset the active tools
            m_pageLayoutActiveTool = null;
            m_mapActiveTool = null;

            //make sure that the last active control is activated
            if (activateMapFirst)
                this.ActivateMap();
            else
                this.ActivatePageLayout();
        }

        /// <summary>
        ///by passing the application's toolbars and TOC to the synchronization class, it saves you the
        ///management of the buddy control each time the active control changes. This method ads the framework
        ///control to an array; once the active control changes, the class iterates through the array and 
        ///calls SetBuddyControl on each of the stored framework control.
        /// </summary>
        /// <param name="control"></param>
        public void AddFrameworkControl(object control)
        {
            if (control == null)
                throw new Exception("ControlsSynchronizer::AddFrameworkControl:\r\nAdded control is not initialized!");

            m_frameworkControls.Add(control);
        }

        /// <summary>
        /// Remove a framework control from the managed list of controls
        /// </summary>
        /// <param name="control"></param>
        public void RemoveFrameworkControl(object control)
        {
            if (control == null)
                throw new Exception("ControlsSynchronizer::RemoveFrameworkControl:\r\nControl to be removed is not initialized!");

            m_frameworkControls.Remove(control);
        }

        /// <summary>
        /// Remove a framework control from the managed list of controls by specifying its index in the list
        /// </summary>
        /// <param name="index"></param>
        public void RemoveFrameworkControlAt(int index)
        {
            if (m_frameworkControls.Count < index)
                throw new Exception("ControlsSynchronizer::RemoveFrameworkControlAt:\r\nIndex is out of range!");

            m_frameworkControls.RemoveAt(index);
        }

        /// <summary>
        /// when the active control changes, the class iterates through the array of the framework controls
        ///  and calls SetBuddyControl on each of the controls.
        /// </summary>
        /// <param name="buddy">the active control</param>
        private void SetBuddies(object buddy)
        {
            try
            {
                if (buddy == null)
                    throw new Exception("ControlsSynchronizer::SetBuddies:\r\nTarget Buddy Control is not initialized!");

                foreach (object obj in m_frameworkControls)
                {
                    if (obj is IToolbarControl)
                    {
                        ((IToolbarControl)obj).SetBuddyControl(buddy);
                    }
                    else if (obj is ITOCControl)
                    {
                        ((ITOCControl)obj).SetBuddyControl(buddy);
                    }
                    else if (obj is IToolbarMenu)
                    {
                        ((IToolbarMenu)obj).SetHook(buddy);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("ControlsSynchronizer::SetBuddies:\r\n{0}", ex.Message));
            }
        }

        #endregion
    }
}
