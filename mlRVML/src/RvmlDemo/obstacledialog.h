#ifndef OBSTACLEDIALOG_H
#define OBSTACLEDIALOG_H

#include <QDialog>
#include<qfiledialog.h>
namespace Ui {
    class ObstacleDialog;
}

class ObstacleDialog : public QDialog
{
    Q_OBJECT

public:
    explicit ObstacleDialog(QWidget *parent = 0);
    ~ObstacleDialog();
    QString srcfilename;
    QString dstfilename;
    int nWindowSize;
    double zfactor;

    // 坡度对障碍图的系数
    double dSlopeCoef;
    //最大坡度门限 dSlopCoef * SlopeValue / dMaxSlope 为障碍代价函数贡献值，以下类似
    double dMaxSlope;
    // 粗糙度系数
    double dRoughnessCoef;
    // 最大粗糙度门限
    double dMaxRoughness;
    // 阶梯系数
    double dStepCoef;
    // 最大阶梯系数
    double dMaxStep;
    // 最大障碍代价门限,障碍代价函数超过该值则视之为障碍
    double dMaxObstacleValue;

private slots:
    void on_pushButtonSource_clicked();

    void on_pushButtonDest_clicked();

    void on_lineEditSource_textChanged(QString );

    void on_lineEditDest_textChanged(QString );

    void on_doubleSpinBoxWindowSize_valueChanged(double );

    void on_doubleSpinBoxZfactor_valueChanged(double );

    void on_ObstacleDialog_accepted();

    void on_doubleSpinBoxSlope_valueChanged(double );

    void on_doubleSpinBoxSlopeCoef_valueChanged(double );

    void on_doubleSpinBoxRoughness_valueChanged(double );

    void on_doubleSpinBoxRoughnessCoef_valueChanged(double );

    void on_doubleSpinBoxStep_valueChanged(double );

    void on_doubleSpinBoxStepCoef_valueChanged(double );

    void on_doubleSpinBoxObstacleValue_valueChanged(double );

    void on_buttonBox_accepted();

private:
    Ui::ObstacleDialog *ui;
};

#endif // OBSTACLEDIALOG_H
