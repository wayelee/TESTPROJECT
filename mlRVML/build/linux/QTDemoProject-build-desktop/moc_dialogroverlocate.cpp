/****************************************************************************
** Meta object code from reading C++ file 'dialogroverlocate.h'
**
** Created: Wed Feb 8 17:54:38 2012
**      by: The Qt Meta Object Compiler version 62 (Qt 4.7.2)
**
** WARNING! All changes made in this file will be lost!
*****************************************************************************/

#include "../../../src/RvmlDemo/dialogroverlocate.h"
#if !defined(Q_MOC_OUTPUT_REVISION)
#error "The header file 'dialogroverlocate.h' doesn't include <QObject>."
#elif Q_MOC_OUTPUT_REVISION != 62
#error "This file was generated using the moc from 4.7.2. It"
#error "cannot be used with the include files from this version of Qt."
#error "(The moc has changed too much.)"
#endif

QT_BEGIN_MOC_NAMESPACE
static const uint qt_meta_data_DialogRoverLocate[] = {

 // content:
       5,       // revision
       0,       // classname
       0,    0, // classinfo
       3,   14, // methods
       0,    0, // properties
       0,    0, // enums/sets
       0,    0, // constructors
       0,       // flags
       0,       // signalCount

 // slots: signature, parameters, type, tag, flags
      19,   18,   18,   18, 0x08,
      49,   18,   18,   18, 0x08,
      77,   18,   18,   18, 0x08,

       0        // eod
};

static const char qt_meta_stringdata_DialogRoverLocate[] = {
    "DialogRoverLocate\0\0on_pushButtonSource_clicked()\0"
    "on_pushButtonDest_clicked()\0"
    "on_buttonBox_accepted()\0"
};

const QMetaObject DialogRoverLocate::staticMetaObject = {
    { &QDialog::staticMetaObject, qt_meta_stringdata_DialogRoverLocate,
      qt_meta_data_DialogRoverLocate, 0 }
};

#ifdef Q_NO_DATA_RELOCATION
const QMetaObject &DialogRoverLocate::getStaticMetaObject() { return staticMetaObject; }
#endif //Q_NO_DATA_RELOCATION

const QMetaObject *DialogRoverLocate::metaObject() const
{
    return QObject::d_ptr->metaObject ? QObject::d_ptr->metaObject : &staticMetaObject;
}

void *DialogRoverLocate::qt_metacast(const char *_clname)
{
    if (!_clname) return 0;
    if (!strcmp(_clname, qt_meta_stringdata_DialogRoverLocate))
        return static_cast<void*>(const_cast< DialogRoverLocate*>(this));
    return QDialog::qt_metacast(_clname);
}

int DialogRoverLocate::qt_metacall(QMetaObject::Call _c, int _id, void **_a)
{
    _id = QDialog::qt_metacall(_c, _id, _a);
    if (_id < 0)
        return _id;
    if (_c == QMetaObject::InvokeMetaMethod) {
        switch (_id) {
        case 0: on_pushButtonSource_clicked(); break;
        case 1: on_pushButtonDest_clicked(); break;
        case 2: on_buttonBox_accepted(); break;
        default: ;
        }
        _id -= 3;
    }
    return _id;
}
QT_END_MOC_NAMESPACE
