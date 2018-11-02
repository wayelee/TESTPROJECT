using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;

using DevComponents.DotNetBar;

namespace LibCerMap
{
    public partial class FrmSkylinePara : OfficeForm
    {
        public double dPosX;        //巡视器位置X
        public double dPosY;        //巡视器位置Y
        public double dPosZ;        //巡视器位置Z
        public double dOriOmg;      //巡视器姿态omg
        public double dOriPhi;      //巡视器姿态phi
        public double dOriKap;      //巡视器姿态Kap
        public double dExpAngle;    //旋转臂展开角
        public double dYawAngle;    //旋转臂偏航角
        public double dPitchAngle;  //旋转臂俯仰角
        public double[] zgVecValue = new double[3];

        public bool isDialogOk = false;

        //zg库定义委托
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate void VecCallBack(double value1, double value2, double value3);
        VecCallBack zg_m_call;
        [DllImport("LibZmsgDLL.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void Register_CallBack_Vec([MarshalAs(UnmanagedType.FunctionPtr)]VecCallBack call);

        [DllImport("LibZmsgDLL.dll", EntryPoint = "zg_vec", CallingConvention = CallingConvention.Cdecl)]
        static extern int zg_vec(int uiTaskCode, int uiObjCode, int vecindex);

        void OutLouVec(double Value1, double Value2, double Value3)
        {
            zgVecValue[0] = Value1;
            zgVecValue[1] = Value2;
            zgVecValue[2] = Value3;
        }

        public FrmSkylinePara()
        {
            InitializeComponent();
        }
        Process myprocess = null;
        private void buttonPicFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = @"|*.tif;*.jpg";
            if (dlg.ShowDialog()==DialogResult.OK)
            {
                textBoxPicFile.Text = dlg.FileName;
            }
        }

        private void buttonParaFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = @"|*.bmp;*.tif;*.jpg";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                textBoxParaFile.Text = dlg.FileName;
            }
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            IsNumberic isNum = new IsNumberic();
            if (txtPosX.Text != null && txtPosY != null && txtPosZ != null && txtOriOmg != null
                && txtOriPhi != null && txtOriKap != null && txtExpAngle != null && txtPitchAngle != null
                && txtBeginYaw != null )
            {
                if (isNum.IsNumber(txtPosX.Text) && isNum.IsNumber(txtPosY.Text) && isNum.IsNumber(txtPosZ.Text)
                    && isNum.IsNumber(txtOriOmg.Text) && isNum.IsNumber(txtOriPhi.Text) && isNum.IsNumber(txtOriKap.Text)
                    && isNum.IsNumber(txtExpAngle.Text) && isNum.IsNumber(txtPitchAngle.Text)
                    && isNum.IsNumber(txtBeginYaw.Text) )
                {
                    dPosX = Convert.ToDouble(txtPosX.Text);
                    dPosY = Convert.ToDouble(txtPosY.Text);
                    dPosZ = Convert.ToDouble(txtPosZ.Text);
                    dOriOmg = Convert.ToDouble(txtOriOmg.Text);
                    dOriPhi = Convert.ToDouble(txtOriPhi.Text);
                    dOriKap = Convert.ToDouble(txtOriKap.Text);
                    dExpAngle = Convert.ToDouble(txtExpAngle.Text);
                    dYawAngle = Convert.ToDouble(txtBeginYaw.Text);
                    dPitchAngle = Convert.ToDouble(txtPitchAngle.Text);
                }
                else
                {
                    MessageBox.Show("相机参数值必须为数字,请检查并重新输入", "输入有误", MessageBoxButtons.OK);
                }
            }
            else
            {
                MessageBox.Show("参数值不能为空", "提示", MessageBoxButtons.OK);
            }

            //string exepath = ClsGDBDataCommon.GetParentPathofExe() + @"Resource\rob.exe";
            try
            {
                string s = ""; 
                //foreach (string arg in args) 
                //{
                //    s = s + arg + " "; 
                //}
                s = s.Trim();
                if (myprocess ==null)
                {
                    myprocess = new Process(); 
                }
                else
                {
                    myprocess.Close();
                    myprocess.Dispose();
                    myprocess = new Process();
                }

                

                //if (textBoxParaFile.Text.Trim() == "" || textBoxPicFile.Text.Trim() == "")
                if (  textBoxPicFile.Text.Trim() == "")
                {
                    MessageBox.Show("参数不能为空");
                    this.DialogResult = DialogResult.None;
                    return;
                }

                isDialogOk = true;
                

                //ProcessStartInfo startInfo = new ProcessStartInfo(exepath, textBoxPicFile.Text + " " + textBoxParaFile.Text); 
                //myprocess.StartInfo = startInfo;
                ////myprocess.StartInfo.CreateNoWindow = true;   
                //myprocess.StartInfo.UseShellExecute = false;               
                //myprocess.Start();  
            }
            catch (Exception ex)
            {
                MessageBox.Show("启动应用程序时出错！原因：" + ex.Message); 
            }            
        }

        public void matlabCalc(Ex_OriPara ori)
        {
            string fileDirPath = System.IO.Path.GetDirectoryName(textBoxPicFile.Text);
            string fileName = System.IO.Path.GetFileNameWithoutExtension(textBoxPicFile.Text);
            StreamWriter sw = new StreamWriter(fileDirPath + "\\" + fileName + "_para.txt", false, Encoding.ASCII);
            textBoxParaFile.Text = fileDirPath + "\\" + fileName + "_para.txt";
            double angle;
            angle = doubleInputSunAngle.Value;
            sw.Write(angle.ToString());
            sw.Write("  %太阳方位角，北向为0，-180°（逆时针） 至180°（顺时针）");
            //sw.Write("\n");
            sw.WriteLine();
            //sw.Write("1.22623");
            //sw.Write("0.05");
            sw.Write(doubleInputForcusDis.Value.ToString());
            sw.Write("  %f焦距,像素");
           // sw.Write("\n");
            sw.WriteLine();
            //sw.Write("511.5");
            //sw.Write("587.5");
            sw.Write(doubleInputX0.Value.ToString());
            sw.Write("  %x0");
            //sw.Write("\n");
            sw.WriteLine();
            //sw.Write("511.5");
            //sw.Write("431.5");
            sw.Write(doubleInputY0.Value.ToString());
            sw.Write("  %y0");
           // sw.Write("\n");
            sw.WriteLine();
            sw.Write(ori.pos.X);
            sw.Write("  %Xs,m%摄影中心位置");
           // sw.Write("\n");
            sw.WriteLine();
            sw.Write(ori.pos.Y);
            sw.Write("  %Ys");
           // sw.Write("\n");
            sw.WriteLine();
            sw.Write(ori.pos.Z);
            sw.Write("  %Zs");
           // sw.Write("\n");
            sw.WriteLine();
            sw.Write(ori.ori.omg * 180 / Math.PI);
            sw.Write("  %omg像点从像空间坐标系转换到站点坐标系的旋转角");
            //sw.Write("\n");
            sw.WriteLine();
            sw.Write(ori.ori.phi * 180 / Math.PI );
            sw.Write("  %phi");
           // sw.Write("\n");
            sw.WriteLine();
            sw.Write(ori.ori.kap * 180 / Math.PI);
            sw.Write("  %Kap");
            //sw.Write("\n");
            sw.WriteLine();
            sw.Flush();
            sw.Close();

            string exepath = ClsGDBDataCommon.GetParentPathofExe() + @"Resource\Skyline\funTianJiXian.exe";
            //string exepath = ClsGDBDataCommon.GetParentPathofExe() + @"Resource\rob.exe";
            string ParafilePath = ClsGDBDataCommon.GetParentPathofExe() + @"Resource\Skyline\DetectionPara.txt";
            //ProcessStartInfo startInfo = new ProcessStartInfo(exepath, textBoxPicFile.Text + " " + textBoxParaFile.Text + " "
            //    + ParafilePath);

            //Process p = new Process();
            //p.StartInfo.FileName = excutefile;
            //p.StartInfo.Arguments = excutecmd;
            //p.StartInfo.UseShellExecute = false;
            //p.StartInfo.RedirectStandardInput = true;
            //p.StartInfo.RedirectStandardOutput = true;
            //p.StartInfo.RedirectStandardError = true;
            //p.StartInfo.CreateNoWindow = true;

            //p.Start();
            //p.StandardInput.WriteLine("exit");
            //strOutput = p.StandardOutput.ReadToEnd();

            //p.WaitForExit();
            //p.Close();




           // myprocess.StartInfo = startInfo;


            myprocess.StartInfo.CreateNoWindow = false;
            myprocess.StartInfo.UseShellExecute = false;

            myprocess.StartInfo.RedirectStandardInput = true;
            myprocess.StartInfo.RedirectStandardOutput = true;
            myprocess.StartInfo.RedirectStandardError = true;
            myprocess.StartInfo.Arguments = textBoxPicFile.Text + " " + textBoxParaFile.Text + " "
                + ParafilePath;
            myprocess.StartInfo.FileName = exepath;
            myprocess.Start();
            string outPut = myprocess.StandardOutput.ReadToEnd();
            myprocess.StandardInput.WriteLine("exit");
            myprocess.WaitForExit();
        }

        private void btnXYZPHK_Click(object sender, EventArgs e)
        {
            zg_vec(3, 5115, 5);
            txtPosX.Text = zgVecValue[0].ToString();
            txtPosY.Text = zgVecValue[1].ToString();
            txtPosZ.Text = zgVecValue[2].ToString();
            zg_vec(3, 5115, 6);
            txtOriOmg.Text = zgVecValue[0].ToString();
            txtOriPhi.Text = zgVecValue[1].ToString();
            txtOriKap.Text = zgVecValue[2].ToString();
        }

        private void FrmSkylinePara_Load(object sender, EventArgs e)
        {
            //初始化zg全局段回调函数
            zg_m_call = new VecCallBack(OutLouVec);
            Register_CallBack_Vec(zg_m_call);
        }
    }
}
