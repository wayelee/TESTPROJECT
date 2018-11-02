#ifndef VIEWSHEDDIALOG_H
#define VIEWSHEDDIALOG_H

#include <QDialog>
#include<qfiledialog.h>

namespace Ui {
    class ViewShedDialog;
}

class ViewShedDialog : public QDialog
{
    Q_OBJECT

public:
    explicit ViewShedDialog(QWidget *parent = 0);
    QString srcfilename;
    QString dstfilename;
    int nx;
    int ny;
    double dviewhight;
    bool InverseHeitht;
    ~ViewShedDialog();

private slots:
    void on_pushButtonSource_clicked();

    void on_pushButtonDest_clicked();

    void on_lineEditSource_textChanged(QString );

    void on_lineEditDest_textChanged(QString );

    void on_doubleSpinBoxY_valueChanged(double );

    void on_doubleSpinBoxZ_valueChanged(double );

    void on_doubleSpinBoxX_valueChanged(double );

    void on_ViewShedDialog_accepted();

    void on_buttonBox_accepted();

private:
    Ui::ViewShedDialog *ui;
};

#endif // VIEWSHEDDIALOG_H
