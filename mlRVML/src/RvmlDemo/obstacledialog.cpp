#include "obstacledialog.h"
#include "ui_obstacledialog.h"

ObstacleDialog::ObstacleDialog(QWidget *parent) :
    QDialog(parent),
    ui(new Ui::ObstacleDialog)
{
    ui->setupUi(this);
    nWindowSize = 3;
    zfactor = 1;

    dMaxSlope = 30;
    dMaxRoughness = 30;
    dMaxStep = 30;
    dMaxObstacleValue = 100000;
    dSlopeCoef = 100;
    dRoughnessCoef = 100;
    dStepCoef = 100;

}

ObstacleDialog::~ObstacleDialog()
{
    delete ui;
}

void ObstacleDialog::on_pushButtonSource_clicked()
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

void ObstacleDialog::on_pushButtonDest_clicked()
{
    QString fileName = QFileDialog::getSaveFileName(this,
                                    tr("save File"), QDir::currentPath(),QString("tif"));
    QString fname = fileName.append(QString(".tif"));
    ui->lineEditDest->setText(fname);
}

void ObstacleDialog::on_lineEditSource_textChanged(QString )
{
    srcfilename = ui->lineEditSource->text();

}

void ObstacleDialog::on_lineEditDest_textChanged(QString )
{
    dstfilename = ui->lineEditDest->text();
}

void ObstacleDialog::on_doubleSpinBoxWindowSize_valueChanged(double e)
{
    nWindowSize = e;
}

void ObstacleDialog::on_doubleSpinBoxZfactor_valueChanged(double e)
{
    zfactor = e;
}

void ObstacleDialog::on_ObstacleDialog_accepted()
{


}

void ObstacleDialog::on_doubleSpinBoxSlope_valueChanged(double )
{

}

void ObstacleDialog::on_doubleSpinBoxSlopeCoef_valueChanged(double )
{

}

void ObstacleDialog::on_doubleSpinBoxRoughness_valueChanged(double )
{

}

void ObstacleDialog::on_doubleSpinBoxRoughnessCoef_valueChanged(double )
{

}

void ObstacleDialog::on_doubleSpinBoxStep_valueChanged(double )
{

}

void ObstacleDialog::on_doubleSpinBoxStepCoef_valueChanged(double )
{

}

void ObstacleDialog::on_doubleSpinBoxObstacleValue_valueChanged(double )
{

}

void ObstacleDialog::on_buttonBox_accepted()
{
    srcfilename = ui->lineEditSource->text();
    dstfilename = ui->lineEditDest->text();
    nWindowSize = ui->doubleSpinBoxWindowSize->value();
    zfactor = ui->doubleSpinBoxZfactor->value();

    dMaxSlope = ui->doubleSpinBoxSlope->value();
    dMaxRoughness = ui->doubleSpinBoxRoughness->value();
    dMaxStep = ui->doubleSpinBoxStep->value();
    dSlopeCoef = ui->doubleSpinBoxSlopeCoef->value();
    dRoughnessCoef = ui->doubleSpinBoxRoughnessCoef->value();
    dStepCoef = ui->doubleSpinBoxStepCoef->value();
    dMaxObstacleValue = ui->doubleSpinBoxObstacleValue->value();
}
