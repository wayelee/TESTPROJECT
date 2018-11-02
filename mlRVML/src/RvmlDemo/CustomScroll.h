#ifndef CUSTOMSCROLL_H
#define CUSTOMSCROLL_H
#include <QScrollArea>
//#include"../../3rdParty/gdal1.8.1/gdal_header.h"
#include <QPrinter>
#include"DrawLabel.h"
#include "CmlRasterBand.h"
#include"CmlRasterBand.h"
#include"computecontourline.h"

#include"ImgPtDataSet.h"
#include"qaction.h"
class ImageViewer;
typedef struct tagRRECT
{
        double    left;
        double    top;
        double    right;
        double    bottom;
} _mlRECT;

typedef struct _tagPOINT
{
        double  x;
        double  y;
} _mlPOINT;
enum ToolType
{
    Tool_None,
    Tool_pan,
    //编辑点
    Tool_EditMatchPoint,
    //添加一个点
    Tool_AddFeatPoint,
    // 添加一对点
    Tool_AddStereFeatPoint,
    Tool_EditFeatPoint,
    Tool_Draw,
    Tool_ToolActSemiAutoAddFeatPoint,
    //显示区域平面向量
    Tool_ToolActShowRegionPlanVect
}  ;

class CustomScroll : public QScrollArea
{
    Q_OBJECT
public:
    CustomScroll( );
    QList<int>  FeatManualSelectedIdx;
    QList<int>  FeatAutoSelectedIdx;
    ImageViewer* MainFrame;
    CImgPtDataSet* FeatPtDataSet;
public:
    DrawLabel *imageLabel;

    ulong RasterWidth;// raster source width
    ulong RasterHeight;// raster source length
    _mlRECT RasterRect;// Raster source envlop rect
    // 画图时整个栅格数据左上角在画布中的位置
    _mlPOINT RasterTopLeftInCanvas;//
    // 画图时图片左上角在raster中的位置
    _mlPOINT ImageLeftTopInRaster;
    // 画图时图片左上角在画布中的位置
    _mlPOINT ImageLeftTopInCanvas;
    GDALDataset *poDataset; //  RasterDataset
    QVector<QRgb> ByteMapColorTable;  // 单字节图片颜色表

     double scaleFactor ;
     ToolType CurrentToolType;
     // 判断鼠标是左键是否按住的变量
     bool MouseDown ;
     bool MouseLeftButtonDown;
     // 判断按住鼠标左键时鼠标在画布中的位置
     _mlPOINT DownPoint;
     //是否捕捉到点
      bool PickedAPoint  ;

     bool ComputeImageSizeAndLocation(_mlRECT RasterSourceRect,
                                      double Scale,
                                      double &ImageWidth,
                                      double &ImageHeight ,
                                      _mlPOINT & LeftTopInRaster,
                                      _mlPOINT & LeftTopInCavas,
                                      _mlPOINT &RightBottomInRaster);

     bool ComputeRasterRect(_mlRECT &rasterRect,double Scale,_mlPOINT topleftincanvas);
     void LoadImage(char* fileName);

     bool CreatCopiedImage();
     void writeimage();
     // 计算出的坐标为在imagelabel中的坐标，与在scrollarea的viewport中的坐标不同
     void RasterLocationToCanvasLocation(double rx,double ry, double& cx,double &cy);
     // 用于计算的坐标为在在scrollarea的viewport中的坐标
     void CanvasLocationToRasterLocation(double cx,double cy, double& rx,double &ry);
     void ProduceContourLine();
     // 将指定栅格数据点居中
     void CenterRasterPoint(double pointx,double pointy);
     //缩放到指定的数据范围
     void ZoomToRect(_mlRECT rasterscoperect);

protected:
     void mousePressEvent(QMouseEvent *event);
     void wheelEvent(QWheelEvent *event) ;
     void mouseMoveEvent ( QMouseEvent * event ) ;
     void mouseReleaseEvent(QMouseEvent *event);
     void paintEvent ( QPaintEvent * event ) ;
     void resizeEvent ( QResizeEvent * event ) ;
     void keyPressEvent ( QKeyEvent * event );
     //void keyReleaseEvent ( QKeyEvent * event ) ;
     void DrawPicture();
     void scrollContentsBy(int dx,int dy);
     //鼠标右键弹出菜单事件
     // 此函数如果有函数体会屏蔽掉鼠标右键的mousepress事件
     void contextMenuEvent(QContextMenuEvent *event);


private:
     // 指向本身的指针
     CustomScroll* scrollArea;

     QImage *image;
     //将变形的 cimage矫正的image
     QImage timage;
     // 未变形的image
     QImage cimage;
// 判断srcrollbar的显示范围发生变化时由Drawpicture引起还是由用户点击滚动条引起，点击滚动条引起时需要重新drawpicture
     bool ScopeChangedByClickScrollBar;
     double MinMaxValue[2];
     //删除点的Action
     QAction * ActDeleteSelectedPts;

     // 在编辑点的工具中，判断是否用鼠标点中了点

 private slots:
     void DrawLabelMouseMoveSlot(int x, int y);
     void DeleteSelectedFeatPt();

signals:
     // 鼠标信号为鼠标相对Imagelable左上顶点的屏幕坐标,Rasterrect的top和left大于零时与之有差别
     void DrawLabelMouseMoveSignal(int x, int y);

     // 鼠标信号为相对scrollarea视窗左上角定点的屏幕坐标
     void MousePressSignal(QMouseEvent* mouseEvent);
     void MouseReleaseSignal(QMouseEvent* mouseEvent);


     // 删点信号
     void DelSelectedPts();

};

#endif // CUSTOMSCROLL_H

