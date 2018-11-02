/****************************************************************************
** Meta object code from reading C++ file 'dialogcamera.h'
**
** Created: Mon Feb 13 14:42:11 2012
**      by: The Qt Meta Object Compiler version 62 (Qt 4.7.2)
**
** WARNING! All changes made in this file will be lost!
*****************************************************************************/

#include "../../../src/RvmlDemo/dialogcamera.h"
#if !defined(Q_MOC_OUTPUT_REVISION)
#error "The header file 'dialogcamera.h' doesn't include <QObject>."
#elif Q_MOC_OUTPUT_REVISION != 62
#error "This file was generated using the moc from 4.7.2. It"
#error "cannot be used with the include files from this version of Qt."
#error "(The moc has changed too much.)"
#endif

QT_BEGIN_MOC_NAMESPACE
static const uint qt_meta_data_DialogCamera[] = {

 // content:
       5,       // revision
       0,       // classname
       0,    0, // classinfo
       4,   14, // methods
       0,    0, // properties
       0,    0, // enums/sets
       0,    0, // constructors
       0,       // flags
       0,       // signalCount

 // slots: signature, parameters, type, tag, flags
      14,   13,   13,   13, 0x08,
      44,   13,   13,   13, 0x08,
      72,   13,   13,   13, 0x08,
      96,   13,   13,   13, 0x08,

       0        // eod
};

static const char qt_meta_stringdata_DialogCamera[] = {
    "DialogCamera\0\0on_pushButtonSource_clicked()\0"
    "on_pushButtonDest_clicked()\0"
    "on_buttonBox_accepted()\0"
    "on_pushButtonDestAccReport_clicked()\0"
};

const QMetaObject DialogCamera::staticMetaObject = {
    { &QDialog::staticMetaObject, qt_meta_stringdata_DialogCamera,
      qt_meta_data_DialogCamera, 0 }
};

#ifdef Q_NO_DATA_RELOCATION
const QMetaObject &DialogCamera::getStaticMetaObject() { return staticMetaObject; }
#endif //Q_NO_DATA_RELOCATION

const QMetaObject *DialogCamera::metaObject() const
{
    return QObject::d_ptr->metaObject ? QObject::d_ptr->metaObject : &staticMetaObject;
}

void *DialogCamera::qt_metacast(const char *_clname)
{
    if (!_clname) return 0;
    if (!strcmp(_clname, qt_meta_stringdata_DialogCamera))
        return static_cast<void*>(const_cast< DialogCamera*>(this));
    return QDialog::qt_metacast(_clname);
}

int DialogCamera::qt_metacall(QMetaObject::Call _c, int _id, void **_a)
{
    _id = QDialog::qt_metacall(_c, _id, _a);
    if (_id < 0)
        return _id;
    if (_c == QMetaObject::InvokeMetaMethod) {
        switch (_id) {
        case 0: on_pushButtonSource_clicked(); break;
        case 1: on_pushButtonDest_clicked(); break;
        case 2: on_buttonBox_accepted(); break;
        case 3: on_pushButtonDestAccReport_clicked(); break;
        default: ;
        }
        _id -= 4;
    }
    return _id;
}
QT_END_MOC_NAMESPACE
