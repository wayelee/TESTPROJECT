#ifndef DIALOGEPIPOLARIMAGE_H
#define DIALOGEPIPOLARIMAGE_H

#include <QDialog>

namespace Ui {
    class DialogEpipolarImage;
}

class DialogEpipolarImage : public QDialog
{
    Q_OBJECT

public:
    explicit DialogEpipolarImage(QWidget *parent = 0);
    ~DialogEpipolarImage();
    QString Projsrcfilename;
    QString Parasrcfilename;
    QString dstfilename;
private slots:
    void on_pushButtonProjSource_clicked();

    void on_pushButtonDOMSource_clicked();

    void on_pushButtonDest_clicked();

    void on_buttonBox_accepted();

private:
    Ui::DialogEpipolarImage *ui;
};

#endif // DIALOGEPIPOLARIMAGE_H
