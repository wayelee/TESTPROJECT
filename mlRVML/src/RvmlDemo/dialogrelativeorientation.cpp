#include "dialogrelativeorientation.h"
#include "ui_dialogrelativeorientation.h"
#include "QFileDialog"

DialogRelativeOrientation::DialogRelativeOrientation(QWidget *parent) :
    QDialog(parent),
    ui(new Ui::DialogRelativeOrientation)
{
    ui->setupUi(this);
}

DialogRelativeOrientation::~DialogRelativeOrientation()
{
    delete ui;
}

void DialogRelativeOrientation::on_pushButtonProjSource_clicked()
{
    QString fileName = QFileDialog::getOpenFileName(this,
                                    tr("Open File"), QDir::currentPath());
    ui->lineEditProjSource->setText(fileName);
        if(fileName != "")
        {
            QFileInfo fileinfo(fileName);
            QDir::setCurrent(fileinfo.absolutePath());
        }
}

void DialogRelativeOrientation::on_pushButtonDOMSource_clicked()
{
    QString fileName = QFileDialog::getOpenFileName(this,
                                    tr("Open File"), QDir::currentPath());
    ui->lineEditDOMSource->setText(fileName);
        if(fileName != "")
        {
            QFileInfo fileinfo(fileName);
            QDir::setCurrent(fileinfo.absolutePath());
        }
}

void DialogRelativeOrientation::on_pushButtonDest_clicked()
{
    QString fileName = QFileDialog::getSaveFileName(this,
                                    tr("save File"), QDir::currentPath());

    //QString fname = fileName.append(QString(".lrf"));
    QString fname = fileName;
    ui->lineEditDest->setText(fname);
}

void DialogRelativeOrientation::on_buttonBox_accepted()
{
    Leftsrcfilename = ui->lineEditProjSource->text();
    Rightsrcfilename = ui->lineEditDOMSource->text();
    dstfilename = ui->lineEditDest->text();
}
