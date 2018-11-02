using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibModelGen
{
    public class SubDivision
    {
        public static int Chaos_GetRandomSeed()
        {
            byte[] bytes = new byte[4];
            System.Security.Cryptography.RNGCryptoServiceProvider rng = new System.Security.Cryptography.RNGCryptoServiceProvider();
            rng.GetBytes(bytes);
            return BitConverter.ToInt32(bytes, 0);
        }

        private static void norm(ref double[,] a)
        {
            int nRows = a.GetLength(0);
            int nCols = a.GetLength(1);

            for (int i = 0; i < nRows; i++)
            {
                double dbResult = 0;

                for (int j = 0; j < nCols; j++)
                {
                    dbResult += a[i, j] * a[i, j];
                }
                dbResult = Math.Sqrt(dbResult);

                if (dbResult != 0)
                {
                    for (int j = 0; j < nCols; j++)
                    {
                        a[i, j] /= dbResult;
                    }
                }
            }
        }

        public static bool subDivision(ref double[,] dbVertice, ref ushort[,] nFaces, int nCount)
        {
            if (dbVertice == null || nFaces == null)
            {
                return false;
            }

            int nFaceNum = nFaces.GetLength(0);
            int nVerticeNum = dbVertice.GetLength(0);
            double[,] dbVerticeBackup = dbVertice;

            int nNewFaceNum = nFaceNum;
            int nNewVerticeNum = nVerticeNum;                       
            for (int i = 0; i < nCount; i++)
            {
                ushort[,] nNewFaces = new ushort[4 * nNewFaceNum, 3];
                double[,] dbNewVertices = new double[nNewVerticeNum + 3 * nNewFaceNum, 3];

                for (int m = 0; m < nNewVerticeNum; m++)
                {
                    dbNewVertices[m, 0] = dbVertice[m, 0];
                    dbNewVertices[m, 1] = dbVertice[m, 1];
                    dbNewVertices[m, 2] = dbVertice[m, 2];
                }

                for (int j = 0; j < nNewFaceNum; j++)
                {
                    dbNewVertices[nNewVerticeNum + 3 * j + 0, 0] = (dbVertice[nFaces[j, 0], 0] + dbVertice[nFaces[j, 1], 0]) / 2 ;
                    dbNewVertices[nNewVerticeNum + 3 * j + 0, 1] = (dbVertice[nFaces[j, 0], 1] + dbVertice[nFaces[j, 1], 1]) / 2;
                    dbNewVertices[nNewVerticeNum + 3 * j + 0, 2] = (dbVertice[nFaces[j, 0], 2] + dbVertice[nFaces[j, 1], 2]) / 2;

                    dbNewVertices[nNewVerticeNum + 3 * j + 1, 0] = (dbVertice[nFaces[j, 1], 0] + dbVertice[nFaces[j, 2], 0]) / 2;
                    dbNewVertices[nNewVerticeNum + 3 * j + 1, 1] = (dbVertice[nFaces[j, 1], 1] + dbVertice[nFaces[j, 2], 1]) / 2;
                    dbNewVertices[nNewVerticeNum + 3 * j + 1, 2] = (dbVertice[nFaces[j, 1], 2] + dbVertice[nFaces[j, 2], 2]) / 2;

                    dbNewVertices[nNewVerticeNum + 3 * j + 2, 0] = (dbVertice[nFaces[j, 2], 0] + dbVertice[nFaces[j, 0], 0]) / 2;
                    dbNewVertices[nNewVerticeNum + 3 * j + 2, 1] = (dbVertice[nFaces[j, 2], 1] + dbVertice[nFaces[j, 0], 1]) / 2;
                    dbNewVertices[nNewVerticeNum + 3 * j + 2, 2] = (dbVertice[nFaces[j, 2], 2] + dbVertice[nFaces[j, 0], 2]) / 2;

                    nNewFaces[nNewFaceNum + 3 * j + 0, 0] = nFaces[j, 0];
                    nNewFaces[nNewFaceNum + 3 * j + 0, 1] = (ushort)(nNewVerticeNum + 3 * j + 0);
                    nNewFaces[nNewFaceNum + 3 * j + 0, 2] = (ushort)(nNewVerticeNum + 3 * j + 2);

                    nNewFaces[nNewFaceNum + 3 * j + 1, 0] = nFaces[j, 1];
                    nNewFaces[nNewFaceNum + 3 * j + 1, 1] = (ushort)(nNewVerticeNum + 3 * j + 1);
                    nNewFaces[nNewFaceNum + 3 * j + 1, 2] = (ushort)(nNewVerticeNum + 3 * j + 0);

                    nNewFaces[nNewFaceNum + 3 * j + 2, 0] = nFaces[j, 2];
                    nNewFaces[nNewFaceNum + 3 * j + 2, 1] = (ushort)(nNewVerticeNum + 3 * j + 2);
                    nNewFaces[nNewFaceNum + 3 * j + 2, 2] = (ushort)(nNewVerticeNum + 3 * j + 1);

                    nNewFaces[j, 0] = (ushort)(nNewVerticeNum + 3 * j + 0);
                    nNewFaces[j, 1] = (ushort)(nNewVerticeNum + 3 * j + 1);
                    nNewFaces[j, 2] = (ushort)(nNewVerticeNum + 3 * j + 2);
                }

                nNewFaceNum = nNewFaces.GetLength(0);
                nNewVerticeNum = dbNewVertices.GetLength(0);
                dbVertice = dbNewVertices;
                nFaces = nNewFaces;
            }

            if (nCount != 0)
            {
                norm(ref dbVertice);
            }
            
            return true;
        }
    }
}
