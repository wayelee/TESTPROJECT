using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using System.IO;
using System.Windows.Forms;

namespace LibEngineCmd
{
     public class ClsGlobal
    {
        public ClsGlobal()
        {

        }
        public static string GetParentPathofExe()
        {
            DirectoryInfo path = new DirectoryInfo(Application.StartupPath);
            string strPath = path.ToString();
            strPath = strPath.Remove(strPath.Length - path.Name.Length);
            return strPath;
        }
        public IMap GetSelectedMap(IHookHelper hookHelper)
        {
            IMap pMap = null;
            //if (hookHelper.Hook is IToolbarControl)
            //{
            //    object obj = ((IToolbarControl)hookHelper.Hook).Buddy;
            //    if (obj is IMapControl3)
            //    {
            //        IMapControl2 mapCtrl = obj as IMapControl2;
            //        pMap = mapCtrl.Tag as IMap;
            //    }
            //    else if (obj is IPageLayoutControl3)
            //    {
            //        IPageLayoutControl2 pageCtl = hookHelper.Hook as IPageLayoutControl2;
            //        pMap = pageCtl.Tag as ILayer;
            //    }
            //}
            //else if (hookHelper.Hook is IMapControl3)
            //{
            //    IMapControl3 mapCtrl = hookHelper.Hook as IMapControl3;
            //    pMap = mapCtrl.Tag as ILayer;
            //}
            //else if (hookHelper.Hook is IPageLayoutControl3)
            //{
            //    IPageLayoutControl3 pageCtl = hookHelper.Hook as IPageLayoutControl3;
            //    pMap = pageCtl.Tag as ILayer;
            //}
            return pMap;
        }
        
        public static void GetSelectedMapAndLayer(ITOCControlDefault tocControl, ref  IBasicMap map, ref ILayer layer)
        {
            Object other = null;
            Object index = null;
            esriTOCControlItem item = esriTOCControlItem.esriTOCControlItemNone;
            tocControl.GetSelectedItem(ref item, ref map, ref layer, ref other, ref index);
        }

        public static ILayer GetSelectedLayer(IHookHelper hookHelper)
        {
            ILayer pLayer = null;
            if (hookHelper.Hook is IToolbarControl)
            {
                object obj = ((IToolbarControl)hookHelper.Hook).Buddy;
                if (obj is IMapControl3)
                {
                    IMapControl3 mapCtrl = obj as IMapControl3;
                    pLayer = mapCtrl.CustomProperty as ILayer;
                }
                else if (obj is IPageLayoutControl3)
                {
                    IPageLayoutControl3 pageCtl = obj as IPageLayoutControl3;
                    pLayer = pageCtl.CustomProperty as ILayer;
                }
            }
            else if (hookHelper.Hook is IMapControl3)
            {
                IMapControl3 mapCtrl = hookHelper.Hook as IMapControl3;
                pLayer = mapCtrl.CustomProperty as ILayer;
            }
            else if (hookHelper.Hook is IPageLayoutControl3)
            {
                IPageLayoutControl3 pageCtl = hookHelper.Hook as IPageLayoutControl3;
                pLayer = pageCtl.CustomProperty as ILayer;
            }
            return pLayer;
        }

        public static IMapControl3 GetMapControl(IHookHelper hookHelper)
        {
            IMapControl3 mapCtrl = null;
            if (hookHelper.Hook is IToolbarControl)
            {
                object obj = ((IToolbarControl)hookHelper.Hook).Buddy;
                if (obj is IMapControl3)
                {
                    mapCtrl = obj as IMapControl3;                   
                }
                
            }
            else if (hookHelper.Hook is IMapControl3)
            {
                mapCtrl = hookHelper.Hook as IMapControl3;
            }

            return mapCtrl;
        }

        public static IPageLayoutControl3 GetPageLayoutControl(IHookHelper hookHelper)
        {
            IPageLayoutControl3 pageCtrl = null;
            if (hookHelper.Hook is IToolbarControl)
            {
                object obj = ((IToolbarControl)hookHelper.Hook).Buddy;
                if (obj is IPageLayoutControl3)
                {
                    pageCtrl = obj as IPageLayoutControl3;
                }

            }
            else if (hookHelper.Hook is IPageLayoutControl3)
            {
                pageCtrl = hookHelper.Hook as IPageLayoutControl3;
            }
            return pageCtrl;
        }

        public static IMap GetFocusMap(IHookHelper hookHelper)
        {
            IMap map = null;
            if (hookHelper.Hook is IToolbarControl)
            {
                object obj = ((IToolbarControl)hookHelper.Hook).Buddy;
                if (obj is IMapControl3)
                {
                    IMapControl3 mapCtrl = obj as IMapControl3;
                    map = mapCtrl.Map;
                }
                else if (obj is IPageLayoutControl3)
                {
                    IPageLayoutControl3 pageCtl = obj as IPageLayoutControl3;
                    map = pageCtl.ActiveView.FocusMap;
                }
            }
            else if (hookHelper.Hook is IMapControl3)
            {
                IMapControl3 mapCtrl = hookHelper.Hook as IMapControl3;
                map = mapCtrl.Map;
            }
            else if (hookHelper.Hook is IPageLayoutControl3)
            {
                IPageLayoutControl3 pageCtl = hookHelper.Hook as IPageLayoutControl3;
                map = pageCtl.ActiveView.FocusMap;
            }
            return map;
        }

        public static IFeatureLayer GetEditTargetLayer()
        {
            IEngineEditor pEngineEdit = new EngineEditorClass();
            IEngineEditLayers pEEditLayers = pEngineEdit as IEngineEditLayers;
            IFeatureLayer targetLayer = pEEditLayers.TargetLayer;
            return targetLayer;
        }
    }
}
