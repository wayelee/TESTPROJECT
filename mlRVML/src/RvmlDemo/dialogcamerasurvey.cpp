#include "dialogcamerasurvey.h"
#include "ui_dialogcamerasurvey.h"

DialogCameraSurvey::DialogCameraSurvey(QWidget *parent) :
    QDialog(parent),
    ui(new Ui::DialogCameraSurvey)
{
    ui->setupUi(this);
}

DialogCameraSurvey::~DialogCameraSurvey()
{
    delete ui;
}

void DialogCameraSurvey::on_pushButtonFeatPoint_clicked()
{
    QString fileName = QFileDialog::getOpenFileName(this,
                                    tr("Open File"), QDir::currentPath());
    ui->lineEditFeatPt->setText(fileName);
        if(fileName != "")
        {
            QFileInfo fileinfo(fileName);
            QDir::setCurrent(fileinfo.absolutePath());
        }
}

void DialogCameraSurvey::on_pushButtonIntEle_clicked()
{
    QString fileName = QFileDialog::getOpenFileName(this,
                                    tr("Open File"), QDir::currentPath());
    ui->lineEditIntEle->setText(fileName);
        if(fileName != "")
        {
            QFileInfo fileinfo(fileName);
            QDir::setCurrent(fileinfo.absolutePath());
        }
}

void DialogCameraSurvey::on_pushButtonDest_clicked()
{
    QString fileName = QFileDialog::getSaveFileName(this,
                                    tr("save File"), QDir::currentPath());
    QString fname = fileName.append(QString(".txt"));
    ui->lineEditDest->setText(fname);
}

void DialogCameraSurvey::on_buttonBox_accepted()
{
    FeatPtfile = ui->lineEditFeatPt->text();
    dstfilename = ui->lineEditDest->text();
    IntEle = ui->lineEditIntEle->text();
}
