#ifndef DIALOGDEMMOSAIC_H
#define DIALOGDEMMOSAIC_H

#include <QDialog>
#include"qfiledialog.h"

namespace Ui {
    class DialogDEMMosaic;
}

class DialogDEMMosaic : public QDialog
{
    Q_OBJECT

public:
    explicit DialogDEMMosaic(QWidget *parent = 0);
    ~DialogDEMMosaic();

    QString srcfilename;
    QString dstfilename;
    double dXResolution;
    double dYResolution;

private slots:
    void on_pushButtonSource_clicked();

    void on_pushButtonDest_clicked();

    void on_buttonBox_accepted();

private:
    Ui::DialogDEMMosaic *ui;
};

#endif // DIALOGDEMMOSAIC_H
