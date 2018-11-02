using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibModelGen
{
    public class TetraGen : ModelBase
    {
        //四面体高
        private double mDbHeight;
        //四面体宽
        private double mDbWidth;

        public TetraGen(double width, double height, ushort subDivisionCount)
            :base()
        {
            mTriType = TriType.TriSubdivision;
            mMappingType = MappingType.Sphere;
            mModelType = ModelType.ModelTetrahedron;

            mDbHeight=height;
            mDbWidth=width;
            uSubDivisionCount = subDivisionCount;
        }

        //计算模型点集
        protected override sealed bool calcModelPts()
        {
            mPtData = new Point3D[4];

            mPtData[1] = new Point3D(-1.0, 0.0, -0.5);
            mPtData[3] = new Point3D(0.0, 0.0, 0.5);

            Random r = new Random(SubDivision.Chaos_GetRandomSeed());
            mPtData[0] = new Point3D(r.NextDouble(), r.NextDouble(), -0.5);
            r = new Random(SubDivision.Chaos_GetRandomSeed());
            mPtData[2] = new Point3D(r.NextDouble(), -1 * r.NextDouble(), -0.5);

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
                mDbVertices[i, 1] *= mDbWidth;
                mDbVertices[i, 2] *= mDbHeight * 2;
            }

            return true;
        }
    }
}
