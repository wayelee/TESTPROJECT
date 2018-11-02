using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Windows.Forms;

namespace LibCerMap
{
    public class GmpPoint
    {
        public string szPointType;//点类型
        public double dPointX;//着陆坐标系下的X
        public double dPointY;//着陆坐标系下的Y
        public double dPointZ;//着陆坐标系下的Z
        public double dPointLng;//月球大地坐标系下的经度
        public double dPointLat;//月球大地坐标系下的纬度
        public double dPointHeight;//月球大地坐标系下的高度
        public double speed;//行驶速度
        public string szPSTime;//规划开始时间
        public string szPTSTime;//规划时长
        public string szWSTime;//行走开始时间
        public string szWTSTime;//行走时长
        public string szESTime;//探测开始时间
        public string szETSTime;//探测时长
        public string szEETTime;//探测结束时间

        public GmpPoint()
        {
            szPointType = "<null>";
            dPointX = -9999;
            dPointY = -9999;
            dPointZ = -9999;
            dPointLng = -9999;
            dPointLat = -9999;
            dPointHeight = -9999;
            speed = -9999;
            szPSTime = "<null>";
            szPTSTime = "<null>";
            szWSTime = "<null>";
            szWTSTime = "<null>";
            szESTime = "<null>";
            szETSTime = "<null>";
            szEETTime = "<null>";
        }

    }
    public class CGmpPoint
    {
        public string szMacroNum;//任务整体规划号
        public int uiPointNum;//整体点个数
        public List<GmpPoint> GmpPointS;
        public CGmpPoint()
        {
            szMacroNum = "<null>";
            uiPointNum = -9999;
            GmpPointS = new List<GmpPoint>();
        }
        public bool ReadGmpXML(string XMLPath)
        {
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(XMLPath);
                XmlNode xn = xmlDoc.SelectSingleNode(@"cmlRoot/szMacroNum");
                if (xn != null)
                {
                    szMacroNum = xn.InnerText;
                }
                xn = xmlDoc.SelectSingleNode(@"cmlRoot/uiPointNum");
                if (xn != null)
                {
                    uiPointNum = Convert.ToInt32(xn.InnerText);
                }
                XmlNodeList xnl = xmlDoc.SelectNodes(@"cmlRoot/stPointInfo");
                foreach (XmlNode xNod in xnl)
                {
                    GmpPoint GmpPointTemp = new GmpPoint();
                    XmlNode xmlnode = xNod.SelectSingleNode("szPointType");
                    if (xmlnode!=null)
                    {
                        GmpPointTemp.szPointType = xmlnode.InnerText;
                    }
                    xmlnode = xNod.SelectSingleNode("dPointY");
                    if (xmlnode!=null)
                    {
                        GmpPointTemp.dPointX = double.Parse(xmlnode.InnerText);
                    }
                    xmlnode = xNod.SelectSingleNode("dPointX");
                    if (xmlnode!=null)
                    {
                        GmpPointTemp.dPointY = double.Parse(xmlnode.InnerText);
                    }
                    xmlnode = xNod.SelectSingleNode("speed");
                    if (xmlnode!=null)
                    {
                        GmpPointTemp.speed = double.Parse(xmlnode.InnerText);
                    }
                    xmlnode = xNod.SelectSingleNode("szPSTime");
                    if (xmlnode!=null)
                    {
                        GmpPointTemp.szPSTime = xmlnode.InnerText;
                    }
                    xmlnode = xNod.SelectSingleNode("szPTSTime");
                    if (xmlnode != null)
                    {
                        GmpPointTemp.szPTSTime = xmlnode.InnerText;
                    }
                    xmlnode = xNod.SelectSingleNode("szWSTime");
                    if (xmlnode != null)
                    {
                        GmpPointTemp.szWSTime = xmlnode.InnerText;
                    }
                    xmlnode = xNod.SelectSingleNode("szWTSTime");
                    if (xmlnode != null)
                    {
                        GmpPointTemp.szWTSTime = xmlnode.InnerText;
                    }
                    xmlnode = xNod.SelectSingleNode("szESTime");
                    if (xmlnode != null)
                    {
                        GmpPointTemp.szESTime = xmlnode.InnerText;
                    }
                    xmlnode = xNod.SelectSingleNode("szETSTime");
                    if (xmlnode != null)
                    {
                        GmpPointTemp.szETSTime = xmlnode.InnerText;
                    }
                    xmlnode = xNod.SelectSingleNode("szEETTime");
                    if (xmlnode != null)
                    {
                        GmpPointTemp.szEETTime = xmlnode.InnerText;
                    }
                    GmpPointS.Add(GmpPointTemp);
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
