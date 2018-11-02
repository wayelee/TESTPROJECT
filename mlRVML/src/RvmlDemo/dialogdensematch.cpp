#include "dialogdensematch.h"
#include "ui_dialogdensematch.h"

DialogDenseMatch::DialogDenseMatch(QWidget *parent) :
    QDialog(parent),
    ui(new Ui::DialogDenseMatch)
{
    index = 1;
    bGlobalMatch = true;
    ui->setupUi(this);
    ui->pushButtonDest->hide();
    ui->lineEditDest->hide();
    ui->label_2->hide();
}

DialogDenseMatch::~DialogDenseMatch()
{
    delete ui;
}

void DialogDenseMatch::on_pushButtonSource_clicked()
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

void DialogDenseMatch::on_pushButtonDest_clicked()
{
    QString fileName = QFileDialog::getSaveFileName(this,
                                    tr("save File"), QDir::currentPath(),QString("tif"));

    QString fname = fileName.append(QString(".tif"));
    ui->lineEditDest->setText(fname);
}

void DialogDenseMatch::on_buttonBox_accepted()
{
    index = ui->spinBox->value();
    srcParafilename = ui->lineEditParaSource->text();
    srcfilename = ui->lineEditSource->text();
    dstfilename = ui->lineEditDest->text();
    bGlobalMatch = ui->radioButtonGlobal->isChecked();
}

void DialogDenseMatch::on_pushButtonParaSource_clicked()
{
    QString fileName = QFileDialog::getOpenFileName(this,
                                    tr("Open File"), QDir::currentPath());
    ui->lineEditParaSource->setText(fileName);
        if(fileName != "")
        {
            QFileInfo fileinfo(fileName);
            QDir::setCurrent(fileinfo.absolutePath());
        }
}
void DialogDenseMatch::on_pushButton_clicked()
{

}
