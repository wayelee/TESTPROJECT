#ifndef DIALOGMULTSITEDEMMOSAIC_H
#define DIALOGMULTSITEDEMMOSAIC_H

#include <QDialog>
#include<qfiledialog.h>

namespace Ui {
    class DialogMultSiteDemMosaic;
}

class DialogMultSiteDemMosaic : public QDialog
{
    Q_OBJECT

public:
    explicit DialogMultSiteDemMosaic(QWidget *parent = 0);
    ~DialogMultSiteDemMosaic();
    double dDEMResolution;
    QString dstfilename;
    QStringList srcfilenames;


private slots:
    void on_pushButtonDest_clicked();

    void on_pushButtonDeleteFile_clicked();

    void on_pushButtonAddFile_clicked();

    void on_buttonBox_accepted();

private:
    Ui::DialogMultSiteDemMosaic *ui;
};

#endif // DIALOGMULTSITEDEMMOSAIC_H
