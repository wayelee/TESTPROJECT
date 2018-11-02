#include"DrawLabel.h"
#include <QtGui>
#include"CustomScroll.h"


DrawLabel::DrawLabel(  )
{
    image = NULL;
    Lineset = NULL;
    // FatherScroll = NULL;
    colorRed.setRed(255);

    colorGreen.setGreen(255);

    colorYellow.setGreen(0);
    colorYellow.setRed(0);
    colorYellow.setBlue(255);

    colorlightYellow.setGreen(100);
    colorlightYellow.setRed(100);
    colorlightYellow.setBlue(255);

    colorlightRed.setRed(255);
    colorlightRed.setGreen(255);

    setMouseTracking(true);
    DrawRect = false;
}
void DrawLabel::paintEvent(QPaintEvent */*event*/)
{
    QPainter painter(this);

    if(image != NULL)
    {
        QPoint pt(DrawPoint.x(),DrawPoint.y());
        painter.drawImage(pt,*image);
        double cx1,cy1;

        DrawFeatPoints(&painter);

        DrawSelectedFeatPoints(&painter);
        if(DrawRect)
        {
            DrawSelectRect(&painter,rect);
        }

        /*
                POINTNEW pt3;
                pt3.x = 2;
                pt3.y = 2;

                FatherScroll->RasterLocationToCanvasLocation(pt3.x,pt3.y, cx1,cy1);
                painter.drawEllipse(cx1,cy1,9,9);
                pt3.x = 4;
                pt3.y = 4;
                FatherScroll->RasterLocationToCanvasLocation(pt3.x,pt3.y,cx1,cy1);
                painter.drawEllipse(cx1,cy1,9,9);
                FatherScroll->RasterLocationToCanvasLocation(2.5,2.5,cx1,cy1);
                painter.drawEllipse(cx1,cy1,9,9);
        */

        /*
         for(int i = 0; i< 100; i ++)
         {
             for(int j = 0; j< 1000; j++)
             {
                 FatherScroll->RasterLocationToCanvasLocation(i,j,cx1,cy1);
                 painter.drawEllipse(cx1,cy1,9,9);
             }

         }
         */
    }
    else
    {
        this->setGeometry(0,0,0,0);
    }


    //painter.setPen(Qt::blue);
    //           painter.setFont(QFont("Arial", 30));
    //        painter.drawText(rect(), Qt::AlignCenter, "Qt");
}

void DrawLabel::keyPressEvent ( QKeyEvent * event )
{
    int ddd = 0;
}

void DrawLabel::mouseMoveEvent(QMouseEvent * event)
{
    int cx = event->x();
    int cy = event->y();
    emit mouseMoveSignal(cx,cy);
    // 拖拽选中点
    double rx,ry;
    // dx = event->x();dy = event->y();


//      rx = (FatherScroll->ImageLeftTopInCanvas.x + dx - FatherScroll->RasterRect.left) * FatherScroll->scaleFactor -0.5;
//      ry = (FatherScroll->ImageLeftTopInCanvas.y + dy - FatherScroll->RasterRect.top) * FatherScroll->scaleFactor-0.5;

    if(FatherScroll->RasterRect.left < 0)
    {
        rx = event->x() * FatherScroll->scaleFactor -0.5 ;
    }
    else
    {
        rx = (event->x() - FatherScroll->RasterRect.left) * FatherScroll->scaleFactor  -0.5 ;
    }
    if(FatherScroll->RasterRect.top < 0)
    {
        ry = event->y() * FatherScroll->scaleFactor  -0.5;
    }
    else
    {
        ry = (event->y() - FatherScroll->RasterRect.top) * FatherScroll->scaleFactor  -0.5 ;
    }
    if(FatherScroll->CurrentToolType == Tool_EditMatchPoint)
    {
        // 拖拽点
        if(FatherScroll->MouseDown && FatherScroll->PickedAPoint)
        {
            // 捕捉到点时应该只有一个选中点,编辑自动点会该点从自动点集中删除，然后在手动点集中添加。
            if(FatherScroll->FeatAutoSelectedIdx.size() == 1)
            {
                // int addsetsize;
                //double rx,ry;
                // FatherScroll->CanvasLocationToRasterLocation(event->x(),event->y(),rx,ry);
                int idx = FatherScroll->FeatAutoSelectedIdx.at(0);
                ULONG lID = FatherScroll->FeatPtDataSet->m_ImgPtSet.vecPts.at(idx).lID;
                BYTE IsMatch = FatherScroll->FeatPtDataSet->m_ImgPtSet.vecPts.at(idx).byIsMatch;
                // addsetsize = FatherScroll->FeatPtDataSet->m_ImgPtSet.vecAddPts.size();
                FatherScroll->FeatPtDataSet->EditAutoPtByIndxe(FatherScroll->FeatAutoSelectedIdx.at(0),rx,ry,lID);
                vector<Pt2d>::iterator it = FatherScroll->FeatPtDataSet->m_ImgPtSet.vecAddPts.end();
                (--it)->byIsMatch = IsMatch;

                FatherScroll->FeatAutoSelectedIdx.clear();
                FatherScroll->FeatManualSelectedIdx.clear();
                // addsetsize = FatherScroll->FeatPtDataSet->m_ImgPtSet.vecAddPts.size();

                int selectidx = FatherScroll->FeatPtDataSet->m_ImgPtSet.vecAddPts.size() - 1;
                FatherScroll->FeatManualSelectedIdx.append(selectidx);
                FatherScroll->imageLabel->update();
            }
            else
                //捕获到手动点
                if(FatherScroll->FeatManualSelectedIdx.size() == 1)
                {
                    //double rx,ry;
                    // FatherScroll->CanvasLocationToRasterLocation(event->x(),event->y(),rx,ry);
                    int idx = FatherScroll->FeatManualSelectedIdx.at(0);
                    ULONG lID = FatherScroll->FeatPtDataSet->m_ImgPtSet.vecPts.at(idx).lID;
                    FatherScroll->FeatPtDataSet->EditManualPtByIndxe(idx,rx,ry);
                    FatherScroll->imageLabel->update();
                }
        }
        else//绘制选择矩形
            if(FatherScroll->MouseLeftButtonDown)
            {
                DrawRect = true;
                FatherScroll->CanvasLocationToRasterLocation(FatherScroll->DownPoint.x,FatherScroll->DownPoint.y
                        ,rect.ltx,rect.lty);
//               rect.ltx = FatherScroll->DownPoint.x;
//               rect.lty = FatherScroll->DownPoint.y;
                if(FatherScroll->RasterRect.left < 0)
                {
                    rect.rbx = event->x() * FatherScroll->scaleFactor -0.5 ;
                }
                else
                {
                    rect.rbx = (event->x() - FatherScroll->RasterRect.left) * FatherScroll->scaleFactor  -0.5 ;
                }
                if(FatherScroll->RasterRect.top < 0)
                {
                    rect.rby = event->y() * FatherScroll->scaleFactor  -0.5;
                }
                else
                {
                    rect.rby = (event->y() - FatherScroll->RasterRect.top) * FatherScroll->scaleFactor  -0.5 ;
                }
                update();
            }
    }

}

void DrawLabel::DrawFeatPoints(QPainter* painter)
{
    double cx1,cy1;
    if(FatherScroll->FeatPtDataSet !=NULL)
    {
        QList<Pt2d> NoMatchPoint;
        pen.setColor(colorRed);
        pen.setWidth(1);
        painter->setPen(pen);
        //自动匹配点
        for(int i = 0; i< FatherScroll->FeatPtDataSet->m_ImgPtSet.vecPts.size(); i++)
        {
            if(FatherScroll->FeatPtDataSet->m_ImgPtSet.vecPts.at(i).byIsMatch == 0)
            {
                NoMatchPoint.append(FatherScroll->FeatPtDataSet->m_ImgPtSet.vecPts.at(i));
                continue;
            }
            FatherScroll->RasterLocationToCanvasLocation(FatherScroll->FeatPtDataSet->m_ImgPtSet.vecPts.at(i).X,
                    FatherScroll->FeatPtDataSet->m_ImgPtSet.vecPts.at(i).Y,
                    cx1,cy1);
            int X,Y;
            X = (int)(cx1+0.5);
            Y = (int)(cy1+0.5);
            painter->drawLine(X-5,Y, X+5,Y);
            painter->drawLine(X,Y-5,X,Y+5);
        }
        pen.setColor(colorYellow);
        pen.setWidth(1);
        painter->setPen(pen);
        //自动未匹配点
        for(int i=0; i < NoMatchPoint.count(); i++)
        {
            FatherScroll->RasterLocationToCanvasLocation(NoMatchPoint.at(i).X,
                    NoMatchPoint.at(i).Y,
                    cx1,cy1);
            int X,Y;
            X = (int)(cx1+0.5);
            Y = (int)(cy1+0.5);
            painter->drawLine(X-5,Y, X+5,Y);
            painter->drawLine(X,Y-5,X,Y+5);
        }
        NoMatchPoint.clear();
        pen.setColor(colorlightRed);
        painter->setPen(pen);
        //手动匹配点
        for(int i = 0; i< FatherScroll->FeatPtDataSet->m_ImgPtSet.vecAddPts.size(); i++)
        {
            if(FatherScroll->FeatPtDataSet->m_ImgPtSet.vecAddPts.at(i).byIsMatch == 0)
            {
                NoMatchPoint.append(FatherScroll->FeatPtDataSet->m_ImgPtSet.vecAddPts.at(i));
                continue;
            }
            FatherScroll->RasterLocationToCanvasLocation(FatherScroll->FeatPtDataSet->m_ImgPtSet.vecAddPts.at(i).X,
                    FatherScroll->FeatPtDataSet->m_ImgPtSet.vecAddPts.at(i).Y,
                    cx1,cy1);
            int X,Y;
            X = (int)(cx1+0.5);
            Y = (int)(cy1+0.5);
            painter->drawLine(X-5,Y, X+5,Y);
            painter->drawLine(X,Y-5,X,Y+5);
        }
        //手动未匹配点
        pen.setColor(colorlightYellow);
        painter->setPen(pen);
        for(int i=0; i < NoMatchPoint.count(); i++)
        {
            FatherScroll->RasterLocationToCanvasLocation(NoMatchPoint.at(i).X,
                    NoMatchPoint.at(i).Y,
                    cx1,cy1);
            int X,Y;
            X = (int)(cx1+0.5);
            Y = (int)(cy1+0.5);
            painter->drawLine(X-5,Y, X+5,Y);
            painter->drawLine(X,Y-5,X,Y+5);
        }
    }
}
void DrawLabel::DrawSelectedFeatPoints(QPainter* painter)
{
    double cx1,cy1;
    pen.setColor(colorGreen);
    pen.setWidth(2);
    painter->setPen(pen);
    for(int i = 0; i< FatherScroll->FeatAutoSelectedIdx.count(); i++)
    {
        int idx = FatherScroll->FeatAutoSelectedIdx.at(i);
        if(idx < 0)
        {
            continue;
        }
        if(idx > FatherScroll->FeatPtDataSet->m_ImgPtSet.vecPts.size())
        {
            continue;
        }
        FatherScroll->RasterLocationToCanvasLocation(FatherScroll->FeatPtDataSet->m_ImgPtSet.vecPts.at(idx).X,
                FatherScroll->FeatPtDataSet->m_ImgPtSet.vecPts.at(idx).Y,
                cx1,cy1);
        int X,Y;
        X = (int)(cx1+0.5);
        Y = (int)(cy1+0.5);
        painter->drawLine(X-5,Y, X+5,Y);
        painter->drawLine(X,Y-5,X,Y+5);
    }

    for(int i = 0; i< FatherScroll->FeatManualSelectedIdx.count(); i++)
    {
        int idx = FatherScroll->FeatManualSelectedIdx.at(i);
        if(idx < 0)
        {
            continue;
        }
        if(idx > FatherScroll->FeatPtDataSet->m_ImgPtSet.vecAddPts.size())
        {
            continue;
        }
        FatherScroll->RasterLocationToCanvasLocation(FatherScroll->FeatPtDataSet->m_ImgPtSet.vecAddPts.at(idx).X,
                FatherScroll->FeatPtDataSet->m_ImgPtSet.vecAddPts.at(idx).Y,
                cx1,cy1);
        int X,Y;
        X = (int)(cx1+0.5);
        Y = (int)(cy1+0.5);
        painter->drawLine(X-5,Y, X+5,Y);
        painter->drawLine(X,Y-5,X,Y+5);
    }
}
void DrawLabel::DrawSelectRect(QPainter* painter, MouseRect mrect)
{
    pen.setColor(colorGreen);
    pen.setWidth(1);
    painter->setPen(pen);
    double cx1,cy1;
    FatherScroll->RasterLocationToCanvasLocation(mrect.ltx,
            mrect.lty,
            cx1,cy1);
    int X,Y,X1,Y1;
    X = (int)(cx1+0.5);
    Y = (int)(cy1+0.5);
    FatherScroll->RasterLocationToCanvasLocation(mrect.rbx,
            mrect.rby,
            cx1,cy1);
    X1 = (int)(cx1+0.5);
    Y1 = (int)(cy1+0.5);
    painter->drawRect(X,Y,(X1-X) ,(Y1-Y));
    DrawRect = false;
}
