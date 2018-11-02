//远程数据库查询操作类

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;

namespace LibCerMap
{
    public struct _ZM_MsgHead                 //消息头
    {
        public uint uiMsgType;          /* 消息类型  */
        public uint uiTaskCode;         /* 任务码    */
        public uint uiObjCode;          /* 目标码    */
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct _ZM_Addr                  //电文地址
    {
        public uint uiHostIP;               // 主机IP
        public uint uiProcID;               // 进程ID
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 8)]
        public String szProcName;           // 进程名
    }

    public struct _ZF_FileHead                //文件头
    {
        public uint uiFileType;         /* 文件类型    */
        public uint uiTaskCode;         /* 任务码     */
        public uint uiObjCode;          /* 目标码     */
    }

    public struct _ZF_FILETYPE
    {
        public int nCode;
        public string strFileType;
        public string strDescription;
    }

    public class ClsZfDatabase
    {
        //[DllImport("LibZmsgDLL.dll", EntryPoint = "zfget", CallingConvention = CallingConvention.Cdecl)]
        //static extern int zfget(string szfileName, string szfilenames, int uiFileTypes, int uiTaskCode, int uiObjCode);

        public ClsZfDatabase()
        {

        }

        public bool InitialZF_FileType(List<_ZF_FILETYPE> listFielType)
        {
            string strFile = ClsGDBDataCommon.GetParentPathofExe() + @"Resource\Configure\ZF_FileType.cfg";
            //读取文件
            try
            {
                //string strFileName = dlg.FileName;
                StreamReader sr = new StreamReader(strFile, System.Text.Encoding.Default);
                string strTemp = "";
                while (sr.Peek() >= 0)
                {
                    _ZF_FILETYPE fileType = new _ZF_FILETYPE();
                    strTemp = sr.ReadLine();
                    string[] strSplitLine;
                    char[] c = new char[ ] {' '};
                    strSplitLine = strTemp.Split(c,StringSplitOptions.RemoveEmptyEntries);
                    if (strSplitLine.Length >= 3)
                    {
                        fileType.nCode = int.Parse(strSplitLine[0]);
                        fileType.strFileType = strSplitLine[1];
                        fileType.strDescription = strSplitLine[2];
                    }
                    listFielType.Add(fileType);
                }
                sr.Close();
            }
            catch (Exception ee)
            {
                return false;
            }

            return true;
        }

        public static int GetZF_FileTypeIndex(List<_ZF_FILETYPE> listFielType,string strDescription)
        {
            int nIndex = -1;
            for (int i = 0; i < listFielType.Count;i++ )
            {
                if (strDescription == listFielType[i].strDescription)
                {
                    nIndex = i;// listFielType[i].nCode;
                    break;
                }
            }

            return nIndex;
        }
        //数据查询
        public int DataQuery()
        {
            int nCount = 0;

            return nCount;
        }

        //数据下载
        public bool DataDownLoad(string strFileName,string strFileNames,int nFileType,int nTaskCode,int nObjCode)
        {
            //int i = zfget(strFileName, strFileNames, nFileType, nTaskCode, nObjCode);
            //if (i != 1) return false;
            return true;
        }
    }
}
