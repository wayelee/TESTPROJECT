/********************************************************************************
** Form generated from reading UI file 'viewsheddialog.ui'
**
** Created: Wed Aug 29 10:22:15 2012
**      by: Qt User Interface Compiler version 4.7.2
**
** WARNING! All changes made in this file will be lost when recompiling UI file!
********************************************************************************/

#ifndef UI_VIEWSHEDDIALOG_H
#define UI_VIEWSHEDDIALOG_H

#include <QtCore/QVariant>
#include <QtGui/QAction>
#include <QtGui/QApplication>
#include <QtGui/QButtonGroup>
#include <QtGui/QCheckBox>
#include <QtGui/QDialog>
#include <QtGui/QDialogButtonBox>
#include <QtGui/QDoubleSpinBox>
#include <QtGui/QHeaderView>
#include <QtGui/QLabel>
#include <QtGui/QLineEdit>
#include <QtGui/QPushButton>

QT_BEGIN_NAMESPACE

class Ui_ViewShedDialog
{
public:
    QDialogButtonBox *buttonBox;
    QDoubleSpinBox *doubleSpinBoxX;
    QPushButton *pushButtonSource;
    QPushButton *pushButtonDest;
    QLabel *label_3;
    QLabel *label_4;
    QLabel *label;
    QLabel *label_2;
    QLineEdit *lineEditSource;
    QLineEdit *lineEditDest;
    QDoubleSpinBox *doubleSpinBoxY;
    QDoubleSpinBox *doubleSpinBoxZ;
    QLabel *label_5;
    QLabel *label_6;
    QLabel *label_7;
    QLabel *label_8;
    QCheckBox *checkBoxInverseHeight;

    void setupUi(QDialog *ViewShedDialog)
    {
        if (ViewShedDialog->objectName().isEmpty())
            ViewShedDialog->setObjectName(QString::fromUtf8("ViewShedDialog"));
        ViewShedDialog->resize(638, 339);
        buttonBox = new QDialogButtonBox(ViewShedDialog);
        buttonBox->setObjectName(QString::fromUtf8("buttonBox"));
        buttonBox->setGeometry(QRect(550, 30, 81, 241));
        buttonBox->setOrientation(Qt::Vertical);
        buttonBox->setStandardButtons(QDialogButtonBox::Cancel|QDialogButtonBox::Ok);
        doubleSpinBoxX = new QDoubleSpinBox(ViewShedDialog);
        doubleSpinBoxX->setObjectName(QString::fromUtf8("doubleSpinBoxX"));
        doubleSpinBoxX->setGeometry(QRect(110, 220, 151, 27));
        doubleSpinBoxX->setDecimals(0);
        doubleSpinBoxX->setMinimum(-999999);
        doubleSpinBoxX->setMaximum(999999);
        doubleSpinBoxX->setValue(0);
        pushButtonSource = new QPushButton(ViewShedDialog);
        pushButtonSource->setObjectName(QString::fromUtf8("pushButtonSource"));
        pushButtonSource->setGeometry(QRect(500, 100, 41, 27));
        pushButtonDest = new QPushButton(ViewShedDialog);
        pushButtonDest->setObjectName(QString::fromUtf8("pushButtonDest"));
        pushButtonDest->setGeometry(QRect(500, 160, 41, 27));
        label_3 = new QLabel(ViewShedDialog);
        label_3->setObjectName(QString::fromUtf8("label_3"));
        label_3->setGeometry(QRect(20, 40, 161, 21));
        label_4 = new QLabel(ViewShedDialog);
        label_4->setObjectName(QString::fromUtf8("label_4"));
        label_4->setGeometry(QRect(70, 200, 141, 17));
        label = new QLabel(ViewShedDialog);
        label->setObjectName(QString::fromUtf8("label"));
        label->setGeometry(QRect(70, 70, 141, 17));
        label_2 = new QLabel(ViewShedDialog);
        label_2->setObjectName(QString::fromUtf8("label_2"));
        label_2->setGeometry(QRect(70, 140, 141, 17));
        lineEditSource = new QLineEdit(ViewShedDialog);
        lineEditSource->setObjectName(QString::fromUtf8("lineEditSource"));
        lineEditSource->setGeometry(QRect(60, 100, 391, 27));
        lineEditDest = new QLineEdit(ViewShedDialog);
        lineEditDest->setObjectName(QString::fromUtf8("lineEditDest"));
        lineEditDest->setGeometry(QRect(60, 160, 391, 27));
        doubleSpinBoxY = new QDoubleSpinBox(ViewShedDialog);
        doubleSpinBoxY->setObjectName(QString::fromUtf8("doubleSpinBoxY"));
        doubleSpinBoxY->setGeometry(QRect(110, 250, 151, 27));
        doubleSpinBoxY->setDecimals(0);
        doubleSpinBoxY->setMinimum(-999999);
        doubleSpinBoxY->setMaximum(999999);
        doubleSpinBoxY->setValue(0);
        doubleSpinBoxZ = new QDoubleSpinBox(ViewShedDialog);
        doubleSpinBoxZ->setObjectName(QString::fromUtf8("doubleSpinBoxZ"));
        doubleSpinBoxZ->setGeometry(QRect(110, 280, 151, 27));
        doubleSpinBoxZ->setMinimum(-999999);
        doubleSpinBoxZ->setMaximum(999999);
        doubleSpinBoxZ->setValue(0);
        label_5 = new QLabel(ViewShedDialog);
        label_5->setObjectName(QString::fromUtf8("label_5"));
        label_5->setGeometry(QRect(70, 230, 141, 17));
        label_6 = new QLabel(ViewShedDialog);
        label_6->setObjectName(QString::fromUtf8("label_6"));
        label_6->setGeometry(QRect(70, 260, 141, 17));
        label_7 = new QLabel(ViewShedDialog);
        label_7->setObjectName(QString::fromUtf8("label_7"));
        label_7->setGeometry(QRect(70, 280, 141, 20));
        label_8 = new QLabel(ViewShedDialog);
        label_8->setObjectName(QString::fromUtf8("label_8"));
        label_8->setGeometry(QRect(280, 280, 141, 20));
        checkBoxInverseHeight = new QCheckBox(ViewShedDialog);
        checkBoxInverseHeight->setObjectName(QString::fromUtf8("checkBoxInverseHeight"));
        checkBoxInverseHeight->setGeometry(QRect(390, 220, 97, 22));

        retranslateUi(ViewShedDialog);
        QObject::connect(buttonBox, SIGNAL(accepted()), ViewShedDialog, SLOT(accept()));
        QObject::connect(buttonBox, SIGNAL(rejected()), ViewShedDialog, SLOT(reject()));

        QMetaObject::connectSlotsByName(ViewShedDialog);
    } // setupUi

    void retranslateUi(QDialog *ViewShedDialog)
    {
        ViewShedDialog->setWindowTitle(QApplication::translate("ViewShedDialog", "Dialog", 0, QApplication::UnicodeUTF8));
        pushButtonSource->setText(QApplication::translate("ViewShedDialog", "...", 0, QApplication::UnicodeUTF8));
        pushButtonDest->setText(QApplication::translate("ViewShedDialog", "...", 0, QApplication::UnicodeUTF8));
        label_3->setText(QApplication::translate("ViewShedDialog", "\351\200\232\350\247\206\345\210\206\346\236\220\350\256\276\347\275\256\345\257\271\350\257\235\346\241\206", 0, QApplication::UnicodeUTF8));
        label_4->setText(QApplication::translate("ViewShedDialog", "\350\247\206\347\202\271\345\235\220\346\240\207", 0, QApplication::UnicodeUTF8));
        label->setText(QApplication::translate("ViewShedDialog", "\350\276\223\345\205\245\351\253\230\347\250\213\346\226\207\344\273\266\350\267\257\345\276\204", 0, QApplication::UnicodeUTF8));
        label_2->setText(QApplication::translate("ViewShedDialog", "\350\276\223\345\207\272\346\226\207\344\273\266\350\267\257\345\276\204", 0, QApplication::UnicodeUTF8));
        label_5->setText(QApplication::translate("ViewShedDialog", "X", 0, QApplication::UnicodeUTF8));
        label_6->setText(QApplication::translate("ViewShedDialog", "Y", 0, QApplication::UnicodeUTF8));
        label_7->setText(QApplication::translate("ViewShedDialog", "Z", 0, QApplication::UnicodeUTF8));
        label_8->setText(QApplication::translate("ViewShedDialog", "\357\274\210\350\267\235\347\246\273\345\234\260\351\235\242\351\253\230\345\272\246\357\274\211", 0, QApplication::UnicodeUTF8));
        checkBoxInverseHeight->setText(QApplication::translate("ViewShedDialog", "\351\253\230\347\250\213\347\277\273\350\275\254", 0, QApplication::UnicodeUTF8));
    } // retranslateUi

};

namespace Ui {
    class ViewShedDialog: public Ui_ViewShedDialog {};
} // namespace Ui

QT_END_NAMESPACE

#endif // UI_VIEWSHEDDIALOG_H
