using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Windows.Forms;

namespace LibCerMap
{
    public class ST_WORKMODE
    {
        public string szID;//工作模式代号
        public string szSymbol; //工作模式名称
        //public string szName;//工作模式名称
        public double dSlice;//工作模式故有时长
        public double dAssignSlice;//工作模式分配时长
        public string szBeginTime;//开始时刻
        public string szEndTime;//结束时刻
        //public double dEndEnergy;//结束能量
        public ST_WORKMODE()
        {
            szID="-9999";
            szSymbol="-9999";
            //szName="-9999";
            dSlice=-9999;
            dAssignSlice=-9999;
            szBeginTime="-9999";
            szEndTime="-9999";
            //dEndEnergy=-9999;
        }
    }
    public class ParkSite
    {
        public string szID;//停泊点代号
        public string szSymbolChanged;//停泊点类型是否改变
        public string szSymbol;//停泊点类型
        public string szBeginTime;//到达停泊点时间
        public string szLeaveTime;//离开停泊点时间
        public string szTimeValid;//工作模式是否有效
        public int uiWorkModeNum;//停泊点工作模式个数
        public string szPosStatus;//位置状态是否有效
        public ST_WORKMODE[] ST_WorkModes;
        public ST_2DPOS stCoord;//位置坐标
        public ParkSite()
        {
            szID = "-9999";
            szSymbolChanged = "-9999";
            szSymbol = "-9999";
            szBeginTime = "-9999";
            szLeaveTime = "-9999";
            szTimeValid = "-9999";
            uiWorkModeNum = -9999;
            szPosStatus = "-9999";
            ST_WorkModes=new ST_WORKMODE[11];
            for (int i = 0; i <= 10;i++)
            {
                ST_WorkModes[i] = new ST_WORKMODE();
            }
            stCoord = new ST_2DPOS();
        }
    
    }
    public class CParkPoint
    {
        public List<ParkSite> ParkSList;
        public string szMacroNum;//整体规划代号
        public string szCyNum;//周期规划代号
        public string szDEMFlNm;//路径搜索对应的DEM图文件名
        public int uiPathRslt;//路径搜索结果
        public int uiParkNum;//停泊点个数
        ST_2DPOS stBeginCoord;//规划起点位置坐标
        ST_2DPOS stEndCoord;//规划终点位置坐标
        public CParkPoint()
        {
            szMacroNum = "-9999";
            szCyNum = "-9999";
            szDEMFlNm = "-9999";
            uiPathRslt = -9999;
            uiParkNum = -9999;
            stEndCoord = new ST_2DPOS();
            stBeginCoord = new ST_2DPOS();
        }

        public bool ReadParkXML(string XMLPath)
        {
            try
            {
                
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(XMLPath);
                XmlNode xn = xmlDoc.SelectSingleNode(@"cmlRoot/szMacroNum");
                if (xn!=null)
                {
                    szMacroNum = xn.InnerText;
                }
                xn = xmlDoc.SelectSingleNode(@"cmlRoot/szCyNum");
                if (xn!=null)
                {
                    szCyNum = xn.InnerText;
                }
                xn = xmlDoc.SelectSingleNode(@"cmlRoot/stBeginCoord/dX");
                if (xn != null)
                {
                    stBeginCoord.dx = double.Parse(xn.InnerText);
                }
                xn = xmlDoc.SelectSingleNode(@"cmlRoot/stBeginCoord/dY");
                if (xn != null)
                {
                    stBeginCoord.dy = double.Parse(xn.InnerText);
                }
                xn = xmlDoc.SelectSingleNode(@"cmlRoot/stEndCoord/dX");
                if (xn != null)
                {
                    stEndCoord.dx = double.Parse(xn.InnerText);
                }
                xn = xmlDoc.SelectSingleNode(@"cmlRoot/stEndCoord/dY");
                if (xn != null)
                {
                    stEndCoord.dy = double.Parse(xn.InnerText);
                }
                xn = xmlDoc.SelectSingleNode(@"cmlRoot/uiPathRslt");
                if (xn!=null)
                {
                    uiPathRslt=int.Parse(xn.InnerText);
                    if (uiPathRslt==0)
                    {
                        xn = xmlDoc.SelectSingleNode(@"cmlRoot/uiParkNum");
                        uiParkNum = int.Parse(xn.InnerText);
                        ParkSList = new List<ParkSite>(uiParkNum);
                        XmlNodeList xnl = xmlDoc.SelectNodes(@"cmlRoot/stParkSite");
                        foreach(XmlNode xNode in xnl)
                        {
                            ParkSite ParkSiteTemp = new ParkSite();
                            XmlNode xmlnode = xNode.SelectSingleNode("szID");
                            if (xmlnode!=null)
                            {
                                ParkSiteTemp.szID = xmlnode.InnerText;
                            }
                            xmlnode = xNode.SelectSingleNode("szSymbolChanged");
                            if (xmlnode != null)
                            {
                                ParkSiteTemp.szSymbolChanged = xmlnode.InnerText;
                            }
                            xmlnode = xNode.SelectSingleNode("szSymbol");
                            if (xmlnode != null)
                            {
                                ParkSiteTemp.szSymbol = xmlnode.InnerText;
                            }
                            xmlnode = xNode.SelectSingleNode("szBeginTime");
                            if (xmlnode != null)
                            {
                                ParkSiteTemp.szBeginTime = xmlnode.InnerText;
                            }
                            xmlnode = xNode.SelectSingleNode("szLeaveTime");
                            if (xmlnode != null)
                            {
                                ParkSiteTemp.szLeaveTime = xmlnode.InnerText;
                            }
                            xmlnode = xNode.SelectSingleNode("szTimeValid");
                            if (xmlnode != null)
                            {
                                ParkSiteTemp.szTimeValid = xmlnode.InnerText;
                            }
                            xmlnode = xNode.SelectSingleNode("szPosStatus");
                            if (xmlnode != null)
                            {
                                ParkSiteTemp.szPosStatus = xmlnode.InnerText;
                            }
                            xmlnode = xNode.SelectSingleNode(@"stCoord/dX");
                            if (xmlnode != null)
                            {
                                ParkSiteTemp.stCoord.dy = double.Parse(xmlnode.InnerText);//坐标系x=y
                            }
                            xmlnode = xNode.SelectSingleNode(@"stCoord/dY");
                            if (xmlnode != null)
                            {
                                ParkSiteTemp.stCoord.dx = double.Parse(xmlnode.InnerText);//坐标系y=x
                            }
                            xmlnode = xNode.SelectSingleNode("uiWorkModeNum");
                            if (xmlnode != null)
                            {
                                ParkSiteTemp.uiWorkModeNum = int.Parse(xmlnode.InnerText);
                            }
                            XmlNodeList WorkModeNode = xNode.SelectNodes("stWorkMode");
                            int i = 0;
                            foreach (XmlNode wmN in WorkModeNode)
                            {
                                XmlNode workModeNode = wmN.SelectSingleNode("szSymbol");
                                ParkSiteTemp.ST_WorkModes[i].szSymbol = workModeNode.InnerText;
                                workModeNode = wmN.SelectSingleNode("szID");
                                ParkSiteTemp.ST_WorkModes[i].szID = workModeNode.InnerText;
                                //workModeNode = wmN.SelectSingleNode("szName");
                                //ParkSiteTemp.ST_WorkModes[i].szName = workModeNode.InnerText;
                                workModeNode = wmN.SelectSingleNode("dSlice");
                                ParkSiteTemp.ST_WorkModes[i].dSlice = double.Parse(workModeNode.InnerText);
                                workModeNode = wmN.SelectSingleNode("dAssignSlice");
                                ParkSiteTemp.ST_WorkModes[i].dAssignSlice = double.Parse(workModeNode.InnerText);
                                workModeNode = wmN.SelectSingleNode("szBeginTime");
                                ParkSiteTemp.ST_WorkModes[i].szBeginTime = workModeNode.InnerText;
                                workModeNode = wmN.SelectSingleNode("szEndTime");
                                ParkSiteTemp.ST_WorkModes[i].szEndTime = workModeNode.InnerText;
                                //workModeNode = wmN.SelectSingleNode("dEndEnergy");
                                //ParkSiteTemp.ST_WorkModes[i].dEndEnergy = double.Parse(workModeNode.InnerText);
                                i++;
                            }
                            ParkSList.Add(ParkSiteTemp);
                        }
                    } 
                    else if(uiPathRslt==1)
                    {
                        MessageBox.Show("目标点不可达", "提示", MessageBoxButtons.OK);
                    }
                    else if (uiPathRslt==2)
                    {
                        MessageBox.Show("导航点个数不够", "提示", MessageBoxButtons.OK);
                    }
                }
                
                return true;
            }
            catch (System.Exception ex)
            {
            	return false;
            }
        }
    }
}
