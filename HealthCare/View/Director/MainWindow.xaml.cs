using Controller.UserControllers;
using HCIProjekat.BlogView;
using HCIProjekat.LekoviView;
using HCIProjekat.OpremaView;
using HCIProjekat.ProstorijeView;
using HCIProjekat.RenoviranjaView;
using HCIProjekat.View.ConfirmationDialogsView;
using HCIProjekat.View.EmployeesFeedbackView;
using HCIProjekat.View.HelpWizardView;
using HCIProjekat.View.ObavestenjeView;
using HCIProjekat.View.ReportView;
using HCIProjekat.View.SoftwareFeedbackView;
using HCIProjekat.ZaposleniView;
using HealthCare;
using Model.Users;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace HCIProjekat
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static MainWindow AppWindow;
        public static List<Page> undoPages;
        public static bool APP_HELP = false;
        public static string paramethers = @"../../View/Director/params.txt";
        public MainWindow()
        {
            //za Dark temu
            //Application.Current.Resources["bela"] = (SolidColorBrush)(new BrushConverter().ConvertFrom("#121212"));
            string line= File.ReadAllText(paramethers);
            if (line == "1")
                APP_HELP = true;
            else
                APP_HELP = false;

            InitializeComponent();

            undoPages = new List<Page>();
            mainPage main = new mainPage();
            navigateWithTitleChange(main, false);
            AppWindow = this;

            if (APP_HELP)
            {
                HelpFrame.Visibility = Visibility.Visible;
                HelpFrame.Navigate(new FirstHelpPage());
            }

            setToggleButton();
        }

        private void setToggleButton()
        {
            if (APP_HELP)
            {
                Bu.Back.Fill = Bu.On;
                Bu.Toggled1 = true;
                Bu.Dot.Margin = Bu.RightSide;
            }
            else
            {
                Bu.Back.Fill = Bu.Off;
                Bu.Toggled1 = false;
                Bu.Dot.Margin = Bu.LeftSide;
            }
        }

        public void navigateWithTitleChange(Page page, bool eventClose)
        {
            if (eventClose)
                closeMenuAnimation();

            MainFrame.Navigate(page);
            undoPages.Add(page);
            setMainTitle(page.Title);
            setOpenAndCloseMenuButtonVisibility();

        }

        private void closeMenuAnimation()
        {
            HelpFrame.Visibility = Visibility.Collapsed;
            CloseMenu.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
        }

        private void setMainTitle(string title)
        {
            textNaslov.Text = title;
            if (textNaslov.Text.Length > 17)
                textNaslov.FontSize = 25;
            else
                textNaslov.FontSize = 30;
        }

        private void setOpenAndCloseMenuButtonVisibility()
        {
            if (undoPages.Count == 1)
            {
                ButtonCloseMenu.Visibility = Visibility.Collapsed;
                ButtonOpenMenu.Visibility = Visibility.Visible;
            }
            else
            {
                ButtonCloseMenu.Visibility = Visibility.Visible;
                ButtonOpenMenu.Visibility = Visibility.Collapsed;
            }
        }

        public void navigateToRootPage(Page page, bool eventClose)
        {
            undoPages.Clear();
            navigateWithTitleChange(page, eventClose);
        }

        public void navigatePrevious(Page page)
        {
            MainFrame.Navigate(page);
            setMainTitle(page.Title);
        }

        private void ButtonOpenMenu_Click(object sender, RoutedEventArgs e)
        {
            if (APP_HELP)
            {
                HelpFrame.Visibility = Visibility.Visible;
                HelpFrame.Navigate(new MenuItemHelp());
            }
        }

        private void ButtonCloseMenu_Click(object sender, RoutedEventArgs e)
        {
            int brojStrana = undoPages.Count;
            if (brojStrana > 1)
            {
                navigatePrevious(undoPages[brojStrana - 2]);
                undoPages.RemoveAt(brojStrana - 1);
                setOpenAndCloseMenuButtonVisibility();
            }

        }

        private void buttonUpravnik_Click(object sender, RoutedEventArgs e)
        {
            navigateWithTitleChange(new Upravnik(), false);
        }

        private void Button_Izvestaj_Click(object sender, RoutedEventArgs e)
        {
            if (PodmeniIzvestaj.Visibility == Visibility.Collapsed)
            {
                reportSumbenuVisibilityChange(Visibility.Visible);
            }
            else
            {
                reportSumbenuVisibilityChange(Visibility.Collapsed);
            }
        }

        private void Button_FeedBack_Click(object sender, RoutedEventArgs e)
        {
            if (PodmeniPovratneInformacije.Visibility == Visibility.Collapsed)
            {
                feedbackSumbenuVisibilityChange(Visibility.Visible);
            }
            else
            {
                feedbackSumbenuVisibilityChange(Visibility.Collapsed);
            }
        }

        private void reportSumbenuVisibilityChange(Visibility state)
        {
            PodmeniIzvestaj.Visibility = state;
            if (state == Visibility.Visible)
                IzvestajDownArrow.Visibility = Visibility.Collapsed;
            else
                IzvestajDownArrow.Visibility = Visibility.Visible;

            IzvestajUpArrow.Visibility = state;
        }

        private void feedbackSumbenuVisibilityChange(Visibility state)
        {
            PodmeniPovratneInformacije.Visibility = state;
            if (state == Visibility.Visible)
                FeedbackDownArrow.Visibility = Visibility.Collapsed;
            else
                FeedbackDownArrow.Visibility = Visibility.Visible;
            FeedbackUpArrow.Visibility = state;
        }

        private void Button_Zaposleni_Click(object sender, RoutedEventArgs e)
        {
            HelpFrame.Visibility = Visibility.Collapsed;
            navigateToRootPage(new Zaposleni(), true);
            if (APP_HELP)
            {
                HelpFrame.Visibility = Visibility.Visible;
                HelpFrame.Navigate(new EntityMenu());
            }
        }

        private void Home_Click(object sender, RoutedEventArgs e)
        {
            navigateToRootPage(new mainPage(), true);
            HelpFrame.Visibility = Visibility.Collapsed;
        }

        private void Lekovi_Click(object sender, RoutedEventArgs e)
        {
            HelpFrame.Visibility = Visibility.Collapsed;
            navigateToRootPage(new Lekovi(), true);
            if (APP_HELP)
            {
                HelpFrame.Visibility = Visibility.Visible;
                HelpFrame.Navigate(new EntityMenu());
            }
        }

        private void Prostorija_Click(object sender, RoutedEventArgs e)
        {
            HelpFrame.Visibility = Visibility.Collapsed;
            navigateToRootPage(new Prostorije(), true);
            if (APP_HELP)
            {
                HelpFrame.Visibility = Visibility.Visible;
                HelpFrame.Navigate(new EntityMenu());
            }
        }

        private void Renoviranja_Click(object sender, RoutedEventArgs e)
        {
            HelpFrame.Visibility = Visibility.Collapsed;
            navigateToRootPage(new Renoviranja(), true);
            if (APP_HELP)
            {
                HelpFrame.Visibility = Visibility.Visible;
                HelpFrame.Navigate(new EntityMenu());
            }

        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            CloseMenu.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
        }

        private void Oprema_Click(object sender, RoutedEventArgs e)
        {
            HelpFrame.Visibility = Visibility.Collapsed;
            navigateToRootPage(new Oprema(), true);
            if (APP_HELP)
            {
                HelpFrame.Visibility = Visibility.Visible;
                HelpFrame.Navigate(new EntityMenu());
            }
        }

        private void Blog_Click(object sender, RoutedEventArgs e)
        {
            HelpFrame.Visibility = Visibility.Collapsed;
            navigateToRootPage(new Blog(), true);
        }

        private void obavestenja_Click(object sender, RoutedEventArgs e)
        {
            HelpFrame.Visibility = Visibility.Collapsed;
            navigateToRootPage(new Obavestenje(), false);
        }

        private void odjavaEvent(object sender, RoutedEventArgs e)
        {
            File.WriteAllText(paramethers, (APP_HELP) ? "1" : "0");
            ResolveView.MainWindow window = (ResolveView.MainWindow)Application.Current.MainWindow;
            (Application.Current as App)._currentUser = null;
            window.CurrentUser = null;
            Application.Current.MainWindow.Show();
            Close();
        }
        private void odustanakEvent(object sender, RoutedEventArgs e)
        {
            dialog.Visibility = Visibility.Collapsed;
        }
        private void odjava_Click(object sender, RoutedEventArgs e)
        {
            dialog.Children.Add(new ConfirmDialog("Odjava",
                            "Da li ste sigurni da zelite da se odjavite?",
                            new RoutedEventHandler(odustanakEvent),
                            new RoutedEventHandler(odjavaEvent)));
            dialog.Visibility = Visibility.Visible;
        }

        private void SoftwareFeedback_Click(object sender, RoutedEventArgs e)
        {
            navigateToRootPage(new SoftwareFeedback(), true);
        }

        private void EmployeesFeedback_Click(object sender, RoutedEventArgs e)
        {
            navigateToRootPage(new EmployeesFeedback(), true);
        }

        private void zauzetostLekar_Click(object sender, RoutedEventArgs e)
        {
            navigateWithTitleChange(new FilterZauzetostiLekara(), true);
        }

        private void CloseMenu_Click(object sender, RoutedEventArgs e)
        {
            reportSumbenuVisibilityChange(Visibility.Collapsed);
            feedbackSumbenuVisibilityChange(Visibility.Collapsed);
        }
        private void pomoc_Click(object sender, RoutedEventArgs e)
        {
            if (!Bu.Toggled1)
            {
                Bu.Back.Fill = Bu.On;
                Bu.Toggled1 = true;
                Bu.Dot.Margin = Bu.RightSide;
                APP_HELP = true;
            }
            else
            {
                APP_HELP = false;
                Bu.Back.Fill = Bu.Off;
                Bu.Toggled1 = false;
                Bu.Dot.Margin = Bu.LeftSide;

            }


            Page currentPage = undoPages[undoPages.Count - 1];
            if (APP_HELP)
            {
                if (currentPage.GetType().Equals(typeof(mainPage)))
                {
                    HelpFrame.Visibility = Visibility.Visible;
                    HelpFrame.Navigate(new FirstHelpPage());
                }
                else if (currentPage.GetType().Equals(typeof(Lekovi)) || currentPage.GetType().Equals(typeof(Zaposleni)) || currentPage.GetType().Equals(typeof(Oprema)) ||
                           currentPage.GetType().Equals(typeof(Prostorije)) || currentPage.GetType().Equals(typeof(Renoviranja)))
                {
                    HelpFrame.Visibility = Visibility.Visible;
                    HelpFrame.Navigate(new EntityMenu());
                }
                else if (currentPage.GetType().Equals(typeof(RenoviranjaAddKalendar)))
                {
                    HelpFrame.Visibility = Visibility.Visible;
                    HelpFrame.Navigate(new CalendarHelpRenoviranje());
                }
                else if (currentPage.GetType().Equals(typeof(RadnoVreme)))
                {
                    HelpFrame.Visibility = Visibility.Visible;
                    HelpFrame.Navigate(new CalendarHelp());
                }
                else if (currentPage.GetType().Equals(typeof(ZaposleniOdsustva)))
                {
                    HelpFrame.Visibility = Visibility.Visible;
                    HelpFrame.Navigate(new AbsenceHelpFirst());
                }
            }
        }

        private void pomocFunkc_Click(object sender, RoutedEventArgs e)
        {
            navigateToRootPage(new HelpStartPage(), false);
        }

        private void PopupBox_Opened(object sender, RoutedEventArgs e)
        {
            HelpFrame.Visibility = Visibility.Collapsed;
        }
    }
}
