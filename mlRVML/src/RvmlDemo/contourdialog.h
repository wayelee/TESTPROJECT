 #ifndef CONTOURDIALOG_H
#define CONTOURDIALOG_H

#include <QDialog>
#include<qfiledialog.h>

namespace Ui {
    class ContourDialog;
}

class ContourDialog : public QDialog
{
    Q_OBJECT

public:
    explicit ContourDialog(QWidget *parent = 0);
    ~ContourDialog();
    QString srcfilename;
    QString dstfilename;
    double interval;
private slots:
    void on_pushButtonSource_clicked();

    void on_pushButtonDest_clicked();

    void on_lineEditSource_textChanged(QString );

    void on_lineEditDest_textChanged(QString );

    void on_doubleSpinBoxInterval_valueChanged(double );

    void on_ContourDialog_accepted();

private:
    Ui::ContourDialog *ui;
};

#endif // CONTOURDIALOG_H
