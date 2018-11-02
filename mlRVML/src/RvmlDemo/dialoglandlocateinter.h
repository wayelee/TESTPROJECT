#ifndef DIALOGLANDLOCATEINTER_H
#define DIALOGLANDLOCATEINTER_H

#include <QDialog>
#include"qfiledialog.h"

namespace Ui {
    class DialogLandLocateInter;
}

class DialogLandLocateInter : public QDialog
{
    Q_OBJECT

public:
    explicit DialogLandLocateInter(QWidget *parent = 0);
    ~DialogLandLocateInter();
    QString Projsrcfilename;
    QString Coordsrcfilename;
    QString dstfilename;


private slots:
    void on_pushButtonProjSource_clicked();

    void on_pushButtonCoordSource_clicked();

    void on_pushButtonDest_clicked();

    void on_buttonBox_accepted();

private:
    Ui::DialogLandLocateInter *ui;
};

#endif // DIALOGLANDLOCATEINTER_H
