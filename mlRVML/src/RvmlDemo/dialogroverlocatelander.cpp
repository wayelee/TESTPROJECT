#include "dialogroverlocatelander.h"
#include "ui_dialogroverlocatelander.h"

DialogRoverLocateLander::DialogRoverLocateLander(QWidget *parent) :
    QDialog(parent),
    ui(new Ui::DialogRoverLocateLander)
{
    ui->setupUi(this);
}

DialogRoverLocateLander::~DialogRoverLocateLander()
{
    delete ui;
}

void DialogRoverLocateLander::on_pushButtonProjSource_clicked()
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

void DialogRoverLocateLander::on_pushButtonCoordSource_clicked()
{
    QString fileName = QFileDialog::getOpenFileName(this,
                                    tr("Open File"), QDir::currentPath());
    ui->lineEditCoordSource->setText(fileName);
        if(fileName != "")
        {
            QFileInfo fileinfo(fileName);
            QDir::setCurrent(fileinfo.absolutePath());
        }
}

void DialogRoverLocateLander::on_pushButtonDest_clicked()
{
    QString fileName = QFileDialog::getSaveFileName(this,
                                    tr("save File"), QDir::currentPath());

    QString fname = fileName.append(QString(""));
    ui->lineEditDest->setText(fname);
}

void DialogRoverLocateLander::on_buttonBox_accepted()
{
    Projsrcfilename = ui->lineEditProjSource->text();
    Coordsrcfilename = ui->lineEditCoordSource->text();
    dstfilename = ui->lineEditDest->text();
}
