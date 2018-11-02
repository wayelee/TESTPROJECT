/********************************************************************************
** Form generated from reading UI file 'obstacledialog.ui'
**
** Created: Thu Jan 5 11:16:12 2012
**      by: Qt User Interface Compiler version 4.7.2
**
** WARNING! All changes made in this file will be lost when recompiling UI file!
********************************************************************************/

#ifndef UI_OBSTACLEDIALOG_H
#define UI_OBSTACLEDIALOG_H

#include <QtCore/QVariant>
#include <QtGui/QAction>
#include <QtGui/QApplication>
#include <QtGui/QButtonGroup>
#include <QtGui/QDialog>
#include <QtGui/QDialogButtonBox>
#include <QtGui/QDoubleSpinBox>
#include <QtGui/QHeaderView>
#include <QtGui/QLabel>
#include <QtGui/QLineEdit>
#include <QtGui/QPushButton>

QT_BEGIN_NAMESPACE

class Ui_ObstacleDialog
{
public:
    QDialogButtonBox *buttonBox;
    QDoubleSpinBox *doubleSpinBoxZfactor;
    QLabel *label_5;
    QLabel *label_2;
    QLabel *label_4;
    QLineEdit *lineEditSource;
    QLabel *label;
    QPushButton *pushButtonDest;
    QPushButton *pushButtonSource;
    QDoubleSpinBox *doubleSpinBoxWindowSize;
    QLineEdit *lineEditDest;
    QLabel *label_3;
    QDoubleSpinBox *doubleSpinBoxSlope;
    QDoubleSpinBox *doubleSpinBoxSlopeCoef;
    QDoubleSpinBox *doubleSpinBoxRoughnessCoef;
    QDoubleSpinBox *doubleSpinBoxRoughness;
    QDoubleSpinBox *doubleSpinBoxStep;
    QDoubleSpinBox *doubleSpinBoxStepCoef;
    QDoubleSpinBox *doubleSpinBoxObstacleValue;
    QLabel *label_6;
    QLabel *label_7;
    QLabel *label_8;
    QLabel *label_9;
    QLabel *label_10;
    QLabel *label_11;
    QLabel *label_12;

    void setupUi(QDialog *ObstacleDialog)
    {
        if (ObstacleDialog->objectName().isEmpty())
            ObstacleDialog->setObjectName(QString::fromUtf8("ObstacleDialog"));
        ObstacleDialog->resize(588, 488);
        buttonBox = new QDialogButtonBox(ObstacleDialog);
        buttonBox->setObjectName(QString::fromUtf8("buttonBox"));
        buttonBox->setGeometry(QRect(490, 40, 81, 241));
        buttonBox->setOrientation(Qt::Vertical);
        buttonBox->setStandardButtons(QDialogButtonBox::Cancel|QDialogButtonBox::Ok);
        doubleSpinBoxZfactor = new QDoubleSpinBox(ObstacleDialog);
        doubleSpinBoxZfactor->setObjectName(QString::fromUtf8("doubleSpinBoxZfactor"));
        doubleSpinBoxZfactor->setGeometry(QRect(200, 280, 71, 27));
        doubleSpinBoxZfactor->setValue(1);
        label_5 = new QLabel(ObstacleDialog);
        label_5->setObjectName(QString::fromUtf8("label_5"));
        label_5->setGeometry(QRect(40, 250, 141, 17));
        label_2 = new QLabel(ObstacleDialog);
        label_2->setObjectName(QString::fromUtf8("label_2"));
        label_2->setGeometry(QRect(60, 190, 141, 17));
        label_4 = new QLabel(ObstacleDialog);
        label_4->setObjectName(QString::fromUtf8("label_4"));
        label_4->setGeometry(QRect(190, 250, 141, 17));
        lineEditSource = new QLineEdit(ObstacleDialog);
        lineEditSource->setObjectName(QString::fromUtf8("lineEditSource"));
        lineEditSource->setGeometry(QRect(10, 150, 391, 27));
        label = new QLabel(ObstacleDialog);
        label->setObjectName(QString::fromUtf8("label"));
        label->setGeometry(QRect(60, 120, 141, 17));
        pushButtonDest = new QPushButton(ObstacleDialog);
        pushButtonDest->setObjectName(QString::fromUtf8("pushButtonDest"));
        pushButtonDest->setGeometry(QRect(450, 210, 41, 27));
        pushButtonSource = new QPushButton(ObstacleDialog);
        pushButtonSource->setObjectName(QString::fromUtf8("pushButtonSource"));
        pushButtonSource->setGeometry(QRect(450, 150, 41, 27));
        doubleSpinBoxWindowSize = new QDoubleSpinBox(ObstacleDialog);
        doubleSpinBoxWindowSize->setObjectName(QString::fromUtf8("doubleSpinBoxWindowSize"));
        doubleSpinBoxWindowSize->setGeometry(QRect(70, 280, 71, 27));
        doubleSpinBoxWindowSize->setDecimals(0);
        doubleSpinBoxWindowSize->setMinimum(3);
        doubleSpinBoxWindowSize->setMaximum(99);
        doubleSpinBoxWindowSize->setSingleStep(2);
        doubleSpinBoxWindowSize->setValue(3);
        lineEditDest = new QLineEdit(ObstacleDialog);
        lineEditDest->setObjectName(QString::fromUtf8("lineEditDest"));
        lineEditDest->setGeometry(QRect(10, 210, 391, 27));
        label_3 = new QLabel(ObstacleDialog);
        label_3->setObjectName(QString::fromUtf8("label_3"));
        label_3->setGeometry(QRect(60, 40, 181, 17));
        doubleSpinBoxSlope = new QDoubleSpinBox(ObstacleDialog);
        doubleSpinBoxSlope->setObjectName(QString::fromUtf8("doubleSpinBoxSlope"));
        doubleSpinBoxSlope->setGeometry(QRect(70, 330, 71, 27));
        doubleSpinBoxSlope->setMaximum(90);
        doubleSpinBoxSlope->setValue(30);
        doubleSpinBoxSlopeCoef = new QDoubleSpinBox(ObstacleDialog);
        doubleSpinBoxSlopeCoef->setObjectName(QString::fromUtf8("doubleSpinBoxSlopeCoef"));
        doubleSpinBoxSlopeCoef->setGeometry(QRect(200, 330, 71, 27));
        doubleSpinBoxSlopeCoef->setMaximum(100);
        doubleSpinBoxSlopeCoef->setValue(100);
        doubleSpinBoxRoughnessCoef = new QDoubleSpinBox(ObstacleDialog);
        doubleSpinBoxRoughnessCoef->setObjectName(QString::fromUtf8("doubleSpinBoxRoughnessCoef"));
        doubleSpinBoxRoughnessCoef->setGeometry(QRect(200, 380, 71, 27));
        doubleSpinBoxRoughnessCoef->setMaximum(100);
        doubleSpinBoxRoughnessCoef->setValue(100);
        doubleSpinBoxRoughness = new QDoubleSpinBox(ObstacleDialog);
        doubleSpinBoxRoughness->setObjectName(QString::fromUtf8("doubleSpinBoxRoughness"));
        doubleSpinBoxRoughness->setGeometry(QRect(70, 380, 71, 27));
        doubleSpinBoxRoughness->setValue(30);
        doubleSpinBoxStep = new QDoubleSpinBox(ObstacleDialog);
        doubleSpinBoxStep->setObjectName(QString::fromUtf8("doubleSpinBoxStep"));
        doubleSpinBoxStep->setGeometry(QRect(70, 430, 71, 27));
        doubleSpinBoxStep->setValue(30);
        doubleSpinBoxStepCoef = new QDoubleSpinBox(ObstacleDialog);
        doubleSpinBoxStepCoef->setObjectName(QString::fromUtf8("doubleSpinBoxStepCoef"));
        doubleSpinBoxStepCoef->setGeometry(QRect(200, 430, 71, 27));
        doubleSpinBoxStepCoef->setMaximum(100);
        doubleSpinBoxStepCoef->setValue(100);
        doubleSpinBoxObstacleValue = new QDoubleSpinBox(ObstacleDialog);
        doubleSpinBoxObstacleValue->setObjectName(QString::fromUtf8("doubleSpinBoxObstacleValue"));
        doubleSpinBoxObstacleValue->setGeometry(QRect(340, 340, 111, 27));
        doubleSpinBoxObstacleValue->setMaximum(100000);
        doubleSpinBoxObstacleValue->setValue(100000);
        label_6 = new QLabel(ObstacleDialog);
        label_6->setObjectName(QString::fromUtf8("label_6"));
        label_6->setGeometry(QRect(20, 310, 181, 17));
        label_7 = new QLabel(ObstacleDialog);
        label_7->setObjectName(QString::fromUtf8("label_7"));
        label_7->setGeometry(QRect(20, 360, 181, 17));
        label_8 = new QLabel(ObstacleDialog);
        label_8->setObjectName(QString::fromUtf8("label_8"));
        label_8->setGeometry(QRect(20, 410, 181, 17));
        label_9 = new QLabel(ObstacleDialog);
        label_9->setObjectName(QString::fromUtf8("label_9"));
        label_9->setGeometry(QRect(160, 410, 181, 17));
        label_10 = new QLabel(ObstacleDialog);
        label_10->setObjectName(QString::fromUtf8("label_10"));
        label_10->setGeometry(QRect(160, 360, 181, 17));
        label_11 = new QLabel(ObstacleDialog);
        label_11->setObjectName(QString::fromUtf8("label_11"));
        label_11->setGeometry(QRect(160, 310, 181, 17));
        label_12 = new QLabel(ObstacleDialog);
        label_12->setObjectName(QString::fromUtf8("label_12"));
        label_12->setGeometry(QRect(330, 310, 181, 17));

        retranslateUi(ObstacleDialog);
        QObject::connect(buttonBox, SIGNAL(accepted()), ObstacleDialog, SLOT(accept()));
        QObject::connect(buttonBox, SIGNAL(rejected()), ObstacleDialog, SLOT(reject()));

        QMetaObject::connectSlotsByName(ObstacleDialog);
    } // setupUi

    void retranslateUi(QDialog *ObstacleDialog)
    {
        ObstacleDialog->setWindowTitle(QApplication::translate("ObstacleDialog", "Dialog", 0, QApplication::UnicodeUTF8));
        label_5->setText(QApplication::translate("ObstacleDialog", "\350\256\241\347\256\227\347\252\227\345\217\243\345\244\247\345\260\217", 0, QApplication::UnicodeUTF8));
        label_2->setText(QApplication::translate("ObstacleDialog", "\350\276\223\345\207\272\346\226\207\344\273\266\350\267\257\345\276\204", 0, QApplication::UnicodeUTF8));
        label_4->setText(QApplication::translate("ObstacleDialog", "\351\253\230\347\250\213\347\274\251\346\224\276\345\233\240\345\255\220", 0, QApplication::UnicodeUTF8));
        label->setText(QApplication::translate("ObstacleDialog", "\350\276\223\345\205\245\351\253\230\347\250\213\346\226\207\344\273\266\350\267\257\345\276\204", 0, QApplication::UnicodeUTF8));
        pushButtonDest->setText(QApplication::translate("ObstacleDialog", "...", 0, QApplication::UnicodeUTF8));
        pushButtonSource->setText(QApplication::translate("ObstacleDialog", "...", 0, QApplication::UnicodeUTF8));
        label_3->setText(QApplication::translate("ObstacleDialog", "\351\232\234\347\242\215\345\233\276\345\217\202\346\225\260\350\256\276\347\275\256", 0, QApplication::UnicodeUTF8));
        label_6->setText(QApplication::translate("ObstacleDialog", "\345\205\201\350\256\270\346\234\200\345\244\247\345\235\241\345\272\246", 0, QApplication::UnicodeUTF8));
        label_7->setText(QApplication::translate("ObstacleDialog", "\345\205\201\350\256\270\346\234\200\345\244\247\347\262\227\347\263\231\345\272\246", 0, QApplication::UnicodeUTF8));
        label_8->setText(QApplication::translate("ObstacleDialog", "\345\205\201\350\256\270\346\234\200\345\244\247\351\230\266\346\242\257\351\232\234\347\242\215", 0, QApplication::UnicodeUTF8));
        label_9->setText(QApplication::translate("ObstacleDialog", "\351\230\266\346\242\257\351\232\234\347\242\215\347\263\273\346\225\260", 0, QApplication::UnicodeUTF8));
        label_10->setText(QApplication::translate("ObstacleDialog", "\347\262\227\347\263\231\345\272\246\351\232\234\347\242\215\347\263\273\346\225\260", 0, QApplication::UnicodeUTF8));
        label_11->setText(QApplication::translate("ObstacleDialog", "\345\235\241\345\272\246\351\232\234\347\242\215\347\263\273\346\225\260", 0, QApplication::UnicodeUTF8));
        label_12->setText(QApplication::translate("ObstacleDialog", "\346\234\200\345\244\247\351\232\234\347\242\215\345\207\275\346\225\260\345\200\274", 0, QApplication::UnicodeUTF8));
    } // retranslateUi

};

namespace Ui {
    class ObstacleDialog: public Ui_ObstacleDialog {};
} // namespace Ui

QT_END_NAMESPACE

#endif // UI_OBSTACLEDIALOG_H
