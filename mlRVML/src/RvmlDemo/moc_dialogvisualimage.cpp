/****************************************************************************
** Meta object code from reading C++ file 'dialogvisualimage.h'
**
** Created: Sat Jan 7 17:18:00 2012
**      by: The Qt Meta Object Compiler version 62 (Qt 4.7.2)
**
** WARNING! All changes made in this file will be lost!
*****************************************************************************/

#include "../../../src/RvmlDemo/dialogvisualimage.h"
#if !defined(Q_MOC_OUTPUT_REVISION)
#error "The header file 'dialogvisualimage.h' doesn't include <QObject>."
#elif Q_MOC_OUTPUT_REVISION != 62
#error "This file was generated using the moc from 4.7.2. It"
#error "cannot be used with the include files from this version of Qt."
#error "(The moc has changed too much.)"
#endif

QT_BEGIN_MOC_NAMESPACE
static const uint qt_meta_data_DialogVisualImage[] = {

 // content:
       5,       // revision
       0,       // classname
       0,    0, // classinfo
       5,   14, // methods
       0,    0, // properties
       0,    0, // enums/sets
       0,    0, // constructors
       0,       // flags
       0,       // signalCount

 // slots: signature, parameters, type, tag, flags
      19,   18,   18,   18, 0x08,
      47,   18,   18,   18, 0x08,
      80,   18,   18,   18, 0x08,
     113,   18,   18,   18, 0x08,
     141,   18,   18,   18, 0x08,

       0        // eod
};

static const char qt_meta_stringdata_DialogVisualImage[] = {
    "DialogVisualImage\0\0on_pushButtonPara_clicked()\0"
    "on_pushButtonDEMSource_clicked()\0"
    "on_pushButtonDOMSource_clicked()\0"
    "on_pushButtonDest_clicked()\0"
    "on_buttonBox_accepted()\0"
};

const QMetaObject DialogVisualImage::staticMetaObject = {
    { &QDialog::staticMetaObject, qt_meta_stringdata_DialogVisualImage,
      qt_meta_data_DialogVisualImage, 0 }
};

#ifdef Q_NO_DATA_RELOCATION
const QMetaObject &DialogVisualImage::getStaticMetaObject() { return staticMetaObject; }
#endif //Q_NO_DATA_RELOCATION

const QMetaObject *DialogVisualImage::metaObject() const
{
    return QObject::d_ptr->metaObject ? QObject::d_ptr->metaObject : &staticMetaObject;
}

void *DialogVisualImage::qt_metacast(const char *_clname)
{
    if (!_clname) return 0;
    if (!strcmp(_clname, qt_meta_stringdata_DialogVisualImage))
        return static_cast<void*>(const_cast< DialogVisualImage*>(this));
    return QDialog::qt_metacast(_clname);
}

int DialogVisualImage::qt_metacall(QMetaObject::Call _c, int _id, void **_a)
{
    _id = QDialog::qt_metacall(_c, _id, _a);
    if (_id < 0)
        return _id;
    if (_c == QMetaObject::InvokeMetaMethod) {
        switch (_id) {
        case 0: on_pushButtonPara_clicked(); break;
        case 1: on_pushButtonDEMSource_clicked(); break;
        case 2: on_pushButtonDOMSource_clicked(); break;
        case 3: on_pushButtonDest_clicked(); break;
        case 4: on_buttonBox_accepted(); break;
        default: ;
        }
        _id -= 5;
    }
    return _id;
}
QT_END_MOC_NAMESPACE
