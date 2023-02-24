using HCIProjekat.View.BlogView.DataView;
using System.Windows.Controls;

namespace HCIProjekat.BlogView
{
    /// <summary>
    /// Interaction logic for BlogDetails.xaml
    /// </summary>
    public partial class BlogDetails : Page
    {
        public UserControl Blog { get; set; }
        public BlogDetails(UserControl Blog)
        {
            this.Blog = Blog;
            InitializeComponent();
            if (((BlogItem)Blog).Picture != "")
                slika.Visibility = System.Windows.Visibility.Visible;
            DataContext = this;
        }
    }
}
