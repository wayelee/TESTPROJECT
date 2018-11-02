using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using lib3ds.Net;

namespace LibModelGen
{
    public enum ModelType
    {
        ModelCrater=0,
        //四面体
        ModelTetrahedron,
        //立方体
        ModelCubic,
        //四棱台
        ModelPyramid
    }

    public enum TriType
    {
        TriForward = 0,
        TriBackward,
        TriSubdivision
    }

    public enum MappingType
    {
        //平面投影
        Flat=0,

        //球体投影
        Sphere
    }

    public class Point3D
    {
        private double x, y, z;

        public double X
        {
            get
            {
                return x;
            }

            set
            {
                x = value;
            }
        }

        public double Y
        {
            get
            {
                return y;
            }

            set
            {
                y = value;
            }
        }

        public double Z
        {
            get
            {
                return z;
            }

            set
            {
                z = value;
            }
        }

        public Point3D()
        {
            x = 0;
            y = 0;
            z = 0;
        }

        public Point3D(double a, double b, double c)
        {
            x = a;
            y = b;
            z = c;
        }
    }

    public class Point2D
    {
        private double x, y;

        public double X
        {
            get
            {
                return x;
            }

            set
            {
                x = value;
            }
        }

        public double Y
        {
            get
            {
                return y;
            }

            set
            {
                y = value;
            }
        }

        public Point2D()
        {
            x = 0;
            y = 0;
        }

        public Point2D(double a, double b)
        {
            x = a;
            y = b;
        }
    }

    public abstract class ModelBase
    {
        //输出3DS路径名
        protected String mSzOutputFilename;
        //三角化类型
        protected TriType mTriType;
        //细化次数
        protected ushort uSubDivisionCount;
        //纹理路径
        protected String mSzTextureFilename;
        //纹理映射方式
        protected MappingType mMappingType;
        //模型类型
        protected ModelType mModelType;
        //模型点集数据
        protected Point3D[,] mPtDataInMeshForm;
        protected Point3D[] mPtData;

        //三角化后各个面的顶点号索引
        protected ushort[,] uFacesIndex;
        //顶点坐标
        protected double[,] mDbVertices;
        //纹理坐标
        protected double[,] mDbTextureCoors;
        
        //输出3DS路径名,可读可写属性
        public String OutputFilename
        {
            set
            {
                mSzOutputFilename = value;
            }

            get
            {
                return mSzOutputFilename;
            }
        }

        //三角化类型,可读可写属性
        public TriType triype
        {
            set
            {
                mTriType = value;
            }

            get
            {
                return mTriType;
            }
        }

        //细化次数，可读可写属性
        public ushort SubDivisionCount
        {
            get
            {
                return uSubDivisionCount;
            }

            set
            {
                uSubDivisionCount = value;
            }
        }

        //纹理图路径, 可读可写属性
        public String TexutreFilename
        {
            get
            {
                return mSzTextureFilename;
            }

            set
            {
                mSzTextureFilename = value;
            }
        }

        //纹理映射方式, 可读可写属性
        public MappingType mappingType
        {
            get
            {
                return mMappingType;
            }

            set
            {
                mMappingType = value;
            }
        }

        //生成模型的标识,只读属性
        public ModelType modelType
        {
            get
            {
                return mModelType;
            }
        }

        //格网形式的三维点数据，只读属性
        public Point3D[,] PtDataInMeshForm
        {
            get
            {
                return mPtDataInMeshForm;
            }
        }

        //三维点数据，只读属性
        public Point3D[] PtData
        {
            get
            {
                return mPtData;
            }
        }

        //三角化后各个面上的顶点号索引,F,只读属性
        public ushort[,] FacesIndex
        {
            get
            {
                return uFacesIndex;
            }
        }

        //顶点坐标,V, 只读属性
        public double[,] Vertices
        {
            get
            {
                return mDbVertices;
            }
        }

        //纹理坐标, 只读属性
        public double[,] TextureCoors
        {
            get
            {
                return mDbTextureCoors;
            }
        }

        protected ModelBase()
        {
            mPtData = null;
            mPtDataInMeshForm = null;
            uFacesIndex = null;
            mDbVertices = null;
            mDbTextureCoors = null;
            mSzTextureFilename = String.Empty;
            mSzOutputFilename = String.Empty;
        }

        //计算模型点集
        protected abstract bool calcModelPts();

        //三角化
        protected virtual bool triangulation()
        {
            return true;
        }

        //拉伸
        protected abstract bool strechPts();

        //计算纹理坐标
        protected virtual bool calcTextureCoor()
        {
            return TextureMapping.calcTextureCoor(mDbVertices, out mDbTextureCoors, mMappingType);
        }

        //写3DS文件
        protected virtual bool writeToFile()
        {
            if (mDbVertices == null || mDbTextureCoors == null || uFacesIndex == null)
                return false;

            //新建LIB3DS文件对象
            Lib3dsFile file = LIB3DS.lib3ds_file_new();
            file.frames = 360;

            //新建网格节点
            Lib3dsMesh mesh = LIB3DS.lib3ds_mesh_new("mesh");
            Lib3dsMeshInstanceNode inst;
            LIB3DS.lib3ds_file_insert_mesh(file, mesh, -1);

            //一、将顶点写入网格
            int nVertices = mDbVertices.GetLength(0);
            LIB3DS.lib3ds_mesh_resize_vertices(mesh, (ushort)nVertices, true, false);
            for (int i = 0; i < nVertices; i++)
            {
                Lib3dsVertex vertexTmp=new Lib3dsVertex(mDbVertices[i, 0], mDbVertices[i, 1], mDbVertices[i, 2]);
                LIB3DS.lib3ds_vector_copy(mesh.vertices[i], vertexTmp);

                //将纹理坐标写入网格
                mesh.texcos[i] = new Lib3dsTexturecoordinate(mDbTextureCoors[i, 0], mDbTextureCoors[i, 1]);
            }

            //二、将纹理信息写入文件
            Lib3dsMaterial mat = LIB3DS.lib3ds_material_new("material1");
            LIB3DS.lib3ds_file_insert_material(file, mat, -1);

            //如果没有指定纹理，则默认为灰色材质
            if (String.IsNullOrEmpty(mSzTextureFilename))
            {
                mat.diffuse[0] = 0.5f;
                mat.diffuse[1] = 0.5f;
                mat.diffuse[2] = 0.5f;
            }
            else
            {
                mat.texture1_map.name = mSzTextureFilename;
                mat.texture1_map.percent = 1.0f;
            }

            //三、将三角化后的面的顶点索引号写入网格
            int nFaces = uFacesIndex.GetLength(0);
            LIB3DS.lib3ds_mesh_resize_faces(mesh, (ushort)nFaces);
            for (int i = 0; i < nFaces; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    mesh.faces[i].index[j] = uFacesIndex[i, j];
                }

                //指定每个三角化后的面的材质
                mesh.faces[i].material = 0;
            }

            inst = LIB3DS.lib3ds_node_new_mesh_instance(mesh, "01", null, null, null);
            LIB3DS.lib3ds_file_append_node(file, inst, null);

            if (!LIB3DS.lib3ds_file_save(file, mSzOutputFilename))
            {
                LIB3DS.lib3ds_file_free(file);
                return false;
            }

            LIB3DS.lib3ds_file_free(file);
            return true;
        }

        //执行生成过程，并写成3DS文件
        public bool generate()
        {
            if (!calcModelPts())
            {
                return false;
            }

            bool bFlag = false;
            if (modelType==ModelType.ModelCrater)
            {
                bFlag = triangulation();
            }
            else
            {
                if (!makeFace())
                {
                    return false;
                }

                bFlag = SubDivision.subDivision(ref mDbVertices, ref uFacesIndex, uSubDivisionCount);
            }

            if (!bFlag)
            {
                return false;
            }

            //拉伸
            if (!strechPts())
            {
                return false;
            }

            if (!calcTextureCoor())
            {
                return false;
            }

            return writeToFile();
        }

        //读取3DS文件，返回成功标识
        public static bool readFile(String szFilename)
        {
            Lib3dsFile file = null;
            file=LIB3DS.lib3ds_file_open(szFilename);
            return (file != null);
        }

        //将点集生成F和V，用于细化操作
        private bool makeFace()
        {
            if (mPtData == null)
                return false;

            if (modelType == ModelType.ModelCrater)
            {
                return false;
            }
            else if (modelType == ModelType.ModelCubic || modelType == ModelType.ModelPyramid)
            {
                if (mPtData.Length != 8)
                {
                    return false;
                }

                mDbVertices = new double[8, 3];
                for (int i = 0; i < 8; i++)
                {
                    mDbVertices[i, 0] = mPtData[i].X;
                    mDbVertices[i, 1] = mPtData[i].Y;
                    mDbVertices[i, 2] = mPtData[i].Z;
                }

                uFacesIndex = new ushort[,]
                {
                    {0, 5, 1},
                    {0, 4, 5},
                    {1, 6, 2},
                    {1, 5, 6},
                    {2, 6, 7},
                    {2, 7, 3},
                    {0, 3, 7},
                    {0, 7, 4},
                    {0, 1, 2},
                    {0, 2, 3},
                    {4, 7, 6},
                    {4, 6, 5}
                };

                return true;
            }
            else if (modelType == ModelType.ModelTetrahedron)
            {
                if (mPtData.Length != 4)
                {
                    return false;
                }

                mDbVertices = new double[4, 3];
                for (int i = 0; i < 4; i++)
                {
                    mDbVertices[i, 0] = mPtData[i].X;
                    mDbVertices[i, 1] = mPtData[i].Y;
                    mDbVertices[i, 2] = mPtData[i].Z;
                }

                uFacesIndex = new ushort[,]
                {
                    {0, 2, 1},
                    {0, 1, 3},
                    {0, 3, 2},
                    {1, 2, 3}
                };

                return true;
            }
            else
            {
                return false;
            }
            
            return true;
        }
    }
}
