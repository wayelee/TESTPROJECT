using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using ESRI.ArcGIS.DataSourcesRaster;
using ESRI.ArcGIS.Geodatabase;

namespace LibCerMap
{
    public partial class FrmSetRasterDataValue : DevComponents.DotNetBar.OfficeForm
    {
        //内部维护的无效值
        private double m_dbNoDataValue = double.NaN;

        //从外部传入的栅格，用于自动获取无效值
        private IRaster m_pSrcRaster = null;

        public double NoDataValue
        {
            get
            {
                return m_dbNoDataValue;
            }
        }

        public FrmSetRasterDataValue(IRaster pSrcRaster)
        {
            InitializeComponent();
            m_pSrcRaster = pSrcRaster;
        }

        private double getNoDataValue(object pObject)
        {
            double dbNoData = double.NaN;

            if (pObject is double[])
                dbNoData = Convert.ToDouble(((double[])pObject)[0]);
            else if (pObject is float[])
                dbNoData = Convert.ToDouble(((float[])pObject)[0]);
            else if (pObject is Int64[])
                dbNoData = Convert.ToDouble(((Int64[])pObject)[0]);
            else if (pObject is Int32[])
                dbNoData = Convert.ToDouble(((Int32[])pObject)[0]);
            else if (pObject is Int16[])
                dbNoData = Convert.ToDouble(((Int16[])pObject)[0]);
            else if (pObject is byte[])
                dbNoData = Convert.ToDouble(((byte[])pObject)[0]);
            else if (pObject is char[])
                dbNoData = Convert.ToDouble(((char[])pObject)[0]);
            else
                ;

            return dbNoData;
        }

        double getNoDataFromRaster()
        {
            //若原始栅格为空，则直接返回默认无效值，double.NAN
            if (m_pSrcRaster == null)
                return double.NaN;

            double dbValue = double.NaN;
            IRasterProps pRasterProps = m_pSrcRaster as IRasterProps;
            object oNoDataValue=pRasterProps.NoDataValue;
            dbValue = getNoDataValue(oNoDataValue);
            
            return dbValue;
        }

        private void FrmSetRasterNoDataValue_Load(object sender, EventArgs e)
        {
            dbiNoDataValue.Value = getNoDataFromRaster();
            m_dbNoDataValue = dbiNoDataValue.Value;
        }

        private void btnGetFromLayer_Click(object sender, EventArgs e)
        {
            dbiNoDataValue.Value = getNoDataFromRaster();
            m_dbNoDataValue = dbiNoDataValue.Value;
        }

        private void FrmSetRasterNoDataValue_FormClosed(object sender, FormClosedEventArgs e)
        {
            m_dbNoDataValue = dbiNoDataValue.Value;
        }

        private void dbiNoDataValue_ValueChanged(object sender, EventArgs e)
        {
            m_dbNoDataValue = dbiNoDataValue.Value;
        }
    }
}