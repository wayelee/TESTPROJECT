#ifndef DIALOGPANOMOSAIC_H
#define DIALOGPANOMOSAIC_H

#include <QDialog>
#include<qfiledialog.h>

namespace Ui {
    class DialogPanoMosaic;
}

class DialogPanoMosaic : public QDialog
{
    Q_OBJECT

public:
    explicit DialogPanoMosaic(QWidget *parent = 0);
    ~DialogPanoMosaic();
    QString srcfilename;
    QString dstfilename;
    double MatchPara;

private slots:
    void on_pushButtonSource_clicked();

    void on_pushButtonDest_clicked();

    void on_buttonBox_accepted();

private:
    Ui::DialogPanoMosaic *ui;
};

#endif // DIALOGPANOMOSAIC_H
