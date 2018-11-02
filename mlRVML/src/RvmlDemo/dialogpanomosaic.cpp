#include "dialogpanomosaic.h"
#include "ui_dialogpanomosaic.h"

DialogPanoMosaic::DialogPanoMosaic(QWidget *parent) :
    QDialog(parent),
    ui(new Ui::DialogPanoMosaic)
{
    ui->setupUi(this);
    MatchPara = 0.35;
}

DialogPanoMosaic::~DialogPanoMosaic()
{
    delete ui;
}

void DialogPanoMosaic::on_pushButtonSource_clicked()
{
    QString fileName = QFileDialog::getOpenFileName(this,
                                    tr("Open File"), QDir::currentPath());
    ui->lineEditSource->setText(fileName);
}

void DialogPanoMosaic::on_pushButtonDest_clicked()
{
    QString fileName = QFileDialog::getSaveFileName(this,
                                    tr("save File"), QDir::currentPath(),QString("tif"));
    QString fname = fileName.append(QString(".tif"));
    ui->lineEditDest->setText(fname);
}

void DialogPanoMosaic::on_buttonBox_accepted()
{
    srcfilename = ui->lineEditSource->text();
    dstfilename = ui->lineEditDest->text();
    MatchPara = ui->doubleSpinBox->value();
}
