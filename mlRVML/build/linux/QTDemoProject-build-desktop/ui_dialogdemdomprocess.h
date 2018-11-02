/********************************************************************************
** Form generated from reading UI file 'dialogdemdomprocess.ui'
**
** Created: Wed Feb 8 17:53:33 2012
**      by: Qt User Interface Compiler version 4.7.2
**
** WARNING! All changes made in this file will be lost when recompiling UI file!
********************************************************************************/

#ifndef UI_DIALOGDEMDOMPROCESS_H
#define UI_DIALOGDEMDOMPROCESS_H

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
#include <QtGui/QRadioButton>
#include <QtGui/QSpinBox>

QT_BEGIN_NAMESPACE

class Ui_DialogDEMDOMProcess
{
public:
    QDialogButtonBox *buttonBox;
    QLineEdit *lineEditSource;
    QLabel *label;
    QLineEdit *lineEditDest;
    QPushButton *pushButtonSource;
    QLabel *label_2;
    QPushButton *pushButtonDest;
    QDoubleSpinBox *doubleSpinBoxLeftTopX;
    QDoubleSpinBox *doubleSpinBoxLeftTopY;
    QDoubleSpinBox *doubleSpinBoxRightBottomY;
    QDoubleSpinBox *doubleSpinBoxRightBottomX;
    QDoubleSpinBox *doubleSpinBoxSampleCoef;
    QLabel *label_3;
    QLabel *label_4;
    QLabel *label_5;
    QLabel *label_6;
    QLabel *label_7;
    QRadioButton *radioButtonPixelBased;
    QRadioButton *radioButtonGeoBased;
    QLabel *label_8;
    QSpinBox *spinBoxBandNum;

    void setupUi(QDialog *DialogDEMDOMProcess)
    {
        if (DialogDEMDOMProcess->objectName().isEmpty())
            DialogDEMDOMProcess->setObjectName(QString::fromUtf8("DialogDEMDOMProcess"));
        DialogDEMDOMProcess->resize(652, 345);
        buttonBox = new QDialogButtonBox(DialogDEMDOMProcess);
        buttonBox->setObjectName(QString::fromUtf8("buttonBox"));
        buttonBox->setGeometry(QRect(570, 10, 81, 241));
        buttonBox->setOrientation(Qt::Vertical);
        buttonBox->setStandardButtons(QDialogButtonBox::Cancel|QDialogButtonBox::Ok);
        lineEditSource = new QLineEdit(DialogDEMDOMProcess);
        lineEditSource->setObjectName(QString::fromUtf8("lineEditSource"));
        lineEditSource->setGeometry(QRect(70, 120, 391, 27));
        label = new QLabel(DialogDEMDOMProcess);
        label->setObjectName(QString::fromUtf8("label"));
        label->setGeometry(QRect(120, 90, 141, 17));
        lineEditDest = new QLineEdit(DialogDEMDOMProcess);
        lineEditDest->setObjectName(QString::fromUtf8("lineEditDest"));
        lineEditDest->setGeometry(QRect(70, 180, 391, 27));
        pushButtonSource = new QPushButton(DialogDEMDOMProcess);
        pushButtonSource->setObjectName(QString::fromUtf8("pushButtonSource"));
        pushButtonSource->setGeometry(QRect(510, 120, 41, 27));
        label_2 = new QLabel(DialogDEMDOMProcess);
        label_2->setObjectName(QString::fromUtf8("label_2"));
        label_2->setGeometry(QRect(120, 160, 141, 17));
        pushButtonDest = new QPushButton(DialogDEMDOMProcess);
        pushButtonDest->setObjectName(QString::fromUtf8("pushButtonDest"));
        pushButtonDest->setGeometry(QRect(510, 180, 41, 27));
        doubleSpinBoxLeftTopX = new QDoubleSpinBox(DialogDEMDOMProcess);
        doubleSpinBoxLeftTopX->setObjectName(QString::fromUtf8("doubleSpinBoxLeftTopX"));
        doubleSpinBoxLeftTopX->setGeometry(QRect(90, 230, 121, 27));
        doubleSpinBoxLeftTopX->setMaximum(100000);
        doubleSpinBoxLeftTopY = new QDoubleSpinBox(DialogDEMDOMProcess);
        doubleSpinBoxLeftTopY->setObjectName(QString::fromUtf8("doubleSpinBoxLeftTopY"));
        doubleSpinBoxLeftTopY->setGeometry(QRect(90, 280, 121, 27));
        doubleSpinBoxLeftTopY->setMaximum(100000);
        doubleSpinBoxRightBottomY = new QDoubleSpinBox(DialogDEMDOMProcess);
        doubleSpinBoxRightBottomY->setObjectName(QString::fromUtf8("doubleSpinBoxRightBottomY"));
        doubleSpinBoxRightBottomY->setGeometry(QRect(231, 280, 101, 27));
        doubleSpinBoxRightBottomY->setMaximum(100000);
        doubleSpinBoxRightBottomX = new QDoubleSpinBox(DialogDEMDOMProcess);
        doubleSpinBoxRightBottomX->setObjectName(QString::fromUtf8("doubleSpinBoxRightBottomX"));
        doubleSpinBoxRightBottomX->setGeometry(QRect(231, 230, 101, 27));
        doubleSpinBoxRightBottomX->setMaximum(100000);
        doubleSpinBoxSampleCoef = new QDoubleSpinBox(DialogDEMDOMProcess);
        doubleSpinBoxSampleCoef->setObjectName(QString::fromUtf8("doubleSpinBoxSampleCoef"));
        doubleSpinBoxSampleCoef->setGeometry(QRect(370, 230, 62, 27));
        label_3 = new QLabel(DialogDEMDOMProcess);
        label_3->setObjectName(QString::fromUtf8("label_3"));
        label_3->setGeometry(QRect(80, 210, 141, 17));
        label_4 = new QLabel(DialogDEMDOMProcess);
        label_4->setObjectName(QString::fromUtf8("label_4"));
        label_4->setGeometry(QRect(80, 260, 141, 17));
        label_5 = new QLabel(DialogDEMDOMProcess);
        label_5->setObjectName(QString::fromUtf8("label_5"));
        label_5->setGeometry(QRect(270, 210, 141, 17));
        label_6 = new QLabel(DialogDEMDOMProcess);
        label_6->setObjectName(QString::fromUtf8("label_6"));
        label_6->setGeometry(QRect(270, 260, 141, 17));
        label_7 = new QLabel(DialogDEMDOMProcess);
        label_7->setObjectName(QString::fromUtf8("label_7"));
        label_7->setGeometry(QRect(370, 210, 141, 17));
        radioButtonPixelBased = new QRadioButton(DialogDEMDOMProcess);
        radioButtonPixelBased->setObjectName(QString::fromUtf8("radioButtonPixelBased"));
        radioButtonPixelBased->setGeometry(QRect(500, 240, 116, 22));
        radioButtonPixelBased->setChecked(true);
        radioButtonGeoBased = new QRadioButton(DialogDEMDOMProcess);
        radioButtonGeoBased->setObjectName(QString::fromUtf8("radioButtonGeoBased"));
        radioButtonGeoBased->setGeometry(QRect(500, 270, 116, 22));
        label_8 = new QLabel(DialogDEMDOMProcess);
        label_8->setObjectName(QString::fromUtf8("label_8"));
        label_8->setGeometry(QRect(370, 260, 141, 17));
        spinBoxBandNum = new QSpinBox(DialogDEMDOMProcess);
        spinBoxBandNum->setObjectName(QString::fromUtf8("spinBoxBandNum"));
        spinBoxBandNum->setGeometry(QRect(370, 280, 59, 27));
        spinBoxBandNum->setMinimum(1);

        retranslateUi(DialogDEMDOMProcess);
        QObject::connect(buttonBox, SIGNAL(accepted()), DialogDEMDOMProcess, SLOT(accept()));
        QObject::connect(buttonBox, SIGNAL(rejected()), DialogDEMDOMProcess, SLOT(reject()));

        QMetaObject::connectSlotsByName(DialogDEMDOMProcess);
    } // setupUi

    void retranslateUi(QDialog *DialogDEMDOMProcess)
    {
        DialogDEMDOMProcess->setWindowTitle(QApplication::translate("DialogDEMDOMProcess", "Dialog", 0, QApplication::UnicodeUTF8));
        label->setText(QApplication::translate("DialogDEMDOMProcess", "\350\276\223\345\205\245\346\226\207\344\273\266\350\267\257\345\276\204", 0, QApplication::UnicodeUTF8));
        pushButtonSource->setText(QApplication::translate("DialogDEMDOMProcess", "...", 0, QApplication::UnicodeUTF8));
        label_2->setText(QApplication::translate("DialogDEMDOMProcess", "\350\276\223\345\207\272\346\226\207\344\273\266\350\267\257\345\276\204", 0, QApplication::UnicodeUTF8));
        pushButtonDest->setText(QApplication::translate("DialogDEMDOMProcess", "...", 0, QApplication::UnicodeUTF8));
        label_3->setText(QApplication::translate("DialogDEMDOMProcess", "\345\267\246\344\270\212\350\247\222X", 0, QApplication::UnicodeUTF8));
        label_4->setText(QApplication::translate("DialogDEMDOMProcess", "\345\267\246\344\270\212\350\247\222Y", 0, QApplication::UnicodeUTF8));
        label_5->setText(QApplication::translate("DialogDEMDOMProcess", "\345\217\263\344\270\213\350\247\222X", 0, QApplication::UnicodeUTF8));
        label_6->setText(QApplication::translate("DialogDEMDOMProcess", "\345\217\263\344\270\213\350\247\222Y", 0, QApplication::UnicodeUTF8));
        label_7->setText(QApplication::translate("DialogDEMDOMProcess", "\351\207\215\351\207\207\346\240\267\347\263\273\346\225\260", 0, QApplication::UnicodeUTF8));
        radioButtonPixelBased->setText(QApplication::translate("DialogDEMDOMProcess", "\346\214\211\345\203\217\347\264\240\345\235\220\346\240\207", 0, QApplication::UnicodeUTF8));
        radioButtonGeoBased->setText(QApplication::translate("DialogDEMDOMProcess", "\346\214\211\345\234\260\347\220\206\345\235\220\346\240\207", 0, QApplication::UnicodeUTF8));
        label_8->setText(QApplication::translate("DialogDEMDOMProcess", "\346\214\207\345\256\232\346\263\242\346\256\265", 0, QApplication::UnicodeUTF8));
    } // retranslateUi

};

namespace Ui {
    class DialogDEMDOMProcess: public Ui_DialogDEMDOMProcess {};
} // namespace Ui

QT_END_NAMESPACE

#endif // UI_DIALOGDEMDOMPROCESS_H
