#include "slopedialog.h"
#include "ui_slopedialog.h"

SlopeDialog::SlopeDialog(QWidget *parent) :
    QDialog(parent),
    ui(new Ui::SlopeDialog)
{
    ui->setupUi(this);
    nWindowSize = 3;
    zfactor = 1;
}

SlopeDialog::~SlopeDialog()
{
    delete ui;
}

void SlopeDialog::on_pushButtonSource_clicked()
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

void SlopeDialog::on_pushButtonDest_clicked()
{
    QString fileName = QFileDialog::getSaveFileName(this,
                                    tr("save File"), QDir::currentPath(),QString("tif"));
    QString fname = fileName.append(QString(".tif"));
    ui->lineEditDest->setText(fname);
}

void SlopeDialog::on_lineEditSource_textChanged(QString )
{
    srcfilename = ui->lineEditSource->text();

}

void SlopeDialog::on_lineEditDest_textChanged(QString )
{
    dstfilename = ui->lineEditDest->text();
}

void SlopeDialog::on_doubleSpinBoxWindowSize_valueChanged(double e)
{
    nWindowSize = e;
}

void SlopeDialog::on_doubleSpinBoxZfactor_valueChanged(double e)
{
    zfactor = e;
}

void SlopeDialog::on_SlopeDialog_accepted()
{
   // srcfilename = ui->lineEditSource->text();
    //dstfilename = ui->lineEditDest->text();
   // nWindowSize = ui->doubleSpinBoxWindowSize->value();
   // zfactor = ui->doubleSpinBoxZfactor->value();
}
