/********************************************************************************
** Form generated from reading UI file 'dialogdisparitymap.ui'
**
** Created: Fri Feb 10 16:54:12 2012
**      by: Qt User Interface Compiler version 4.7.2
**
** WARNING! All changes made in this file will be lost when recompiling UI file!
********************************************************************************/

#ifndef UI_DIALOGDISPARITYMAP_H
#define UI_DIALOGDISPARITYMAP_H

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

class Ui_DialogDisparityMap
{
public:
    QDialogButtonBox *buttonBox;
    QPushButton *pushButtonSourceRight;
    QLineEdit *lineEditSourceRight;
    QLabel *label_2;
    QLabel *label;
    QLineEdit *lineEditSourceLeft;
    QPushButton *pushButtonSourceLeft;
    QLineEdit *lineEditDest;
    QPushButton *pushButtonDest;
    QLabel *label_3;

    void setupUi(QDialog *DialogDisparityMap)
    {
        if (DialogDisparityMap->objectName().isEmpty())
            DialogDisparityMap->setObjectName(QString::fromUtf8("DialogDisparityMap"));
        DialogDisparityMap->resize(563, 315);
        buttonBox = new QDialogButtonBox(DialogDisparityMap);
        buttonBox->setObjectName(QString::fromUtf8("buttonBox"));
        buttonBox->setGeometry(QRect(450, 20, 81, 241));
        buttonBox->setOrientation(Qt::Vertical);
        buttonBox->setStandardButtons(QDialogButtonBox::Cancel|QDialogButtonBox::Ok);
        pushButtonSourceRight = new QPushButton(DialogDisparityMap);
        pushButtonSourceRight->setObjectName(QString::fromUtf8("pushButtonSourceRight"));
        pushButtonSourceRight->setGeometry(QRect(450, 190, 41, 27));
        lineEditSourceRight = new QLineEdit(DialogDisparityMap);
        lineEditSourceRight->setObjectName(QString::fromUtf8("lineEditSourceRight"));
        lineEditSourceRight->setGeometry(QRect(10, 190, 391, 27));
        label_2 = new QLabel(DialogDisparityMap);
        label_2->setObjectName(QString::fromUtf8("label_2"));
        label_2->setGeometry(QRect(20, 160, 141, 17));
        label = new QLabel(DialogDisparityMap);
        label->setObjectName(QString::fromUtf8("label"));
        label->setGeometry(QRect(20, 90, 141, 17));
        lineEditSourceLeft = new QLineEdit(DialogDisparityMap);
        lineEditSourceLeft->setObjectName(QString::fromUtf8("lineEditSourceLeft"));
        lineEditSourceLeft->setGeometry(QRect(10, 120, 391, 27));
        pushButtonSourceLeft = new QPushButton(DialogDisparityMap);
        pushButtonSourceLeft->setObjectName(QString::fromUtf8("pushButtonSourceLeft"));
        pushButtonSourceLeft->setGeometry(QRect(450, 120, 41, 27));
        lineEditDest = new QLineEdit(DialogDisparityMap);
        lineEditDest->setObjectName(QString::fromUtf8("lineEditDest"));
        lineEditDest->setGeometry(QRect(10, 250, 391, 27));
        pushButtonDest = new QPushButton(DialogDisparityMap);
        pushButtonDest->setObjectName(QString::fromUtf8("pushButtonDest"));
        pushButtonDest->setGeometry(QRect(450, 250, 41, 27));
        label_3 = new QLabel(DialogDisparityMap);
        label_3->setObjectName(QString::fromUtf8("label_3"));
        label_3->setGeometry(QRect(20, 230, 141, 17));

        retranslateUi(DialogDisparityMap);
        QObject::connect(buttonBox, SIGNAL(accepted()), DialogDisparityMap, SLOT(accept()));
        QObject::connect(buttonBox, SIGNAL(rejected()), DialogDisparityMap, SLOT(reject()));

        QMetaObject::connectSlotsByName(DialogDisparityMap);
    } // setupUi

    void retranslateUi(QDialog *DialogDisparityMap)
    {
        DialogDisparityMap->setWindowTitle(QApplication::translate("DialogDisparityMap", "Dialog", 0, QApplication::UnicodeUTF8));
        pushButtonSourceRight->setText(QApplication::translate("DialogDisparityMap", "...", 0, QApplication::UnicodeUTF8));
        label_2->setText(QApplication::translate("DialogDisparityMap", "\350\276\223\345\205\245\345\217\263\345\233\276\347\202\271\346\226\207\344\273\266\350\267\257\345\276\204", 0, QApplication::UnicodeUTF8));
        label->setText(QApplication::translate("DialogDisparityMap", "\350\276\223\345\205\245\345\267\246\345\233\276\347\202\271\346\226\207\344\273\266\350\267\257\345\276\204", 0, QApplication::UnicodeUTF8));
        pushButtonSourceLeft->setText(QApplication::translate("DialogDisparityMap", "...", 0, QApplication::UnicodeUTF8));
        pushButtonDest->setText(QApplication::translate("DialogDisparityMap", "...", 0, QApplication::UnicodeUTF8));
        label_3->setText(QApplication::translate("DialogDisparityMap", "\350\276\223\345\207\272\346\226\207\344\273\266\350\267\257\345\276\204", 0, QApplication::UnicodeUTF8));
    } // retranslateUi

};

namespace Ui {
    class DialogDisparityMap: public Ui_DialogDisparityMap {};
} // namespace Ui

QT_END_NAMESPACE

#endif // UI_DIALOGDISPARITYMAP_H
