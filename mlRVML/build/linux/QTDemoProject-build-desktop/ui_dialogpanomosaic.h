/********************************************************************************
** Form generated from reading UI file 'dialogpanomosaic.ui'
**
** Created: Wed Feb 8 17:53:33 2012
**      by: Qt User Interface Compiler version 4.7.2
**
** WARNING! All changes made in this file will be lost when recompiling UI file!
********************************************************************************/

#ifndef UI_DIALOGPANOMOSAIC_H
#define UI_DIALOGPANOMOSAIC_H

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

class Ui_DialogPanoMosaic
{
public:
    QDialogButtonBox *buttonBox;
    QLineEdit *lineEditSource;
    QPushButton *pushButtonDest;
    QLineEdit *lineEditDest;
    QLabel *label;
    QPushButton *pushButtonSource;
    QLabel *label_2;

    void setupUi(QDialog *DialogPanoMosaic)
    {
        if (DialogPanoMosaic->objectName().isEmpty())
            DialogPanoMosaic->setObjectName(QString::fromUtf8("DialogPanoMosaic"));
        DialogPanoMosaic->resize(601, 300);
        buttonBox = new QDialogButtonBox(DialogPanoMosaic);
        buttonBox->setObjectName(QString::fromUtf8("buttonBox"));
        buttonBox->setGeometry(QRect(500, 30, 81, 241));
        buttonBox->setOrientation(Qt::Vertical);
        buttonBox->setStandardButtons(QDialogButtonBox::Cancel|QDialogButtonBox::Ok);
        lineEditSource = new QLineEdit(DialogPanoMosaic);
        lineEditSource->setObjectName(QString::fromUtf8("lineEditSource"));
        lineEditSource->setGeometry(QRect(30, 140, 391, 27));
        pushButtonDest = new QPushButton(DialogPanoMosaic);
        pushButtonDest->setObjectName(QString::fromUtf8("pushButtonDest"));
        pushButtonDest->setGeometry(QRect(470, 200, 41, 27));
        lineEditDest = new QLineEdit(DialogPanoMosaic);
        lineEditDest->setObjectName(QString::fromUtf8("lineEditDest"));
        lineEditDest->setGeometry(QRect(30, 200, 391, 27));
        label = new QLabel(DialogPanoMosaic);
        label->setObjectName(QString::fromUtf8("label"));
        label->setGeometry(QRect(80, 110, 141, 17));
        pushButtonSource = new QPushButton(DialogPanoMosaic);
        pushButtonSource->setObjectName(QString::fromUtf8("pushButtonSource"));
        pushButtonSource->setGeometry(QRect(470, 140, 41, 27));
        label_2 = new QLabel(DialogPanoMosaic);
        label_2->setObjectName(QString::fromUtf8("label_2"));
        label_2->setGeometry(QRect(80, 180, 141, 17));

        retranslateUi(DialogPanoMosaic);
        QObject::connect(buttonBox, SIGNAL(accepted()), DialogPanoMosaic, SLOT(accept()));
        QObject::connect(buttonBox, SIGNAL(rejected()), DialogPanoMosaic, SLOT(reject()));

        QMetaObject::connectSlotsByName(DialogPanoMosaic);
    } // setupUi

    void retranslateUi(QDialog *DialogPanoMosaic)
    {
        DialogPanoMosaic->setWindowTitle(QApplication::translate("DialogPanoMosaic", "Dialog", 0, QApplication::UnicodeUTF8));
        pushButtonDest->setText(QApplication::translate("DialogPanoMosaic", "...", 0, QApplication::UnicodeUTF8));
        label->setText(QApplication::translate("DialogPanoMosaic", "\350\276\223\345\205\245\346\226\207\344\273\266\350\267\257\345\276\204", 0, QApplication::UnicodeUTF8));
        pushButtonSource->setText(QApplication::translate("DialogPanoMosaic", "...", 0, QApplication::UnicodeUTF8));
        label_2->setText(QApplication::translate("DialogPanoMosaic", "\350\276\223\345\207\272\346\226\207\344\273\266\350\267\257\345\276\204", 0, QApplication::UnicodeUTF8));
    } // retranslateUi

};

namespace Ui {
    class DialogPanoMosaic: public Ui_DialogPanoMosaic {};
} // namespace Ui

QT_END_NAMESPACE

#endif // UI_DIALOGPANOMOSAIC_H
