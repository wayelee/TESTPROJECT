/********************************************************************************
** Form generated from reading UI file 'dialogcoordtrans.ui'
**
** Created: Mon Feb 27 16:09:18 2012
**      by: Qt User Interface Compiler version 4.7.2
**
** WARNING! All changes made in this file will be lost when recompiling UI file!
********************************************************************************/

#ifndef UI_DIALOGCOORDTRANS_H
#define UI_DIALOGCOORDTRANS_H

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
#include <QtGui/QRadioButton>

QT_BEGIN_NAMESPACE

class Ui_Dialogcoordtrans
{
public:
    QDialogButtonBox *buttonBox;
    QLabel *label;
    QLineEdit *lineEditSource;
    QLabel *label_2;
    QLineEdit *lineEditDest;
    QPushButton *pushButtonSource;
    QPushButton *pushButtonDest;
    QRadioButton *radioButton_BasedOnLatLon;
    QRadioButton *radioButton_BasedOn3DCoord;

    void setupUi(QDialog *Dialogcoordtrans)
    {
        if (Dialogcoordtrans->objectName().isEmpty())
            Dialogcoordtrans->setObjectName(QString::fromUtf8("Dialogcoordtrans"));
        Dialogcoordtrans->resize(640, 277);
        buttonBox = new QDialogButtonBox(Dialogcoordtrans);
        buttonBox->setObjectName(QString::fromUtf8("buttonBox"));
        buttonBox->setGeometry(QRect(540, 40, 81, 241));
        buttonBox->setOrientation(Qt::Vertical);
        buttonBox->setStandardButtons(QDialogButtonBox::Cancel|QDialogButtonBox::Ok);
        label = new QLabel(Dialogcoordtrans);
        label->setObjectName(QString::fromUtf8("label"));
        label->setGeometry(QRect(50, 90, 141, 17));
        lineEditSource = new QLineEdit(Dialogcoordtrans);
        lineEditSource->setObjectName(QString::fromUtf8("lineEditSource"));
        lineEditSource->setGeometry(QRect(40, 120, 391, 27));
        label_2 = new QLabel(Dialogcoordtrans);
        label_2->setObjectName(QString::fromUtf8("label_2"));
        label_2->setGeometry(QRect(50, 160, 141, 17));
        lineEditDest = new QLineEdit(Dialogcoordtrans);
        lineEditDest->setObjectName(QString::fromUtf8("lineEditDest"));
        lineEditDest->setGeometry(QRect(40, 180, 391, 27));
        pushButtonSource = new QPushButton(Dialogcoordtrans);
        pushButtonSource->setObjectName(QString::fromUtf8("pushButtonSource"));
        pushButtonSource->setGeometry(QRect(480, 120, 41, 27));
        pushButtonDest = new QPushButton(Dialogcoordtrans);
        pushButtonDest->setObjectName(QString::fromUtf8("pushButtonDest"));
        pushButtonDest->setGeometry(QRect(480, 180, 41, 27));
        radioButton_BasedOnLatLon = new QRadioButton(Dialogcoordtrans);
        radioButton_BasedOnLatLon->setObjectName(QString::fromUtf8("radioButton_BasedOnLatLon"));
        radioButton_BasedOnLatLon->setGeometry(QRect(50, 40, 116, 22));
        radioButton_BasedOnLatLon->setChecked(true);
        radioButton_BasedOn3DCoord = new QRadioButton(Dialogcoordtrans);
        radioButton_BasedOn3DCoord->setObjectName(QString::fromUtf8("radioButton_BasedOn3DCoord"));
        radioButton_BasedOn3DCoord->setGeometry(QRect(240, 40, 116, 22));

        retranslateUi(Dialogcoordtrans);
        QObject::connect(buttonBox, SIGNAL(accepted()), Dialogcoordtrans, SLOT(accept()));
        QObject::connect(buttonBox, SIGNAL(rejected()), Dialogcoordtrans, SLOT(reject()));

        QMetaObject::connectSlotsByName(Dialogcoordtrans);
    } // setupUi

    void retranslateUi(QDialog *Dialogcoordtrans)
    {
        Dialogcoordtrans->setWindowTitle(QApplication::translate("Dialogcoordtrans", "Dialog", 0, QApplication::UnicodeUTF8));
        label->setText(QApplication::translate("Dialogcoordtrans", "\350\276\223\345\205\245\346\226\207\344\273\266\350\267\257\345\276\204", 0, QApplication::UnicodeUTF8));
        label_2->setText(QApplication::translate("Dialogcoordtrans", "\350\276\223\345\207\272\346\226\207\344\273\266\350\267\257\345\276\204", 0, QApplication::UnicodeUTF8));
        pushButtonSource->setText(QApplication::translate("Dialogcoordtrans", "...", 0, QApplication::UnicodeUTF8));
        pushButtonDest->setText(QApplication::translate("Dialogcoordtrans", "...", 0, QApplication::UnicodeUTF8));
        radioButton_BasedOnLatLon->setText(QApplication::translate("Dialogcoordtrans", "\346\240\271\346\215\256\347\273\217\347\272\254\345\272\246", 0, QApplication::UnicodeUTF8));
        radioButton_BasedOn3DCoord->setText(QApplication::translate("Dialogcoordtrans", "\346\240\271\346\215\256\344\270\211\347\273\264\345\235\220\346\240\207", 0, QApplication::UnicodeUTF8));
    } // retranslateUi

};

namespace Ui {
    class Dialogcoordtrans: public Ui_Dialogcoordtrans {};
} // namespace Ui

QT_END_NAMESPACE

#endif // UI_DIALOGCOORDTRANS_H
