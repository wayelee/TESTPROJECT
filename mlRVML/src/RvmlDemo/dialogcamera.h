#ifndef DIALOGCAMERA_H
#define DIALOGCAMERA_H

#include <QDialog>
#include<qfiledialog.h>

namespace Ui {
    class DialogCamera;
}

class DialogCamera : public QDialog
{
    Q_OBJECT

public:
    explicit DialogCamera(QWidget *parent = 0);
    ~DialogCamera();
    QString srcfilename;
    QString dstfilename;
    QString dstfilenameAccInfo;
    void HideSomeItems();

private slots:
    void on_pushButtonSource_clicked();

    void on_pushButtonDest_clicked();

    void on_buttonBox_accepted();

    void on_pushButtonDestAccReport_clicked();

private:
    Ui::DialogCamera *ui;
};

#endif // DIALOGCAMERA_H
