/****************************************************************************
** Meta object code from reading C++ file 'imageviewer.h'
**
** Created: Wed Feb 8 19:26:49 2012
**      by: The Qt Meta Object Compiler version 62 (Qt 4.7.2)
**
** WARNING! All changes made in this file will be lost!
*****************************************************************************/

#include "../../../src/RvmlDemo/imageviewer.h"
#if !defined(Q_MOC_OUTPUT_REVISION)
#error "The header file 'imageviewer.h' doesn't include <QObject>."
#elif Q_MOC_OUTPUT_REVISION != 62
#error "This file was generated using the moc from 4.7.2. It"
#error "cannot be used with the include files from this version of Qt."
#error "(The moc has changed too much.)"
#endif

QT_BEGIN_MOC_NAMESPACE
static const uint qt_meta_data_ImageViewer[] = {

 // content:
       5,       // revision
       0,       // classname
       0,    0, // classinfo
      40,   14, // methods
       0,    0, // properties
       0,    0, // enums/sets
       0,    0, // constructors
       0,       // flags
       0,       // signalCount

 // slots: signature, parameters, type, tag, flags
      13,   12,   12,   12, 0x08,
      23,   12,   12,   12, 0x08,
      30,   12,   12,   12, 0x08,
      38,   12,   12,   12, 0x08,
      47,   12,   12,   12, 0x08,
      57,   12,   12,   12, 0x08,
      70,   12,   12,   12, 0x08,
      84,   12,   12,   12, 0x08,
      92,   12,   12,   12, 0x08,
     104,   12,   12,   12, 0x08,
     119,   12,   12,   12, 0x08,
     139,   12,   12,   12, 0x08,
     154,   12,   12,   12, 0x08,
     171,   12,   12,   12, 0x08,
     187,   12,   12,   12, 0x08,
     200,   12,   12,   12, 0x08,
     218,   12,   12,   12, 0x08,
     234,   12,   12,   12, 0x08,
     254,   12,   12,   12, 0x08,
     272,   12,   12,   12, 0x08,
     291,   12,   12,   12, 0x08,
     305,   12,   12,   12, 0x08,
     321,   12,   12,   12, 0x08,
     335,   12,   12,   12, 0x08,
     349,   12,   12,   12, 0x08,
     357,   12,   12,   12, 0x08,
     371,   12,   12,   12, 0x08,
     381,   12,   12,   12, 0x08,
     391,   12,   12,   12, 0x08,
     404,   12,   12,   12, 0x08,
     417,   12,   12,   12, 0x08,
     435,   12,   12,   12, 0x08,
     453,   12,   12,   12, 0x08,
     470,   12,   12,   12, 0x08,
     490,   12,   12,   12, 0x08,
     514,  504,   12,   12, 0x08,
     553,  504,   12,   12, 0x08,
     598,  586,  581,   12, 0x08,
     625,  619,   12,   12, 0x08,
     690,  670,   12,   12, 0x08,

       0        // eod
};

static const char qt_meta_stringdata_ImageViewer[] = {
    "ImageViewer\0\0Contour()\0open()\0print()\0"
    "zoomIn()\0zoomOut()\0normalSize()\0"
    "fitToWindow()\0about()\0rightopen()\0"
    "OpenMatchPts()\0CameraCalibration()\0"
    "CameraSurvey()\0SeriesImageDem()\0"
    "OrbitImageDem()\0PanoMosaic()\0"
    "PersImageCreate()\0SiteDemMosaic()\0"
    "MultSiteDemMosaic()\0WideBaselineMap()\0"
    "WidebaseAnalysis()\0TinSimplify()\0"
    "DemDomProcess()\0VisualImage()\0"
    "ContourLine()\0Slope()\0SlopeAspect()\0"
    "Barrier()\0Insight()\0DenseMatch()\0"
    "LandLocate()\0LandLocateMatch()\0"
    "LandLocateInter()\0CoordTransform()\0"
    "RoverLocateLander()\0RoverLocate()\0"
    "IsChecked\0ToolActEditMatchPointToolToggled(bool)\0"
    "ToolActPanToolToggled(bool)\0bool\0"
    "strFileName\0LoadGdalImage(char*)\0index\0"
    "on_PointTableView_doubleClicked(QModelIndex)\0"
    "selected,deselected\0"
    "on_PointTableView_selectionChanged(QItemSelection&,QItemSelection&)\0"
};

const QMetaObject ImageViewer::staticMetaObject = {
    { &QMainWindow::staticMetaObject, qt_meta_stringdata_ImageViewer,
      qt_meta_data_ImageViewer, 0 }
};

#ifdef Q_NO_DATA_RELOCATION
const QMetaObject &ImageViewer::getStaticMetaObject() { return staticMetaObject; }
#endif //Q_NO_DATA_RELOCATION

const QMetaObject *ImageViewer::metaObject() const
{
    return QObject::d_ptr->metaObject ? QObject::d_ptr->metaObject : &staticMetaObject;
}

void *ImageViewer::qt_metacast(const char *_clname)
{
    if (!_clname) return 0;
    if (!strcmp(_clname, qt_meta_stringdata_ImageViewer))
        return static_cast<void*>(const_cast< ImageViewer*>(this));
    return QMainWindow::qt_metacast(_clname);
}

int ImageViewer::qt_metacall(QMetaObject::Call _c, int _id, void **_a)
{
    _id = QMainWindow::qt_metacall(_c, _id, _a);
    if (_id < 0)
        return _id;
    if (_c == QMetaObject::InvokeMetaMethod) {
        switch (_id) {
        case 0: Contour(); break;
        case 1: open(); break;
        case 2: print(); break;
        case 3: zoomIn(); break;
        case 4: zoomOut(); break;
        case 5: normalSize(); break;
        case 6: fitToWindow(); break;
        case 7: about(); break;
        case 8: rightopen(); break;
        case 9: OpenMatchPts(); break;
        case 10: CameraCalibration(); break;
        case 11: CameraSurvey(); break;
        case 12: SeriesImageDem(); break;
        case 13: OrbitImageDem(); break;
        case 14: PanoMosaic(); break;
        case 15: PersImageCreate(); break;
        case 16: SiteDemMosaic(); break;
        case 17: MultSiteDemMosaic(); break;
        case 18: WideBaselineMap(); break;
        case 19: WidebaseAnalysis(); break;
        case 20: TinSimplify(); break;
        case 21: DemDomProcess(); break;
        case 22: VisualImage(); break;
        case 23: ContourLine(); break;
        case 24: Slope(); break;
        case 25: SlopeAspect(); break;
        case 26: Barrier(); break;
        case 27: Insight(); break;
        case 28: DenseMatch(); break;
        case 29: LandLocate(); break;
        case 30: LandLocateMatch(); break;
        case 31: LandLocateInter(); break;
        case 32: CoordTransform(); break;
        case 33: RoverLocateLander(); break;
        case 34: RoverLocate(); break;
        case 35: ToolActEditMatchPointToolToggled((*reinterpret_cast< bool(*)>(_a[1]))); break;
        case 36: ToolActPanToolToggled((*reinterpret_cast< bool(*)>(_a[1]))); break;
        case 37: { bool _r = LoadGdalImage((*reinterpret_cast< char*(*)>(_a[1])));
            if (_a[0]) *reinterpret_cast< bool*>(_a[0]) = _r; }  break;
        case 38: on_PointTableView_doubleClicked((*reinterpret_cast< QModelIndex(*)>(_a[1]))); break;
        case 39: on_PointTableView_selectionChanged((*reinterpret_cast< QItemSelection(*)>(_a[1])),(*reinterpret_cast< QItemSelection(*)>(_a[2]))); break;
        default: ;
        }
        _id -= 40;
    }
    return _id;
}
QT_END_MOC_NAMESPACE
