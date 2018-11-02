#include "dialogroverlocate.h"
#include "ui_dialogroverlocate.h"

DialogRoverLocate::DialogRoverLocate(QWidget *parent) :
    QDialog(parent),
    ui(new Ui::DialogRoverLocate)
{
    ui->setupUi(this);
    nFrontSiteNum = 1;
    nBackSiteNum = 2;
}

DialogRoverLocate::~DialogRoverLocate()
{
    delete ui;
}

void DialogRoverLocate::on_pushButtonSource_clicked()
{
    QString fileName = QFileDialog::getOpenFileName(this,
                                    tr("Open File"), QDir::currentPath());
    ui->lineEditSource->setText(fileName);
        if(fileName != "")
        {
            QFileInfo fileinfo(fileName);
            QDir::setCurrent(fileinfo.absolutePath());
        }
}

void DialogRoverLocate::on_pushButtonDest_clicked()
{
    QString fileName = QFileDialog::getSaveFileName(this,
                                    tr("save File"), QDir::currentPath());

    QString fname = fileName.append(QString(".lrf"));
    ui->lineEditDest->setText(fname);
}

void DialogRoverLocate::on_buttonBox_accepted()
{
    srcfilename = ui->lineEditSource->text();
    dstfilename = ui->lineEditDest->text();
    nFrontSiteNum = ui->spinBoxFrontSiteNum->value();
    nBackSiteNum = ui->spinBoxBackSiteNum->value();
}
