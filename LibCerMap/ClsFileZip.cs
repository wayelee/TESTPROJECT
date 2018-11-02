using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;
using ICSharpCode.SharpZipLib.Checksums;
using ICSharpCode.SharpZipLib.Zip;
using ICSharpCode.SharpZipLib.GZip;

namespace LibCerMap
{
    class ClsFileZip
    {
        public string cutStr = "";

        public void ZipFile(string FileToZip, string ZipedFile, int CompressionLevel, int BlockSize)
        {
            //如果文件没有找到则报错。
            if (!System.IO.File.Exists(FileToZip))
            {
                throw new System.IO.FileNotFoundException("The specified file " + FileToZip + " could not be found. Zipping aborderd");
            }

            System.IO.FileStream StreamToZip = new System.IO.FileStream(FileToZip, System.IO.FileMode.Open, System.IO.FileAccess.Read);
            System.IO.FileStream ZipFile = System.IO.File.Create(ZipedFile);
            ZipOutputStream ZipStream = new ZipOutputStream(ZipFile);
            ZipEntry ZipEntry = new ZipEntry("ZippedFile");
            ZipStream.PutNextEntry(ZipEntry);
            ZipStream.SetLevel(CompressionLevel);
            byte[] buffer = new byte[BlockSize];
            System.Int32 size = StreamToZip.Read(buffer, 0, buffer.Length);
            ZipStream.Write(buffer, 0, size);
            try
            {
                while (size < StreamToZip.Length)
                {
                    int sizeRead = StreamToZip.Read(buffer, 0, buffer.Length);
                    ZipStream.Write(buffer, 0, sizeRead);
                    size += sizeRead;
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            ZipStream.Finish();
            ZipStream.Close();
            StreamToZip.Close();
        }
        //Get all DirectoryInfo
        private void direct(DirectoryInfo di, ref ZipOutputStream s, Crc32 crc)
        {
            //DirectoryInfo di = new DirectoryInfo(filenames);
            DirectoryInfo[] dirs = di.GetDirectories("*");

            //遍历目录下面的所有的子目录
            foreach (DirectoryInfo dirNext in dirs)
            {
                //将该目录下的所有文件添加到 ZipOutputStream s 压缩流里面
                FileInfo[] a = dirNext.GetFiles();
                this.writeStream(ref s, a, crc);

                //递归调用直到把所有的目录遍历完成
                direct(dirNext, ref s, crc);
            }
        }

        private void writeStream(ref ZipOutputStream s, FileInfo[] a, Crc32 crc)
        {
            foreach (FileInfo fi in a)
            {
                //string fifn = fi.FullName;
                FileStream fs = fi.OpenRead();

                byte[] buffer = new byte[fs.Length];
                fs.Read(buffer, 0, buffer.Length);


                //ZipEntry entry = new ZipEntry(file);    Path.GetFileName(file)
                string file = fi.FullName;
                file = file.Replace(cutStr, "");

                ZipEntry entry = new ZipEntry(file);
                entry.DateTime = DateTime.Now;

                // set Size and the crc, because the information
                // about the size and crc should be stored in the header
                // if it is not set it is automatically written in the footer.
                // (in this case size == crc == -1 in the header)
                // Some ZIP programs have problems with zip files that don't store
                // the size and crc in the header.
                entry.Size = fs.Length;
                fs.Close();

                crc.Reset();
                crc.Update(buffer);

                entry.Crc = crc.Value;

                s.PutNextEntry(entry);

                s.Write(buffer, 0, buffer.Length);
            }
        }
        /// <summary>
        /// 压缩指定目录下指定文件(包括子目录下的文件)
        /// </summary>
        /// <param name="zippath">args[0]为你要压缩的目录所在的路径 
        /// 例如：D:\\temp\\   (注意temp 后面加 \\ 但是你写程序的时候怎么修改都可以)</param>
        /// <param name="zipfilename">args[1]为压缩后的文件名及其路径
        /// 例如：D:\\temp.zip</param>
        /// <param name="fileFilter">文件过滤, 例如*.xml,这样只压缩.xml文件.</param>
        ///
        public bool ZipFileMain(string zippath, string zipfilename, string fileFilter)
        {
            try
            {
                //string filenames = Directory.GetFiles(args[0]);

                Crc32 crc = new Crc32();
                ZipOutputStream s = new ZipOutputStream(File.Create(zipfilename));

                s.SetLevel(6); // 0 - store only to 9 - means best compression

                DirectoryInfo di = new DirectoryInfo(zippath);

                FileInfo[] a = di.GetFiles(fileFilter);

                cutStr = zippath.Trim();
                //压缩这个目录下的所有文件
                writeStream(ref s, a, crc);
                //压缩这个目录下子目录及其文件
                direct(di, ref s, crc);

                s.Finish();
                s.Close();
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}

/////<summary>
/////解压缩文件
/////</summary>
//using System;
//using System.Text;
//using System.Collections;
//using System.IO;
//using System.Diagnostics;
//using System.Runtime.Serialization.Formatters.Binary;
//using System.Data;

//using ICSharpCode.SharpZipLib.BZip2;
//using ICSharpCode.SharpZipLib.Zip;
//using ICSharpCode.SharpZipLib.Zip.Compression;
//using ICSharpCode.SharpZipLib.Zip.Compression.Streams;
//using ICSharpCode.SharpZipLib.GZip;

//namespace FtpResume.Utility
//{
//    public class UnZipClass
//    {
//        /// <summary>
//        /// 解压缩文件(压缩文件中含有子目录)
//        /// </summary>
//        /// <param name="zipfilepath">待解压缩的文件路径</param>
//        /// <param name="unzippath">解压缩到指定目录</param>
//        public void UnZip(string zipfilepath, string unzippath)
//        {
//            ZipInputStream s = new ZipInputStream(File.OpenRead(zipfilepath));

//            ZipEntry theEntry;
//            while ((theEntry = s.GetNextEntry()) != null)
//            {
//                string directoryName = Path.GetDirectoryName(unzippath);
//                string fileName = Path.GetFileName(theEntry.Name);
//                  //生成解压目录
//                Directory.CreateDirectory(directoryName);

//                if (fileName != String.Empty)
//                {
//                    //如果文件的压缩后大小为0那么说明这个文件是空的,因此不需要进行读出写入
//                    if (theEntry.CompressedSize == 0)
//                        break;
//                    //解压文件到指定的目录
//                    directoryName = Path.GetDirectoryName(unzippath + theEntry.Name);
//                    //建立下面的目录和子目录
//                    Directory.CreateDirectory(directoryName);

//                    FileStream streamWriter = File.Create(unzippath + theEntry.Name);

//                    int size = 2048;
//                    byte[] data = new byte[2048];
//                    while (true)
//                    {
//                        size = s.Read(data, 0, data.Length);
//                        if (size > 0)
//                        {
//                            streamWriter.Write(data, 0, size);
//                        }
//                        else
//                        {
//                            break;
//                        }
//                    }
//                    streamWriter.Close();
//                }
//            }
//            s.Close();
//        }
//    }
//}
//    }
//}
