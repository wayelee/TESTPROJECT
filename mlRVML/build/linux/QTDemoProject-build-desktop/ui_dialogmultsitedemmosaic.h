/********************************************************************************
** Form generated from reading UI file 'dialogmultsitedemmosaic.ui'
**
** Created: Wed Feb 8 17:53:33 2012
**      by: Qt User Interface Compiler version 4.7.2
**
** WARNING! All changes made in this file will be lost when recompiling UI file!
********************************************************************************/

#ifndef UI_DIALOGMULTSITEDEMMOSAIC_H
#define UI_DIALOGMULTSITEDEMMOSAIC_H

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
#include <QtGui/QTextEdit>

QT_BEGIN_NAMESPACE

class Ui_DialogMultSiteDemMosaic
{
public:
    QDialogButtonBox *buttonBox;
    QTextEdit *textEditSource;
    QLabel *label;
    QPushButton *pushButtonAddFile;
    QPushButton *pushButtonDeleteFile;
    QLineEdit *lineEditDest;
    QPushButton *pushButtonDest;
    QLabel *label_2;
    QDoubleSpinBox *doubleSpinBox;

    void setupUi(QDialog *DialogMultSiteDemMosaic)
    {
        if (DialogMultSiteDemMosaic->objectName().isEmpty())
            DialogMultSiteDemMosaic->setObjectName(QString::fromUtf8("DialogMultSiteDemMosaic"));
        DialogMultSiteDemMosaic->resize(705, 374);
        buttonBox = new QDialogButtonBox(DialogMultSiteDemMosaic);
        buttonBox->setObjectName(QString::fromUtf8("buttonBox"));
        buttonBox->setGeometry(QRect(600, 30, 81, 241));
        buttonBox->setOrientation(Qt::Vertical);
        buttonBox->setStandardButtons(QDialogButtonBox::Cancel|QDialogButtonBox::Ok);
        textEditSource = new QTextEdit(DialogMultSiteDemMosaic);
        textEditSource->setObjectName(QString::fromUtf8("textEditSource"));
        textEditSource->setGeometry(QRect(40, 90, 511, 171));
        textEditSource->setHorizontalScrollBarPolicy(Qt::ScrollBarAsNeeded);
        textEditSource->setLineWrapMode(QTextEdit::NoWrap);
        textEditSource->setReadOnly(true);
        label = new QLabel(DialogMultSiteDemMosaic);
        label->setObjectName(QString::fromUtf8("label"));
        label->setGeometry(QRect(50, 50, 101, 17));
        pushButtonAddFile = new QPushButton(DialogMultSiteDemMosaic);
        pushButtonAddFile->setObjectName(QString::fromUtf8("pushButtonAddFile"));
        pushButtonAddFile->setGeometry(QRect(590, 130, 97, 27));
        pushButtonDeleteFile = new QPushButton(DialogMultSiteDemMosaic);
        pushButtonDeleteFile->setObjectName(QString::fromUtf8("pushButtonDeleteFile"));
        pushButtonDeleteFile->setGeometry(QRect(590, 170, 101, 27));
        lineEditDest = new QLineEdit(DialogMultSiteDemMosaic);
        lineEditDest->setObjectName(QString::fromUtf8("lineEditDest"));
        lineEditDest->setGeometry(QRect(30, 280, 391, 27));
        pushButtonDest = new QPushButton(DialogMultSiteDemMosaic);
        pushButtonDest->setObjectName(QString::fromUtf8("pushButtonDest"));
        pushButtonDest->setGeometry(QRect(440, 280, 41, 27));
        label_2 = new QLabel(DialogMultSiteDemMosaic);
        label_2->setObjectName(QString::fromUtf8("label_2"));
        label_2->setGeometry(QRect(30, 320, 101, 17));
        doubleSpinBox = new QDoubleSpinBox(DialogMultSiteDemMosaic);
        doubleSpinBox->setObjectName(QString::fromUtf8("doubleSpinBox"));
        doubleSpinBox->setGeometry(QRect(160, 320, 62, 27));
        doubleSpinBox->setValue(1);

        retranslateUi(DialogMultSiteDemMosaic);
        QObject::connect(buttonBox, SIGNAL(accepted()), DialogMultSiteDemMosaic, SLOT(accept()));
        QObject::connect(buttonBox, SIGNAL(rejected()), DialogMultSiteDemMosaic, SLOT(reject()));

        QMetaObject::connectSlotsByName(DialogMultSiteDemMosaic);
    } // setupUi

    void retranslateUi(QDialog *DialogMultSiteDemMosaic)
    {
        DialogMultSiteDemMosaic->setWindowTitle(QApplication::translate("DialogMultSiteDemMosaic", "Dialog", 0, QApplication::UnicodeUTF8));
        label->setText(QApplication::translate("DialogMultSiteDemMosaic", "\350\276\223\345\205\245\346\226\207\344\273\266\350\267\257\345\276\204", 0, QApplication::UnicodeUTF8));
        pushButtonAddFile->setText(QApplication::translate("DialogMultSiteDemMosaic", "\346\267\273\345\212\240\346\226\207\344\273\266", 0, QApplication::UnicodeUTF8));
        pushButtonDeleteFile->setText(QApplication::translate("DialogMultSiteDemMosaic", "\345\210\240\351\231\244\344\270\200\344\270\252\346\226\207\344\273\266", 0, QApplication::UnicodeUTF8));
        pushButtonDest->setText(QApplication::translate("DialogMultSiteDemMosaic", "...", 0, QApplication::UnicodeUTF8));
        label_2->setText(QApplication::translate("DialogMultSiteDemMosaic", "DEM\345\210\206\350\276\250\347\216\207", 0, QApplication::UnicodeUTF8));
    } // retranslateUi

};

namespace Ui {
    class DialogMultSiteDemMosaic: public Ui_DialogMultSiteDemMosaic {};
} // namespace Ui

QT_END_NAMESPACE

#endif // UI_DIALOGMULTSITEDEMMOSAIC_H
