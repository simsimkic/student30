using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HCIProjekat.View.ConfirmationDialogsView
{
    /// <summary>
    /// Interaction logic for ConfirmDialog.xaml
    /// </summary>
    public partial class ConfirmDialog : UserControl
    {
        public ConfirmDialog(string titleDialog, string message
                            , RoutedEventHandler odustanakAkcija, RoutedEventHandler potvrdiAkcija
                            , string buttonLeftText = "Odustani", string buttonRightText = "Potvrdi")
        {
            InitializeComponent();
            titleWindow.Text = titleDialog;
            tekstDijaloga.Text = message;
            odustanak.AddHandler(Button.ClickEvent, odustanakAkcija);
            potvrda.AddHandler(Button.ClickEvent, potvrdiAkcija);
            potvrda.Content = buttonRightText;
            odustanak.Content = buttonLeftText;
        }
    }
}
