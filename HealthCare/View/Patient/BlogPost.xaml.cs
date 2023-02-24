using Model.Communication;
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

namespace HealthCare.View.Patient
{
    /// <summary>
    /// Interaction logic for BlogPost.xaml
    /// </summary>
    public partial class BlogPost : Page
    {

        public Article BlogPostData { get; set; }

        public BlogPost(Article item)
        {
            BlogPostData = item;
            InitializeComponent();
            this.DataContext = this;
        }
    }
}
