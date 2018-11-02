#ifndef DIALOGCOORDTRANS_H
#define DIALOGCOORDTRANS_H

#include <QDialog>
#include<qfiledialog.h>


namespace Ui {
    class Dialogcoordtrans;
}

class Dialogcoordtrans : public QDialog
{
    Q_OBJECT

public:
    explicit Dialogcoordtrans(QWidget *parent = 0);
    ~Dialogcoordtrans();

    QString srcfilename;
    QString dstfilename;
    bool BasedOnLatLon;
private slots:
    void on_pushButtonSource_clicked();

    void on_pushButtonDest_clicked();

    void on_buttonBox_accepted();

private:
    Ui::Dialogcoordtrans *ui;
};

#endif // DIALOGCOORDTRANS_H
