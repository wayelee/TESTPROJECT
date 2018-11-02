/********************************************************************************
** Form generated from reading UI file 'dialogdensematch.ui'
**
** Created: Wed May 9 21:41:51 2012
**      by: Qt User Interface Compiler version 4.7.2
**
** WARNING! All changes made in this file will be lost when recompiling UI file!
********************************************************************************/

#ifndef UI_DIALOGDENSEMATCH_H
#define UI_DIALOGDENSEMATCH_H

#include <QtCore/QVariant>
#include <QtGui/QAction>
#include <QtGui/QApplication>
#include <QtGui/QButtonGroup>
#include <QtGui/QDialog>
#include <QtGui/QDialogButtonBox>
#include <QtGui/QHeaderView>
#include <QtGui/QLabel>
#include <QtGui/QLineEdit>
#include <QtGui/QPushButton>
#include <QtGui/QRadioButton>
#include <QtGui/QSpinBox>

QT_BEGIN_NAMESPACE

class Ui_DialogDenseMatch
{
public:
    QDialogButtonBox *buttonBox;
    QLineEdit *lineEditDest;
    QLabel *label;
    QPushButton *pushButtonDest;
    QLineEdit *lineEditSource;
    QPushButton *pushButtonSource;
    QLabel *label_2;
    QRadioButton *radioButtonLocal;
    QRadioButton *radioButtonGlobal;
    QLabel *label_3;
    QLineEdit *lineEditParaSource;
    QPushButton *pushButtonParaSource;
    QSpinBox *spinBox;
    QLabel *label_4;

    void setupUi(QDialog *DialogDenseMatch)
    {
        if (DialogDenseMatch->objectName().isEmpty())
            DialogDenseMatch->setObjectName(QString::fromUtf8("DialogDenseMatch"));
        DialogDenseMatch->resize(554, 361);
        buttonBox = new QDialogButtonBox(DialogDenseMatch);
        buttonBox->setObjectName(QString::fromUtf8("buttonBox"));
        buttonBox->setGeometry(QRect(430, 30, 81, 241));
        buttonBox->setOrientation(Qt::Vertical);
        buttonBox->setStandardButtons(QDialogButtonBox::Cancel|QDialogButtonBox::Ok);
        lineEditDest = new QLineEdit(DialogDenseMatch);
        lineEditDest->setObjectName(QString::fromUtf8("lineEditDest"));
        lineEditDest->setGeometry(QRect(20, 300, 391, 27));
        label = new QLabel(DialogDenseMatch);
        label->setObjectName(QString::fromUtf8("label"));
        label->setGeometry(QRect(30, 130, 141, 17));
        pushButtonDest = new QPushButton(DialogDenseMatch);
        pushButtonDest->setObjectName(QString::fromUtf8("pushButtonDest"));
        pushButtonDest->setGeometry(QRect(460, 300, 41, 27));
        lineEditSource = new QLineEdit(DialogDenseMatch);
        lineEditSource->setObjectName(QString::fromUtf8("lineEditSource"));
        lineEditSource->setGeometry(QRect(20, 160, 391, 27));
        pushButtonSource = new QPushButton(DialogDenseMatch);
        pushButtonSource->setObjectName(QString::fromUtf8("pushButtonSource"));
        pushButtonSource->setGeometry(QRect(460, 160, 41, 27));
        label_2 = new QLabel(DialogDenseMatch);
        label_2->setObjectName(QString::fromUtf8("label_2"));
        label_2->setGeometry(QRect(30, 280, 141, 17));
        radioButtonLocal = new QRadioButton(DialogDenseMatch);
        radioButtonLocal->setObjectName(QString::fromUtf8("radioButtonLocal"));
        radioButtonLocal->setGeometry(QRect(290, 190, 116, 22));
        radioButtonLocal->setChecked(false);
        radioButtonGlobal = new QRadioButton(DialogDenseMatch);
        radioButtonGlobal->setObjectName(QString::fromUtf8("radioButtonGlobal"));
        radioButtonGlobal->setGeometry(QRect(140, 190, 121, 22));
        radioButtonGlobal->setChecked(true);
        label_3 = new QLabel(DialogDenseMatch);
        label_3->setObjectName(QString::fromUtf8("label_3"));
        label_3->setGeometry(QRect(30, 70, 141, 17));
        lineEditParaSource = new QLineEdit(DialogDenseMatch);
        lineEditParaSource->setObjectName(QString::fromUtf8("lineEditParaSource"));
        lineEditParaSource->setGeometry(QRect(20, 100, 391, 27));
        pushButtonParaSource = new QPushButton(DialogDenseMatch);
        pushButtonParaSource->setObjectName(QString::fromUtf8("pushButtonParaSource"));
        pushButtonParaSource->setGeometry(QRect(460, 100, 41, 27));
        spinBox = new QSpinBox(DialogDenseMatch);
        spinBox->setObjectName(QString::fromUtf8("spinBox"));
        spinBox->setGeometry(QRect(30, 210, 59, 27));
        spinBox->setMinimum(1);
        label_4 = new QLabel(DialogDenseMatch);
        label_4->setObjectName(QString::fromUtf8("label_4"));
        label_4->setGeometry(QRect(30, 190, 71, 17));

        retranslateUi(DialogDenseMatch);
        QObject::connect(buttonBox, SIGNAL(accepted()), DialogDenseMatch, SLOT(accept()));
        QObject::connect(buttonBox, SIGNAL(rejected()), DialogDenseMatch, SLOT(reject()));

        QMetaObject::connectSlotsByName(DialogDenseMatch);
    } // setupUi

    void retranslateUi(QDialog *DialogDenseMatch)
    {
        DialogDenseMatch->setWindowTitle(QApplication::translate("DialogDenseMatch", "Dialog", 0, QApplication::UnicodeUTF8));
        label->setText(QApplication::translate("DialogDenseMatch", "\350\276\223\345\205\245\345\267\245\347\250\213\346\226\207\344\273\266\350\267\257\345\276\204", 0, QApplication::UnicodeUTF8));
        pushButtonDest->setText(QApplication::translate("DialogDenseMatch", "...", 0, QApplication::UnicodeUTF8));
        pushButtonSource->setText(QApplication::translate("DialogDenseMatch", "...", 0, QApplication::UnicodeUTF8));
        label_2->setText(QApplication::translate("DialogDenseMatch", "\350\276\223\345\207\272\346\226\207\344\273\266\350\267\257\345\276\204", 0, QApplication::UnicodeUTF8));
        radioButtonLocal->setText(QApplication::translate("DialogDenseMatch", "\345\261\200\351\203\250\345\257\206\351\233\206\345\214\271\351\205\215", 0, QApplication::UnicodeUTF8));
        radioButtonGlobal->setText(QApplication::translate("DialogDenseMatch", "\345\205\250\345\261\200\345\257\206\351\233\206\345\214\271\351\205\215", 0, QApplication::UnicodeUTF8));
        label_3->setText(QApplication::translate("DialogDenseMatch", "\350\276\223\345\205\245\345\217\202\346\225\260\346\226\207\344\273\266\350\267\257\345\276\204", 0, QApplication::UnicodeUTF8));
        pushButtonParaSource->setText(QApplication::translate("DialogDenseMatch", "...", 0, QApplication::UnicodeUTF8));
        label_4->setText(QApplication::translate("DialogDenseMatch", "\345\203\217\347\202\271\345\272\217\345\217\267", 0, QApplication::UnicodeUTF8));
    } // retranslateUi

};

namespace Ui {
    class DialogDenseMatch: public Ui_DialogDenseMatch {};
} // namespace Ui

QT_END_NAMESPACE

#endif // UI_DIALOGDENSEMATCH_H
