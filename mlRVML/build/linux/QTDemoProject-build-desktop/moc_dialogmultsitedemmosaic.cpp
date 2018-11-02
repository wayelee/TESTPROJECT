/****************************************************************************
** Meta object code from reading C++ file 'dialogmultsitedemmosaic.h'
**
** Created: Wed Feb 8 17:54:33 2012
**      by: The Qt Meta Object Compiler version 62 (Qt 4.7.2)
**
** WARNING! All changes made in this file will be lost!
*****************************************************************************/

#include "../../../src/RvmlDemo/dialogmultsitedemmosaic.h"
#if !defined(Q_MOC_OUTPUT_REVISION)
#error "The header file 'dialogmultsitedemmosaic.h' doesn't include <QObject>."
#elif Q_MOC_OUTPUT_REVISION != 62
#error "This file was generated using the moc from 4.7.2. It"
#error "cannot be used with the include files from this version of Qt."
#error "(The moc has changed too much.)"
#endif

QT_BEGIN_MOC_NAMESPACE
static const uint qt_meta_data_DialogMultSiteDemMosaic[] = {

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
      25,   24,   24,   24, 0x08,
      53,   24,   24,   24, 0x08,
      87,   24,   24,   24, 0x08,
     118,   24,   24,   24, 0x08,

       0        // eod
};

static const char qt_meta_stringdata_DialogMultSiteDemMosaic[] = {
    "DialogMultSiteDemMosaic\0\0"
    "on_pushButtonDest_clicked()\0"
    "on_pushButtonDeleteFile_clicked()\0"
    "on_pushButtonAddFile_clicked()\0"
    "on_buttonBox_accepted()\0"
};

const QMetaObject DialogMultSiteDemMosaic::staticMetaObject = {
    { &QDialog::staticMetaObject, qt_meta_stringdata_DialogMultSiteDemMosaic,
      qt_meta_data_DialogMultSiteDemMosaic, 0 }
};

#ifdef Q_NO_DATA_RELOCATION
const QMetaObject &DialogMultSiteDemMosaic::getStaticMetaObject() { return staticMetaObject; }
#endif //Q_NO_DATA_RELOCATION

const QMetaObject *DialogMultSiteDemMosaic::metaObject() const
{
    return QObject::d_ptr->metaObject ? QObject::d_ptr->metaObject : &staticMetaObject;
}

void *DialogMultSiteDemMosaic::qt_metacast(const char *_clname)
{
    if (!_clname) return 0;
    if (!strcmp(_clname, qt_meta_stringdata_DialogMultSiteDemMosaic))
        return static_cast<void*>(const_cast< DialogMultSiteDemMosaic*>(this));
    return QDialog::qt_metacast(_clname);
}

int DialogMultSiteDemMosaic::qt_metacall(QMetaObject::Call _c, int _id, void **_a)
{
    _id = QDialog::qt_metacall(_c, _id, _a);
    if (_id < 0)
        return _id;
    if (_c == QMetaObject::InvokeMetaMethod) {
        switch (_id) {
        case 0: on_pushButtonDest_clicked(); break;
        case 1: on_pushButtonDeleteFile_clicked(); break;
        case 2: on_pushButtonAddFile_clicked(); break;
        case 3: on_buttonBox_accepted(); break;
        default: ;
        }
        _id -= 4;
    }
    return _id;
}
QT_END_MOC_NAMESPACE
