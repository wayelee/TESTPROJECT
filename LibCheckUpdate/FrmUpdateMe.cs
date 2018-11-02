using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using teboweb;
using System.IO;

namespace LibCheckUpdate
{
    public partial class UpdateMe : Form
    {

        public const string updaterPrefix = "XYZZZZZZZ_";
        private static string processToEnd = "CERMapping";
        private static string postProcess = Application.StartupPath + @"\" + processToEnd + ".exe";
        public static string updater = Application.StartupPath + @"\update.exe";

        public const string updateSuccess = "成功完成更新！";
        public const string updateCurrent = "没有可用的更新！";
        public const string updateInfoError = "获取更新版本文件错误";

        public static List<string> info = new List<string>();

        public UpdateMe()
        {
            InitializeComponent();
        }

        //Update的目标目录应该是bin目录，是执行程序的上一级目录
        private void update_bttn_Click(object sender, EventArgs e)
        {
            update.installUpdateRestart(info[3], info[4], "\"" + GetParentPathofExe(), processToEnd, postProcess, "updated", updater);
            Close();
            Application.Exit();
        }

        private string GetParentPathofExe()
        {
            DirectoryInfo path = new DirectoryInfo(Application.StartupPath);
            string strPath = path.ToString();
            strPath = strPath.Remove(strPath.Length - path.Name.Length);
            return strPath;
        }

        private void unpackCommandline()
        {

            bool commandPresent = false;
            string tempStr = "";

            foreach (string arg in Environment.GetCommandLineArgs())
            {

                if (!commandPresent)
                {

                    commandPresent = arg.Trim().StartsWith("/");

                }

                if (commandPresent)
                {

                    tempStr += arg;

                }

            }


            if (commandPresent)
            {

                if (tempStr.Remove(0, 2) == "updated")
                {

                    updateResult.Visible = true;
                    updateResult.Text = updateSuccess;

                }

            }


        }

        private void UpdateMe_Load(object sender, EventArgs e)
        {
            //update.updateMe(updaterPrefix, GetParentPathofExe() + @"Resource\");
            updateResult.Visible = false;
            Update_bttn.Visible = false;
            unpackCommandline();

        }


        private void checkForUpdate_bttn_Click(object sender, EventArgs e)
        {

            info = update.getUpdateInfo(downloadsurl.Text, versionfilename.Text, Application.StartupPath + @"\", 1);

            if (info == null)
            {

                Update_bttn.Visible = false;
                updateResult.Text = updateInfoError;
                updateResult.Visible = true;

            }
            else
            {

                if (decimal.Parse(info[1]) > decimal.Parse(thisversion.Text))
                {

                    Update_bttn.Visible = true;
                    updateResult.Visible = false;

                }
                else
                {

                    Update_bttn.Visible = false;
                    updateResult.Visible = true;
                    updateResult.Text = updateCurrent;

                }



            }

        }

 



    }
}