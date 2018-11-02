/********************************************************************************
** Form generated from reading UI file 'dialoglandlocate.ui'
**
** Created: Wed Feb 8 17:53:33 2012
**      by: Qt User Interface Compiler version 4.7.2
**
** WARNING! All changes made in this file will be lost when recompiling UI file!
********************************************************************************/

#ifndef UI_DIALOGLANDLOCATE_H
#define UI_DIALOGLANDLOCATE_H

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

class Ui_DialogLandLocate
{
public:
    QDialogButtonBox *buttonBox;
    QLineEdit *lineEditDest;
    QLineEdit *lineEditProjSource;
    QPushButton *pushButtonProjSource;
    QLabel *label;
    QLabel *label_2;
    QPushButton *pushButtonDest;
    QLabel *label_3;
    QLineEdit *lineEditDOMSource;
    QPushButton *pushButtonDOMSource;

    void setupUi(QDialog *DialogLandLocate)
    {
        if (DialogLandLocate->objectName().isEmpty())
            DialogLandLocate->setObjectName(QString::fromUtf8("DialogLandLocate"));
        DialogLandLocate->resize(564, 397);
        buttonBox = new QDialogButtonBox(DialogLandLocate);
        buttonBox->setObjectName(QString::fromUtf8("buttonBox"));
        buttonBox->setGeometry(QRect(440, 30, 81, 241));
        buttonBox->setOrientation(Qt::Vertical);
        buttonBox->setStandardButtons(QDialogButtonBox::Cancel|QDialogButtonBox::Ok);
        lineEditDest = new QLineEdit(DialogLandLocate);
        lineEditDest->setObjectName(QString::fromUtf8("lineEditDest"));
        lineEditDest->setGeometry(QRect(30, 290, 391, 27));
        lineEditProjSource = new QLineEdit(DialogLandLocate);
        lineEditProjSource->setObjectName(QString::fromUtf8("lineEditProjSource"));
        lineEditProjSource->setGeometry(QRect(30, 160, 391, 27));
        pushButtonProjSource = new QPushButton(DialogLandLocate);
        pushButtonProjSource->setObjectName(QString::fromUtf8("pushButtonProjSource"));
        pushButtonProjSource->setGeometry(QRect(470, 160, 41, 27));
        label = new QLabel(DialogLandLocate);
        label->setObjectName(QString::fromUtf8("label"));
        label->setGeometry(QRect(40, 130, 141, 17));
        label_2 = new QLabel(DialogLandLocate);
        label_2->setObjectName(QString::fromUtf8("label_2"));
        label_2->setGeometry(QRect(40, 270, 141, 17));
        pushButtonDest = new QPushButton(DialogLandLocate);
        pushButtonDest->setObjectName(QString::fromUtf8("pushButtonDest"));
        pushButtonDest->setGeometry(QRect(470, 290, 41, 27));
        label_3 = new QLabel(DialogLandLocate);
        label_3->setObjectName(QString::fromUtf8("label_3"));
        label_3->setGeometry(QRect(40, 200, 141, 17));
        lineEditDOMSource = new QLineEdit(DialogLandLocate);
        lineEditDOMSource->setObjectName(QString::fromUtf8("lineEditDOMSource"));
        lineEditDOMSource->setGeometry(QRect(30, 230, 391, 27));
        pushButtonDOMSource = new QPushButton(DialogLandLocate);
        pushButtonDOMSource->setObjectName(QString::fromUtf8("pushButtonDOMSource"));
        pushButtonDOMSource->setGeometry(QRect(470, 230, 41, 27));

        retranslateUi(DialogLandLocate);
        QObject::connect(buttonBox, SIGNAL(accepted()), DialogLandLocate, SLOT(accept()));
        QObject::connect(buttonBox, SIGNAL(rejected()), DialogLandLocate, SLOT(reject()));

        QMetaObject::connectSlotsByName(DialogLandLocate);
    } // setupUi

    void retranslateUi(QDialog *DialogLandLocate)
    {
        DialogLandLocate->setWindowTitle(QApplication::translate("DialogLandLocate", "Dialog", 0, QApplication::UnicodeUTF8));
        pushButtonProjSource->setText(QApplication::translate("DialogLandLocate", "...", 0, QApplication::UnicodeUTF8));
        label->setText(QApplication::translate("DialogLandLocate", "\350\276\223\345\205\245Proj\346\226\207\344\273\266\350\267\257\345\276\204", 0, QApplication::UnicodeUTF8));
        label_2->setText(QApplication::translate("DialogLandLocate", "\350\276\223\345\207\272\346\226\207\344\273\266\350\267\257\345\276\204", 0, QApplication::UnicodeUTF8));
        pushButtonDest->setText(QApplication::translate("DialogLandLocate", "...", 0, QApplication::UnicodeUTF8));
        label_3->setText(QApplication::translate("DialogLandLocate", "\350\276\223\345\205\245DOM\346\226\207\344\273\266\350\267\257\345\276\204", 0, QApplication::UnicodeUTF8));
        pushButtonDOMSource->setText(QApplication::translate("DialogLandLocate", "...", 0, QApplication::UnicodeUTF8));
    } // retranslateUi

};

namespace Ui {
    class DialogLandLocate: public Ui_DialogLandLocate {};
} // namespace Ui

QT_END_NAMESPACE

#endif // UI_DIALOGLANDLOCATE_H
