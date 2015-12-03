using System;
using System.Collections.Generic;
using System.IO;
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

namespace FileTree
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();


            
            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            System.Windows.Data.CollectionViewSource treeFileViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("treeFileViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // treeFileViewSource.Source = [generic data source]


            DirectoryInfo d = new DirectoryInfo(@"C:\Users\Cody Clawson\Desktop\Folder Size Lab");

            List<TreeFile> files = new List<TreeFile>();

            files.Add(new TreeFile(d));
            treeFileViewSource.Source = files;

        }

    }
}
