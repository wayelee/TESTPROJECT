using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibCerMap
{
    class CGaussCalculation
    {
        public double a;
        public double b;
        public bool do3Dtransform;
        protected double q = 206264.806252992;      //弧度秒=180*3600/pi  

        public CGaussCalculation()
        {
            a = 6378137.0;
            b = 6356752.3142;
        }

        public CGaussCalculation(bool is3D)
        {
            a = 0;
            b = 0;
        }

        public CGaussCalculation(double _a, double _b, bool is3D)
        {
            a = _a;
            b = _b;
            do3Dtransform = is3D;
        }

        /************高斯正算:(B,L)=>(x,y) ***************
          输入:double a ,f 椭球参数，B,L为大地坐标,L0为中央子午线的经度，单位为角度; x,y为高斯平面坐标,y加上了500000常量
          ******************************************/
        public void GaussPositive(double B, double L, double L0, out double xGauss, out double yGauss)
        {
            L0 = D_Rad(L0);
            B = D_Rad(B);
            L = D_Rad(L);
            double c, e1, e2;               //!<极点处的子午线曲率半径，第一偏心率，第二偏心率
            double l, W, N, M, daihao; //!<W为常用辅助函数，N为子午圈曲率半径，M为卯酉圈曲率半径
            double Xlong;                  //!<子午线弧长，高斯投影的坐标
            double ita, sb, cb, t;
            double[] m = new double[5];
            double[] n = new double[5];
            //!<计算一些基本常量

            e1 = Math.Sqrt(a * a - b * b) / a;
            e2 = Math.Sqrt(a * a - b * b) / b;
            c = a * a / b;
            m[0] = a * (1 - e1 * e1);
            m[1] = 3 * (e1 * e1 * m[0]) / 2.0;
            m[2] = 5 * (e1 * e1 * m[1]) / 4.0;
            m[3] = 7 * (e1 * e1 * m[2]) / 6.0;
            m[4] = 9 * (e1 * e1 * m[3]) / 8.0;
            n[0] = m[0] + m[1] / 2 + 3 * m[2] / 8 + 5 * m[3] / 16 + 35 * m[4] / 128;
            n[1] = m[1] / 2 + m[2] / 2 + 15 * m[3] / 32 + 7 * m[4] / 16;
            n[2] = m[2] / 8 + 3 * m[3] / 16 + 7 * m[4] / 32;
            n[3] = m[3] / 32 + m[4] / 16;
            n[4] = m[4] / 128;

            //!<由纬度计算子午线弧长
            {
                Xlong = n[0] * B - Math.Sin(B) * Math.Cos(B) * ((n[1] - n[2] + n[3]) + (2 * n[2] - (16 * n[3] / 3.0)) * Math.Sin(B) * Math.Sin(B) + 16 * n[3] * Math.Pow(Math.Sin(B), 4) / 3.0);
            }
            l = L - L0;       //!<弧度
            ita = e2 * Math.Cos(B);
            sb = Math.Sin(B);
            cb = Math.Cos(B);
            W = Math.Sqrt(1 - e1 * e1 * sb * sb);
            N = a / W;
            t = Math.Tan(B);
            xGauss = (Xlong + N * sb * cb * l * l / 2 + N * sb * cb * cb * cb * (5 - t * t + 9 * ita * ita + 4 * ita * ita * ita * ita) * l * l * l * l / 24 + N * sb * cb * cb * cb * cb * cb * (61 - 58 * t * t + t * t * t * t) * l * l * l * l * l * l / 720);
            yGauss = (N * cb * l + N * cb * cb * cb * (1 - t * t + ita * ita) * l * l * l / 6 + N * cb * cb * cb * cb * cb * (5 - 18 * t * t + t * t * t * t + 14 * ita * ita - 58 * ita * ita * t * t) * l * l * l * l * l / 120);
            yGauss = yGauss + 500000;
        }

        /**************高斯反算:(x,y)=>(B,L)***************
        double a ,b椭球参数,x,y为高斯平面坐标,L0为中央子午线的经度;B,L为大地坐标,单位为角度
        *****************************/
        public void GaussNegative(double xGauss, double yGauss, double L0, out double B, out double L)
        {
            L0 = D_Rad(L0);
            double c, e1, e2;          //!<短半轴，极点处的子午线曲率半径，第一偏心率，第二偏心率
            double Bf, itaf, tf, Nf, Mf, Wf;
            double l;
            double[] m = new double[5];
            double[] n = new double[5];
            yGauss = yGauss - 500000;	
            //!<计算一些基本常量
            {
                e1 = Math.Sqrt(a * a - b * b) / a;
                e2 = Math.Sqrt(a * a - b * b) / b;
                c = a * a / b;
                m[0] = a * (1 - e1 * e1);
                m[1] = 3 * (e1 * e1 * m[0]) / 2.0;
                m[2] = 5 * (e1 * e1 * m[1]) / 4.0;
                m[3] = 7 * (e1 * e1 * m[2]) / 6.0;
                m[4] = 9 * (e1 * e1 * m[3]) / 8.0;
                n[0] = m[0] + m[1] / 2 + 3 * m[2] / 8 + 5 * m[3] / 16 + 35 * m[4] / 128;
                n[1] = m[1] / 2 + m[2] / 2 + 15 * m[3] / 32 + 7 * m[4] / 16;
                n[2] = m[2] / 8 + 3 * m[3] / 16 + 7 * m[4] / 32;
                n[3] = m[3] / 32 + m[4] / 16;
                n[4] = m[4] / 128;
            }
            //计算Bf
            {
                double Bf1, Bfi0, Bfi1, FBfi;
                Bf1 = xGauss / n[0];
                Bfi0 = Bf1;
                Bfi1 = 0;
                FBfi = 0;
                int num = 0;
                do
                {
                    num = 0;
                    FBfi = 0.0 - n[1] * Math.Sin(2 * Bfi0) / 2.0 + n[2] * Math.Sin(4 * Bfi0) / 4.0 - n[3] * Math.Sin(6 * Bfi0) / 6.0;
                    Bfi1 = (xGauss - FBfi) / n[0];
                    if (Math.Abs(Bfi1 - Bfi0) > (Math.PI * Math.Pow(10.0, -8) / (36 * 18)))
                    {
                        num = 1;
                        Bfi0 = Bfi1;
                    }
                } while (num == 1);
                Bf = Bfi1;
            }
            tf = Math.Tan(Bf);
            Wf = Math.Sqrt(1 - e1 * e1 * Math.Sin(Bf) * Math.Sin(Bf));
            Nf = a / Wf;
            Mf = a * (1 - e1 * e1) / (Wf * Wf * Wf);
            itaf = e2 * Math.Cos(Bf);
            B = Bf - tf * yGauss * yGauss / (2 * Mf * Nf) + tf * (5 + 3 * tf * tf + itaf * itaf - 9 * itaf * itaf * tf * tf) * Math.Pow(yGauss, 4) / (24 * Mf * Math.Pow(Nf, 3)) - tf * (61 + 90 * tf * tf + 45 * Math.Pow(tf, 4)) * Math.Pow(yGauss, 6) / (720 * Mf * Math.Pow(Nf, 5));
            l = yGauss / (Nf * Math.Cos(Bf)) - (1 + 2 * tf * tf + itaf * itaf) * Math.Pow(yGauss, 3) / (6 * Math.Pow(Nf, 3) * Math.Cos(Bf)) + (5 + 28 * tf * tf + 24 * Math.Pow(tf, 4) + 6 * itaf * itaf + 8 * itaf * itaf * tf * tf) * Math.Pow(yGauss, 5) / (120 * Math.Pow(Nf, 5) * Math.Cos(Bf));
            L = l + L0;
            B = Rad_D(B);
            L = Rad_D(L);
        }

        //!<弧度转换为度
        public double Rad_D(double a)
        {
            double D, M, S;
            a = (a / (Math.PI)) * 180;
            D = Math.Truncate(a);
            M = Math.Truncate((a - D) * 60);
            S = ((a - D) * 60 - M) * 60;
            return D + M / 60 + S / 3600;
        }
        //!<度转换为弧度
        public double D_Rad(double p)
        {
            return p * Math.PI / 180;
        }
        //!<度转换为度分秒显示
        public string D_DFM(double a)
        {
            string sDFM = "";
            double D = Math.Truncate(a);
            double F = Math.Truncate((a - D) * 60);
            double S = ((a - D) * 60 - F) * 60;
            sDFM = D.ToString() + "°" + F.ToString() + "'" + S.ToString() + "\"";
            return sDFM;
        }
        public double DFM_D(string dfm)
        {
            string[] sDFM = dfm.Split(new char[3] { '°', '\'', '\"' });
            double D = Convert.ToDouble(sDFM[0]);
            double F = Convert.ToDouble(sDFM[1]) / 60;
            double M = Convert.ToDouble(sDFM[2]) / 3600;
            double degree = D + F + M;
            return degree;
        }
    }
}
