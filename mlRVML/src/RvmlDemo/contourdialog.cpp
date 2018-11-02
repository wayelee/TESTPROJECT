#include "contourdialog.h"
#include "ui_contourdialog.h"

ContourDialog::ContourDialog(QWidget *parent) :
    QDialog(parent),
    ui(new Ui::ContourDialog)
{
    ui->setupUi(this);
    interval =300;
}

ContourDialog::~ContourDialog()
{
    delete ui;
}

void ContourDialog::on_pushButtonSource_clicked()
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

void ContourDialog::on_pushButtonDest_clicked()
{
    QString fileName = QFileDialog::getSaveFileName(this,
                                    tr("save File"), QDir::currentPath(),QString("shp"));

    QString fname = fileName.append(QString(".shp"));
    ui->lineEditDest->setText(fname);

}

void ContourDialog::on_lineEditSource_textChanged(QString  )
{
    //  QByteArray gb = ui->lineEdit->text().toLatin1();
    //  srcfilename = gb.data();
    srcfilename = ui->lineEditSource->text();
}

void ContourDialog::on_lineEditDest_textChanged(QString  )
{
    dstfilename = ui->lineEditDest->text();
}

void ContourDialog::on_doubleSpinBoxInterval_valueChanged(double e )
{
   interval = e;
   return;
}

void ContourDialog::on_ContourDialog_accepted()
{
    srcfilename = ui->lineEditSource->text();
    dstfilename = ui->lineEditDest->text();
    interval = ui->doubleSpinBoxInterval->value();
}
