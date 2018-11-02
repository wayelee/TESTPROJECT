#include "dialogpointcoord.h"
#include "ui_dialogpointcoord.h"

DialogPointCoord::DialogPointCoord(QWidget *parent) :
    QDialog(parent),
    ui(new Ui::DialogPointCoord)
{
    ui->setupUi(this);
    X=0;
    Y=0;
    ui->pushButton_Cancel->hide();
    ui->pushButton_OK->hide();
}

DialogPointCoord::~DialogPointCoord()
{
    delete ui;
}

void DialogPointCoord::on_pushButton_OK_clicked()
{
    X = ui->doubleSpinBox_X->value();
    Y = ui->doubleSpinBox_Y->value();
}
void DialogPointCoord::SetXValue(double X)
{
    ui->doubleSpinBox_X->setValue(X);
}

void DialogPointCoord::SetYValue(double Y)
{
    ui->doubleSpinBox_Y->setValue(Y);
}
void DialogPointCoord::SetText(QString text)
{
    ui->label_Info->setText(text);
}
void DialogPointCoord::SetID(unsigned long long int lID)
{
    ui->label_ID->setText(tr("ID") + QString::number(lID));
}
