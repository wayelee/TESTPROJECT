/********************************************************************************
** Form generated from reading UI file 'dialoglandlocateinter.ui'
**
** Created: Wed Feb 8 17:53:33 2012
**      by: Qt User Interface Compiler version 4.7.2
**
** WARNING! All changes made in this file will be lost when recompiling UI file!
********************************************************************************/

#ifndef UI_DIALOGLANDLOCATEINTER_H
#define UI_DIALOGLANDLOCATEINTER_H

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

class Ui_DialogLandLocateInter
{
public:
    QDialogButtonBox *buttonBox;
    QLineEdit *lineEditProjSource;
    QLabel *label_2;
    QLabel *label;
    QLineEdit *lineEditDest;
    QPushButton *pushButtonProjSource;
    QPushButton *pushButtonCoordSource;
    QLineEdit *lineEditCoordSource;
    QPushButton *pushButtonDest;
    QLabel *label_3;

    void setupUi(QDialog *DialogLandLocateInter)
    {
        if (DialogLandLocateInter->objectName().isEmpty())
            DialogLandLocateInter->setObjectName(QString::fromUtf8("DialogLandLocateInter"));
        DialogLandLocateInter->resize(529, 324);
        buttonBox = new QDialogButtonBox(DialogLandLocateInter);
        buttonBox->setObjectName(QString::fromUtf8("buttonBox"));
        buttonBox->setGeometry(QRect(440, 40, 81, 241));
        buttonBox->setOrientation(Qt::Vertical);
        buttonBox->setStandardButtons(QDialogButtonBox::Cancel|QDialogButtonBox::Ok);
        lineEditProjSource = new QLineEdit(DialogLandLocateInter);
        lineEditProjSource->setObjectName(QString::fromUtf8("lineEditProjSource"));
        lineEditProjSource->setGeometry(QRect(30, 120, 391, 27));
        label_2 = new QLabel(DialogLandLocateInter);
        label_2->setObjectName(QString::fromUtf8("label_2"));
        label_2->setGeometry(QRect(40, 230, 141, 17));
        label = new QLabel(DialogLandLocateInter);
        label->setObjectName(QString::fromUtf8("label"));
        label->setGeometry(QRect(40, 90, 141, 17));
        lineEditDest = new QLineEdit(DialogLandLocateInter);
        lineEditDest->setObjectName(QString::fromUtf8("lineEditDest"));
        lineEditDest->setGeometry(QRect(30, 250, 391, 27));
        pushButtonProjSource = new QPushButton(DialogLandLocateInter);
        pushButtonProjSource->setObjectName(QString::fromUtf8("pushButtonProjSource"));
        pushButtonProjSource->setGeometry(QRect(470, 120, 41, 27));
        pushButtonCoordSource = new QPushButton(DialogLandLocateInter);
        pushButtonCoordSource->setObjectName(QString::fromUtf8("pushButtonCoordSource"));
        pushButtonCoordSource->setGeometry(QRect(470, 190, 41, 27));
        lineEditCoordSource = new QLineEdit(DialogLandLocateInter);
        lineEditCoordSource->setObjectName(QString::fromUtf8("lineEditCoordSource"));
        lineEditCoordSource->setGeometry(QRect(30, 190, 391, 27));
        pushButtonDest = new QPushButton(DialogLandLocateInter);
        pushButtonDest->setObjectName(QString::fromUtf8("pushButtonDest"));
        pushButtonDest->setGeometry(QRect(470, 250, 41, 27));
        label_3 = new QLabel(DialogLandLocateInter);
        label_3->setObjectName(QString::fromUtf8("label_3"));
        label_3->setGeometry(QRect(40, 160, 191, 17));
        buttonBox->raise();
        pushButtonProjSource->raise();
        lineEditProjSource->raise();
        label_2->raise();
        label->raise();
        lineEditDest->raise();
        pushButtonCoordSource->raise();
        lineEditCoordSource->raise();
        pushButtonDest->raise();
        label_3->raise();

        retranslateUi(DialogLandLocateInter);
        QObject::connect(buttonBox, SIGNAL(accepted()), DialogLandLocateInter, SLOT(accept()));
        QObject::connect(buttonBox, SIGNAL(rejected()), DialogLandLocateInter, SLOT(reject()));

        QMetaObject::connectSlotsByName(DialogLandLocateInter);
    } // setupUi

    void retranslateUi(QDialog *DialogLandLocateInter)
    {
        DialogLandLocateInter->setWindowTitle(QApplication::translate("DialogLandLocateInter", "Dialog", 0, QApplication::UnicodeUTF8));
        label_2->setText(QApplication::translate("DialogLandLocateInter", "\350\276\223\345\207\272\346\226\207\344\273\266\350\267\257\345\276\204", 0, QApplication::UnicodeUTF8));
        label->setText(QApplication::translate("DialogLandLocateInter", "\350\276\223\345\205\245Proj\346\226\207\344\273\266\350\267\257\345\276\204", 0, QApplication::UnicodeUTF8));
        pushButtonProjSource->setText(QApplication::translate("DialogLandLocateInter", "...", 0, QApplication::UnicodeUTF8));
        pushButtonCoordSource->setText(QApplication::translate("DialogLandLocateInter", "...", 0, QApplication::UnicodeUTF8));
        pushButtonDest->setText(QApplication::translate("DialogLandLocateInter", "...", 0, QApplication::UnicodeUTF8));
        label_3->setText(QApplication::translate("DialogLandLocateInter", "\350\276\223\345\205\245\345\234\260\346\240\207\347\202\271\345\235\220\346\240\207\346\226\207\344\273\266\350\267\257\345\276\204", 0, QApplication::UnicodeUTF8));
    } // retranslateUi

};

namespace Ui {
    class DialogLandLocateInter: public Ui_DialogLandLocateInter {};
} // namespace Ui

QT_END_NAMESPACE

#endif // UI_DIALOGLANDLOCATEINTER_H
