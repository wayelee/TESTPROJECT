#ifndef DIALOGDISPARITYMAP_H
#define DIALOGDISPARITYMAP_H

#include <QDialog>
#include"qfiledialog.h"

namespace Ui {
    class DialogDisparityMap;
}

class DialogDisparityMap : public QDialog
{
    Q_OBJECT

public:
    explicit DialogDisparityMap(QWidget *parent = 0);
    ~DialogDisparityMap();
    QString Leftsrcfilename;
    QString Rightsrcfilename;
    QString dstfilename;

private slots:
    void on_pushButtonSourceLeft_clicked();

    void on_pushButtonSourceRight_clicked();

    void on_pushButtonDest_clicked();

    void on_buttonBox_accepted();

private:
    Ui::DialogDisparityMap *ui;
};

#endif // DIALOGDISPARITYMAP_H
