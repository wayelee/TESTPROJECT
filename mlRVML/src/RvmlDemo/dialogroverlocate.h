#ifndef DIALOGROVERLOCATE_H
#define DIALOGROVERLOCATE_H

#include <QDialog>
#include "qfiledialog.h"

namespace Ui {
    class DialogRoverLocate;
}

class DialogRoverLocate : public QDialog
{
    Q_OBJECT

public:
    explicit DialogRoverLocate(QWidget *parent = 0);
    ~DialogRoverLocate();
    QString srcfilename;
    QString dstfilename;
    int nFrontSiteNum;
    int nBackSiteNum;

private slots:
    void on_pushButtonSource_clicked();

    void on_pushButtonDest_clicked();

    void on_buttonBox_accepted();

private:
    Ui::DialogRoverLocate *ui;
};

#endif // DIALOGROVERLOCATE_H
