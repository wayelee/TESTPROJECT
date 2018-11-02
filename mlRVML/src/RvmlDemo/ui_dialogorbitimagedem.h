/********************************************************************************
** Form generated from reading UI file 'dialogorbitimagedem.ui'
**
** Created: Wed Feb 29 15:41:31 2012
**      by: Qt User Interface Compiler version 4.7.2
**
** WARNING! All changes made in this file will be lost when recompiling UI file!
********************************************************************************/

#ifndef UI_DIALOGORBITIMAGEDEM_H
#define UI_DIALOGORBITIMAGEDEM_H

#include <QtCore/QVariant>
#include <QtGui/QAction>
#include <QtGui/QApplication>
#include <QtGui/QButtonGroup>
#include <QtGui/QDialog>
#include <QtGui/QDialogButtonBox>
#include <QtGui/QDoubleSpinBox>
#include <QtGui/QFrame>
#include <QtGui/QHeaderView>
#include <QtGui/QLabel>
#include <QtGui/QLineEdit>
#include <QtGui/QPushButton>
#include <QtGui/QRadioButton>

QT_BEGIN_NAMESPACE

class Ui_DialogOrbitImageDEM
{
public:
    QDialogButtonBox *buttonBox;
    QPushButton *pushButtonSource;
    QLabel *label_4;
    QLineEdit *lineEditDestDEM;
    QLabel *label_5;
    QPushButton *pushButtonDestDEM;
    QLineEdit *lineEditSource;
    QLabel *label;
    QLineEdit *lineEditDestDOM;
    QPushButton *pushButtonDestDOM;
    QDoubleSpinBox *doubleSpinBoxResolution;
    QLabel *label_6;
    QRadioButton *radioButtonLeft;
    QRadioButton *radioButtonRight;
    QLabel *label_2;
    QFrame *frame;
    QRadioButton *radioButtonUsePointsFile;
    QRadioButton *radioButtonNotUsePointsFile;
    QLineEdit *lineEditSourcePointsFile;
    QPushButton *pushButtonSourcePointsFile;

    void setupUi(QDialog *DialogOrbitImageDEM)
    {
        if (DialogOrbitImageDEM->objectName().isEmpty())
            DialogOrbitImageDEM->setObjectName(QString::fromUtf8("DialogOrbitImageDEM"));
        DialogOrbitImageDEM->resize(570, 536);
        buttonBox = new QDialogButtonBox(DialogOrbitImageDEM);
        buttonBox->setObjectName(QString::fromUtf8("buttonBox"));
        buttonBox->setGeometry(QRect(470, 30, 81, 241));
        buttonBox->setOrientation(Qt::Vertical);
        buttonBox->setStandardButtons(QDialogButtonBox::Cancel|QDialogButtonBox::Ok);
        pushButtonSource = new QPushButton(DialogOrbitImageDEM);
        pushButtonSource->setObjectName(QString::fromUtf8("pushButtonSource"));
        pushButtonSource->setGeometry(QRect(440, 106, 41, 31));
        label_4 = new QLabel(DialogOrbitImageDEM);
        label_4->setObjectName(QString::fromUtf8("label_4"));
        label_4->setGeometry(QRect(40, 270, 301, 17));
        lineEditDestDEM = new QLineEdit(DialogOrbitImageDEM);
        lineEditDestDEM->setObjectName(QString::fromUtf8("lineEditDestDEM"));
        lineEditDestDEM->setGeometry(QRect(20, 300, 381, 27));
        label_5 = new QLabel(DialogOrbitImageDEM);
        label_5->setObjectName(QString::fromUtf8("label_5"));
        label_5->setGeometry(QRect(40, 340, 301, 17));
        pushButtonDestDEM = new QPushButton(DialogOrbitImageDEM);
        pushButtonDestDEM->setObjectName(QString::fromUtf8("pushButtonDestDEM"));
        pushButtonDestDEM->setGeometry(QRect(440, 300, 41, 31));
        lineEditSource = new QLineEdit(DialogOrbitImageDEM);
        lineEditSource->setObjectName(QString::fromUtf8("lineEditSource"));
        lineEditSource->setGeometry(QRect(20, 110, 391, 27));
        label = new QLabel(DialogOrbitImageDEM);
        label->setObjectName(QString::fromUtf8("label"));
        label->setGeometry(QRect(40, 70, 141, 17));
        lineEditDestDOM = new QLineEdit(DialogOrbitImageDEM);
        lineEditDestDOM->setObjectName(QString::fromUtf8("lineEditDestDOM"));
        lineEditDestDOM->setGeometry(QRect(20, 370, 381, 27));
        pushButtonDestDOM = new QPushButton(DialogOrbitImageDEM);
        pushButtonDestDOM->setObjectName(QString::fromUtf8("pushButtonDestDOM"));
        pushButtonDestDOM->setGeometry(QRect(440, 370, 41, 31));
        doubleSpinBoxResolution = new QDoubleSpinBox(DialogOrbitImageDEM);
        doubleSpinBoxResolution->setObjectName(QString::fromUtf8("doubleSpinBoxResolution"));
        doubleSpinBoxResolution->setGeometry(QRect(50, 460, 111, 27));
        doubleSpinBoxResolution->setDecimals(4);
        doubleSpinBoxResolution->setMaximum(10000);
        doubleSpinBoxResolution->setValue(150);
        label_6 = new QLabel(DialogOrbitImageDEM);
        label_6->setObjectName(QString::fromUtf8("label_6"));
        label_6->setGeometry(QRect(60, 440, 301, 17));
        radioButtonLeft = new QRadioButton(DialogOrbitImageDEM);
        radioButtonLeft->setObjectName(QString::fromUtf8("radioButtonLeft"));
        radioButtonLeft->setGeometry(QRect(260, 460, 116, 22));
        radioButtonLeft->setChecked(true);
        radioButtonRight = new QRadioButton(DialogOrbitImageDEM);
        radioButtonRight->setObjectName(QString::fromUtf8("radioButtonRight"));
        radioButtonRight->setGeometry(QRect(260, 500, 116, 22));
        label_2 = new QLabel(DialogOrbitImageDEM);
        label_2->setObjectName(QString::fromUtf8("label_2"));
        label_2->setGeometry(QRect(180, 470, 41, 17));
        frame = new QFrame(DialogOrbitImageDEM);
        frame->setObjectName(QString::fromUtf8("frame"));
        frame->setGeometry(QRect(10, 150, 411, 81));
        frame->setFrameShape(QFrame::NoFrame);
        frame->setFrameShadow(QFrame::Raised);
        radioButtonUsePointsFile = new QRadioButton(frame);
        radioButtonUsePointsFile->setObjectName(QString::fromUtf8("radioButtonUsePointsFile"));
        radioButtonUsePointsFile->setGeometry(QRect(40, 10, 181, 22));
        radioButtonNotUsePointsFile = new QRadioButton(frame);
        radioButtonNotUsePointsFile->setObjectName(QString::fromUtf8("radioButtonNotUsePointsFile"));
        radioButtonNotUsePointsFile->setGeometry(QRect(220, 10, 116, 22));
        radioButtonNotUsePointsFile->setChecked(true);
        lineEditSourcePointsFile = new QLineEdit(frame);
        lineEditSourcePointsFile->setObjectName(QString::fromUtf8("lineEditSourcePointsFile"));
        lineEditSourcePointsFile->setGeometry(QRect(10, 40, 391, 27));
        pushButtonSourcePointsFile = new QPushButton(DialogOrbitImageDEM);
        pushButtonSourcePointsFile->setObjectName(QString::fromUtf8("pushButtonSourcePointsFile"));
        pushButtonSourcePointsFile->setGeometry(QRect(440, 190, 41, 31));

        retranslateUi(DialogOrbitImageDEM);
        QObject::connect(buttonBox, SIGNAL(accepted()), DialogOrbitImageDEM, SLOT(accept()));
        QObject::connect(buttonBox, SIGNAL(rejected()), DialogOrbitImageDEM, SLOT(reject()));

        QMetaObject::connectSlotsByName(DialogOrbitImageDEM);
    } // setupUi

    void retranslateUi(QDialog *DialogOrbitImageDEM)
    {
        DialogOrbitImageDEM->setWindowTitle(QApplication::translate("DialogOrbitImageDEM", "Dialog", 0, QApplication::UnicodeUTF8));
        pushButtonSource->setText(QApplication::translate("DialogOrbitImageDEM", "...", 0, QApplication::UnicodeUTF8));
        label_4->setText(QApplication::translate("DialogOrbitImageDEM", "\350\276\223\345\207\272DEM\346\226\207\344\273\266", 0, QApplication::UnicodeUTF8));
        label_5->setText(QApplication::translate("DialogOrbitImageDEM", "\350\276\223\345\207\272DOM\346\226\207\344\273\266", 0, QApplication::UnicodeUTF8));
        pushButtonDestDEM->setText(QApplication::translate("DialogOrbitImageDEM", "...", 0, QApplication::UnicodeUTF8));
        label->setText(QApplication::translate("DialogOrbitImageDEM", "\350\276\223\345\205\245\346\226\207\344\273\266\350\267\257\345\276\204", 0, QApplication::UnicodeUTF8));
        pushButtonDestDOM->setText(QApplication::translate("DialogOrbitImageDEM", "...", 0, QApplication::UnicodeUTF8));
        label_6->setText(QApplication::translate("DialogOrbitImageDEM", "DEM\345\233\276\345\203\217\345\210\206\350\276\250\347\216\207", 0, QApplication::UnicodeUTF8));
        radioButtonLeft->setText(QApplication::translate("DialogOrbitImageDEM", "\344\273\245\345\267\246\347\211\207\344\270\272\345\237\272\345\207\206", 0, QApplication::UnicodeUTF8));
        radioButtonRight->setText(QApplication::translate("DialogOrbitImageDEM", "\344\273\245\345\217\263\347\211\207\344\270\272\345\237\272\345\207\206", 0, QApplication::UnicodeUTF8));
        label_2->setText(QApplication::translate("DialogOrbitImageDEM", "\347\261\263", 0, QApplication::UnicodeUTF8));
        radioButtonUsePointsFile->setText(QApplication::translate("DialogOrbitImageDEM", "\344\275\277\347\224\250\345\267\262\346\234\211\345\214\271\351\205\215\347\202\271\346\226\207\344\273\266", 0, QApplication::UnicodeUTF8));
        radioButtonNotUsePointsFile->setText(QApplication::translate("DialogOrbitImageDEM", "\351\207\215\346\226\260\345\214\271\351\205\215", 0, QApplication::UnicodeUTF8));
        pushButtonSourcePointsFile->setText(QApplication::translate("DialogOrbitImageDEM", "...", 0, QApplication::UnicodeUTF8));
    } // retranslateUi

};

namespace Ui {
    class DialogOrbitImageDEM: public Ui_DialogOrbitImageDEM {};
} // namespace Ui

QT_END_NAMESPACE

#endif // UI_DIALOGORBITIMAGEDEM_H
