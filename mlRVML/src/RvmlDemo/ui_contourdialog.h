/********************************************************************************
** Form generated from reading UI file 'contourdialog.ui'
**
** Created: Thu Jan 5 16:42:47 2012
**      by: Qt User Interface Compiler version 4.7.2
**
** WARNING! All changes made in this file will be lost when recompiling UI file!
********************************************************************************/

#ifndef UI_CONTOURDIALOG_H
#define UI_CONTOURDIALOG_H

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

class Ui_ContourDialog
{
public:
    QDialogButtonBox *buttonBox;
    QPushButton *pushButtonSource;
    QLineEdit *lineEditSource;
    QLineEdit *lineEditDest;
    QPushButton *pushButtonDest;
    QLabel *label;
    QLabel *label_2;
    QLabel *label_3;
    QDoubleSpinBox *doubleSpinBoxInterval;
    QLabel *label_4;

    void setupUi(QDialog *ContourDialog)
    {
        if (ContourDialog->objectName().isEmpty())
            ContourDialog->setObjectName(QString::fromUtf8("ContourDialog"));
        ContourDialog->resize(676, 306);
        buttonBox = new QDialogButtonBox(ContourDialog);
        buttonBox->setObjectName(QString::fromUtf8("buttonBox"));
        buttonBox->setGeometry(QRect(270, 190, 341, 32));
        buttonBox->setOrientation(Qt::Horizontal);
        buttonBox->setStandardButtons(QDialogButtonBox::Cancel|QDialogButtonBox::Ok);
        pushButtonSource = new QPushButton(ContourDialog);
        pushButtonSource->setObjectName(QString::fromUtf8("pushButtonSource"));
        pushButtonSource->setGeometry(QRect(510, 70, 41, 27));
        lineEditSource = new QLineEdit(ContourDialog);
        lineEditSource->setObjectName(QString::fromUtf8("lineEditSource"));
        lineEditSource->setGeometry(QRect(70, 70, 391, 27));
        lineEditDest = new QLineEdit(ContourDialog);
        lineEditDest->setObjectName(QString::fromUtf8("lineEditDest"));
        lineEditDest->setGeometry(QRect(70, 130, 391, 27));
        pushButtonDest = new QPushButton(ContourDialog);
        pushButtonDest->setObjectName(QString::fromUtf8("pushButtonDest"));
        pushButtonDest->setGeometry(QRect(510, 130, 41, 27));
        label = new QLabel(ContourDialog);
        label->setObjectName(QString::fromUtf8("label"));
        label->setGeometry(QRect(80, 40, 141, 17));
        label_2 = new QLabel(ContourDialog);
        label_2->setObjectName(QString::fromUtf8("label_2"));
        label_2->setGeometry(QRect(80, 110, 141, 17));
        label_3 = new QLabel(ContourDialog);
        label_3->setObjectName(QString::fromUtf8("label_3"));
        label_3->setGeometry(QRect(30, 10, 131, 21));
        doubleSpinBoxInterval = new QDoubleSpinBox(ContourDialog);
        doubleSpinBoxInterval->setObjectName(QString::fromUtf8("doubleSpinBoxInterval"));
        doubleSpinBoxInterval->setGeometry(QRect(80, 200, 151, 27));
        doubleSpinBoxInterval->setMaximum(999999);
        doubleSpinBoxInterval->setValue(0.1);
        label_4 = new QLabel(ContourDialog);
        label_4->setObjectName(QString::fromUtf8("label_4"));
        label_4->setGeometry(QRect(80, 170, 141, 17));

        retranslateUi(ContourDialog);
        QObject::connect(buttonBox, SIGNAL(accepted()), ContourDialog, SLOT(accept()));
        QObject::connect(buttonBox, SIGNAL(rejected()), ContourDialog, SLOT(reject()));

        QMetaObject::connectSlotsByName(ContourDialog);
    } // setupUi

    void retranslateUi(QDialog *ContourDialog)
    {
        ContourDialog->setWindowTitle(QApplication::translate("ContourDialog", "Dialog", 0, QApplication::UnicodeUTF8));
        pushButtonSource->setText(QApplication::translate("ContourDialog", "...", 0, QApplication::UnicodeUTF8));
        pushButtonDest->setText(QApplication::translate("ContourDialog", "...", 0, QApplication::UnicodeUTF8));
        label->setText(QApplication::translate("ContourDialog", "\350\276\223\345\205\245\351\253\230\347\250\213\346\226\207\344\273\266\350\267\257\345\276\204", 0, QApplication::UnicodeUTF8));
        label_2->setText(QApplication::translate("ContourDialog", "\350\276\223\345\207\272\346\226\207\344\273\266\350\267\257\345\276\204", 0, QApplication::UnicodeUTF8));
        label_3->setText(QApplication::translate("ContourDialog", "\347\255\211\351\253\230\347\272\277\350\256\276\347\275\256\345\257\271\350\257\235\346\241\206", 0, QApplication::UnicodeUTF8));
        label_4->setText(QApplication::translate("ContourDialog", "\347\255\211\351\253\230\350\267\235", 0, QApplication::UnicodeUTF8));
    } // retranslateUi

};

namespace Ui {
    class ContourDialog: public Ui_ContourDialog {};
} // namespace Ui

QT_END_NAMESPACE

#endif // UI_CONTOURDIALOG_H
