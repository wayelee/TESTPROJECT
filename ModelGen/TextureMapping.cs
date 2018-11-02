using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibModelGen
{
    class TextureMapping
    {
        public static bool calcTextureCoor(double[,] dbVertices, out double[,] dbTextureCoors, MappingType type)
        {
            dbTextureCoors = null;
            if (dbVertices == null)
            {
                return false;
            }

            //平铺映射
            int nVertices = dbVertices.GetLength(0);
            dbTextureCoors = new double[nVertices, 2];
            if (type == MappingType.Flat)
            {
                double dbMaxX = -999, dbMaxY = -999, dbMinX = 999, dbMinY = 999;
                for (int i = 0; i < nVertices; i++)
                {
                    if (dbVertices[i, 0] > dbMaxX)
                    {
                        dbMaxX = dbVertices[i, 0];
                    }

                    if (dbVertices[i, 0] < dbMinX)
                    {
                        dbMinX = dbVertices[i, 0];
                    }

                    if (dbVertices[i, 1] > dbMaxY)
                    {
                        dbMaxY = dbVertices[i, 1];
                    }

                    if (dbVertices[i, 1] < dbMinY)
                    {
                        dbMinY = dbVertices[i, 1];
                    }
                }

                for (int i = 0; i < nVertices; i++)
                {
                    dbTextureCoors[i, 0] = (dbVertices[i, 0] - dbMinX) / (dbMaxX - dbMinX);
                    dbTextureCoors[i, 1] = (dbVertices[i, 1] - dbMinY) / (dbMaxY - dbMinY);
                }
            }
            else if (type == MappingType.Sphere)
            {
                for (int i = 0; i < nVertices; i++)
                {
                    double dbX = dbVertices[i, 0];
                    double dbY = dbVertices[i, 1];
                    double dbZ = dbVertices[i, 2];

                    double dbR = Math.Sqrt(dbX * dbX + dbY * dbY + dbZ * dbZ);
                    if (dbR == 0)
                    {
                        dbTextureCoors[i, 0] = 0;
                        dbTextureCoors[i, 1] = 0;
                        continue;
                    }

                    //如果 (x, y) 在第 1 象限，则 0 < θ < π/2。
                    //如果 (x, y) 在第 2 象限，则 π/2 < θ≤π。
                    //如果 (x, y) 在第 3 象限，则 -π < θ < -π/2。
                    //如果 (x, y) 在第 4 象限，则 -π/2 < θ < 0。 
                    double dbPhy = Math.Atan2(dbY, dbX);
                    if (dbY < 0)
                    {
                        dbPhy += Math.PI * 2;
                    }

                    // 0 ≤ θ ≤ π
                    double dbTheta = Math.Acos(dbZ / dbR);

                    Random r = new Random(SubDivision.Chaos_GetRandomSeed());
                    dbTextureCoors[i, 0] = dbPhy / (2 * Math.PI);
                    dbTextureCoors[i, 1] = dbTheta / Math.PI;
                }
            }
            else
            {
                return false;
            }

            return true;
        }
    }
}
