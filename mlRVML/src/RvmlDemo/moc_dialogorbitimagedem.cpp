/****************************************************************************
** Meta object code from reading C++ file 'dialogorbitimagedem.h'
**
** Created: Wed Feb 15 17:15:53 2012
**      by: The Qt Meta Object Compiler version 62 (Qt 4.7.2)
**
** WARNING! All changes made in this file will be lost!
*****************************************************************************/

#include "../../../src/RvmlDemo/dialogorbitimagedem.h"
#if !defined(Q_MOC_OUTPUT_REVISION)
#error "The header file 'dialogorbitimagedem.h' doesn't include <QObject>."
#elif Q_MOC_OUTPUT_REVISION != 62
#error "This file was generated using the moc from 4.7.2. It"
#error "cannot be used with the include files from this version of Qt."
#error "(The moc has changed too much.)"
#endif

QT_BEGIN_MOC_NAMESPACE
static const uint qt_meta_data_DialogOrbitImageDEM[] = {

 // content:
       5,       // revision
       0,       // classname
       0,    0, // classinfo
       6,   14, // methods
       0,    0, // properties
       0,    0, // enums/sets
       0,    0, // constructors
       0,       // flags
       0,       // signalCount

 // slots: signature, parameters, type, tag, flags
      21,   20,   20,   20, 0x08,
      51,   20,   20,   20, 0x08,
      82,   20,   20,   20, 0x08,
     113,   20,   20,   20, 0x08,
     137,   20,   20,   20, 0x08,
     185,  177,   20,   20, 0x08,

       0        // eod
};

static const char qt_meta_stringdata_DialogOrbitImageDEM[] = {
    "DialogOrbitImageDEM\0\0on_pushButtonSource_clicked()\0"
    "on_pushButtonDestDEM_clicked()\0"
    "on_pushButtonDestDOM_clicked()\0"
    "on_buttonBox_accepted()\0"
    "on_pushButtonSourcePointsFile_clicked()\0"
    "checked\0on_radioButtonUsePointsFile_toggled(bool)\0"
};

const QMetaObject DialogOrbitImageDEM::staticMetaObject = {
    { &QDialog::staticMetaObject, qt_meta_stringdata_DialogOrbitImageDEM,
      qt_meta_data_DialogOrbitImageDEM, 0 }
};

#ifdef Q_NO_DATA_RELOCATION
const QMetaObject &DialogOrbitImageDEM::getStaticMetaObject() { return staticMetaObject; }
#endif //Q_NO_DATA_RELOCATION

const QMetaObject *DialogOrbitImageDEM::metaObject() const
{
    return QObject::d_ptr->metaObject ? QObject::d_ptr->metaObject : &staticMetaObject;
}

void *DialogOrbitImageDEM::qt_metacast(const char *_clname)
{
    if (!_clname) return 0;
    if (!strcmp(_clname, qt_meta_stringdata_DialogOrbitImageDEM))
        return static_cast<void*>(const_cast< DialogOrbitImageDEM*>(this));
    return QDialog::qt_metacast(_clname);
}

int DialogOrbitImageDEM::qt_metacall(QMetaObject::Call _c, int _id, void **_a)
{
    _id = QDialog::qt_metacall(_c, _id, _a);
    if (_id < 0)
        return _id;
    if (_c == QMetaObject::InvokeMetaMethod) {
        switch (_id) {
        case 0: on_pushButtonSource_clicked(); break;
        case 1: on_pushButtonDestDEM_clicked(); break;
        case 2: on_pushButtonDestDOM_clicked(); break;
        case 3: on_buttonBox_accepted(); break;
        case 4: on_pushButtonSourcePointsFile_clicked(); break;
        case 5: on_radioButtonUsePointsFile_toggled((*reinterpret_cast< bool(*)>(_a[1]))); break;
        default: ;
        }
        _id -= 6;
    }
    return _id;
}
QT_END_MOC_NAMESPACE
