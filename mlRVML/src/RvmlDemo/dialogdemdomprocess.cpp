#include "dialogdemdomprocess.h"
#include "ui_dialogdemdomprocess.h"

DialogDEMDOMProcess::DialogDEMDOMProcess(QWidget *parent) :
    QDialog(parent),
    ui(new Ui::DialogDEMDOMProcess)
{
    ui->setupUi(this);
    LTx = 0;
    LTy = 0;
    RBx =0;
    RBY =0;
    SampleCoef = 0;
    BandNum = 1;
    PixelBased = true;
}

DialogDEMDOMProcess::~DialogDEMDOMProcess()
{
    delete ui;
}

void DialogDEMDOMProcess::on_pushButtonSource_clicked()
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

void DialogDEMDOMProcess::on_pushButtonDest_clicked()
{
    QString fileName = QFileDialog::getSaveFileName(this,
                                    tr("save File"), QDir::currentPath());
    QString fname = fileName.append(QString(""));
    ui->lineEditDest->setText(fname);
}

void DialogDEMDOMProcess::on_buttonBox_accepted()
{
    srcfilename = ui->lineEditSource->text();
    dstfilename = ui->lineEditDest->text();
    LTx = ui->doubleSpinBoxLeftTopX->value();
    LTy = ui->doubleSpinBoxLeftTopY->value();
    RBx = ui->doubleSpinBoxRightBottomX->value();
    RBY = ui->doubleSpinBoxRightBottomY->value();
    SampleCoef = ui->doubleSpinBoxSampleCoef->value();
    BandNum = ui->spinBoxBandNum->value();
    PixelBased = ui->radioButtonPixelBased->isChecked();
}
