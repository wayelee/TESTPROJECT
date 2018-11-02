#include "dialogopenmatchpts.h"
#include "ui_dialogopenmatchpts.h"

DialogOpenMatchPts::DialogOpenMatchPts(QWidget *parent) :
    QDialog(parent),
    ui(new Ui::DialogOpenMatchPts)
{
    ui->setupUi(this);
    bAddfmf = true;
    bAdddmf = false;
    bAddtmf = false;
}

DialogOpenMatchPts::~DialogOpenMatchPts()
{
    delete ui;
}

void DialogOpenMatchPts::on_pushButtonSource_clicked()
{
    QString fileName = QFileDialog::getOpenFileName(this,
                                    tr("Open File"), QDir::currentPath());
    ui->lineEditSource->setText(fileName);
        if(fileName != "")
        {
            QFileInfo fileinfo(fileName);
            QDir::setCurrent(fileinfo.absolutePath());
        }

        QFileInfo fileinfo (fileName);
        QString fname = fileinfo.fileName();
        QString newname = fname.replace("Left","Right");
        QString rfilename;
        if(newname != fname)
        {
            rfilename = fileinfo.absolutePath() + "/" + newname;
            ui->lineEditSource_2->setText(rfilename);
            return;
        }
        newname = fname.replace("left","right");
        if(newname != fname)
        {
            rfilename = fileinfo.absolutePath() + "/" + newname;
            ui->lineEditSource_2->setText(rfilename);
            return;
        }
        newname = fname.replace("L","R");
        {
            rfilename = fileinfo.absolutePath() + "/" + newname;
            ui->lineEditSource_2->setText(rfilename);
            return;
        }
}

void DialogOpenMatchPts::on_pushButtonSource_2_clicked()
{
    QString fileName = QFileDialog::getOpenFileName(this,
                                    tr("Open File"), QDir::currentPath());
    ui->lineEditSource_2->setText(fileName);
        if(fileName != "")
        {
            QFileInfo fileinfo(fileName);
            QDir::setCurrent(fileinfo.absolutePath());
        }
        QFileInfo fileinfo (fileName);
        QString fname = fileinfo.fileName();
        QString newname = fname.replace("Right","Left");
        QString rfilename;
        if(newname != fname)
        {
            rfilename = fileinfo.absolutePath() + "/" + newname;
            ui->lineEditSource->setText(rfilename);
            return;
        }
        newname = fname.replace("right","left");
        if(newname != fname)
        {
            rfilename = fileinfo.absolutePath() + "/" + newname;
            ui->lineEditSource->setText(rfilename);
            return;
        }
        newname = fname.replace("R","L");
        {
            rfilename = fileinfo.absolutePath() + "/" + newname;
            ui->lineEditSource->setText(rfilename);
            return;
        }
}

void DialogOpenMatchPts::on_buttonBox_accepted()
{
    srcfilenameLeft = ui->lineEditSource->text();
    srcfilenameRight = ui->lineEditSource_2->text();
    bAddfmf = ui->radioButton_fmf->isChecked();
    bAdddmf = ui->radioButton_dmf->isChecked();
    bAddtmf = ui->radioButton_tmf->isChecked();

}
