/****************************************************************************
** Meta object code from reading C++ file 'obstacledialog.h'
**
** Created: Wed Feb 8 17:54:30 2012
**      by: The Qt Meta Object Compiler version 62 (Qt 4.7.2)
**
** WARNING! All changes made in this file will be lost!
*****************************************************************************/

#include "../../../src/RvmlDemo/obstacledialog.h"
#if !defined(Q_MOC_OUTPUT_REVISION)
#error "The header file 'obstacledialog.h' doesn't include <QObject>."
#elif Q_MOC_OUTPUT_REVISION != 62
#error "This file was generated using the moc from 4.7.2. It"
#error "cannot be used with the include files from this version of Qt."
#error "(The moc has changed too much.)"
#endif

QT_BEGIN_MOC_NAMESPACE
static const uint qt_meta_data_ObstacleDialog[] = {

 // content:
       5,       // revision
       0,       // classname
       0,    0, // classinfo
      15,   14, // methods
       0,    0, // properties
       0,    0, // enums/sets
       0,    0, // constructors
       0,       // flags
       0,       // signalCount

 // slots: signature, parameters, type, tag, flags
      16,   15,   15,   15, 0x08,
      46,   15,   15,   15, 0x08,
      74,   15,   15,   15, 0x08,
     113,   15,   15,   15, 0x08,
     150,   15,   15,   15, 0x08,
     198,   15,   15,   15, 0x08,
     243,   15,   15,   15, 0x08,
     272,   15,   15,   15, 0x08,
     315,   15,   15,   15, 0x08,
     362,   15,   15,   15, 0x08,
     409,   15,   15,   15, 0x08,
     460,   15,   15,   15, 0x08,
     502,   15,   15,   15, 0x08,
     548,   15,   15,   15, 0x08,
     599,   15,   15,   15, 0x08,

       0        // eod
};

static const char qt_meta_stringdata_ObstacleDialog[] = {
    "ObstacleDialog\0\0on_pushButtonSource_clicked()\0"
    "on_pushButtonDest_clicked()\0"
    "on_lineEditSource_textChanged(QString)\0"
    "on_lineEditDest_textChanged(QString)\0"
    "on_doubleSpinBoxWindowSize_valueChanged(double)\0"
    "on_doubleSpinBoxZfactor_valueChanged(double)\0"
    "on_ObstacleDialog_accepted()\0"
    "on_doubleSpinBoxSlope_valueChanged(double)\0"
    "on_doubleSpinBoxSlopeCoef_valueChanged(double)\0"
    "on_doubleSpinBoxRoughness_valueChanged(double)\0"
    "on_doubleSpinBoxRoughnessCoef_valueChanged(double)\0"
    "on_doubleSpinBoxStep_valueChanged(double)\0"
    "on_doubleSpinBoxStepCoef_valueChanged(double)\0"
    "on_doubleSpinBoxObstacleValue_valueChanged(double)\0"
    "on_buttonBox_accepted()\0"
};

const QMetaObject ObstacleDialog::staticMetaObject = {
    { &QDialog::staticMetaObject, qt_meta_stringdata_ObstacleDialog,
      qt_meta_data_ObstacleDialog, 0 }
};

#ifdef Q_NO_DATA_RELOCATION
const QMetaObject &ObstacleDialog::getStaticMetaObject() { return staticMetaObject; }
#endif //Q_NO_DATA_RELOCATION

const QMetaObject *ObstacleDialog::metaObject() const
{
    return QObject::d_ptr->metaObject ? QObject::d_ptr->metaObject : &staticMetaObject;
}

void *ObstacleDialog::qt_metacast(const char *_clname)
{
    if (!_clname) return 0;
    if (!strcmp(_clname, qt_meta_stringdata_ObstacleDialog))
        return static_cast<void*>(const_cast< ObstacleDialog*>(this));
    return QDialog::qt_metacast(_clname);
}

int ObstacleDialog::qt_metacall(QMetaObject::Call _c, int _id, void **_a)
{
    _id = QDialog::qt_metacall(_c, _id, _a);
    if (_id < 0)
        return _id;
    if (_c == QMetaObject::InvokeMetaMethod) {
        switch (_id) {
        case 0: on_pushButtonSource_clicked(); break;
        case 1: on_pushButtonDest_clicked(); break;
        case 2: on_lineEditSource_textChanged((*reinterpret_cast< QString(*)>(_a[1]))); break;
        case 3: on_lineEditDest_textChanged((*reinterpret_cast< QString(*)>(_a[1]))); break;
        case 4: on_doubleSpinBoxWindowSize_valueChanged((*reinterpret_cast< double(*)>(_a[1]))); break;
        case 5: on_doubleSpinBoxZfactor_valueChanged((*reinterpret_cast< double(*)>(_a[1]))); break;
        case 6: on_ObstacleDialog_accepted(); break;
        case 7: on_doubleSpinBoxSlope_valueChanged((*reinterpret_cast< double(*)>(_a[1]))); break;
        case 8: on_doubleSpinBoxSlopeCoef_valueChanged((*reinterpret_cast< double(*)>(_a[1]))); break;
        case 9: on_doubleSpinBoxRoughness_valueChanged((*reinterpret_cast< double(*)>(_a[1]))); break;
        case 10: on_doubleSpinBoxRoughnessCoef_valueChanged((*reinterpret_cast< double(*)>(_a[1]))); break;
        case 11: on_doubleSpinBoxStep_valueChanged((*reinterpret_cast< double(*)>(_a[1]))); break;
        case 12: on_doubleSpinBoxStepCoef_valueChanged((*reinterpret_cast< double(*)>(_a[1]))); break;
        case 13: on_doubleSpinBoxObstacleValue_valueChanged((*reinterpret_cast< double(*)>(_a[1]))); break;
        case 14: on_buttonBox_accepted(); break;
        default: ;
        }
        _id -= 15;
    }
    return _id;
}
QT_END_MOC_NAMESPACE
