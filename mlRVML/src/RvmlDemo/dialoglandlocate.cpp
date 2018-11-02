#include "dialoglandlocate.h"
#include "ui_dialoglandlocate.h"
#include <QtCore/QTextCodec>

DialogLandLocate::DialogLandLocate(QWidget *parent) :
    QDialog(parent),
    ui(new Ui::DialogLandLocate)
{
    ui->setupUi(this);
}

DialogLandLocate::~DialogLandLocate()
{
    delete ui;
}

void DialogLandLocate::on_pushButtonProjSource_clicked()
{
    QString fileName = QFileDialog::getOpenFileName(this,
                                    tr("Open File"), QDir::currentPath());
    ui->lineEditProjSource->setText(fileName);
        if(fileName != "")
        {
            QFileInfo fileinfo(fileName);
            QDir::setCurrent(fileinfo.absolutePath());
        }
}

void DialogLandLocate::on_pushButtonDOMSource_clicked()
{
    QString fileName = QFileDialog::getOpenFileName(this,
                                    tr("Open File"), QDir::currentPath());
    ui->lineEditDOMSource->setText(fileName);
        if(fileName != "")
        {
            QFileInfo fileinfo(fileName);
            QDir::setCurrent(fileinfo.absolutePath());
        }
}

void DialogLandLocate::on_pushButtonDest_clicked()
{
    QString fileName = QFileDialog::getSaveFileName(this,
                                    tr("save File"), QDir::currentPath());

    QString fname = fileName.append(QString(".lrf"));
    ui->lineEditDest->setText(fname);
}

void DialogLandLocate::on_buttonBox_accepted()
{
    Projsrcfilename = ui->lineEditProjSource->text();
    DOMsrcfilename = ui->lineEditDOMSource->text();
    dstfilename = ui->lineEditDest->text();
}
void DialogLandLocate::ChangeToCameraDlg()
{
    QTextCodec::setCodecForLocale(QTextCodec::codecForName("GB2312"));
    QTextCodec *codec = QTextCodec::codecForLocale();
    QString a = codec->toUnicode("特征点文件");
    ui->label->setText(a);
    a = codec->toUnicode("初始内方位文件");
    ui->label_3->setText(a);
    a = codec->toUnicode("单目量测结果");
    ui->label_2->setText(a);
}
