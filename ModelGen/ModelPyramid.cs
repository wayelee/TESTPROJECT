using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibModelGen
{
    public class PyramidGen : ModelBase
    {
        //石块高
        private double mDbHeight;
        //上底面宽
        private double mDbUpperWidth;
        //下底面宽
        private double mDbLowerWidth;

        public PyramidGen(double upperWidth, double lowerWidth, double height, ushort subDivisionCount)
            :base()
        {
            mTriType = TriType.TriSubdivision;
            mMappingType = MappingType.Sphere;
            mModelType = ModelType.ModelPyramid;

            mDbHeight=height;
            mDbUpperWidth = upperWidth;
            mDbLowerWidth = lowerWidth;
            uSubDivisionCount = subDivisionCount;
        }

        //计算模型点集
        protected override sealed bool calcModelPts()
        {
            double dbRatio = mDbUpperWidth / mDbLowerWidth;
            double dbValue = dbRatio * 0.5;

            mPtData = new Point3D[8];
            mPtData[0] = new Point3D(-0.5, -0.5, -0.5);
            mPtData[1]=new Point3D(-0.5,0.5,-0.5);
            mPtData[2]=new Point3D(0.5,0.5,-0.5);
            mPtData[3]=new Point3D(0.5,-0.5,-0.5);
            mPtData[4] = new Point3D(-dbValue, -dbValue, 0.5);
            mPtData[5] = new Point3D(-dbValue, dbValue, 0.5);
            mPtData[6] = new Point3D(dbValue, dbValue, 0.5);
            mPtData[7] = new Point3D(dbValue, -dbValue, 0.5);

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
                double dbMultiply = mDbLowerWidth / 0.5;

                mDbVertices[i, 0] *= dbMultiply;
                mDbVertices[i, 1] *= dbMultiply;
                mDbVertices[i, 2] *= mDbHeight * 2;
            }

            return true;
        }
    }
}
