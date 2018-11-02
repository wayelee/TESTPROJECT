/********************************************************************************
** Form generated from reading UI file 'dialoglandlocatematch.ui'
**
** Created: Wed Feb 8 17:53:33 2012
**      by: Qt User Interface Compiler version 4.7.2
**
** WARNING! All changes made in this file will be lost when recompiling UI file!
********************************************************************************/

#ifndef UI_DIALOGLANDLOCATEMATCH_H
#define UI_DIALOGLANDLOCATEMATCH_H

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

class Ui_DialogLandLocateMatch
{
public:
    QDialogButtonBox *buttonBox;
    QLineEdit *lineEditLandDOMSource;
    QLabel *label_2;
    QLabel *label;
    QLineEdit *lineEditDest;
    QPushButton *pushButtonLandDOMSource;
    QPushButton *pushButtonSatelliteDOMSource;
    QLineEdit *lineEditSatelliteDOMSource;
    QPushButton *pushButtonDest;
    QLabel *label_3;

    void setupUi(QDialog *DialogLandLocateMatch)
    {
        if (DialogLandLocateMatch->objectName().isEmpty())
            DialogLandLocateMatch->setObjectName(QString::fromUtf8("DialogLandLocateMatch"));
        DialogLandLocateMatch->resize(587, 369);
        buttonBox = new QDialogButtonBox(DialogLandLocateMatch);
        buttonBox->setObjectName(QString::fromUtf8("buttonBox"));
        buttonBox->setGeometry(QRect(470, 40, 81, 241));
        buttonBox->setOrientation(Qt::Vertical);
        buttonBox->setStandardButtons(QDialogButtonBox::Cancel|QDialogButtonBox::Ok);
        lineEditLandDOMSource = new QLineEdit(DialogLandLocateMatch);
        lineEditLandDOMSource->setObjectName(QString::fromUtf8("lineEditLandDOMSource"));
        lineEditLandDOMSource->setGeometry(QRect(30, 120, 391, 27));
        label_2 = new QLabel(DialogLandLocateMatch);
        label_2->setObjectName(QString::fromUtf8("label_2"));
        label_2->setGeometry(QRect(40, 230, 141, 17));
        label = new QLabel(DialogLandLocateMatch);
        label->setObjectName(QString::fromUtf8("label"));
        label->setGeometry(QRect(40, 90, 181, 17));
        lineEditDest = new QLineEdit(DialogLandLocateMatch);
        lineEditDest->setObjectName(QString::fromUtf8("lineEditDest"));
        lineEditDest->setGeometry(QRect(30, 250, 391, 27));
        pushButtonLandDOMSource = new QPushButton(DialogLandLocateMatch);
        pushButtonLandDOMSource->setObjectName(QString::fromUtf8("pushButtonLandDOMSource"));
        pushButtonLandDOMSource->setGeometry(QRect(470, 120, 41, 27));
        pushButtonSatelliteDOMSource = new QPushButton(DialogLandLocateMatch);
        pushButtonSatelliteDOMSource->setObjectName(QString::fromUtf8("pushButtonSatelliteDOMSource"));
        pushButtonSatelliteDOMSource->setGeometry(QRect(470, 190, 41, 27));
        lineEditSatelliteDOMSource = new QLineEdit(DialogLandLocateMatch);
        lineEditSatelliteDOMSource->setObjectName(QString::fromUtf8("lineEditSatelliteDOMSource"));
        lineEditSatelliteDOMSource->setGeometry(QRect(30, 190, 391, 27));
        pushButtonDest = new QPushButton(DialogLandLocateMatch);
        pushButtonDest->setObjectName(QString::fromUtf8("pushButtonDest"));
        pushButtonDest->setGeometry(QRect(470, 250, 41, 27));
        label_3 = new QLabel(DialogLandLocateMatch);
        label_3->setObjectName(QString::fromUtf8("label_3"));
        label_3->setGeometry(QRect(40, 160, 171, 17));

        retranslateUi(DialogLandLocateMatch);
        QObject::connect(buttonBox, SIGNAL(accepted()), DialogLandLocateMatch, SLOT(accept()));
        QObject::connect(buttonBox, SIGNAL(rejected()), DialogLandLocateMatch, SLOT(reject()));

        QMetaObject::connectSlotsByName(DialogLandLocateMatch);
    } // setupUi

    void retranslateUi(QDialog *DialogLandLocateMatch)
    {
        DialogLandLocateMatch->setWindowTitle(QApplication::translate("DialogLandLocateMatch", "Dialog", 0, QApplication::UnicodeUTF8));
        label_2->setText(QApplication::translate("DialogLandLocateMatch", "\350\276\223\345\207\272\346\226\207\344\273\266\350\267\257\345\276\204", 0, QApplication::UnicodeUTF8));
        label->setText(QApplication::translate("DialogLandLocateMatch", "\350\276\223\345\205\245\345\234\260\351\235\242DOM\346\226\207\344\273\266\350\267\257\345\276\204", 0, QApplication::UnicodeUTF8));
        pushButtonLandDOMSource->setText(QApplication::translate("DialogLandLocateMatch", "...", 0, QApplication::UnicodeUTF8));
        pushButtonSatelliteDOMSource->setText(QApplication::translate("DialogLandLocateMatch", "...", 0, QApplication::UnicodeUTF8));
        pushButtonDest->setText(QApplication::translate("DialogLandLocateMatch", "...", 0, QApplication::UnicodeUTF8));
        label_3->setText(QApplication::translate("DialogLandLocateMatch", "\350\276\223\345\205\245\345\215\253\346\230\237DOM\346\226\207\344\273\266\350\267\257\345\276\204", 0, QApplication::UnicodeUTF8));
    } // retranslateUi

};

namespace Ui {
    class DialogLandLocateMatch: public Ui_DialogLandLocateMatch {};
} // namespace Ui

QT_END_NAMESPACE

#endif // UI_DIALOGLANDLOCATEMATCH_H
