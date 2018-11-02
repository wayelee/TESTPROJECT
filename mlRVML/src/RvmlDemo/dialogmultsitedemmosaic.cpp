#include "dialogmultsitedemmosaic.h"
#include "ui_dialogmultsitedemmosaic.h"

DialogMultSiteDemMosaic::DialogMultSiteDemMosaic(QWidget *parent) :
    QDialog(parent),
    ui(new Ui::DialogMultSiteDemMosaic)
{
    ui->setupUi(this);
    dDEMResolution = 1;
}

DialogMultSiteDemMosaic::~DialogMultSiteDemMosaic()
{
    delete ui;
}

void DialogMultSiteDemMosaic::on_pushButtonDest_clicked()
{
    QString fileName = QFileDialog::getSaveFileName(this,
                                    tr("save File"), QDir::currentPath(),QString("tif"));
    QString fname = fileName.append(QString(".tif"));
    ui->lineEditDest->setText(fname);
}

void DialogMultSiteDemMosaic::on_pushButtonDeleteFile_clicked()
{
    if(srcfilenames.count() == 0)
    {
        return;
    }
    srcfilenames.removeAt(srcfilenames.count() - 1);
    QString text = tr("");
    for(int i = 0; i< srcfilenames.count(); i++)
    {
        text += srcfilenames.at(i);
        text += tr("\n");
    }
    ui->textEditSource->setText(text);
}

void DialogMultSiteDemMosaic::on_pushButtonAddFile_clicked()
{

    QStringList fileNames = QFileDialog::getOpenFileNames(this,
                                     tr("Open File"), QDir::currentPath());

     for(int i= 0; i< fileNames.count(); i++)
     {
        if(srcfilenames.contains(fileNames.at(i)) == false)
        {
            srcfilenames.append(fileNames.at(i));
        }
     }
     QString text = tr("");
     for(int i = 0; i< srcfilenames.count(); i++)
     {
         text += srcfilenames.at(i);
         text += tr("\n");
     }
     ui->textEditSource->setText(text);
}

void DialogMultSiteDemMosaic::on_buttonBox_accepted()
{
    dstfilename = ui->lineEditDest->text();
    dDEMResolution = ui->doubleSpinBox->value();
}
