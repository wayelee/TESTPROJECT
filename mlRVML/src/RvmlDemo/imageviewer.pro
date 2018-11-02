HEADERS       = imageviewer.h \
    ../../../3rdParty/gdal1.8.1/include/CxImage/ximage.h \
    stdafx.h \
    ../../../3rdParty/gdal1.8.1/include/CxImage/DrawLabel.h \
    CustomScroll.h \
    GISTool.h \
    contourdialog.h \
    computecontourline.h \
    contourline.h \
    CmlRasterBand.h \
    CmlRasterDataset.h
SOURCES       = imageviewer.cpp \
                main.cpp \
    DrawLabel.cpp \
    CustomScroll.cpp \
    GISTool.cpp \
    contourdialog.cpp \
    computecontourline.cpp \
    contourline.cpp \
    CmlRasterBand.cpp \
    CmlRasterDataset.cpp

# install
target.path = $$[QT_INSTALL_EXAMPLES]/widgets/imageviewer
sources.files = $$SOURCES $$HEADERS $$RESOURCES $$FORMS imageviewer.pro
sources.path = $$[QT_INSTALL_EXAMPLES]/widgets/imageviewer
INSTALLS += target sources

INCLUDEPATH += ../../../3rdParty/gdal1.8.1/include/CxImage
INCLUDEPATH += ../../../3rdParty/gdal1.8.1/include/GDAL
#INCLUDEPATH += /usr/include/gdal

LIBS += ../../../3rdParty/gdal1.8.1/linux/lib/libgdal.so
LIBS += ../../../3rdParty/gdal1.8.1/linux/lib/libgdal.so.1
LIBS += ../../../3rdParty/gdal1.8.1/linux/lib/libgdal.so.1.15.1
LIBS += -L../../../3rdParty/gdal1.8.1/include/CxImage


symbian: include($$QT_SOURCE_TREE/examples/symbianpkgrules.pri)

wince*: {
   DEPLOYMENT_PLUGIN += qjpeg qmng qgif
}

FORMS += \
    contourdialog.ui
