#include "dialogorbitimagedem.h"
#include "ui_dialogorbitimagedem.h"

DialogOrbitImageDEM::DialogOrbitImageDEM(QWidget *parent) :
    QDialog(parent),
    ui(new Ui::DialogOrbitImageDEM)
{
    ui->setupUi(this);
    dResolution = 0;
    bBasedOnLeftImg = true;
    bUseMatchPoints = false;
    ui->lineEditSourcePointsFile->hide();
    ui->pushButtonSourcePointsFile->hide();
}

DialogOrbitImageDEM::~DialogOrbitImageDEM()
{
    delete ui;
}

void DialogOrbitImageDEM::on_pushButtonSource_clicked()
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

void DialogOrbitImageDEM::on_pushButtonDestDEM_clicked()
{
    QString fileName = QFileDialog::getSaveFileName(this,
                                    tr("save File"), QDir::currentPath(),QString("tif"));
    QString fname = fileName.append(QString(".tif"));
    ui->lineEditDestDEM->setText(fname);
}

void DialogOrbitImageDEM::on_pushButtonDestDOM_clicked()
{
    QString fileName = QFileDialog::getSaveFileName(this,
                                    tr("save File"), QDir::currentPath(),QString("tif"));
    QString fname = fileName.append(QString(".tif"));
    ui->lineEditDestDOM->setText(fname);
}

void DialogOrbitImageDEM::on_buttonBox_accepted()
{
    dResolution = ui->doubleSpinBoxResolution->value();
    dstDEMfilename = ui->lineEditDestDEM->text();
    dstDOMfilename = ui->lineEditDestDOM->text();
    srcfilename = ui->lineEditSource->text();
    bBasedOnLeftImg = ui->radioButtonLeft->isChecked();

    MatchPointsfilename = ui->lineEditSourcePointsFile->text();
    bUseMatchPoints = ui->radioButtonUsePointsFile->isChecked();
}

void DialogOrbitImageDEM::on_pushButtonSourcePointsFile_clicked()
{
    QString fileName = QFileDialog::getOpenFileName(this,
                                    tr("Open File"), QDir::currentPath());
    ui->lineEditSourcePointsFile->setText(fileName);
        if(fileName != "")
        {
            QFileInfo fileinfo(fileName);
            QDir::setCurrent(fileinfo.absolutePath());
        }
}

void DialogOrbitImageDEM::on_radioButtonUsePointsFile_toggled(bool checked)
{
    if(checked)
    {
       // ui->lineEditSourcePointsFile->show();
//        ui->pushButtonSourcePointsFile->show();
    }
    else
    {
         ui->lineEditSourcePointsFile->hide();
         ui->pushButtonSourcePointsFile->hide();
    }
}
