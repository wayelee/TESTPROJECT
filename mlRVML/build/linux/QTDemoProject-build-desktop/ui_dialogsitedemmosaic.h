/********************************************************************************
** Form generated from reading UI file 'dialogsitedemmosaic.ui'
**
** Created: Wed Feb 8 17:53:33 2012
**      by: Qt User Interface Compiler version 4.7.2
**
** WARNING! All changes made in this file will be lost when recompiling UI file!
********************************************************************************/

#ifndef UI_DIALOGSITEDEMMOSAIC_H
#define UI_DIALOGSITEDEMMOSAIC_H

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

class Ui_DialogSiteDEMMosaic
{
public:
    QDialogButtonBox *buttonBox;
    QLineEdit *lineEditDestDEM;
    QPushButton *pushButtonDestDEM;
    QLabel *label_4;
    QLineEdit *lineEditSource;
    QLabel *label;
    QPushButton *pushButtonSource;
    QLabel *label_5;
    QLineEdit *lineEditDestDOM;
    QPushButton *pushButtonDestDOM;
    QDoubleSpinBox *doubleSpinBoxMapRange;
    QLabel *label_2;
    QDoubleSpinBox *doubleSpinBoxResolution;
    QLabel *label_3;

    void setupUi(QDialog *DialogSiteDEMMosaic)
    {
        if (DialogSiteDEMMosaic->objectName().isEmpty())
            DialogSiteDEMMosaic->setObjectName(QString::fromUtf8("DialogSiteDEMMosaic"));
        DialogSiteDEMMosaic->resize(696, 405);
        buttonBox = new QDialogButtonBox(DialogSiteDEMMosaic);
        buttonBox->setObjectName(QString::fromUtf8("buttonBox"));
        buttonBox->setGeometry(QRect(600, 30, 81, 241));
        buttonBox->setOrientation(Qt::Vertical);
        buttonBox->setStandardButtons(QDialogButtonBox::Cancel|QDialogButtonBox::Ok);
        lineEditDestDEM = new QLineEdit(DialogSiteDEMMosaic);
        lineEditDestDEM->setObjectName(QString::fromUtf8("lineEditDestDEM"));
        lineEditDestDEM->setGeometry(QRect(40, 200, 381, 27));
        pushButtonDestDEM = new QPushButton(DialogSiteDEMMosaic);
        pushButtonDestDEM->setObjectName(QString::fromUtf8("pushButtonDestDEM"));
        pushButtonDestDEM->setGeometry(QRect(460, 200, 41, 31));
        label_4 = new QLabel(DialogSiteDEMMosaic);
        label_4->setObjectName(QString::fromUtf8("label_4"));
        label_4->setGeometry(QRect(60, 170, 301, 17));
        lineEditSource = new QLineEdit(DialogSiteDEMMosaic);
        lineEditSource->setObjectName(QString::fromUtf8("lineEditSource"));
        lineEditSource->setGeometry(QRect(40, 120, 391, 27));
        label = new QLabel(DialogSiteDEMMosaic);
        label->setObjectName(QString::fromUtf8("label"));
        label->setGeometry(QRect(70, 90, 141, 17));
        pushButtonSource = new QPushButton(DialogSiteDEMMosaic);
        pushButtonSource->setObjectName(QString::fromUtf8("pushButtonSource"));
        pushButtonSource->setGeometry(QRect(460, 116, 41, 31));
        label_5 = new QLabel(DialogSiteDEMMosaic);
        label_5->setObjectName(QString::fromUtf8("label_5"));
        label_5->setGeometry(QRect(60, 240, 301, 17));
        lineEditDestDOM = new QLineEdit(DialogSiteDEMMosaic);
        lineEditDestDOM->setObjectName(QString::fromUtf8("lineEditDestDOM"));
        lineEditDestDOM->setGeometry(QRect(40, 270, 381, 27));
        pushButtonDestDOM = new QPushButton(DialogSiteDEMMosaic);
        pushButtonDestDOM->setObjectName(QString::fromUtf8("pushButtonDestDOM"));
        pushButtonDestDOM->setGeometry(QRect(460, 270, 41, 31));
        doubleSpinBoxMapRange = new QDoubleSpinBox(DialogSiteDEMMosaic);
        doubleSpinBoxMapRange->setObjectName(QString::fromUtf8("doubleSpinBoxMapRange"));
        doubleSpinBoxMapRange->setGeometry(QRect(70, 340, 62, 27));
        label_2 = new QLabel(DialogSiteDEMMosaic);
        label_2->setObjectName(QString::fromUtf8("label_2"));
        label_2->setGeometry(QRect(70, 310, 67, 17));
        doubleSpinBoxResolution = new QDoubleSpinBox(DialogSiteDEMMosaic);
        doubleSpinBoxResolution->setObjectName(QString::fromUtf8("doubleSpinBoxResolution"));
        doubleSpinBoxResolution->setGeometry(QRect(210, 340, 62, 27));
        label_3 = new QLabel(DialogSiteDEMMosaic);
        label_3->setObjectName(QString::fromUtf8("label_3"));
        label_3->setGeometry(QRect(210, 310, 67, 17));

        retranslateUi(DialogSiteDEMMosaic);
        QObject::connect(buttonBox, SIGNAL(accepted()), DialogSiteDEMMosaic, SLOT(accept()));
        QObject::connect(buttonBox, SIGNAL(rejected()), DialogSiteDEMMosaic, SLOT(reject()));

        QMetaObject::connectSlotsByName(DialogSiteDEMMosaic);
    } // setupUi

    void retranslateUi(QDialog *DialogSiteDEMMosaic)
    {
        DialogSiteDEMMosaic->setWindowTitle(QApplication::translate("DialogSiteDEMMosaic", "Dialog", 0, QApplication::UnicodeUTF8));
        pushButtonDestDEM->setText(QApplication::translate("DialogSiteDEMMosaic", "...", 0, QApplication::UnicodeUTF8));
        label_4->setText(QApplication::translate("DialogSiteDEMMosaic", "\350\276\223\345\207\272DEM\346\226\207\344\273\266", 0, QApplication::UnicodeUTF8));
        label->setText(QApplication::translate("DialogSiteDEMMosaic", "\350\276\223\345\205\245\346\226\207\344\273\266\350\267\257\345\276\204", 0, QApplication::UnicodeUTF8));
        pushButtonSource->setText(QApplication::translate("DialogSiteDEMMosaic", "...", 0, QApplication::UnicodeUTF8));
        label_5->setText(QApplication::translate("DialogSiteDEMMosaic", "\350\276\223\345\207\272DOM\346\226\207\344\273\266", 0, QApplication::UnicodeUTF8));
        pushButtonDestDOM->setText(QApplication::translate("DialogSiteDEMMosaic", "...", 0, QApplication::UnicodeUTF8));
        label_2->setText(QApplication::translate("DialogSiteDEMMosaic", "\345\210\266\345\233\276\350\214\203\345\233\264", 0, QApplication::UnicodeUTF8));
        label_3->setText(QApplication::translate("DialogSiteDEMMosaic", "\345\210\206\350\276\250\347\216\207", 0, QApplication::UnicodeUTF8));
    } // retranslateUi

};

namespace Ui {
    class DialogSiteDEMMosaic: public Ui_DialogSiteDEMMosaic {};
} // namespace Ui

QT_END_NAMESPACE

#endif // UI_DIALOGSITEDEMMOSAIC_H
