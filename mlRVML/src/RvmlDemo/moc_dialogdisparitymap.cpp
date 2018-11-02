/****************************************************************************
** Meta object code from reading C++ file 'dialogdisparitymap.h'
**
** Created: Fri Feb 10 16:55:34 2012
**      by: The Qt Meta Object Compiler version 62 (Qt 4.7.2)
**
** WARNING! All changes made in this file will be lost!
*****************************************************************************/

#include "../../../src/RvmlDemo/dialogdisparitymap.h"
#if !defined(Q_MOC_OUTPUT_REVISION)
#error "The header file 'dialogdisparitymap.h' doesn't include <QObject>."
#elif Q_MOC_OUTPUT_REVISION != 62
#error "This file was generated using the moc from 4.7.2. It"
#error "cannot be used with the include files from this version of Qt."
#error "(The moc has changed too much.)"
#endif

QT_BEGIN_MOC_NAMESPACE
static const uint qt_meta_data_DialogDisparityMap[] = {

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
      20,   19,   19,   19, 0x08,
      54,   19,   19,   19, 0x08,
      89,   19,   19,   19, 0x08,
     117,   19,   19,   19, 0x08,

       0        // eod
};

static const char qt_meta_stringdata_DialogDisparityMap[] = {
    "DialogDisparityMap\0\0"
    "on_pushButtonSourceLeft_clicked()\0"
    "on_pushButtonSourceRight_clicked()\0"
    "on_pushButtonDest_clicked()\0"
    "on_buttonBox_accepted()\0"
};

const QMetaObject DialogDisparityMap::staticMetaObject = {
    { &QDialog::staticMetaObject, qt_meta_stringdata_DialogDisparityMap,
      qt_meta_data_DialogDisparityMap, 0 }
};

#ifdef Q_NO_DATA_RELOCATION
const QMetaObject &DialogDisparityMap::getStaticMetaObject() { return staticMetaObject; }
#endif //Q_NO_DATA_RELOCATION

const QMetaObject *DialogDisparityMap::metaObject() const
{
    return QObject::d_ptr->metaObject ? QObject::d_ptr->metaObject : &staticMetaObject;
}

void *DialogDisparityMap::qt_metacast(const char *_clname)
{
    if (!_clname) return 0;
    if (!strcmp(_clname, qt_meta_stringdata_DialogDisparityMap))
        return static_cast<void*>(const_cast< DialogDisparityMap*>(this));
    return QDialog::qt_metacast(_clname);
}

int DialogDisparityMap::qt_metacall(QMetaObject::Call _c, int _id, void **_a)
{
    _id = QDialog::qt_metacall(_c, _id, _a);
    if (_id < 0)
        return _id;
    if (_c == QMetaObject::InvokeMetaMethod) {
        switch (_id) {
        case 0: on_pushButtonSourceLeft_clicked(); break;
        case 1: on_pushButtonSourceRight_clicked(); break;
        case 2: on_pushButtonDest_clicked(); break;
        case 3: on_buttonBox_accepted(); break;
        default: ;
        }
        _id -= 4;
    }
    return _id;
}
QT_END_MOC_NAMESPACE
