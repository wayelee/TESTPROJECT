#include "dialoglandlocatematch.h"
#include "ui_dialoglandlocatematch.h"

DialogLandLocateMatch::DialogLandLocateMatch(QWidget *parent) :
    QDialog(parent),
    ui(new Ui::DialogLandLocateMatch)
{
    ui->setupUi(this);
}

DialogLandLocateMatch::~DialogLandLocateMatch()
{
    delete ui;
}

void DialogLandLocateMatch::on_pushButtonLandDOMSource_clicked()
{
    QString fileName = QFileDialog::getOpenFileName(this,
                                    tr("Open File"), QDir::currentPath());
    ui->lineEditLandDOMSource->setText(fileName);
        if(fileName != "")
        {
            QFileInfo fileinfo(fileName);
            QDir::setCurrent(fileinfo.absolutePath());
        }
}

void DialogLandLocateMatch::on_pushButtonSatelliteDOMSource_clicked()
{
    QString fileName = QFileDialog::getOpenFileName(this,
                                    tr("Open File"), QDir::currentPath());
    ui->lineEditSatelliteDOMSource->setText(fileName);
        if(fileName != "")
        {
            QFileInfo fileinfo(fileName);
            QDir::setCurrent(fileinfo.absolutePath());
        }
}

void DialogLandLocateMatch::on_pushButtonDest_clicked()
{
    QString fileName = QFileDialog::getSaveFileName(this,
                                    tr("save File"), QDir::currentPath());

    QString fname = fileName.append(QString(".lrf"));
    ui->lineEditDest->setText(fname);
}

void DialogLandLocateMatch::on_buttonBox_accepted()
{
    LandDOMsrcfilename = ui->lineEditLandDOMSource->text();
    SatelliteDOMsrcfilename = ui->lineEditSatelliteDOMSource->text();
    dstfilename = ui->lineEditDest->text();
}
