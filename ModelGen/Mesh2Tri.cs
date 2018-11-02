using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibModelGen
{
    public class Mesh2Tri
    {
        static public bool mesh2Tri(Point3D[,] dbArray, double dbThreshold, out ushort[,] nFacesIndex, out double[,] dbVertices, TriType type)
        {
            nFacesIndex = null;
            dbVertices = null;

            if (dbArray == null)
                return false;

            int nRows = dbArray.GetLength(0);
            int nCols = dbArray.GetLength(1);


            #region 判断产生的模型点是否在坑径范围内
                bool[,] bFlags = new bool[nRows, nCols];
                dbVertices = new double[nRows * nCols, 3];

                for (int i = 0; i < nRows; i++)
                {
                    for (int j = 0; j < nCols; j++)
                    {
                        double dbX = dbArray[i, j].X;
                        double dbY = dbArray[i, j].Y;
                        double dbZ = dbArray[i, j].Z;

                        dbVertices[i * nCols + j, 0] = dbX;
                        dbVertices[i * nCols + j, 1] = dbY;
                        dbVertices[i * nCols + j, 2] = dbZ;

                        if (Math.Sqrt(dbX * dbX + dbY * dbY) <= dbThreshold)
                        {
                            bFlags[i, j] = true;
                        }
                        else
                        {
                            bFlags[i, j] = false;
                        }
                    }
                }
            #endregion

            #region 构面
                //分别记录坑径范围内的点构成面的各节点的索引
                List<Point3D> listFaces = new List<Point3D>();
                switch (type)
                {
                    case TriType.TriForward:
                        for (int i = 0; i < nRows - 1; i++)
                        {
                            for (int j = 0; j < nCols - 1; j++)
                            {
                                if (bFlags[i, j] && bFlags[i + 1, j] && bFlags[i, j + 1])
                                {
                                    listFaces.Add(new Point3D(i * nCols + j, (i + 1) * nCols + j, i * nCols + j + 1));
                                }


                                if (bFlags[i + 1, j + 1] && bFlags[i, j + 1] && bFlags[i + 1, j])
                                {
                                    listFaces.Add(new Point3D((i + 1) * nCols + j + 1, i * nCols + j + 1, (i + 1) * nCols + j));
                                }
                            }
                        }
                        break;
                    case TriType.TriBackward:
                        for (int i = 0; i < nRows - 1; i++)
                        {
                            for (int j = 0; j < nCols - 1; j++)
                            {
                                if (bFlags[i, j] && bFlags[i + 1, j] && bFlags[i + 1, j + 1])
                                {
                                    listFaces.Add(new Point3D(i * nCols + j, (i + 1) * nCols + j, (i + 1) * nCols + j + 1));
                                }

                                if (bFlags[i, j] && bFlags[i, j + 1] && bFlags[i + 1, j + 1])
                                {
                                    listFaces.Add(new Point3D(i * nCols + j, i * nCols + j + 1, (i + 1) * nCols + j + 1));
                                }
                            }
                        }
                        break;
                    default:
                        break;
                }
            #endregion

            #region
                int nFaceCount = listFaces.Count;
                nFacesIndex = new ushort[nFaceCount, 3];
                for (int i = 0; i < nFaceCount; i++)
                {
                    nFacesIndex[i, 0] = (ushort)listFaces[i].X;
                    nFacesIndex[i, 1] = (ushort)listFaces[i].Y;
                    nFacesIndex[i, 2] = (ushort)listFaces[i].Z;
                }
            #endregion

            return true;
        }
    }
}
