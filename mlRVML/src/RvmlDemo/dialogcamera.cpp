#include "dialogcamera.h"
#include "ui_dialogcamera.h"
#include <QtCore/QTextCodec>

DialogCamera::DialogCamera(QWidget *parent) :
    QDialog(parent),
    ui(new Ui::DialogCamera)
{
    ui->setupUi(this);
    //ui->lineEditDest->hide();
    //ui->label_2->hide();
    //ui->pushButtonDest->hide();
}

DialogCamera::~DialogCamera()
{
    delete ui;
}

void DialogCamera::on_pushButtonSource_clicked()
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

void DialogCamera::on_pushButtonDest_clicked()
{
    QString fileName = QFileDialog::getSaveFileName(this,
                                    tr("save File"), QDir::currentPath(),QString("txt"));
    QString fname = fileName.append(QString(".txt"));
    ui->lineEditDest->setText(fname);
}

void DialogCamera::on_buttonBox_accepted()
{
    srcfilename = ui->lineEditSource->text();
    dstfilename = ui->lineEditDest->text();
    dstfilenameAccInfo = ui->lineEditAccReport->text();
}

void DialogCamera::on_pushButtonDestAccReport_clicked()
{
    QString fileName = QFileDialog::getSaveFileName(this,
                                    tr("save File"), QDir::currentPath(),QString("txt"));
    QString fname = fileName.append(QString(".txt"));
    ui->lineEditDest->setText(fname);
}

void DialogCamera::HideSomeItems()
{
    ui->lineEditDest->hide();
    ui->label_2->hide();
    ui->pushButtonDest->hide();
    ui->pushButtonDestAccReport->hide();
    ui->label_3->hide();
    ui->lineEditAccReport->hide();

   QTextCodec::setCodecForTr(QTextCodec::codecForName("GB2312"));
   QTextCodec::setCodecForCStrings(QTextCodec::codecForName("GB2312"));
   QTextCodec::setCodecForLocale(QTextCodec::codecForName("GB2312"));

   QTextCodec *codec = QTextCodec::codecForLocale();
   QString a = codec->toUnicode("特征点文件");
   ui->label->setText(a);

}


