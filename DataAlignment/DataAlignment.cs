using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.Carto;
using System.Data;
using ESRI.ArcGIS.Server;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Geodatabase;
namespace DataAlignment
{
    public class DataAlignment
    {
        public IMap pMap;
        public IFeatureLayer MiddleLinePointLayer;
        public IFeatureLayer InsideInspectionPointLayer;

        
        static public void CanlculateDistanceInMiddlePointTable(DataTable dt)
        {
            if (dt.Columns.Contains("MDistance") == false)
            {
                dt.Columns.Add("MDistance");  
            }                     
            IPoint PrePt = null;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow r = dt.Rows[i];
                r["MDistance"] = 0;
                IPoint pt = new PointClass();               
                pt.X = double.Parse(r["X_经度"].ToString());
                pt.Y = double.Parse(r["Y_纬度"].ToString());
                pt.Z = double.Parse(r["Z_高程（米)"].ToString());
                if (i == 0)
                {
                    r["MDistance"] = 0;
                    PrePt = pt;
                    continue;
                }
                else
                {
                    r["MDistance"] = CalculateDistanceBetween84TwoPoints(PrePt, pt);
                }
            }
             
        }
        static public double CalculateDistanceBetween84TwoPoints(IPoint pt1, IPoint pt2)
        {
            ESRI.ArcGIS.Geometry.ISpatialReferenceFactory spatialReferenceFactory = new ESRI.ArcGIS.Geometry.SpatialReferenceEnvironmentClass();
            //wgs 84
            IGeographicCoordinateSystem wgs84 = spatialReferenceFactory.CreateGeographicCoordinateSystem(4326) as IGeographicCoordinateSystem;
            IUnit meterUnit = spatialReferenceFactory.CreateUnit((int)ESRI.ArcGIS.Geometry.esriSRUnitType.esriSRUnit_Meter);
            ISpatialReference pSR = wgs84 as ISpatialReference;
            IGeometryServer2 pGS = new GeometryServerClass();
            double dis = pGS.GetDistanceGeodesic(wgs84, pt1, pt2, (ILinearUnit)meterUnit);
            IZAware z1 = pt1 as IZAware;
            IZAware z2 = pt2 as IZAware;

            if (z1.ZAware && z2.ZAware)
            {
                double deltaZ = Math.Abs(pt1.Z - pt2.Z);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(pGS);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(spatialReferenceFactory);
                return Math.Sqrt(dis * dis + deltaZ * deltaZ);
            }
            else
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(pGS);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(spatialReferenceFactory);
                return dis;
            }
          
        }
    }
}
