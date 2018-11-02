#include "viewsheddialog.h"
#include "ui_viewsheddialog.h"


ViewShedDialog::ViewShedDialog(QWidget *parent) :
    QDialog(parent),
    ui(new Ui::ViewShedDialog)
{
    ui->setupUi(this);
    nx = 0;
    ny =0;
    dviewhight =0;
    InverseHeitht = false;
}

ViewShedDialog::~ViewShedDialog()
{
    delete ui;
}

void ViewShedDialog::on_pushButtonSource_clicked()
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

void ViewShedDialog::on_pushButtonDest_clicked()
{
    QString fileName = QFileDialog::getSaveFileName(this,
                                    tr("save File"), QDir::currentPath(),QString("tif"));

    QString fname = fileName.append(QString(".tif"));
    ui->lineEditDest->setText(fname);
}

void ViewShedDialog::on_lineEditSource_textChanged(QString )
{
     srcfilename = ui->lineEditSource->text();
}

void ViewShedDialog::on_lineEditDest_textChanged(QString )
{
    dstfilename = ui->lineEditDest->text();
}

void ViewShedDialog::on_doubleSpinBoxX_valueChanged(double e)
{
    nx = e;
}

void ViewShedDialog::on_doubleSpinBoxY_valueChanged(double e)
{
    ny = e;
}

void ViewShedDialog::on_doubleSpinBoxZ_valueChanged(double e)
{
    dviewhight = e;
}

void ViewShedDialog::on_ViewShedDialog_accepted()
{

}
/*
void ViewShedDialog::on_pushButtonSource_clicked()
{
    QString fileName = QFileDialog::getOpenFileName(this,
                                    tr("Open File"), QDir::currentPath());
    ui->lineEdit->setText(fileName);
}
*/

void ViewShedDialog::on_buttonBox_accepted()
{
    srcfilename = ui->lineEditSource->text();
    dstfilename = ui->lineEditDest->text();
    nx = ui->doubleSpinBoxX->value();
    ny = ui->doubleSpinBoxY->value();
    dviewhight = ui->doubleSpinBoxZ->value();
    InverseHeitht = ui->checkBoxInverseHeight->checkState();
}
