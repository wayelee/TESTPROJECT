using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;


namespace LibCerMap
{
    //读取文件夹的对话框
    public class FrmFolderDialog:System.Windows.Forms.Design.FolderNameEditor        
    {
        FolderNameEditor.FolderBrowser fDialog = new System.Windows.Forms.Design.FolderNameEditor.FolderBrowser();
        public FrmFolderDialog()
        { }
        public DialogResult DisplayDialog()
        {
            //fDialog.StartLocation = FolderBrowserFolder.Recent;
            return DisplayDialog("请选择一个tin图层文件夹");
        }
        public DialogResult DisplayDialog(string description)
        {
            fDialog.Description = description;
            return fDialog.ShowDialog();
        }
        public string Path
        {
            get
            {
                return fDialog.DirectoryPath;
            }
        }
        ~FrmFolderDialog()
        {
            fDialog.Dispose();
        }
    } 
}
