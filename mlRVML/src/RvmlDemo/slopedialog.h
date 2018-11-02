#ifndef SLOPEDIALOG_H
#define SLOPEDIALOG_H

#include <QDialog>
#include<qfiledialog.h>

namespace Ui {
    class SlopeDialog;
}

class SlopeDialog : public QDialog
{
    Q_OBJECT

public:
    explicit SlopeDialog(QWidget *parent = 0);
    ~SlopeDialog();
    QString srcfilename;
    QString dstfilename;
    int nWindowSize;
    double zfactor;

private slots:
    void on_pushButtonSource_clicked();

    void on_pushButtonDest_clicked();

    void on_lineEditSource_textChanged(QString );

    void on_lineEditDest_textChanged(QString );

    void on_doubleSpinBoxWindowSize_valueChanged(double );

    void on_doubleSpinBoxZfactor_valueChanged(double );

    void on_SlopeDialog_accepted();

private:
    Ui::SlopeDialog *ui;
};

#endif // SLOPEDIALOG_H
