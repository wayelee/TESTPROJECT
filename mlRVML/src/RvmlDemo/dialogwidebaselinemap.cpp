#include "dialogwidebaselinemap.h"
#include "ui_dialogwidebaselinemap.h"

DialogWideBaselineMap::DialogWideBaselineMap(QWidget *parent) :
    QDialog(parent),
    ui(new Ui::DialogWideBaselineMap)
{
    ui->setupUi(this);
    nColRadius = 0;
    nRowRadius = 0;
    nTemplateSize = 1;
    nGridSize = 1;
    dDEMResolution =0;
    dCoef = 0;
}

DialogWideBaselineMap::~DialogWideBaselineMap()
{
    delete ui;
}

void DialogWideBaselineMap::on_pushButtonSource_clicked()
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

void DialogWideBaselineMap::on_pushButtonDest_clicked()
{
    QString fileName = QFileDialog::getSaveFileName(this,
                                    tr("save File"), QDir::currentPath(),QString("tif"));
    QString fname = fileName.append(QString(".tif"));
    ui->lineEditDest->setText(fname);
}

void DialogWideBaselineMap::on_buttonBox_accepted()
{
    srcfilename = ui->lineEditSource->text();
    dstfilename = ui->lineEditDest->text();
    nColRadius = ui->doubleSpinBoxColRadius->value();
    nRowRadius = ui->doubleSpinBoxRowRadius->value();
    nGridSize = ui->doubleSpinBoxGridSize->value();
    nTemplateSize = ui->doubleSpinBoxTemplateSize->value();
    dCoef = ui->doubleSpinBoxCoef->value();
    dDEMResolution = ui->doubleSpinBoxDEMResolution->value();
}
