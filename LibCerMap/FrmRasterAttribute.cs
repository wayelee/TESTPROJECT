using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.DataSourcesRaster;
using ESRI.ArcGIS.Geodatabase;

using DevComponents.DotNetBar;

namespace LibCerMap
{
    public partial class FrmRasterAttribute : OfficeForm
    {
        ILayer pLayer;
        public FrmRasterAttribute(ILayer layer)
        {
            InitializeComponent();
            this.EnableGlass = false;
            pLayer = layer;
        }
    }
}
