/********************************************************************************
** Form generated from reading UI file 'dialogcamerasurvey.ui'
**
** Created: Thu May 17 19:22:51 2012
**      by: Qt User Interface Compiler version 4.7.2
**
** WARNING! All changes made in this file will be lost when recompiling UI file!
********************************************************************************/

#ifndef UI_DIALOGCAMERASURVEY_H
#define UI_DIALOGCAMERASURVEY_H

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

class Ui_DialogCameraSurvey
{
public:
    QDialogButtonBox *buttonBox;
    QPushButton *pushButtonIntEle;
    QLabel *label_2;
    QLineEdit *lineEditFeatPt;
    QPushButton *pushButtonDest;
    QLineEdit *lineEditIntEle;
    QLabel *label;
    QPushButton *pushButtonFeatPoint;
    QLabel *label_3;
    QLineEdit *lineEditDest;

    void setupUi(QDialog *DialogCameraSurvey)
    {
        if (DialogCameraSurvey->objectName().isEmpty())
            DialogCameraSurvey->setObjectName(QString::fromUtf8("DialogCameraSurvey"));
        DialogCameraSurvey->resize(580, 300);
        buttonBox = new QDialogButtonBox(DialogCameraSurvey);
        buttonBox->setObjectName(QString::fromUtf8("buttonBox"));
        buttonBox->setGeometry(QRect(450, 30, 81, 241));
        buttonBox->setOrientation(Qt::Vertical);
        buttonBox->setStandardButtons(QDialogButtonBox::Cancel|QDialogButtonBox::Ok);
        pushButtonIntEle = new QPushButton(DialogCameraSurvey);
        pushButtonIntEle->setObjectName(QString::fromUtf8("pushButtonIntEle"));
        pushButtonIntEle->setGeometry(QRect(450, 170, 41, 27));
        label_2 = new QLabel(DialogCameraSurvey);
        label_2->setObjectName(QString::fromUtf8("label_2"));
        label_2->setGeometry(QRect(20, 140, 141, 17));
        lineEditFeatPt = new QLineEdit(DialogCameraSurvey);
        lineEditFeatPt->setObjectName(QString::fromUtf8("lineEditFeatPt"));
        lineEditFeatPt->setGeometry(QRect(10, 100, 391, 27));
        pushButtonDest = new QPushButton(DialogCameraSurvey);
        pushButtonDest->setObjectName(QString::fromUtf8("pushButtonDest"));
        pushButtonDest->setGeometry(QRect(450, 230, 41, 27));
        lineEditIntEle = new QLineEdit(DialogCameraSurvey);
        lineEditIntEle->setObjectName(QString::fromUtf8("lineEditIntEle"));
        lineEditIntEle->setGeometry(QRect(10, 170, 391, 27));
        label = new QLabel(DialogCameraSurvey);
        label->setObjectName(QString::fromUtf8("label"));
        label->setGeometry(QRect(20, 70, 141, 17));
        pushButtonFeatPoint = new QPushButton(DialogCameraSurvey);
        pushButtonFeatPoint->setObjectName(QString::fromUtf8("pushButtonFeatPoint"));
        pushButtonFeatPoint->setGeometry(QRect(450, 100, 41, 27));
        label_3 = new QLabel(DialogCameraSurvey);
        label_3->setObjectName(QString::fromUtf8("label_3"));
        label_3->setGeometry(QRect(20, 210, 181, 17));
        lineEditDest = new QLineEdit(DialogCameraSurvey);
        lineEditDest->setObjectName(QString::fromUtf8("lineEditDest"));
        lineEditDest->setGeometry(QRect(10, 230, 391, 27));

        retranslateUi(DialogCameraSurvey);
        QObject::connect(buttonBox, SIGNAL(accepted()), DialogCameraSurvey, SLOT(accept()));
        QObject::connect(buttonBox, SIGNAL(rejected()), DialogCameraSurvey, SLOT(reject()));

        QMetaObject::connectSlotsByName(DialogCameraSurvey);
    } // setupUi

    void retranslateUi(QDialog *DialogCameraSurvey)
    {
        DialogCameraSurvey->setWindowTitle(QApplication::translate("DialogCameraSurvey", "Dialog", 0, QApplication::UnicodeUTF8));
        pushButtonIntEle->setText(QApplication::translate("DialogCameraSurvey", "...", 0, QApplication::UnicodeUTF8));
        label_2->setText(QApplication::translate("DialogCameraSurvey", "\344\270\211\347\273\264\346\216\247\345\210\266\347\202\271\346\226\207\344\273\266", 0, QApplication::UnicodeUTF8));
        pushButtonDest->setText(QApplication::translate("DialogCameraSurvey", "...", 0, QApplication::UnicodeUTF8));
        label->setText(QApplication::translate("DialogCameraSurvey", "\345\275\261\345\203\217\347\211\271\345\276\201\347\202\271\346\226\207\344\273\266", 0, QApplication::UnicodeUTF8));
        pushButtonFeatPoint->setText(QApplication::translate("DialogCameraSurvey", "...", 0, QApplication::UnicodeUTF8));
        label_3->setText(QApplication::translate("DialogCameraSurvey", "\345\244\226\346\226\271\344\275\215\345\205\203\347\264\240\345\217\212\347\262\276\345\272\246\347\273\223\346\236\234", 0, QApplication::UnicodeUTF8));
    } // retranslateUi

};

namespace Ui {
    class DialogCameraSurvey: public Ui_DialogCameraSurvey {};
} // namespace Ui

QT_END_NAMESPACE

#endif // UI_DIALOGCAMERASURVEY_H
