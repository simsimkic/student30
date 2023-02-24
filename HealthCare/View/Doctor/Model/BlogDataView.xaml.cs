using Controller.CommunicationControllers;
using HealthCareClinic.View.Doctor.Converter;
using Model.Communication;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace HealthCareClinic.View.Doctor.Model
{
    /// <summary>
    /// Interaction logic for BlogDataView.xaml
    /// </summary>
    public partial class BlogDataView : UserControl
    {
        private readonly IArticleController _articleController;

        private ObservableCollection<UserControl> lista=  new ObservableCollection<UserControl>();

        public static ObservableCollection<UserControl> Data { get; set; }
        public BlogDataView()
        {
            InitializeComponent();
            this.DataContext = this;

            var app = Application.Current as HealthCare.App;
            _articleController = app.articleController;

            Data = new ObservableCollection<UserControl>(ArticleConverter
               .ConvertArticleListToTArticleViewList(_articleController.GetAll().ToList()));
        }



    }
}
