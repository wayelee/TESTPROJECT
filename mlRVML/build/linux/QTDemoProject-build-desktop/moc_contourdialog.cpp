/****************************************************************************
** Meta object code from reading C++ file 'contourdialog.h'
**
** Created: Wed Feb 8 17:54:24 2012
**      by: The Qt Meta Object Compiler version 62 (Qt 4.7.2)
**
** WARNING! All changes made in this file will be lost!
*****************************************************************************/

#include "../../../src/RvmlDemo/contourdialog.h"
#if !defined(Q_MOC_OUTPUT_REVISION)
#error "The header file 'contourdialog.h' doesn't include <QObject>."
#elif Q_MOC_OUTPUT_REVISION != 62
#error "This file was generated using the moc from 4.7.2. It"
#error "cannot be used with the include files from this version of Qt."
#error "(The moc has changed too much.)"
#endif

QT_BEGIN_MOC_NAMESPACE
static const uint qt_meta_data_ContourDialog[] = {

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
      15,   14,   14,   14, 0x08,
      45,   14,   14,   14, 0x08,
      73,   14,   14,   14, 0x08,
     112,   14,   14,   14, 0x08,
     149,   14,   14,   14, 0x08,
     195,   14,   14,   14, 0x08,

       0        // eod
};

static const char qt_meta_stringdata_ContourDialog[] = {
    "ContourDialog\0\0on_pushButtonSource_clicked()\0"
    "on_pushButtonDest_clicked()\0"
    "on_lineEditSource_textChanged(QString)\0"
    "on_lineEditDest_textChanged(QString)\0"
    "on_doubleSpinBoxInterval_valueChanged(double)\0"
    "on_ContourDialog_accepted()\0"
};

const QMetaObject ContourDialog::staticMetaObject = {
    { &QDialog::staticMetaObject, qt_meta_stringdata_ContourDialog,
      qt_meta_data_ContourDialog, 0 }
};

#ifdef Q_NO_DATA_RELOCATION
const QMetaObject &ContourDialog::getStaticMetaObject() { return staticMetaObject; }
#endif //Q_NO_DATA_RELOCATION

const QMetaObject *ContourDialog::metaObject() const
{
    return QObject::d_ptr->metaObject ? QObject::d_ptr->metaObject : &staticMetaObject;
}

void *ContourDialog::qt_metacast(const char *_clname)
{
    if (!_clname) return 0;
    if (!strcmp(_clname, qt_meta_stringdata_ContourDialog))
        return static_cast<void*>(const_cast< ContourDialog*>(this));
    return QDialog::qt_metacast(_clname);
}

int ContourDialog::qt_metacall(QMetaObject::Call _c, int _id, void **_a)
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
        case 4: on_doubleSpinBoxInterval_valueChanged((*reinterpret_cast< double(*)>(_a[1]))); break;
        case 5: on_ContourDialog_accepted(); break;
        default: ;
        }
        _id -= 6;
    }
    return _id;
}
QT_END_MOC_NAMESPACE
