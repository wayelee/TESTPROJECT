#ifndef DIALOGPERSIMAGECREATE_H
#define DIALOGPERSIMAGECREATE_H

#include <QDialog>
#include<qfiledialog.h>

namespace Ui {
    class DialogPersImageCreate;
}

class DialogPersImageCreate : public QDialog
{
    Q_OBJECT

public:
    explicit DialogPersImageCreate(QWidget *parent = 0);
    ~DialogPersImageCreate();
    QString srcfilename;
    QString dstfilename;
    int nLTX;
    int nLTY;
    int nWidth;
    int nHight;
    double dFocus;

private slots:
    void on_pushButtonSource_clicked();

    void on_pushButtonDest_clicked();

    void on_buttonBox_accepted();

private:
    Ui::DialogPersImageCreate *ui;
};

#endif // DIALOGPERSIMAGECREATE_H
