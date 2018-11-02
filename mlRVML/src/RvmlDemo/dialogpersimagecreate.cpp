#include "dialogpersimagecreate.h"
#include "ui_dialogpersimagecreate.h"

DialogPersImageCreate::DialogPersImageCreate(QWidget *parent) :
    QDialog(parent),
    ui(new Ui::DialogPersImageCreate)
{
    ui->setupUi(this);
    nLTX = 0;
    nLTY = 0;
    nWidth = 0;
    nHight = 0;
    dFocus = 0;
}

DialogPersImageCreate::~DialogPersImageCreate()
{
    delete ui;
}

void DialogPersImageCreate::on_pushButtonSource_clicked()
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

void DialogPersImageCreate::on_pushButtonDest_clicked()
{
    QString fileName = QFileDialog::getSaveFileName(this,
                                    tr("save File"), QDir::currentPath(),QString("tif"));
    QString fname = fileName.append(QString(".tif"));
    ui->lineEditDest->setText(fname);
}

void DialogPersImageCreate::on_buttonBox_accepted()
{
    nLTX = ui->spinBoxLeftX->value();
    nLTY = ui->spinBoxLeftX->value();
    nWidth = ui->spinBoxWidth->value();
    nHight = ui->spinBoxLenth->value();
    dFocus = ui->doubleSpinBoxFocus->value();
    srcfilename = ui->lineEditSource->text();
    dstfilename = ui->lineEditDest->text();
}
