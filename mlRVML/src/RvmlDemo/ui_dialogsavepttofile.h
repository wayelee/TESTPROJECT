/********************************************************************************
** Form generated from reading UI file 'dialogsavepttofile.ui'
**
** Created: Fri Mar 2 14:43:35 2012
**      by: Qt User Interface Compiler version 4.7.2
**
** WARNING! All changes made in this file will be lost when recompiling UI file!
********************************************************************************/

#ifndef UI_DIALOGSAVEPTTOFILE_H
#define UI_DIALOGSAVEPTTOFILE_H

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

class Ui_DialogSavePtToFile
{
public:
    QDialogButtonBox *buttonBox;
    QPushButton *pushButtonDestLeft;
    QLabel *label_2;
    QLineEdit *lineEditDestLeft;
    QLabel *label_3;
    QPushButton *pushButtonDestRight;
    QLineEdit *lineEditDestRight;

    void setupUi(QDialog *DialogSavePtToFile)
    {
        if (DialogSavePtToFile->objectName().isEmpty())
            DialogSavePtToFile->setObjectName(QString::fromUtf8("DialogSavePtToFile"));
        DialogSavePtToFile->resize(620, 288);
        buttonBox = new QDialogButtonBox(DialogSavePtToFile);
        buttonBox->setObjectName(QString::fromUtf8("buttonBox"));
        buttonBox->setGeometry(QRect(510, 20, 81, 241));
        buttonBox->setOrientation(Qt::Vertical);
        buttonBox->setStandardButtons(QDialogButtonBox::Cancel|QDialogButtonBox::Ok);
        pushButtonDestLeft = new QPushButton(DialogSavePtToFile);
        pushButtonDestLeft->setObjectName(QString::fromUtf8("pushButtonDestLeft"));
        pushButtonDestLeft->setGeometry(QRect(460, 100, 41, 27));
        label_2 = new QLabel(DialogSavePtToFile);
        label_2->setObjectName(QString::fromUtf8("label_2"));
        label_2->setGeometry(QRect(100, 80, 141, 17));
        lineEditDestLeft = new QLineEdit(DialogSavePtToFile);
        lineEditDestLeft->setObjectName(QString::fromUtf8("lineEditDestLeft"));
        lineEditDestLeft->setGeometry(QRect(50, 100, 391, 27));
        label_3 = new QLabel(DialogSavePtToFile);
        label_3->setObjectName(QString::fromUtf8("label_3"));
        label_3->setGeometry(QRect(100, 150, 141, 17));
        pushButtonDestRight = new QPushButton(DialogSavePtToFile);
        pushButtonDestRight->setObjectName(QString::fromUtf8("pushButtonDestRight"));
        pushButtonDestRight->setGeometry(QRect(460, 180, 41, 27));
        lineEditDestRight = new QLineEdit(DialogSavePtToFile);
        lineEditDestRight->setObjectName(QString::fromUtf8("lineEditDestRight"));
        lineEditDestRight->setGeometry(QRect(50, 180, 391, 27));

        retranslateUi(DialogSavePtToFile);
        QObject::connect(buttonBox, SIGNAL(accepted()), DialogSavePtToFile, SLOT(accept()));
        QObject::connect(buttonBox, SIGNAL(rejected()), DialogSavePtToFile, SLOT(reject()));

        QMetaObject::connectSlotsByName(DialogSavePtToFile);
    } // setupUi

    void retranslateUi(QDialog *DialogSavePtToFile)
    {
        DialogSavePtToFile->setWindowTitle(QApplication::translate("DialogSavePtToFile", "Dialog", 0, QApplication::UnicodeUTF8));
        pushButtonDestLeft->setText(QApplication::translate("DialogSavePtToFile", "...", 0, QApplication::UnicodeUTF8));
        label_2->setText(QApplication::translate("DialogSavePtToFile", "\345\267\246\345\233\276\345\203\217\347\202\271\345\255\230\345\202\250\350\267\257\345\276\204", 0, QApplication::UnicodeUTF8));
        label_3->setText(QApplication::translate("DialogSavePtToFile", "\345\217\263\345\233\276\345\203\217\347\202\271\345\255\230\345\202\250\350\267\257\345\276\204", 0, QApplication::UnicodeUTF8));
        pushButtonDestRight->setText(QApplication::translate("DialogSavePtToFile", "...", 0, QApplication::UnicodeUTF8));
    } // retranslateUi

};

namespace Ui {
    class DialogSavePtToFile: public Ui_DialogSavePtToFile {};
} // namespace Ui

QT_END_NAMESPACE

#endif // UI_DIALOGSAVEPTTOFILE_H
