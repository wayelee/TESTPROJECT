#ifndef DIALOGLANDLOCATEMATCH_H
#define DIALOGLANDLOCATEMATCH_H

#include <QDialog>
#include <qfiledialog.h>

namespace Ui {
    class DialogLandLocateMatch;
}

class DialogLandLocateMatch : public QDialog
{
    Q_OBJECT

public:
    explicit DialogLandLocateMatch(QWidget *parent = 0);
    ~DialogLandLocateMatch();
    QString LandDOMsrcfilename;
    QString SatelliteDOMsrcfilename;
    QString dstfilename;


private slots:
    void on_pushButtonLandDOMSource_clicked();

    void on_pushButtonSatelliteDOMSource_clicked();

    void on_pushButtonDest_clicked();

    void on_buttonBox_accepted();

private:
    Ui::DialogLandLocateMatch *ui;
};

#endif // DIALOGLANDLOCATEMATCH_H
