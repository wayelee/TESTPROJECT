#include "dialogsitedemmosaic.h"
#include "ui_dialogsitedemmosaic.h"

DialogSiteDEMMosaic::DialogSiteDEMMosaic(QWidget *parent) :
    QDialog(parent),
    ui(new Ui::DialogSiteDEMMosaic)
{
    ui->setupUi(this);
    dMapRange = 0;
    dResolution = 0;
}

DialogSiteDEMMosaic::~DialogSiteDEMMosaic()
{
    delete ui;
}

void DialogSiteDEMMosaic::on_pushButtonSource_clicked()
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

void DialogSiteDEMMosaic::on_pushButtonDestDEM_clicked()
{
    QString fileName = QFileDialog::getSaveFileName(this,
                                    tr("save File"), QDir::currentPath(),QString("tif"));
    QString fname = fileName.append(QString(".tif"));
    ui->lineEditDestDEM->setText(fname);
}

void DialogSiteDEMMosaic::on_pushButtonDestDOM_clicked()
{
    QString fileName = QFileDialog::getSaveFileName(this,
                                    tr("save File"), QDir::currentPath(),QString("tif"));
    QString fname = fileName.append(QString(".tif"));
    ui->lineEditDestDOM->setText(fname);
}

void DialogSiteDEMMosaic::on_buttonBox_accepted()
{
    dMapRange = ui->doubleSpinBoxMapRange->value();
    dResolution = ui->doubleSpinBoxResolution->value();
    dstDEMfilename = ui->lineEditDestDEM->text();
    dstDOMfilename = ui->lineEditDestDOM->text();
    srcfilename = ui->lineEditSource->text();
}
