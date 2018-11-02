using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using Microsoft.Office.Interop.Excel;
using System.Windows.Forms;
using System.IO;
namespace LibCerMap
{
    public class ST_2DPOS
    {
        public double dx;
        public double dy;
        public ST_2DPOS()
        {
            dx = -9999;       // -9999代表无效值
            dy = -9999;       // -9999代表无效值
        }
    }
    public class PathPoint
    {
        public string szWorkMode;   //行走控制模式类型
        public string szMove;       //前进标示
        public double dCurv;        //路径曲率
        public double dMileage;     //路径里程
        //public ushort usMoveTime;   //路径预估时间（注入数据使用）
        //public uint uiTime;         //路径预估时间（指令时长计算使用）
        //public byte ucMode;         //开闭环控制模式
        public double dAngle;       //原地转向角度
        public string szSign;       //原地转向角度符号
        public int uiPathPointNum; //路径点个数
        public ST_2DPOS[] stPathPoints; //路径点位置
        public PathPoint()
        {
            szWorkMode = "-9999";
            szMove = "-9999";
            dCurv = -9999;
            dMileage = -9999;
            dAngle = -9999;
            szSign = "-9999";
            uiPathPointNum = -9999;
            stPathPoints = new ST_2DPOS[13];
            for (int i = 0; i < 13; i++)
            {
                stPathPoints[i] = new ST_2DPOS();
            }
        }
    }
    public class CPathPoint
    {
        public List<PathPoint> PathPointList;
        public double dCurWorkCSX; //坐标系原点在着陆坐标系X位置
        public double dCurWorkCSY; //坐标系原点在着陆坐标系Y位置
        public double dCurWorkCSZ; //坐标系原点在着陆坐标系Z位置
        public ST_2DPOS stStartPointPos; //导航单元起点位置
        public int uiPathPhaseNum;
        public double startpathAngle;//起始航向角
        public CPathPoint()
        {
            dCurWorkCSX = -9999;
            dCurWorkCSY = -9999;
            dCurWorkCSZ = -9999;
            startpathAngle = 0.0;
            stStartPointPos = new ST_2DPOS();
        }
        public double readStartAngle(string XMLPath)
        {
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(XMLPath);

                return startpathAngle;
            }
            catch (System.Exception ex)
            {
                return startpathAngle;
            }
            
        }
        public bool ReadXML(string XMLPath)
        {
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(XMLPath);
                XmlNode cs = xmlDoc.SelectSingleNode(@"cmlRoot/dCurWorkCSX");
                if (cs != null)
                {
                    dCurWorkCSX = double.Parse(cs.InnerText);
                }
                cs = xmlDoc.SelectSingleNode(@"cmlRoot/dCurWorkCSY");
                if (cs != null)
                {
                    dCurWorkCSY = double.Parse(cs.InnerText);
                }
                cs = xmlDoc.SelectSingleNode(@"cmlRoot/dCurWorkCSZ");
                if (cs != null)
                {
                    dCurWorkCSZ = double.Parse(cs.InnerText);
                }
                cs = xmlDoc.SelectSingleNode(@"cmlRoot/stStartPointPos/dX");
                if (cs != null)
                {
                    //stStartPointPos.dx = double.Parse(cs.InnerText);
                    stStartPointPos.dy = double.Parse(cs.InnerText);
                }
                cs = xmlDoc.SelectSingleNode(@"cmlRoot/stStartPointPos/dY");
                if (cs != null)
                {
                    //stStartPointPos.dy = double.Parse(cs.InnerText);
                    stStartPointPos.dx = double.Parse(cs.InnerText);
                }
                XmlNode xn = xmlDoc.SelectSingleNode(@"cmlRoot/uiPathPhaseNum");
                uiPathPhaseNum = int.Parse(xn.InnerText);
                PathPointList = new List<PathPoint>(int.Parse(xn.InnerText));
                XmlNodeList xnl = xmlDoc.SelectNodes(@"cmlRoot/stPathPhases");
                foreach (XmlNode xnlist in xnl)
                {
                    PathPoint PathPointTemp = new PathPoint();
                    XmlNode NTemp = xnlist.SelectSingleNode("szWorkMode");
                    if (NTemp != null)
                    {
                        PathPointTemp.szWorkMode = NTemp.InnerText;
                    }
                    NTemp = xnlist.SelectSingleNode("szMove");
                    if (NTemp != null)
                    {
                        PathPointTemp.szMove = NTemp.InnerText;
                    }
                    NTemp = xnlist.SelectSingleNode("dCurv");
                    if (NTemp != null)
                    {
                        PathPointTemp.dCurv = double.Parse(NTemp.InnerText);
                    }
                    NTemp = xnlist.SelectSingleNode("dMileage");
                    if (NTemp != null)
                    {
                        PathPointTemp.dMileage = double.Parse(NTemp.InnerText);
                    }
                    NTemp = xnlist.SelectSingleNode("dAngle");
                    if (NTemp != null)
                    {
                        PathPointTemp.dAngle = double.Parse(NTemp.InnerText);
                    }
                    NTemp = xnlist.SelectSingleNode("szSign");
                    if (NTemp != null)
                    {
                        PathPointTemp.szSign = NTemp.InnerText;
                    }
                    NTemp = xnlist.SelectSingleNode("uiPathPointNum");
                    if (NTemp != null)
                    {
                        PathPointTemp.uiPathPointNum = int.Parse(NTemp.InnerText);
                    }
                    XmlNodeList PointsList = xnlist.SelectNodes("stPathPoints");
                    int i = 0;
                    foreach (XmlNode pl in PointsList)
                    {
                        NTemp = pl.SelectSingleNode("dX");
                        //PathPointTemp.stPathPoints[i].dx = double.Parse(NTemp.InnerText);
                        PathPointTemp.stPathPoints[i].dy = double.Parse(NTemp.InnerText);
                        NTemp = pl.SelectSingleNode("dY");
                        //PathPointTemp.stPathPoints[i].dy = double.Parse(NTemp.InnerText);
                        PathPointTemp.stPathPoints[i].dx = double.Parse(NTemp.InnerText);
                        i = i + 1;
                    }
                    PathPointList.Add(PathPointTemp);
                }
                return true;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
    }
    public class CPointInformation
    {
        public int ID;
        public int PointID;
        public double X;
        public double Y;
        public double Z;
        public double omg;
        public double phi;
        public double kap;
        public int width;
        public int height;
        public int fx;
        public int fy;
        public double f;
        public CPointInformation()
        {
            ID = 0;
            PointID = 0;
            X = 0;
            Y = 0;
            Z = 0;
            omg = 0;
            phi = 0;
            kap = 0;
            width = 0;
            height = 0;
            fx = 0;
            fy = 0;
            f = 0;
        }
    }

    public class CViewRange
    {
        public List<CPointInformation> PointInformationList;
        public int count;

        public bool ReadTextFile(string strFile)
        {
            //读取文件
            try
            {
                string strText = "";
                StreamReader sr = new StreamReader(strFile, System.Text.Encoding.Default);
                string strTemp = "";
                count = 0;
                while (sr.Peek() >= 0)
                {
                    strTemp = sr.ReadLine();
                    //读取文本
                    strText += strTemp + "\n";
                    count++;
                }
                sr.Close();

                PointInformationList = new List<CPointInformation>(count);
                for (int i = 0; i < count; i++ )
                {
                    //读取文本
                    string[] strSplitLine;
                    string[] strOri;

                    strSplitLine = System.Text.RegularExpressions.Regex.Split(strText.Trim(), @"\n");
                    CPointInformation pointinfo = new CPointInformation();
                    if (strSplitLine.Length >0)
                    {
                        strOri = System.Text.RegularExpressions.Regex.Split(strSplitLine[i], @" ");
                        pointinfo.ID = int.Parse(strOri[0]);
                        pointinfo.PointID = int.Parse(strOri[1]);
                        pointinfo.X = double.Parse(strOri[2]);
                        pointinfo.Y = double.Parse(strOri[3]);
                        pointinfo.Z = double.Parse(strOri[4]);
                        pointinfo.omg = double.Parse(strOri[5]);
                        pointinfo.phi = double.Parse(strOri[6]);
                        pointinfo.kap = double.Parse(strOri[7]);
                        pointinfo.width = int.Parse(strOri[8]);
                        pointinfo.height = int.Parse(strOri[9]);
                        pointinfo.fx = int.Parse(strOri[10]); 
                        pointinfo.fy = int.Parse(strOri[11]);
                        pointinfo.f = double.Parse(strOri[12]);

                        PointInformationList.Add(pointinfo);
                    }
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
                return false;
            }
            return true;
        }
        public bool ReadExcel(string str)
        {
            try
            {
                Microsoft.Office.Interop.Excel.Application ExcelApp = new Microsoft.Office.Interop.Excel.Application();
                //打开目标文件
                Workbook wbook = ExcelApp.Workbooks.Open(str, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                //设置第一个工作薄
                Worksheet wsheet = (Microsoft.Office.Interop.Excel.Worksheet)wbook.Worksheets[1];
                //获得使用的行数、列数
                int rowCount = wsheet.UsedRange.CurrentRegion.Rows.Count;
                if (rowCount <= 1)
                {
                    System.Windows.Forms.MessageBox.Show("文件中没有数据记录", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return false;
                }
                count = rowCount - 1;
                PointInformationList = new List<CPointInformation>(rowCount - 1);
                int colCount = wsheet.UsedRange.CurrentRegion.Columns.Count;
                if (colCount < 13)
                {
                    System.Windows.Forms.MessageBox.Show("字段个数不正确", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return false;
                }
                object[,] sheetdata = wsheet.UsedRange.get_Value(Type.Missing) as object[,];

                //定义字段索引
                int IDindex = -1;
                int PointIDindex = -1;
                int Xindex = -1;
                int Yindex = -1;
                int Zindex = -1;
                int omgindex = -1;
                int phiindex = -1;
                int kapindex = -1;
                int widthindex = -1;
                int heightindex = -1;
                int fxindex = -1;
                int fyindex = -1;
                int findex = -1;
                for (int i = 1; i <= colCount; i++)
                {
                    switch(sheetdata[1,i].ToString())
                    {
                        case "ID":
                            IDindex = i;
                            break;
                        case "PointID":
                            PointIDindex = i;
                            break;
                        case "X":
                            Xindex = i;
                            break;
                        case "Y":
                            Yindex = i;
                            break;
                        case "Z":
                            Zindex = i;
                            break;
                        case "omg":
                            omgindex = i;
                            break;
                        case "phi":
                            phiindex = i;
                            break;
                        case "kap":
                            kapindex = i;
                            break;
                        case "width":
                            widthindex = i;
                            break;
                        case "height":
                            heightindex = i;
                            break;
                        case "fx":
                            fxindex = i;
                            break;
                        case "fy":
                            fyindex = i;
                            break;
                        case "f":
                            findex = i;
                            break;
                        default:
                            break;
                    }
                }
                if (IDindex == -1)
                {
                    System.Windows.Forms.MessageBox.Show("ID字段缺失", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return false;
                }
                if (PointIDindex == -1)
                {
                    System.Windows.Forms.MessageBox.Show("PointID字段缺失", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return false;
                }
                if (Xindex == -1)
                {
                    System.Windows.Forms.MessageBox.Show("X字段缺失", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return false;
                }
                if (Yindex == -1)
                {
                    System.Windows.Forms.MessageBox.Show("Y字段缺失", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return false;
                }
                if (Zindex == -1)
                {
                    System.Windows.Forms.MessageBox.Show("Z字段缺失", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return false;
                }
                if (omgindex == -1)
                {
                    System.Windows.Forms.MessageBox.Show("omg字段缺失", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return false;
                }
                if (phiindex == -1)
                {
                    System.Windows.Forms.MessageBox.Show("phi字段缺失", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return false;
                }
                if (kapindex == -1)
                {
                    System.Windows.Forms.MessageBox.Show("kap字段缺失", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return false;
                }
                if (widthindex == -1)
                {
                    System.Windows.Forms.MessageBox.Show("width字段缺失", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return false;
                }
                if (heightindex == -1)
                {
                    System.Windows.Forms.MessageBox.Show("height字段缺失", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return false;
                }
                if (fxindex == -1)
                {
                    System.Windows.Forms.MessageBox.Show("fx字段缺失", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return false;
                }
                if (fyindex == -1)
                {
                    System.Windows.Forms.MessageBox.Show("fy字段缺失", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return false;
                }
                if (findex == -1)
                {
                    System.Windows.Forms.MessageBox.Show("f字段缺失", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return false;
                }
                for (int j = 2; j <=rowCount; j++)
                {
                    CPointInformation pointinfo = new CPointInformation();
                    if (sheetdata[j, IDindex] == null)
                    {
                        System.Windows.Forms.MessageBox.Show("请检查第" + j.ToString() + "行ID字段是否为空", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return false;
                    }
                    if (int.TryParse(sheetdata[j,IDindex].ToString(), out pointinfo.ID) == false)
                    {
                        System.Windows.Forms.MessageBox.Show("请检查第" + j.ToString() + "行ID字段是否为整数", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return false;
                    }
                    if (sheetdata[j, PointIDindex] == null)
                    {
                        System.Windows.Forms.MessageBox.Show("请检查第" + j.ToString() + "行PointID字段是否为空", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return false;
                    }
                    if (int.TryParse(sheetdata[j, PointIDindex].ToString(), out pointinfo.PointID) == false)
                    {
                        System.Windows.Forms.MessageBox.Show("请检查第" + j.ToString() + "行PointID字段是否为整数", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return false;
                    }
                    if (sheetdata[j, Xindex] == null)
                    {
                        System.Windows.Forms.MessageBox.Show("请检查第" + j.ToString() + "行X字段是否为空", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return false;
                    }
                    if (double.TryParse(sheetdata[j, Xindex].ToString(), out pointinfo.X) == false)
                    {
                        System.Windows.Forms.MessageBox.Show("请检查第" + j.ToString() + "行X字段是否为整数", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return false;
                    }
                    if (sheetdata[j, Yindex] == null)
                    {
                        System.Windows.Forms.MessageBox.Show("请检查第" + j.ToString() + "行Y字段是否为空", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return false;
                    }
                    if (double.TryParse(sheetdata[j, Yindex].ToString(), out pointinfo.Y) == false)
                    {
                        System.Windows.Forms.MessageBox.Show("请检查第" + j.ToString() + "行Y字段是否为整数", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return false;
                    }
                    if (sheetdata[j, Zindex] == null)
                    {
                        System.Windows.Forms.MessageBox.Show("请检查第" + j.ToString() + "行Z字段是否为空", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return false;
                    }
                    if (double.TryParse(sheetdata[j, Zindex].ToString(), out pointinfo.Z) == false)
                    {
                        System.Windows.Forms.MessageBox.Show("请检查第" + j.ToString() + "行Z字段是否为整数", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return false;
                    }
                    if (sheetdata[j, omgindex] == null)
                    {
                        System.Windows.Forms.MessageBox.Show("请检查第" + j.ToString() + "行omg字段是否为空", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return false;
                    }
                    if (double.TryParse(sheetdata[j, omgindex].ToString(), out pointinfo.omg) == false)
                    {
                        System.Windows.Forms.MessageBox.Show("请检查第" + j.ToString() + "行omg字段是否为整数", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return false;
                    }
                    if (sheetdata[j, phiindex] == null)
                    {
                        System.Windows.Forms.MessageBox.Show("请检查第" + j.ToString() + "行phi字段是否为空", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return false;
                    }
                    if (double.TryParse(sheetdata[j, phiindex].ToString(), out pointinfo.phi) == false)
                    {
                        System.Windows.Forms.MessageBox.Show("请检查第" + j.ToString() + "行phi字段是否为整数", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return false;
                    }
                    if (sheetdata[j, kapindex] == null)
                    {
                        System.Windows.Forms.MessageBox.Show("请检查第" + j.ToString() + "行ka字段是否为空", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return false;
                    }
                    if (double.TryParse(sheetdata[j, kapindex].ToString(), out pointinfo.kap) == false)
                    {
                        System.Windows.Forms.MessageBox.Show("请检查第" + j.ToString() + "行ka字段是否为整数", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return false;
                    }
                    if (sheetdata[j, widthindex] == null)
                    {
                        System.Windows.Forms.MessageBox.Show("请检查第" + j.ToString() + "行width字段是否为空", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return false;
                    }
                    if (int.TryParse(sheetdata[j, widthindex].ToString(), out pointinfo.width) == false)
                    {
                        System.Windows.Forms.MessageBox.Show("请检查第" + j.ToString() + "行width字段是否为整数", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return false;
                    }
                    if (sheetdata[j, heightindex] == null)
                    {
                        System.Windows.Forms.MessageBox.Show("请检查第" + j.ToString() + "行height字段是否为空", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return false;
                    }
                    if (int.TryParse(sheetdata[j, heightindex].ToString(), out pointinfo.height) == false)
                    {
                        System.Windows.Forms.MessageBox.Show("请检查第" + j.ToString() + "行height字段是否为整数", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return false;
                    }
                    if (sheetdata[j, fxindex] == null)
                    {
                        System.Windows.Forms.MessageBox.Show("请检查第" + j.ToString() + "行fx字段是否为空", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return false;
                    }
                    if (int.TryParse(sheetdata[j, fxindex].ToString(), out pointinfo.fx) == false)
                    {
                        System.Windows.Forms.MessageBox.Show("请检查第" + j.ToString() + "行fx字段是否为整数", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return false;
                    }
                    if (sheetdata[j, fyindex] == null)
                    {
                        System.Windows.Forms.MessageBox.Show("请检查第" + j.ToString() + "行fy字段是否为空", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return false;
                    }
                    if (int.TryParse(sheetdata[j, fyindex].ToString(), out pointinfo.fy) == false)
                    {
                        System.Windows.Forms.MessageBox.Show("请检查第" + j.ToString() + "行fy字段是否为整数", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return false;
                    }
                    if (sheetdata[j, findex] == null)
                    {
                        System.Windows.Forms.MessageBox.Show("请检查第" + j.ToString() + "行f字段是否为空", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return false;
                    }
                    if (double.TryParse(sheetdata[j, findex].ToString(), out pointinfo.f) == false)
                    {
                        System.Windows.Forms.MessageBox.Show("请检查第" + j.ToString() + "行f字段是否为整数", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return false;
                    }
                    PointInformationList.Add(pointinfo);
                }
                ExcelApp.Quit();
                ExcelApp = null;
                return true;

            }
            catch(Exception ex)
            {
                return false;
            }
        }
    }

}
