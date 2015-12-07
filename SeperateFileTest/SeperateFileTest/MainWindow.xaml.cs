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

            //Topics.Add(new Topic("Using Controls and Dialog Boxes", -1));
            //Topics.Add(new Topic("Getting Started with Controls", 1));
            //Topic DataGridTopic = new Topic("DataGrid", 4);
            //DataGridTopic.ChildTopics.Add(
            //    new Topic("Default Keyboard and Mouse Behavior in the DataGrid Control", -1));
            //DataGridTopic.ChildTopics.Add(
            //    new Topic("How to: Add a DataGrid Control to a Page", -1));
            //DataGridTopic.ChildTopics.Add(
            //    new Topic("How to: Display and Configure Row Details in the DataGrid Control", 1));
            //    DataGridTopic.ChildTopics[0].ChildTopics.Add(new Topic("Grandchild", 4));
            //Topics.Add(DataGridTopic);

            Treefile start = new Treefile(new DirectoryInfo(@"C:\wamp\www\trying"));
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

            private long getSize(DirectoryInfo d)
            {
                return 0;
            }
        }
    }


}