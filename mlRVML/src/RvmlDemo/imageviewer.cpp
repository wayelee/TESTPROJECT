/****************************************************************************
**
** Copyright (C) 2011 Nokia Corporation and/or its subsidiary(-ies).
** All rights reserved.
** Contact: Nokia Corporation (qt-info@nokia.com)
**
** This file is part of the examples of the Qt Toolkit.
**
** $QT_BEGIN_LICENSE:BSD$
** You may use this file under the terms of the BSD license as follows:
**
** "Redistribution and use in source and binary forms, with or without
** modification, are permitted provided that the following conditions are
** met:
**   * Redistributions of source code must retain the above copyright
**     notice, this list of conditions and the following disclaimer.
**   * Redistributions in binary form must reproduce the above copyright
**     notice, this list of conditions and the following disclaimer in
**     the documentation and/or other materials provided with the
**     distribution.
**   * Neither the name of Nokia Corporation and its Subsidiary(-ies) nor
**     the names of its contributors may be used to endorse or promote
**     products derived from this software without specific prior written
**     permission.
**
** THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS
** "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT
** LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR
** A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT
** OWNER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL,
** SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT
** LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE,
** DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY
** THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
** (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE
** OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE."
** $QT_END_LICENSE$
**
****************************************************************************/


#include "imageviewer.h"
#include <math.h>

#include "../../include/mlRVML.h"


//#include "../../../3rdParty/PanoMosaic/Panorama.h"
#include "camCalibIO.h"
#include "PtFilesRW.h"
#include <pthread.h>
using namespace std;


//! [0]
ImageViewer::ImageViewer()
{
    m_nCurPos = 0;
    BasicToolBar = new QToolBar();

    scrollArea = new CustomScroll();
    scrollArea->MainFrame = this;
    scrollArea->setBackgroundRole(QPalette::Dark);
    scrollArea->setFocusPolicy(Qt::ClickFocus);
    scrollArea->FeatPtDataSet = & LeftFeatPtDataset;

    RightScrollArea = new CustomScroll();
    RightScrollArea->MainFrame = this;
    RightScrollArea->setBackgroundRole(QPalette::Dark);
    RightScrollArea->setFocusPolicy(Qt::ClickFocus);
    RightScrollArea->FeatPtDataSet = & RightFeatPtDataset;

    LeftLayout = new QVBoxLayout;
    RightLayout = new QVBoxLayout;


    MainLayout = new QHBoxLayout;
    Splitter = new QSplitter;
    Splitter->addWidget(scrollArea);
    Splitter->addWidget(RightScrollArea);

    MainSplitter = new QSplitter(Qt::Vertical);
    MainSplitter->addWidget(Splitter);
    m_ProgressBar = new QProgressBar();
    m_ProgressBarLable = new QLabel();
    m_ProgressBarLable->setAlignment(Qt::AlignCenter);
//    MainSplitter->addWidget(ProgressBarLable);
    m_ProgressBarLable->setText("ProgressBar");
//    MainSplitter->addWidget(ProgressBar);

    //显示坐标的标签
    LeftImgCoordLable = new QLabel();
    LeftImgCoordLable->setAlignment(Qt::AlignCenter);
    LeftImgCoordLable->setText("Left x:       y:      ");
    connect(scrollArea,SIGNAL(DrawLabelMouseMoveSignal(int ,int )),this,SLOT(LeftImgMouseMove(int,int)));
    connect(scrollArea,SIGNAL(MousePressSignal(QMouseEvent* )),this,SLOT(LeftScrollAreaMousePress(QMouseEvent* )));
    connect(scrollArea,SIGNAL(MouseReleaseSignal(QMouseEvent* )),this,SLOT(LeftScrollAreaMouseRealease(QMouseEvent* )));
    connect(scrollArea,SIGNAL(DelSelectedPts()),this,SLOT(On_LeftScrollAreaDelPts()));

    RightImgCoordLable = new QLabel();
    RightImgCoordLable->setAlignment(Qt::AlignCenter);
    RightImgCoordLable->setText("Right x:       y:      ");
    connect(RightScrollArea,SIGNAL(DrawLabelMouseMoveSignal(int ,int )),this,SLOT(RightImgMouseMove(int,int)));
    connect(RightScrollArea,SIGNAL(MousePressSignal(QMouseEvent* )),this,SLOT(RightScrollAreaMousePress(QMouseEvent* )));
    connect(RightScrollArea,SIGNAL(MouseReleaseSignal(QMouseEvent* )),this,SLOT(RightScrollAreaMouseRealease(QMouseEvent* )));
    connect(RightScrollArea,SIGNAL(DelSelectedPts()),this,SLOT(On_RightScrollAreaDelPts()));

    MainStatusBar = new QStatusBar(this);
    MainStatusBar->addWidget(LeftImgCoordLable);
    MainStatusBar->addWidget(RightImgCoordLable);
    MainStatusBar->addWidget(m_ProgressBarLable);
    MainStatusBar->addWidget(m_ProgressBar);
    setStatusBar(MainStatusBar);
    LeftImgCoordLable->setGeometry(0,0,150,25);
    RightImgCoordLable->setGeometry(0,0,150,25);
    m_ProgressBarLable->setGeometry(0,0,200,25);
    // TableView is not used
    //TableView = new QTableView;
    //MainSplitter->addWidget(TableView);
    // 保证splitter占据多余的空间
    MainSplitter->setStretchFactor(0,1);
    setCentralWidget(MainSplitter);

    createActions();
    createMenus();
    creatToolBar();
    InitializeTableview();
    resize(1000, 800);
    this->showMaximized();
    //scaleFactor =1;

    AddFeatPointStep = 0;


    QDockWidget *dock = new QDockWidget(tr("ProjectTreeView"), this);
    dock->setAllowedAreas(Qt::BottomDockWidgetArea | Qt::TopDockWidgetArea | Qt::LeftDockWidgetArea | Qt::RightDockWidgetArea);
    ProjectTreeView = new QTreeView(dock);
    dock->setWidget(ProjectTreeView);
    addDockWidget(Qt::LeftDockWidgetArea,dock);
    viewMenu->addAction(dock->toggleViewAction());
    //MouseDown = false;
    //CurrentToolType = Tool_pan;
}
ImageViewer::~ImageViewer()
{

}
void ImageViewer::closeEvent(QCloseEvent *event)
{
    QMainWindow::closeEvent(event);
    dlgPointCoord.close();
}
void ImageViewer::keyPressEvent(QKeyEvent *event)
{

//    if(event->key() == Qt::Key_Escape)
//    {
//        dlgPointCoord.SetID(0);
//        AddFeatPointStep = 0;
//        AddLeftOrRight = 0;
//    }

}


void ImageViewer::On_LeftScrollAreaDelPts()
{
    QTextCodec::setCodecForTr(QTextCodec::codecForName("GB2312"));
    QTextCodec::setCodecForCStrings(QTextCodec::codecForName("GB2312"));
    QTextCodec::setCodecForLocale(QTextCodec::codecForName("GB2312"));
    QTextCodec *codec = QTextCodec::codecForLocale();
    QString a = codec->toUnicode("删除选中点");

    QMessageBox msgBox;
    msgBox.setText(tr("Delete"));
    msgBox.setInformativeText(a);
    msgBox.setStandardButtons(QMessageBox::Ok | QMessageBox::Cancel);
    msgBox.setDefaultButton(QMessageBox::Ok);
    if( QMessageBox::Ok != msgBox.exec())
    {
        return;
    }
    QMessageBox msgBox1;
    a = codec->toUnicode("删除右片同名点");
    msgBox.setText(tr("Delete"));
    msgBox1.setInformativeText(a);
    msgBox1.setStandardButtons(QMessageBox::Ok | QMessageBox::Cancel);
    msgBox1.setDefaultButton(QMessageBox::Ok);
    // 只删除左边点
    if(QMessageBox::Ok != msgBox1.exec())
    {
        for(int i = scrollArea->FeatManualSelectedIdx.count() - 1; i >= 0; i--)
        {
            Pt2d pt;
            int idx = scrollArea->FeatManualSelectedIdx.at(i);
            UINT nindex ;
            scrollArea->FeatPtDataSet->FindManualPtByIndex(idx,pt);
            scrollArea->FeatPtDataSet->DelManualPtByIndex(idx);
            ULONG lID = pt.lID;
            if(RightScrollArea->FeatPtDataSet->FindAutoPtIndexByID(lID,nindex))
            {
                RightScrollArea->FeatPtDataSet->m_ImgPtSet.vecPts.at(nindex).byIsMatch = 0;
                continue;
            }
            if(RightScrollArea->FeatPtDataSet->FindManualPtIndexByID(lID,nindex))
            {
                RightScrollArea->FeatPtDataSet->m_ImgPtSet.vecAddPts.at(nindex).byIsMatch = 0;
            }

            //LeftFeatPtTableModel->removeRow(idx + scrollArea->FeatPtDataSet->m_ImgPtSet.vecPts.size());
        }
        for(int i = scrollArea->FeatAutoSelectedIdx.count() - 1; i >= 0; i--)
        {
            Pt2d pt;
            int idx = scrollArea->FeatAutoSelectedIdx.at(i);
            UINT nindex ;
            scrollArea->FeatPtDataSet->FindAutoPtByIndex(idx,pt);
            scrollArea->FeatPtDataSet->DelAutoPtByIndex(idx);
            ULONG lID = pt.lID;
            if(RightScrollArea->FeatPtDataSet->FindAutoPtIndexByID(lID,nindex))
            {
                RightScrollArea->FeatPtDataSet->m_ImgPtSet.vecPts.at(nindex).byIsMatch = 0;
                continue;
            }
            if(RightScrollArea->FeatPtDataSet->FindManualPtIndexByID(lID,nindex))
            {
                RightScrollArea->FeatPtDataSet->m_ImgPtSet.vecAddPts.at(nindex).byIsMatch = 0;
            }

            //LeftFeatPtTableModel->removeRow(idx);
        }
        scrollArea->FeatAutoSelectedIdx.clear();
        scrollArea->FeatManualSelectedIdx.clear();
        RightScrollArea->FeatAutoSelectedIdx.clear();
        RightScrollArea->FeatManualSelectedIdx.clear();
        AddLeftMatchPoints();
        //UpdateFeatPtTableView();
    }
    //同时删除右边点
    else
    {
        for(int i = scrollArea->FeatManualSelectedIdx.count() - 1; i >= 0; i--)
        {
            Pt2d pt;
            int idx = scrollArea->FeatManualSelectedIdx.at(i);
            scrollArea->FeatPtDataSet->FindManualPtByIndex(idx,pt);
            ULONG lID = pt.lID;
            RightScrollArea->FeatPtDataSet->DelPtByID(lID);
            scrollArea->FeatPtDataSet->DelManualPtByIndex(idx);
            //LeftFeatPtTableModel->removeRow(idx + scrollArea->FeatPtDataSet->m_ImgPtSet.vecPts.size());
        }
        for(int i = scrollArea->FeatAutoSelectedIdx.count() - 1; i >= 0; i--)
        {
            int idx = scrollArea->FeatAutoSelectedIdx.at(i);
            Pt2d pt;
            scrollArea->FeatPtDataSet->FindAutoPtByIndex(idx,pt);
            ULONG lID = pt.lID;
            RightScrollArea->FeatPtDataSet->DelPtByID(lID);
            scrollArea->FeatPtDataSet->DelAutoPtByIndex(idx);
            // LeftFeatPtTableModel->removeRow(idx);
        }
        scrollArea->FeatAutoSelectedIdx.clear();
        scrollArea->FeatManualSelectedIdx.clear();
        RightScrollArea->FeatAutoSelectedIdx.clear();
        RightScrollArea->FeatManualSelectedIdx.clear();
        AddLeftMatchPoints();
        AddRightMatchPoints();
    }
}
void ImageViewer::On_RightScrollAreaDelPts()
{
    QTextCodec::setCodecForTr(QTextCodec::codecForName("GB2312"));
    QTextCodec::setCodecForCStrings(QTextCodec::codecForName("GB2312"));
    QTextCodec::setCodecForLocale(QTextCodec::codecForName("GB2312"));
    QTextCodec *codec = QTextCodec::codecForLocale();
    QString a = codec->toUnicode("删除选中点");

    QMessageBox msgBox;
    msgBox.setText(tr("Delete"));
    msgBox.setInformativeText(a);
    msgBox.setStandardButtons(QMessageBox::Ok | QMessageBox::Cancel);
    msgBox.setDefaultButton(QMessageBox::Ok);
    if( QMessageBox::Ok != msgBox.exec())
    {
        return;
    }
    QMessageBox msgBox1;
    a = codec->toUnicode("删除左片同名点");
    msgBox1.setText(tr("Delete"));
    msgBox1.setInformativeText(a);
    msgBox1.setStandardButtons(QMessageBox::Ok | QMessageBox::Cancel);
    msgBox1.setDefaultButton(QMessageBox::Ok);
    // 只删除右边点
    if(QMessageBox::Ok != msgBox1.exec())
    {
        for(int i = RightScrollArea->FeatManualSelectedIdx.count() - 1; i >= 0; i--)
        {
            Pt2d pt;
            int idx = RightScrollArea->FeatManualSelectedIdx.at(i);
            UINT nindex ;
            RightScrollArea->FeatPtDataSet->FindManualPtByIndex(idx,pt);
            RightScrollArea->FeatPtDataSet->DelManualPtByIndex(idx);
            ULONG lID = pt.lID;
            if(scrollArea->FeatPtDataSet->FindAutoPtIndexByID(lID,nindex))
            {
                scrollArea->FeatPtDataSet->m_ImgPtSet.vecPts.at(nindex).byIsMatch = 0;
                continue;
            }
            if(scrollArea->FeatPtDataSet->FindManualPtIndexByID(lID,nindex))
            {
                scrollArea->FeatPtDataSet->m_ImgPtSet.vecAddPts.at(nindex).byIsMatch = 0;
            }

            //LeftFeatPtTableModel->removeRow(idx + scrollArea->FeatPtDataSet->m_ImgPtSet.vecPts.size());
        }
        for(int i = RightScrollArea->FeatAutoSelectedIdx.count() - 1; i >= 0; i--)
        {
            int idx = RightScrollArea->FeatAutoSelectedIdx.at(i);
            Pt2d pt;
            UINT nindex ;
            RightScrollArea->FeatPtDataSet->FindAutoPtByIndex(idx,pt);
            RightScrollArea->FeatPtDataSet->DelAutoPtByIndex(idx);
            ULONG lID = pt.lID;
            if(scrollArea->FeatPtDataSet->FindAutoPtIndexByID(lID,nindex))
            {
                scrollArea->FeatPtDataSet->m_ImgPtSet.vecPts.at(nindex).byIsMatch = 0;
                continue;
            }
            if(scrollArea->FeatPtDataSet->FindManualPtIndexByID(lID,nindex))
            {
                scrollArea->FeatPtDataSet->m_ImgPtSet.vecAddPts.at(nindex).byIsMatch = 0;
            }

            //LeftFeatPtTableModel->removeRow(idx);
        }
        scrollArea->FeatAutoSelectedIdx.clear();
        scrollArea->FeatManualSelectedIdx.clear();
        RightScrollArea->FeatAutoSelectedIdx.clear();
        RightScrollArea->FeatManualSelectedIdx.clear();
        AddRightMatchPoints();
        //UpdateFeatPtTableView();
    }
    //同时删除左边点
    else
    {
        for(int i = RightScrollArea->FeatManualSelectedIdx.count() - 1; i >= 0; i--)
        {
            Pt2d pt;
            int idx = RightScrollArea->FeatManualSelectedIdx.at(i);
            RightScrollArea->FeatPtDataSet->FindManualPtByIndex(idx,pt);
            ULONG lID = pt.lID;
            scrollArea->FeatPtDataSet->DelPtByID(lID);
            RightScrollArea->FeatPtDataSet->DelManualPtByIndex(idx);
            //LeftFeatPtTableModel->removeRow(idx + scrollArea->FeatPtDataSet->m_ImgPtSet.vecPts.size());
        }
        for(int i = RightScrollArea->FeatAutoSelectedIdx.count() - 1; i >= 0; i--)
        {
            int idx = RightScrollArea->FeatAutoSelectedIdx.at(i);
            Pt2d pt;
            RightScrollArea->FeatPtDataSet->FindAutoPtByIndex(idx,pt);
            ULONG lID = pt.lID;
            scrollArea->FeatPtDataSet->DelPtByID(lID);
            RightScrollArea->FeatPtDataSet->DelAutoPtByIndex(idx);
            // LeftFeatPtTableModel->removeRow(idx);
        }
        scrollArea->FeatAutoSelectedIdx.clear();
        scrollArea->FeatManualSelectedIdx.clear();
        RightScrollArea->FeatAutoSelectedIdx.clear();
        RightScrollArea->FeatManualSelectedIdx.clear();
        AddLeftMatchPoints();
        AddRightMatchPoints();
    }


}
void ImageViewer::UpdateFeatPtTableView()
{
    QItemSelectionModel* LSelectionmodel = LeftFeatPtTableView->selectionModel();
    QItemSelectionModel* RSelectionmodel =  RightFeatPtTableView->selectionModel();

    LSelectionmodel->clearSelection();
    RSelectionmodel->clearSelection();
    LeftFeatPtTableView->setSelectionMode(QAbstractItemView::MultiSelection);
    RightFeatPtTableView->setSelectionMode(QAbstractItemView::MultiSelection);

    QItemSelection tselection;

    QModelIndex Beginindex;
    QModelIndex endindex;
    for(int i = 0; i< scrollArea->FeatAutoSelectedIdx.count(); i++)
    {
        // 这语句的作用是让tableview的当前行变成最后一个选中的点
        if(i == scrollArea->FeatAutoSelectedIdx.count() - 1)
        {
            LeftFeatPtTableView->selectRow(scrollArea->FeatAutoSelectedIdx.at(i));
        }
        // 因为QItemSelection类的merge方法效率低，以下语句的目的是为了将连续的记录用一个QItemSelection构造，不相连的记录再merge
        if(i ==0)
        {
            Beginindex =  LeftFeatPtTableModel->index(scrollArea->FeatAutoSelectedIdx.at(i),0);
            endindex = LeftFeatPtTableModel->index(scrollArea->FeatAutoSelectedIdx.at(i),2);

        }
        else
        {
            if(scrollArea->FeatAutoSelectedIdx.at(i) - scrollArea->FeatAutoSelectedIdx.at(i-1) == 1)
            {
                endindex =  LeftFeatPtTableModel->index(scrollArea->FeatAutoSelectedIdx.at(i),2);
            }
            else
            {
                QItemSelection selection (Beginindex,
                                          endindex);
                tselection.merge(selection,QItemSelectionModel::Select);
                Beginindex =  LeftFeatPtTableModel->index(scrollArea->FeatAutoSelectedIdx.at(i),0);
                endindex = LeftFeatPtTableModel->index(scrollArea->FeatAutoSelectedIdx.at(i),2);
            }
        }
        if(i ==scrollArea->FeatAutoSelectedIdx.count() -1)
        {
            QItemSelection selection (Beginindex,
                                      endindex);
            tselection.merge(selection,QItemSelectionModel::Select);
        }
    }

    int autoptcount = LeftFeatPtDataset.m_ImgPtSet.vecPts.size();
    for(int i = 0; i < scrollArea->FeatManualSelectedIdx.count(); i++)
    {
        if(i == scrollArea->FeatManualSelectedIdx.count() -1)
        {
            LeftFeatPtTableView->selectRow(scrollArea->FeatManualSelectedIdx.at(i) + autoptcount);
        }

        if(i ==0)
        {
            Beginindex =  LeftFeatPtTableModel->index(scrollArea->FeatManualSelectedIdx.at(i) + autoptcount,0);
            endindex = LeftFeatPtTableModel->index(scrollArea->FeatManualSelectedIdx.at(i) + autoptcount,2);
        }
        else
        {
            if(scrollArea->FeatManualSelectedIdx.at(i) - scrollArea->FeatManualSelectedIdx.at(i-1) == 1)
            {
                endindex = LeftFeatPtTableModel->index(scrollArea->FeatManualSelectedIdx.at(i) + autoptcount,2);
            }
            else
            {
                QItemSelection selection (Beginindex,
                                          endindex);
                tselection.merge(selection,QItemSelectionModel::Select);
                Beginindex =  LeftFeatPtTableModel->index(scrollArea->FeatManualSelectedIdx.at(i) + autoptcount,0);
                endindex = LeftFeatPtTableModel->index(scrollArea->FeatManualSelectedIdx.at(i) + autoptcount,2);
            }
        }
        if(i ==scrollArea->FeatManualSelectedIdx.count() -1)
        {
            QItemSelection selection (Beginindex,
                                      endindex);
            tselection.merge(selection,QItemSelectionModel::Select);
        }
    }
    LSelectionmodel->select(tselection,QItemSelectionModel::Select);
    scrollArea->imageLabel->update();

    //右表的选择开始
    tselection.clear();

    for(int i = 0; i< RightScrollArea->FeatAutoSelectedIdx.count(); i++)
    {
        if(i == RightScrollArea->FeatAutoSelectedIdx.count() - 1)
        {
            RightFeatPtTableView->selectRow(RightScrollArea->FeatAutoSelectedIdx.at(i));
        }

        // 因为QItemSelection类的merge方法效率低，以下语句的目的是为了将连续的记录用一个QItemSelection构造，不相连的记录再merge
        if(i ==0)
        {
            Beginindex =  RightFeatPtTableModel->index(RightScrollArea->FeatAutoSelectedIdx.at(i),0);
            endindex = RightFeatPtTableModel->index(RightScrollArea->FeatAutoSelectedIdx.at(i),2);

        }
        else
        {
            if(RightScrollArea->FeatAutoSelectedIdx.at(i) - RightScrollArea->FeatAutoSelectedIdx.at(i-1) == 1)
            {
                endindex =  RightFeatPtTableModel->index(RightScrollArea->FeatAutoSelectedIdx.at(i),2);
            }
            else
            {
                QItemSelection selection (Beginindex,
                                          endindex);
                tselection.merge(selection,QItemSelectionModel::Select);
                Beginindex =  RightFeatPtTableModel->index(RightScrollArea->FeatAutoSelectedIdx.at(i),0);
                endindex = RightFeatPtTableModel->index(RightScrollArea->FeatAutoSelectedIdx.at(i),2);
            }
        }
        if(i ==RightScrollArea->FeatAutoSelectedIdx.count() -1)
        {
            QItemSelection selection (Beginindex,
                                      endindex);
            tselection.merge(selection,QItemSelectionModel::Select);
        }
    }

    autoptcount = RightFeatPtDataset.m_ImgPtSet.vecPts.size();
    for(int i = 0; i < RightScrollArea->FeatManualSelectedIdx.count(); i++)
    {
        if(i == RightScrollArea->FeatManualSelectedIdx.count() - 1)
        {
            RightFeatPtTableView->selectRow(RightScrollArea->FeatManualSelectedIdx.at(i) + autoptcount);
        }

        if(i ==0)
        {
            Beginindex =  RightFeatPtTableModel->index(RightScrollArea->FeatManualSelectedIdx.at(i) + autoptcount,0);
            endindex = RightFeatPtTableModel->index(RightScrollArea->FeatManualSelectedIdx.at(i) + autoptcount,2);
        }
        else
        {
            if(RightScrollArea->FeatManualSelectedIdx.at(i) - RightScrollArea->FeatManualSelectedIdx.at(i-1) == 1)
            {
                endindex = RightFeatPtTableModel->index(RightScrollArea->FeatManualSelectedIdx.at(i) + autoptcount,2);
            }
            else
            {
                QItemSelection selection (Beginindex,
                                          endindex);
                tselection.merge(selection,QItemSelectionModel::Select);
                Beginindex =  RightFeatPtTableModel->index(RightScrollArea->FeatManualSelectedIdx.at(i) + autoptcount,0);
                endindex = RightFeatPtTableModel->index(RightScrollArea->FeatManualSelectedIdx.at(i) + autoptcount,2);
            }
        }
        if(i ==RightScrollArea->FeatManualSelectedIdx.count() -1)
        {
            QItemSelection selection (Beginindex,
                                      endindex);
            tselection.merge(selection,QItemSelectionModel::Select);
        }
    }
    RSelectionmodel->select(tselection,QItemSelectionModel::Select);
    //RightFeatPtTableView->setSelectionModel(RSelectionmodel);
    RightScrollArea->imageLabel->update();
    LeftFeatPtTableView->setSelectionMode(QAbstractItemView::ExtendedSelection);
    RightFeatPtTableView->setSelectionMode(QAbstractItemView::ExtendedSelection);

}


void ImageViewer::on_PointTableView_doubleClicked(QModelIndex e)
{
//    QItemSelectionModel* selectionModel = PointTableView->selectionModel();
//    QModelIndexList selections = selectionModel->selectedRows();

//    //scrollArea->SelectedIdxs->clear();
//    // RightScrollArea->SelectedIdxs->clear();
//    //QModelIndexList selected = selections->selectedRows();
//    for(int i = 0 ; i < selections.count(); i++)
//    {
//        int idx = selections.at(i).row();
//        SelectedIdxs->append(idx);
//        //scrollArea->SelectedIdxs->append(idx);
//        //RightScrollArea->SelectedIdxs->append(idx);
//    }
//    scrollArea->imageLabel->update();
//    RightScrollArea->imageLabel->update();
//    return;
}

void ImageViewer::on_LeftFeatPtTableView_doubleClicked(QModelIndex e)
{
    QItemSelectionModel * selectionModel = LeftFeatPtTableView->selectionModel();
    QModelIndexList selections = selectionModel->selectedRows();
    scrollArea->FeatAutoSelectedIdx.clear();
    scrollArea->FeatManualSelectedIdx.clear();
    for(int i = 0 ; i < selections.count(); i++)
    {
        int idx = selections.at(i).row();
        if(idx < scrollArea->FeatPtDataSet->m_ImgPtSet.vecPts.size())
        {
            scrollArea->FeatAutoSelectedIdx.append(idx);
        }
        else
        {
            scrollArea->FeatManualSelectedIdx.append(idx - scrollArea->FeatPtDataSet->m_ImgPtSet.vecPts.size() );
        }

        //SelectedIdxs->append(idx);
        //scrollArea->SelectedIdxs->append(idx);
        //RightScrollArea->SelectedIdxs->append(idx);
    }
    SelectRightPtsByLeftSelection();
    //居中显示第一个选中点
    if(scrollArea->FeatAutoSelectedIdx.count() > 0)
    {

        double centerx = scrollArea->FeatPtDataSet->m_ImgPtSet.vecPts.at(scrollArea->FeatAutoSelectedIdx.at(0)).X;
        double centery = scrollArea->FeatPtDataSet->m_ImgPtSet.vecPts.at(scrollArea->FeatAutoSelectedIdx.at(0)).Y;
        scrollArea->CenterRasterPoint(centerx,centery);
        for(int i = 0; i < scrollArea->FeatAutoSelectedIdx.count(); i++)
        {
            UINT lidx = scrollArea->FeatAutoSelectedIdx.at(i);
            ULONG ID = LeftFeatPtDataset.m_ImgPtSet.vecPts.at(lidx).lID;
            UINT idx ;
            if(RightFeatPtDataset.FindAutoPtIndexByID(ID,idx))
            {
                // RightScrollArea->FeatAutoSelectedIdx.append(idx);
                centerx =  RightScrollArea->FeatPtDataSet->m_ImgPtSet.vecPts.at(idx).X;
                centery =  RightScrollArea->FeatPtDataSet->m_ImgPtSet.vecPts.at(idx).Y;
                RightScrollArea->CenterRasterPoint(centerx,centery);
                break;
            }
            if(RightFeatPtDataset.FindManualPtIndexByID(ID,idx))
            {
                //RightScrollArea->FeatManualSelectedIdx.append(idx);
                // RightScrollArea->FeatAutoSelectedIdx.append(idx);
                centerx =  RightScrollArea->FeatPtDataSet->m_ImgPtSet.vecAddPts.at(idx).X;
                centery =  RightScrollArea->FeatPtDataSet->m_ImgPtSet.vecAddPts.at(idx).Y;
                RightScrollArea->CenterRasterPoint(centerx,centery);
                break;
            }
        }
    }

    if(scrollArea->FeatManualSelectedIdx.count() > 0)
    {
        double centerx = scrollArea->FeatPtDataSet->m_ImgPtSet.vecAddPts.at(scrollArea->FeatManualSelectedIdx.at(0)).X;
        double centery = scrollArea->FeatPtDataSet->m_ImgPtSet.vecAddPts.at(scrollArea->FeatManualSelectedIdx.at(0)).Y;
        scrollArea->CenterRasterPoint(centerx,centery);
        for(int i = 0; i< scrollArea->FeatManualSelectedIdx.count(); i++)
        {
            UINT lidx = scrollArea->FeatManualSelectedIdx.at(i);
            ULONG ID = LeftFeatPtDataset.m_ImgPtSet.vecAddPts.at(lidx).lID;
            UINT idx ;
            if(RightFeatPtDataset.FindAutoPtIndexByID(ID,idx))
            {
                // RightScrollArea->FeatAutoSelectedIdx.append(idx);
                centerx =  RightScrollArea->FeatPtDataSet->m_ImgPtSet.vecPts.at(idx).X;
                centery =  RightScrollArea->FeatPtDataSet->m_ImgPtSet.vecPts.at(idx).Y;
                RightScrollArea->CenterRasterPoint(centerx,centery);
                break;
            }
            if(RightFeatPtDataset.FindManualPtIndexByID(ID,idx))
            {
                //RightScrollArea->FeatManualSelectedIdx.append(idx);
                // RightScrollArea->FeatAutoSelectedIdx.append(idx);
                centerx =  RightScrollArea->FeatPtDataSet->m_ImgPtSet.vecAddPts.at(idx).X;
                centery =  RightScrollArea->FeatPtDataSet->m_ImgPtSet.vecAddPts.at(idx).Y;
                RightScrollArea->CenterRasterPoint(centerx,centery);
                break;
            }
        }
    }

    UpdateFeatPtTableView();
}
void ImageViewer::on_RightFeatPtTableView_doubleClicked(QModelIndex e)
{
    QItemSelectionModel * selectionModel = RightFeatPtTableView->selectionModel();
    QModelIndexList selections = selectionModel->selectedRows();
    RightScrollArea->FeatAutoSelectedIdx.clear();
    RightScrollArea->FeatManualSelectedIdx.clear();
    for(int i = 0 ; i < selections.count(); i++)
    {
        int idx = selections.at(i).row();
        if(idx < RightScrollArea->FeatPtDataSet->m_ImgPtSet.vecPts.size())
        {
            RightScrollArea->FeatAutoSelectedIdx.append(idx);
        }
        else
        {
            RightScrollArea->FeatManualSelectedIdx.append(idx - RightScrollArea->FeatPtDataSet->m_ImgPtSet.vecPts.size() );
        }

        //SelectedIdxs->append(idx);
        //scrollArea->SelectedIdxs->append(idx);
        //RightScrollArea->SelectedIdxs->append(idx);
    }
    SelectLeftPtsByRightSelection();
    //居中显示第一个选中点
    if(RightScrollArea->FeatAutoSelectedIdx.count() >0)
    {
        double centerx = RightScrollArea->FeatPtDataSet->m_ImgPtSet.vecPts.at(RightScrollArea->FeatAutoSelectedIdx.at(0)).X;
        double centery = RightScrollArea->FeatPtDataSet->m_ImgPtSet.vecPts.at(RightScrollArea->FeatAutoSelectedIdx.at(0)).Y;
        RightScrollArea->CenterRasterPoint(centerx,centery);
        for(int i = 0; i < RightScrollArea->FeatAutoSelectedIdx.count(); i++)
        {
            UINT lidx = RightScrollArea->FeatAutoSelectedIdx.at(i);
            ULONG ID = RightFeatPtDataset.m_ImgPtSet.vecPts.at(lidx).lID;
            UINT idx ;
            if(LeftFeatPtDataset.FindAutoPtIndexByID(ID,idx))
            {
                // RightScrollArea->FeatAutoSelectedIdx.append(idx);
                centerx =  scrollArea->FeatPtDataSet->m_ImgPtSet.vecPts.at(idx).X;
                centery =  scrollArea->FeatPtDataSet->m_ImgPtSet.vecPts.at(idx).Y;
                scrollArea->CenterRasterPoint(centerx,centery);
                break;
            }
            if(LeftFeatPtDataset.FindManualPtIndexByID(ID,idx))
            {
                //RightScrollArea->FeatManualSelectedIdx.append(idx);
                // RightScrollArea->FeatAutoSelectedIdx.append(idx);
                centerx =  scrollArea->FeatPtDataSet->m_ImgPtSet.vecAddPts.at(idx).X;
                centery =  scrollArea->FeatPtDataSet->m_ImgPtSet.vecAddPts.at(idx).Y;
                scrollArea->CenterRasterPoint(centerx,centery);
                break;
            }
        }
    }
    if(RightScrollArea->FeatManualSelectedIdx.count() > 0)
    {
        double centerx = RightScrollArea->FeatPtDataSet->m_ImgPtSet.vecAddPts.at(RightScrollArea->FeatManualSelectedIdx.at(0)).X;
        double centery = RightScrollArea->FeatPtDataSet->m_ImgPtSet.vecAddPts.at(RightScrollArea->FeatManualSelectedIdx.at(0)).Y;
        RightScrollArea->CenterRasterPoint(centerx,centery);
        for(int i = 0; i < RightScrollArea->FeatManualSelectedIdx.count(); i++)
        {
            UINT lidx = RightScrollArea->FeatManualSelectedIdx.at(i);
            ULONG ID = RightFeatPtDataset.m_ImgPtSet.vecAddPts.at(lidx).lID;
            UINT idx ;
            if(LeftFeatPtDataset.FindAutoPtIndexByID(ID,idx))
            {
                // RightScrollArea->FeatAutoSelectedIdx.append(idx);
                centerx =  scrollArea->FeatPtDataSet->m_ImgPtSet.vecPts.at(idx).X;
                centery =  scrollArea->FeatPtDataSet->m_ImgPtSet.vecPts.at(idx).Y;
                scrollArea->CenterRasterPoint(centerx,centery);
                break;
            }
            if(LeftFeatPtDataset.FindManualPtIndexByID(ID,idx))
            {
                //RightScrollArea->FeatManualSelectedIdx.append(idx);
                // RightScrollArea->FeatAutoSelectedIdx.append(idx);
                centerx =  scrollArea->FeatPtDataSet->m_ImgPtSet.vecAddPts.at(idx).X;
                centery =  scrollArea->FeatPtDataSet->m_ImgPtSet.vecAddPts.at(idx).Y;
                scrollArea->CenterRasterPoint(centerx,centery);
                break;
            }
        }
    }

    UpdateFeatPtTableView();
}

void ImageViewer::SelectRightPtsByLeftSelection()
{
    // 根据选中左图点找选中的右图点
    RightScrollArea->FeatAutoSelectedIdx.clear();
    RightScrollArea->FeatManualSelectedIdx.clear();
    for(int i = 0; i < scrollArea->FeatAutoSelectedIdx.count(); i++)
    {
        UINT lidx = scrollArea->FeatAutoSelectedIdx.at(i);
        ULONG ID = LeftFeatPtDataset.m_ImgPtSet.vecPts.at(lidx).lID;
        UINT idx ;
        if(RightFeatPtDataset.FindAutoPtIndexByID(ID,idx))
        {
            RightScrollArea->FeatAutoSelectedIdx.append(idx);
            continue;
        }
        if(RightFeatPtDataset.FindManualPtIndexByID(ID,idx))
        {
            RightScrollArea->FeatManualSelectedIdx.append(idx);
        }
    }
    for(int i = 0; i< scrollArea->FeatManualSelectedIdx.count(); i++)
    {
        UINT lidx = scrollArea->FeatManualSelectedIdx.at(i);
        ULONG ID = LeftFeatPtDataset.m_ImgPtSet.vecAddPts.at(lidx).lID;
        UINT idx ;
        if(RightFeatPtDataset.FindAutoPtIndexByID(ID,idx))
        {
            RightScrollArea->FeatAutoSelectedIdx.append(idx);
            continue;
        }
        if(RightFeatPtDataset.FindManualPtIndexByID(ID,idx))
        {
            RightScrollArea->FeatManualSelectedIdx.append(idx);
        }
    }

}
void ImageViewer::SelectLeftPtsByRightSelection()
{
    // 根据选中右图点找选中的左图点
    scrollArea->FeatAutoSelectedIdx.clear();
    scrollArea->FeatManualSelectedIdx.clear();
    for(int i = 0; i < RightScrollArea->FeatAutoSelectedIdx.count(); i++)
    {
        UINT lidx = RightScrollArea->FeatAutoSelectedIdx.at(i);
        ULONG ID = RightFeatPtDataset.m_ImgPtSet.vecPts.at(lidx).lID;
        UINT idx ;
        if(LeftFeatPtDataset.FindAutoPtIndexByID(ID,idx))
        {
            scrollArea->FeatAutoSelectedIdx.append(idx);
            continue;
        }
        if(LeftFeatPtDataset.FindManualPtIndexByID(ID,idx))
        {
            scrollArea->FeatManualSelectedIdx.append(idx);
        }
    }
    for(int i = 0; i< RightScrollArea->FeatManualSelectedIdx.count(); i++)
    {
        UINT lidx = RightScrollArea->FeatManualSelectedIdx.at(i);
        ULONG ID = RightFeatPtDataset.m_ImgPtSet.vecAddPts.at(lidx).lID;
        UINT idx ;
        if(LeftFeatPtDataset.FindAutoPtIndexByID(ID,idx))
        {
            scrollArea->FeatAutoSelectedIdx.append(idx);
            continue;
        }
        if(LeftFeatPtDataset.FindManualPtIndexByID(ID,idx))
        {
            scrollArea->FeatManualSelectedIdx.append(idx);
        }
    }
}

//初始化三个表格窗口的结构
void ImageViewer::InitializeTableview()
{
    // 点浮动窗口

    QDockWidget *dock = new QDockWidget(tr("PointTableView"), this);
    dock->setAllowedAreas(Qt::BottomDockWidgetArea | Qt::TopDockWidgetArea | Qt::LeftDockWidgetArea | Qt::RightDockWidgetArea);
    PointTableView = new QTableView(dock);
    dock->setWidget(PointTableView);
    addDockWidget(Qt::BottomDockWidgetArea,dock);
    viewMenu->addAction(dock->toggleViewAction());

    pMatchPtsTableModel = new QStandardItemModel(0,7);
    pMatchPtsTableModel->setHeaderData(0, Qt::Horizontal,QObject::tr("LeftPointX"));
    pMatchPtsTableModel->setHeaderData(1, Qt::Horizontal,QObject::tr("LeftPointY"));
    pMatchPtsTableModel->setHeaderData(2, Qt::Horizontal,QObject::tr("RightPointX"));
    pMatchPtsTableModel->setHeaderData(3, Qt::Horizontal,QObject::tr("RightPointY"));
    pMatchPtsTableModel->setHeaderData(4, Qt::Horizontal,QObject::tr("GeoX"));
    pMatchPtsTableModel->setHeaderData(5, Qt::Horizontal,QObject::tr("GeoY"));
    pMatchPtsTableModel->setHeaderData(6, Qt::Horizontal,QObject::tr("GeoZ"));

    // 使tableview处于不可编辑状态
    PointTableView->setEditTriggers(QAbstractItemView::NoEditTriggers);
    // 使tableview只能一行一行选中
    PointTableView->setSelectionBehavior(QAbstractItemView::SelectRows);
    PointTableView->setModel(pMatchPtsTableModel);
    connect(PointTableView,SIGNAL(doubleClicked(QModelIndex)),this,SLOT(on_PointTableView_doubleClicked(QModelIndex)));
    dock->hide();

    // 文件浮动窗口
    dock = new QDockWidget(tr("FileTableView"), this);
    dock->setAllowedAreas(Qt::BottomDockWidgetArea | Qt::TopDockWidgetArea | Qt::LeftDockWidgetArea | Qt::RightDockWidgetArea);
    FileTableView = new QTableView(dock);
    dock->setWidget(FileTableView);
    addDockWidget(Qt::BottomDockWidgetArea,dock);
    viewMenu->addAction(dock->toggleViewAction());
    dock->hide();

    // 其他浮动窗口
    dock = new QDockWidget(tr("CTableView"), this);
    dock->setAllowedAreas(Qt::BottomDockWidgetArea | Qt::TopDockWidgetArea | Qt::LeftDockWidgetArea | Qt::RightDockWidgetArea);
    CustomTableView = new QTableView(dock);
    dock->setWidget(CustomTableView);
    addDockWidget(Qt::BottomDockWidgetArea,dock);
    viewMenu->addAction(dock->toggleViewAction());
    dock->hide();

    //左featpttable窗口
    dock = new QDockWidget(tr("LeftPointTableView"),this);
    dock->setAllowedAreas(Qt::BottomDockWidgetArea | Qt::TopDockWidgetArea | Qt::LeftDockWidgetArea | Qt::RightDockWidgetArea);
    LeftFeatPtTableView = new QTableView(dock);
    dock->setWidget(LeftFeatPtTableView);
    addDockWidget(Qt::BottomDockWidgetArea,dock);
    viewMenu->addAction(dock->toggleViewAction());
    LeftFeatPtTableModel = new QStandardItemModel(0,3);
    LeftFeatPtTableModel->setHeaderData(0, Qt::Horizontal,QObject::tr("ID"));
    LeftFeatPtTableModel->setHeaderData(1, Qt::Horizontal,QObject::tr("X"));
    LeftFeatPtTableModel->setHeaderData(2, Qt::Horizontal,QObject::tr("Y"));
    LeftFeatPtTableView->setModel(LeftFeatPtTableModel);
    connect(LeftFeatPtTableView,SIGNAL(doubleClicked(QModelIndex)),SLOT(on_LeftFeatPtTableView_doubleClicked(QModelIndex)));

    //右featpttable窗口
    dock = new QDockWidget(tr("RightPointTableView"),this);
    dock->setAllowedAreas(Qt::BottomDockWidgetArea | Qt::TopDockWidgetArea | Qt::LeftDockWidgetArea | Qt::RightDockWidgetArea);
    RightFeatPtTableView = new QTableView(dock);
    dock->setWidget(RightFeatPtTableView);
    addDockWidget(Qt::BottomDockWidgetArea,dock);
    viewMenu->addAction(dock->toggleViewAction());
    RightFeatPtTableModel = new QStandardItemModel(0,3);
    RightFeatPtTableModel->setHeaderData(0, Qt::Horizontal,QObject::tr("ID"));
    RightFeatPtTableModel->setHeaderData(1, Qt::Horizontal,QObject::tr("X"));
    RightFeatPtTableModel->setHeaderData(2, Qt::Horizontal,QObject::tr("Y"));
    RightFeatPtTableView->setModel(RightFeatPtTableModel);
    connect(RightFeatPtTableView,SIGNAL(doubleClicked(QModelIndex)),SLOT(on_RightFeatPtTableView_doubleClicked(QModelIndex)));




    return;






    //QStandardItemModel *model = new QStandardItemModel(4,2);
    QStandardItemModel *model = new QStandardItemModel(3,4);

    QList<QStandardItem * > *ddd =new QList<QStandardItem * >;
    // ddd.append(qi);
    // qi = new QStandardItem("x");
    // ddd.append(qi);
    // int z = ddd.count();
    /*
        model->appendColumn(ddd);
        model->appendColumn(ddd);
        model->appendColumn(ddd);
    */

    model->setHeaderData(0, Qt::Horizontal,QObject::tr("index"));
    model->setHeaderData(1, Qt::Horizontal, QObject::tr("x"));
    model->setHeaderData(2,Qt::Horizontal,QObject::tr("y"));



    PointTableView->setModel(model);
    QStandardItem* qi = new QStandardItem("dfdf");
    ddd->append(qi);
    qi = new QStandardItem("zzzdsf");
    qi->setData("liudehua");
    ddd->append(qi);
    QString www =  qi->data().toString();
    QString qqq = qi->accessibleText();
    QString eee = qi->text();

    qi = new QStandardItem("sdfsdf");
    ddd->append(qi);
    model->appendRow(*ddd);
    // model->appendRow(*ddd);
    // 使tableview处于不可编辑状态
    PointTableView->setEditTriggers(QAbstractItemView::NoEditTriggers);
    // 使tableview只能一行一行选中
    PointTableView->setSelectionBehavior(QAbstractItemView::SelectRows);
    // 响应tableview的doubleclicked信号槽
    connect(PointTableView,SIGNAL(doubleClicked(QModelIndex)),this,SLOT(on_PointTableView_clicked(QModelIndex)));

    for(int i = 0; i < model->rowCount() ; i++)
        for(int j =0; j<model->columnCount(); j++)
        {
            QModelIndex index = model->index(i,j);
            model->setData(index,QVariant(j+i));
        }
}

void ImageViewer::rightopen()
{
    QString fileName = QFileDialog::getOpenFileName(this,
                       tr("Open File"), QDir::currentPath());
    QByteArray gb = fileName.toLatin1();
    char* dd = gb.data();
    RightScrollArea->LoadImage(dd);
    return;
}
void ImageViewer::OpenMatchPts()
{

    // QString fileName = QFileDialog::getOpenFileName(this,
//                       tr("Open File"), QDir::currentPath());
    DialogOpenMatchPts cd;
    cd.setWindowTitle(tr("OpenMatchPts"));
    if(!cd.exec())
    {
        return;
    }
    //  QByteArray gb = fileName.toLatin1();
    QByteArray gb = cd.srcfilenameLeft.toLatin1();
    char* dd = gb.data();
    string filepath = (string)(dd);

    gb = cd.srcfilenameRight.toLatin1();
    dd = gb.data();
    string filepath2 = (string)(dd);


    if(cd.srcfilenameLeft.trimmed() != "")
    {
        LeftFeatPtDataset.ClearData();
        LeftFeatPtDataset.SetPtData(filepath);
        scrollArea->FeatAutoSelectedIdx.clear();
        scrollArea->FeatManualSelectedIdx.clear();
        AddLeftMatchPoints();

    }
    if(cd.srcfilenameRight.trimmed() !="")
    {
        RightFeatPtDataset.ClearData();
        RightFeatPtDataset.SetPtData(filepath2);
        RightScrollArea->FeatAutoSelectedIdx.clear();
        RightScrollArea->FeatManualSelectedIdx.clear();
        AddRightMatchPoints();
    }
    CheckIsMatch();

    LeftPtPath = filepath;
    RightPtPath = filepath2;



//    CmlStereoProc CSP;
//    vector<StereoMatchPt> vecStereoPt;
//    try
//    {
//       // CSP.ReadFeatMatchPts(filepath,vecStereoPt);
//        CSP.ReadFeatMatchPts(filepath,filepath2, vecStereoPt);
//    }
//    catch(...)
//    {
//        return;
//    }
//    ClearMatchPoints();
//    scrollArea->MatchPointList.clear();
//    RightScrollArea->MatchPointList.clear();
//    int ddd = vecStereoPt.size();
//    AddMatchPoints(&vecStereoPt);

}

void ImageViewer::SavePtToFile()
{
    DialogSavePtToFile cd;
    cd.setWindowTitle(tr("Save PT To File"));
    if(!cd.exec())
    {
        return;
    }
    QByteArray gb = cd.dstfilenameLeft.toLatin1();
    char* dd = gb.data();
    string filepath = (string)(dd);

    gb = cd.dstfilenameRight.toLatin1();
    dd = gb.data();
    string filepath2 = (string)(dd);
    if(cd.dstfilenameLeft.trimmed() != "")
    {
        if(!LeftFeatPtDataset.SavePtData(filepath))
        {
//            QMessageBox::about(this, tr("Error"),
//                               tr("Left PT Save Failed!"));
        }

    }
    if(cd.dstfilenameRight.trimmed() != "")
    {
        if(!RightFeatPtDataset.SavePtData(filepath2))
        {
//            QMessageBox::about(this, tr("Error"),
//                               tr("Right PT Save Failed!"));
        }
    }

}

void ImageViewer::SavePt()
{
    if(LeftPtPath != "")
    {
        if(!LeftFeatPtDataset.SavePtData(LeftPtPath))
        {
//            QMessageBox::about(this, tr("Error"),
//                               tr("Left PT Save Failed!"));
        }

    }
    if(RightPtPath != "")
    {
        if(!RightFeatPtDataset.SavePtData(RightPtPath))
        {
//            QMessageBox::about(this, tr("Error"),
//                               tr("Right PT Save Failed!"));
        }
    }
}

void ImageViewer::AddLeftMatchPoints()
{
    if(LeftFeatPtTableModel != NULL)
    {
        delete LeftFeatPtTableModel;
        LeftFeatPtTableModel = NULL;
    }
    int tableviewrows = LeftFeatPtDataset.m_ImgPtSet.vecPts.size() + LeftFeatPtDataset.m_ImgPtSet.vecAddPts.size();

    LeftFeatPtTableModel = new QStandardItemModel(tableviewrows,3);
    LeftFeatPtTableModel->setHeaderData(0, Qt::Horizontal,QObject::tr("ID"));
    LeftFeatPtTableModel->setHeaderData(1, Qt::Horizontal,QObject::tr("X"));
    LeftFeatPtTableModel->setHeaderData(2, Qt::Horizontal,QObject::tr("Y"));
    for(int i = 0; i<LeftFeatPtDataset.m_ImgPtSet.vecPts.size(); i++)
    {
        QModelIndex index;
        index = LeftFeatPtTableModel->index(i,0);
        LeftFeatPtTableModel->setData(index,QVariant(LeftFeatPtDataset.m_ImgPtSet.vecPts.at(i).lID));
        index = LeftFeatPtTableModel->index(i,1);
        LeftFeatPtTableModel->setData(index,QVariant(LeftFeatPtDataset.m_ImgPtSet.vecPts.at(i).X));
        index = LeftFeatPtTableModel->index(i,2);
        LeftFeatPtTableModel->setData(index,QVariant(LeftFeatPtDataset.m_ImgPtSet.vecPts.at(i).Y));
    }
    int vecsize = LeftFeatPtDataset.m_ImgPtSet.vecPts.size();
    for(int i = 0; i<LeftFeatPtDataset.m_ImgPtSet.vecAddPts.size(); i++)
    {
        QModelIndex index;
        index = LeftFeatPtTableModel->index(i + vecsize,0);
        LeftFeatPtTableModel->setData(index,QVariant(LeftFeatPtDataset.m_ImgPtSet.vecAddPts.at(i).lID));
        index = LeftFeatPtTableModel->index(i + vecsize,1);
        LeftFeatPtTableModel->setData(index,QVariant(LeftFeatPtDataset.m_ImgPtSet.vecAddPts.at(i).X));
        index = LeftFeatPtTableModel->index(i + vecsize,2);
        LeftFeatPtTableModel->setData(index,QVariant(LeftFeatPtDataset.m_ImgPtSet.vecAddPts.at(i).Y));
    }
    // 使tableview处于不可编辑状态
    LeftFeatPtTableView->setEditTriggers(QAbstractItemView::NoEditTriggers);
    // 使tableview只能一行一行选中
    LeftFeatPtTableView->setSelectionBehavior(QAbstractItemView::SelectRows);
    LeftFeatPtTableView->setModel(LeftFeatPtTableModel);

}
void ImageViewer::AddRightMatchPoints()
{
    if(RightFeatPtTableModel != NULL)
    {
        delete RightFeatPtTableModel;
        RightFeatPtTableModel = NULL;
    }
    int tableviewrows = RightFeatPtDataset.m_ImgPtSet.vecPts.size() + RightFeatPtDataset.m_ImgPtSet.vecAddPts.size();

    RightFeatPtTableModel = new QStandardItemModel(tableviewrows,3);
    RightFeatPtTableModel->setHeaderData(0, Qt::Horizontal,QObject::tr("ID"));
    RightFeatPtTableModel->setHeaderData(1, Qt::Horizontal,QObject::tr("X"));
    RightFeatPtTableModel->setHeaderData(2, Qt::Horizontal,QObject::tr("Y"));
    for(int i = 0; i<RightFeatPtDataset.m_ImgPtSet.vecPts.size(); i++)
    {
        QModelIndex index;
        index = RightFeatPtTableModel->index(i,0);
        RightFeatPtTableModel->setData(index,QVariant(RightFeatPtDataset.m_ImgPtSet.vecPts.at(i).lID));
        index = RightFeatPtTableModel->index(i,1);
        RightFeatPtTableModel->setData(index,QVariant(RightFeatPtDataset.m_ImgPtSet.vecPts.at(i).X));
        index = RightFeatPtTableModel->index(i,2);
        RightFeatPtTableModel->setData(index,QVariant(RightFeatPtDataset.m_ImgPtSet.vecPts.at(i).Y));
        // 如果不用new一个新对象当QStandardItem析构时 tablemodel会报错， tablemodel运行clear的时候是不是会自动析构？
    }
    int vecsize = RightFeatPtDataset.m_ImgPtSet.vecPts.size();
    for(int i = 0; i<RightFeatPtDataset.m_ImgPtSet.vecAddPts.size(); i++)
    {
        QModelIndex index;
        index = RightFeatPtTableModel->index(i + vecsize,0);
        RightFeatPtTableModel->setData(index,QVariant(RightFeatPtDataset.m_ImgPtSet.vecAddPts.at(i).lID));
        index = RightFeatPtTableModel->index(i + vecsize,1);
        RightFeatPtTableModel->setData(index,QVariant(RightFeatPtDataset.m_ImgPtSet.vecAddPts.at(i).X));
        index = RightFeatPtTableModel->index(i + vecsize,2);
        RightFeatPtTableModel->setData(index,QVariant(RightFeatPtDataset.m_ImgPtSet.vecAddPts.at(i).Y));
        // 如果不用new一个新对象当QStandardItem析构时 tablemodel会报错， tablemodel运行clear的时候是不是会自动析构？
    }
    // 使tableview处于不可编辑状态
    RightFeatPtTableView->setEditTriggers(QAbstractItemView::NoEditTriggers);
    // 使tableview只能一行一行选中
    RightFeatPtTableView->setSelectionBehavior(QAbstractItemView::SelectRows);
    RightFeatPtTableView->setModel(LeftFeatPtTableModel);
    RightFeatPtTableView->setModel(RightFeatPtTableModel);

}
void ImageViewer::CheckIsMatch()
{
    for(int i =0; i< LeftFeatPtDataset.m_ImgPtSet.vecPts.size(); ++i )
    {
        Pt2d* ptCur = &LeftFeatPtDataset.m_ImgPtSet.vecPts.at(i);
        Pt2d ptRes;

        UINT nIndex = -1;
        if( true == RightFeatPtDataset.FindAutoPtIndexByID( ptCur->lID, nIndex ) )
        {
            // Pt2d* ptTemp =
            ptCur->byIsMatch = 1;
            RightFeatPtDataset.m_ImgPtSet.vecPts.at(nIndex).byIsMatch = 1;
            continue;
        }
        if(true == RightFeatPtDataset.FindManualPtIndexByID( ptCur->lID, nIndex ))
        {
            ptCur->byIsMatch = 1;
            RightFeatPtDataset.m_ImgPtSet.vecAddPts.at(nIndex).byIsMatch = 1;
            continue;
        }
    }

    for(int i =0; i< LeftFeatPtDataset.m_ImgPtSet.vecAddPts.size(); ++i )
    {
        Pt2d* ptCur = &LeftFeatPtDataset.m_ImgPtSet.vecAddPts.at(i);
        Pt2d ptRes;

        UINT nIndex = -1;
        if(true == RightFeatPtDataset.FindManualPtIndexByID( ptCur->lID, nIndex ))
        {
            ptCur->byIsMatch = 1;
            RightFeatPtDataset.m_ImgPtSet.vecAddPts.at(nIndex).byIsMatch = 1;
            continue;
        }
        if( true == RightFeatPtDataset.FindAutoPtIndexByID( ptCur->lID, nIndex ) )
        {
            // Pt2d* ptTemp =
            ptCur->byIsMatch = 1;
            RightFeatPtDataset.m_ImgPtSet.vecPts.at(nIndex).byIsMatch = 1;
            continue;
        }
    }

}


void ImageViewer::OpenDenseMatchPts()
{
    DialogOpenMatchPts cd;
    cd.setWindowTitle(tr("OpenDenseMatchPts"));
    if(!cd.exec())
    {
        return;
    }
    //  QByteArray gb = fileName.toLatin1();
    QByteArray gb = cd.srcfilenameLeft.toLatin1();
    char* dd = gb.data();
    string filepath = (string)(dd);

    gb = cd.srcfilenameRight.toLatin1();
    dd = gb.data();
        return;

}

void ImageViewer::open()
//! [1] //! [2]
{
//    QString fileName = QFileDialog::getOpenFileName(this,
//                       tr("Open File"), QDir::currentPath());
//    QByteArray gb = fileName.toLatin1();
//    char* dd = gb.data();
//    if(fileName == "")
//    {
//        return;
//    }
    // scrollArea->LoadImage(dd);
    DialogOpenMatchPts cd;
    cd.setWindowTitle(tr("Open Image File"));
    if(!cd.exec())
    {
        return;
    }
    //  QByteArray gb = fileName.toLatin1();
    QByteArray gb = cd.srcfilenameLeft.toLatin1();
    char* dd = gb.data();
    string filepath = (string)(dd);

    char* dd2;
    QByteArray gb2 = cd.srcfilenameRight.toLatin1();
    dd2 = gb2.data();
    string filepath2 = (string)(dd2);


    if(cd.srcfilenameLeft.trimmed() != "")
    {
        scrollArea->LoadImage(dd);
    }
    if(cd.srcfilenameRight.trimmed() !="")
    {
        RightScrollArea->LoadImage(dd2);
    }
    strLeftOpenedImg = filepath;
    strRightOpenedImg = filepath2;
    string strKey = "/StereoImg/";
    int nKeyLength = strKey.length();
    string strTarget = "/MatchRes/";
    int nTLength = strTarget.length();

    string strTempLPath, strTempRPath;
    int nTLPos = strLeftOpenedImg.find( strKey.c_str() );
    int nTRPos = strRightOpenedImg.find( strKey.c_str() );
    strTempLPath = strLeftOpenedImg;
    strTempRPath = strRightOpenedImg;
    if( ( nTLPos < 0 )||( nTLPos < 0 ) )
    {
        return;
    }

    strTempLPath.replace( nTLPos, nKeyLength, strTarget, 0, nTLength );
    strTempRPath.replace( nTRPos, nKeyLength, strTarget, 0, nTLength );
    int nTempLPos = strTempLPath.rfind(".");
    int nTempRPos = strTempRPath.rfind(".");
    string strPrefix;
    if( cd.bAdddmf == true )
    {
        strPrefix = ".dmf";
    }
    else if( cd.bAddfmf == true )
    {
        strPrefix = ".fmf";
    }
    else if( cd.bAddtmf == true )
    {
        strPrefix = ".tmf";
    }

    if( ( nTempLPos < 0 )||( nTempRPos < 0 ) )
    {
        return;
    }

    strTempLPath.replace( nTempLPos, (strTempLPath.length()-nTempLPos), strPrefix, 0, strPrefix.length()  );
    strTempRPath.replace( nTempRPos, (strTempRPath.length()-nTempRPos), strPrefix, 0, strPrefix.length()  );

    LeftFeatPtDataset.ClearData();
    RightFeatPtDataset.ClearData();
    if( true == LeftFeatPtDataset.SetPtData( strTempLPath) )
    {
        scrollArea->FeatAutoSelectedIdx.clear();
        scrollArea->FeatManualSelectedIdx.clear();
        AddLeftMatchPoints();
        LeftPtPath = strTempLPath;
    }

    if( true == RightFeatPtDataset.SetPtData( strTempRPath ) )
    {
        RightScrollArea->FeatAutoSelectedIdx.clear();
        RightScrollArea->FeatManualSelectedIdx.clear();
        AddRightMatchPoints();
        RightPtPath = strTempRPath;

    }
    CheckIsMatch();


    return;
}
void ImageViewer::CameraCalibration()
{
    // mlCamCalib("","");
    DialogCamera cd;
    cd.setWindowTitle(tr("CameraCalibration"));
    if(cd.exec())
    {
        QByteArray gbsrc   = cd.srcfilename.toLatin1();
        SCHAR* srcPath = gbsrc.data() ;
        QByteArray gbdest1 = cd.dstfilename.toLatin1();
        SCHAR* destPath1 = gbdest1.data() ;
        QByteArray gbdest2 = cd.dstfilenameAccInfo.toLatin1();
        SCHAR* destPath2 = gbdest2.data() ;
        string strInputPath(srcPath) ;
        string strCamInfoFile(destPath1) ;
        string strCamAccuracyFile(destPath2);
//        SINT nPos = strInputPath.rfind("/");
//        strCamInfoFile.assign( strInputPath, 0, nPos );
//        strCamAccuracyFile = strCamInfoFile ;
//        strCamInfoFile.append( "/camInfo.txt" );
//        strCamAccuracyFile.append("/accuracyInfo.txt") ;

//  老版本
//        CmlCamCalib camDemo ;
//        // 读取标志点信息文件判断时单相机还是立体相机标定信息
//        std::ifstream stmOpen(srcPath) ;
//        SINT nWidth , nHeight , nFlag ;
//        stmOpen >> nWidth >> nHeight >> nFlag ;
//        stmOpen.close() ;
//        if(!nFlag) // 单相机标定
//        {
//            camDemo.mlCamCalib(srcPath , strCamInfoFile.c_str()) ;
//            camDemo.mlImgErrorCheck(srcPath , strCamAccuracyFile.c_str() , strCamInfoFile.c_str()) ;
//        }
//        else
//        {
//            camDemo.mlStereoCalib(srcPath , strCamInfoFile.c_str()) ;
//            camDemo.mlObjErrorCheck(srcPath , strCamAccuracyFile.c_str() , strCamInfoFile.c_str()) ;
//        }
        CCamCalibIO camIO ;
        if(camIO.readCamSignPts(strInputPath))
        {
            if(camIO.nCamNum == 1)
            {
                mlSingleCamCalib(camIO.vecLImg2DPts , camIO.vecObj3DPts , camIO.m_nW , camIO.m_nH , camIO.inLCamPara , camIO.exLCamPara , camIO.vecErrorPts) ;
            }
            else if(camIO.nCamNum == 2)
            {
                mlStereoCamCalib(camIO.vecLImg2DPts , camIO.vecRImg2DPts , camIO.vecObj3DPts , camIO.m_nW , camIO.m_nH ,
                                 camIO.inLCamPara , camIO.inRCamPara , camIO.exLCamPara , camIO.exRCamPara , camIO.exStereoPara ,camIO.vecErrorPts) ;
            }
            // 写相机内参数信息
            camIO.writeCamInfoFile(strCamInfoFile.c_str()) ;
            camIO.writeAccuracyFile(strCamAccuracyFile.c_str()) ;
        }

    }

}

void ImageViewer::CameraSurvey()
{
//    DialogCameraSurvey cd;
//
//    if(cd.exec())
//    {
//        //输入特征点文件
//        QByteArray gbfea   = cd.FeatPtfile.toLatin1();
//        char * FeaPath = gbfea.data();
//        //输入内方位元素初始值
//        QByteArray gbinele   = cd.IntEle.toLatin1();
//        char * ElePath = gbinele.data();
//        //指定输出文件的路径
//        QByteArray gbdst = cd.dstfilename.toLatin1();
//        char * dstPath = gbdst.data();
//
//        ifstream stmele(ElePath);
//        CCamCalibIO camIO ;
//        // 读取标志点信息文件
//        string strInputPath(FeaPath) ;
//        if(camIO.readCamSignPts(strInputPath))
//        {
//            if(stmele.is_open())
//            {
//                stmele >> camIO.inLCamPara.f >> camIO.inLCamPara.x >> camIO.inLCamPara.y;
//            }
//            stmele.close();
//            mlMonoSurvey(camIO.vecLImg2DPts, camIO.vecObj3DPts, camIO.inLCamPara , camIO.exLCamPara ) ;
//        }
//        //输出单目量测结果
//        ofstream monoresult(dstPath);
//        if(monoresult.is_open())
//        {
//            monoresult << camIO.exLCamPara.pos.X <<" "<<camIO.exLCamPara.pos.Y<<" "<<camIO.exLCamPara.pos.Z<<" "<<camIO.exLCamPara.ori.omg<<" "<<camIO.exLCamPara.ori.phi<<" "<<camIO.exLCamPara.ori.kap;
//        }
//        monoresult.close();
//    }
    DialogCameraSurvey cd;

    if(cd.exec())
    {
        //输入特征点文件
        QByteArray gbfea   = cd.FeatPtfile.toLatin1();
        char * FeaPath = gbfea.data();
        //输入内方位元素初始值
        QByteArray gbinele   = cd.IntEle.toLatin1();
        char * ElePath = gbinele.data();
        //指定输出文件的路径
        QByteArray gbdst = cd.dstfilename.toLatin1();
        char * dstPath = gbdst.data();

        ifstream stmele(ElePath);
        CCamCalibIO camIO ;
        // 读取标志点信息文件
        string strInputPath(FeaPath) ;
        ifstream stmFeatPt(FeaPath);
        ifstream stmGCP(ElePath);
        vector<Pt2d> vecFeatPts;
        vector<Pt3d> vecGCPs;

        int nFeatPts = 0;
        int nGCPs = 0;
        double df = 0;
        stmFeatPt >> df;
        stmFeatPt >> nFeatPts;
        stmGCP >> nGCPs;
        for( int i = 0; i < nGCPs; ++i )
        {
            Pt3d ptCur;
            stmGCP >> ptCur.lID >> ptCur.X >>ptCur.Y >>ptCur.Z;
            vecGCPs.push_back( ptCur );
        }
        for( int i = 0; i < nFeatPts; ++i )
        {
            Pt2d ptCur;
            stmFeatPt >> ptCur.lID >> ptCur.X >>ptCur.Y;
            vecFeatPts.push_back( ptCur );
        }
        ImgPtSet imgPts;
        imgPts.vecPts = vecFeatPts;
        imgPts.imgInfo.inOri.f = df;
        ExOriPara exOri;
        vector<RMS2d> vecRMSRes;
        double dTotalRMS;
        mlLocalBySImgIntersection(  vecGCPs, imgPts,  exOri, vecRMSRes, dTotalRMS  );



        //输出单目量测结果
        ofstream monoresult(dstPath);
        if(monoresult.is_open())
        {
            monoresult << camIO.exLCamPara.pos.X <<" "<<camIO.exLCamPara.pos.Y<<" "<<camIO.exLCamPara.pos.Z<<" "<<camIO.exLCamPara.ori.omg<<" "<<camIO.exLCamPara.ori.phi<<" "<<camIO.exLCamPara.ori.kap;
        }
        monoresult.close();
    }
}
void ImageViewer::SeriesImageDem()
{
    //mlSiteImageMapping("","");
}
void ImageViewer::OrbitImageDem()
{
    DialogOrbitImageDEM cd;
    cd.setWindowTitle(tr("OrbitImageDem"));
    if(cd.exec())
    {
        QByteArray gbsrc   = cd.srcfilename.toLatin1();
        char * srcPath = gbsrc.data();
        QByteArray gbdem   = cd.dstDEMfilename.toLatin1();
        char * demPath = gbdem.data();
        QByteArray gbdom   = cd.dstDOMfilename.toLatin1();
        char * domPath = gbdom.data();

        DOUBLE dRes = cd.dResolution;
        bool bBaseLeft = cd.bBasedOnLeftImg;
        bool bUsePts = cd.bUseMatchPoints;
        CmlSatMapProj mapproj;
        if(bUsePts)
        {
            char * srcPath = "/home/lyl612/RabbitVCS/rvml/DL/CODE/trunk/program/UnitTestData/TestSatMappingData/CE-2/CE2Proj.txt";
            char * demPath = "/home/lyl612/RabbitVCS/rvml/DL/CODE/trunk/program/UnitTestData/TestSatMappingData/CE-2/result/today1.tif";
            char * domPath = "/home/lyl612/RabbitVCS/rvml/DL/CODE/trunk/program/UnitTestData/TestSatMappingData/CE-2/result/today2.tif";
            mapproj.SatMapping( srcPath, demPath, domPath, dRes, bBaseLeft,bUsePts );
        }
        else
        {
            //char * srcPath = "/home/lyl612/RabbitVCS/rvml/DL/CODE/trunk/program/UnitTestData/TestSatMappingData/OrbitImg.txt";
            //char * demPath = "/home/lyl612/RabbitVCS/rvml/DL/CODE/trunk/program/UnitTestData/TestSatMappingData/today1.tif";
            //char * domPath = "/home/lyl612/RabbitVCS/rvml/DL/CODE/trunk/program/UnitTestData/TestSatMappingData/today2.tif";
            mapproj.SatMapping( srcPath, demPath, domPath, dRes, bBaseLeft,bUsePts );
        }
    }
}
void ImageViewer::PanoMosaic()
{
    DialogPanoMosaic cd;
    cd.setWindowTitle(tr("PanoMosaic"));
    if(cd.exec())
    {
        QByteArray qPanoSrc = cd.srcfilename.toLatin1();
        char *cProjectFile = qPanoSrc.data();
        QByteArray qPanoDst = cd.dstfilename.toLatin1();
        char *cOutputFile = qPanoDst.data();


        CmlSiteMapProj clsSMapProj;
        clsSMapProj.LoadProj(cProjectFile);

        vector<char*> cParamList;

        cParamList.push_back("opencv_stitching");
        cParamList.push_back("--warp");
        cParamList.push_back("cylindrical");
        cParamList.push_back("--match_conf");
        cParamList.push_back("0.65");
        clsSMapProj.Mosaic( cParamList, cOutputFile );




    }
}
void ImageViewer::PersImageCreate()
{
    DialogPersImageCreate cd;
    cd.setWindowTitle(tr("PersImageCreate"));
    if(cd.exec())
    {
        QByteArray qPanoSrc = cd.srcfilename.toLatin1();
        char * cPanoSrc = qPanoSrc.data();
        QByteArray qPresDstSrc = cd.dstfilename.toLatin1();
        char * cPresDstSrc = qPresDstSrc.data();

        mlPano2Prespective(cPanoSrc, cPresDstSrc, cd.nLTX, cd.nLTY, cd.nWidth, cd.nHight, cd.dFocus);
    }
}
void ImageViewer::SiteDemMosaic()
{
    DialogSiteDEMMosaic cd;
    cd.setWindowTitle(tr("SiteDemMosaic"));
    if(cd.exec())
    {
        QByteArray gbsrc   = cd.srcfilename.toLatin1();
        QByteArray gbdst1 = cd.dstDEMfilename.toLatin1() ;
        QByteArray gbdst2 = cd.dstDOMfilename.toLatin1() ;
        double dRange = cd.dMapRange ;
        double dRes = cd.dResolution ;
        SCHAR* srcPath = gbsrc.data();

        //SCHAR* srcPath = "/home/whwan/trunk/program/TestProj/TestBacc1Epi/1.smp";
        //SCHAR* srcPath = "/home/whwan/trunk/program/TestProj/TestTemp/1.smp";
        //SCHAR* srcPath = "/home/whwan/trunk/program/TestProj/SiteMapping/Mars/SiteMapping.smp";

        string strInputPath(srcPath);
        SCHAR* dstPath1 = gbdst1.data();
        string strDemFile(dstPath1);
        //string strDemFile(  "/home/whwan/trunk/program/TestProj/TestBacc1Epi/DemAll2.tif" );

        SCHAR* dstPath2 = gbdst2.data();
        string strDomFile(dstPath2);
        //string strDomFile( "/home/whwan/trunk/program/TestProj/TestBacc1Epi/DomAll2.tif"  );

        CmlSiteMapProj site;
        Pt3d ptCenter;
        if( false == site.LoadProj( strInputPath ) )
        {
            return;
        }
        site.GetSiteCenter( ptCenter);

        double dLTx = ptCenter.X - dRange ;
        double dLTy = ptCenter.Y + dRange ;
        double dRTx = ptCenter.X + dRange ;
        double dRTy = ptCenter.Y - dRange ;

        ExtractFeatureOpt extractPtsOpts;
        extractPtsOpts.dThresCoef = 0.1;
        MatchInRegPara matchOpts;
        RANSACHomePara ransacOpts;
        matchOpts.dXMax = 0;
        matchOpts.dXMin = -300;
        matchOpts.dYMax = 2;
        matchOpts.dYMin = -2;
        MedFilterOpts mFilterOpts;
        site.CreateDemAndDom( dLTx, dLTy, dRTx, dRTy, dRes, 1, 1, -1, true, false, extractPtsOpts, matchOpts, ransacOpts, mFilterOpts, strDemFile.c_str(), strDomFile.c_str());
    }
}

void ImageViewer::MultSiteDemMosaic()
{
    DialogMultSiteDemMosaic cd;
    cd.setWindowTitle(tr("MultSiteDemMosaic"));
    if(cd.exec())
    {

    }
}
void ImageViewer::WideBaselineMap()
{
    DialogWideBaselineMap cd;
    cd.setWindowTitle(tr("WideBaselineMap"));
    if(cd.exec())
    {
        //设定长基线测图参数
        WideOptions WidePara;
        WidePara.bIsUsingFeatPt = false;
        WidePara.bIsUsingPartion = true;
        WidePara.dCoef = cd.dCoef;
        WidePara.nRadiusX = cd.nColRadius;
        WidePara.nRadiusY = cd.nRowRadius;
        WidePara.nStep = cd.nGridSize;
        WidePara.XResolution = cd.dDEMResolution;

        //输入长基线的工程文件smp
        QByteArray gbsrc   = cd.srcfilename.toLatin1();
        char * srcPath = gbsrc.data();
        //指定输出文件的路径
        QByteArray gbdst = cd.dstfilename.toLatin1();
        char * dstPath = gbdst.data();

//        char *srcPath="/home/celavie/testProj/SiteMapping/WideMars2/SiteMapping.smp";
//        char *dstPath="/home/celavie/testProj/SiteMapping/WideMars2/WideDemss.tif";
        CmlSiteMapProj site;
        //读入工程文件
        if( false == site.LoadProj( srcPath ))
        {
            return;
        }
        int nSiteID, nRollID, nImgID;



        vector<StereoSet> vecStereoSet;
        //读入工程文件中的像对信息
        site.GetDealSet( 1, 1, -1, vecStereoSet);
        int nNum = vecStereoSet.size();
        vector<ImgPtSet> vecFPtL(nNum), vecFPtR(nNum), vecDPtL(nNum), vecDPtR(nNum);


        //长基线制图
        char *strLogName = "/home/celavie/testProj/log.txt";
        mlSetLogFilePath(strLogName);
        site.CreateWideDem(vecStereoSet, WidePara, vecFPtL, vecFPtR, vecDPtL, vecDPtR, dstPath);



    }

}
void ImageViewer::WidebaseAnalysis()
{
    DialogWideBaseAnalysis cd;
    cd.setWindowTitle(tr("WidebaseAnalysis"));
    if(cd.exec())
    {
        //输入长基线分析的参数文件
        QByteArray gbsrc   = cd.srcfilename.toLatin1();
        char * srcPath = gbsrc.data();
        //指定输出文件的路径
        QByteArray gbdst = cd.dstfilename.toLatin1();
        char * dstPath = gbdst.data();
        //读入文件获得相关参数
        ifstream stm(srcPath);
        FILE *pIOFile;
        InOriPara mlNav;
        InOriPara mlPan;
        BaseOptions AnaPara;
        DOUBLE dBestBase;
        if((pIOFile = fopen(srcPath,"r")))
        {


            stm >> mlNav.f >> mlNav.x >> mlNav.y >> mlNav.k1 >> mlNav.k2 >> mlNav.k3 >> mlNav.p1 >> mlNav.p2 >> mlNav.alpha >> mlNav.beta ;
            stm >> mlPan.f >> mlPan.x >> mlPan.y >> mlPan.k1 >> mlPan.k2 >> mlPan.k3 >> mlNav.p1 >> mlPan.p2 >> mlPan.alpha >> mlPan.beta ;
            stm >> AnaPara.dFixBase >> AnaPara.dPixel >> AnaPara.dTarget >> AnaPara.nWidth >> AnaPara.dThresHold >> AnaPara.nIterTime;
            stm.close();
        }
        //使用相关参数进行长基线分析
        mlWideBaseAnalysis(mlNav, mlPan, AnaPara,dBestBase);
        //将结果写入文件中
        ofstream fbase(dstPath);
        fbase << dBestBase;
        fbase.close();
        cout<<"Option Base has been done!"<<endl;
    }

}
void ImageViewer::TinSimplify()
{

    DialogTinSimplify cd;
    cd.setWindowTitle(tr("TinSimplify"));
    if(cd.exec())
    {
        QByteArray gbsrc = cd.srcfilename.toLatin1();
        char* srcPath = gbsrc.data();
        QByteArray gbdst = cd.dstfilename.toLatin1();
        char * dstPath = gbdst.data();
        double simpleIndex = cd.SimplifyCoef;
        // mlTinSimply(srcPath,dstPath,simpleIndex);
        vector<Pt3d> vecin;
        vector<Pt3d> vecout;
        Pt3d pt3d;
        ifstream infile(srcPath);
        ofstream outfile(dstPath);
        while(!infile.eof())
        {
            infile >> pt3d.X >> pt3d.Y >> pt3d.Z;
            vecin.push_back(pt3d);

        }
        infile.close();
        mlTinSimply(vecin,vecout,simpleIndex);

        for(int i=0; i<vecout.size(); i++)
        {
            outfile << vecout.at(i).X << ' ' << vecout.at(i).Y << ' '<< vecout.at(i).Z << '\n';

        }
        outfile.close();

        // 以下代码用于Demo显示,不用于正式工程中
//        string strout(dstPath);
//        string strdst = strout+"smf";
//        const char* cdst = strdst.c_str();
//        char* dst = new char[100];
//        strcpy(dst,cdst);
//        CmlPhgProc phg;
//        phg.mlTinSimplyDemoFile(vecout,dst);




    }



}
void ImageViewer::DemDomProcess()
{

    DialogDEMDOMProcess cd;
    cd.setWindowTitle(tr("DemDomProcess"));

    QString srcfilename;
    QString dstfilename;

    if(cd.exec())
    {
        QByteArray gbDEMsrc = cd.srcfilename.toLatin1();
        char* srcDEMPath = gbDEMsrc.data();

        QByteArray gbDEMdst = cd.dstfilename.toLatin1();
        char* dstDEMPath = gbDEMdst.data();

        double pttl_x,pttl_y,ptbr_x,ptbr_y;
        pttl_x = cd.LTx;
        pttl_y = cd.LTy;
        ptbr_x = cd.RBx;
        ptbr_y = cd.RBY;



        int nCutBands = cd.BandNum;
        double dZoom = cd.SampleCoef;
        int nflag;

        if(cd.PixelBased == TRUE)
        {
            nflag = 1;
        }
        else
        {
            nflag = 2;
        }


        mlGeoRasterCut(srcDEMPath,dstDEMPath, pttl_x,pttl_y,ptbr_x,ptbr_y,nflag,nCutBands,dZoom);


    }

//    CmlTIN tin;
//    double x[3]  = {1,2,3};
//    double y[3] = {3,4,9};
//    DbRect rect;
//    tin.ComputeTriangleRect(x,y,&rect);
//    cout << rect.left <<  ' ' << rect.top << ' ' << rect.right<< ' ' << rect.bottom<< '\n';





}
void ImageViewer::VisualImage()
{
    DialogVisualImage cd;
    cd.setWindowTitle(tr("VisualImage"));

    if(cd.exec())
    {

        QByteArray gbpara = cd.srcParafilename.toLatin1();
        char* srcParaPath = gbpara.data();

        QByteArray gbDem = cd.srcDEMfilename.toLatin1();
        char* srcDemPath = gbDem.data();

        QByteArray gbDOM = cd.srcDOMfilename.toLatin1();
        char* srcDomPath = gbDOM.data();

        QByteArray gbNewImg = cd.dstfilename.toLatin1();
        char* NewImgPath = gbNewImg.data();

        ifstream infile(srcParaPath);
        ExOriPara exori;
        double fx,fy;

        infile >> exori.ori.omg >> exori.ori.phi >> exori.ori.kap >> exori.pos.X >> exori.pos.Y
        >> exori.pos.Z >> fx >> fy;
        mlVisualImage(srcDemPath,srcDomPath,NewImgPath,exori,fx,fy,1024,1024);

        cout << "finished!\n";




    }
////
//    CmlPhgProc phg;
//    char* sInDEM = "/home/zhangcy/cy_svn/rvml/DL/CODE/trunk/program/TestProj/VisualImage/DemReverse.tif";
//    char* sInDOM = "/home/zhangcy/cy_svn/rvml/DL/CODE/trunk/program/TestProj/VisualImage/DOMReverse.tif";
//    //char* sOut = "/home/zhangcy/cy_svn/rvml/DL/CODE/trunk/program/TestProj/VisualImage/newimg.tif";
//    ExOriPara exori;

//    exori.ori.omg = 2.734665703;
//    exori.ori.phi = 1.229205691;
//    exori.ori.kap = 1.933945632;
//    exori.pos.X = 161.351759;
//    exori.pos.Y = 169.062072;
//    exori.pos.Z = -1.35644;

//    exori.ori.omg =  -1.888407414;
//    exori.ori.phi = 0.4303669265;
//    exori.ori.kap =  0.0885459891;
//    exori.pos.X = 161.30174;
//    exori.pos.Y = 168.962353;
//    exori.pos.Z = -1.356028;

//    int nID;
//   // phg.mlImageReprj(sInDEM,sInDOM,sOut,exori,1226.23,1226.23,1024,1024);
//
//    ifstream infile("/home/zhangcy/cy_svn/rvml/DL/CODE/trunk/program/TestProj/VisualImage/ImgPairInfo.opk");
//    for(int i=0; i<10; i++)
//    {
//        infile >> nID >> exori.ori.omg >> exori.ori.phi >> exori.ori.kap >> exori.pos.X >> exori.pos.Y >> exori.pos.Z;
//
//        cout << "process img: " << nID;
//        char chID[10];
//        //itoa(nID,chID,);
//        sprintf(chID,"%d",nID);
//        string strID(chID);
//        string strout = "/home/zhangcy/cy_svn/rvml/DL/CODE/trunk/program/TestProj/VisualImage/"+ strID + "tif";// + nID + "tif";
//        const char* str_out = strout.c_str();
//        char* sOut = new char[100];
//        strcpy(sOut,str_out);
//
//
//        phg.mlImageReprj(sInDEM,sInDOM,sOut,exori,1226.23,1226.23,1024,1024);
//
//
//    }





    //phg.mlImageReprj(sInDEM,sInDOM,sOut,exori,1226.23,1226.23,1024,1024);
    //mlImageReprj(char* strDem,char* strDom,char* outImg,ExOriPara exori,double fx,double fy)
//   mlImageReprj(char* strDem,char* strDom,char* outImg,ExOriPara exori,double fx,double fy,int nImgWid,int nImgHei)

//    cout << "finished! \n";

}
void ImageViewer::DenseMatch()
{
    DialogDenseMatch cd;
    cd.setWindowTitle(tr("DenseMatch"));
    if(cd.exec())
    {
        //读入工程文件，密集匹配，写视差图
        GaussianFilterOpt GauPara;
        MatchInRegPara MatchPara;
        RANSACHomePara RanPara;
        MLRectSearch RectSearch;

        WideOptions WidePara;
//        WidePara.nStep = 1;
//        WidePara.nRadiusX =5;
//        WidePara.nRadiusY = 3;
//        WidePara.nTemplateSize = 13;
//        WidePara.dCoef = 0;

        ExtractFeatureOpt ExtractPara;
        SINT Lx, Ly, nColRange, nRowRange;

        //输入单站点工程文件smp
        QByteArray gbsrc   = cd.srcfilename.toLatin1();
        char * srcPath = gbsrc.data();

        QByteArray gbsrcpara   = cd.srcParafilename.toLatin1();
        char * paraPath = gbsrcpara.data();

        //加入全局和局部的选项
        bool bGlobal = cd.bGlobalMatch;
        SINT nIndex = cd.index;
        //读入参数文件
        ifstream stm(paraPath);
        FILE *pIOFile;

        if((pIOFile = fopen(paraPath,"r")))
        {

            stm >> GauPara.nTemplateSize >> GauPara.dCoef ;
            stm >> ExtractPara.nGridSize >> ExtractPara.nPtMaxNum;
            stm >> MatchPara.nTempSize >> MatchPara.dCoefThres;
            stm >> RanPara.dThres >> RanPara.dConfidence;
            stm >> RectSearch.dXMin >> RectSearch.dYMin >> RectSearch.dXMax >> RectSearch.dYMax;
            stm >> WidePara.nStep >> WidePara.nRadiusX >> WidePara.nRadiusY >> WidePara.nTemplateSize >> WidePara.dCoef  ;
            stm >> Lx >> Ly >> nColRange >> nRowRange;
            stm.close();
        }
        else
        {
            cout << "The photo number is wrong! \n";
            return;
        }
        CmlSiteMapProj site;
        if( false == site.LoadProj( srcPath ))
        {
            return;
        }
        int nSiteID, nRollID, nImgID;
        vector<StereoSet> vecStereoSet;

        site.GetDealSet( 1, 1, nIndex, vecStereoSet);


        int nNum = vecStereoSet.size();
        vector<ImgPtSet> vecDPtL(nNum), vecDPtR(nNum);
        vector<ImgPtSet> vecFPtL(nNum), vecFPtR(nNum);
        vector<Pt3d> vecPt3d;
        vector<DOUBLE> vecCorr;


        if(bGlobal)
        {
            site.CreateDmf(vecStereoSet, WidePara, vecFPtL, vecFPtR, vecDPtL, vecDPtR);

        }
        else
        {
            //site.CreateRegionDmf(vecStereoSet, ExtractPara, WidePara, Lx, Ly, nColRange, nRowRange, vecDPtL, vecDPtR, vecPt3d, vecCorr);
            site.CreateRegionDmf(vecStereoSet, GauPara, ExtractPara, MatchPara, RanPara, RectSearch, WidePara, Lx, Ly, nColRange, nRowRange, vecDPtL, vecDPtR, vecPt3d, vecCorr);

        }


    }

}
void ImageViewer::DEMMosaic()
{
    DialogDEMMosaic cd;
    cd.setWindowTitle(tr("DEMMosaic"));
    if(cd.exec())
    {
        QByteArray qDemSrc = cd.srcfilename.toLatin1();
        char *cProjectFile = qDemSrc.data();
        QByteArray qDemDst = cd.dstfilename.toLatin1();
        char *cOutputFile = qDemDst.data();


        vector<string> vecDem;
        string sGetLine;


        ifstream ifProjFile(cProjectFile);
        if(ifProjFile.good())
        {
            while(!ifProjFile.eof())
            {
                getline(ifProjFile, sGetLine);
                char* cPch;
                char* cLineParam[10];

                char* cTemp = const_cast<char*>(sGetLine.c_str());
                if(sGetLine[0] == 'd')
                {
                    int nCount = 1;
                    cPch = strtok (cTemp," ");
                    cLineParam[0] = cPch;
                    while(cPch != NULL)
                    {
                        cPch = strtok (NULL," ");
                        cLineParam[nCount] = cPch;
                        nCount++;
                    }
                    vecDem.push_back(cLineParam[2]);

                }

                else
                {
                    continue;
                }
            }
        }

        string strProjPath( cProjectFile );
        int nPos = strProjPath.rfind( "/"  );
        string strMainProj;
        strMainProj.assign( strProjPath, 0, nPos );
        strMainProj.append( "/");

        for( int i = 0; i < vecDem.size(); ++i )
        {
            string strCurMain = strMainProj;
            string strCurPath = vecDem[i];
            string *pCur = &vecDem.at(i);
            strCurMain.append( strCurPath );
            *pCur = strCurMain;
        }
        mlDEMMosaic(vecDem, cOutputFile, cd.dXResolution, cd.dYResolution, 2, 0);


    }
}

void ImageViewer::DisParityMap()
{
    DialogDisparityMap cd;
    cd.setWindowTitle(tr("DisParityMap"));
    if(cd.exec())
    {
        //输入左片密集点文件dmf
        QByteArray gbsrcl   = cd.Leftsrcfilename.toLatin1();
        string srcPathL = gbsrcl.data();
        //输入右边片密集点文件dmf
        QByteArray gbsrcr   = cd.Rightsrcfilename.toLatin1();
        string srcPathR = gbsrcr.data();
        //指定输出文件的路径
        QByteArray gbdst = cd.dstfilename.toLatin1();
        char* dstPath = gbdst.data();

        CPtFilesRW clsFileRW;
        ImgPtSet imgPtL, imgPtR;
        clsFileRW.LoadImgPtSet(srcPathL, imgPtL);
        clsFileRW.LoadImgPtSet(srcPathR, imgPtR);
        mlDisparityMap(imgPtL, imgPtR, dstPath);
    }
}

void ImageViewer::EpipolarImage()
{
    DialogEpipolarImage cd;
    if(cd.exec())
    {

        //输入左片密集点文件dmf
        QByteArray gbsrcl   = cd.Projsrcfilename.toLatin1();
        string srcPathL = gbsrcl.data();
        //输入右边片密集点文件dmf
        QByteArray gbsrcr   = cd.Parasrcfilename.toLatin1();
        string srcPathR = gbsrcr.data();
        //指定输出文件的路径
        QByteArray gbdst = cd.dstfilename.toLatin1();
        string dstPath = gbdst.data();

        CmlSiteMapProj clsSite;
        clsSite.EpipolarImgProj( srcPathL, srcPathR, dstPath );

    }

}

void ImageViewer::RelOrientation()
{
    DialogRelativeOrientation cd;
    if(cd.exec())
    {
        //输入左片密集点文件dmf
        QByteArray gbsrcl   = cd.Leftsrcfilename.toLatin1();
        string srcPathL = gbsrcl.data();
        //输入右边片密集点文件dmf
        QByteArray gbsrcr   = cd.Rightsrcfilename.toLatin1();
        string srcPathR = gbsrcr.data();
        //指定输出文件的路径
        QByteArray gbdst = cd.dstfilename.toLatin1();
        char* dstPath = gbdst.data();
    }
}

void ImageViewer::ContourLine()
{
    ContourDialog cd ;
    cd.setWindowTitle(tr("ContourLine"));
    if(cd.exec())
    {
        double interval = cd.interval;
        QByteArray gb   = cd.srcfilename.toLatin1();
        char * src = gb.data();
        QByteArray gb1 = cd.dstfilename.toLatin1();
        char * dst = gb1.data();

        // int d = mlComeputeContour(interval,src,dst);
        //if(d < 0)
        bool d = mlComputeContourMap(interval,src,dst);
        if(!d)
        {
            QMessageBox::about(this, tr("Error"),
                               tr("Check the Parameters!"));
            return;
        }
        return;
    }
}
void ImageViewer::Slope()
{
    SlopeDialog cd;
    cd.setWindowTitle(tr("Slope"));
    if(cd.exec())
    {
        int nWindowSize = cd.nWindowSize;
        double dZFactor = cd.zfactor;
        QByteArray gb   = cd.srcfilename.toLatin1();
        char * src = gb.data();
        QByteArray gb1 = cd.dstfilename.toLatin1();
        char * dst = gb1.data();
        //CmlDemAnalyse  cda;
        //int d = cda.ComputeSlopeInterface(src,dst,nWindowSize,dZFactor);
        //if(d < 0)
        bool d = mlComputeSlopeMap(src,dst,nWindowSize,dZFactor);
        if(!d)
        {
            //QMessageBox::information(this,tr("消息"),tr("参数输入有误，计算失败！"),NULL);
            QMessageBox::about(this, tr("Error"),
                               tr("Check the Parameters!"));
            return;
        }
        return;
    }
}

void ImageViewer::SlopeAspect()
{
    SlopeDialog cd;
    cd.setWindowTitle(tr("SlopeAspect"));
    if(cd.exec())
    {
        int nWindowSize = cd.nWindowSize;
        double dZFactor = cd.zfactor;
        QByteArray gb   = cd.srcfilename.toLatin1();
        char * src = gb.data();
        QByteArray gb1 = cd.dstfilename.toLatin1();
        char * dst = gb1.data();
        ///CmlDemAnalyse  cda;
        ///int d = cda.ComputeSlopeAspectInterface(src,dst,nWindowSize, dZFactor);
        ///if(d < 0)
        bool d = mlComputeSlopeAspectMap(src,dst,nWindowSize, dZFactor);
        if(!d)
        {
            //QMessageBox::information(this,tr("消息"),tr("参数输入有误，计算失败！"),NULL);
            QMessageBox::about(this, tr("Error"),
                               tr("Check the Parameters!"));
            return;
        }
        return;
    }
}

void ImageViewer::Barrier()
{
    ObstacleDialog cd;
    cd.setWindowTitle(tr("ObstacleMap"));
    if(cd.exec())
    {
        int nWindowSize = cd.nWindowSize;
        double dZFactor = cd.zfactor;
        QByteArray gb   = cd.srcfilename.toLatin1();
        char * src = gb.data();
        QByteArray gb1 = cd.dstfilename.toLatin1();
        char * dst = gb1.data();
        // CmlDemAnalyse  cda;
        ObstacleMapPara OMP;
        OMP.dMaxObstacleValue = cd.dMaxObstacleValue;
        OMP.dMaxRoughness = cd.dMaxRoughness;
        OMP.dMaxSlope = cd.dMaxSlope;
        OMP.dMaxStep = cd.dMaxStep;
        OMP.dRoughnessCoef = cd.dRoughnessCoef;
        OMP.dSlopeCoef = cd.dSlopeCoef;
        OMP.dStepCoef = cd.dStepCoef;

        //int d = cda.ComputeObstacleMapInterface(src,dst,nWindowSize, dZFactor,OMP);
        //if(d < 0)
        bool d = mlComputeBarrierMap(src,dst,nWindowSize, dZFactor,OMP);
        if(!d)
        {
            //QMessageBox::information(this,tr("消息"),tr("参数输入有误，计算失败！"),NULL);
            QMessageBox::about(this, tr("Error"),
                               tr("Check the Parameters!"));
            return;
        }
        return;
    }
}
void ImageViewer::Insight()
{
    ViewShedDialog cd ;
    cd.setWindowTitle(tr("Insight"));
    if(cd.exec())
    {
        double viewheight = cd.dviewhight;
        int x = cd.nx;
        int y = cd.ny;
        bool InversHeight = cd.InverseHeitht;
        QByteArray gb   = cd.srcfilename.toLatin1();
        char * src = gb.data();
        QByteArray gb1 = cd.dstfilename.toLatin1();
        char * dst = gb1.data();
//       CmlDemAnalyse  cda;
        //int d = cda.ComputeViewShedInterface(src,x,y,viewheight,dst);
        //if(d < 0)
        bool d = mlComputeInsightMap(src,x,y,viewheight,dst, InversHeight);
        if(!d)
        {
            //QMessageBox::information(this,tr("消息"),tr("参数输入有误，计算失败！"),NULL);
            QMessageBox::about(this, tr("Error"),
                               tr("Check the Parameters!"));
            return;
        }
        return;
    }
}

void ImageViewer::LandLocate()
{
    QString Projsrcfilename;
    QString DOMsrcfilename;
    QString dstfilename;

    DialogLandLocate cd;
    cd.setWindowTitle(tr("LandLocate"));
    if(cd.exec())
    {
        QByteArray qAProjPath = cd.Projsrcfilename.toLatin1();
        char* cProjPath = qAProjPath.data();

        QByteArray qASatPath = cd.DOMsrcfilename.toLatin1();
        char* cSatPath = qASatPath.data();

        QByteArray qAOutPath = cd.dstfilename.toLatin1();
        char* cOutPath = qAOutPath.data();

        CmlSiteMapProj clsSiteProj;
        if( true == clsSiteProj.LoadProj( cProjPath ) )
        {
            clsSiteProj.LocalBySeqImg( cSatPath, cOutPath );
        }
    }
}
void ImageViewer::LandLocateMatch()
{
    DialogLandLocateMatch cd;
    cd.setWindowTitle(tr("LandLocateMatch"));
    if(cd.exec())
    {
        QByteArray qALandDom = cd.LandDOMsrcfilename.toLatin1();
        char* cLandDom = qALandDom.data();

        QByteArray qASatDom = cd.SatelliteDOMsrcfilename.toLatin1();
        char* cSatDom = qASatDom.data();

        QByteArray qAOutDom = cd.dstfilename.toLatin1();
        char* cOutPath = qAOutDom.data();

        Pt3d ptLocalRes;
        DOUBLE dLocalAccuracy = 0;

        ImgPtSet satPtSet;
        ImgPtSet LandPtSet;

        LocalByMatchOpts stuLocalByMOpts;
        Pt2d ptCent;
        ptCent.X = 199.5;
        ptCent.Y = 199.5;

        mlLocalByMatch( cLandDom, cSatDom, LandPtSet, satPtSet, stuLocalByMOpts, ptCent, ptLocalRes, dLocalAccuracy );

        string strSatDom(cSatDom);
        string strLandDom(cLandDom);
        int nSatPos = strSatDom.rfind( "." );
        int nLandPos = strLandDom.rfind( "." );
        CPtFilesRW clsPtRW;
        if( ( nSatPos > 0 )&&( nLandPos > 0 ) )
        {
            string strTempSat, strTempLand;
            strTempLand.assign( strLandDom, 0,  nLandPos );
            strTempSat.assign( strSatDom, 0,  nSatPos );
            strTempLand.append( ".fmf" );
            strTempSat.append( ".fmf" );
            clsPtRW.SaveImgPtSet( strTempLand, LandPtSet );
            clsPtRW.SaveImgPtSet( strTempSat, satPtSet );
        }

        clsPtRW.SaveLocalRes( cOutPath, ptLocalRes, dLocalAccuracy );

    }
}
void ImageViewer::LandLocateInter()
{
    DialogLandLocateInter cd;
    cd.setWindowTitle(tr("LandLocateInter"));
    if(cd.exec())
    {
        QByteArray qAProjFile = cd.Projsrcfilename.toLatin1();
        char* cProjFile = qAProjFile.data();//"/home/wan/program/TestProj/LocalByIntersec/Mars1/Local.smp";

        QByteArray qAGCPFile = cd.Coordsrcfilename.toLatin1();
        char* cGCPFile = qAGCPFile.data(); //"/home/wan/program/TestProj/LocalByIntersec/test.gcp"

        QByteArray qAOutPutFile = cd.dstfilename.toLatin1();
        char* cOutPath = qAOutPutFile.data();//;"/home/wan/program/TestProj/LocalByIntersec/res.lrf"

        CmlSiteMapProj siteProj;
        if( true == siteProj.LoadProj( cProjFile ) )
        {
            siteProj.LocalByBundleResection( cGCPFile, cOutPath );
        }
    }
}
void ImageViewer::CoordTransform()
{

    Dialogcoordtrans cd;
    cd.setWindowTitle("CoordTransform");
    if(cd.exec())
    {
        //mlCalcTransMatrixByXYZ(double dLocResult_x,double dLocResult_y,double dLocResult_z,double* pTransMat, double* pTransVec)
        QByteArray gbsrc = cd.srcfilename.toLatin1();
        char* srcfile = gbsrc.data();
        QByteArray gbdst = cd.dstfilename.toLatin1();
        char* dstfile = gbdst.data();

        ifstream infile(srcfile);
        ofstream outfile(dstfile);

//
//        double transMat[9];
//        double transVec[3];
        TransMat transmat;

        if(cd.BasedOnLatLon == true)  //根据经纬度定位
        {
            double dLat;
            double dLon;
            infile >> dLat >> dLon;
            mlCalcTransMatrixByLatLong(dLat,dLon,transmat);

        }
        else  //根据三位坐标定位
        {
            double dLocResult_x,dLocResult_y,dLocResult_z;
            infile >> dLocResult_x >> dLocResult_y >> dLocResult_z;
            mlCalcTransMatrixByXYZ(dLocResult_x,dLocResult_y,dLocResult_z,transmat);


        }

        for(int i=0; i<9; i++)
        {
            outfile << transmat.dMat[i] << ' ';
            if( (i+1)%3 == 0)
                outfile << '\n';

        }
        outfile << transmat.dVec[0] << ' ' << transmat.dVec[1] << ' ' << transmat.dVec[2] <<'\n';
        outfile.close();


    }

}

void ImageViewer::RoverLocateLander()
{
    DialogRoverLocateLander cd;
    cd.setWindowTitle(tr("RoverLocateLander"));
    if(cd.exec())
    {

    }
}

//typedef struct tagThreadLIn2Site
//{
//    CmlSiteMapping *pSiteMap;
//    int nFrontSiteID;
//    int nEndSiteID;
//    string strOutPath;
//
//}ThdInfoLocalIn2Site;

void ImageViewer::RoverLocate()
{
    QString srcfilename;
    QString dstfilename;

    DialogRoverLocate cd;
    cd.setWindowTitle(tr("RoverLocate"));
    if(cd.exec())
    {
        QByteArray qAMainProj = cd.srcfilename.toLatin1();
        char* cSiteProjFile = qAMainProj.data();//"/home/wan/program/TestProj/LocalInTwoSite/Mars1/Local.smp";

        QByteArray qAOutRes = cd.dstfilename.toLatin1();
        char* cOutPath = qAOutRes.data();//"/home/wan/Local2SiteMatchRes.lrf";

        CmlSiteMapProj* pSiteProj = new CmlSiteMapProj;

        if( true == pSiteProj->LoadProj( cSiteProjFile ) )
        {


            pSiteProj->LocalInTwoSite( cd.nFrontSiteNum, cd.nBackSiteNum, cOutPath );
            delete pSiteProj;

//            pthread_t id;
//            int i, ret;
//            ret = pthread_create( &id, NULL, (void*)(&siteProj.LocalInTwoSite), NULL ) );//( cd.nFrontSiteNum, cd.nBackSiteNum, cOutPath )
//            if( ret != 0 )
//            {
//
//            }
        }
    }
}

void ImageViewer::LeftImgMouseMove(int x, int y)
{
    // 减去0.5是为了让像素中心坐标取整
    double rx,ry;
    if(scrollArea->RasterRect.left < 0)
    {
        rx = x * scrollArea->scaleFactor -0.5 ;
    }
    else
    {
        rx = (x - scrollArea->RasterRect.left) * scrollArea->scaleFactor  -0.5 ;
    }
    if(scrollArea->RasterRect.top < 0)
    {
        ry = y * scrollArea->scaleFactor  -0.5;
    }
    else
    {
        ry = (y - scrollArea->RasterRect.top) * scrollArea->scaleFactor  -0.5 ;
    }
    QString X = QString::number(rx);
    QString Y = QString::number(ry );
    X = tr("Left x:") + X + tr("            y:") + Y + tr("                 ");
    LeftImgCoordLable->setText(X);
    dlgPointCoord.SetXValue(rx);
    dlgPointCoord.SetYValue(ry);

}

void ImageViewer::RightImgMouseMove(int x, int y)
{
    double rx,ry;
    if(RightScrollArea->RasterRect.left < 0)
    {
        rx = x * RightScrollArea->scaleFactor -0.5 ;
    }
    else
    {
        rx = (x - RightScrollArea->RasterRect.left) * RightScrollArea->scaleFactor  -0.5 ;
    }
    if(RightScrollArea->RasterRect.top < 0)
    {
        ry = y * RightScrollArea->scaleFactor  -0.5;
    }
    else
    {
        ry = (y - RightScrollArea->RasterRect.top) * RightScrollArea->scaleFactor  -0.5 ;
    }
    QString X = QString::number(rx);
    QString Y = QString::number(ry);
    X = tr("Right x:") + X + tr("            y:") + Y + tr("                 ");
    RightImgCoordLable->setText(X);
    dlgPointCoord.SetXValue(rx);
    dlgPointCoord.SetYValue(ry);
}
void ImageViewer::LeftScrollAreaMousePress(QMouseEvent* mouseEvent)
{
    // scrollarea与rightscrollarea的CurrentToolType是同步的
    int x = mouseEvent->pos().x();
    int y = mouseEvent->pos().y();
    switch (scrollArea->CurrentToolType)
    {
    case Tool_EditMatchPoint:
    {
        // 选中点的判断放置到mouserelease事件
        /*
        if(mouseEvent->button()!= Qt::LeftButton)
        {
            break;
        }
        bool Found = false;
        // 自动点中查找
        for(int i = 0; i<LeftFeatPtDataset.m_ImgPtSet.vecPts.size(); i++)
        {
            double cx,cy;
            scrollArea->RasterLocationToCanvasLocation(LeftFeatPtDataset.m_ImgPtSet.vecPts.at(i).X,
                                           LeftFeatPtDataset.m_ImgPtSet.vecPts.at(i).Y,
                                           cx,cy);
            //cx，cy得到的坐标为在imagelable中的坐标值，要转换在scrollarea中的坐标值
            //选中点在scrollarea中的坐标
            double dXInscrollarea,dyInscrollarea;
            if(scrollArea->RasterRect.left<0)
            {
                dXInscrollarea = scrollArea->RasterRect.left + cx;
            }
            else
            {
                 dXInscrollarea = cx;
            }
            if(scrollArea->RasterRect.top < 0)
            {
                dyInscrollarea = scrollArea->RasterRect.top + cy;
            }
            else
            {
                 dyInscrollarea = cy;
            }

            if(fabs(scrollArea->DownPoint.x - dXInscrollarea) < 5
                                && fabs(scrollArea->DownPoint.y - dyInscrollarea) < 5)
            {
                scrollArea->FeatAutoSelectedIdx.clear();
                scrollArea->FeatManualSelectedIdx.clear();
                scrollArea->FeatAutoSelectedIdx.append(i);
                Found = true;
                break;
            }
        }
        //手动点中查找
        for(int i = 0; i<LeftFeatPtDataset.m_ImgPtSet.vecAddPts.size(); i++)
        {
            if(Found)
            {
                break;
            }
            double cx,cy;
            scrollArea->RasterLocationToCanvasLocation(LeftFeatPtDataset.m_ImgPtSet.vecAddPts.at(i).X,
                                           LeftFeatPtDataset.m_ImgPtSet.vecAddPts.at(i).Y,
                                           cx,cy);
            //cx，cy得到的坐标为在imagelable中的坐标值，要转换在scrollarea中的坐标值
            //选中点在scrollarea中的坐标
            double dXInscrollarea,dyInscrollarea;
            if(scrollArea->RasterRect.left<0)
            {
                dXInscrollarea = scrollArea->RasterRect.left + cx;
            }
            else
            {
                 dXInscrollarea = cx;
            }
            if(scrollArea->RasterRect.top < 0)
            {
                dyInscrollarea = scrollArea->RasterRect.top + cy;
            }
            else
            {
                 dyInscrollarea = cy;
            }

            if(fabs(scrollArea->DownPoint.x - dXInscrollarea) < 5
                                && fabs(scrollArea->DownPoint.y - dyInscrollarea) < 5)
            {
                scrollArea->FeatManualSelectedIdx.clear();
                scrollArea->FeatAutoSelectedIdx.clear();
                scrollArea->FeatManualSelectedIdx.append(i);
                Found = true;
                UpdateFeatPtTableView();
                break;
            }
        }
        SelectRightPtsByLeftSelection();
        UpdateFeatPtTableView();
        //LeftFeatPtTableView->setFocus();
         */
        break;

    }

    case Tool_AddFeatPoint:
    {
        if(mouseEvent->button()!= Qt::LeftButton)
        {
            break;
        }
        double rx,ry;
        scrollArea->CanvasLocationToRasterLocation(x,y,rx,ry);
        ULONG lID;
        if(false == mlGetNewSinglePtID(scrollArea->FeatPtDataSet->m_ImgPtSet,lID))
        {
            QMessageBox msgbox;
            msgbox.setText(tr("Get Point ID Error!"));
            msgbox.exec();
            break;
        }
        scrollArea->FeatPtDataSet->AddPt(lID,rx,ry);
        QList<QStandardItem*>   pColList ;
        LeftFeatPtTableModel->appendRow(pColList);
        QModelIndex index;
        index = LeftFeatPtTableModel->index(LeftFeatPtTableModel->rowCount() - 1,0);
        LeftFeatPtTableModel->setData(index,lID);
        index = LeftFeatPtTableModel->index(LeftFeatPtTableModel->rowCount() - 1,1);
        LeftFeatPtTableModel->setData(index,rx);
        index = LeftFeatPtTableModel->index(LeftFeatPtTableModel->rowCount() - 1,2);
        LeftFeatPtTableModel->setData(index,ry);
        // scrollArea->imageLabel->update();

        scrollArea->FeatManualSelectedIdx.clear();
        scrollArea->FeatAutoSelectedIdx.clear();
        scrollArea->FeatManualSelectedIdx.append(scrollArea->FeatPtDataSet->m_ImgPtSet.vecAddPts.size() -1 );

        SelectRightPtsByLeftSelection();
        UpdateFeatPtTableView();

//        scrollArea->imageLabel->update();
//        RightScrollArea->FeatAutoSelectedIdx.clear();
//        RightScrollArea->FeatManualSelectedIdx.clear();
//        RightScrollArea->imageLabel->update();
//        LeftFeatPtTableView->setCurrentIndex(index);
        break;
    }
    case Tool_AddStereFeatPoint:
    {
        if(mouseEvent->button()!= Qt::LeftButton)
        {
            break;
        }
        if(AddFeatPointStep == 0)
        {
            double rx,ry;
            AddLeftOrRight = 1;
            scrollArea->CanvasLocationToRasterLocation(x,y,rx,ry);
            ULONG lID;
            if(false == mlGetNewStereoPtID(scrollArea->FeatPtDataSet->m_ImgPtSet,
                                           RightScrollArea->FeatPtDataSet->m_ImgPtSet,lID))
            {
                QMessageBox msgbox;
                msgbox.setText(tr("Get Point ID Error!"));
                msgbox.exec();
                break;
            }
            NewID = lID;
            scrollArea->FeatPtDataSet->AddPt(lID,rx,ry);
            QList<QStandardItem*>   pColList ;
            LeftFeatPtTableModel->appendRow(pColList);
            QModelIndex index;
            index = LeftFeatPtTableModel->index(LeftFeatPtTableModel->rowCount() - 1,0);
            LeftFeatPtTableModel->setData(index,lID);
            index = LeftFeatPtTableModel->index(LeftFeatPtTableModel->rowCount() - 1,1);
            LeftFeatPtTableModel->setData(index,rx);
            index = LeftFeatPtTableModel->index(LeftFeatPtTableModel->rowCount() - 1,2);
            LeftFeatPtTableModel->setData(index,ry);
            scrollArea->imageLabel->update();

            scrollArea->FeatManualSelectedIdx.clear();
            scrollArea->FeatAutoSelectedIdx.clear();
            scrollArea->FeatManualSelectedIdx.append(scrollArea->FeatPtDataSet->m_ImgPtSet.vecAddPts.size() -1 );
//            scrollArea->imageLabel->update();
//            LeftFeatPtTableView->setCurrentIndex(index);
            SelectRightPtsByLeftSelection();
            UpdateFeatPtTableView();
            dlgPointCoord.SetText(tr("Choose a point in right image"));
            dlgPointCoord.SetID(lID);
            AddFeatPointStep = 1;
            AddLeftOrRight = 1;
            dlgPointCoord.show();
        }
        else if(AddFeatPointStep == 1)
        {
            if(AddLeftOrRight == 1)
            {
                QMessageBox msgbox;
                msgbox.setText(tr("Click Wrong Image!"));
                msgbox.exec();
                break;
            }
            double rx,ry;
            ULONG lID = NewID;
            scrollArea->CanvasLocationToRasterLocation(x,y,rx,ry);
            scrollArea->FeatPtDataSet->AddPt(lID,rx,ry);
            QList<QStandardItem*>   pColList ;
            LeftFeatPtTableModel->appendRow(pColList);
            QModelIndex index;
            index = LeftFeatPtTableModel->index(LeftFeatPtTableModel->rowCount() - 1,0);
            LeftFeatPtTableModel->setData(index,lID);
            index = LeftFeatPtTableModel->index(LeftFeatPtTableModel->rowCount() - 1,1);
            LeftFeatPtTableModel->setData(index,rx);
            index = LeftFeatPtTableModel->index(LeftFeatPtTableModel->rowCount() - 1,2);
            LeftFeatPtTableModel->setData(index,ry);
            scrollArea->imageLabel->update();

            scrollArea->FeatManualSelectedIdx.clear();
            scrollArea->FeatAutoSelectedIdx.clear();
            scrollArea->FeatManualSelectedIdx.append(scrollArea->FeatPtDataSet->m_ImgPtSet.vecAddPts.size() -1 );
            LeftFeatPtTableView->setCurrentIndex(index);
            SelectRightPtsByLeftSelection();

            //手动添加的点为匹配上的点
            scrollArea->FeatPtDataSet->m_ImgPtSet.vecAddPts.at(
                scrollArea->FeatPtDataSet->m_ImgPtSet.vecAddPts.size() - 1).byIsMatch = 1;
            RightScrollArea->FeatPtDataSet->m_ImgPtSet.vecAddPts.at(
                RightScrollArea->FeatPtDataSet->m_ImgPtSet.vecAddPts.size() - 1).byIsMatch = 1;

            UpdateFeatPtTableView();
            dlgPointCoord.SetText(tr("Choose a point in one image"));
            dlgPointCoord.SetID(0);
            // scrollArea->imageLabel->update();
            // 还原添加步骤变量
            AddFeatPointStep = 0;
            AddLeftOrRight = 0;
            dlgPointCoord.show();
        }

        break;
    }

    case Tool_ToolActSemiAutoAddFeatPoint:
    {
        if(mouseEvent->button()!= Qt::LeftButton)
        {
            break;
        }

        double rx,ry;
        AddLeftOrRight = 1;
        scrollArea->CanvasLocationToRasterLocation(x,y,rx,ry);

        double rightrx,rightry;

        Pt2i ptCurClick, ptRes;
        ptCurClick.X = int(rx+0.001);
        ptCurClick.Y = int(ry+0.001);
        MatchInRegPara matchPara;
        double dCoefRes;
        if( false == mlSemiAutoMatchInRegion( strLeftOpenedImg.c_str(), strRightOpenedImg.c_str(), ptCurClick, matchPara, ptRes, dCoefRes ))
        {
            return;
        }
        rx = ptCurClick.X;
        ry = ptCurClick.Y;
        rightrx = ptRes.X;
        rightry = ptRes.Y;

        Pt2d ptTemp, ptTempRes;
        ptTemp.X = rx;
        ptTemp.Y = ry;
        ptTempRes.X = rightrx;
        ptTempRes.Y = rightry;
        if( true == mlLsMatch(  strLeftOpenedImg.c_str(), strRightOpenedImg.c_str(), ptTemp, 7, ptTempRes, dCoefRes ) )
        {
            rightrx = ptTempRes.X;
            rightry = ptTempRes.Y;
        }

        ULONG lID;
        if(false == mlGetNewStereoPtID(scrollArea->FeatPtDataSet->m_ImgPtSet,
                                       RightScrollArea->FeatPtDataSet->m_ImgPtSet,lID))
        {
            QMessageBox msgbox;
            msgbox.setText(tr("Get Point ID Error!"));
            msgbox.exec();
            break;
        }
        NewID = lID;
        scrollArea->FeatPtDataSet->AddPt(lID,rx,ry);
        QList<QStandardItem*>   pColList ;
        LeftFeatPtTableModel->appendRow(pColList);
        QModelIndex index;
        index = LeftFeatPtTableModel->index(LeftFeatPtTableModel->rowCount() - 1,0);
        LeftFeatPtTableModel->setData(index,lID);
        index = LeftFeatPtTableModel->index(LeftFeatPtTableModel->rowCount() - 1,1);
        LeftFeatPtTableModel->setData(index,rx);
        index = LeftFeatPtTableModel->index(LeftFeatPtTableModel->rowCount() - 1,2);
        LeftFeatPtTableModel->setData(index,ry);
        scrollArea->imageLabel->update();

        scrollArea->FeatManualSelectedIdx.clear();
        scrollArea->FeatAutoSelectedIdx.clear();
        scrollArea->FeatManualSelectedIdx.append(scrollArea->FeatPtDataSet->m_ImgPtSet.vecAddPts.size() -1 );

        SelectRightPtsByLeftSelection();
        UpdateFeatPtTableView();



        //自动获得匹配点
        rx = rightrx;
        ry = rightry;
        RightScrollArea->FeatPtDataSet->AddPt(lID,rx,ry);

        //QList<QStandardItem*>   pColList ;
        RightFeatPtTableModel->appendRow(pColList);
        //QModelIndex index;
        index = RightFeatPtTableModel->index(RightFeatPtTableModel->rowCount() - 1,0);
        RightFeatPtTableModel->setData(index,lID);
        index = RightFeatPtTableModel->index(RightFeatPtTableModel->rowCount() - 1,1);
        RightFeatPtTableModel->setData(index,rx);
        index = RightFeatPtTableModel->index(RightFeatPtTableModel->rowCount() - 1,2);
        RightFeatPtTableModel->setData(index,ry);
        RightScrollArea->imageLabel->update();

        RightScrollArea->FeatManualSelectedIdx.clear();
        RightScrollArea->FeatAutoSelectedIdx.clear();
        RightScrollArea->FeatManualSelectedIdx.append(RightScrollArea->FeatPtDataSet->m_ImgPtSet.vecAddPts.size() -1 );
        RightFeatPtTableView->setCurrentIndex(index);
        SelectRightPtsByLeftSelection();

        //手动添加的点为匹配上的点
        scrollArea->FeatPtDataSet->m_ImgPtSet.vecAddPts.at(
            scrollArea->FeatPtDataSet->m_ImgPtSet.vecAddPts.size() - 1).byIsMatch = 1;
        int ddd = RightScrollArea->FeatPtDataSet->m_ImgPtSet.vecAddPts.size();
        RightScrollArea->FeatPtDataSet->m_ImgPtSet.vecAddPts.at(
            RightScrollArea->FeatPtDataSet->m_ImgPtSet.vecAddPts.size() - 1).byIsMatch = 1;

        UpdateFeatPtTableView();

        break;
    }
    case Tool_ToolActShowRegionPlanVect:
    {
        if(mouseEvent->button()!= Qt::LeftButton)
        {
            break;
        }
        //图像坐标
        double rx,ry;
        vector<ImgPtSet> vecDPtL(1), vecDPtR(1);
        vector<ImgPtSet> vecFPtL(1), vecFPtR(1);
        vector<Pt3d> vecPt3d;
        vector<DOUBLE> vecCorr;
        scrollArea->CanvasLocationToRasterLocation(x,y,rx,ry);

        int Lx = rx;
        int Ly = ry;
        m_site.CreateRegionDmf(m_vecStereoSet, m_GauPara, m_ExtractPara, m_MatchPara,
                             m_RanPara, m_RectSearch, m_WidePara, Lx, Ly, m_nColRange, m_nRowRange, vecDPtL, vecDPtR, vecPt3d, vecCorr);


        break;
    }

    default:
    {
        break;
    }
    }
}
void ImageViewer::RightScrollAreaMousePress(QMouseEvent* mouseEvent)
{
    // scrollarea与rightscrollarea的CurrentToolType是同步的
    int x = mouseEvent->pos().x();
    int y = mouseEvent->pos().y();
    switch (RightScrollArea->CurrentToolType)
    {
    case Tool_EditMatchPoint:
    {
        // 选中点的判断放置到mouserelease事件中
//        if(mouseEvent->button()!= Qt::LeftButton)
//        {
//            break;
//        }
//        bool Found = false;
//        // 自动点中查找
//        for(int i = 0; i<RightFeatPtDataset.m_ImgPtSet.vecPts.size(); i++)
//        {
//            double cx,cy;
//            RightScrollArea->RasterLocationToCanvasLocation(RightFeatPtDataset.m_ImgPtSet.vecPts.at(i).X,
//                                           RightFeatPtDataset.m_ImgPtSet.vecPts.at(i).Y,
//                                           cx,cy);
//            //cx，cy得到的坐标为在imagelable中的坐标值，要转换在scrollarea中的坐标值
//            //选中点在scrollarea中的坐标
//            double dXInscrollarea,dyInscrollarea;
//            if(RightScrollArea->RasterRect.left<0)
//            {
//                dXInscrollarea = RightScrollArea->RasterRect.left + cx;
//            }
//            else
//            {
//                 dXInscrollarea = cx;
//            }
//            if(RightScrollArea->RasterRect.top < 0)
//            {
//                dyInscrollarea = RightScrollArea->RasterRect.top + cy;
//            }
//            else
//            {
//                 dyInscrollarea = cy;
//            }

//            if(fabs(RightScrollArea->DownPoint.x - dXInscrollarea) < 5
//                                && fabs(RightScrollArea->DownPoint.y - dyInscrollarea) < 5)
//            {
//                RightScrollArea->FeatAutoSelectedIdx.clear();
//                RightScrollArea->FeatManualSelectedIdx.clear();
//                RightScrollArea->FeatAutoSelectedIdx.append(i);
//                Found = true;
//                break;
//            }
//        }
//        //手动点中查找
//        for(int i = 0; i<RightFeatPtDataset.m_ImgPtSet.vecAddPts.size(); i++)
//        {
//            if(Found)
//            {
//                break;
//            }
//            double cx,cy;
//            RightScrollArea->RasterLocationToCanvasLocation(RightFeatPtDataset.m_ImgPtSet.vecAddPts.at(i).X,
//                                           RightFeatPtDataset.m_ImgPtSet.vecAddPts.at(i).Y,
//                                           cx,cy);
//            //cx，cy得到的坐标为在imagelable中的坐标值，要转换在scrollarea中的坐标值
//            //选中点在scrollarea中的坐标
//            double dXInscrollarea,dyInscrollarea;
//            if(RightScrollArea->RasterRect.left<0)
//            {
//                dXInscrollarea = RightScrollArea->RasterRect.left + cx;
//            }
//            else
//            {
//                 dXInscrollarea = cx;
//            }
//            if(RightScrollArea->RasterRect.top < 0)
//            {
//                dyInscrollarea = RightScrollArea->RasterRect.top + cy;
//            }
//            else
//            {
//                 dyInscrollarea = cy;
//            }

//            if(fabs(RightScrollArea->DownPoint.x - dXInscrollarea) < 5
//                                && fabs(RightScrollArea->DownPoint.y - dyInscrollarea) < 5)
//            {
//                RightScrollArea->FeatManualSelectedIdx.clear();
//                RightScrollArea->FeatAutoSelectedIdx.clear();
//                RightScrollArea->FeatManualSelectedIdx.append(i);
//                Found = true;
//                //UpdateFeatPtTableView();
//                break;
//            }
//        }

//        SelectLeftPtsByRightSelection();
//        UpdateFeatPtTableView();
//        //RightFeatPtTableView->setFocus();
        break;
    }
    case Tool_AddFeatPoint:
    {
        if(mouseEvent->button()!= Qt::LeftButton)
        {
            break;
        }
        double rx,ry;
        RightScrollArea->CanvasLocationToRasterLocation(x,y,rx,ry);
        ULONG lID;
        if(false == mlGetNewSinglePtID(RightScrollArea->FeatPtDataSet->m_ImgPtSet,lID))
        {
            QMessageBox msgbox;
            msgbox.setText(tr("Get Point ID Error!"));
            msgbox.exec();
            break;
        }
        RightScrollArea->FeatPtDataSet->AddPt(lID,rx,ry);
        QList<QStandardItem*>   pColList ;
        RightFeatPtTableModel->appendRow(pColList);
        QModelIndex index;
        index = RightFeatPtTableModel->index(RightFeatPtTableModel->rowCount() - 1,0);
        RightFeatPtTableModel->setData(index,lID);
        index = RightFeatPtTableModel->index(RightFeatPtTableModel->rowCount() - 1,1);
        RightFeatPtTableModel->setData(index,rx);
        index = RightFeatPtTableModel->index(RightFeatPtTableModel->rowCount() - 1,2);
        RightFeatPtTableModel->setData(index,ry);
        RightScrollArea->imageLabel->update();

        RightScrollArea->FeatManualSelectedIdx.clear();
        RightScrollArea->FeatAutoSelectedIdx.clear();
        RightScrollArea->FeatManualSelectedIdx.append(RightScrollArea->FeatPtDataSet->m_ImgPtSet.vecAddPts.size() -1 );
        RightScrollArea->imageLabel->update();
        scrollArea->FeatAutoSelectedIdx.clear();
        scrollArea->FeatManualSelectedIdx.clear();
        scrollArea->imageLabel->update();
        RightFeatPtTableView->setCurrentIndex(index);
        RightFeatPtTableView->setFocus();
        break;
    }
    case Tool_AddStereFeatPoint:
    {
        if(mouseEvent->button()!= Qt::LeftButton)
        {
            break;
        }
        if(AddFeatPointStep == 0)
        {
            double rx,ry;
            AddLeftOrRight = 2;
            RightScrollArea->CanvasLocationToRasterLocation(x,y,rx,ry);
            ULONG lID;
            if(false == mlGetNewStereoPtID(scrollArea->FeatPtDataSet->m_ImgPtSet,
                                           RightScrollArea->FeatPtDataSet->m_ImgPtSet,lID))
            {
                QMessageBox msgbox;
                msgbox.setText(tr("Get Point ID Error!"));
                msgbox.exec();
                break;
            }
            NewID = lID;
            RightScrollArea->FeatPtDataSet->AddPt(lID,rx,ry);
            QList<QStandardItem*>   pColList ;
            RightFeatPtTableModel->appendRow(pColList);
            QModelIndex index;
            index = RightFeatPtTableModel->index(RightFeatPtTableModel->rowCount() - 1,0);
            RightFeatPtTableModel->setData(index,lID);
            index = RightFeatPtTableModel->index(RightFeatPtTableModel->rowCount() - 1,1);
            RightFeatPtTableModel->setData(index,rx);
            index = RightFeatPtTableModel->index(RightFeatPtTableModel->rowCount() - 1,2);
            RightFeatPtTableModel->setData(index,ry);
            //RightScrollArea->imageLabel->update();

            RightScrollArea->FeatManualSelectedIdx.clear();
            RightScrollArea->FeatAutoSelectedIdx.clear();
            RightScrollArea->FeatManualSelectedIdx.append(RightScrollArea->FeatPtDataSet->m_ImgPtSet.vecAddPts.size() -1 );
            // RightScrollArea->imageLabel->update();
            RightFeatPtTableView->setCurrentIndex(index);
            SelectLeftPtsByRightSelection();
            UpdateFeatPtTableView();
            dlgPointCoord.SetText(tr("Choose a point in left image"));
            dlgPointCoord.SetID(lID);
            AddFeatPointStep = 1;
            AddLeftOrRight = 2;
            dlgPointCoord.show();
        }
        else if(AddFeatPointStep == 1)
        {
            if(AddLeftOrRight == 2)
            {
                QMessageBox msgbox;
                msgbox.setText(tr("Click Wrong Image!"));
                msgbox.exec();
                break;
            }
            double rx,ry;
            ULONG lID = NewID;
            RightScrollArea->CanvasLocationToRasterLocation(x,y,rx,ry);
            RightScrollArea->FeatPtDataSet->AddPt(lID,rx,ry);
            QList<QStandardItem*>   pColList ;
            RightFeatPtTableModel->appendRow(pColList);
            QModelIndex index;
            index = RightFeatPtTableModel->index(RightFeatPtTableModel->rowCount() - 1,0);
            RightFeatPtTableModel->setData(index,lID);
            index = RightFeatPtTableModel->index(RightFeatPtTableModel->rowCount() - 1,1);
            RightFeatPtTableModel->setData(index,rx);
            index = RightFeatPtTableModel->index(RightFeatPtTableModel->rowCount() - 1,2);
            RightFeatPtTableModel->setData(index,ry);
            RightScrollArea->imageLabel->update();

            RightScrollArea->FeatManualSelectedIdx.clear();
            RightScrollArea->FeatAutoSelectedIdx.clear();
            RightScrollArea->FeatManualSelectedIdx.append(RightScrollArea->FeatPtDataSet->m_ImgPtSet.vecAddPts.size() -1 );
            //RightScrollArea->imageLabel->update();
            RightFeatPtTableView->setCurrentIndex(index);
            dlgPointCoord.SetText(tr("Choose a point in one image"));
            dlgPointCoord.SetID(0);
            SelectLeftPtsByRightSelection();

            //手动添加的点为匹配上的点
            scrollArea->FeatPtDataSet->m_ImgPtSet.vecAddPts.at(
                scrollArea->FeatPtDataSet->m_ImgPtSet.vecAddPts.size() - 1).byIsMatch = 1;
            RightScrollArea->FeatPtDataSet->m_ImgPtSet.vecAddPts.at(
                RightScrollArea->FeatPtDataSet->m_ImgPtSet.vecAddPts.size() - 1).byIsMatch = 1;

            UpdateFeatPtTableView();
            // 还原添加步骤变量
            AddFeatPointStep = 0;
            AddLeftOrRight = 0;
            dlgPointCoord.show();
        }

        break;
    }
    case Tool_ToolActSemiAutoAddFeatPoint:
    {
        if(mouseEvent->button()!= Qt::LeftButton)
        {
            break;
        }

        double rx,ry;
        AddLeftOrRight = 1;
        RightScrollArea->CanvasLocationToRasterLocation(x,y,rx,ry);

        double leftrx,leftry;

        Pt2i ptCurClick, ptRes;
        ptCurClick.X = int(rx+0.001);
        ptCurClick.Y = int(ry+0.001);
        MatchInRegPara matchPara;

        int nMax = matchPara.dXMax;
        matchPara.dXMax = -1.0*matchPara.dXMin;
        matchPara.dXMin = -1.0*nMax;
        double dCoefRes;
        if( false == mlSemiAutoMatchInRegion( strRightOpenedImg.c_str(), strLeftOpenedImg.c_str(), ptCurClick, matchPara, ptRes, dCoefRes ))
        {
            return;
        }
        rx = ptCurClick.X;
        ry = ptCurClick.Y;
        leftrx = ptRes.X;
        leftry = ptRes.Y;

        Pt2d ptTemp, ptTempRes;
        ptTemp.X = rx;
        ptTemp.Y = ry;
        ptTempRes.X = leftrx;
        ptTempRes.Y = leftry;
        if( true == mlLsMatch(  strRightOpenedImg.c_str(), strLeftOpenedImg.c_str(), ptTemp, 7, ptTempRes, dCoefRes ) )
        {
            leftrx = ptTempRes.X;
            leftry = ptTempRes.Y;
        }


        ULONG lID;
        if(false == mlGetNewStereoPtID(scrollArea->FeatPtDataSet->m_ImgPtSet,
                                       RightScrollArea->FeatPtDataSet->m_ImgPtSet,lID))
        {
            QMessageBox msgbox;
            msgbox.setText(tr("Get Point ID Error!"));
            msgbox.exec();
            break;
        }
        NewID = lID;
        RightScrollArea->FeatPtDataSet->AddPt(lID,rx,ry);
        QList<QStandardItem*>   pColList ;
        RightFeatPtTableModel->appendRow(pColList);
        QModelIndex index;
        index = RightFeatPtTableModel->index(RightFeatPtTableModel->rowCount() - 1,0);
        RightFeatPtTableModel->setData(index,lID);
        index = RightFeatPtTableModel->index(RightFeatPtTableModel->rowCount() - 1,1);
        RightFeatPtTableModel->setData(index,rx);
        index = RightFeatPtTableModel->index(RightFeatPtTableModel->rowCount() - 1,2);
        RightFeatPtTableModel->setData(index,ry);
        RightScrollArea->imageLabel->update();

        RightScrollArea->FeatManualSelectedIdx.clear();
        RightScrollArea->FeatAutoSelectedIdx.clear();
        RightScrollArea->FeatManualSelectedIdx.append(RightScrollArea->FeatPtDataSet->m_ImgPtSet.vecAddPts.size() -1 );

        SelectLeftPtsByRightSelection();
        UpdateFeatPtTableView();



        //自动获得匹配点
        rx = leftrx;
        ry = leftry;
        scrollArea->FeatPtDataSet->AddPt(lID,rx,ry);

        //QList<QStandardItem*>   pColList ;
        LeftFeatPtTableModel->appendRow(pColList);
        //QModelIndex index;
        index = LeftFeatPtTableModel->index(LeftFeatPtTableModel->rowCount() - 1,0);
        LeftFeatPtTableModel->setData(index,lID);
        index = LeftFeatPtTableModel->index(LeftFeatPtTableModel->rowCount() - 1,1);
        LeftFeatPtTableModel->setData(index,rx);
        index = LeftFeatPtTableModel->index(LeftFeatPtTableModel->rowCount() - 1,2);
        LeftFeatPtTableModel->setData(index,ry);
        scrollArea->imageLabel->update();

        scrollArea->FeatManualSelectedIdx.clear();
        scrollArea->FeatAutoSelectedIdx.clear();
        scrollArea->FeatManualSelectedIdx.append(scrollArea->FeatPtDataSet->m_ImgPtSet.vecAddPts.size() -1 );
        LeftFeatPtTableView->setCurrentIndex(index);
        SelectRightPtsByLeftSelection();

        //手动添加的点为匹配上的点
        scrollArea->FeatPtDataSet->m_ImgPtSet.vecAddPts.at(
            scrollArea->FeatPtDataSet->m_ImgPtSet.vecAddPts.size() - 1).byIsMatch = 1;
        int ddd = RightScrollArea->FeatPtDataSet->m_ImgPtSet.vecAddPts.size();
        RightScrollArea->FeatPtDataSet->m_ImgPtSet.vecAddPts.at(
            RightScrollArea->FeatPtDataSet->m_ImgPtSet.vecAddPts.size() - 1).byIsMatch = 1;

        UpdateFeatPtTableView();

        break;
    }
    case Tool_ToolActShowRegionPlanVect:
    {
        if(mouseEvent->button()!= Qt::LeftButton)
        {
            break;
        }
        //图像坐标
        double rx,ry;
        vector<ImgPtSet> vecDPtL(1), vecDPtR(1);
        vector<ImgPtSet> vecFPtL(1), vecFPtR(1);
        vector<Pt3d> vecPt3d;
        vector<DOUBLE> vecCorr;
        RightScrollArea->CanvasLocationToRasterLocation(x,y,rx,ry);

        int Lx = rx;
        int Ly = ry;
        m_site.CreateRegionDmf(m_vecStereoSet, m_GauPara, m_ExtractPara, m_MatchPara,
                             m_RanPara, m_RectSearch, m_WidePara, Lx, Ly, m_nColRange, m_nRowRange, vecDPtL, vecDPtR, vecPt3d, vecCorr);


    }
    default:
    {
        break;
    }
    }


}
void ImageViewer::LeftScrollAreaMouseRealease(QMouseEvent* mouseEvent)
{
    double dx = scrollArea->DownPoint.x;
    double dy = scrollArea->DownPoint.y;
    double ux = mouseEvent->x();
    double uy = mouseEvent->y();
    switch (scrollArea->CurrentToolType)
    {
    case Tool_EditMatchPoint:
    {
        if(mouseEvent->button()!= Qt::LeftButton)
        {
            break;
        }
        // 鼠标点击选择时
        if(fabs(dx - ux)<3 && fabs(dy - uy) <3)
        {
            bool Found = false;
            // 自动点中查找
            for(int i = 0; i<LeftFeatPtDataset.m_ImgPtSet.vecPts.size(); i++)
            {
                double cx,cy;
                scrollArea->RasterLocationToCanvasLocation(LeftFeatPtDataset.m_ImgPtSet.vecPts.at(i).X,
                        LeftFeatPtDataset.m_ImgPtSet.vecPts.at(i).Y,
                        cx,cy);
                //cx，cy得到的坐标为在imagelable中的坐标值，要转换在scrollarea中的坐标值
                //选中点在scrollarea中的坐标
                double dXInscrollarea,dyInscrollarea;
                if(scrollArea->RasterRect.left<0)
                {
                    dXInscrollarea = scrollArea->RasterRect.left + cx;
                }
                else
                {
                    dXInscrollarea = cx;
                }
                if(scrollArea->RasterRect.top < 0)
                {
                    dyInscrollarea = scrollArea->RasterRect.top + cy;
                }
                else
                {
                    dyInscrollarea = cy;
                }

                if(fabs(scrollArea->DownPoint.x - dXInscrollarea) < 5
                        && fabs(scrollArea->DownPoint.y - dyInscrollarea) < 5)
                {
                    scrollArea->FeatAutoSelectedIdx.clear();
                    scrollArea->FeatManualSelectedIdx.clear();
                    scrollArea->FeatAutoSelectedIdx.append(i);
                    Found = true;
                    break;
                }
            }
            //手动点中查找
            for(int i = 0; i<LeftFeatPtDataset.m_ImgPtSet.vecAddPts.size(); i++)
            {
                if(Found)
                {
                    break;
                }
                double cx,cy;
                scrollArea->RasterLocationToCanvasLocation(LeftFeatPtDataset.m_ImgPtSet.vecAddPts.at(i).X,
                        LeftFeatPtDataset.m_ImgPtSet.vecAddPts.at(i).Y,
                        cx,cy);
                //cx，cy得到的坐标为在imagelable中的坐标值，要转换在scrollarea中的坐标值
                //选中点在scrollarea中的坐标
                double dXInscrollarea,dyInscrollarea;
                if(scrollArea->RasterRect.left<0)
                {
                    dXInscrollarea = scrollArea->RasterRect.left + cx;
                }
                else
                {
                    dXInscrollarea = cx;
                }
                if(scrollArea->RasterRect.top < 0)
                {
                    dyInscrollarea = scrollArea->RasterRect.top + cy;
                }
                else
                {
                    dyInscrollarea = cy;
                }

                if(fabs(scrollArea->DownPoint.x - dXInscrollarea) < 5
                        && fabs(scrollArea->DownPoint.y - dyInscrollarea) < 5)
                {
                    scrollArea->FeatManualSelectedIdx.clear();
                    scrollArea->FeatAutoSelectedIdx.clear();
                    scrollArea->FeatManualSelectedIdx.append(i);
                    Found = true;
                    UpdateFeatPtTableView();
                    break;
                }
            }
            SelectRightPtsByLeftSelection();
            UpdateFeatPtTableView();
            // LeftFeatPtTableView->setFocus();
            break;
        }
        // 鼠标拖拽矩形选择时
        else
            // 若以选中点则应该是拖拽点，而不是拖矩形选点
            if(!scrollArea->PickedAPoint)
            {
                scrollArea->FeatAutoSelectedIdx.clear();
                scrollArea->FeatManualSelectedIdx.clear();
                // 计算矩形左上右下点坐标
                double ltx = min(dx,ux);
                double lty = min(dy,uy);
                double rbx = max(dx,ux);
                double rby = max(dy,uy);
                double ltrx,ltry;
                double rbrx,rbry;
                //转换矩形坐标到图片的坐标
                scrollArea->CanvasLocationToRasterLocation(ltx,lty,ltrx,ltry);
                scrollArea->CanvasLocationToRasterLocation(rbx,rby,rbrx,rbry);
                // 自动点中查找
                for(int i = 0; i<LeftFeatPtDataset.m_ImgPtSet.vecPts.size(); i++)
                {
                    double rx,ry ;
                    rx = LeftFeatPtDataset.m_ImgPtSet.vecPts.at(i).X;
                    ry = LeftFeatPtDataset.m_ImgPtSet.vecPts.at(i).Y;

                    if(rx > ltrx && rx < rbrx && ry > ltry && ry < rbry)
                    {
                        //                    scrollArea->FeatAutoSelectedIdx.clear();
                        //                    scrollArea->FeatManualSelectedIdx.clear();
                        scrollArea->FeatAutoSelectedIdx.append(i);
                    }
                }
                //手动点中查找
                for(int i = 0; i<LeftFeatPtDataset.m_ImgPtSet.vecAddPts.size(); i++)
                {

                    double rx,ry ;
                    rx = LeftFeatPtDataset.m_ImgPtSet.vecAddPts.at(i).X;
                    ry = LeftFeatPtDataset.m_ImgPtSet.vecAddPts.at(i).Y;

                    if(rx > ltrx && rx < rbrx && ry > ltry && ry < rbry)
                    {
                        //                    scrollArea->FeatAutoSelectedIdx.clear();
                        //                    scrollArea->FeatManualSelectedIdx.clear();
                        scrollArea->FeatManualSelectedIdx.append(i);
                    }
                }
                SelectRightPtsByLeftSelection();
                UpdateFeatPtTableView();
                // LeftFeatPtTableView->setFocus();
            }
        }

    }

}
void ImageViewer::RightScrollAreaMouseRealease(QMouseEvent* mouseEvent)
{
    double dx = RightScrollArea->DownPoint.x;
    double dy = RightScrollArea->DownPoint.y;
    double ux = mouseEvent->x();
    double uy = mouseEvent->y();
    switch (RightScrollArea->CurrentToolType)
    {
    case Tool_EditMatchPoint:
    {
        if(mouseEvent->button()!= Qt::LeftButton)
        {
            break;
        }
        // 鼠标点击选择时
        if(fabs(dx - ux)<3 && fabs(dy - uy) <3)
        {
            bool Found = false;
            // 自动点中查找
            for(int i = 0; i<RightFeatPtDataset.m_ImgPtSet.vecPts.size(); i++)
            {
                double cx,cy;
                RightScrollArea->RasterLocationToCanvasLocation(RightFeatPtDataset.m_ImgPtSet.vecPts.at(i).X,
                        RightFeatPtDataset.m_ImgPtSet.vecPts.at(i).Y,
                        cx,cy);
                //cx，cy得到的坐标为在imagelable中的坐标值，要转换在scrollarea中的坐标值
                //选中点在scrollarea中的坐标
                double dXInscrollarea,dyInscrollarea;
                if(RightScrollArea->RasterRect.left<0)
                {
                    dXInscrollarea = RightScrollArea->RasterRect.left + cx;
                }
                else
                {
                    dXInscrollarea = cx;
                }
                if(RightScrollArea->RasterRect.top < 0)
                {
                    dyInscrollarea = RightScrollArea->RasterRect.top + cy;
                }
                else
                {
                    dyInscrollarea = cy;
                }

                if(fabs(RightScrollArea->DownPoint.x - dXInscrollarea) < 5
                        && fabs(RightScrollArea->DownPoint.y - dyInscrollarea) < 5)
                {
                    RightScrollArea->FeatAutoSelectedIdx.clear();
                    RightScrollArea->FeatManualSelectedIdx.clear();
                    RightScrollArea->FeatAutoSelectedIdx.append(i);
                    Found = true;
                    break;
                }
            }
            //手动点中查找
            for(int i = 0; i<RightFeatPtDataset.m_ImgPtSet.vecAddPts.size(); i++)
            {
                if(Found)
                {
                    break;
                }
                double cx,cy;
                RightScrollArea->RasterLocationToCanvasLocation(RightFeatPtDataset.m_ImgPtSet.vecAddPts.at(i).X,
                        RightFeatPtDataset.m_ImgPtSet.vecAddPts.at(i).Y,
                        cx,cy);
                //cx，cy得到的坐标为在imagelable中的坐标值，要转换在scrollarea中的坐标值
                //选中点在scrollarea中的坐标
                double dXInscrollarea,dyInscrollarea;
                if(RightScrollArea->RasterRect.left<0)
                {
                    dXInscrollarea = RightScrollArea->RasterRect.left + cx;
                }
                else
                {
                    dXInscrollarea = cx;
                }
                if(RightScrollArea->RasterRect.top < 0)
                {
                    dyInscrollarea = RightScrollArea->RasterRect.top + cy;
                }
                else
                {
                    dyInscrollarea = cy;
                }

                if(fabs(RightScrollArea->DownPoint.x - dXInscrollarea) < 5
                        && fabs(RightScrollArea->DownPoint.y - dyInscrollarea) < 5)
                {
                    RightScrollArea->FeatManualSelectedIdx.clear();
                    RightScrollArea->FeatAutoSelectedIdx.clear();
                    RightScrollArea->FeatManualSelectedIdx.append(i);
                    Found = true;
                    //UpdateFeatPtTableView();
                    break;
                }
            }

            SelectLeftPtsByRightSelection();
            UpdateFeatPtTableView();
            // RightFeatPtTableView->setFocus();
            break;
        }
        // 鼠标拖拽矩形选择时
        else
            // 若以选中点则应该是拖拽点，而不是拖矩形选点
            if(!RightScrollArea->PickedAPoint)
            {
                RightScrollArea->FeatAutoSelectedIdx.clear();
                RightScrollArea->FeatManualSelectedIdx.clear();
                // 计算矩形左上右下点坐标
                double ltx = min(dx,ux);
                double lty = min(dy,uy);
                double rbx = max(dx,ux);
                double rby = max(dy,uy);
                double ltrx,ltry;
                double rbrx,rbry;
                //转换矩形坐标到图片的坐标
                RightScrollArea->CanvasLocationToRasterLocation(ltx,lty,ltrx,ltry);
                RightScrollArea->CanvasLocationToRasterLocation(rbx,rby,rbrx,rbry);
                // 自动点中查找
                for(int i = 0; i<RightFeatPtDataset.m_ImgPtSet.vecPts.size(); i++)
                {
                    double rx,ry ;
                    rx = RightFeatPtDataset.m_ImgPtSet.vecPts.at(i).X;
                    ry = RightFeatPtDataset.m_ImgPtSet.vecPts.at(i).Y;

                    if(rx > ltrx && rx < rbrx && ry > ltry && ry < rbry)
                    {
                        //                    scrollArea->FeatAutoSelectedIdx.clear();
                        //                    scrollArea->FeatManualSelectedIdx.clear();
                        RightScrollArea->FeatAutoSelectedIdx.append(i);
                    }
                }
                //手动点中查找
                for(int i = 0; i<RightFeatPtDataset.m_ImgPtSet.vecAddPts.size(); i++)
                {

                    double rx,ry ;
                    rx = RightFeatPtDataset.m_ImgPtSet.vecAddPts.at(i).X;
                    ry = RightFeatPtDataset.m_ImgPtSet.vecAddPts.at(i).Y;

                    if(rx > ltrx && rx < rbrx && ry > ltry && ry < rbry)
                    {
                        //                    scrollArea->FeatAutoSelectedIdx.clear();
                        //                    scrollArea->FeatManualSelectedIdx.clear();
                        RightScrollArea->FeatManualSelectedIdx.append(i);
                    }
                }
                SelectLeftPtsByRightSelection();
                UpdateFeatPtTableView();
            }
        }

    }


}

//! [15]
void ImageViewer::about()
//! [15] //! [16]
{

//    CmlGeoRaster cgr;
//    DialogCamera cd;
//    if(cd.exec())
//    {
//        double m_dXResolution ;
//        double m_dYResolution;
//        double  m_PtOriginX ;
//        double  m_PtOriginY ;
//         m_dXResolution = 0.00494668;
//         m_dYResolution = -0.00494668;
//          m_PtOriginX = -31.8617  ;
//          m_PtOriginY =  49.997 ;
//        QByteArray qAProjPath = cd.srcfilename.toLatin1();
//        char* cProjPath = qAProjPath.data();
//        cgr.LoadFile(cProjPath);
//        double m_dGdalTransPara[6];
//        cgr.GetGdalDataSet()->GetGeoTransform(m_dGdalTransPara);
//        m_dGdalTransPara[1] = m_dXResolution;
//        m_dGdalTransPara[5] = m_dYResolution;
//        m_dGdalTransPara[0] = m_PtOriginX;
//        m_dGdalTransPara[3] = m_PtOriginY;
//        char* projection;
//        OGRSpatialReference ogr;
//        ogr.SetWellKnownGeogCS("WGS84");
//        ogr.exportToWkt(&projection);
//        //const  char* dd = cgr.GetGdalDataSet()->GetProjectionRef();
//        //string ddd = dd;
//    CPLErr cp =    cgr.GetGdalDataSet()->SetGeoTransform(m_dGdalTransPara);
//           cp =   cgr.GetGdalDataSet()->SetProjection(projection);
//           cgr.GetGdalDataSet()->FlushCache();
//
//        int a = 0;
        return;

    }




//    m_ProgressBar->setRange( 0, 1000 );
//    m_ProgressBar->setValue( 402 );
//    m_ProgressBarLable->setText( QString::fromStdString( "sssssssssss" ));

//    CmlGeoRaster gdataset;
//    gdataset.LoadGeoFile("/home/lw/Desktop/testDem1.tif");
//    // CmlRasterBlock * prasterblock = new CmlRasterBlock();
//    // gdataset.GetRasterOriginBlock(1,0,0,5,5,1,prasterblock);
//    // DOUBLE dz ;
//    // char dz;
//    /*
//        dz = 0.1;
//        prasterblock->SetPtrAt(0,0,(BYTE*)&dz);
//        prasterblock->SetPtrAt(1,0,(BYTE*)&dz);
//        prasterblock->SetPtrAt(2,0,(BYTE*)&dz);
//        prasterblock->SetPtrAt(3,0,(BYTE*)&dz);
//        prasterblock->SetPtrAt(4,0,(BYTE*)&dz);
//
//        dz = 0.05;
//        prasterblock->SetPtrAt(0,1,(BYTE*)&dz);
//        prasterblock->SetPtrAt(1,1,(BYTE*)&dz);
//        prasterblock->SetPtrAt(2,1,(BYTE*)&dz);
//        prasterblock->SetPtrAt(3,1,(BYTE*)&dz);
//        prasterblock->SetPtrAt(4,1,(BYTE*)&dz);
//
//        dz = 0;
//        prasterblock->SetPtrAt(0,2,(BYTE*)&dz);
//        prasterblock->SetPtrAt(1,2,(BYTE*)&dz);
//        prasterblock->SetPtrAt(2,2,(BYTE*)&dz);
//        prasterblock->SetPtrAt(3,2,(BYTE*)&dz);
//        prasterblock->SetPtrAt(4,2,(BYTE*)&dz);
//
//
//        dz = - 0.05;
//        prasterblock->SetPtrAt(0,3,(BYTE*)&dz);
//        prasterblock->SetPtrAt(1,3,(BYTE*)&dz);
//        prasterblock->SetPtrAt(2,3,(BYTE*)&dz);
//        prasterblock->SetPtrAt(3,3,(BYTE*)&dz);
//        prasterblock->SetPtrAt(4,3,(BYTE*)&dz);
//
//
//        dz = - 0.1;
//        prasterblock->SetPtrAt(0,4,(BYTE*)&dz);
//        prasterblock->SetPtrAt(1,4,(BYTE*)&dz);
//        prasterblock->SetPtrAt(2,4,(BYTE*)&dz);
//        prasterblock->SetPtrAt(3,4,(BYTE*)&dz);
//        prasterblock->SetPtrAt(4,4,(BYTE*)&dz);
//        */
//    /*
//        dz = 0;
//        prasterblock->SetPtrAt(0,0,(BYTE*)&dz);
//        prasterblock->SetPtrAt(1,1,(BYTE*)&dz);
//        prasterblock->SetPtrAt(2,2,(BYTE*)&dz);
//        prasterblock->SetPtrAt(3,3,(BYTE*)&dz);
//        prasterblock->SetPtrAt(4,4,(BYTE*)&dz);
//
//        dz = 0.05 * sqrt(2);
//        prasterblock->SetPtrAt(0,1,(BYTE*)&dz);
//        prasterblock->SetPtrAt(1,2,(BYTE*)&dz);
//        prasterblock->SetPtrAt(2,3,(BYTE*)&dz);
//        prasterblock->SetPtrAt(3,4,(BYTE*)&dz);
//
//
//        dz = 0.05 * sqrt(2) * 2;
//        prasterblock->SetPtrAt(0,2,(BYTE*)&dz);
//        prasterblock->SetPtrAt(1,3,(BYTE*)&dz);
//        prasterblock->SetPtrAt(2,4,(BYTE*)&dz);
//
//
//        dz = 0.05 * sqrt(2) * 3;
//        prasterblock->SetPtrAt(0,3,(BYTE*)&dz);
//        prasterblock->SetPtrAt(1,4,(BYTE*)&dz);
//
//        dz =  0.05 * sqrt(2) * 4;
//        prasterblock->SetPtrAt(0,4,(BYTE*)&dz);
//
//
//        dz = 0 - 0.05 * sqrt(2);
//        prasterblock->SetPtrAt(1,0,(BYTE*)&dz);
//        prasterblock->SetPtrAt(2,1,(BYTE*)&dz);
//        prasterblock->SetPtrAt(3,2,(BYTE*)&dz);
//        prasterblock->SetPtrAt(4,3,(BYTE*)&dz);
//
//
//        dz =0 - 0.05 * sqrt(2) * 2;
//        prasterblock->SetPtrAt(2,0,(BYTE*)&dz);
//        prasterblock->SetPtrAt(3,1,(BYTE*)&dz);
//        prasterblock->SetPtrAt(4,2,(BYTE*)&dz);
//
//
//        dz =  0 - 0.05 * sqrt(2) * 3;
//        prasterblock->SetPtrAt(3,0,(BYTE*)&dz);
//        prasterblock->SetPtrAt(4,1,(BYTE*)&dz);
//
//        dz = 0- 0.05 * sqrt(2) * 4;
//        prasterblock->SetPtrAt(4,0,(BYTE*)&dz);
//    */
//    /*
//        dz = 0;
//        prasterblock->SetPtrAt(0,0,(BYTE*)&dz);
//        prasterblock->SetPtrAt(1,0,(BYTE*)&dz);
//        prasterblock->SetPtrAt(2,0,(BYTE*)&dz);
//        prasterblock->SetPtrAt(3,0,(BYTE*)&dz);
//        prasterblock->SetPtrAt(4,0,(BYTE*)&dz);
//
//        dz = 0;
//        prasterblock->SetPtrAt(0,1,(BYTE*)&dz);
//        prasterblock->SetPtrAt(1,1,(BYTE*)&dz);
//        prasterblock->SetPtrAt(2,1,(BYTE*)&dz);
//        prasterblock->SetPtrAt(3,1,(BYTE*)&dz);
//        prasterblock->SetPtrAt(4,1,(BYTE*)&dz);
//
//        dz = 0;
//        prasterblock->SetPtrAt(0,2,(BYTE*)&dz);
//        prasterblock->SetPtrAt(1,2,(BYTE*)&dz);
//        prasterblock->SetPtrAt(2,2,(BYTE*)&dz);
//        prasterblock->SetPtrAt(3,2,(BYTE*)&dz);
//        prasterblock->SetPtrAt(4,2,(BYTE*)&dz);
//
//
//        dz = 0;
//        prasterblock->SetPtrAt(0,3,(BYTE*)&dz);
//        prasterblock->SetPtrAt(1,3,(BYTE*)&dz);
//        prasterblock->SetPtrAt(2,3,(BYTE*)&dz);
//        prasterblock->SetPtrAt(3,3,(BYTE*)&dz);
//        prasterblock->SetPtrAt(4,3,(BYTE*)&dz);
//
//
//        dz = 0;
//        prasterblock->SetPtrAt(0,4,(BYTE*)&dz);
//        prasterblock->SetPtrAt(1,4,(BYTE*)&dz);
//        prasterblock->SetPtrAt(2,4,(BYTE*)&dz);
//        prasterblock->SetPtrAt(3,4,(BYTE*)&dz);
//        prasterblock->SetPtrAt(4,4,(BYTE*)&dz);
//       double zzz;
//       prasterblock->GetDoubleVal(0,0,zzz);
//
//    gdataset.SaveBlockToFile(1,0,0,prasterblock,1,1,3,3);
//    */
//
//    /*gdataset.GetGdalDataSet()->GetRasterBand(1)->FlushCache();
//    delete prasterblock;
//    prasterblock = NULL;
//    prasterblock = new CmlRasterBlock();
//    gdataset.GetRasterOriginBlock(1,0,0,5,5,1,prasterblock);
//    prasterblock->GetDoubleVal(0,0,zzz);
//    */
////CmlGeoRaster pdestdataset;
////pdestdataset.CreateGeoFile("/home/lw/Desktop/createdfile.tif",gdataset.m_PtOrigin,gdataset.m_dXResolution,gdataset.m_dYResolution
//    //  ,gdataset.GetHeight(),gdataset.GetWidth(),1,GDT_Byte,-99999);
////pdestdataset.SaveBlockToFile(1,0,0,prasterblock);
//
//
//
////gdataset.SaveBlockToFile(1,0,0,prasterblock,1,1,4,4);
//
//    CmlGeoRaster destdataset;
//    destdataset.CreateGeoFile("/home/lw/Desktop/testDEMSlope.tif",gdataset.m_PtOrigin,gdataset.m_dXResolution,gdataset.m_dYResolution
//                              ,gdataset.GetHeight(),gdataset.GetWidth(),1,GDT_Float64,-99999);
    //CmlDemAnalyse demanaly;
    //demanaly.ComputeSlope(&gdataset,5,&destdataset,gdataset.m_dXResolution,1);

    /*
    scrollArea->CreatCopiedImage();
    */
    /*
       CmlGeoRaster gdataset;
       gdataset.LoadGeoFile("/home/lw/Desktop/hghgh3.tif");
      // CmlDemAnalyse demanaly(&gdataset);
       CmlRasterBlock RasterBlock;
       gdataset.GetRasterOriginBlock(1,0,0,gdataset.GetWidth(),gdataset.GetHeight() ,1,&RasterBlock);
       CmlGeoRaster destdataset;
       destdataset.CreateGeoFile("/home/lw/Desktop/hghgh3viewshed.tif",gdataset.m_PtOrigin,gdataset.m_dXResolution,gdataset.m_dYResolution
                             ,gdataset.GetHeight(),gdataset.GetWidth(),1,GDT_Byte,-99999);
       CmlDemAnalyse demanaly(&RasterBlock);
       CmlRasterBlock* pOutBlock;
       demanaly.m_ComputeViewShed(3,3,300,pOutBlock);

       destdataset.SaveBlockToFile(1,0,0,pOutBlock);
       */
    // demanaly.ComputeViewShed();
//scrollArea->ProduceContourLine();
    // dla.show();
    // dla.raise();
    // dla.activateWindow();
    // dla.exec();   // dla.show();

    /*
        QMessageBox::about(this, tr("About Image Viewer"),
                tr("<p>The <b>Image Viewer</b> example shows how to combine QLabel "
                   "and QScrollArea to display an image. QLabel is typically used "
                   "for displaying a text, but it can also display an image. "
                   "QScrollArea provides a scrolling view around another widget. "
                   "If the child widget exceeds the size of the frame, QScrollArea "
                   "automatically provides scroll bars. </p><p>The example "
                   "demonstrates how QLabel's ability to scale its contents "
                   "(QLabel::scaledContents), and QScrollArea's ability to "
                   "automatically resize its contents "
                   "(QScrollArea::widgetResizable), can be used to implement "
                   "zooming and scaling features. </p><p>In addition the example "
                   "shows how to use QPainter to print an image.</p>"));
    */
//}
//! [16]
void ImageViewer::Contour()
{
    ContourDialog cd ;
    if(cd.exec())
    {
        double interval = cd.interval;
        QByteArray gb   = cd.srcfilename.toLatin1();
        char * src = gb.data();
        QByteArray gb1 = cd.dstfilename.toLatin1();
        char * dst = gb1.data();
        int d = mlComeputeContour(interval,src,dst);
        if(d < 0)
        {
            return;
        }
        return;
    }
}
//! [17]
void ImageViewer::createActions()
//! [17] //! [18]
{
    openAct = new QAction(tr("&Open..."), this);
    openAct->setShortcut(tr("Ctrl+O"));
    connect(openAct, SIGNAL(triggered()), this, SLOT(open()));

    RightopenAct = new QAction(tr("&OpenRight..."),this);
    RightopenAct->setShortcut(tr("Ctrl+R"));
    connect(RightopenAct,SIGNAL(triggered()),this,SLOT(rightopen()));

    OpenMatchPtsAct = new QAction(tr("OpenMatchPts"),this);
    connect(OpenMatchPtsAct,SIGNAL(triggered()),this,SLOT(OpenMatchPts()));

    OpenDenseMatchPtsAct = new QAction(tr("OpenDenseMatchPts"),this);
    connect(OpenDenseMatchPtsAct,SIGNAL(triggered()),this,SLOT(OpenDenseMatchPts()));

    SavePtToFileAct = new QAction(tr("SavePtToFile"),this);
    connect(SavePtToFileAct,SIGNAL(triggered()),this,SLOT(SavePtToFile()));

    SavePtAct = new QAction(tr("SavePt"),this);
    connect(SavePtAct,SIGNAL(triggered()),this,SLOT(SavePt()));

//    printAct = new QAction(tr("&Print..."), this);
//    printAct->setShortcut(tr("Ctrl+P"));
//    printAct->setEnabled(false);
//    connect(printAct, SIGNAL(triggered()), this, SLOT(print()));

    exitAct = new QAction(tr("E&xit"), this);
    exitAct->setShortcut(tr("Ctrl+Q"));
    connect(exitAct, SIGNAL(triggered()), this, SLOT(close()));

//    zoomInAct = new QAction(tr("Zoom &In (25%)"), this);
//    zoomInAct->setShortcut(tr("Ctrl++"));
//    zoomInAct->setEnabled(false);
//    connect(zoomInAct, SIGNAL(triggered()), this, SLOT(zoomIn()));

//    zoomOutAct = new QAction(tr("Zoom &Out (25%)"), this);
//    zoomOutAct->setShortcut(tr("Ctrl+-"));
//    zoomOutAct->setEnabled(false);
//    connect(zoomOutAct, SIGNAL(triggered()), this, SLOT(zoomOut()));

//    normalSizeAct = new QAction(tr("&Normal Size"), this);
//    normalSizeAct->setShortcut(tr("Ctrl+S"));
//    normalSizeAct->setEnabled(false);
//    connect(normalSizeAct, SIGNAL(triggered()), this, SLOT(normalSize()));

//    fitToWindowAct = new QAction(tr("&Fit to Window"), this);
//    fitToWindowAct->setEnabled(false);
//    fitToWindowAct->setCheckable(true);
//    fitToWindowAct->setShortcut(tr("Ctrl+F"));
//    connect(fitToWindowAct, SIGNAL(triggered()), this, SLOT(fitToWindow()));

    //地形菜单名称
    actCameraCalibration = new QAction(tr("Camera Calibration"),this);//相机标定
    connect(actCameraCalibration,SIGNAL(triggered()), this ,SLOT(CameraCalibration()));

    actCameraSurvey = new QAction(tr("Camera Survey"),this);//单目相机量测
    connect(actCameraSurvey ,SIGNAL(triggered()), this ,SLOT(CameraSurvey()));

    actSeriesImageDem = new QAction(tr("SeriesImage Dem"),this);//序列图像着陆区三维重构
    connect(actSeriesImageDem ,SIGNAL(triggered()), this ,SLOT(SeriesImageDem()));

    actOrbitImageDem = new QAction(tr("OrbitImage Dem"),this);//卫星影像着陆区三维重构
    connect(actOrbitImageDem ,SIGNAL(triggered()), this ,SLOT(OrbitImageDem()));

    actPanoMosaic = new QAction(tr("Pano Mosaic"),this);//全景图像拼接
    connect(actPanoMosaic ,SIGNAL(triggered()), this ,SLOT(PanoMosaic()));

    actPersImageCreate = new QAction(tr("PersImage Create"),this);//原视点下透视图像生成
    connect(actPersImageCreate ,SIGNAL(triggered()), this ,SLOT(PersImageCreate()));

    actSiteDemMosaic = new QAction(tr("SiteDem Mosaic"),this);//单站点地形拼接
    connect(actSiteDemMosaic ,SIGNAL(triggered()), this ,SLOT(SiteDemMosaic()));

    actMultSiteDemMosaic = new QAction(tr("MultSiteDem Mosaic"),this);//多站点地形拼接
    connect(actMultSiteDemMosaic ,SIGNAL(triggered()), this ,SLOT(MultSiteDemMosaic()));

    actWideBaselineMap = new QAction(tr("WideBaseline Map"),this);//长基线测图
    connect(actWideBaselineMap ,SIGNAL(triggered()), this ,SLOT(WideBaselineMap()));

    actWidebaseAnalysis = new QAction(tr("Widebase Analysis"),this);//长基线分析
    connect(actWidebaseAnalysis ,SIGNAL(triggered()), this ,SLOT(WidebaseAnalysis()));

    actTinSimplify = new QAction(tr("Tin Simplify"),this);//TIN简化
    connect(actTinSimplify ,SIGNAL(triggered()), this ,SLOT(TinSimplify()));

    actDemDomProcess = new QAction(tr("DemDom Process"),this);//DEM、DOM处理
    connect(actDemDomProcess ,SIGNAL(triggered()), this ,SLOT(DemDomProcess()));

    actVisualImage = new QAction(tr("Visual Image"),this);//指定视角图像生成
    connect(actVisualImage ,SIGNAL(triggered()), this ,SLOT(VisualImage()));

    actContourLine = new QAction(tr("ContourLine"),this);//等高线生成
    connect(actContourLine ,SIGNAL(triggered()), this ,SLOT(ContourLine()));

    actSlope = new QAction(tr("Slope"),this);//坡度图生成
    connect(actSlope ,SIGNAL(triggered()), this ,SLOT(Slope()));

    actSlopeAspect = new QAction(tr("SlopeAspect"),this);//坡向图生成
    connect(actSlopeAspect ,SIGNAL(triggered()), this ,SLOT(SlopeAspect()));

    actBarrier = new QAction(tr("Barrier"),this);//障碍物分布
    connect(actBarrier ,SIGNAL(triggered()), this ,SLOT(Barrier()));

    actInsight = new  QAction(tr("Insight"),this);//通视分析
    connect(actInsight ,SIGNAL(triggered()), this ,SLOT(Insight()));

    actDenseMatch = new QAction(tr("DenseMatch"),this);//密集匹配
    connect(actDenseMatch, SIGNAL(triggered()), this, SLOT(DenseMatch()));

    actDEMMosaic = new QAction(tr("DEMMosaic"), this);// DEM pingjie
    connect(actDEMMosaic, SIGNAL(triggered()), this, SLOT(DEMMosaic()));

    actDisParityMap = new QAction(tr("DisParityMap"), this);// DisParityMap
    connect(actDisParityMap, SIGNAL(triggered()), this, SLOT(DisParityMap()));

    actEpipolarImage = new QAction(tr("EpipolarImage"), this);//核线影像
    connect(actEpipolarImage, SIGNAL(triggered()), this, SLOT(EpipolarImage()));

    actRelOrientation = new QAction(tr("actRelOrientation"), this);//相对定向
    connect(actRelOrientation, SIGNAL(triggered()), this, SLOT(RelOrientation()));

    //视觉定位菜单名称
    actLandLocate = new QAction(tr("LandLocate"),this);//着陆点概略定位
    connect(actLandLocate ,SIGNAL(triggered()), this ,SLOT(LandLocate()));

    actLandLocateMatch = new QAction(tr("LandLocateMatch"),this);//基于图像匹配的着陆点定位
    connect(actLandLocateMatch ,SIGNAL(triggered()), this ,SLOT(LandLocateMatch()));

    actLandLocateInter = new QAction(tr("LandLocateInter"),this);//基于图像匹配的着陆点定位
    connect(actLandLocateInter ,SIGNAL(triggered()), this ,SLOT(LandLocateInter()));

    actCoordTransform = new QAction(tr("CoordTransform"),this);//坐标转换
    connect(actCoordTransform ,SIGNAL(triggered()), this ,SLOT(CoordTransform()));

    actRoverLocateLander = new QAction(tr("RoverLocateLander"),this);//巡视器同着陆器相对定位
    connect(actRoverLocateLander ,SIGNAL(triggered()), this ,SLOT(RoverLocateLander()));

    actRoverLocate = new QAction(tr("RoverLocate"),this);//巡视器定位
    connect(actRoverLocate ,SIGNAL(triggered()), this ,SLOT(RoverLocate()));

    aboutAct = new QAction(tr("&About"), this);
    connect(aboutAct, SIGNAL(triggered()), this, SLOT(about()));

    aboutQtAct = new QAction(tr("About &Qt"), this);
    connect(aboutQtAct, SIGNAL(triggered()), qApp, SLOT(aboutQt()));

    ContourAct = new QAction(tr("Contour"),this);
    connect(ContourAct,SIGNAL(triggered()), this, SLOT(Contour()));
}
//! [18]


//! [19]
void ImageViewer::creatToolBar()
{
    BasicToolBar = this->addToolBar(tr("Basic ToolBar"));

    QFileInfo fileinfo("img/pan.png");
    QString zz = fileinfo.absoluteFilePath();
    QByteArray dd = zz.toLatin1();
    char* ddd = dd.data();

    ToolActPanTool = new QAction(QIcon("img/pan.png"), tr("Pan"),this);
    ToolActPanTool->setCheckable(true);
    ToolActPanTool->setChecked(true);
    connect(ToolActPanTool,SIGNAL(triggered()),this,SLOT(ToolActPanTooltriggered( )));

    ToolActEditMatchPoint = new QAction(QIcon("img/Edit FeatPoint.png"),tr("Edit FeatPoint"),this);
    ToolActEditMatchPoint->setCheckable(true);
    connect(ToolActEditMatchPoint,SIGNAL(triggered()),this,SLOT(ToolActEditMatchPointTooltriggered( )));

    ToolActAddFeatPoint = new QAction(QIcon("img/Add FeatPoint.png"),tr("Add FeatPoint"),this);
    ToolActAddFeatPoint->setCheckable(true);
    connect(ToolActAddFeatPoint,SIGNAL(triggered()), this,SLOT(ToolActAddFeatPointTooltriggered( )));

    ToolActEditFeatPoint = new QAction(tr("Edit FeatPoint"),this);
    ToolActEditFeatPoint->setCheckable(true);
    connect(ToolActEditFeatPoint,SIGNAL(triggered()), this,SLOT(ToolActEditFeatPointTooltriggered( )));


    ToolActAddStereFeatPoint = new QAction(QIcon("img/Add Stere Points.png"),tr("Add Stere Points"),this);
    ToolActAddStereFeatPoint->setCheckable(true);
    connect(ToolActAddStereFeatPoint,SIGNAL(triggered()),this,SLOT(ToolActAddStereFeatPointtriggerd()));

    ToolActSemiAutoAddFeatPoint = new QAction(QIcon("img/Add Stere Points.png"),tr("Semi-Auto Add Stere Points"),this);
    ToolActSemiAutoAddFeatPoint->setCheckable(true);
    connect(ToolActSemiAutoAddFeatPoint,SIGNAL(triggered()),this,SLOT(ToolActSemiAutoAddFeatPointtriggerd()));

    ToolActShowRegionPlanVect = new QAction(QIcon("img/Add Stere Points.png"),tr("Show Plane Vector"),this);
    ToolActShowRegionPlanVect->setCheckable(true);
    connect(ToolActShowRegionPlanVect,SIGNAL(triggered()),this,SLOT(ToolActShowRegionPlanVecttriggerd()));

    BasicToolBar->addAction(ToolActPanTool);
    BasicToolBar->addAction(ToolActEditMatchPoint);
    BasicToolBar->addAction(ToolActAddFeatPoint);
    BasicToolBar->addAction(ToolActAddStereFeatPoint);
    BasicToolBar->addAction(ToolActSemiAutoAddFeatPoint);
    BasicToolBar->addAction(ToolActShowRegionPlanVect);
    // BasicToolBar->addAction(ToolActEditFeatPoint);

    MutexToolList.append(ToolActPanTool);
    MutexToolList.append(ToolActEditMatchPoint);
    MutexToolList.append(ToolActAddFeatPoint);
    MutexToolList.append(ToolActEditFeatPoint);
    MutexToolList.append(ToolActAddStereFeatPoint);
    MutexToolList.append(ToolActSemiAutoAddFeatPoint);
    MutexToolList.append(ToolActShowRegionPlanVect);
}
void ImageViewer::ToolActEditMatchPointTooltriggered( )
{
    ToolActEditMatchPoint->setChecked(true);
    //if(Checked)
    {
        SetCurrentToolActionCheckStatus(ToolActEditMatchPoint);
        scrollArea->CurrentToolType = Tool_EditMatchPoint;
        RightScrollArea->CurrentToolType =Tool_EditMatchPoint;
    }
}

void ImageViewer::ToolActPanTooltriggered( )
{
    ToolActPanTool->setChecked(true);
    // if(Checked)
    {
        SetCurrentToolActionCheckStatus(ToolActPanTool);
        scrollArea->CurrentToolType =  Tool_pan;
        RightScrollArea->CurrentToolType = Tool_pan;
    }
}
void ImageViewer::ToolActAddFeatPointTooltriggered( )
{
    ToolActAddFeatPoint->setChecked(true);
    // if(Checked)
    {
        SetCurrentToolActionCheckStatus(ToolActAddFeatPoint);
        scrollArea->CurrentToolType =  Tool_AddFeatPoint;
        RightScrollArea->CurrentToolType = Tool_AddFeatPoint;
    }
}
void ImageViewer::ToolActEditFeatPointTooltriggered( )
{
    ToolActEditFeatPoint->setChecked(true);
    //if(Checked)
    {
        SetCurrentToolActionCheckStatus(ToolActEditFeatPoint);
        scrollArea->CurrentToolType =  Tool_EditFeatPoint;
        RightScrollArea->CurrentToolType = Tool_EditFeatPoint;
    }
}

void ImageViewer::ToolActAddStereFeatPointtriggerd()
{
    if(AddFeatPointStep == 0)
    {
        dlgPointCoord.SetText(tr("Choose a point in one image"));
    }
    else
    {
        if(AddLeftOrRight == 1)
        {
            dlgPointCoord.SetText(tr("Choose a poin in right image"));
        }
        else
        {
            dlgPointCoord.SetText(tr("Choose a poin in left image"));
        }
    }
    dlgPointCoord.show();

    ToolActAddStereFeatPoint->setChecked(true);
    dlgPointCoord.show();
    SetCurrentToolActionCheckStatus(ToolActAddStereFeatPoint);
    scrollArea->CurrentToolType = Tool_AddStereFeatPoint;
    RightScrollArea->CurrentToolType = Tool_AddStereFeatPoint;
}

void ImageViewer::ToolActSemiAutoAddFeatPointtriggerd()
{
    dlgPointCoord.SetText(tr("Choose a point in one image"));

    ToolActSemiAutoAddFeatPoint->setChecked(true);

    SetCurrentToolActionCheckStatus(ToolActSemiAutoAddFeatPoint);
    scrollArea->CurrentToolType = Tool_ToolActSemiAutoAddFeatPoint;
    RightScrollArea->CurrentToolType = Tool_ToolActSemiAutoAddFeatPoint;

}
void ImageViewer::ToolActShowRegionPlanVecttriggerd()
{

    ToolActShowRegionPlanVect->setChecked(true);
    SetCurrentToolActionCheckStatus(ToolActShowRegionPlanVect);
    scrollArea->CurrentToolType = Tool_ToolActShowRegionPlanVect;
    RightScrollArea->CurrentToolType = Tool_ToolActShowRegionPlanVect;


    DialogDenseMatch cd;
    cd.setWindowTitle(tr("DenseMatch"));
    if(cd.exec())
    {
        //读入工程文件，密集匹配，写视差图
        GaussianFilterOpt GauPara;
        MatchInRegPara MatchPara;
        RANSACHomePara RanPara;
        MLRectSearch RectSearch;

        WideOptions WidePara;
//        WidePara.nStep = 1;
//        WidePara.nRadiusX =5;
//        WidePara.nRadiusY = 3;
//        WidePara.nTemplateSize = 13;
//        WidePara.dCoef = 0;

        ExtractFeatureOpt ExtractPara;
        SINT Lx, Ly, nColRange, nRowRange;

        //输入单站点工程文件smp
        QByteArray gbsrc   = cd.srcfilename.toLatin1();
        char * srcPath = gbsrc.data();

        QByteArray gbsrcpara   = cd.srcParafilename.toLatin1();
        char * paraPath = gbsrcpara.data();

        //加入全局和局部的选项
        bool bGlobal = cd.bGlobalMatch;
        SINT nIndex = cd.index;
        //读入参数文件
        ifstream stm(paraPath);
        FILE *pIOFile;

        if((pIOFile = fopen(paraPath,"r")))
        {

            stm >> GauPara.nTemplateSize >> GauPara.dCoef ;
            stm >> ExtractPara.nGridSize >> ExtractPara.nPtMaxNum;
            stm >> MatchPara.nTempSize >> MatchPara.dCoefThres;
            stm >> RanPara.dThres >> RanPara.dConfidence;
            stm >> RectSearch.dXMin >> RectSearch.dYMin >> RectSearch.dXMax >> RectSearch.dYMax;
            stm >> WidePara.nStep >> WidePara.nRadiusX >> WidePara.nRadiusY >> WidePara.nTemplateSize >> WidePara.dCoef  ;
            stm >> Lx >> Ly >> nColRange >> nRowRange;
            stm.close();
        }
        else
        {
            cout << "The photo number is wrong! \n";
            return;
        }
        CmlSiteMapProj site;
        if( false == site.LoadProj( srcPath ))
        {
            return;
        }
        int nSiteID, nRollID, nImgID;
        vector<StereoSet> vecStereoSet;

        site.GetDealSet( 1, 1, nIndex, vecStereoSet);


        int nNum = vecStereoSet.size();
        vector<ImgPtSet> vecDPtL(nNum), vecDPtR(nNum);
        vector<ImgPtSet> vecFPtL(nNum), vecFPtR(nNum);
        vector<Pt3d> vecPt3d;
        vector<DOUBLE> vecCorr;

        m_site = site;
        m_vecStereoSet = vecStereoSet;
        m_ExtractPara = ExtractPara;
        m_GauPara = GauPara;
        m_MatchPara = MatchPara;
        m_RanPara = RanPara;
        m_RectSearch = RectSearch;
        m_WidePara = WidePara;
        m_nColRange = nColRange;
        m_nRowRange = nRowRange;

        //site.CreateRegionDmf(vecStereoSet, ExtractPara, WidePara, Lx, Ly, nColRange, nRowRange, vecDPtL, vecDPtR, vecPt3d, vecCorr);
       // site.CreateRegionDmf(vecStereoSet, GauPara, ExtractPara, MatchPara, RanPara, RectSearch, WidePara, Lx, Ly, nColRange, nRowRange, vecDPtL, vecDPtR, vecPt3d, vecCorr);

    }
    else
    {
        ToolActShowRegionPlanVect->setChecked(false);
        scrollArea->CurrentToolType = Tool_None;
        RightScrollArea->CurrentToolType = Tool_None;
    }
}

void ImageViewer::SetCurrentToolActionCheckStatus(QAction* act)
{
    for(int i = 0; i < MutexToolList.count(); i++)
    {
        if(act != NULL)
        {
            QString dd = MutexToolList.at(i)->objectName();
            if( MutexToolList.at(i) ==  act)
            {
                //MutexToolList.at(i)->setChecked(true);
                continue;
            }
            MutexToolList.at(i)->setChecked(false);
        }
        else
        {
            MutexToolList.at(i)->setChecked(false);
        }
    }
}

void ImageViewer::createMenus()
//! [19] //! [20]
{
    fileMenu = new QMenu(tr("&File"), this);
    fileMenu->addAction(openAct);
    // fileMenu->addAction(RightopenAct);
    fileMenu->addAction(OpenMatchPtsAct);
    fileMenu->addAction(OpenDenseMatchPtsAct);
    fileMenu->addAction(SavePtToFileAct);
    fileMenu->addAction(SavePtAct);
    //fileMenu->addAction(printAct);
    fileMenu->addSeparator();
    fileMenu->addAction(exitAct);

    viewMenu = new QMenu(tr("&View"), this);
    // viewMenu->addAction(zoomInAct);
    // viewMenu->addAction(zoomOutAct);
    // viewMenu->addAction(normalSizeAct);
    viewMenu->addSeparator();
    //viewMenu->addAction(fitToWindowAct);

    //地形建立菜单
    menuMc = new QMenu(tr("&MC"),this);
    menuMc->addAction(actCameraCalibration);
    menuMc->addAction(actCameraSurvey);
    menuMc->addAction(actSeriesImageDem);
    menuMc->addAction(actOrbitImageDem);
    menuMc->addAction(actPanoMosaic);
    menuMc->addAction(actPersImageCreate);
    menuMc->addAction(actSiteDemMosaic);
    //menuMc->addAction(actMultSiteDemMosaic);
    menuMc->addAction(actWideBaselineMap);
    menuMc->addAction(actWidebaseAnalysis);
    menuMc->addAction(actTinSimplify);
    menuMc->addAction(actDemDomProcess);
    menuMc->addAction(actVisualImage);
    menuMc->addAction(actContourLine);
    menuMc->addAction(actSlope);
    menuMc->addAction(actSlopeAspect);
    menuMc->addAction(actBarrier);
    menuMc->addAction(actInsight);
    menuMc->addAction(actDenseMatch);
    menuMc->addAction(actDEMMosaic);
    menuMc->addAction(actDisParityMap);
    menuMc->addAction(actEpipolarImage);
    menuMc->addAction(actRelOrientation);

    //视觉定位菜单
    menuMo = new QMenu(tr("M&O"),this);
    menuMo->addAction(actLandLocate);
    menuMo->addAction(actLandLocateMatch);
    menuMo->addAction(actLandLocateInter);
    menuMo->addAction(actCoordTransform);
    menuMo->addAction(actRoverLocateLander);
    menuMo->addAction(actRoverLocate);

    //帮助菜单
    helpMenu = new QMenu(tr("&Help"), this);
    helpMenu->addAction(aboutAct);
//    helpMenu->addAction(aboutQtAct);
//    helpMenu->addAction(ContourAct);


    menuBar()->addMenu(fileMenu);
    menuBar()->addMenu(viewMenu);
    menuBar()->addMenu(menuMc);
    menuBar()->addMenu(menuMo);
    menuBar()->addMenu(helpMenu);

}
bool ImageViewer::SetProgressBar( string strCaption, int nStart, int nEnd )
{
    SetValToProgressBar( 0 );
    m_ProgressBar->setRange( nStart, nEnd );

    m_ProgressBarLable->setText( QString::fromStdString( strCaption ));

    return true;
}
bool ImageViewer::SetValToProgressBar( int nPos )
{
    m_ProgressBar->setValue( nPos );
    m_nCurPos = nPos;
    return true;
}
bool ImageViewer::StepIn()
{
    ++m_nCurPos;
    SetValToProgressBar( m_nCurPos );
    return true;
}




