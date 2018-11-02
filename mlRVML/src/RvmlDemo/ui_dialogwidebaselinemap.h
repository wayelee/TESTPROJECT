/********************************************************************************
** Form generated from reading UI file 'dialogwidebaselinemap.ui'
**
** Created: Sat Jan 7 17:17:17 2012
**      by: Qt User Interface Compiler version 4.7.2
**
** WARNING! All changes made in this file will be lost when recompiling UI file!
********************************************************************************/

#ifndef UI_DIALOGWIDEBASELINEMAP_H
#define UI_DIALOGWIDEBASELINEMAP_H

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

class Ui_DialogWideBaselineMap
{
public:
    QDialogButtonBox *buttonBox;
    QLabel *label;
    QLabel *label_2;
    QLineEdit *lineEditDest;
    QLineEdit *lineEditSource;
    QPushButton *pushButtonDest;
    QPushButton *pushButtonSource;
    QDoubleSpinBox *doubleSpinBoxTemplateSize;
    QDoubleSpinBox *doubleSpinBoxColRadius;
    QDoubleSpinBox *doubleSpinBoxDEMResolution;
    QDoubleSpinBox *doubleSpinBoxCoef;
    QDoubleSpinBox *doubleSpinBoxGridSize;
    QDoubleSpinBox *doubleSpinBoxRowRadius;
    QLabel *label_3;
    QLabel *label_4;
    QLabel *label_5;
    QLabel *label_6;
    QLabel *label_7;
    QLabel *label_8;

    void setupUi(QDialog *DialogWideBaselineMap)
    {
        if (DialogWideBaselineMap->objectName().isEmpty())
            DialogWideBaselineMap->setObjectName(QString::fromUtf8("DialogWideBaselineMap"));
        DialogWideBaselineMap->resize(643, 367);
        buttonBox = new QDialogButtonBox(DialogWideBaselineMap);
        buttonBox->setObjectName(QString::fromUtf8("buttonBox"));
        buttonBox->setGeometry(QRect(530, 20, 81, 241));
        buttonBox->setOrientation(Qt::Vertical);
        buttonBox->setStandardButtons(QDialogButtonBox::Cancel|QDialogButtonBox::Ok);
        label = new QLabel(DialogWideBaselineMap);
        label->setObjectName(QString::fromUtf8("label"));
        label->setGeometry(QRect(90, 60, 141, 17));
        label_2 = new QLabel(DialogWideBaselineMap);
        label_2->setObjectName(QString::fromUtf8("label_2"));
        label_2->setGeometry(QRect(90, 130, 141, 17));
        lineEditDest = new QLineEdit(DialogWideBaselineMap);
        lineEditDest->setObjectName(QString::fromUtf8("lineEditDest"));
        lineEditDest->setGeometry(QRect(40, 150, 391, 27));
        lineEditSource = new QLineEdit(DialogWideBaselineMap);
        lineEditSource->setObjectName(QString::fromUtf8("lineEditSource"));
        lineEditSource->setGeometry(QRect(40, 90, 391, 27));
        pushButtonDest = new QPushButton(DialogWideBaselineMap);
        pushButtonDest->setObjectName(QString::fromUtf8("pushButtonDest"));
        pushButtonDest->setGeometry(QRect(470, 150, 41, 27));
        pushButtonSource = new QPushButton(DialogWideBaselineMap);
        pushButtonSource->setObjectName(QString::fromUtf8("pushButtonSource"));
        pushButtonSource->setGeometry(QRect(470, 90, 41, 27));
        doubleSpinBoxTemplateSize = new QDoubleSpinBox(DialogWideBaselineMap);
        doubleSpinBoxTemplateSize->setObjectName(QString::fromUtf8("doubleSpinBoxTemplateSize"));
        doubleSpinBoxTemplateSize->setGeometry(QRect(50, 200, 62, 27));
        doubleSpinBoxTemplateSize->setDecimals(0);
        doubleSpinBoxTemplateSize->setMinimum(1);
        doubleSpinBoxTemplateSize->setValue(13);
        doubleSpinBoxColRadius = new QDoubleSpinBox(DialogWideBaselineMap);
        doubleSpinBoxColRadius->setObjectName(QString::fromUtf8("doubleSpinBoxColRadius"));
        doubleSpinBoxColRadius->setGeometry(QRect(50, 260, 62, 27));
        doubleSpinBoxColRadius->setDecimals(0);
        doubleSpinBoxColRadius->setValue(10);
        doubleSpinBoxDEMResolution = new QDoubleSpinBox(DialogWideBaselineMap);
        doubleSpinBoxDEMResolution->setObjectName(QString::fromUtf8("doubleSpinBoxDEMResolution"));
        doubleSpinBoxDEMResolution->setGeometry(QRect(50, 310, 62, 27));
        doubleSpinBoxDEMResolution->setValue(0.1);
        doubleSpinBoxCoef = new QDoubleSpinBox(DialogWideBaselineMap);
        doubleSpinBoxCoef->setObjectName(QString::fromUtf8("doubleSpinBoxCoef"));
        doubleSpinBoxCoef->setGeometry(QRect(190, 310, 62, 27));
        doubleSpinBoxCoef->setValue(0.75);
        doubleSpinBoxGridSize = new QDoubleSpinBox(DialogWideBaselineMap);
        doubleSpinBoxGridSize->setObjectName(QString::fromUtf8("doubleSpinBoxGridSize"));
        doubleSpinBoxGridSize->setGeometry(QRect(190, 200, 62, 27));
        doubleSpinBoxGridSize->setDecimals(0);
        doubleSpinBoxGridSize->setMinimum(1);
        doubleSpinBoxGridSize->setSingleStep(2);
        doubleSpinBoxGridSize->setValue(5);
        doubleSpinBoxRowRadius = new QDoubleSpinBox(DialogWideBaselineMap);
        doubleSpinBoxRowRadius->setObjectName(QString::fromUtf8("doubleSpinBoxRowRadius"));
        doubleSpinBoxRowRadius->setGeometry(QRect(190, 260, 62, 27));
        doubleSpinBoxRowRadius->setDecimals(0);
        doubleSpinBoxRowRadius->setValue(5);
        label_3 = new QLabel(DialogWideBaselineMap);
        label_3->setObjectName(QString::fromUtf8("label_3"));
        label_3->setGeometry(QRect(40, 290, 141, 17));
        label_4 = new QLabel(DialogWideBaselineMap);
        label_4->setObjectName(QString::fromUtf8("label_4"));
        label_4->setGeometry(QRect(190, 180, 141, 17));
        label_5 = new QLabel(DialogWideBaselineMap);
        label_5->setObjectName(QString::fromUtf8("label_5"));
        label_5->setGeometry(QRect(50, 240, 141, 17));
        label_6 = new QLabel(DialogWideBaselineMap);
        label_6->setObjectName(QString::fromUtf8("label_6"));
        label_6->setGeometry(QRect(180, 240, 141, 17));
        label_7 = new QLabel(DialogWideBaselineMap);
        label_7->setObjectName(QString::fromUtf8("label_7"));
        label_7->setGeometry(QRect(180, 290, 141, 17));
        label_8 = new QLabel(DialogWideBaselineMap);
        label_8->setObjectName(QString::fromUtf8("label_8"));
        label_8->setGeometry(QRect(40, 180, 141, 17));

        retranslateUi(DialogWideBaselineMap);
        QObject::connect(buttonBox, SIGNAL(accepted()), DialogWideBaselineMap, SLOT(accept()));
        QObject::connect(buttonBox, SIGNAL(rejected()), DialogWideBaselineMap, SLOT(reject()));

        QMetaObject::connectSlotsByName(DialogWideBaselineMap);
    } // setupUi

    void retranslateUi(QDialog *DialogWideBaselineMap)
    {
        DialogWideBaselineMap->setWindowTitle(QApplication::translate("DialogWideBaselineMap", "Dialog", 0, QApplication::UnicodeUTF8));
        label->setText(QApplication::translate("DialogWideBaselineMap", "\350\276\223\345\205\245SMP\346\226\207\344\273\266\350\267\257\345\276\204", 0, QApplication::UnicodeUTF8));
        label_2->setText(QApplication::translate("DialogWideBaselineMap", "\350\276\223\345\207\272\346\226\207\344\273\266\350\267\257\345\276\204", 0, QApplication::UnicodeUTF8));
        pushButtonDest->setText(QApplication::translate("DialogWideBaselineMap", "...", 0, QApplication::UnicodeUTF8));
        pushButtonSource->setText(QApplication::translate("DialogWideBaselineMap", "...", 0, QApplication::UnicodeUTF8));
        label_3->setText(QApplication::translate("DialogWideBaselineMap", "DEM\345\210\206\350\276\250\347\216\207", 0, QApplication::UnicodeUTF8));
        label_4->setText(QApplication::translate("DialogWideBaselineMap", "\345\214\271\351\205\215\346\240\274\347\275\221\345\244\247\345\260\217", 0, QApplication::UnicodeUTF8));
        label_5->setText(QApplication::translate("DialogWideBaselineMap", "\345\210\227\350\247\206\345\267\256\351\242\204\346\265\213\350\214\203\345\233\264", 0, QApplication::UnicodeUTF8));
        label_6->setText(QApplication::translate("DialogWideBaselineMap", "\350\241\214\350\247\206\345\267\256\351\242\204\346\265\213\350\214\203\345\233\264", 0, QApplication::UnicodeUTF8));
        label_7->setText(QApplication::translate("DialogWideBaselineMap", "\347\233\270\345\205\263\347\263\273\346\225\260\351\230\210\345\200\274", 0, QApplication::UnicodeUTF8));
        label_8->setText(QApplication::translate("DialogWideBaselineMap", "\346\250\241\346\235\277\345\214\271\351\205\215\345\244\247\345\260\217", 0, QApplication::UnicodeUTF8));
    } // retranslateUi

};

namespace Ui {
    class DialogWideBaselineMap: public Ui_DialogWideBaselineMap {};
} // namespace Ui

QT_END_NAMESPACE

#endif // UI_DIALOGWIDEBASELINEMAP_H
