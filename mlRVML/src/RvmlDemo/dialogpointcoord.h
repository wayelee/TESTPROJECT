#ifndef DIALOGPOINTCOORD_H
#define DIALOGPOINTCOORD_H

#include <QDialog>

namespace Ui {
    class DialogPointCoord;
}

class DialogPointCoord : public QDialog
{
    Q_OBJECT

public:
    explicit DialogPointCoord(QWidget *parent = 0);
    ~DialogPointCoord();
    double X;
    double Y;
    void SetXValue(double X);
    void SetYValue(double Y);
    void SetText(QString text);
    void SetID(unsigned long long int lID);

private slots:
    void on_pushButton_OK_clicked();

private:
    Ui::DialogPointCoord *ui;
};

#endif // DIALOGPOINTCOORD_H
