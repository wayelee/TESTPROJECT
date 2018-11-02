#ifndef DIALOGLANDLOCATE_H
#define DIALOGLANDLOCATE_H

#include <QDialog>
#include<qfiledialog.h>

namespace Ui {
    class DialogLandLocate;
}

class DialogLandLocate : public QDialog
{
    Q_OBJECT

public:
    explicit DialogLandLocate(QWidget *parent = 0);
    ~DialogLandLocate();
    QString Projsrcfilename;
    QString DOMsrcfilename;
    QString dstfilename;
    void ChangeToCameraDlg();

private slots:
    void on_pushButtonProjSource_clicked();

    void on_pushButtonDOMSource_clicked();

    void on_pushButtonDest_clicked();

    void on_buttonBox_accepted();

private:
    Ui::DialogLandLocate *ui;
};

#endif // DIALOGLANDLOCATE_H
