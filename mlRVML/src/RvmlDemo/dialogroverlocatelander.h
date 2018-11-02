#ifndef DIALOGROVERLOCATELANDER_H
#define DIALOGROVERLOCATELANDER_H

#include <QDialog>
#include"qfiledialog.h"

namespace Ui {
    class DialogRoverLocateLander;
}

class DialogRoverLocateLander : public QDialog
{
    Q_OBJECT

public:
    explicit DialogRoverLocateLander(QWidget *parent = 0);
    ~DialogRoverLocateLander();

    QString Projsrcfilename;
    QString Coordsrcfilename;
    QString dstfilename;
private slots:
    void on_pushButtonProjSource_clicked();

    void on_pushButtonCoordSource_clicked();

    void on_pushButtonDest_clicked();

    void on_buttonBox_accepted();

private:
    Ui::DialogRoverLocateLander *ui;
};

#endif // DIALOGROVERLOCATELANDER_H
