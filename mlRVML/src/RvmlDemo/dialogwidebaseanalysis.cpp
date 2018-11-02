#include "dialogwidebaseanalysis.h"
#include "ui_dialogwidebaseanalysis.h"

DialogWideBaseAnalysis::DialogWideBaseAnalysis(QWidget *parent) :
    QDialog(parent),
    ui(new Ui::DialogWideBaseAnalysis)
{
    ui->setupUi(this);
}

DialogWideBaseAnalysis::~DialogWideBaseAnalysis()
{
    delete ui;
}

void DialogWideBaseAnalysis::on_pushButtonSource_clicked()
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

void DialogWideBaseAnalysis::on_pushButtonDest_clicked()
{
    QString fileName = QFileDialog::getSaveFileName(this,
                                    tr("save File"), QDir::currentPath(),QString("txt"));
    QString fname = fileName.append(QString(".txt"));
    ui->lineEditDest->setText(fname);
}

void DialogWideBaseAnalysis::on_buttonBox_accepted()
{
    srcfilename = ui->lineEditSource->text();
    dstfilename = ui->lineEditDest->text();
}
