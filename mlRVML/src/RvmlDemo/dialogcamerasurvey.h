#ifndef DIALOGCAMERASURVEY_H
#define DIALOGCAMERASURVEY_H

#include <QDialog>
#include<qfiledialog.h>

namespace Ui {
    class DialogCameraSurvey;
}

class DialogCameraSurvey : public QDialog
{
    Q_OBJECT

public:
    explicit DialogCameraSurvey(QWidget *parent = 0);
    ~DialogCameraSurvey();
    QString FeatPtfile;
    QString IntEle;
    QString dstfilename;

private slots:
    void on_pushButtonFeatPoint_clicked();

    void on_pushButtonIntEle_clicked();

    void on_pushButtonDest_clicked();

    void on_buttonBox_accepted();

private:
    Ui::DialogCameraSurvey *ui;
};

#endif // DIALOGCAMERASURVEY_H
