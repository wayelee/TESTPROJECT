#include "computecontourline.h"

ComputeContourLine::ComputeContourLine()
{
}
ComputeContourLine::~ComputeContourLine()
{
    for(int i = 0; i < Doc.LineSet.size(); i++)
    {
        ContourLine* pline = Doc.LineSet.at(i);
        if(pline !=NULL)
        {
            delete pline;
            pline = NULL;
        }
    }
}

int ComputeContourLine::GetDataInfo(CmlRasterBand *pBand, double interval)
{
    if(pBand == NULL)
        return -1;
    GDALDataset* podataset =  pBand->GetDataset();
    CmlRasterDataset* pdataset = new CmlRasterDataset(podataset);
    Doc.ltx = 0;
    Doc.lty = 0;
    Doc.gridx = 0;//pdataset->m_pDataset->GetRasterXSize();
    Doc.gridy = 0;// pdataset->m_pDataset->GetRasterYSize();
    Doc.row = pdataset->GetRasterHeight();
    Doc.col = pdataset->GetRasterWidth();
    Doc.zoom = 1;
  //  Doc.zmax = pBand->poBand->GetMaximum();
  //  Doc.zmin = pBand->poBand->GetMinimum();
    Doc.zmax = -99999;
    Doc.zmin = 99999;
    DataDocument *pDoc = &Doc;
    char* pData;
    try
    {
        pData = new char[pdataset->GetRasterHeight()*pdataset->GetRasterWidth() * pBand->m_nPixelDataSize];
        pDoc->matrix=new double *[pDoc->row];
        for (int k=0;k<pDoc->row;k++)
        {
            pDoc->matrix[k]=new double [pDoc->col];
        }
        pBand->ReadRasterBlock(0,0,pdataset->GetRasterWidth(),pdataset->GetRasterHeight(), pData);
        switch (pBand->m_DataType)
        {
        case GDT_Unknown:
        {
            return -1;
            break;
        }
        case GDT_Byte:
        {
            CmlRasterDataBuffer<char> bf(pData);
            break;
        }
        case GDT_UInt16:
        {
            CmlRasterDataBuffer<unsigned short> bf(pData);
            break;
        }
        case GDT_Int16:
        {
            CmlRasterDataBuffer<short> bf(pData);
            for(int i =0; i< pDoc->row ; i++)
                for(int j = 0; j< pDoc->col; j++)
                {
                    pDoc->matrix[i][j] = (double)bf.Buffer[i * pDoc->col + j];
                    if(pDoc->matrix[i][j] <pDoc->zmin)
                        pDoc->zmin = pDoc->matrix[i][j];
                    if(pDoc->matrix[i][j] > pDoc->zmax)
                        pDoc->zmax = pDoc->matrix[i][j];
                }
            break;
        }
        case GDT_UInt32:
        {
            CmlRasterDataBuffer<int32_t> bf(pData);
            break;
        }
        case GDT_Int32:
        {
            CmlRasterDataBuffer<unsigned int> bf(pData);
            for(int i =0; i< pDoc->row ; i++)
                for(int j = 0; j< pDoc->col; j++)
                {
                    pDoc->matrix[i][j] = (double)bf.Buffer[i * pDoc->col + j];
                    if(pDoc->matrix[i][j] <pDoc->zmin)
                        pDoc->zmin = pDoc->matrix[i][j];
                    if(pDoc->matrix[i][j] > pDoc->zmax)
                        pDoc->zmax = pDoc->matrix[i][j];
                }
            break;
        }
        case GDT_Float32:
        {
            CmlRasterDataBuffer<float> bf(pData);
            break;
        }
        case GDT_Float64:
        {
            CmlRasterDataBuffer<double> bf(pData);
            break;
        }
        case GDT_CInt16:
        {
            CmlRasterDataBuffer<int16_t> bf(pData);
            break;
        }
        case GDT_CInt32:
        {
            CmlRasterDataBuffer<int32_t> bf(pData);
            break;
        }
        case GDT_CFloat32:
        {
            CmlRasterDataBuffer<float> bf(pData);
            break;
        }
        case GDT_CFloat64:
        {
            CmlRasterDataBuffer<double> bf(pData);
            break;
        }
        default:
        {
            break;
        }
        }
        pDoc->distant=interval*pDoc->zoom;
        // Label
        int i,j;
        flagx=new bool *[pDoc->row];
        flagy=new bool *[pDoc->row-1];
        for (i=0;i<pDoc->row;i++)
        {
            flagx[i]=new bool [pDoc->col-1];
        }
        for (i=0;i<pDoc->row-1;i++)
        {
            flagy[i]=new bool [pDoc->col];
        }
        for (i=0;i<pDoc->row;i++)
        {
            for(j=0;j<pDoc->col-1;j++)
            {
                flagx[i][j]=false;
            }
        }
        for (i=0;i<pDoc->row-1;i++)
        {
            for(j=0;j<pDoc->col;j++)
            {
                flagy[i][j]=false;
            }
        }
        return 0;
    }
    catch(...)
    {
        if(pData !=NULL)
        {
            delete[] pData;
            pData = NULL;
        }
        if(pDoc->matrix !=NULL)
        {
            for(int i = 0; i < pDoc->row; i++)
            {
                delete []pDoc->matrix[i];
            }
            delete []pDoc->matrix;
        }
        return -1;
    }
    delete pdataset;
}

void ComputeContourLine::Gaussfilter()
{
    DataDocument* pDoc = &Doc;
    int i,j;
    for (i=1;i<=pDoc->row-2;i++)
    {
        for (j=1;j<=pDoc->col-2;j++)
        {
            //if (pDoc->matrix[i][j]<pDoc->matrix[i-1][j-1]&&pDoc->matrix[i][j]<pDoc->matrix[i-1][j]&&pDoc->matrix[i][j]<pDoc->matrix[i-1][j+1]&&pDoc->matrix[i][j]<pDoc->matrix[i][j-1]&&pDoc->matrix[i][j]<pDoc->matrix[i][j+1]&&pDoc->matrix[i][j]<pDoc->matrix[i+1][j-1]&&pDoc->matrix[i][j]<pDoc->matrix[i+1][j]&&pDoc->matrix[i][j]<pDoc->matrix[i+1][j+1])
            {  //计算高斯滤波所采用的数据不应该为经高斯滤波处理过的数据点
                pDoc->matrix[i][j]=(pDoc->matrix[i-1][j-1]+2*pDoc->matrix[i-1][j]+pDoc->matrix[i-1][j+1]+2*pDoc->matrix[i][j-1]+2*pDoc->matrix[i][j+1]+pDoc->matrix[i+1][j-1]+2*pDoc->matrix[i+1][j]+pDoc->matrix[i+1][j+1]+4*pDoc->matrix[i][j])/16;
            }
            //if (pDoc->matrix[i][j]>pDoc->matrix[i-1][j-1]&&pDoc->matrix[i][j]>pDoc->matrix[i-1][j]&&pDoc->matrix[i][j]>pDoc->matrix[i-1][j+1]&&pDoc->matrix[i][j]>pDoc->matrix[i][j-1]&&pDoc->matrix[i][j]>pDoc->matrix[i][j+1]&&pDoc->matrix[i][j]>pDoc->matrix[i+1][j-1]&&pDoc->matrix[i][j]>pDoc->matrix[i+1][j]&&pDoc->matrix[i][j]>pDoc->matrix[i+1][j+1])
            {
                //	   pDoc->matrix[i][j]=(pDoc->matrix[i-1][j-1]+pDoc->matrix[i-1][j]+pDoc->matrix[i-1][j+1]+pDoc->matrix[i][j-1]+pDoc->matrix[i][j+1]+pDoc->matrix[i+1][j-1]+pDoc->matrix[i+1][j]+pDoc->matrix[i+1][j+1]+pDoc->matrix[i][j])/9;
            }
        }
    }
}

void ComputeContourLine::cacculation()
{
        DataDocument* pDoc = &Doc;
    float cru_height=float(pDoc->zmin+pDoc->distant);
    for(cru_height=pDoc->zmin+pDoc->distant;cru_height<pDoc->zmax;cru_height+=pDoc->distant)
        {
        ScanLine(cru_height);
        }
        /*
        int t=pDoc->LineSet.GetSize();
        ContourLine * temline;
        for (int i=t-1;i>=0;i--)
        {
                temline=pDoc->LineSet.at(i);
                if(temline->m_Points.GetSize()<=3||
                        (temline->m_Points.at(0).x>pDoc->ltx&&temline->m_Points.at(0).x<pDoc->ltx+pDoc->gridx*(pDoc->col-2)&&
                        temline->m_Points.at(0).y>pDoc->lty&&temline->m_Points.at(0).y<pDoc->lty+pDoc->gridy*(pDoc->row-2)&&
                        temline->m_Points.at(temline->m_Points.size()-1).x>pDoc->ltx&&temline->m_Points.at(temline->m_Points.size()-1).x<pDoc->ltx+pDoc->gridx*(pDoc->col-2)&&
                        temline->m_Points.at(temline->m_Points.size()-1).y>pDoc->lty&&temline->m_Points.at(temline->m_Points.size()-1).y<pDoc->lty+pDoc->gridy*(pDoc->row-2)&&
                        temline->m_Points.at(0).x!=temline->m_Points.at(temline->m_Points.size()-1).x&&
                        temline->m_Points.at(0).y!=temline->m_Points.at(temline->m_Points.size()-1).y))
                {
                        pDoc->LineSet.RemoveAt(i);
                }
        }
        */
        int t= pDoc->LineSet.size();
//        pDoc->LineSet.end();
        ContourLine * temline;
        for (int i=t-1;i>=0;i--)
        {
            temline=pDoc->LineSet.at(i);
            if(temline->m_Points.size()<=3||
                        (temline->m_Points.at(0).x>pDoc->ltx&&temline->m_Points.at(0).x<pDoc->ltx+pDoc->gridx*(pDoc->col-2)&&
                        temline->m_Points.at(0).y>pDoc->lty&&temline->m_Points.at(0).y<pDoc->lty+pDoc->gridy*(pDoc->row-2)&&
                        temline->m_Points.at(temline->m_Points.size()-1).x>pDoc->ltx&&temline->m_Points.at(temline->m_Points.size()-1).x<pDoc->ltx+pDoc->gridx*(pDoc->col-2)&&
                        temline->m_Points.at(temline->m_Points.size()-1).y>pDoc->lty&&temline->m_Points.at(temline->m_Points.size()-1).y<pDoc->lty+pDoc->gridy*(pDoc->row-2)&&
                        temline->m_Points.at(0).x!=temline->m_Points.at(temline->m_Points.size()-1).x&&
                        temline->m_Points.at(0).y!=temline->m_Points.at(temline->m_Points.size()-1).y))
                {
                        pDoc->LineSet.erase(pDoc->LineSet.begin() + i);
                }
        }
}

void ComputeContourLine::ScanLine(float cru_height)
{
    DataDocument* pDoc = &Doc;
    double **mat;
    mat=pDoc->matrix;
    int j,i;
    ContourLine * newline;
    int count=0;
    //存放横边上的等值点数组,默认值位-2，表示没有等值点，若有等值点，存储该点到数据点[i][j]的距离
    float **SS;
    //存放竖边上的等值点数组，默认值位-2，表示没有等值点，若有等值点，存储该点到数据点[i][j]的距离
    float **HH;
    SS = new float*[pDoc->row];
    for(int i = 0; i< pDoc->row ; i++)
    {
        SS[i] = new float[pDoc->col - 1];
        for(int j = 0; j < pDoc->col - 1; j++);
        {
            // 当等高线高程值与数据点值相等，为了在追踪时不造成混乱，需要将数据点加上一个足够小的数值予以纠正,
            if(cru_height == mat[i][j])
            {
                mat[i][j] += 1;
            }
            if(cru_height == mat[i][j+1])
            {
                mat[i][j+1] += 1;
            }
            if((cru_height-mat[i][j])*(cru_height-mat[i][j+1])<0)
            {
                SS[i][j] = (cru_height - mat[i][j])/(mat[i][j+1] - mat[i][j]);
            }
            else
            {
                SS[i][j] = -2;
            }
        }
    }
    HH = new float*[pDoc->row -1];
    for(int i =0; i<pDoc->row -1; i++)
    {
        HH[i] = new float[pDoc->col];
        for(int j =0; j< pDoc->col ; j++)
        {
            // 当等高线高程值与数据点值相等，为了在追踪时不造成混乱，需要将数据点加上一个足够小的数值予以纠正,
            if(cru_height == mat[i][j])
            {
                mat[i][j] += 1;
            }
            if(cru_height == mat[i+1][j])
            {
                mat[i+1][j] += 1;
            }
            if((cru_height-mat[i][j])*(cru_height-mat[i+1][j])<0)
            {
                HH[i][j] = (cru_height - mat[i][j])/(mat[i+1][j] - mat[i][j]);
            }
            else
            {
                HH[i][j] = -2;
            }
        }
    }



        for(j=0;j<=pDoc->row-1;j+=pDoc->row-1)
        {
          for (i=0;i<=pDoc->col-2;i++)
          {
                float cru=cru_height;
                // 当等值点等于DEM数据点的时候应该改变数据点的数值，而不应该改变等值点高程
                if (cru_height==mat[j][i]||cru_height==mat[j][i+1]) cru_height-=1;
                //if (cru_height==mat[j][i]||cru_height==mat[j][i+1]) {mat[j][i]-=1;mat[j][i+1]-=1;}
                if (flagx[j][i]==false&&(cru_height-mat[j][i])*(cru_height-mat[j][i+1])<0)
                {
                        POINTNEW P;
                        P.x=pDoc->ltx+pDoc->gridx*i+(cru_height-mat[j][i])*pDoc->gridx/(mat[j][i+1]-mat[j][i]);
                        P.y=pDoc->lty+pDoc->gridy*j;
                        flagx[j][i]=true;
                        newline=new ContourLine;
                        newline->height=cru_height;
                        //newline->m_Points.Add(P);
                        newline->m_Points.push_back(P);
                        temple tem;
                        tem.a=j;tem.b=i;tem.Direction=1;
                        do
                        {
                            tem=RecruScan(newline,tem,cru_height,mat);

                                if (tem.Direction==1)
                                {
                                        P.x=pDoc->ltx+pDoc->gridx*tem.b+pDoc->gridx*(cru_height-mat[tem.a][tem.b])/(mat[tem.a][tem.b+1]-mat[tem.a][tem.b]);
                                        P.y=pDoc->lty+pDoc->gridy*tem.a;
                                        newline->m_Points.push_back(P);
                                }
                                else if(tem.Direction==2)
                                {
                                        P.x=pDoc->ltx+pDoc->gridx*tem.b;
                                        P.y=pDoc->lty+pDoc->gridy*tem.a+pDoc->gridy*(cru_height-mat[tem.a][tem.b])/(mat[tem.a+1][tem.b]-mat[tem.a][tem.b]);
                                    newline->m_Points.push_back(P);
                                }
                        }while(tem.Direction!=0);
                        pDoc->LineSet.push_back(newline);
                        count++;
                }
                if ((cru==mat[j][i]||cru==mat[j][i+1])&&flagx[j][i]==false)  cru_height=cru;
          }
        }

        //扫描纵向边界
        for(j=0;j<=pDoc->col-1;j+=pDoc->col-1)
        {
          for (i=0;i<=pDoc->row-2;i+=1)
          {
                float cru=cru_height;
                // 当等值点等于DEM数据点的时候应该改变数据点的数值，而不应该改变等值点高程
                if (cru_height==mat[i][j]||cru_height==mat[i+1][j]) cru_height-=1;
               // if (cru_height==mat[i][j]||cru_height==mat[i+1][j]){ mat[i][j]-=1;mat[i+1][j] -=1;}
                if (flagy[i][j]==false&&(cru_height-mat[i][j])*(cru_height-mat[i+1][j])<0)
                {
                        POINTNEW P;
                        P.x=pDoc->ltx+pDoc->gridx*j;
                        P.y=pDoc->lty+pDoc->gridy*i+pDoc->gridy*(cru_height-mat[i][j])/(mat[i+1][j]-mat[i][j]);
                        flagy[i][j]=true;
                        newline=new ContourLine;
                        newline->height=cru_height;
                        newline->m_Points.push_back(P);
                        temple tem;
                        tem.a=i;tem.b=j;tem.Direction=2;
                        do
                        {
                tem=RecruScan(newline,tem,cru_height,mat);

                                if (tem.Direction==1)
                                {
                                        P.x=pDoc->ltx+pDoc->gridx*tem.b+pDoc->gridx*(cru_height-mat[tem.a][tem.b])/(mat[tem.a][tem.b+1]-mat[tem.a][tem.b]);
                                        P.y=pDoc->lty+pDoc->gridy*tem.a;
                                        newline->m_Points.push_back(P);
                                }
                                else if(tem.Direction==2)
                                {
                                        P.x=pDoc->ltx+pDoc->gridx*tem.b;
                                        P.y=pDoc->lty+pDoc->gridy*tem.a+pDoc->gridy*(cru_height-mat[tem.a][tem.b])/(mat[tem.a+1][tem.b]-mat[tem.a][tem.b]);
                                    newline->m_Points.push_back(P);
                                }
                        }while(tem.Direction!=0);
                        pDoc->LineSet.push_back(newline);
                        count++;
                }
                if ((cru==mat[i][j]||cru==mat[i+1][j])&&flagy[i][j]==false)  cru_height=cru;
          }
        }

        //扫描内部格网
        for(j=1;j<=pDoc->row-2;j+=1)
        {
          for (i=0;i<=pDoc->col-2;i++)
          {
                float cru=cru_height;
                // 当等值点等于DEM数据点的时候应该改变数据点的数值，而不应该改变等值点高程
                if (cru_height==mat[j][i]||cru_height==mat[j][i+1]) cru_height-=1;
               // if (cru_height==mat[j][i]||cru_height==mat[j][i+1]){ mat[j][i]-=1;mat[j][i+1]-=1;}
                if (flagx[j][i]==false&&(cru_height-mat[j][i])*(cru_height-mat[j][i+1])<0)
                {
                        POINTNEW P;
                        P.x=pDoc->ltx+pDoc->gridx*i+(cru_height-mat[j][i])*pDoc->gridx/(mat[j][i+1]-mat[j][i]);
                        P.y=pDoc->lty+pDoc->gridy*j;
                        flagx[j][i]=true;
                        newline=new ContourLine;
                        newline->height=cru_height;
                        newline->m_Points.push_back(P);
                        temple tem;
                        tem.a=j;tem.b=i;tem.Direction=1;
                        do
                        {
                tem=RecruScan(newline,tem,cru_height,mat);

                                if (tem.Direction==1)
                                {
                                        P.x=pDoc->ltx+pDoc->gridx*tem.b+pDoc->gridx*(cru_height-mat[tem.a][tem.b])/(mat[tem.a][tem.b+1]-mat[tem.a][tem.b]);
                                        P.y=pDoc->lty+pDoc->gridy*tem.a;
                                        newline->m_Points.push_back(P);
                                }
                                else if(tem.Direction==2)
                                {
                                        P.x=pDoc->ltx+pDoc->gridx*tem.b;
                                        P.y=pDoc->lty+pDoc->gridy*tem.a+pDoc->gridy*(cru_height-mat[tem.a][tem.b])/(mat[tem.a+1][tem.b]-mat[tem.a][tem.b]);
                                    newline->m_Points.push_back(P);
                                }
                        }while(tem.Direction!=0);
                        POINTNEW temP=newline->m_Points.at(newline->m_Points.size() - 1);
                        if (dist(newline->m_Points.at(0),temP)<3*pDoc->gridx&&newline->m_Points.size()>3)
                        {
                            newline->m_Points.push_back(newline->m_Points.at(0));
                        }
                        pDoc->LineSet.push_back(newline);
                        count++;
                }
                if ((cru==mat[j][i]||cru==mat[j][i+1])&&flagx[j][i]==false)  cru_height=cru;
          }
        }
    //整合
        Integration(count);
        count=0;
}

float ComputeContourLine::dist(POINTNEW P1, POINTNEW P2)
{
        float a=(float)sqrt((P1.x-P2.x)*(P1.x-P2.x)+(P1.y-P2.y)*(P1.y-P2.y));
        return a;
}

temple ComputeContourLine::RecruScan(ContourLine *newline,temple tem, float h, double **mat)
{
       // DataDocument* pDoc =&Doc;
        float dis[6]={99999,99999,99999,99999,99999,99999};
        POINTNEW P=newline->m_Points.at(newline->m_Points.size()-1);
        switch(tem.Direction)
        {
           case 1: dis[0]=intersectornot(2,h,tem.a-1,tem.b,mat,P);
                   dis[1]=intersectornot(1,h,tem.a-1,tem.b,mat,P);
                   dis[2]=intersectornot(2,h,tem.a-1,tem.b+1,mat,P);
                   dis[3]=intersectornot(2,h,tem.a,tem.b+1,mat,P);
                   dis[4]=intersectornot(1,h,tem.a+1,tem.b,mat,P);
                   dis[5]=intersectornot(2,h,tem.a,tem.b,mat,P);
                   break;
           case 2: dis[0]=intersectornot(1,h,tem.a,tem.b,mat,P);
                   dis[1]=intersectornot(2,h,tem.a,tem.b+1,mat,P);
                   dis[2]=intersectornot(1,h,tem.a+1,tem.b,mat,P);
                   dis[3]=intersectornot(1,h,tem.a+1,tem.b-1,mat,P);
                   dis[4]=intersectornot(2,h,tem.a,tem.b-1,mat,P);
                   dis[5]=intersectornot(1,h,tem.a,tem.b-1,mat,P);
                   break;
        }

        float d;
        d=99999;
        int k=0;
        if (dis[0]!=99999||dis[2]!=99999||dis[3]!=99999||dis[5]!=99999)
        {
                for(int i=0;i<=5;i++)
                {
                  if (i==1||i==4) continue;
                  if (dis[i]<d) {d=dis[i];k=i;}
                }
        }
        else
        {
           for(int i=0;i<=5;i++)
           {
             if (dis[i]<d) {d=dis[i];k=i;}
           }
        }

        if (d==99999) {tem.Direction=0;return tem;}
    int mark=tem.Direction;
        if (mark==1)
        {
                switch(k)
                {
                   case 0: tem.a-=1;tem.Direction=2;break;
                   case 1: tem.a-=1;tem.Direction=1;break;
                   case 2: tem.a-=1;tem.b+=1;tem.Direction=2;break;
                   case 3: tem.b+=1;tem.Direction=2;break;
                   case 4: tem.a+=1;tem.Direction=1;break;
                   case 5: tem.Direction=2;break;
                }
        }
        else if(mark==2)
        {
                switch(k)
                {
                   case 0: tem.Direction=1;break;
                   case 1: tem.b+=1;tem.Direction=2;break;
                   case 2: tem.a+=1;tem.Direction=1;break;
                   case 3: tem.a+=1;tem.b-=1;tem.Direction=1;break;
                   case 4: tem.b-=1;tem.Direction=2;break;
                   case 5: tem.b-=1;tem.Direction=1;break;
                }
        }

        return tem;
}

float ComputeContourLine::intersectornot(int arctype,float h,int i,int j,double **mat,POINTNEW P)
{
        DataDocument* pDoc = &Doc;
        float dis=99999;
        switch (arctype)
        {
           case 1:   if(i<0||i>=pDoc->row||j<0||j>=pDoc->col-1) return 99999;
                                 if(flagx[i][j]==true) return 99999;
                                 //
                         if (h==mat[i][j]||h==mat[i][j+1]) h+=1;
                         if (((h-mat[i][j])*(h-mat[i][j+1])<0)&&flagx[i][j]==false)
                                 {
                                    POINTNEW P1;
                                    P1.x=pDoc->ltx+pDoc->gridx*j+pDoc->gridx*(h-mat[i][j])/(mat[i][j+1]-mat[i][j]);
                                    P1.y=pDoc->lty+pDoc->gridy*i;
                                    dis=dist(P1,P);
                                        flagx[i][j]=true;
                                 }
                             break;
           case 2:    if (i<0||i>=pDoc->row-1||j<0||j>=pDoc->col) return 99999;
                                  if (flagy[i][j]==true) return 99999;
                          if (h==mat[i][j]||h==mat[i+1][j]) h+=1;
                          if (((h-mat[i][j])*(h-mat[i+1][j])<0)&&flagy[i][j]==false)
                                  {
                                     POINTNEW P1;
                                     P1.x=pDoc->ltx+pDoc->gridx*j;
                                     P1.y=pDoc->lty+pDoc->gridy*i+pDoc->gridy*(h-mat[i][j])/(mat[i+1][j]-mat[i][j]);
                                     dis=dist(P1,P);
                                         flagy[i][j]=true;
                                  }
                             break;
        }
        return dis;
}

void ComputeContourLine::Integration(int count)
{
        DataDocument* pDoc = &Doc;
        int bottom=pDoc->LineSet.size()-1-count+1;
        int i,j;
        for(i=bottom;i<= (int)pDoc->LineSet.size()-1-1;i++)
        {
                ContourLine *pi=pDoc->LineSet.at(i);
                for(j=i+1;j<=pDoc->LineSet.size()-1 ;j++)
                {
                    ContourLine *pj=pDoc->LineSet.at(j);
                    if (pi->m_Points.at(0).x==pi->m_Points.at(pi->m_Points.size()-1).x&&pi->m_Points.at(0).y==pi->m_Points.at(pi->m_Points.size()-1).y)
                        break;
                    if (pj->m_Points.at(0).x==pj->m_Points.at(pj->m_Points.size()-1).x&&pj->m_Points.at(0).y==pj->m_Points.at(pj->m_Points.size()-1).y)
                        continue;
                    if (dist(pi->m_Points.at(pi->m_Points.size()-1),pj->m_Points.at(0))<4*pDoc->gridx)
                    {
                        for (int k=0;k<=pj->m_Points.size() - 1;k++)
                        {
                            pi->m_Points.push_back(pj->m_Points.at(k));
                        }
                        pDoc->LineSet.erase(pDoc->LineSet.begin() + j);
                        i--;
                        break;
                    }
                    if (dist(pi->m_Points.at(pi->m_Points.size()-1),pj->m_Points.at(pj->m_Points.size()-1))<4*pDoc->gridx)
                    {
                        for (int k=pj->m_Points.size()-1;k>=0;k--)
                        {
                            pi->m_Points.push_back(pj->m_Points.at(k));
                        }
                        pDoc->LineSet.erase(pDoc->LineSet.begin() + j);
                        i--;
                        break;
                    }
                    if (dist(pi->m_Points.at(0),pj->m_Points.at(0))<4*pDoc->gridx)
                    {
                        for (int k=0;k<=pj->m_Points.size()-1;k++)
                        {
                            pi->m_Points.insert(pi->m_Points.begin() + 0,pj->m_Points.at(k));
                        }
                        pDoc->LineSet.erase(pDoc->LineSet.begin() + j);
                        i--;
                        break;
                    }
                    if (dist(pi->m_Points.at(0),pj->m_Points.at(pj->m_Points.size()-1))<4*pDoc->gridx)
                    {
                        for (int k=pj->m_Points.size()-1;k>=0;k--)
                        {
                            pi->m_Points.insert(pi->m_Points.begin() + 0 ,pj->m_Points.at(k));
                        }
                        pDoc->LineSet.erase(pDoc->LineSet.begin() + j);
                        i--;
                        break;
                    }
                }
                if (dist(pi->m_Points.at(0),pi->m_Points.at(pi->m_Points.size()-1))<3*pDoc->gridx)
                {
                        pi->m_Points.push_back(pi->m_Points.at(0));
                }
        }
}

void ComputeContourLine::ComputeLines()
{
    Gaussfilter();
    cacculation();
}
