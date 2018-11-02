#include "dialogdemmosaic.h"
#include "ui_dialogdemmosaic.h"

DialogDEMMosaic::DialogDEMMosaic(QWidget *parent) :
    QDialog(parent),
    ui(new Ui::DialogDEMMosaic)
{
    ui->setupUi(this);
    dXResolution = 0;
    dYResolution = 0;
}

DialogDEMMosaic::~DialogDEMMosaic()
{
    delete ui;
}

void DialogDEMMosaic::on_pushButtonSource_clicked()
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

void DialogDEMMosaic::on_pushButtonDest_clicked()
{
    QString fileName = QFileDialog::getSaveFileName(this,
                                    tr("save File"), QDir::currentPath(),QString("tif"));
    QString fname = fileName.append(QString(".tif"));
    ui->lineEditDest->setText(fname);
}

void DialogDEMMosaic::on_buttonBox_accepted()
{
    srcfilename = ui->lineEditSource->text();
    dstfilename = ui->lineEditDest->text();
    dXResolution = ui->doubleSpinBoxXResolution->value();
    dYResolution = ui->doubleSpinBoxYResolution->value();
}
