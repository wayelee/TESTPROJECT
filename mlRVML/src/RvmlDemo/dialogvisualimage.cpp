#include "dialogvisualimage.h"
#include "ui_dialogvisualimage.h"

DialogVisualImage::DialogVisualImage(QWidget *parent) :
    QDialog(parent),
    ui(new Ui::DialogVisualImage)
{
    ui->setupUi(this);
}

DialogVisualImage::~DialogVisualImage()
{
    delete ui;
}

void DialogVisualImage::on_pushButtonPara_clicked()
{
    QString fileName = QFileDialog::getOpenFileName(this,
                                    tr("Open File"), QDir::currentPath());
    ui->lineEditPara->setText(fileName);
        if(fileName != "")
        {
            QFileInfo fileinfo(fileName);
            QDir::setCurrent(fileinfo.absolutePath());
        }
}

void DialogVisualImage::on_pushButtonDEMSource_clicked()
{
    QString fileName = QFileDialog::getOpenFileName(this,
                                    tr("Open File"), QDir::currentPath());
    ui->lineEditDEMSource->setText(fileName);
        if(fileName != "")
        {
            QFileInfo fileinfo(fileName);
            QDir::setCurrent(fileinfo.absolutePath());
        }
}

void DialogVisualImage::on_pushButtonDOMSource_clicked()
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

void DialogVisualImage::on_pushButtonDest_clicked()
{
    QString fileName = QFileDialog::getSaveFileName(this,
                                    tr("save File"), QDir::currentPath());
    QString fname = fileName.append(QString(""));
    ui->lineEditDest->setText(fname);
}

void DialogVisualImage::on_buttonBox_accepted()
{
    srcParafilename = ui->lineEditPara->text();
    srcDEMfilename = ui->lineEditDEMSource->text();
    srcDOMfilename = ui->lineEditDOMSource->text();
    dstfilename = ui->lineEditDest->text();
}
