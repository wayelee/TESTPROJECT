#ifndef DIALOGSITEDEMMOSAIC_H
#define DIALOGSITEDEMMOSAIC_H

#include <QDialog>
#include<qfiledialog.h>

namespace Ui {
    class DialogSiteDEMMosaic;
}

class DialogSiteDEMMosaic : public QDialog
{
    Q_OBJECT

public:
    explicit DialogSiteDEMMosaic(QWidget *parent = 0);
    ~DialogSiteDEMMosaic();
    QString dstDEMfilename;
    QString dstDOMfilename;
    QString srcfilename;
    double dMapRange;
    double dResolution;

private slots:
    void on_pushButtonSource_clicked();

    void on_pushButtonDestDEM_clicked();

    void on_pushButtonDestDOM_clicked();

    void on_buttonBox_accepted();

private:
    Ui::DialogSiteDEMMosaic *ui;
};

#endif // DIALOGSITEDEMMOSAIC_H
