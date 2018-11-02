#include "dialogepipolarimage.h"
#include "ui_dialogepipolarimage.h"
#include "QFileDialog"

DialogEpipolarImage::DialogEpipolarImage(QWidget *parent) :
    QDialog(parent),
    ui(new Ui::DialogEpipolarImage)
{
    ui->setupUi(this);
}

DialogEpipolarImage::~DialogEpipolarImage()
{
    delete ui;
}

void DialogEpipolarImage::on_pushButtonProjSource_clicked()
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

void DialogEpipolarImage::on_pushButtonDOMSource_clicked()
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

void DialogEpipolarImage::on_pushButtonDest_clicked()
{
    QString fileName = QFileDialog::getSaveFileName(this,
                                    tr("save File"), QDir::currentPath());

    //QString fname = fileName.append(QString(".lrf"));
    QString fname = fileName;
    ui->lineEditDest->setText(fname);
}

void DialogEpipolarImage::on_buttonBox_accepted()
{
    Projsrcfilename = ui->lineEditProjSource->text();
    Parasrcfilename = ui->lineEditDOMSource->text();
    dstfilename = ui->lineEditDest->text();
}
