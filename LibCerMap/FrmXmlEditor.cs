using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.IO;
using ESRI.ArcGIS.Carto;

namespace LibCerMap
{
    public partial class FrmXmlEditor : Form
    {
        //保存模型
        private List<Model> pListModels=new List<Model>();
        private Model m_CurrentModel = new Model();

        public IRasterLayer m_pRasterLayer = null;

        public List<Model> ListModels
        {
            get { return pListModels; }
            set { pListModels = value; }
        }

        public FrmXmlEditor(IRasterLayer pRasterLayer)
        {
            InitializeComponent();
            m_pRasterLayer = pRasterLayer;
        }

        private void cmbModelType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbModelType.SelectedItem==cmiRock)
                dbiDepth.Enabled = false;
            else
                dbiDepth.Enabled = true;
        }

        private bool readModelInfoFromXml(string szFilename)
        {
            //文件名空，则直接返回
            if (String.IsNullOrEmpty(szFilename))
                return false;

            //先清空
            pListModels.Clear();
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(szFilename);

                //读取模型的个数
                int nModelNum = int.MinValue;
                XmlNode xnModelNum = xmlDoc.SelectSingleNode(@"Models/Num");
                if (xnModelNum != null)
                {
                    nModelNum = int.Parse(xnModelNum.InnerText);
                    if (nModelNum <= 0)
                        return false;
                }

                XmlNodeList xnl = xmlDoc.SelectNodes(@"Models/Model");
                foreach (XmlNode xNod in xnl)
                {
                    Model tmpModel = new Model();

                    //模型标识
                    XmlNode xmlnode = xNod.SelectSingleNode(@"ModelID");
                    if (xmlnode != null)
                    {
                        tmpModel.szModelID = xmlnode.InnerText;
                    }

                    //模型类型
                    xmlnode = xNod.SelectSingleNode(@"ModelType");
                    if (xmlnode != null)
                    {
                        tmpModel.szModelType = xmlnode.InnerText;
                    }

                    //地理坐标
                    xmlnode = xNod.SelectSingleNode(@"Coordinate/GeoX");
                    if (xmlnode != null)
                    {
                        tmpModel.x = Double.Parse(xmlnode.InnerText);
                    }

                    xmlnode = xNod.SelectSingleNode(@"Coordinate/GeoY");
                    if (xmlnode != null)
                    {
                        tmpModel.y = Double.Parse(xmlnode.InnerText);
                    }

                    //大小
                    xmlnode = xNod.SelectSingleNode(@"Size");
                    if (xmlnode != null)
                    {
                        tmpModel.dbSize = Double.Parse(xmlnode.InnerText);
                    }

                    //深度
                    xmlnode = xNod.SelectSingleNode(@"Depth");
                    if (xmlnode != null)
                    {
                        tmpModel.dbDepth = Double.Parse(xmlnode.InnerText);
                    }

                    pListModels.Add(tmpModel);
                }
            }
            catch (System.Exception ex)
            {
                return false;
            }

            //更新
            if (pListModels.Count > 0)
            {
                listModelList.SelectedIndex = 0;
                updateCurrentModelInfo();
            }

            return true;
        }

        private bool writeModelInfoToXml(string szFilename)
        {
            if (string.IsNullOrEmpty(szFilename))
                return false;

            int nSize = pListModels.Count;
            if (nSize < 0)
                return false;

            XmlDocument doc = new XmlDocument(); // 创建XML对象

            XmlElement root = doc.CreateElement("Models"); // 创建根节点Models
            doc.AppendChild(root);    //  加入到xml document

            XmlElement num = doc.CreateElement("Num"); // 创建节点Num
            XmlText text = doc.CreateTextNode(nSize.ToString());
            num.AppendChild(text);
            root.AppendChild(num);    //  加入到xml document

            //循环添加模型
            for (int i = 0; i < nSize; i++)
            {
                XmlElement tmpModel = doc.CreateElement("Model");

                //添加模型标识
                XmlElement tmpFlag = doc.CreateElement("ModelID");
                XmlText tmpText = doc.CreateTextNode(pListModels[i].szModelID);
                tmpFlag.AppendChild(tmpText);
                tmpModel.AppendChild(tmpFlag);

                //添加模型类型
                tmpFlag = doc.CreateElement("ModelType");
                tmpText = doc.CreateTextNode(pListModels[i].szModelType);
                tmpFlag.AppendChild(tmpText);
                tmpModel.AppendChild(tmpFlag);

                //添加地理位置信息
                XmlElement tmpCoordinate = doc.CreateElement("Coordinate");
                XmlElement tmpX = doc.CreateElement("GeoX");
                tmpText = doc.CreateTextNode(pListModels[i].x.ToString());
                tmpX.AppendChild(tmpText);

                XmlElement tmpY = doc.CreateElement("GeoY");
                tmpText = doc.CreateTextNode(pListModels[i].y.ToString());
                tmpY.AppendChild(tmpText);

                tmpCoordinate.AppendChild(tmpX);
                tmpCoordinate.AppendChild(tmpY);
                tmpModel.AppendChild(tmpCoordinate);

                //添加大小信息
                XmlElement tmpSize = doc.CreateElement("Size");
                tmpText = doc.CreateTextNode(pListModels[i].dbSize.ToString());
                tmpSize.AppendChild(tmpText);
                tmpModel.AppendChild(tmpSize);

                //添加深度信息
                XmlElement tmpDepth = doc.CreateElement("Depth");
                tmpText = doc.CreateTextNode(pListModels[i].dbDepth.ToString());
                tmpDepth.AppendChild(tmpText);
                tmpModel.AppendChild(tmpDepth);

                //往根节点里添加模型信息
                root.AppendChild(tmpModel);
            }

            try
            {
                doc.Save(szFilename);
            }
            catch (System.Exception ex)
            {
                return false;
            }

            return true;
        }

        private void updateListInfo()
        {
            int nCount=pListModels.Count;

            //先清空
            listModelList.Items.Clear();

            for(int i=0;i<nCount;i++)
            {
                if (string.IsNullOrEmpty(pListModels[i].szModelID)) 
                    listModelList.Items.Add("IDUnknown");
                else
                    listModelList.Items.Add(pListModels[i].szModelID);
            }
        }

        private void updateCurrentModelInfo()
        {
            int nIndex = listModelList.SelectedIndex;
            if (nIndex < 0)
                return;

            //更新界面信息
            m_CurrentModel = pListModels[nIndex];
            if (m_CurrentModel == null)
                return;

            if (m_CurrentModel.szModelType.ToUpper().Contains("CRATER"))
            {
                cmbModelType.SelectedItem = cmiCrater;
                dbiDepth.Enabled = true;
                dbiDepth.Text = m_CurrentModel.dbDepth.ToString();
            }
            else
            {
                cmbModelType.SelectedItem = cmiRock;
                dbiDepth.Enabled = false;
            }

            if(string.IsNullOrEmpty(m_CurrentModel.szModelID))
                txtModelID.Text = "IDUnknown";
            else
                txtModelID.Text = m_CurrentModel.szModelID;
            dbiSize.Text = m_CurrentModel.dbSize.ToString();
            dbiGeoX.Text = m_CurrentModel.x.ToString();
            dbiGeoY.Text = m_CurrentModel.y.ToString();

            ////更新列表框信息
            //int nCount = pListModels.Count;
            //for (int i = 0; i < nCount; i++)
            //{
            //    if (string.IsNullOrEmpty(pListModels[i].szModelID))
            //        listModelList.Items[i]="IDUnknown";
            //    else
            //        listModelList.Items[i]=pListModels[i].szModelID;
            //}
            //listModelList.SelectedIndex = nIndex;
        }

        private void btnXmlFilename_Click(object sender, EventArgs e)
        {
            OpenFileDialog openfile = new OpenFileDialog();
            openfile.Filter = "相机姿态文件(*.xml)|*.xml|所有文件|*.*";
            if (openfile.ShowDialog() == DialogResult.OK)
            {
                txtXmlFilename.Text = openfile.FileName;

                //XML解析成功，更新列表
                if(readModelInfoFromXml(txtXmlFilename.Text))
                    updateListInfo();
            }
        }

        private void listModelList_SelectedIndexChanged(object sender, EventArgs e)
        {
            //保留更新信息
            if (cmbModelType.SelectedItem == cmiCrater)
                m_CurrentModel.szModelType = "Crater";
            else
                m_CurrentModel.szModelType = "Rock";

            m_CurrentModel.szModelID = txtModelID.Text;
            m_CurrentModel.dbDepth = dbiDepth.Value;
            m_CurrentModel.dbSize = dbiSize.Value;
            m_CurrentModel.x = dbiGeoX.Value;
            m_CurrentModel.y = dbiGeoY.Value;

            updateCurrentModelInfo();
            //updateListInfo();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            //删除
            int nIndex = listModelList.SelectedIndex;
            if (nIndex < 0)
                return;

            pListModels.RemoveAt(nIndex);
            updateListInfo();

            //更新
            listModelList.SelectedIndex = Math.Min(pListModels.Count - 1, nIndex);
            updateCurrentModelInfo();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            //增加
            Model pTmpModel = new Model();

            if(cmbModelType.SelectedItem==cmiCrater)
                pTmpModel.szModelType="Crater";
            else
                pTmpModel.szModelType = "Rock";

            pTmpModel.szModelID = txtModelID.Text;
            pTmpModel.dbDepth=dbiDepth.Value;
            pTmpModel.dbSize=dbiSize.Value;
            pTmpModel.x=dbiGeoX.Value;
            pTmpModel.y=dbiGeoY.Value;

            int nIndex = listModelList.SelectedIndex;
            nIndex = Math.Max(0, nIndex);
            pListModels.Insert(nIndex, pTmpModel);
            updateListInfo();

            //更新
            //listModelList.SelectedIndex = Math.Min(pListModels.Count - 1, nIndex);
            updateCurrentModelInfo();
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            if (cmbModelType.SelectedItem == cmiCrater)
                m_CurrentModel.szModelType = "Crater";
            else
                m_CurrentModel.szModelType = "Rock";

            m_CurrentModel.szModelID = txtModelID.Text;
            m_CurrentModel.dbDepth = dbiDepth.Value;
            m_CurrentModel.dbSize = dbiSize.Value;
            m_CurrentModel.x = dbiGeoX.Value;
            m_CurrentModel.y = dbiGeoY.Value;
        }

        private void btnSaveAs_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlgOutputFile = new SaveFileDialog();
            dlgOutputFile.Title = "选择输出文件路径：";
            dlgOutputFile.InitialDirectory = ".";
            dlgOutputFile.Filter = "XML文件(*.xml;*.XML)|*.XML;*.xml|所有文件(*.*)|*.*";
            dlgOutputFile.RestoreDirectory = true;
            dlgOutputFile.DefaultExt = "xml";

            if (dlgOutputFile.ShowDialog() == DialogResult.OK)
            {
                string szOutputFilename = dlgOutputFile.FileName;
                try
                {
                    if (writeModelInfoToXml(szOutputFilename))
                        MessageBox.Show("保存成功！");
                    else
                        MessageBox.Show("保存失败！");
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                string szFilename = txtXmlFilename.Text;
                if (!string.IsNullOrEmpty(szFilename))
                {
                    File.Delete(szFilename);
                }
                else
                {
                    DateTime dt=DateTime.Now;
                    string szAppendix = "_" + dt.Year.ToString() + dt.Month.ToString() + dt.Day.ToString() + "_" + dt.Hour.ToString() + dt.Minute.ToString() + dt.Second.ToString();
                    szFilename=ClsGDBDataCommon.GetParentPathofExe() + @"Resource\ModelInfo" + szAppendix + ".xml";
                }
                    
                writeModelInfoToXml(szFilename);

                //更新图层
                if(m_pRasterLayer!=null)
                {
                    ClsAddModelToTerrain pAddModelToTerrain = new ClsAddModelToTerrain();
                    pAddModelToTerrain.addModelToTerrain(szFilename, m_pRasterLayer);
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);            	
            }
        }

        private void FrmXmlEditor_Load(object sender, EventArgs e)
        {
            updateListInfo();

            //更新
            if (pListModels.Count > 0)
            {
                listModelList.SelectedIndex = 0;
                updateCurrentModelInfo();
            }
        }

        private void dbiGeoX_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
