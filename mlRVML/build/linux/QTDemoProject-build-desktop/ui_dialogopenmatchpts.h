/********************************************************************************
** Form generated from reading UI file 'dialogopenmatchpts.ui'
**
** Created: Wed Feb 8 19:08:39 2012
**      by: Qt User Interface Compiler version 4.7.2
**
** WARNING! All changes made in this file will be lost when recompiling UI file!
********************************************************************************/

#ifndef UI_DIALOGOPENMATCHPTS_H
#define UI_DIALOGOPENMATCHPTS_H

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

class Ui_DialogOpenMatchPts
{
public:
    QDialogButtonBox *buttonBox;
    QLabel *label;
    QLineEdit *lineEditSource;
    QPushButton *pushButtonSource;
    QLabel *label_2;
    QPushButton *pushButtonSource_2;
    QLineEdit *lineEditSource_2;

    void setupUi(QDialog *DialogOpenMatchPts)
    {
        if (DialogOpenMatchPts->objectName().isEmpty())
            DialogOpenMatchPts->setObjectName(QString::fromUtf8("DialogOpenMatchPts"));
        DialogOpenMatchPts->resize(532, 300);
        buttonBox = new QDialogButtonBox(DialogOpenMatchPts);
        buttonBox->setObjectName(QString::fromUtf8("buttonBox"));
        buttonBox->setGeometry(QRect(410, 10, 81, 241));
        buttonBox->setOrientation(Qt::Vertical);
        buttonBox->setStandardButtons(QDialogButtonBox::Cancel|QDialogButtonBox::Ok);
        label = new QLabel(DialogOpenMatchPts);
        label->setObjectName(QString::fromUtf8("label"));
        label->setGeometry(QRect(30, 90, 141, 17));
        lineEditSource = new QLineEdit(DialogOpenMatchPts);
        lineEditSource->setObjectName(QString::fromUtf8("lineEditSource"));
        lineEditSource->setGeometry(QRect(20, 120, 391, 27));
        pushButtonSource = new QPushButton(DialogOpenMatchPts);
        pushButtonSource->setObjectName(QString::fromUtf8("pushButtonSource"));
        pushButtonSource->setGeometry(QRect(460, 120, 41, 27));
        label_2 = new QLabel(DialogOpenMatchPts);
        label_2->setObjectName(QString::fromUtf8("label_2"));
        label_2->setGeometry(QRect(30, 160, 141, 17));
        pushButtonSource_2 = new QPushButton(DialogOpenMatchPts);
        pushButtonSource_2->setObjectName(QString::fromUtf8("pushButtonSource_2"));
        pushButtonSource_2->setGeometry(QRect(460, 190, 41, 27));
        lineEditSource_2 = new QLineEdit(DialogOpenMatchPts);
        lineEditSource_2->setObjectName(QString::fromUtf8("lineEditSource_2"));
        lineEditSource_2->setGeometry(QRect(20, 190, 391, 27));

        retranslateUi(DialogOpenMatchPts);
        QObject::connect(buttonBox, SIGNAL(accepted()), DialogOpenMatchPts, SLOT(accept()));
        QObject::connect(buttonBox, SIGNAL(rejected()), DialogOpenMatchPts, SLOT(reject()));

        QMetaObject::connectSlotsByName(DialogOpenMatchPts);
    } // setupUi

    void retranslateUi(QDialog *DialogOpenMatchPts)
    {
        DialogOpenMatchPts->setWindowTitle(QApplication::translate("DialogOpenMatchPts", "Dialog", 0, QApplication::UnicodeUTF8));
        label->setText(QApplication::translate("DialogOpenMatchPts", "\350\276\223\345\205\245\345\267\246\345\233\276\347\202\271\346\226\207\344\273\266\350\267\257\345\276\204", 0, QApplication::UnicodeUTF8));
        pushButtonSource->setText(QApplication::translate("DialogOpenMatchPts", "...", 0, QApplication::UnicodeUTF8));
        label_2->setText(QApplication::translate("DialogOpenMatchPts", "\350\276\223\345\205\245\345\217\263\345\233\276\347\202\271\346\226\207\344\273\266\350\267\257\345\276\204", 0, QApplication::UnicodeUTF8));
        pushButtonSource_2->setText(QApplication::translate("DialogOpenMatchPts", "...", 0, QApplication::UnicodeUTF8));
    } // retranslateUi

};

namespace Ui {
    class DialogOpenMatchPts: public Ui_DialogOpenMatchPts {};
} // namespace Ui

QT_END_NAMESPACE

#endif // UI_DIALOGOPENMATCHPTS_H
