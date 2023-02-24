using Model.DataReport;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace HealthCare.View.Doctor.Util
{
    public class ReviewPDFGenerator
    {

        public static void ExportAsPDF(PatientTreatmentReport report)
        {
            FlowDocument doc = new FlowDocument();

            doc.PagePadding = new System.Windows.Thickness(40);
            doc.PageWidth = 1240;
            doc.MaxPageWidth = 1240;
            doc.ColumnWidth = 1240;
            doc.PageHeight = 1754;
            doc.IsColumnWidthFlexible = false;
            doc.Background = Brushes.White;

            StackPanel naziv = new StackPanel();
            naziv.Orientation = Orientation.Vertical;
            naziv.VerticalAlignment = System.Windows.VerticalAlignment.Center;
            naziv.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;

            //logo.Height = 120;
            TextBlock tbNaziv = new TextBlock();
            tbNaziv.Text = "Izvestaj o pregledu pacijenta za odredjeni vremenski period";
            tbNaziv.FontSize = 22;
            tbNaziv.VerticalAlignment = System.Windows.VerticalAlignment.Center;
            tbNaziv.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;

            Separator separator1 = new Separator();
            separator1.Width = 1240;

            naziv.Children.Add(tbNaziv);
            naziv.Children.Add(separator1);

            Paragraph top = new Paragraph();
            top.Inlines.Add(naziv);

            doc.Blocks.Add(top);

            //// informacije 
            StackPanel informacije = new StackPanel();
            informacije.Orientation = Orientation.Vertical;

            TextBlock tbInformacije = new TextBlock();
            tbInformacije.Text += "Datum od: " + report.DateFrom.ToString("dd MMM yyyy") + "\n";
            tbInformacije.Text += "Datum do: " + report.DateTo.ToString("dd MMM yyyy");

            Separator separator2 = new Separator();
            separator2.Width = 1240;
            separator2.Margin = new System.Windows.Thickness(0, 15, 0, 0);

            informacije.Children.Add(tbInformacije);
            informacije.Children.Add(separator2);
            Paragraph top2 = new Paragraph();
            top2.Inlines.Add(informacije);

            doc.Blocks.Add(top2);

 

            foreach (var v in report.treatment)
            {
                
                Separator separator3 = new Separator();
                separator3.Width = 1240;
                separator3.Margin = new System.Windows.Thickness(0, 15, 0, 0);

                StackPanel mergeStackPanel = new StackPanel();

                ///////////////////////////////INFO O PREGLEDU///////////////////////////
                
                StackPanel treatmentInformation = new StackPanel();
                treatmentInformation.Orientation = Orientation.Vertical;
                treatmentInformation.Width = 1240;

                TextBlock tbDoctorName = new TextBlock();
                tbDoctorName.Text = "Doktor:  ";
                tbDoctorName.Text += v.Creator;

                TextBlock tbDateName = new TextBlock();
                tbDateName.Text = "Datum pregleda:  ";
                tbDateName.Text += v.Date.ToString("dd MMM yyyy");

                TextBlock tbTypeName = new TextBlock();
                tbTypeName.Text = "Tip pregleda:  ";
                tbTypeName.Text += v.TreatmentType.ToString();

                treatmentInformation.Children.Add(tbDoctorName);
                treatmentInformation.Children.Add(tbDateName);
                treatmentInformation.Children.Add(tbTypeName);



                ///////////////////////////////ANAMNEZA?/////////////////////////////////
                StackPanel anamnesisInformation = new StackPanel();
                anamnesisInformation.Margin= new System.Windows.Thickness(0, 15, 0, 0);
                anamnesisInformation.Orientation = Orientation.Horizontal;
                anamnesisInformation.Width = 1240;

                TextBlock tbAnamnesisTitle = new TextBlock();
                tbAnamnesisTitle.Text = "Anamenza";
                tbAnamnesisTitle.Width = 100;
                TextBox tbAnamnesisText = new TextBox();
                tbAnamnesisText.Margin= new System.Windows.Thickness(15, 0, 0, 0);
                tbAnamnesisText.HorizontalAlignment= System.Windows.HorizontalAlignment.Right;
                tbAnamnesisText.Height = 150;
                tbAnamnesisText.Width = 1045;
                tbAnamnesisText.TextWrapping = System.Windows.TextWrapping.Wrap;
                tbAnamnesisText.Text = v.TreatmentReport.Anamnesis;
                
                
                anamnesisInformation.Children.Add(tbAnamnesisTitle);
                anamnesisInformation.Children.Add(tbAnamnesisText);



                //////////////////////////////DIJAGNOZA/////////////////////////////////

                StackPanel diagnosisInformation = new StackPanel();
                diagnosisInformation.Margin = new System.Windows.Thickness(0, 15, 0, 0);
                diagnosisInformation.Orientation = Orientation.Horizontal;
                diagnosisInformation.Width = 1240;

                TextBlock tbDiagnosisTitle = new TextBlock();
                tbDiagnosisTitle.Text = "Dijagnoza";
                tbDiagnosisTitle.Width = 100;
                TextBox tbDiagnosisText = new TextBox();
                tbDiagnosisText.Margin = new System.Windows.Thickness(15, 0, 0, 0);
                tbDiagnosisText.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
                tbDiagnosisText.Height = 150;
                tbDiagnosisText.TextWrapping = System.Windows.TextWrapping.Wrap;
                tbDiagnosisText.Width = 1045;
                tbDiagnosisText.Text = v.TreatmentReport.Diagnosis;


                diagnosisInformation.Children.Add(tbDiagnosisTitle);
                diagnosisInformation.Children.Add(tbDiagnosisText);



                //////////////////////////////TERAPIJA/////////////////////////////////

                StackPanel theraphyInformation = new StackPanel();
                theraphyInformation.Margin = new System.Windows.Thickness(0, 15, 0, 0);
                theraphyInformation.Orientation = Orientation.Horizontal;
                theraphyInformation.Width = 1240;

                TextBlock tbTheraphyName = new TextBlock();
                tbTheraphyName.Text = "Terapija";
                tbTheraphyName.Width = 100;
                TextBox tbTheraphyText = new TextBox();
                tbTheraphyText.Margin = new System.Windows.Thickness(15, 0, 0, 0);
                tbTheraphyText.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
                tbTheraphyText.Height = 150;
                tbTheraphyText.TextWrapping = System.Windows.TextWrapping.Wrap;
                tbTheraphyText.Width = 1045;
                tbTheraphyText.Text = v.TreatmentReport.Theraphy;


                theraphyInformation.Children.Add(tbTheraphyName);
                theraphyInformation.Children.Add(tbTheraphyText);

                //////////////////////////////NAPOMENA/////////////////////////////////

                StackPanel noteInformation = new StackPanel();
                noteInformation.Margin = new System.Windows.Thickness(0, 15, 0, 0);
                noteInformation.Orientation = Orientation.Horizontal;
                noteInformation.Width = 1240;

                TextBlock tbNoteName = new TextBlock();
                tbNoteName.Text = "Napomena";
                tbNoteName.Width = 100;
                TextBox tbNoteText = new TextBox();
                tbNoteText.Margin = new System.Windows.Thickness(15, 0, 0, 0);
                tbNoteText.TextWrapping = System.Windows.TextWrapping.Wrap;
                tbNoteText.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
                tbNoteText.Height = 150;
                tbNoteText.Width = 1045;
                tbNoteText.Text = v.TreatmentReport.Note ;


                noteInformation.Children.Add(tbNoteName);
                noteInformation.Children.Add(tbNoteText);

                /////////////////////////////////MERGE/////////////////////////////////


                mergeStackPanel.Children.Add(treatmentInformation);
                mergeStackPanel.Children.Add(anamnesisInformation);
                mergeStackPanel.Children.Add(diagnosisInformation);
                mergeStackPanel.Children.Add(theraphyInformation);
                mergeStackPanel.Children.Add(noteInformation);
                mergeStackPanel.Children.Add(separator3);


                Paragraph top21 = new Paragraph();
                top21.Inlines.Add(mergeStackPanel);

                doc.Blocks.Add(top21);
             
            }


            PrintDialog printDialog = new PrintDialog();

            if (printDialog.ShowDialog() == true)
            {
                printDialog.PrintDocument(((IDocumentPaginatorSource)doc).DocumentPaginator, "");
            }
        }
    }
}
