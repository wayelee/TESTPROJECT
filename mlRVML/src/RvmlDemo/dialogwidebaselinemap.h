#ifndef DIALOGWIDEBASELINEMAP_H
#define DIALOGWIDEBASELINEMAP_H

#include <QDialog>
#include<qfiledialog.h>

namespace Ui {
    class DialogWideBaselineMap;
}

class DialogWideBaselineMap : public QDialog
{
    Q_OBJECT

public:
    explicit DialogWideBaselineMap(QWidget *parent = 0);
    ~DialogWideBaselineMap();
    QString srcfilename;
    QString dstfilename;
    int nGridSize;
    int nTemplateSize;
    double dDEMResolution;
    int nColRadius;
    int nRowRadius;
    double dCoef;

private slots:
    void on_pushButtonSource_clicked();

    void on_pushButtonDest_clicked();

    void on_buttonBox_accepted();

private:
    Ui::DialogWideBaselineMap *ui;
};

#endif // DIALOGWIDEBASELINEMAP_H
