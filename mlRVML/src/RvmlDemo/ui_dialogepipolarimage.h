/********************************************************************************
** Form generated from reading UI file 'dialogepipolarimage.ui'
**
** Created: Sun May 27 18:02:13 2012
**      by: Qt User Interface Compiler version 4.7.2
**
** WARNING! All changes made in this file will be lost when recompiling UI file!
********************************************************************************/

#ifndef UI_DIALOGEPIPOLARIMAGE_H
#define UI_DIALOGEPIPOLARIMAGE_H

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

class Ui_DialogEpipolarImage
{
public:
    QDialogButtonBox *buttonBox;
    QLabel *label;
    QLabel *label_2;
    QPushButton *pushButtonDOMSource;
    QPushButton *pushButtonProjSource;
    QLabel *label_3;
    QLineEdit *lineEditDest;
    QPushButton *pushButtonDest;
    QLineEdit *lineEditProjSource;
    QLineEdit *lineEditDOMSource;

    void setupUi(QDialog *DialogEpipolarImage)
    {
        if (DialogEpipolarImage->objectName().isEmpty())
            DialogEpipolarImage->setObjectName(QString::fromUtf8("DialogEpipolarImage"));
        DialogEpipolarImage->resize(567, 391);
        buttonBox = new QDialogButtonBox(DialogEpipolarImage);
        buttonBox->setObjectName(QString::fromUtf8("buttonBox"));
        buttonBox->setGeometry(QRect(460, 20, 81, 241));
        buttonBox->setOrientation(Qt::Vertical);
        buttonBox->setStandardButtons(QDialogButtonBox::Cancel|QDialogButtonBox::Ok);
        label = new QLabel(DialogEpipolarImage);
        label->setObjectName(QString::fromUtf8("label"));
        label->setGeometry(QRect(40, 100, 141, 17));
        label_2 = new QLabel(DialogEpipolarImage);
        label_2->setObjectName(QString::fromUtf8("label_2"));
        label_2->setGeometry(QRect(40, 240, 141, 17));
        pushButtonDOMSource = new QPushButton(DialogEpipolarImage);
        pushButtonDOMSource->setObjectName(QString::fromUtf8("pushButtonDOMSource"));
        pushButtonDOMSource->setGeometry(QRect(470, 200, 41, 27));
        pushButtonProjSource = new QPushButton(DialogEpipolarImage);
        pushButtonProjSource->setObjectName(QString::fromUtf8("pushButtonProjSource"));
        pushButtonProjSource->setGeometry(QRect(470, 130, 41, 27));
        label_3 = new QLabel(DialogEpipolarImage);
        label_3->setObjectName(QString::fromUtf8("label_3"));
        label_3->setGeometry(QRect(40, 170, 141, 17));
        lineEditDest = new QLineEdit(DialogEpipolarImage);
        lineEditDest->setObjectName(QString::fromUtf8("lineEditDest"));
        lineEditDest->setGeometry(QRect(30, 260, 391, 27));
        pushButtonDest = new QPushButton(DialogEpipolarImage);
        pushButtonDest->setObjectName(QString::fromUtf8("pushButtonDest"));
        pushButtonDest->setGeometry(QRect(470, 260, 41, 27));
        lineEditProjSource = new QLineEdit(DialogEpipolarImage);
        lineEditProjSource->setObjectName(QString::fromUtf8("lineEditProjSource"));
        lineEditProjSource->setGeometry(QRect(30, 130, 391, 27));
        lineEditDOMSource = new QLineEdit(DialogEpipolarImage);
        lineEditDOMSource->setObjectName(QString::fromUtf8("lineEditDOMSource"));
        lineEditDOMSource->setGeometry(QRect(30, 200, 391, 27));

        retranslateUi(DialogEpipolarImage);
        QObject::connect(buttonBox, SIGNAL(accepted()), DialogEpipolarImage, SLOT(accept()));
        QObject::connect(buttonBox, SIGNAL(rejected()), DialogEpipolarImage, SLOT(reject()));

        QMetaObject::connectSlotsByName(DialogEpipolarImage);
    } // setupUi

    void retranslateUi(QDialog *DialogEpipolarImage)
    {
        DialogEpipolarImage->setWindowTitle(QApplication::translate("DialogEpipolarImage", "EpipolarImage", 0, QApplication::UnicodeUTF8));
        label->setText(QApplication::translate("DialogEpipolarImage", "\350\276\223\345\205\245Proj\346\226\207\344\273\266\350\267\257\345\276\204", 0, QApplication::UnicodeUTF8));
        label_2->setText(QApplication::translate("DialogEpipolarImage", "\350\276\223\345\207\272\346\226\207\344\273\266\350\267\257\345\276\204", 0, QApplication::UnicodeUTF8));
        pushButtonDOMSource->setText(QApplication::translate("DialogEpipolarImage", "...", 0, QApplication::UnicodeUTF8));
        pushButtonProjSource->setText(QApplication::translate("DialogEpipolarImage", "...", 0, QApplication::UnicodeUTF8));
        label_3->setText(QApplication::translate("DialogEpipolarImage", "\347\233\270\345\257\271\345\256\232\345\220\221\345\217\202\346\225\260\350\267\257\345\276\204", 0, QApplication::UnicodeUTF8));
        pushButtonDest->setText(QApplication::translate("DialogEpipolarImage", "...", 0, QApplication::UnicodeUTF8));
    } // retranslateUi

};

namespace Ui {
    class DialogEpipolarImage: public Ui_DialogEpipolarImage {};
} // namespace Ui

QT_END_NAMESPACE

#endif // UI_DIALOGEPIPOLARIMAGE_H
