using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibModelGen
{
    public class CubicGen : ModelBase
    {
        //石块高
        private double mDbHeight;
        //石块长
        private double mDbLength;
        //石块宽
        private double mDbWidth;
        
        public CubicGen(double width, double length, double height, ushort subDivisionCount)
            :base()
        {
            mTriType = TriType.TriSubdivision;
            mMappingType = MappingType.Sphere;
            mModelType = ModelType.ModelCubic;

            mDbHeight=height;
            mDbWidth=width;
            mDbLength=length;
            uSubDivisionCount = subDivisionCount;
        }

        //计算模型点集
        protected override sealed bool calcModelPts()
        {
            mPtData = new Point3D[]
            {
                new Point3D(-0.5,-0.5,-0.5),
                new Point3D(-0.5,0.5,-0.5),
                new Point3D(0.5,0.5,-0.5),
                new Point3D(0.5,-0.5,-0.5),
                new Point3D(-0.5,-0.5,0.5),
                new Point3D(-0.5,0.5,0.5),
                new Point3D(0.5,0.5,0.5),
                new Point3D(0.5,-0.5,0.5)
            };

            return true;
        }

        protected override sealed bool strechPts()
        {
            if (mPtData == null)
            {
                return false;
            }

            for (int i = 0; i < mDbVertices.GetLength(0); i++)
            {
                mDbVertices[i, 0] *= mDbWidth;
                mDbVertices[i, 1] *= mDbLength;
                mDbVertices[i, 2] *= mDbHeight * 2;
            }

            return true;
        }
    }
}
