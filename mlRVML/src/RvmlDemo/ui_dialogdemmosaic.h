/********************************************************************************
** Form generated from reading UI file 'dialogdemmosaic.ui'
**
** Created: Fri Feb 10 10:01:04 2012
**      by: Qt User Interface Compiler version 4.7.2
**
** WARNING! All changes made in this file will be lost when recompiling UI file!
********************************************************************************/

#ifndef UI_DIALOGDEMMOSAIC_H
#define UI_DIALOGDEMMOSAIC_H

#include <QtCore/QVariant>
#include <QtGui/QAction>
#include <QtGui/QApplication>
#include <QtGui/QButtonGroup>
#include <QtGui/QDialog>
#include <QtGui/QDialogButtonBox>
#include <QtGui/QDoubleSpinBox>
#include <QtGui/QHeaderView>
#include <QtGui/QLabel>
#include <QtGui/QLineEdit>
#include <QtGui/QPushButton>

QT_BEGIN_NAMESPACE

class Ui_DialogDEMMosaic
{
public:
    QDialogButtonBox *buttonBox;
    QLineEdit *lineEditDest;
    QLabel *label;
    QLabel *label_2;
    QPushButton *pushButtonDest;
    QPushButton *pushButtonSource;
    QLineEdit *lineEditSource;
    QDoubleSpinBox *doubleSpinBoxXResolution;
    QDoubleSpinBox *doubleSpinBoxYResolution;
    QLabel *label_3;
    QLabel *label_4;

    void setupUi(QDialog *DialogDEMMosaic)
    {
        if (DialogDEMMosaic->objectName().isEmpty())
            DialogDEMMosaic->setObjectName(QString::fromUtf8("DialogDEMMosaic"));
        DialogDEMMosaic->resize(595, 387);
        buttonBox = new QDialogButtonBox(DialogDEMMosaic);
        buttonBox->setObjectName(QString::fromUtf8("buttonBox"));
        buttonBox->setGeometry(QRect(490, 20, 81, 241));
        buttonBox->setOrientation(Qt::Vertical);
        buttonBox->setStandardButtons(QDialogButtonBox::Cancel|QDialogButtonBox::Ok);
        lineEditDest = new QLineEdit(DialogDEMMosaic);
        lineEditDest->setObjectName(QString::fromUtf8("lineEditDest"));
        lineEditDest->setGeometry(QRect(10, 210, 391, 27));
        label = new QLabel(DialogDEMMosaic);
        label->setObjectName(QString::fromUtf8("label"));
        label->setGeometry(QRect(60, 120, 141, 17));
        label_2 = new QLabel(DialogDEMMosaic);
        label_2->setObjectName(QString::fromUtf8("label_2"));
        label_2->setGeometry(QRect(60, 190, 141, 17));
        pushButtonDest = new QPushButton(DialogDEMMosaic);
        pushButtonDest->setObjectName(QString::fromUtf8("pushButtonDest"));
        pushButtonDest->setGeometry(QRect(450, 210, 41, 27));
        pushButtonSource = new QPushButton(DialogDEMMosaic);
        pushButtonSource->setObjectName(QString::fromUtf8("pushButtonSource"));
        pushButtonSource->setGeometry(QRect(450, 150, 41, 27));
        lineEditSource = new QLineEdit(DialogDEMMosaic);
        lineEditSource->setObjectName(QString::fromUtf8("lineEditSource"));
        lineEditSource->setGeometry(QRect(10, 150, 391, 27));
        doubleSpinBoxXResolution = new QDoubleSpinBox(DialogDEMMosaic);
        doubleSpinBoxXResolution->setObjectName(QString::fromUtf8("doubleSpinBoxXResolution"));
        doubleSpinBoxXResolution->setGeometry(QRect(50, 290, 101, 27));
        doubleSpinBoxXResolution->setMaximum(10000);
        doubleSpinBoxXResolution->setValue(0.05);
        doubleSpinBoxYResolution = new QDoubleSpinBox(DialogDEMMosaic);
        doubleSpinBoxYResolution->setObjectName(QString::fromUtf8("doubleSpinBoxYResolution"));
        doubleSpinBoxYResolution->setGeometry(QRect(190, 290, 101, 27));
        doubleSpinBoxYResolution->setMaximum(10000);
        doubleSpinBoxYResolution->setValue(0.05);
        label_3 = new QLabel(DialogDEMMosaic);
        label_3->setObjectName(QString::fromUtf8("label_3"));
        label_3->setGeometry(QRect(70, 260, 141, 17));
        label_4 = new QLabel(DialogDEMMosaic);
        label_4->setObjectName(QString::fromUtf8("label_4"));
        label_4->setGeometry(QRect(200, 260, 141, 17));

        retranslateUi(DialogDEMMosaic);
        QObject::connect(buttonBox, SIGNAL(accepted()), DialogDEMMosaic, SLOT(accept()));
        QObject::connect(buttonBox, SIGNAL(rejected()), DialogDEMMosaic, SLOT(reject()));

        QMetaObject::connectSlotsByName(DialogDEMMosaic);
    } // setupUi

    void retranslateUi(QDialog *DialogDEMMosaic)
    {
        DialogDEMMosaic->setWindowTitle(QApplication::translate("DialogDEMMosaic", "Dialog", 0, QApplication::UnicodeUTF8));
        label->setText(QApplication::translate("DialogDEMMosaic", "\350\276\223\345\205\245\345\267\245\347\250\213\346\226\207\344\273\266\350\267\257\345\276\204", 0, QApplication::UnicodeUTF8));
        label_2->setText(QApplication::translate("DialogDEMMosaic", "\350\276\223\345\207\272\346\226\207\344\273\266\350\267\257\345\276\204", 0, QApplication::UnicodeUTF8));
        pushButtonDest->setText(QApplication::translate("DialogDEMMosaic", "...", 0, QApplication::UnicodeUTF8));
        pushButtonSource->setText(QApplication::translate("DialogDEMMosaic", "...", 0, QApplication::UnicodeUTF8));
        label_3->setText(QApplication::translate("DialogDEMMosaic", "X\345\210\206\350\276\250\347\216\207", 0, QApplication::UnicodeUTF8));
        label_4->setText(QApplication::translate("DialogDEMMosaic", "Y\345\210\206\350\276\250\347\216\207", 0, QApplication::UnicodeUTF8));
    } // retranslateUi

};

namespace Ui {
    class DialogDEMMosaic: public Ui_DialogDEMMosaic {};
} // namespace Ui

QT_END_NAMESPACE

#endif // UI_DIALOGDEMMOSAIC_H
