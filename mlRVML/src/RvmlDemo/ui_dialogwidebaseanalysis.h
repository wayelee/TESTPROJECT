/********************************************************************************
** Form generated from reading UI file 'dialogwidebaseanalysis.ui'
**
** Created: Sat Jan 7 17:17:17 2012
**      by: Qt User Interface Compiler version 4.7.2
**
** WARNING! All changes made in this file will be lost when recompiling UI file!
********************************************************************************/

#ifndef UI_DIALOGWIDEBASEANALYSIS_H
#define UI_DIALOGWIDEBASEANALYSIS_H

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

QT_BEGIN_NAMESPACE

class Ui_DialogWideBaseAnalysis
{
public:
    QDialogButtonBox *buttonBox;
    QLineEdit *lineEditSource;
    QLineEdit *lineEditDest;
    QLabel *label;
    QPushButton *pushButtonDest;
    QPushButton *pushButtonSource;
    QLabel *label_2;

    void setupUi(QDialog *DialogWideBaseAnalysis)
    {
        if (DialogWideBaseAnalysis->objectName().isEmpty())
            DialogWideBaseAnalysis->setObjectName(QString::fromUtf8("DialogWideBaseAnalysis"));
        DialogWideBaseAnalysis->resize(623, 300);
        buttonBox = new QDialogButtonBox(DialogWideBaseAnalysis);
        buttonBox->setObjectName(QString::fromUtf8("buttonBox"));
        buttonBox->setGeometry(QRect(530, 40, 81, 241));
        buttonBox->setOrientation(Qt::Vertical);
        buttonBox->setStandardButtons(QDialogButtonBox::Cancel|QDialogButtonBox::Ok);
        lineEditSource = new QLineEdit(DialogWideBaseAnalysis);
        lineEditSource->setObjectName(QString::fromUtf8("lineEditSource"));
        lineEditSource->setGeometry(QRect(40, 100, 391, 27));
        lineEditDest = new QLineEdit(DialogWideBaseAnalysis);
        lineEditDest->setObjectName(QString::fromUtf8("lineEditDest"));
        lineEditDest->setGeometry(QRect(40, 160, 391, 27));
        label = new QLabel(DialogWideBaseAnalysis);
        label->setObjectName(QString::fromUtf8("label"));
        label->setGeometry(QRect(90, 70, 141, 17));
        pushButtonDest = new QPushButton(DialogWideBaseAnalysis);
        pushButtonDest->setObjectName(QString::fromUtf8("pushButtonDest"));
        pushButtonDest->setGeometry(QRect(480, 160, 41, 27));
        pushButtonSource = new QPushButton(DialogWideBaseAnalysis);
        pushButtonSource->setObjectName(QString::fromUtf8("pushButtonSource"));
        pushButtonSource->setGeometry(QRect(480, 100, 41, 27));
        label_2 = new QLabel(DialogWideBaseAnalysis);
        label_2->setObjectName(QString::fromUtf8("label_2"));
        label_2->setGeometry(QRect(90, 140, 141, 17));

        retranslateUi(DialogWideBaseAnalysis);
        QObject::connect(buttonBox, SIGNAL(accepted()), DialogWideBaseAnalysis, SLOT(accept()));
        QObject::connect(buttonBox, SIGNAL(rejected()), DialogWideBaseAnalysis, SLOT(reject()));

        QMetaObject::connectSlotsByName(DialogWideBaseAnalysis);
    } // setupUi

    void retranslateUi(QDialog *DialogWideBaseAnalysis)
    {
        DialogWideBaseAnalysis->setWindowTitle(QApplication::translate("DialogWideBaseAnalysis", "Dialog", 0, QApplication::UnicodeUTF8));
        label->setText(QApplication::translate("DialogWideBaseAnalysis", "\350\276\223\345\205\245\346\226\207\344\273\266\350\267\257\345\276\204", 0, QApplication::UnicodeUTF8));
        pushButtonDest->setText(QApplication::translate("DialogWideBaseAnalysis", "...", 0, QApplication::UnicodeUTF8));
        pushButtonSource->setText(QApplication::translate("DialogWideBaseAnalysis", "...", 0, QApplication::UnicodeUTF8));
        label_2->setText(QApplication::translate("DialogWideBaseAnalysis", "\350\276\223\345\207\272\346\226\207\344\273\266\350\267\257\345\276\204", 0, QApplication::UnicodeUTF8));
    } // retranslateUi

};

namespace Ui {
    class DialogWideBaseAnalysis: public Ui_DialogWideBaseAnalysis {};
} // namespace Ui

QT_END_NAMESPACE

#endif // UI_DIALOGWIDEBASEANALYSIS_H
