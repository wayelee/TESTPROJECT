/********************************************************************************
** Form generated from reading UI file 'dialogroverlocatelander.ui'
**
** Created: Wed Feb 8 17:53:33 2012
**      by: Qt User Interface Compiler version 4.7.2
**
** WARNING! All changes made in this file will be lost when recompiling UI file!
********************************************************************************/

#ifndef UI_DIALOGROVERLOCATELANDER_H
#define UI_DIALOGROVERLOCATELANDER_H

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

class Ui_DialogRoverLocateLander
{
public:
    QDialogButtonBox *buttonBox;
    QPushButton *pushButtonDest;
    QPushButton *pushButtonProjSource;
    QLineEdit *lineEditDest;
    QLineEdit *lineEditCoordSource;
    QPushButton *pushButtonCoordSource;
    QLabel *label_3;
    QLabel *label;
    QLineEdit *lineEditProjSource;
    QLabel *label_2;

    void setupUi(QDialog *DialogRoverLocateLander)
    {
        if (DialogRoverLocateLander->objectName().isEmpty())
            DialogRoverLocateLander->setObjectName(QString::fromUtf8("DialogRoverLocateLander"));
        DialogRoverLocateLander->resize(578, 300);
        buttonBox = new QDialogButtonBox(DialogRoverLocateLander);
        buttonBox->setObjectName(QString::fromUtf8("buttonBox"));
        buttonBox->setGeometry(QRect(470, 10, 81, 241));
        buttonBox->setOrientation(Qt::Vertical);
        buttonBox->setStandardButtons(QDialogButtonBox::Cancel|QDialogButtonBox::Ok);
        pushButtonDest = new QPushButton(DialogRoverLocateLander);
        pushButtonDest->setObjectName(QString::fromUtf8("pushButtonDest"));
        pushButtonDest->setGeometry(QRect(480, 220, 41, 27));
        pushButtonProjSource = new QPushButton(DialogRoverLocateLander);
        pushButtonProjSource->setObjectName(QString::fromUtf8("pushButtonProjSource"));
        pushButtonProjSource->setGeometry(QRect(480, 90, 41, 27));
        lineEditDest = new QLineEdit(DialogRoverLocateLander);
        lineEditDest->setObjectName(QString::fromUtf8("lineEditDest"));
        lineEditDest->setGeometry(QRect(40, 220, 391, 27));
        lineEditCoordSource = new QLineEdit(DialogRoverLocateLander);
        lineEditCoordSource->setObjectName(QString::fromUtf8("lineEditCoordSource"));
        lineEditCoordSource->setGeometry(QRect(40, 160, 391, 27));
        pushButtonCoordSource = new QPushButton(DialogRoverLocateLander);
        pushButtonCoordSource->setObjectName(QString::fromUtf8("pushButtonCoordSource"));
        pushButtonCoordSource->setGeometry(QRect(480, 160, 41, 27));
        label_3 = new QLabel(DialogRoverLocateLander);
        label_3->setObjectName(QString::fromUtf8("label_3"));
        label_3->setGeometry(QRect(50, 130, 191, 17));
        label = new QLabel(DialogRoverLocateLander);
        label->setObjectName(QString::fromUtf8("label"));
        label->setGeometry(QRect(50, 60, 141, 17));
        lineEditProjSource = new QLineEdit(DialogRoverLocateLander);
        lineEditProjSource->setObjectName(QString::fromUtf8("lineEditProjSource"));
        lineEditProjSource->setGeometry(QRect(40, 90, 391, 27));
        label_2 = new QLabel(DialogRoverLocateLander);
        label_2->setObjectName(QString::fromUtf8("label_2"));
        label_2->setGeometry(QRect(50, 200, 141, 17));

        retranslateUi(DialogRoverLocateLander);
        QObject::connect(buttonBox, SIGNAL(accepted()), DialogRoverLocateLander, SLOT(accept()));
        QObject::connect(buttonBox, SIGNAL(rejected()), DialogRoverLocateLander, SLOT(reject()));

        QMetaObject::connectSlotsByName(DialogRoverLocateLander);
    } // setupUi

    void retranslateUi(QDialog *DialogRoverLocateLander)
    {
        DialogRoverLocateLander->setWindowTitle(QApplication::translate("DialogRoverLocateLander", "Dialog", 0, QApplication::UnicodeUTF8));
        pushButtonDest->setText(QApplication::translate("DialogRoverLocateLander", "...", 0, QApplication::UnicodeUTF8));
        pushButtonProjSource->setText(QApplication::translate("DialogRoverLocateLander", "...", 0, QApplication::UnicodeUTF8));
        pushButtonCoordSource->setText(QApplication::translate("DialogRoverLocateLander", "...", 0, QApplication::UnicodeUTF8));
        label_3->setText(QApplication::translate("DialogRoverLocateLander", "\350\276\223\345\205\245\346\240\207\350\257\206\347\202\271\345\235\220\346\240\207\346\226\207\344\273\266\350\267\257\345\276\204", 0, QApplication::UnicodeUTF8));
        label->setText(QApplication::translate("DialogRoverLocateLander", "\350\276\223\345\205\245Proj\346\226\207\344\273\266\350\267\257\345\276\204", 0, QApplication::UnicodeUTF8));
        label_2->setText(QApplication::translate("DialogRoverLocateLander", "\350\276\223\345\207\272\346\226\207\344\273\266\350\267\257\345\276\204", 0, QApplication::UnicodeUTF8));
    } // retranslateUi

};

namespace Ui {
    class DialogRoverLocateLander: public Ui_DialogRoverLocateLander {};
} // namespace Ui

QT_END_NAMESPACE

#endif // UI_DIALOGROVERLOCATELANDER_H
