using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace SeperateFileTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static public ObservableCollection<Treefile> files = new ObservableCollection<Treefile>();
        public MainWindow()
        {
            InitializeComponent();
            Treefile start = new Treefile(new DirectoryInfo("C:\\Users\\Cody Clawson\\Desktop"));
            files.Add(start);

            myTreeView.DataContext = files;
          }
        public class Treefile
        {
            public string Name { get; set; }
            public long Size { get; set; }
            public DirectoryInfo myDir { get; set; }
            private ObservableCollection<Treefile> childTopicsValue = new ObservableCollection<Treefile>();
            private ObservableCollection<FileInfo> childfilevalues = new ObservableCollection<FileInfo>();

            public ObservableCollection<Treefile> ChildDirectories
            {
                get
                {
                    return childTopicsValue;
                }
                set
                {
                    childTopicsValue = value;
                }
            }

            public ObservableCollection<FileInfo> Childfiles
            {
                get
                {
                    return childfilevalues;
                }
                set
                {
                    childfilevalues = value;
                }
            }
            public Treefile() { }
            public Treefile(DirectoryInfo d)
            {
                Name = d.Name;
                Size = getSize(d);
                myDir = d;

                foreach(var child in d.EnumerateDirectories()) {
                    Treefile childtree = new Treefile(child);
                    ChildDirectories.Add(childtree);
                }
                foreach (var c in myDir.EnumerateFiles())
                {
                    Childfiles.Add(c);
                }
            }

            public static long getSize(DirectoryInfo d)
            {
                long Size = 0;
             
                foreach (FileInfo fi in d.GetFiles())
                {
                    Size += fi.Length;
                }
                foreach (DirectoryInfo di in d.GetDirectories())
                {
                    Size += getSize(di);
                }
                return (Size);
            }
        }
    }


}