using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;
using Ionic.Zip;
using System.Diagnostics;
using System.Threading;
using teboweb;


namespace update
{
    //Procesing
    //Remove previous download file
    //Download file
    //Unzip file contents to temp folder
    //Remove files from destination folder present in temp folder
    //Move unzipped files to destination folder
    //Remove download file
    //Remove temp folder
        

    public partial class update : Form
    {
        bool called = true;

        private string tempDownloadFolder = "";
        private string processToEnd = "";
        private string downloadFile = "";
        private string URL = "";
        private string destinationFolder = "";
        private string updateFolder = GetParentPathofExe() + @"updates\";
        private string postProcessFile = "";
        private string postProcessCommand = "";

        delegate void SetLabelCallback(Label label, string text);

        //得到执行程序的上一级目录
        private static string GetParentPathofExe()
        {
            DirectoryInfo path = new DirectoryInfo(Application.StartupPath);
            string strPath = path.ToString();
            strPath = strPath.Remove(strPath.Length - path.Name.Length);
            return strPath;
        }

        public void SetLabel(Label label, string text)
        {
            if (label.InvokeRequired)
            {
                SetLabelCallback d = new SetLabelCallback(SetLabel);
                label.Invoke(d, new object[] { label, text });
            }
            else
            {
                label.Text = text;
                label.Refresh();
                Invalidate();
            }
        }

        public update()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            Hide();
           
            if (called)
            {
                WindowState = FormWindowState.Normal;
                Show();

                BackgroundWorker bw = new BackgroundWorker();

                bw.DoWork -= new DoWorkEventHandler(backgroundWorker);
                bw.DoWork += new DoWorkEventHandler(backgroundWorker);
                bw.WorkerSupportsCancellation = true;
                bw.RunWorkerAsync();

            }

        }
       
        private void backgroundWorker(object sender, DoWorkEventArgs e)
        {

            preDownload();

            if (called)
            {
                WindowState = FormWindowState.Normal;
                Show();

                SetLabel(line1, "停止 " + processToEnd);
                Thread.Sleep(1000);

                try
                {

                    Process[] processes = Process.GetProcesses();

                    foreach (Process process in processes)
                    {
                        if (process.ProcessName == processToEnd)
                        {
                            process.Kill();
                        }
                    }

                }
                catch (Exception)
                { }


                webdata.bytesDownloaded += Bytesdownloaded;
                webdata.downloadFromWeb(URL, downloadFile, tempDownloadFolder);

                SetLabel(line1, "解压缩包...");
                Thread.Sleep(1000);
                unZip(tempDownloadFolder + downloadFile, tempDownloadFolder);
                //unZip(tempDownloadFolder + downloadFile, destinationFolder);
                SetLabel(line1, "更新原文件...");
                Thread.Sleep(1000);
                moveFiles(tempDownloadFolder,destinationFolder);
                SetLabel(line1, "清理临时文件...");
                Thread.Sleep(1000);
                wrapUp();
                if (postProcessFile != "") postDownload();

            }

            Close();


        }


        
        private void unpackCommandline()
        {

            string cmdLn = "";

            foreach (string arg in Environment.GetCommandLineArgs())
            {
                cmdLn += arg;

            }

            if (cmdLn.IndexOf('|') == -1)
            {
                called = false;
                FrmInfo info = new FrmInfo();
                info.ShowDialog();
                Close();
            }

           //cmdLn =@"|downloadFile|bin.zip|URL|\\210.72.27.172\Data\update\|destinationFolder|"D:\SVN-Document\Program\CE-3\CERMapping\bin\|processToEnd|CERMapping|postProcess|D:\SVN-Document\Program\CE-3\CERMapping\bin\CERMapping.exe|command| / updated";		
            string[] tmpCmd = cmdLn.Split('|');
 

            for (int i = 1; i < tmpCmd.GetLength(0); i++)
            {
                if (tmpCmd[i] == "downloadFile") downloadFile = tmpCmd[i + 1];
                if (tmpCmd[i] == "URL") URL = tmpCmd[i + 1];
                if (tmpCmd[i] == "destinationFolder") destinationFolder = tmpCmd[i + 1];
                if (tmpCmd[i] == "processToEnd") processToEnd = tmpCmd[i + 1];
                if (tmpCmd[i] == "postProcess") postProcessFile = tmpCmd[i + 1];
                if (tmpCmd[i] == "command") postProcessCommand += @" /" + tmpCmd[i + 1];
                i++;
            }

            //test
            //downloadFile = "Bin.zip";
            //URL = @"\\11.10.70.42\sharedoc\";
            //destinationFolder = @"D:\SVN_DOC\CerMapping\Bin\";
            //processToEnd = "CERMapping";
            //postProcessFile = @"D:\SVN_DOC\CerMapping\Bin\CERMapping.exe";
            //postProcessCommand += @" /" + @"/ updated";
        }

        private void unZip(string file, string unZipTo)
        {
            try
            {
                // Specifying Console.Out here causes diagnostic msgs to be sent to the Console
                // In a WinForms or WPF or Web app, you could specify nothing, or an alternate
                // TextWriter to capture diagnostic messages. 

                using (ZipFile zip = ZipFile.Read(file,System.Text.Encoding.Default))
                {
                    // This call to ExtractAll() assumes:
                    //   - none of the entries are password-protected.
                    //   - want to extract all entries to current working directory
                    //   - none of the files in the zip already exist in the directory;
                    //     if they do, the method will throw.
                    zip.ExtractAll(unZipTo);
                }
            }
            catch (System.Exception)
            {
            }
        }


        private void preDownload()
        {

            if (!Directory.Exists(updateFolder)) Directory.CreateDirectory(updateFolder);

            tempDownloadFolder = updateFolder + DateTime.Now.ToString("yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture) + @"\";

            if (Directory.Exists(tempDownloadFolder))
            {
                Directory.Delete(tempDownloadFolder, true);
            }

            Directory.CreateDirectory(tempDownloadFolder);

            unpackCommandline();

        }

        private void postDownload()
        {

            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = postProcessFile;
            startInfo.Arguments = postProcessCommand;
            Process.Start(startInfo);

        }


        private void wrapUp()
        {

            if (Directory.Exists(tempDownloadFolder))
            {
                Directory.Delete(tempDownloadFolder, true);
            }

        }


        private void moveFiles(string srcPath, string aimPath)
        {
            try
            {
                // 检查目标目录是否以目录分割字符结束如果不是则添加
                if (aimPath[aimPath.Length - 1] != System.IO.Path.DirectorySeparatorChar)
                {
                    aimPath += System.IO.Path.DirectorySeparatorChar;
                }
                // 判断目标目录是否存在如果不存在则新建
                if (!System.IO.Directory.Exists(aimPath))
                {
                    System.IO.Directory.CreateDirectory(aimPath);
                }
                string[] fileList = System.IO.Directory.GetFileSystemEntries(srcPath);
                foreach (string file in fileList)
                {
                    // 先当作目录处理如果存在这个目录就递归Copy该目录下面的文件
                    if (System.IO.Directory.Exists(file))
                    {
                        moveFiles(file, aimPath + System.IO.Path.GetFileName(file));
                    }
                    // 否则直接Copy文件
                    else
                    {
                        if (System.IO.Path.GetFileName(file) != downloadFile)
                        {
                            try//在此可以继续更新其他文件
                            {
                                System.IO.File.Copy(file, aimPath + System.IO.Path.GetFileName(file), true);
                            }
                            catch (System.Exception ex)
                            {
                                //MessageBox.Show(ex.Message);
                            }   
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

 
        private void Bytesdownloaded(ByteArgs e)
        {

            progressBar1.Maximum = e.total;

            if (progressBar1.Value + e.downloaded <= progressBar1.Maximum)
            {
                progressBar1.Value += e.downloaded;
                SetLabel(line1, "下载更新...");
            }
            else
            {
                SetLabel(line1, "下载完成.");
            }

            progressBar1.Refresh();
            Invalidate();

        }



    }
}