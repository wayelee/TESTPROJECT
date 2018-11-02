using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using lib3ds.Net;

namespace LibModelGen
{
    public class CraterGen : ModelBase
    {
        //撞击坑的坑径
        private double mDbDiameter;
        //撞击坑的深度
        private double mDbDepth;
        //格网密度
        private double mDbOffset;
        //坑径影响范围
        private double mDbEffecedDiameter;

        public double Diameter
        {
            get
            {
                return mDbDiameter;
            }

            set
            {
                mDbDiameter = value;
            }
        }

        public double Depth
        {
            get
            {
                return mDbDepth;
            }

            set
            {
                mDbDepth = value;
            }
        }

        public double Offset
        {
            get
            {
                return mDbOffset;
            }
        }

        public CraterGen(double craterDiameter, double craterDepth)
            : base()
        {
            mTriType = TriType.TriForward;
            mMappingType = MappingType.Flat;
            mModelType = ModelType.ModelCrater;
            mDbDiameter = craterDiameter;
            mDbDepth = craterDepth;
            mDbOffset = 2 * craterDiameter / 200;   // 插值格网间距
            mDbEffecedDiameter = 1.0;
        }

        //计算模型点集
        protected override sealed bool calcModelPts()
        {
            //坑半径
            double dbX = mDbDiameter / 2;
            //抛物线参数a
            double dbA = Math.Sqrt(dbX * dbX / (2 * mDbDepth));
            //撞击坑影响范围
            double dbCount = mDbEffecedDiameter;
            double dbFirstThreshod = dbX;
            double dbSecondThreshod = dbCount * dbX;

            double dbRange = 2 * dbCount * dbX;
            int nRows = (int)Math.Ceiling(dbRange / mDbOffset + 1e-10);
            int nCols = nRows;

            mPtDataInMeshForm = new Point3D[nRows, nCols];
            for (int i = 0; i < nRows; i++)
            {
                for (int j = 0; j < nCols; j++)
                {
                    double dbTmpX = -1 * dbCount * dbX + j * mDbOffset;
                    double dbTmpY = -1 * dbCount * dbX + i * mDbOffset;
                    double dbTmpZ = 0;

                    double dbTmpR = Math.Sqrt(dbTmpX * dbTmpX + dbTmpY * dbTmpY);
                    if (dbTmpR <= dbFirstThreshod)
                    {
                        dbTmpZ = (dbTmpX * dbTmpX + dbTmpY * dbTmpY) / (2 * dbA * dbA);
                    }
                    else if (dbTmpR > dbFirstThreshod && dbTmpR <= dbSecondThreshod)
                    {
                        //double dbDelta = 0.14 * Math.Pow(1.19 * dbX, 0.74) * Math.Pow(1.19 * dbTmpR / dbX, -3);
                        double dbDelta = 0.094 * Math.Pow(dbX, 0.74) * Math.Pow(dbTmpR / dbX, -3);
                        dbTmpZ = mDbDepth + dbDelta;
                    }
                    else
                    {
                        dbTmpR = dbSecondThreshod;
                        //double dbDelta = 0.14 * Math.Pow(1.19 * dbX, 0.74) * Math.Pow(1.19 * dbTmpR / dbX, -3);
                        double dbDelta = 0.094 * Math.Pow(dbX, 0.74) * Math.Pow(dbTmpR / dbX, -3);
                        dbTmpZ = mDbDepth + dbDelta;
                    }

                    mPtDataInMeshForm[i, j] = new Point3D(dbTmpX, dbTmpY, dbTmpZ);
                }
            }

            return true;
        }

        //重写三角化函数
        protected override bool triangulation()
        {
            if (mPtDataInMeshForm == null)
            {
                return false;
            }
            else
            {
                return Mesh2Tri.mesh2Tri(mPtDataInMeshForm, mDbEffecedDiameter * mDbDiameter / 2, out uFacesIndex, out mDbVertices, mTriType);
            }

            return true;
        }

        //拉伸点集
        protected override sealed bool strechPts()
        {
            //if (mPtDataInMeshForm == null)
            //{
            //    return false;
            //}

            //for (int i = 0; i < mDbVertices.GetLength(0); i++)

            //for (int i = 0; i < mPtDataInMeshForm.GetLength(0); i++)
            //{
            //    for (int j = 0; j < mPtDataInMeshForm.GetLength(1); j++)
            //    {
            //        mDbVertices[i, 0] *= 1;
            //        mDbVertices[i, 1] *= 5;
            //        mDbVertices[i, 2] *= 1;
            //    }
            //}

            if (mDbVertices == null)
            {
                return false;
            }

            for (int i = 0; i < mDbVertices.GetLength(0); i++)
            {
                mDbVertices[i, 0] *= 1;
                mDbVertices[i, 1] *= 1;
                mDbVertices[i, 2] -= mDbDepth;
            }

            return true;
        }
    }
}
