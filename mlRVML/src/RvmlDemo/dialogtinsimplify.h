#ifndef DIALOGTINSIMPLIFY_H
#define DIALOGTINSIMPLIFY_H

#include <QDialog>
#include<qfiledialog.h>
namespace Ui {
    class DialogTinSimplify;
}

class DialogTinSimplify : public QDialog
{
    Q_OBJECT

public:
    explicit DialogTinSimplify(QWidget *parent = 0);
    ~DialogTinSimplify();
    QString srcfilename;
    QString dstfilename;
    double SimplifyCoef;

private slots:
    void on_pushButtonSource_clicked();

    void on_pushButtonDest_clicked();

    void on_buttonBox_accepted();

private:
    Ui::DialogTinSimplify *ui;
};

#endif // DIALOGTINSIMPLIFY_H
