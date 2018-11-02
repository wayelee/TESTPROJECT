using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;


using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.DataSourcesFile;
using ESRI.ArcGIS.DataSourcesRaster;
using ESRI.ArcGIS.Controls;

using DevComponents.DotNetBar;

namespace LibCerMap
{
    public partial class FrmMxdRAR : OfficeForm
    {
        public List<string> layerPathList = new List<string>();
        public FrmMxdRAR()
        {
            InitializeComponent();
        }

        private void BtnWorkBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlgMapSymbol = new OpenFileDialog();
            dlgMapSymbol.Title = "选择Mxd文件";
            dlgMapSymbol.InitialDirectory = ".";
            dlgMapSymbol.Filter = "mxd文件(*.mxd)|*.mxd";
            dlgMapSymbol.RestoreDirectory = true;
            if (dlgMapSymbol.ShowDialog() == DialogResult.OK)
            {
                txtMxdData.Text = dlgMapSymbol.FileName;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fdlg = new FolderBrowserDialog();
            //fdlg.SelectedPath = @"D:\cs";
            if (fdlg.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            if (fdlg.SelectedPath != "")
            {
                //DirectoryInfo dir = Directory.CreateDirectory(fdlg.SelectedPath);
                //fdpath = fdlg.SelectedPath;
                textBoxX1.Text = fdlg.SelectedPath;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                string LaySourcePath = "";

                //首先将mxd文件拷贝到目标路径下
                System.IO.File.Copy(txtMxdData.Text, textBoxX1.Text + "\\" + System.IO.Path.GetFileName(txtMxdData.Text), true);
                //FileCopy(txtMxdData.Text, textBoxX1.Text + "\\" + System.IO.Path.GetFileName(txtMxdData.Text));
                IMapDocument pMapDocumentDest = new MapDocumentClass();
                pMapDocumentDest.Open(textBoxX1.Text + "\\" + System.IO.Path.GetFileName(txtMxdData.Text));

                IMapDocument pMapDocument = new MapDocumentClass();
                pMapDocument.Open(txtMxdData.Text, null);
                IMap pMap = pMapDocument.Map[0];
                for (int i = 0; i < pMap.LayerCount; i++)
                {

                    ILayer pLayer = pMap.Layer[i];
                    //LaySourcePath=pLayer.
                    IDataLayer pDataLayer = pLayer as IDataLayer;
                    IDatasetName pDName = pDataLayer.DataSourceName as IDatasetName;
                    LaySourcePath = pDName.WorkspaceName.PathName;//得到图层的源文件路径
                    string[] strArray = LaySourcePath.Split('\\');
                    if (strArray[strArray.Length - 1].Contains(".gdb"))//如果图层来源于gdb数据库
                    {
                        CopyDirectory(LaySourcePath, textBoxX1.Text + "\\" + strArray[strArray.Length - 1]);
                        ConnectSource(pMapDocumentDest.Map[0].Layer[i], textBoxX1.Text + "\\" + strArray[strArray.Length - 1], 0);
                    }
                    else if (!strArray[strArray.Length - 1].Contains(".gdb"))//图层来源不是gdb数据库，则只能为shp图层或TIF影像
                    {
                        if (pLayer is IFeatureLayer)//图层为矢量图
                        {
                            ExportDatatoShape(textBoxX1.Text + "\\" + pLayer.Name, pLayer);
                            ConnectSource(pMapDocumentDest.Map[0].Layer[i], textBoxX1.Text, 1);
                        }
                        else if (pLayer is IRasterLayer)
                        {
                            //File.Copy(LaySourcePath + "\\" + pDName.Name, textBoxX1.Text + "\\" + pLayer.Name);
                            //FileCopy(LaySourcePath + "\\" + pDName.Name, textBoxX1.Text + "\\" + pLayer.Name);
                            File.Copy(LaySourcePath + "\\" + pDName.Name, textBoxX1.Text + "\\" + pDName.Name);
                            ConnectSource(pMapDocumentDest.Map[0].Layer[i], textBoxX1.Text, 2);
                        }
                    }
                }

                //以相对路径存储新目录下Mxd文件
                pMapDocumentDest.Save(true, true);

                pMapDocumentDest.Close();

                //压缩文件
                ClsFileZip filezip = new ClsFileZip();
                filezip.ZipFileMain(textBoxX1.Text + "\\", textBoxX1.Text + ".zip", "*.*");

                this.Close();
            }
            catch (System.Exception ex)
            {
                if (ex.ToString().Contains("未能找到路径"))
                {
                    MessageBox.Show("Mxd文档对应的源文件被移动", "提示", MessageBoxButtons.OK);
                }
                else
                {
                    MessageBox.Show(ex.ToString());
                }
            }


        }

        /// <summary>
        /// 复制文件夹以及文件夹下所有文件和文件夹
        /// </summary>
        /// <param name="sourcePath"></param>
        /// <param name="destinationPath"></param>
        public void CopyDirectory(string sourcePath, string destinationPath)
        {
            try
            {
                DirectoryInfo info = new DirectoryInfo(sourcePath);
                Directory.CreateDirectory(destinationPath);
                foreach (FileSystemInfo fsi in info.GetFileSystemInfos())
                {
                    string destName = System.IO.Path.Combine(destinationPath, fsi.Name);
                    if (fsi is System.IO.FileInfo)//如果是文件，复制文件
                    {
                        File.Copy(fsi.FullName, destName);
                        //FileCopy(fsi.FullName, destName);
                    }
                    else                          //如果是文件夹，新建文件夹，递归
                    {
                        Directory.CreateDirectory(destName);
                        CopyDirectory(fsi.FullName, destName);
                    }
                }
            }
            catch (System.Exception ex)
            {
                
            }            
        }

        public void ExportDatatoShape(string strFullName,ILayer m_pLayer)
        {
            ClsGDBDataCommon comm = new ClsGDBDataCommon();
            
            
            string strPath = System.IO.Path.GetDirectoryName(strFullName);
            string strName = System.IO.Path.GetFileName(strFullName);

            //导出数据
            IFeatureLayer pFtlayer = (IFeatureLayer)m_pLayer;
            IFeatureClass pFtClass = pFtlayer.FeatureClass;
            IFields pFields = pFtClass.Fields;
            //设置空间参考
            ISpatialReference pSpatialRef;
           
            IGeoDataset pGeo = (IGeoDataset)pFtlayer;
            pSpatialRef = pGeo.SpatialReference;
           
            IFeatureClass pFtClassNew = comm.CreateShapefile(strPath, strName, pFields, pSpatialRef);
            if (pFtClassNew == null)
            {
                MessageBox.Show("创建shp文件失败!");
                return;
            }
            //////////////////////////////////////////////////////////////////////////
            IDataset dataset = (IDataset)pFtClassNew;
            IWorkspace workspace = dataset.Workspace;

            //Cast for an IWorkspaceEdit
            IWorkspaceEdit workspaceEdit = (IWorkspaceEdit)workspace;
            workspaceEdit.StartEditing(true);
            workspaceEdit.StartEditOperation();

            IFeatureBuffer featureBuffer;
            IFeatureCursor featureCursor = null;
            object featureOID;

            //////////////////////////////////////////////////////////////////////////

            IFeature pFeature;
            long nCount;
            long nProcess = 0;

            ITable pTable = (ITable)m_pLayer;
            nCount = pTable.RowCount(null);

            IFeatureCursor pFtCursor = pFtlayer.Search(null, true);
            if (pFtCursor == null) return;
            pFeature = pFtCursor.NextFeature();
            while (pFeature != null)
            {
                //////////////////////////////////////////////////////////////////////////
                featureBuffer = pFtClassNew.CreateFeatureBuffer();
                featureCursor = pFtClassNew.Insert(true);

                featureBuffer.Shape = pFeature.ShapeCopy;
                CopyFeatureField(pFeature, featureBuffer);
                featureOID = featureCursor.InsertFeature(featureBuffer);

                nProcess++;
                this.Text = string.Format("共有:{0}条数据，已处理:{1}条", nCount, nProcess);
                pFeature = pFtCursor.NextFeature();
            }
           
            featureCursor.Flush();
            workspaceEdit.StopEditOperation();
            workspaceEdit.StopEditing(true);

            IFeatureLayer pOutFL = new FeatureLayerClass();
            pOutFL.FeatureClass = pFtClassNew;
            pOutFL.Name = pFtClassNew.AliasName;
        }

        public void CopyFeatureField(IFeature pFt, IFeatureBuffer pFtNew)
        {
            IFields pFlds = pFt.Fields;
            IFields pFldsNew = pFtNew.Fields;
            int nCount = pFlds.FieldCount;
            for (int i = 0; i < nCount; i++)
            {
                if (pFlds.get_Field(i).Type != esriFieldType.esriFieldTypeGeometry &&
                   pFlds.get_Field(i).Type != esriFieldType.esriFieldTypeOID)
                {
                    pFtNew.set_Value(i, (object)pFt.get_Value(i));
                }
            }

        }

        public void ConnectSource(ILayer pLayer,string ConnectFloder,int mode)
        {
            switch (mode)
            {
                case 0:          //图层来源为gdb
                    IFeatureLayer pFlayer = pLayer as IFeatureLayer;
                    IDataLayer2 pDLayer = pFlayer as IDataLayer2;
                    IDatasetName pDsName = pDLayer.DataSourceName as IDatasetName;
                    IWorkspaceName ws = pDsName.WorkspaceName;
                    ws.WorkspaceFactoryProgID = "esriDataSourcesGDB.FileGDBWorkspaceFactory";
                    ws.PathName = ConnectFloder;
                    pDsName.WorkspaceName = ws;
                    break;
                case 1:         //图层来源为shp文件
                    IFeatureLayer pFLayer = pLayer as IFeatureLayer;
                    IDataLayer2 pDlayer = pFLayer as IDataLayer2;
                    IDatasetName pDsname = pDlayer.DataSourceName as IDatasetName;
                    IWorkspaceName wsf = pDsname.WorkspaceName;
                    wsf.PathName = ConnectFloder;
                    pDsname.WorkspaceName = wsf;
                    break; 
                case 2:         //图层来源为栅格图像
                    IRasterLayer pRlayer = pLayer as IRasterLayer;
                    IDataLayer2 pDlayerr = pRlayer as IDataLayer2;
                    IDatasetName pDsnamer = pDlayerr.DataSourceName as IDatasetName;
                    IWorkspaceName wsfr = pDsnamer.WorkspaceName;
                    wsfr.PathName = ConnectFloder;
                    pDsnamer.WorkspaceName = wsfr;
                    break;
            }
        }

        private void btnCancle_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 利用filestream实现文件拷贝，该函数也可应用于大文件拷贝
        /// </summary>
        /// <param name="sourcePath">源文件路径</param>
        /// <param name="destPath">目标文件路径</param>
        public void FileCopy(string sourcePath, string destPath)
        {
            //创建一个文件流指向源文件
            System.IO.FileStream fsRead = new System.IO.FileStream(sourcePath, FileMode.Open);
            //创建一个文件流指向目标文件
            System.IO.FileStream fsWrite = new System.IO.FileStream(destPath, FileMode.Create);

            //记录一下文件的长度
            long fileLength = fsRead.Length;
            //定义一个1M的缓存
            byte[] buffer = new byte[1024 * 1024];
            //先读取一次并且将读取的真正内容长度记录下来
            int readLength = fsRead.Read(buffer, 0, buffer.Length);
            //用来记录已经将多少内容写入到文件中
            long readCount = 0;
            
            //只要读取到的内容不为0就接着读
            while (readLength!=0)
            {
                //将前面读取到内存的数据写入文件
                fsWrite.Write(buffer, 0, readLength);
                //已经读取的数量累加
                readCount += readLength;

                ////计算已经读取的数据百分比
                //int percentage = (int)(readCount * 100 / fileLength);
                //this.ptpgressBar1.Value = percentage;
                
                //进行下一次读取
                readLength = fsRead.Read(buffer, 0, buffer.Length);
            }

            fsRead.Close();
            fsWrite.Close();
            buffer = null;
            //回收内存
            GC.Collect();
        }
    }
}
