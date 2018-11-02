/****************************************************************************
** Meta object code from reading C++ file 'dialogrelativeorientation.h'
**
** Created: Sun May 27 18:24:11 2012
**      by: The Qt Meta Object Compiler version 62 (Qt 4.7.2)
**
** WARNING! All changes made in this file will be lost!
*****************************************************************************/

#include "../../../src/RvmlDemo/dialogrelativeorientation.h"
#if !defined(Q_MOC_OUTPUT_REVISION)
#error "The header file 'dialogrelativeorientation.h' doesn't include <QObject>."
#elif Q_MOC_OUTPUT_REVISION != 62
#error "This file was generated using the moc from 4.7.2. It"
#error "cannot be used with the include files from this version of Qt."
#error "(The moc has changed too much.)"
#endif

QT_BEGIN_MOC_NAMESPACE
static const uint qt_meta_data_DialogRelativeOrientation[] = {

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
      27,   26,   26,   26, 0x08,
      61,   26,   26,   26, 0x08,
      94,   26,   26,   26, 0x08,
     122,   26,   26,   26, 0x08,

       0        // eod
};

static const char qt_meta_stringdata_DialogRelativeOrientation[] = {
    "DialogRelativeOrientation\0\0"
    "on_pushButtonProjSource_clicked()\0"
    "on_pushButtonDOMSource_clicked()\0"
    "on_pushButtonDest_clicked()\0"
    "on_buttonBox_accepted()\0"
};

const QMetaObject DialogRelativeOrientation::staticMetaObject = {
    { &QDialog::staticMetaObject, qt_meta_stringdata_DialogRelativeOrientation,
      qt_meta_data_DialogRelativeOrientation, 0 }
};

#ifdef Q_NO_DATA_RELOCATION
const QMetaObject &DialogRelativeOrientation::getStaticMetaObject() { return staticMetaObject; }
#endif //Q_NO_DATA_RELOCATION

const QMetaObject *DialogRelativeOrientation::metaObject() const
{
    return QObject::d_ptr->metaObject ? QObject::d_ptr->metaObject : &staticMetaObject;
}

void *DialogRelativeOrientation::qt_metacast(const char *_clname)
{
    if (!_clname) return 0;
    if (!strcmp(_clname, qt_meta_stringdata_DialogRelativeOrientation))
        return static_cast<void*>(const_cast< DialogRelativeOrientation*>(this));
    return QDialog::qt_metacast(_clname);
}

int DialogRelativeOrientation::qt_metacall(QMetaObject::Call _c, int _id, void **_a)
{
    _id = QDialog::qt_metacall(_c, _id, _a);
    if (_id < 0)
        return _id;
    if (_c == QMetaObject::InvokeMetaMethod) {
        switch (_id) {
        case 0: on_pushButtonProjSource_clicked(); break;
        case 1: on_pushButtonDOMSource_clicked(); break;
        case 2: on_pushButtonDest_clicked(); break;
        case 3: on_buttonBox_accepted(); break;
        default: ;
        }
        _id -= 4;
    }
    return _id;
}
QT_END_MOC_NAMESPACE
