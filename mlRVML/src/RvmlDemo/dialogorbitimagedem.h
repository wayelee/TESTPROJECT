#ifndef DIALOGORBITIMAGEDEM_H
#define DIALOGORBITIMAGEDEM_H

#include <QDialog>
#include<qfiledialog.h>

namespace Ui {
    class DialogOrbitImageDEM;
}

class DialogOrbitImageDEM : public QDialog
{
    Q_OBJECT

public:
    explicit DialogOrbitImageDEM(QWidget *parent = 0);
    ~DialogOrbitImageDEM();

    QString dstDEMfilename;
    QString dstDOMfilename;
    QString srcfilename;
    QString MatchPointsfilename;
    bool bUseMatchPoints;
    double dResolution;
    bool bBasedOnLeftImg;

private slots:
    void on_pushButtonSource_clicked();

    void on_pushButtonDestDEM_clicked();

    void on_pushButtonDestDOM_clicked();

    void on_buttonBox_accepted();

    void on_pushButtonSourcePointsFile_clicked();

    void on_radioButtonUsePointsFile_toggled(bool checked);

private:
    Ui::DialogOrbitImageDEM *ui;
};

#endif // DIALOGORBITIMAGEDEM_H
