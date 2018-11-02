/********************************************************************************
** Form generated from reading UI file 'dialogpointcoord.ui'
**
** Created: Wed Feb 29 15:01:34 2012
**      by: Qt User Interface Compiler version 4.7.2
**
** WARNING! All changes made in this file will be lost when recompiling UI file!
********************************************************************************/

#ifndef UI_DIALOGPOINTCOORD_H
#define UI_DIALOGPOINTCOORD_H

#include <QtCore/QVariant>
#include <QtGui/QAction>
#include <QtGui/QApplication>
#include <QtGui/QButtonGroup>
#include <QtGui/QDialog>
#include <QtGui/QDoubleSpinBox>
#include <QtGui/QHeaderView>
#include <QtGui/QLabel>
#include <QtGui/QPushButton>

QT_BEGIN_NAMESPACE

class Ui_DialogPointCoord
{
public:
    QDoubleSpinBox *doubleSpinBox_X;
    QDoubleSpinBox *doubleSpinBox_Y;
    QLabel *label;
    QLabel *label_2;
    QPushButton *pushButton_OK;
    QPushButton *pushButton_Cancel;
    QLabel *label_Info;
    QLabel *label_ID;

    void setupUi(QDialog *DialogPointCoord)
    {
        if (DialogPointCoord->objectName().isEmpty())
            DialogPointCoord->setObjectName(QString::fromUtf8("DialogPointCoord"));
        DialogPointCoord->resize(335, 161);
        doubleSpinBox_X = new QDoubleSpinBox(DialogPointCoord);
        doubleSpinBox_X->setObjectName(QString::fromUtf8("doubleSpinBox_X"));
        doubleSpinBox_X->setGeometry(QRect(20, 20, 131, 27));
        doubleSpinBox_X->setReadOnly(true);
        doubleSpinBox_X->setDecimals(4);
        doubleSpinBox_X->setMinimum(-1e+07);
        doubleSpinBox_X->setMaximum(1e+07);
        doubleSpinBox_Y = new QDoubleSpinBox(DialogPointCoord);
        doubleSpinBox_Y->setObjectName(QString::fromUtf8("doubleSpinBox_Y"));
        doubleSpinBox_Y->setGeometry(QRect(180, 20, 131, 27));
        doubleSpinBox_Y->setReadOnly(true);
        doubleSpinBox_Y->setDecimals(4);
        doubleSpinBox_Y->setMinimum(-1e+07);
        doubleSpinBox_Y->setMaximum(1e+07);
        doubleSpinBox_Y->setValue(0);
        label = new QLabel(DialogPointCoord);
        label->setObjectName(QString::fromUtf8("label"));
        label->setGeometry(QRect(40, 0, 67, 17));
        label_2 = new QLabel(DialogPointCoord);
        label_2->setObjectName(QString::fromUtf8("label_2"));
        label_2->setGeometry(QRect(200, 0, 67, 17));
        pushButton_OK = new QPushButton(DialogPointCoord);
        pushButton_OK->setObjectName(QString::fromUtf8("pushButton_OK"));
        pushButton_OK->setGeometry(QRect(50, 110, 97, 27));
        pushButton_Cancel = new QPushButton(DialogPointCoord);
        pushButton_Cancel->setObjectName(QString::fromUtf8("pushButton_Cancel"));
        pushButton_Cancel->setGeometry(QRect(180, 110, 97, 27));
        label_Info = new QLabel(DialogPointCoord);
        label_Info->setObjectName(QString::fromUtf8("label_Info"));
        label_Info->setGeometry(QRect(30, 100, 281, 20));
        QFont font;
        font.setPointSize(14);
        label_Info->setFont(font);
        label_Info->setAlignment(Qt::AlignCenter);
        label_ID = new QLabel(DialogPointCoord);
        label_ID->setObjectName(QString::fromUtf8("label_ID"));
        label_ID->setGeometry(QRect(30, 50, 281, 20));
        QPalette palette;
        QBrush brush(QColor(255, 0, 0, 255));
        brush.setStyle(Qt::SolidPattern);
        palette.setBrush(QPalette::Active, QPalette::WindowText, brush);
        palette.setBrush(QPalette::Active, QPalette::Text, brush);
        palette.setBrush(QPalette::Inactive, QPalette::WindowText, brush);
        palette.setBrush(QPalette::Inactive, QPalette::Text, brush);
        QBrush brush1(QColor(159, 158, 158, 255));
        brush1.setStyle(Qt::SolidPattern);
        palette.setBrush(QPalette::Disabled, QPalette::WindowText, brush1);
        palette.setBrush(QPalette::Disabled, QPalette::Text, brush1);
        label_ID->setPalette(palette);
        label_ID->setFont(font);
        label_ID->setAlignment(Qt::AlignCenter);

        retranslateUi(DialogPointCoord);
        QObject::connect(pushButton_OK, SIGNAL(clicked()), DialogPointCoord, SLOT(accept()));
        QObject::connect(pushButton_Cancel, SIGNAL(clicked()), DialogPointCoord, SLOT(reject()));

        QMetaObject::connectSlotsByName(DialogPointCoord);
    } // setupUi

    void retranslateUi(QDialog *DialogPointCoord)
    {
        DialogPointCoord->setWindowTitle(QApplication::translate("DialogPointCoord", "Add Stereo Point", 0, QApplication::UnicodeUTF8));
        label->setText(QApplication::translate("DialogPointCoord", "X", 0, QApplication::UnicodeUTF8));
        label_2->setText(QApplication::translate("DialogPointCoord", "Y", 0, QApplication::UnicodeUTF8));
        pushButton_OK->setText(QApplication::translate("DialogPointCoord", "OK", 0, QApplication::UnicodeUTF8));
        pushButton_Cancel->setText(QApplication::translate("DialogPointCoord", "Cancel", 0, QApplication::UnicodeUTF8));
        label_Info->setText(QString());
        label_ID->setText(QApplication::translate("DialogPointCoord", "ID:0", 0, QApplication::UnicodeUTF8));
    } // retranslateUi

};

namespace Ui {
    class DialogPointCoord: public Ui_DialogPointCoord {};
} // namespace Ui

QT_END_NAMESPACE

#endif // UI_DIALOGPOINTCOORD_H
