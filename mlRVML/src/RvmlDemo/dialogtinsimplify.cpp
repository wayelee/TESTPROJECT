#include "dialogtinsimplify.h"
#include "ui_dialogtinsimplify.h"

DialogTinSimplify::DialogTinSimplify(QWidget *parent) :
    QDialog(parent),
    ui(new Ui::DialogTinSimplify)
{
    ui->setupUi(this);
}

DialogTinSimplify::~DialogTinSimplify()
{
    delete ui;
    SimplifyCoef = 0;
}

void DialogTinSimplify::on_pushButtonSource_clicked()
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

void DialogTinSimplify::on_pushButtonDest_clicked()
{
    QString fileName = QFileDialog::getSaveFileName(this,
                                    tr("save File"), QDir::currentPath());
    QString fname = fileName.append(QString(""));
    ui->lineEditDest->setText(fname);
}

void DialogTinSimplify::on_buttonBox_accepted()
{
    srcfilename = ui->lineEditSource->text();
    dstfilename = ui->lineEditDest->text();
    SimplifyCoef = ui->doubleSpinBoxSimplifyCoef->value();
}
