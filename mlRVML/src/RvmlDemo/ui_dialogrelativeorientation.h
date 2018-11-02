/********************************************************************************
** Form generated from reading UI file 'dialogrelativeorientation.ui'
**
** Created: Sun May 27 18:27:59 2012
**      by: Qt User Interface Compiler version 4.7.2
**
** WARNING! All changes made in this file will be lost when recompiling UI file!
********************************************************************************/

#ifndef UI_DIALOGRELATIVEORIENTATION_H
#define UI_DIALOGRELATIVEORIENTATION_H

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

class Ui_DialogRelativeOrientation
{
public:
    QDialogButtonBox *buttonBox;
    QLineEdit *lineEditDOMSource;
    QPushButton *pushButtonProjSource;
    QLabel *label;
    QPushButton *pushButtonDOMSource;
    QLineEdit *lineEditProjSource;
    QLabel *label_2;
    QLineEdit *lineEditDest;
    QLabel *label_3;
    QPushButton *pushButtonDest;

    void setupUi(QDialog *DialogRelativeOrientation)
    {
        if (DialogRelativeOrientation->objectName().isEmpty())
            DialogRelativeOrientation->setObjectName(QString::fromUtf8("DialogRelativeOrientation"));
        DialogRelativeOrientation->resize(611, 357);
        buttonBox = new QDialogButtonBox(DialogRelativeOrientation);
        buttonBox->setObjectName(QString::fromUtf8("buttonBox"));
        buttonBox->setGeometry(QRect(480, 40, 81, 241));
        buttonBox->setOrientation(Qt::Vertical);
        buttonBox->setStandardButtons(QDialogButtonBox::Cancel|QDialogButtonBox::Ok);
        lineEditDOMSource = new QLineEdit(DialogRelativeOrientation);
        lineEditDOMSource->setObjectName(QString::fromUtf8("lineEditDOMSource"));
        lineEditDOMSource->setGeometry(QRect(50, 210, 391, 27));
        pushButtonProjSource = new QPushButton(DialogRelativeOrientation);
        pushButtonProjSource->setObjectName(QString::fromUtf8("pushButtonProjSource"));
        pushButtonProjSource->setGeometry(QRect(490, 140, 41, 27));
        label = new QLabel(DialogRelativeOrientation);
        label->setObjectName(QString::fromUtf8("label"));
        label->setGeometry(QRect(60, 110, 141, 16));
        pushButtonDOMSource = new QPushButton(DialogRelativeOrientation);
        pushButtonDOMSource->setObjectName(QString::fromUtf8("pushButtonDOMSource"));
        pushButtonDOMSource->setGeometry(QRect(490, 210, 41, 27));
        lineEditProjSource = new QLineEdit(DialogRelativeOrientation);
        lineEditProjSource->setObjectName(QString::fromUtf8("lineEditProjSource"));
        lineEditProjSource->setGeometry(QRect(50, 140, 391, 27));
        label_2 = new QLabel(DialogRelativeOrientation);
        label_2->setObjectName(QString::fromUtf8("label_2"));
        label_2->setGeometry(QRect(60, 250, 141, 17));
        lineEditDest = new QLineEdit(DialogRelativeOrientation);
        lineEditDest->setObjectName(QString::fromUtf8("lineEditDest"));
        lineEditDest->setGeometry(QRect(50, 270, 391, 27));
        label_3 = new QLabel(DialogRelativeOrientation);
        label_3->setObjectName(QString::fromUtf8("label_3"));
        label_3->setGeometry(QRect(60, 180, 141, 17));
        pushButtonDest = new QPushButton(DialogRelativeOrientation);
        pushButtonDest->setObjectName(QString::fromUtf8("pushButtonDest"));
        pushButtonDest->setGeometry(QRect(490, 270, 41, 27));

        retranslateUi(DialogRelativeOrientation);
        QObject::connect(buttonBox, SIGNAL(accepted()), DialogRelativeOrientation, SLOT(accept()));
        QObject::connect(buttonBox, SIGNAL(rejected()), DialogRelativeOrientation, SLOT(reject()));

        QMetaObject::connectSlotsByName(DialogRelativeOrientation);
    } // setupUi

    void retranslateUi(QDialog *DialogRelativeOrientation)
    {
        DialogRelativeOrientation->setWindowTitle(QApplication::translate("DialogRelativeOrientation", "Dialog", 0, QApplication::UnicodeUTF8));
        pushButtonProjSource->setText(QApplication::translate("DialogRelativeOrientation", "...", 0, QApplication::UnicodeUTF8));
        label->setText(QApplication::translate("DialogRelativeOrientation", " \345\267\246\345\275\261\345\203\217\347\202\271\346\226\207\344\273\266", 0, QApplication::UnicodeUTF8));
        pushButtonDOMSource->setText(QApplication::translate("DialogRelativeOrientation", "...", 0, QApplication::UnicodeUTF8));
        label_2->setText(QApplication::translate("DialogRelativeOrientation", "\350\276\223\345\207\272\346\226\207\344\273\266\350\267\257\345\276\204", 0, QApplication::UnicodeUTF8));
        label_3->setText(QApplication::translate("DialogRelativeOrientation", "\345\217\263\345\275\261\345\203\217\347\202\271\346\226\207\344\273\266", 0, QApplication::UnicodeUTF8));
        pushButtonDest->setText(QApplication::translate("DialogRelativeOrientation", "...", 0, QApplication::UnicodeUTF8));
    } // retranslateUi

};

namespace Ui {
    class DialogRelativeOrientation: public Ui_DialogRelativeOrientation {};
} // namespace Ui

QT_END_NAMESPACE

#endif // UI_DIALOGRELATIVEORIENTATION_H
