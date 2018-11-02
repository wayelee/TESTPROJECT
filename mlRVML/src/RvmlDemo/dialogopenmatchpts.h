#ifndef DIALOGOPENMATCHPTS_H
#define DIALOGOPENMATCHPTS_H

#include <QDialog>
#include "qfiledialog.h"

namespace Ui {
    class DialogOpenMatchPts;
}

class DialogOpenMatchPts : public QDialog
{
    Q_OBJECT

public:
    explicit DialogOpenMatchPts(QWidget *parent = 0);
    ~DialogOpenMatchPts();
    QString srcfilenameLeft;
    QString srcfilenameRight;

    bool bAddfmf;
    bool bAdddmf;
    bool bAddtmf;

private slots:
    void on_pushButtonSource_clicked();

    void on_pushButtonSource_2_clicked();

    void on_buttonBox_accepted();

private:
    Ui::DialogOpenMatchPts *ui;
};

#endif // DIALOGOPENMATCHPTS_H
