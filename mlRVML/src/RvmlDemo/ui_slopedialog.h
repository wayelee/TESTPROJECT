/********************************************************************************
** Form generated from reading UI file 'slopedialog.ui'
**
** Created: Thu Jan 5 11:16:12 2012
**      by: Qt User Interface Compiler version 4.7.2
**
** WARNING! All changes made in this file will be lost when recompiling UI file!
********************************************************************************/

#ifndef UI_SLOPEDIALOG_H
#define UI_SLOPEDIALOG_H

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

class Ui_SlopeDialog
{
public:
    QDialogButtonBox *buttonBox;
    QLabel *label_2;
    QLabel *label;
    QLabel *label_3;
    QPushButton *pushButtonSource;
    QPushButton *pushButtonDest;
    QLineEdit *lineEditSource;
    QLineEdit *lineEditDest;
    QDoubleSpinBox *doubleSpinBoxWindowSize;
    QDoubleSpinBox *doubleSpinBoxZfactor;
    QLabel *label_4;
    QLabel *label_5;

    void setupUi(QDialog *SlopeDialog)
    {
        if (SlopeDialog->objectName().isEmpty())
            SlopeDialog->setObjectName(QString::fromUtf8("SlopeDialog"));
        SlopeDialog->resize(591, 333);
        buttonBox = new QDialogButtonBox(SlopeDialog);
        buttonBox->setObjectName(QString::fromUtf8("buttonBox"));
        buttonBox->setGeometry(QRect(500, 20, 81, 241));
        buttonBox->setOrientation(Qt::Vertical);
        buttonBox->setStandardButtons(QDialogButtonBox::Cancel|QDialogButtonBox::Ok);
        label_2 = new QLabel(SlopeDialog);
        label_2->setObjectName(QString::fromUtf8("label_2"));
        label_2->setGeometry(QRect(70, 170, 141, 17));
        label = new QLabel(SlopeDialog);
        label->setObjectName(QString::fromUtf8("label"));
        label->setGeometry(QRect(70, 100, 141, 17));
        label_3 = new QLabel(SlopeDialog);
        label_3->setObjectName(QString::fromUtf8("label_3"));
        label_3->setGeometry(QRect(20, 70, 201, 21));
        pushButtonSource = new QPushButton(SlopeDialog);
        pushButtonSource->setObjectName(QString::fromUtf8("pushButtonSource"));
        pushButtonSource->setGeometry(QRect(460, 130, 41, 27));
        pushButtonDest = new QPushButton(SlopeDialog);
        pushButtonDest->setObjectName(QString::fromUtf8("pushButtonDest"));
        pushButtonDest->setGeometry(QRect(460, 190, 41, 27));
        lineEditSource = new QLineEdit(SlopeDialog);
        lineEditSource->setObjectName(QString::fromUtf8("lineEditSource"));
        lineEditSource->setGeometry(QRect(20, 130, 391, 27));
        lineEditDest = new QLineEdit(SlopeDialog);
        lineEditDest->setObjectName(QString::fromUtf8("lineEditDest"));
        lineEditDest->setGeometry(QRect(20, 190, 391, 27));
        doubleSpinBoxWindowSize = new QDoubleSpinBox(SlopeDialog);
        doubleSpinBoxWindowSize->setObjectName(QString::fromUtf8("doubleSpinBoxWindowSize"));
        doubleSpinBoxWindowSize->setGeometry(QRect(80, 260, 62, 27));
        doubleSpinBoxWindowSize->setDecimals(0);
        doubleSpinBoxWindowSize->setMinimum(3);
        doubleSpinBoxWindowSize->setMaximum(99);
        doubleSpinBoxWindowSize->setSingleStep(2);
        doubleSpinBoxWindowSize->setValue(3);
        doubleSpinBoxZfactor = new QDoubleSpinBox(SlopeDialog);
        doubleSpinBoxZfactor->setObjectName(QString::fromUtf8("doubleSpinBoxZfactor"));
        doubleSpinBoxZfactor->setGeometry(QRect(210, 260, 62, 27));
        doubleSpinBoxZfactor->setValue(1);
        label_4 = new QLabel(SlopeDialog);
        label_4->setObjectName(QString::fromUtf8("label_4"));
        label_4->setGeometry(QRect(200, 230, 141, 17));
        label_5 = new QLabel(SlopeDialog);
        label_5->setObjectName(QString::fromUtf8("label_5"));
        label_5->setGeometry(QRect(50, 230, 141, 17));

        retranslateUi(SlopeDialog);
        QObject::connect(buttonBox, SIGNAL(accepted()), SlopeDialog, SLOT(accept()));
        QObject::connect(buttonBox, SIGNAL(rejected()), SlopeDialog, SLOT(reject()));

        QMetaObject::connectSlotsByName(SlopeDialog);
    } // setupUi

    void retranslateUi(QDialog *SlopeDialog)
    {
        SlopeDialog->setWindowTitle(QApplication::translate("SlopeDialog", "Dialog", 0, QApplication::UnicodeUTF8));
        label_2->setText(QApplication::translate("SlopeDialog", "\350\276\223\345\207\272\346\226\207\344\273\266\350\267\257\345\276\204", 0, QApplication::UnicodeUTF8));
        label->setText(QApplication::translate("SlopeDialog", "\350\276\223\345\205\245\351\253\230\347\250\213\346\226\207\344\273\266\350\267\257\345\276\204", 0, QApplication::UnicodeUTF8));
        label_3->setText(QApplication::translate("SlopeDialog", "\345\235\241\345\272\246\345\235\241\345\220\221\350\256\241\347\256\227\345\217\202\346\225\260\350\256\276\347\275\256\345\257\271\350\257\235\346\241\206", 0, QApplication::UnicodeUTF8));
        pushButtonSource->setText(QApplication::translate("SlopeDialog", "...", 0, QApplication::UnicodeUTF8));
        pushButtonDest->setText(QApplication::translate("SlopeDialog", "...", 0, QApplication::UnicodeUTF8));
        label_4->setText(QApplication::translate("SlopeDialog", "\351\253\230\347\250\213\347\274\251\346\224\276\345\233\240\345\255\220", 0, QApplication::UnicodeUTF8));
        label_5->setText(QApplication::translate("SlopeDialog", "\350\256\241\347\256\227\347\252\227\345\217\243\345\244\247\345\260\217", 0, QApplication::UnicodeUTF8));
    } // retranslateUi

};

namespace Ui {
    class SlopeDialog: public Ui_SlopeDialog {};
} // namespace Ui

QT_END_NAMESPACE

#endif // UI_SLOPEDIALOG_H
