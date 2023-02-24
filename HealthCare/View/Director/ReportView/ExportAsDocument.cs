using Controller.UserControllers;
using Model.DataReport;
using System;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace View.Director.ReportView
{
    public class ExportAsDocument
    {

        public static void ExportAsPDF(DoctorOccupationReport report, int ukupno, int zauzeto, IUserController userController)
        {
            FlowDocument doc = new FlowDocument();

            doc.PagePadding = new System.Windows.Thickness(40);
            doc.PageWidth = 1240;
            doc.ColumnWidth = 1240;
            doc.PageHeight = 1754;
            doc.IsColumnWidthFlexible = false;
            doc.Background = Brushes.White;

            StackPanel stackPanel = new StackPanel();
            stackPanel.Orientation = Orientation.Horizontal;
            stackPanel.VerticalAlignment = System.Windows.VerticalAlignment.Center;

            Image logo = new Image();
            logo.Source = new BitmapImage(new Uri(@"..\..\View\Director\Styles\Images\Logo2.png", UriKind.RelativeOrAbsolute));
            logo.Height = 120;
            TextBlock tb = new TextBlock();
            tb.Text = "Zdravo korporacija";
            tb.FontSize = 80;
            tb.VerticalAlignment = System.Windows.VerticalAlignment.Center;
            tb.Margin = new System.Windows.Thickness(50, 0, 0, 0);
            tb.FontFamily = new FontFamily("Segoe UI Light");

            stackPanel.Children.Add(logo);
            stackPanel.Children.Add(tb);

            Paragraph top = new Paragraph();
            top.Inlines.Add(stackPanel);

            doc.Blocks.Add(top);

            Paragraph datum = new Paragraph();
            datum.FontSize = 35;
            tb = new TextBlock();
            tb.FontFamily = new FontFamily("Sagoe UI");
            tb.Text = "Izvestaj za period od " + report.DateFrom.ToShortDateString() + " do " + report.DateTo.ToShortDateString();
            TextBlock textBlock = new TextBlock();
            textBlock.FontFamily = new FontFamily("Sagoe UI");
            textBlock.Text = "Od ukupno " + ukupno + " sati doktori su bili zauzeti " + zauzeto;

            datum.Inlines.Add(tb);
            datum.Inlines.Add(textBlock);

            doc.Blocks.Add(datum);

            Table table = new Table();
            table.FontSize = 20;
            table.FontFamily = new FontFamily("Sagoe UI");
            TableColumn kolona1 = new TableColumn();
            TableColumn kolona2 = new TableColumn();
            TableColumn kolona3 = new TableColumn();
            kolona1.Width = new System.Windows.GridLength(100);
            kolona2.Width = new System.Windows.GridLength(500);
            kolona3.Width = new System.Windows.GridLength(640);
            table.Columns.Add(kolona1);
            table.Columns.Add(kolona2);
            table.Columns.Add(kolona3);
            TableRowGroup tableRowGroup = new TableRowGroup();

            foreach (var v in report.doctorOccupation)
            {
                TableRow r = new TableRow();

                v.doctor = (Model.Users.Doctor)userController.GetFromID(v.doctor.Id);

                //slika
                Image slika = new Image();
                slika.Source = new BitmapImage(new Uri(v.doctor.Picture, UriKind.RelativeOrAbsolute));
                slika.Width = 80;
                slika.Height = 80;
                Paragraph slikaParagraf = new Paragraph();
                slikaParagraf.Inlines.Add(slika);

                TableCell slikaCelija = new TableCell(slikaParagraf);
                slikaCelija.ColumnSpan = 1;
                slikaCelija.BorderThickness = new System.Windows.Thickness(0, 0, 0, 1);
                slikaCelija.BorderBrush = Brushes.LightGray;

                r.Cells.Add(slikaCelija);

                StackPanel idPanel = new StackPanel();
                idPanel.Orientation = Orientation.Horizontal;

                //id
                TextBlock tbId = new TextBlock();
                tbId.Text = "ID zaposlenog: ";
                TextBlock tbbId = new TextBlock();
                tbbId.Foreground = (Brush)(new BrushConverter()).ConvertFrom("#FF047AA6");
                tbbId.Text = v.doctor.Id.ToString();

                idPanel.Children.Add(tbId);
                idPanel.Children.Add(tbbId);

                //ime
                StackPanel imePanel = new StackPanel();
                imePanel.Orientation = Orientation.Horizontal;

                TextBlock tbIme = new TextBlock();
                tbIme.Text = "Ime: ";
                TextBlock tbbIme = new TextBlock();
                tbbIme.Foreground = (Brush)(new BrushConverter()).ConvertFrom("#FF047AA6");
                tbbIme.Text = v.doctor.Name;

                imePanel.Children.Add(tbIme);
                imePanel.Children.Add(tbbIme);

                //prezime
                StackPanel prezimePanel = new StackPanel();
                prezimePanel.Orientation = Orientation.Horizontal;

                TextBlock tbPrezime = new TextBlock();
                tbPrezime.Text = "Prezime: ";
                TextBlock tbbPrezime = new TextBlock();
                tbbPrezime.Foreground = (Brush)(new BrushConverter()).ConvertFrom("#FF047AA6");
                tbbPrezime.Text = v.doctor.Surname;

                prezimePanel.Children.Add(tbPrezime);
                prezimePanel.Children.Add(tbbPrezime);

                //radno mesto
                StackPanel radnoMestoPanel = new StackPanel();
                radnoMestoPanel.Orientation = Orientation.Horizontal;

                TextBlock tbRadnoMesto = new TextBlock();
                tbRadnoMesto.Text = "Radno mesto: ";
                TextBlock tbbRadnoMesto = new TextBlock();
                tbbRadnoMesto.Foreground = (Brush)(new BrushConverter()).ConvertFrom("#FF047AA6");
                tbbRadnoMesto.Text = v.doctor.WorkPlace.Name;

                radnoMestoPanel.Children.Add(tbRadnoMesto);
                radnoMestoPanel.Children.Add(tbbRadnoMesto);

                StackPanel mergeInfo = new StackPanel();
                mergeInfo.VerticalAlignment = System.Windows.VerticalAlignment.Center;
                mergeInfo.Margin = new System.Windows.Thickness(50, 0, 0, 0);

                mergeInfo.Children.Add(idPanel);
                mergeInfo.Children.Add(imePanel);
                mergeInfo.Children.Add(prezimePanel);
                mergeInfo.Children.Add(radnoMestoPanel);

                Paragraph doctorInfo = new Paragraph();
                doctorInfo.TextAlignment = System.Windows.TextAlignment.Left;
                doctorInfo.Inlines.Add(mergeInfo);

                TableCell celijaInfo = new TableCell(doctorInfo);
                celijaInfo.ColumnSpan = 1;

                celijaInfo.BorderThickness = new System.Windows.Thickness(0, 0, 0, 1);
                celijaInfo.BorderBrush = Brushes.LightGray;


                r.Cells.Add(celijaInfo);

                //Ukupno radno vreme
                StackPanel urvPanel = new StackPanel();
                urvPanel.Orientation = Orientation.Horizontal;

                TextBlock tbUrv = new TextBlock();
                tbUrv.Text = "Ukupno radno vreme: ";
                TextBlock tbbUrv = new TextBlock();
                tbbUrv.Foreground = (Brush)(new BrushConverter()).ConvertFrom("#FF047AA6");
                tbbUrv.Text = v.TotalWorkTime.ToString();

                urvPanel.Children.Add(tbUrv);
                urvPanel.Children.Add(tbbUrv);

                //Zauzeto radno vreme
                StackPanel zrvPanel = new StackPanel();
                zrvPanel.Orientation = Orientation.Horizontal;

                TextBlock tbZrv = new TextBlock();
                tbZrv.Text = "Zauzeto radno vreme: ";
                TextBlock tbbZrv = new TextBlock();
                tbbZrv.Foreground = (Brush)(new BrushConverter()).ConvertFrom("#FF047AA6");
                tbbZrv.Text = v.OccupationWorkTime.ToString();

                zrvPanel.Children.Add(tbZrv);
                zrvPanel.Children.Add(tbbZrv);

                //Zauzeto radno vreme
                StackPanel procenatPanel = new StackPanel();
                procenatPanel.Orientation = Orientation.Horizontal;

                TextBlock tbProcenat = new TextBlock();
                tbProcenat.Text = "Procenat zauzetosti: ";
                TextBlock tbbProcenat = new TextBlock();
                tbbProcenat.Foreground = (Brush)(new BrushConverter()).ConvertFrom("#FF047AA6");
                if (v.TotalWorkTime > 0)
                    tbbProcenat.Text = (((double)v.OccupationWorkTime / (double)v.TotalWorkTime) * 100).ToString("#,##0.00");
                else
                    tbbProcenat.Text = "0";

                TextBlock posto = new TextBlock();
                posto.Text = "%";
                procenatPanel.Children.Add(tbProcenat);
                procenatPanel.Children.Add(tbbProcenat);
                procenatPanel.Children.Add(posto);

                StackPanel mergeStats = new StackPanel();
                mergeStats.VerticalAlignment = System.Windows.VerticalAlignment.Center;
                mergeStats.Margin = new System.Windows.Thickness(100, 0, 0, 0);

                mergeStats.Children.Add(urvPanel);
                mergeStats.Children.Add(zrvPanel);
                mergeStats.Children.Add(procenatPanel);

                Paragraph statsInfo = new Paragraph();
                statsInfo.Inlines.Add(mergeStats);

                TableCell celijaStats = new TableCell(statsInfo);
                celijaStats.ColumnSpan = 1;

                celijaStats.BorderThickness = new System.Windows.Thickness(0, 0, 0, 1);
                celijaStats.BorderBrush = Brushes.LightGray;


                r.Cells.Add(celijaStats);

                tableRowGroup.Rows.Add(r);
            }

            table.RowGroups.Add(tableRowGroup);
            doc.Blocks.Add(table);

            PrintDialog printDialog = new PrintDialog();

            if (printDialog.ShowDialog() == true)
            {
                printDialog.PrintDocument(((IDocumentPaginatorSource)doc).DocumentPaginator, "");
            }
        }
    }
}
