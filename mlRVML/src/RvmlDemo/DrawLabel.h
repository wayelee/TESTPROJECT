#ifndef DRAWLABEL_H
#define DRAWLABEL_H
#include <QLabel>
#include <QPrinter>
#include <QPen>
#include "computecontourline.h"

class CustomScroll;

class DrawLabel : public QLabel
{
    Q_OBJECT
public:
    DrawLabel( );
public:
    QImage* image;
    QPoint DrawPoint;
    vector<ContourLine*> * Lineset;
    CustomScroll* FatherScroll ;
    QColor colorRed/*(255,0,0)*/;
    QColor colorGreen/*(0,255,0)*/;
    QColor colorlightRed;
    QColor colorYellow;
    QColor colorlightYellow;
    QPen pen;
    //控制是否需要绘制矩形
    bool DrawRect;

    struct MouseRect
    {
        double ltx;
        double lty;
        double rbx;
        double rby;
    } rect;

protected:
    void paintEvent(QPaintEvent *event);
    void keyPressEvent ( QKeyEvent * event );
    void mouseMoveEvent(QMouseEvent * event);
    void DrawFeatPoints(QPainter* painter);
    void DrawSelectedFeatPoints(QPainter* painter);
    // 画选择点的时候的矩形框
    void DrawSelectRect(QPainter* painter,MouseRect mrect);


signals:
    // 鼠标信号中的x，y为相对 drawpoint的差
    void mouseMoveSignal(int x, int y);



};

#endif // DRAWLABEL_H
