using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ESRI.ArcGIS.Carto;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Controls;
using System.Data;

namespace LibCerMap
{
    class ClsAttributeTable
    {
        ILayer m_pLayer = null;
        DataTable m_pDataTable = null;
        DataGridViewX m_pDataGrid = null;
        public ClsAttributeTable(ILayer layer, DataGridViewX dataGrid)
        {           
            m_pLayer = layer;
            m_pDataGrid = dataGrid;
            m_pDataTable = dataGrid.DataSource as DataTable;
        }




    }
}
