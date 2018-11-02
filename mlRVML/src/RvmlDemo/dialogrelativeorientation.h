#ifndef DIALOGRELATIVEORIENTATION_H
#define DIALOGRELATIVEORIENTATION_H

#include <QDialog>

namespace Ui {
    class DialogRelativeOrientation;
}

class DialogRelativeOrientation : public QDialog
{
    Q_OBJECT

public:
    explicit DialogRelativeOrientation(QWidget *parent = 0);
    ~DialogRelativeOrientation();
    QString Leftsrcfilename;
    QString Rightsrcfilename;
    QString dstfilename;

private slots:
    void on_pushButtonProjSource_clicked();

    void on_pushButtonDOMSource_clicked();

    void on_pushButtonDest_clicked();

    void on_buttonBox_accepted();

private:
    Ui::DialogRelativeOrientation *ui;
};

#endif // DIALOGRELATIVEORIENTATION_H
