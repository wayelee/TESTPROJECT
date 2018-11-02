#ifndef DIALOGWIDEBASEANALYSIS_H
#define DIALOGWIDEBASEANALYSIS_H

#include <QDialog>
#include<qfiledialog.h>
namespace Ui {
    class DialogWideBaseAnalysis;
}

class DialogWideBaseAnalysis : public QDialog
{
    Q_OBJECT

public:
    explicit DialogWideBaseAnalysis(QWidget *parent = 0);
    ~DialogWideBaseAnalysis();
    QString srcfilename;
    QString dstfilename;

private slots:
    void on_pushButtonSource_clicked();

    void on_pushButtonDest_clicked();

    void on_buttonBox_accepted();

private:
    Ui::DialogWideBaseAnalysis *ui;
};

#endif // DIALOGWIDEBASEANALYSIS_H
