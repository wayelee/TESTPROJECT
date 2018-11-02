/********************************************************************************
** Form generated from reading UI file 'dialogtinsimplify.ui'
**
** Created: Tue Jan 10 18:06:20 2012
**      by: Qt User Interface Compiler version 4.7.2
**
** WARNING! All changes made in this file will be lost when recompiling UI file!
********************************************************************************/

#ifndef UI_DIALOGTINSIMPLIFY_H
#define UI_DIALOGTINSIMPLIFY_H

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

class Ui_DialogTinSimplify
{
public:
    QDialogButtonBox *buttonBox;
    QLineEdit *lineEditSource;
    QLineEdit *lineEditDest;
    QLabel *label;
    QPushButton *pushButtonDest;
    QPushButton *pushButtonSource;
    QLabel *label_2;
    QDoubleSpinBox *doubleSpinBoxSimplifyCoef;
    QLabel *label_3;

    void setupUi(QDialog *DialogTinSimplify)
    {
        if (DialogTinSimplify->objectName().isEmpty())
            DialogTinSimplify->setObjectName(QString::fromUtf8("DialogTinSimplify"));
        DialogTinSimplify->resize(576, 350);
        buttonBox = new QDialogButtonBox(DialogTinSimplify);
        buttonBox->setObjectName(QString::fromUtf8("buttonBox"));
        buttonBox->setGeometry(QRect(490, 10, 81, 241));
        buttonBox->setOrientation(Qt::Vertical);
        buttonBox->setStandardButtons(QDialogButtonBox::Cancel|QDialogButtonBox::Ok);
        lineEditSource = new QLineEdit(DialogTinSimplify);
        lineEditSource->setObjectName(QString::fromUtf8("lineEditSource"));
        lineEditSource->setGeometry(QRect(30, 120, 391, 27));
        lineEditDest = new QLineEdit(DialogTinSimplify);
        lineEditDest->setObjectName(QString::fromUtf8("lineEditDest"));
        lineEditDest->setGeometry(QRect(30, 180, 391, 27));
        label = new QLabel(DialogTinSimplify);
        label->setObjectName(QString::fromUtf8("label"));
        label->setGeometry(QRect(80, 90, 141, 17));
        pushButtonDest = new QPushButton(DialogTinSimplify);
        pushButtonDest->setObjectName(QString::fromUtf8("pushButtonDest"));
        pushButtonDest->setGeometry(QRect(470, 180, 41, 27));
        pushButtonSource = new QPushButton(DialogTinSimplify);
        pushButtonSource->setObjectName(QString::fromUtf8("pushButtonSource"));
        pushButtonSource->setGeometry(QRect(470, 120, 41, 27));
        label_2 = new QLabel(DialogTinSimplify);
        label_2->setObjectName(QString::fromUtf8("label_2"));
        label_2->setGeometry(QRect(80, 160, 141, 17));
        doubleSpinBoxSimplifyCoef = new QDoubleSpinBox(DialogTinSimplify);
        doubleSpinBoxSimplifyCoef->setObjectName(QString::fromUtf8("doubleSpinBoxSimplifyCoef"));
        doubleSpinBoxSimplifyCoef->setGeometry(QRect(70, 260, 62, 27));
        doubleSpinBoxSimplifyCoef->setValue(0.5);
        label_3 = new QLabel(DialogTinSimplify);
        label_3->setObjectName(QString::fromUtf8("label_3"));
        label_3->setGeometry(QRect(70, 230, 141, 17));

        retranslateUi(DialogTinSimplify);
        QObject::connect(buttonBox, SIGNAL(accepted()), DialogTinSimplify, SLOT(accept()));
        QObject::connect(buttonBox, SIGNAL(rejected()), DialogTinSimplify, SLOT(reject()));

        QMetaObject::connectSlotsByName(DialogTinSimplify);
    } // setupUi

    void retranslateUi(QDialog *DialogTinSimplify)
    {
        DialogTinSimplify->setWindowTitle(QApplication::translate("DialogTinSimplify", "Dialog", 0, QApplication::UnicodeUTF8));
        label->setText(QApplication::translate("DialogTinSimplify", "\350\276\223\345\205\245\346\226\207\344\273\266\350\267\257\345\276\204", 0, QApplication::UnicodeUTF8));
        pushButtonDest->setText(QApplication::translate("DialogTinSimplify", "...", 0, QApplication::UnicodeUTF8));
        pushButtonSource->setText(QApplication::translate("DialogTinSimplify", "...", 0, QApplication::UnicodeUTF8));
        label_2->setText(QApplication::translate("DialogTinSimplify", "\350\276\223\345\207\272\346\226\207\344\273\266\350\267\257\345\276\204", 0, QApplication::UnicodeUTF8));
        label_3->setText(QApplication::translate("DialogTinSimplify", "\347\256\200\345\214\226\347\263\273\346\225\260", 0, QApplication::UnicodeUTF8));
    } // retranslateUi

};

namespace Ui {
    class DialogTinSimplify: public Ui_DialogTinSimplify {};
} // namespace Ui

QT_END_NAMESPACE

#endif // UI_DIALOGTINSIMPLIFY_H
