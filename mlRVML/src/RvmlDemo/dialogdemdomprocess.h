#ifndef DIALOGDEMDOMPROCESS_H
#define DIALOGDEMDOMPROCESS_H

#include <QDialog>
#include<qfiledialog.h>

namespace Ui {
    class DialogDEMDOMProcess;
}

class DialogDEMDOMProcess : public QDialog
{
    Q_OBJECT

public:
    explicit DialogDEMDOMProcess(QWidget *parent = 0);
    ~DialogDEMDOMProcess();
    QString srcfilename;
    QString dstfilename;
    double LTx;
    double LTy;
    double RBx;
    double RBY;
    double SampleCoef;
    int BandNum;
    bool PixelBased;


private slots:
    void on_pushButtonSource_clicked();

    void on_pushButtonDest_clicked();

    void on_buttonBox_accepted();

private:
    Ui::DialogDEMDOMProcess *ui;
};

#endif // DIALOGDEMDOMPROCESS_H
