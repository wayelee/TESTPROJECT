#include "dialoglandlocateinter.h"
#include "ui_dialoglandlocateinter.h"

DialogLandLocateInter::DialogLandLocateInter(QWidget *parent) :
    QDialog(parent),
    ui(new Ui::DialogLandLocateInter)
{
    ui->setupUi(this);
}

DialogLandLocateInter::~DialogLandLocateInter()
{
    delete ui;
}

void DialogLandLocateInter::on_pushButtonProjSource_clicked()
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

void DialogLandLocateInter::on_pushButtonCoordSource_clicked()
{
    QString fileName = QFileDialog::getOpenFileName(this,
                                    tr("Open File"), QDir::currentPath());
    ui->lineEditCoordSource->setText(fileName);
        if(fileName != "")
        {
            QFileInfo fileinfo(fileName);
            QDir::setCurrent(fileinfo.absolutePath());
        }
}

void DialogLandLocateInter::on_pushButtonDest_clicked()
{
    QString fileName = QFileDialog::getSaveFileName(this,
                                    tr("save File"), QDir::currentPath());

    QString fname = fileName.append(QString(".lrf"));
    ui->lineEditDest->setText(fname);
}

void DialogLandLocateInter::on_buttonBox_accepted()
{
    Projsrcfilename = ui->lineEditProjSource->text();
    Coordsrcfilename = ui->lineEditCoordSource->text();
    dstfilename = ui->lineEditDest->text();
}
