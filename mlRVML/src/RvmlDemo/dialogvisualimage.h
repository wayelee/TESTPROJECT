#ifndef DIALOGVISUALIMAGE_H
#define DIALOGVISUALIMAGE_H

#include <QDialog>
#include<qfiledialog.h>

namespace Ui {
    class DialogVisualImage;
}

class DialogVisualImage : public QDialog
{
    Q_OBJECT

public:
    explicit DialogVisualImage(QWidget *parent = 0);
    ~DialogVisualImage();
    QString srcParafilename;
    QString srcDEMfilename;
    QString srcDOMfilename;
    QString dstfilename;

private slots:
    void on_pushButtonPara_clicked();

    void on_pushButtonDEMSource_clicked();

    void on_pushButtonDOMSource_clicked();

    void on_pushButtonDest_clicked();

    void on_buttonBox_accepted();

private:
    Ui::DialogVisualImage *ui;
};

#endif // DIALOGVISUALIMAGE_H
