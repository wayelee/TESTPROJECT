#include "dialogdisparitymap.h"
#include "ui_dialogdisparitymap.h"

DialogDisparityMap::DialogDisparityMap(QWidget *parent) :
    QDialog(parent),
    ui(new Ui::DialogDisparityMap)
{
    ui->setupUi(this);
}

DialogDisparityMap::~DialogDisparityMap()
{
    delete ui;
}

void DialogDisparityMap::on_pushButtonSourceLeft_clicked()
{
    QString fileName = QFileDialog::getOpenFileName(this,
                                    tr("Open File"), QDir::currentPath());
    ui->lineEditSourceLeft->setText(fileName);
        if(fileName != "")
        {
            QFileInfo fileinfo(fileName);
            QDir::setCurrent(fileinfo.absolutePath());
        }
}

void DialogDisparityMap::on_pushButtonSourceRight_clicked()
{
    QString fileName = QFileDialog::getOpenFileName(this,
                                    tr("Open File"), QDir::currentPath());
    ui->lineEditSourceRight->setText(fileName);
        if(fileName != "")
        {
            QFileInfo fileinfo(fileName);
            QDir::setCurrent(fileinfo.absolutePath());
        }
}

void DialogDisparityMap::on_pushButtonDest_clicked()
{
    QString fileName = QFileDialog::getSaveFileName(this,
                                    tr("save File"), QDir::currentPath(),QString("tif"));

    QString fname = fileName.append(QString(".tif"));
    ui->lineEditDest->setText(fname);
}

void DialogDisparityMap::on_buttonBox_accepted()
{
    Leftsrcfilename = ui->lineEditSourceLeft->text();
    Rightsrcfilename = ui->lineEditSourceRight->text();
    dstfilename = ui->lineEditDest->text();
}
