/********************************************************************************
** Form generated from reading UI file 'dialogvisualimage.ui'
**
** Created: Wed Feb 8 17:53:33 2012
**      by: Qt User Interface Compiler version 4.7.2
**
** WARNING! All changes made in this file will be lost when recompiling UI file!
********************************************************************************/

#ifndef UI_DIALOGVISUALIMAGE_H
#define UI_DIALOGVISUALIMAGE_H

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

class Ui_DialogVisualImage
{
public:
    QDialogButtonBox *buttonBox;
    QLineEdit *lineEditPara;
    QLineEdit *lineEditDEMSource;
    QLineEdit *lineEditDOMSource;
    QLineEdit *lineEditDest;
    QPushButton *pushButtonPara;
    QPushButton *pushButtonDEMSource;
    QPushButton *pushButtonDOMSource;
    QPushButton *pushButtonDest;
    QLabel *label;
    QLabel *label_2;
    QLabel *label_3;
    QLabel *label_4;

    void setupUi(QDialog *DialogVisualImage)
    {
        if (DialogVisualImage->objectName().isEmpty())
            DialogVisualImage->setObjectName(QString::fromUtf8("DialogVisualImage"));
        DialogVisualImage->resize(545, 300);
        buttonBox = new QDialogButtonBox(DialogVisualImage);
        buttonBox->setObjectName(QString::fromUtf8("buttonBox"));
        buttonBox->setGeometry(QRect(430, 20, 81, 241));
        buttonBox->setOrientation(Qt::Vertical);
        buttonBox->setStandardButtons(QDialogButtonBox::Cancel|QDialogButtonBox::Ok);
        lineEditPara = new QLineEdit(DialogVisualImage);
        lineEditPara->setObjectName(QString::fromUtf8("lineEditPara"));
        lineEditPara->setGeometry(QRect(30, 70, 301, 27));
        lineEditDEMSource = new QLineEdit(DialogVisualImage);
        lineEditDEMSource->setObjectName(QString::fromUtf8("lineEditDEMSource"));
        lineEditDEMSource->setGeometry(QRect(30, 120, 301, 27));
        lineEditDOMSource = new QLineEdit(DialogVisualImage);
        lineEditDOMSource->setObjectName(QString::fromUtf8("lineEditDOMSource"));
        lineEditDOMSource->setGeometry(QRect(30, 180, 301, 27));
        lineEditDest = new QLineEdit(DialogVisualImage);
        lineEditDest->setObjectName(QString::fromUtf8("lineEditDest"));
        lineEditDest->setGeometry(QRect(30, 230, 301, 27));
        pushButtonPara = new QPushButton(DialogVisualImage);
        pushButtonPara->setObjectName(QString::fromUtf8("pushButtonPara"));
        pushButtonPara->setGeometry(QRect(350, 70, 31, 27));
        pushButtonDEMSource = new QPushButton(DialogVisualImage);
        pushButtonDEMSource->setObjectName(QString::fromUtf8("pushButtonDEMSource"));
        pushButtonDEMSource->setGeometry(QRect(350, 120, 31, 27));
        pushButtonDOMSource = new QPushButton(DialogVisualImage);
        pushButtonDOMSource->setObjectName(QString::fromUtf8("pushButtonDOMSource"));
        pushButtonDOMSource->setGeometry(QRect(350, 180, 31, 27));
        pushButtonDest = new QPushButton(DialogVisualImage);
        pushButtonDest->setObjectName(QString::fromUtf8("pushButtonDest"));
        pushButtonDest->setGeometry(QRect(350, 230, 31, 27));
        label = new QLabel(DialogVisualImage);
        label->setObjectName(QString::fromUtf8("label"));
        label->setGeometry(QRect(30, 30, 301, 17));
        label_2 = new QLabel(DialogVisualImage);
        label_2->setObjectName(QString::fromUtf8("label_2"));
        label_2->setGeometry(QRect(20, 100, 301, 17));
        label_3 = new QLabel(DialogVisualImage);
        label_3->setObjectName(QString::fromUtf8("label_3"));
        label_3->setGeometry(QRect(10, 160, 301, 17));
        label_4 = new QLabel(DialogVisualImage);
        label_4->setObjectName(QString::fromUtf8("label_4"));
        label_4->setGeometry(QRect(20, 210, 301, 17));

        retranslateUi(DialogVisualImage);
        QObject::connect(buttonBox, SIGNAL(accepted()), DialogVisualImage, SLOT(accept()));
        QObject::connect(buttonBox, SIGNAL(rejected()), DialogVisualImage, SLOT(reject()));

        QMetaObject::connectSlotsByName(DialogVisualImage);
    } // setupUi

    void retranslateUi(QDialog *DialogVisualImage)
    {
        DialogVisualImage->setWindowTitle(QApplication::translate("DialogVisualImage", "Dialog", 0, QApplication::UnicodeUTF8));
        pushButtonPara->setText(QApplication::translate("DialogVisualImage", "...", 0, QApplication::UnicodeUTF8));
        pushButtonDEMSource->setText(QApplication::translate("DialogVisualImage", "...", 0, QApplication::UnicodeUTF8));
        pushButtonDOMSource->setText(QApplication::translate("DialogVisualImage", "...", 0, QApplication::UnicodeUTF8));
        pushButtonDest->setText(QApplication::translate("DialogVisualImage", "...", 0, QApplication::UnicodeUTF8));
        label->setText(QApplication::translate("DialogVisualImage", "\345\206\205\345\244\226\346\226\271\344\275\215\345\205\203\347\264\240\345\217\202\346\225\260\346\226\207\344\273\266", 0, QApplication::UnicodeUTF8));
        label_2->setText(QApplication::translate("DialogVisualImage", "DEM\346\226\207\344\273\266", 0, QApplication::UnicodeUTF8));
        label_3->setText(QApplication::translate("DialogVisualImage", "DOM\346\226\207\344\273\266", 0, QApplication::UnicodeUTF8));
        label_4->setText(QApplication::translate("DialogVisualImage", "\350\276\223\345\207\272\346\226\207\344\273\266", 0, QApplication::UnicodeUTF8));
    } // retranslateUi

};

namespace Ui {
    class DialogVisualImage: public Ui_DialogVisualImage {};
} // namespace Ui

QT_END_NAMESPACE

#endif // UI_DIALOGVISUALIMAGE_H
