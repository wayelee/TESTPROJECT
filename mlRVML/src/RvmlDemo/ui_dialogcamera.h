/********************************************************************************
** Form generated from reading UI file 'dialogcamera.ui'
**
** Created: Mon Feb 13 14:36:28 2012
**      by: Qt User Interface Compiler version 4.7.2
**
** WARNING! All changes made in this file will be lost when recompiling UI file!
********************************************************************************/

#ifndef UI_DIALOGCAMERA_H
#define UI_DIALOGCAMERA_H

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

class Ui_DialogCamera
{
public:
    QDialogButtonBox *buttonBox;
    QLabel *label;
    QPushButton *pushButtonDest;
    QPushButton *pushButtonSource;
    QLineEdit *lineEditSource;
    QLineEdit *lineEditDest;
    QLabel *label_2;
    QLineEdit *lineEditAccReport;
    QLabel *label_3;
    QPushButton *pushButtonDestAccReport;

    void setupUi(QDialog *DialogCamera)
    {
        if (DialogCamera->objectName().isEmpty())
            DialogCamera->setObjectName(QString::fromUtf8("DialogCamera"));
        DialogCamera->resize(586, 300);
        buttonBox = new QDialogButtonBox(DialogCamera);
        buttonBox->setObjectName(QString::fromUtf8("buttonBox"));
        buttonBox->setGeometry(QRect(470, 20, 81, 241));
        buttonBox->setOrientation(Qt::Vertical);
        buttonBox->setStandardButtons(QDialogButtonBox::Cancel|QDialogButtonBox::Ok);
        label = new QLabel(DialogCamera);
        label->setObjectName(QString::fromUtf8("label"));
        label->setGeometry(QRect(110, 100, 141, 17));
        pushButtonDest = new QPushButton(DialogCamera);
        pushButtonDest->setObjectName(QString::fromUtf8("pushButtonDest"));
        pushButtonDest->setGeometry(QRect(500, 190, 41, 27));
        pushButtonSource = new QPushButton(DialogCamera);
        pushButtonSource->setObjectName(QString::fromUtf8("pushButtonSource"));
        pushButtonSource->setGeometry(QRect(500, 130, 41, 27));
        lineEditSource = new QLineEdit(DialogCamera);
        lineEditSource->setObjectName(QString::fromUtf8("lineEditSource"));
        lineEditSource->setGeometry(QRect(60, 130, 391, 27));
        lineEditDest = new QLineEdit(DialogCamera);
        lineEditDest->setObjectName(QString::fromUtf8("lineEditDest"));
        lineEditDest->setGeometry(QRect(60, 190, 391, 27));
        label_2 = new QLabel(DialogCamera);
        label_2->setObjectName(QString::fromUtf8("label_2"));
        label_2->setGeometry(QRect(110, 170, 141, 17));
        lineEditAccReport = new QLineEdit(DialogCamera);
        lineEditAccReport->setObjectName(QString::fromUtf8("lineEditAccReport"));
        lineEditAccReport->setGeometry(QRect(60, 250, 391, 27));
        label_3 = new QLabel(DialogCamera);
        label_3->setObjectName(QString::fromUtf8("label_3"));
        label_3->setGeometry(QRect(110, 230, 141, 17));
        pushButtonDestAccReport = new QPushButton(DialogCamera);
        pushButtonDestAccReport->setObjectName(QString::fromUtf8("pushButtonDestAccReport"));
        pushButtonDestAccReport->setGeometry(QRect(500, 250, 41, 27));

        retranslateUi(DialogCamera);
        QObject::connect(buttonBox, SIGNAL(accepted()), DialogCamera, SLOT(accept()));
        QObject::connect(buttonBox, SIGNAL(rejected()), DialogCamera, SLOT(reject()));

        QMetaObject::connectSlotsByName(DialogCamera);
    } // setupUi

    void retranslateUi(QDialog *DialogCamera)
    {
        DialogCamera->setWindowTitle(QApplication::translate("DialogCamera", "Dialog", 0, QApplication::UnicodeUTF8));
        label->setText(QApplication::translate("DialogCamera", "\347\233\270\346\234\272\346\240\207\350\257\206\347\202\271\346\226\207\344\273\266", 0, QApplication::UnicodeUTF8));
        pushButtonDest->setText(QApplication::translate("DialogCamera", "...", 0, QApplication::UnicodeUTF8));
        pushButtonSource->setText(QApplication::translate("DialogCamera", "...", 0, QApplication::UnicodeUTF8));
        label_2->setText(QApplication::translate("DialogCamera", "\347\233\270\346\234\272\345\206\205\345\217\202\346\225\260\346\226\207\344\273\266", 0, QApplication::UnicodeUTF8));
        label_3->setText(QApplication::translate("DialogCamera", "\347\262\276\345\272\246\346\212\245\345\221\212\346\226\207\344\273\266", 0, QApplication::UnicodeUTF8));
        pushButtonDestAccReport->setText(QApplication::translate("DialogCamera", "...", 0, QApplication::UnicodeUTF8));
    } // retranslateUi

};

namespace Ui {
    class DialogCamera: public Ui_DialogCamera {};
} // namespace Ui

QT_END_NAMESPACE

#endif // UI_DIALOGCAMERA_H
