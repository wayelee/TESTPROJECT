/****************************************************************************
** Meta object code from reading C++ file 'CustomScroll.h'
**
** Created: Tue May 15 09:23:10 2012
**      by: The Qt Meta Object Compiler version 62 (Qt 4.7.2)
**
** WARNING! All changes made in this file will be lost!
*****************************************************************************/

#include "../../../src/RvmlDemo/CustomScroll.h"
#if !defined(Q_MOC_OUTPUT_REVISION)
#error "The header file 'CustomScroll.h' doesn't include <QObject>."
#elif Q_MOC_OUTPUT_REVISION != 62
#error "This file was generated using the moc from 4.7.2. It"
#error "cannot be used with the include files from this version of Qt."
#error "(The moc has changed too much.)"
#endif

QT_BEGIN_MOC_NAMESPACE
static const uint qt_meta_data_CustomScroll[] = {

 // content:
       5,       // revision
       0,       // classname
       0,    0, // classinfo
       6,   14, // methods
       0,    0, // properties
       0,    0, // enums/sets
       0,    0, // constructors
       0,       // flags
       4,       // signalCount

 // signals: signature, parameters, type, tag, flags
      18,   14,   13,   13, 0x05,
      63,   52,   13,   13, 0x05,
      94,   52,   13,   13, 0x05,
     127,   13,   13,   13, 0x05,

 // slots: signature, parameters, type, tag, flags
     144,   14,   13,   13, 0x08,
     176,   13,   13,   13, 0x08,

       0        // eod
};

static const char qt_meta_stringdata_CustomScroll[] = {
    "CustomScroll\0\0x,y\0DrawLabelMouseMoveSignal(int,int)\0"
    "mouseEvent\0MousePressSignal(QMouseEvent*)\0"
    "MouseReleaseSignal(QMouseEvent*)\0"
    "DelSelectedPts()\0DrawLabelMouseMoveSlot(int,int)\0"
    "DeleteSelectedFeatPt()\0"
};

const QMetaObject CustomScroll::staticMetaObject = {
    { &QScrollArea::staticMetaObject, qt_meta_stringdata_CustomScroll,
      qt_meta_data_CustomScroll, 0 }
};

#ifdef Q_NO_DATA_RELOCATION
const QMetaObject &CustomScroll::getStaticMetaObject() { return staticMetaObject; }
#endif //Q_NO_DATA_RELOCATION

const QMetaObject *CustomScroll::metaObject() const
{
    return QObject::d_ptr->metaObject ? QObject::d_ptr->metaObject : &staticMetaObject;
}

void *CustomScroll::qt_metacast(const char *_clname)
{
    if (!_clname) return 0;
    if (!strcmp(_clname, qt_meta_stringdata_CustomScroll))
        return static_cast<void*>(const_cast< CustomScroll*>(this));
    return QScrollArea::qt_metacast(_clname);
}

int CustomScroll::qt_metacall(QMetaObject::Call _c, int _id, void **_a)
{
    _id = QScrollArea::qt_metacall(_c, _id, _a);
    if (_id < 0)
        return _id;
    if (_c == QMetaObject::InvokeMetaMethod) {
        switch (_id) {
        case 0: DrawLabelMouseMoveSignal((*reinterpret_cast< int(*)>(_a[1])),(*reinterpret_cast< int(*)>(_a[2]))); break;
        case 1: MousePressSignal((*reinterpret_cast< QMouseEvent*(*)>(_a[1]))); break;
        case 2: MouseReleaseSignal((*reinterpret_cast< QMouseEvent*(*)>(_a[1]))); break;
        case 3: DelSelectedPts(); break;
        case 4: DrawLabelMouseMoveSlot((*reinterpret_cast< int(*)>(_a[1])),(*reinterpret_cast< int(*)>(_a[2]))); break;
        case 5: DeleteSelectedFeatPt(); break;
        default: ;
        }
        _id -= 6;
    }
    return _id;
}

// SIGNAL 0
void CustomScroll::DrawLabelMouseMoveSignal(int _t1, int _t2)
{
    void *_a[] = { 0, const_cast<void*>(reinterpret_cast<const void*>(&_t1)), const_cast<void*>(reinterpret_cast<const void*>(&_t2)) };
    QMetaObject::activate(this, &staticMetaObject, 0, _a);
}

// SIGNAL 1
void CustomScroll::MousePressSignal(QMouseEvent * _t1)
{
    void *_a[] = { 0, const_cast<void*>(reinterpret_cast<const void*>(&_t1)) };
    QMetaObject::activate(this, &staticMetaObject, 1, _a);
}

// SIGNAL 2
void CustomScroll::MouseReleaseSignal(QMouseEvent * _t1)
{
    void *_a[] = { 0, const_cast<void*>(reinterpret_cast<const void*>(&_t1)) };
    QMetaObject::activate(this, &staticMetaObject, 2, _a);
}

// SIGNAL 3
void CustomScroll::DelSelectedPts()
{
    QMetaObject::activate(this, &staticMetaObject, 3, 0);
}
QT_END_MOC_NAMESPACE
