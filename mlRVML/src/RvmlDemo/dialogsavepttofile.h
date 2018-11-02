#ifndef DIALOGSAVEPTTOFILE_H
#define DIALOGSAVEPTTOFILE_H

#include <QDialog>
#include"qfiledialog.h"
namespace Ui {
    class DialogSavePtToFile;
}

class DialogSavePtToFile : public QDialog
{
    Q_OBJECT

public:
    explicit DialogSavePtToFile(QWidget *parent = 0);
    ~DialogSavePtToFile();
    QString  dstfilenameLeft;
    QString  dstfilenameRight;

private slots:
    void on_buttonBox_accepted();

    void on_pushButtonDestLeft_clicked();

    void on_pushButtonDestRight_clicked();

private:
    Ui::DialogSavePtToFile *ui;
};

#endif // DIALOGSAVEPTTOFILE_H
