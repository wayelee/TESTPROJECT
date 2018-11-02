/********************************************************************************
** Form generated from reading UI file 'dialogpersimagecreate.ui'
**
** Created: Wed Feb 8 17:53:33 2012
**      by: Qt User Interface Compiler version 4.7.2
**
** WARNING! All changes made in this file will be lost when recompiling UI file!
********************************************************************************/

#ifndef UI_DIALOGPERSIMAGECREATE_H
#define UI_DIALOGPERSIMAGECREATE_H

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

class Ui_DialogPersImageCreate
{
public:
    QDialogButtonBox *buttonBox;
    QLineEdit *lineEditSource;
    QPushButton *pushButtonDest;
    QLineEdit *lineEditDest;
    QLabel *label;
    QPushButton *pushButtonSource;
    QLabel *label_2;
    QDoubleSpinBox *doubleSpinBoxLTX;
    QDoubleSpinBox *doubleSpinBoxRBX;
    QDoubleSpinBox *doubleSpinBoxLTY;
    QDoubleSpinBox *doubleSpinBoxRBY;
    QDoubleSpinBox *doubleSpinBoxFocus;
    QLabel *label_3;
    QLabel *label_4;
    QLabel *label_5;
    QLabel *label_6;
    QLabel *label_7;

    void setupUi(QDialog *DialogPersImageCreate)
    {
        if (DialogPersImageCreate->objectName().isEmpty())
            DialogPersImageCreate->setObjectName(QString::fromUtf8("DialogPersImageCreate"));
        DialogPersImageCreate->resize(533, 340);
        buttonBox = new QDialogButtonBox(DialogPersImageCreate);
        buttonBox->setObjectName(QString::fromUtf8("buttonBox"));
        buttonBox->setGeometry(QRect(440, 30, 81, 241));
        buttonBox->setOrientation(Qt::Vertical);
        buttonBox->setStandardButtons(QDialogButtonBox::Cancel|QDialogButtonBox::Ok);
        lineEditSource = new QLineEdit(DialogPersImageCreate);
        lineEditSource->setObjectName(QString::fromUtf8("lineEditSource"));
        lineEditSource->setGeometry(QRect(20, 120, 391, 27));
        pushButtonDest = new QPushButton(DialogPersImageCreate);
        pushButtonDest->setObjectName(QString::fromUtf8("pushButtonDest"));
        pushButtonDest->setGeometry(QRect(460, 180, 41, 27));
        lineEditDest = new QLineEdit(DialogPersImageCreate);
        lineEditDest->setObjectName(QString::fromUtf8("lineEditDest"));
        lineEditDest->setGeometry(QRect(20, 180, 391, 27));
        label = new QLabel(DialogPersImageCreate);
        label->setObjectName(QString::fromUtf8("label"));
        label->setGeometry(QRect(70, 90, 141, 17));
        pushButtonSource = new QPushButton(DialogPersImageCreate);
        pushButtonSource->setObjectName(QString::fromUtf8("pushButtonSource"));
        pushButtonSource->setGeometry(QRect(460, 120, 41, 27));
        label_2 = new QLabel(DialogPersImageCreate);
        label_2->setObjectName(QString::fromUtf8("label_2"));
        label_2->setGeometry(QRect(70, 160, 141, 17));
        doubleSpinBoxLTX = new QDoubleSpinBox(DialogPersImageCreate);
        doubleSpinBoxLTX->setObjectName(QString::fromUtf8("doubleSpinBoxLTX"));
        doubleSpinBoxLTX->setGeometry(QRect(40, 240, 61, 27));
        doubleSpinBoxRBX = new QDoubleSpinBox(DialogPersImageCreate);
        doubleSpinBoxRBX->setObjectName(QString::fromUtf8("doubleSpinBoxRBX"));
        doubleSpinBoxRBX->setGeometry(QRect(140, 240, 62, 27));
        doubleSpinBoxLTY = new QDoubleSpinBox(DialogPersImageCreate);
        doubleSpinBoxLTY->setObjectName(QString::fromUtf8("doubleSpinBoxLTY"));
        doubleSpinBoxLTY->setGeometry(QRect(40, 290, 62, 27));
        doubleSpinBoxRBY = new QDoubleSpinBox(DialogPersImageCreate);
        doubleSpinBoxRBY->setObjectName(QString::fromUtf8("doubleSpinBoxRBY"));
        doubleSpinBoxRBY->setGeometry(QRect(140, 290, 62, 27));
        doubleSpinBoxFocus = new QDoubleSpinBox(DialogPersImageCreate);
        doubleSpinBoxFocus->setObjectName(QString::fromUtf8("doubleSpinBoxFocus"));
        doubleSpinBoxFocus->setGeometry(QRect(270, 290, 62, 27));
        label_3 = new QLabel(DialogPersImageCreate);
        label_3->setObjectName(QString::fromUtf8("label_3"));
        label_3->setGeometry(QRect(40, 210, 67, 17));
        label_4 = new QLabel(DialogPersImageCreate);
        label_4->setObjectName(QString::fromUtf8("label_4"));
        label_4->setGeometry(QRect(130, 210, 67, 17));
        label_5 = new QLabel(DialogPersImageCreate);
        label_5->setObjectName(QString::fromUtf8("label_5"));
        label_5->setGeometry(QRect(40, 270, 67, 17));
        label_6 = new QLabel(DialogPersImageCreate);
        label_6->setObjectName(QString::fromUtf8("label_6"));
        label_6->setGeometry(QRect(130, 270, 67, 17));
        label_7 = new QLabel(DialogPersImageCreate);
        label_7->setObjectName(QString::fromUtf8("label_7"));
        label_7->setGeometry(QRect(270, 270, 67, 17));

        retranslateUi(DialogPersImageCreate);
        QObject::connect(buttonBox, SIGNAL(accepted()), DialogPersImageCreate, SLOT(accept()));
        QObject::connect(buttonBox, SIGNAL(rejected()), DialogPersImageCreate, SLOT(reject()));

        QMetaObject::connectSlotsByName(DialogPersImageCreate);
    } // setupUi

    void retranslateUi(QDialog *DialogPersImageCreate)
    {
        DialogPersImageCreate->setWindowTitle(QApplication::translate("DialogPersImageCreate", "Dialog", 0, QApplication::UnicodeUTF8));
        pushButtonDest->setText(QApplication::translate("DialogPersImageCreate", "...", 0, QApplication::UnicodeUTF8));
        label->setText(QApplication::translate("DialogPersImageCreate", "\350\276\223\345\205\245\346\226\207\344\273\266\350\267\257\345\276\204", 0, QApplication::UnicodeUTF8));
        pushButtonSource->setText(QApplication::translate("DialogPersImageCreate", "...", 0, QApplication::UnicodeUTF8));
        label_2->setText(QApplication::translate("DialogPersImageCreate", "\350\276\223\345\207\272\346\226\207\344\273\266\350\267\257\345\276\204", 0, QApplication::UnicodeUTF8));
        label_3->setText(QApplication::translate("DialogPersImageCreate", "\345\267\246\344\270\212\350\247\222X", 0, QApplication::UnicodeUTF8));
        label_4->setText(QApplication::translate("DialogPersImageCreate", "\345\217\263\344\270\213\350\247\222X", 0, QApplication::UnicodeUTF8));
        label_5->setText(QApplication::translate("DialogPersImageCreate", "\345\267\246\344\270\212\350\247\222Y", 0, QApplication::UnicodeUTF8));
        label_6->setText(QApplication::translate("DialogPersImageCreate", "\345\217\263\344\270\213\350\247\222Y", 0, QApplication::UnicodeUTF8));
        label_7->setText(QApplication::translate("DialogPersImageCreate", "\347\204\246\350\267\235", 0, QApplication::UnicodeUTF8));
    } // retranslateUi

};

namespace Ui {
    class DialogPersImageCreate: public Ui_DialogPersImageCreate {};
} // namespace Ui

QT_END_NAMESPACE

#endif // UI_DIALOGPERSIMAGECREATE_H
