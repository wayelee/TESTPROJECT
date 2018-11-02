#include "dialogsavepttofile.h"
#include "ui_dialogsavepttofile.h"

DialogSavePtToFile::DialogSavePtToFile(QWidget *parent) :
    QDialog(parent),
    ui(new Ui::DialogSavePtToFile)
{
    ui->setupUi(this);
}

DialogSavePtToFile::~DialogSavePtToFile()
{
    delete ui;
}

void DialogSavePtToFile::on_buttonBox_accepted()
{
    dstfilenameLeft = ui->lineEditDestLeft->text();
    dstfilenameRight = ui->lineEditDestRight->text();
}

void DialogSavePtToFile::on_pushButtonDestLeft_clicked()
{
    QString fileName = QFileDialog::getSaveFileName(this,
                                    tr("save File"), QDir::currentPath());
    QString fname = fileName.append(QString(""));
    ui->lineEditDestLeft->setText(fname);
}

void DialogSavePtToFile::on_pushButtonDestRight_clicked()
{
    QString fileName = QFileDialog::getSaveFileName(this,
                                    tr("save File"), QDir::currentPath());
    QString fname = fileName.append(QString(""));
    ui->lineEditDestRight->setText(fname);
}
