#ifndef DIALOGDENSEMATCH_H
#define DIALOGDENSEMATCH_H

#include <QDialog>
#include<qfiledialog.h>

namespace Ui {
    class DialogDenseMatch;
}

class DialogDenseMatch : public QDialog
{
    Q_OBJECT

public:
    explicit DialogDenseMatch(QWidget *parent = 0);
    ~DialogDenseMatch();
    int index;
    QString srcParafilename;
    QString srcfilename;
    QString dstfilename;
    bool bGlobalMatch;
private slots:
    void on_pushButtonSource_clicked();

    void on_pushButtonDest_clicked();

    void on_buttonBox_accepted();

    void on_pushButton_clicked();

    void on_pushButtonParaSource_clicked();

private:
    Ui::DialogDenseMatch *ui;
};

#endif // DIALOGDENSEMATCH_H
