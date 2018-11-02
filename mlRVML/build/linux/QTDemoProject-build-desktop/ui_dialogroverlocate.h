/********************************************************************************
** Form generated from reading UI file 'dialogroverlocate.ui'
**
** Created: Wed Feb 8 17:53:33 2012
**      by: Qt User Interface Compiler version 4.7.2
**
** WARNING! All changes made in this file will be lost when recompiling UI file!
********************************************************************************/

#ifndef UI_DIALOGROVERLOCATE_H
#define UI_DIALOGROVERLOCATE_H

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

class Ui_DialogRoverLocate
{
public:
    QDialogButtonBox *buttonBox;
    QLabel *label;
    QLineEdit *lineEditSource;
    QLabel *label_2;
    QLineEdit *lineEditDest;
    QPushButton *pushButtonSource;
    QPushButton *pushButtonDest;

    void setupUi(QDialog *DialogRoverLocate)
    {
        if (DialogRoverLocate->objectName().isEmpty())
            DialogRoverLocate->setObjectName(QString::fromUtf8("DialogRoverLocate"));
        DialogRoverLocate->resize(591, 268);
        buttonBox = new QDialogButtonBox(DialogRoverLocate);
        buttonBox->setObjectName(QString::fromUtf8("buttonBox"));
        buttonBox->setGeometry(QRect(460, 20, 81, 241));
        buttonBox->setOrientation(Qt::Vertical);
        buttonBox->setStandardButtons(QDialogButtonBox::Cancel|QDialogButtonBox::Ok);
        label = new QLabel(DialogRoverLocate);
        label->setObjectName(QString::fromUtf8("label"));
        label->setGeometry(QRect(30, 70, 141, 17));
        lineEditSource = new QLineEdit(DialogRoverLocate);
        lineEditSource->setObjectName(QString::fromUtf8("lineEditSource"));
        lineEditSource->setGeometry(QRect(20, 100, 391, 27));
        label_2 = new QLabel(DialogRoverLocate);
        label_2->setObjectName(QString::fromUtf8("label_2"));
        label_2->setGeometry(QRect(30, 140, 141, 17));
        lineEditDest = new QLineEdit(DialogRoverLocate);
        lineEditDest->setObjectName(QString::fromUtf8("lineEditDest"));
        lineEditDest->setGeometry(QRect(20, 160, 391, 27));
        pushButtonSource = new QPushButton(DialogRoverLocate);
        pushButtonSource->setObjectName(QString::fromUtf8("pushButtonSource"));
        pushButtonSource->setGeometry(QRect(460, 100, 41, 27));
        pushButtonDest = new QPushButton(DialogRoverLocate);
        pushButtonDest->setObjectName(QString::fromUtf8("pushButtonDest"));
        pushButtonDest->setGeometry(QRect(460, 160, 41, 27));

        retranslateUi(DialogRoverLocate);
        QObject::connect(buttonBox, SIGNAL(accepted()), DialogRoverLocate, SLOT(accept()));
        QObject::connect(buttonBox, SIGNAL(rejected()), DialogRoverLocate, SLOT(reject()));

        QMetaObject::connectSlotsByName(DialogRoverLocate);
    } // setupUi

    void retranslateUi(QDialog *DialogRoverLocate)
    {
        DialogRoverLocate->setWindowTitle(QApplication::translate("DialogRoverLocate", "Dialog", 0, QApplication::UnicodeUTF8));
        label->setText(QApplication::translate("DialogRoverLocate", "\350\276\223\345\205\245\346\226\207\344\273\266\350\267\257\345\276\204", 0, QApplication::UnicodeUTF8));
        label_2->setText(QApplication::translate("DialogRoverLocate", "\350\276\223\345\207\272\346\226\207\344\273\266\350\267\257\345\276\204", 0, QApplication::UnicodeUTF8));
        pushButtonSource->setText(QApplication::translate("DialogRoverLocate", "...", 0, QApplication::UnicodeUTF8));
        pushButtonDest->setText(QApplication::translate("DialogRoverLocate", "...", 0, QApplication::UnicodeUTF8));
    } // retranslateUi

};

namespace Ui {
    class DialogRoverLocate: public Ui_DialogRoverLocate {};
} // namespace Ui

QT_END_NAMESPACE

#endif // UI_DIALOGROVERLOCATE_H
