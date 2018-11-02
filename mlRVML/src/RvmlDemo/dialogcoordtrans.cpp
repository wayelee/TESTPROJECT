#include "dialogcoordtrans.h"
#include "ui_dialogcoordtrans.h"

Dialogcoordtrans::Dialogcoordtrans(QWidget *parent) :
    QDialog(parent),
    ui(new Ui::Dialogcoordtrans)
{
    ui->setupUi(this);
    BasedOnLatLon = true;
}

Dialogcoordtrans::~Dialogcoordtrans()
{
    delete ui;
}

void Dialogcoordtrans::on_pushButtonSource_clicked()
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

void Dialogcoordtrans::on_pushButtonDest_clicked()
{
    QString fileName = QFileDialog::getSaveFileName(this,
                                    tr("save File"), QDir::currentPath());

    QString fname = fileName.append(QString(""));
    ui->lineEditDest->setText(fname);
}

void Dialogcoordtrans::on_buttonBox_accepted()
{
    srcfilename = ui->lineEditSource->text();
    dstfilename = ui->lineEditDest->text();
    BasedOnLatLon = ui->radioButton_BasedOnLatLon->isChecked();

}
