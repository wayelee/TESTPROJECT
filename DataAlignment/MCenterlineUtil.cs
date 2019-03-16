using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;
using System;
using System.Drawing;


namespace DataAlignment
{
    public class MCenterlineUtil
    {
        private double _Rotation;
        public double Rotation
            {
                get { return _Rotation; }
            }


            private IPolygon _Bound;
            public IPolygon Bound
            {
                get { return _Bound; }
            }

            private double _WidthHeightRatio;
            private IPoint _BegPt;

            private IPoint _EndPt;
            public MCenterlineUtil(IPoint begPt, IPoint endPt, double widthHeightRatio)
            {
                _BegPt = begPt;
                _EndPt = endPt;
                PopulateData(begPt, endPt, widthHeightRatio);
            }

            public IEnvelope GetRotatedBound()
            {

                IPolygon viewBound = this.Bound;
                double rotation = this.Rotation;

                IEnvelope rotatedBound = AOFunctions.Geometry.GeometryUtil.GetRotatedClipBound(viewBound, -rotation);
                return rotatedBound;

            }

          
            public IEnvelope GetNonRotatedBound()
            {

                IPolygon viewBound = this.Bound;
                IEnvelope rotatedBound = AOFunctions.Geometry.GeometryUtil.GetRotatedClipBound(viewBound, 0);
                return rotatedBound;

            }

        
            public void FitActiveViewTo(IActiveView mapAV, bool rotateMap = true)
            {
                IEnvelope rotatedBound = null;
                if (rotateMap)
                {
                    rotatedBound = GetRotatedBound();
                    mapAV.ScreenDisplay.DisplayTransformation.Rotation = -this.Rotation;
                }
                else
                {
                    rotatedBound = GetNonRotatedBound();
                    mapAV.ScreenDisplay.DisplayTransformation.Rotation = 0;
                }
                mapAV.ScreenDisplay.DisplayTransformation.VisibleBounds = rotatedBound;
                mapAV.Refresh();
            }


            #region "Private Functions"


            private void PopulateData(IPoint begPt, IPoint endPt, double widthHeightRatio)
            {
                _Rotation = CountRotation(begPt, endPt);
                _WidthHeightRatio = widthHeightRatio;
                _Bound = CreatePolygon(begPt, endPt);

            }


            private double CountRotation(IPoint pt1, IPoint pt2)
            {

                ILine line = default(ILine);
                line = new Line();
                line.FromPoint = pt1;
                line.ToPoint = pt2;

                double angle = line.Angle;
                return (angle * 180) / Math.PI;

            }

            private double CountRotationAsRatians(IPoint pt1, IPoint pt2)
            {

                ILine line = default(ILine);
                line = new Line();
                line.FromPoint = pt1;
                line.ToPoint = pt2;

                return line.Angle;

            }


            private IPolygon CreatePolygon(IPoint pt1, IPoint pt2)
            {


                try
                {
                    //Dim rotation As Double = CountRotationAsRatians(pt1, pt2)

                    ILine line = new Line();
                    line.FromPoint = pt1;
                    line.ToPoint = pt2;

                    IPoint centerPt = new ESRI.ArcGIS.Geometry.Point();
                    centerPt.PutCoords((pt1.X + pt2.X) / 2, (pt1.Y + pt2.Y) / 2);


                    ITransform2D trans = line as ITransform2D;
                    trans.Rotate(centerPt, -Rotation);


                    IPolygon poly = BuildRectange(centerPt);
                    ITransform2D transForPoly = poly as ITransform2D;
                    transForPoly.Rotate(centerPt, Rotation * (Math.PI / 180)); 

                    poly.SpatialReference = pt1.SpatialReference;

                    return poly;

                }
                catch (Exception ex)
                {
                    return null;
                }

            }

            private IPolygon BuildRectange(IPoint centerPt)
            {

                double fwWidth = 0;
                double fwHeight = 0;
                fwWidth = CountFilmWindowWidth();
                fwHeight = fwWidth / _WidthHeightRatio;

                IPointCollection ptCol = new Polygon();

                IPoint pts_0 = new ESRI.ArcGIS.Geometry.Point();
                pts_0.PutCoords(centerPt.X - fwWidth / 2, centerPt.Y - fwHeight / 2);
                IPoint pts_1 = new ESRI.ArcGIS.Geometry.Point();
                pts_1.PutCoords(centerPt.X - fwWidth / 2, centerPt.Y + fwHeight / 2);
                IPoint pts_2 = new ESRI.ArcGIS.Geometry.Point();
                pts_2.PutCoords(centerPt.X + fwWidth / 2, centerPt.Y + fwHeight / 2);
                IPoint pts_3 = new ESRI.ArcGIS.Geometry.Point();
                pts_3.PutCoords(centerPt.X + fwWidth / 2, centerPt.Y - fwHeight / 2);

                ptCol.AddPoint(pts_0);
                ptCol.AddPoint(pts_1);
                ptCol.AddPoint(pts_2);
                ptCol.AddPoint(pts_3);
                ptCol.AddPoint(pts_0);

                return ptCol as IPolygon;

            }


            private PointF[] BuildRectange(PointF centerPt)
            {

                float fwWidth = 0;
                float fwHeight = 0;
                fwWidth = Convert.ToSingle(CountFilmWindowWidth());
                fwHeight = Convert.ToSingle(fwWidth / _WidthHeightRatio);

                PointF[] pts = new PointF[4];
                pts[0] = new PointF(centerPt.X - fwWidth / 2, centerPt.Y - fwHeight / 2);
                pts[1] = new PointF(centerPt.X - fwWidth / 2, centerPt.Y + fwHeight / 2);
                pts[2] = new PointF(centerPt.X + fwWidth / 2, centerPt.Y + fwHeight / 2);
                pts[3] = new PointF(centerPt.X + fwWidth / 2, centerPt.Y - fwHeight / 2);

                return pts;

            }

            private double CountFilmWindowWidth()
            {

                ILine line = new Line();
                line.FromPoint = _BegPt;
                line.ToPoint = _EndPt;
                line.SpatialReference = _BegPt.SpatialReference;
                return line.Length;

            }

            private IPolygon CreatePolygonFromPointF(PointF[] pts)
            {


                IPolygon poly = new Polygon() as IPolygon;
                IPointCollection ptCollection = poly as IPointCollection;

                for (int i = 0; i <= pts.Length - 1; i++)
                {
                    IPoint pPt = new ESRI.ArcGIS.Geometry.Point();
                    pPt.X = pts[i].X;
                    pPt.Y = pts[i].Y;
                    ptCollection.AddPoint(pPt);

                }

                IPoint pPtEnd = new ESRI.ArcGIS.Geometry.Point();
                pPtEnd.X = pts[0].X;
                pPtEnd.Y = pts[0].Y;
                ptCollection.AddPoint(pPtEnd);

                return poly;

            }


            #endregion


        }

}
