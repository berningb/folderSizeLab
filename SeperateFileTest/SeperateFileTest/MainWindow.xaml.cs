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
        // creates a collection of treeefiles to bind the treeview to
        static public ObservableCollection<Treefile> files = new ObservableCollection<Treefile>();
        public MainWindow()
        {
            InitializeComponent();
            //Treefile start = new Treefile(new DirectoryInfo("C:\\Users\\Cody Clawson\\Desktop"));
            //files.Add(start);

            ////set the datacontext of our treeview to our list of tree files
            //myTreeView.DataContext = files;
        }
        public class Treefile
        {
            public string Name { get; set; }
            public decimal percentsize { get; set; }
            public long Size { get; set; }
            public Treefile parent { get; set; }
            public DirectoryInfo myDir { get; set; }
            private ObservableCollection<Treefile> childTopicsValue = new ObservableCollection<Treefile>();
            private ObservableCollection<FileInfo> childfilevalues = new ObservableCollection<FileInfo>();

            // stores all the subdirectories of this treefile
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

            // a collection of all the files in the directory
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
            //constructor takes a directory and gets its size and name and creates a new treefile for all of its subdirectories
            public Treefile(DirectoryInfo d)
            {
                Name = d.Name;
                Size = getSize(d);
                myDir = d;
                parent = this;
                this.percentsize = calculatepercent();
                foreach (var child in d.EnumerateDirectories())
                {
                    Treefile childtree = new Treefile(child);
                    ChildDirectories.Add(childtree);
                }
                foreach (var c in myDir.EnumerateFiles())
                {
                    Childfiles.Add(c);
                }
            }
            //constructor takes a directory and a parent and gets its size and name and creates a new treefile for all of its subdirectories
            // the parent is used to get the percent of parent parameter

            public Treefile(DirectoryInfo d, Treefile parent)
            {
                Name = d.Name;
                Size = getSize(d);
                myDir = d;
                this.parent = parent;
                this.percentsize = calculatepercent();
                foreach (var child in d.EnumerateDirectories())
                {
                    Treefile childtree = new Treefile(child, this);
                    ChildDirectories.Add(childtree);
                }
                foreach (var c in myDir.EnumerateFiles())
                {
                    Childfiles.Add(c);
                }
            }

            private decimal calculatepercent()
            {
                if(parent.Size != 0)
                {
                    return Math.Round((Decimal)(this.Size / parent.Size), 2);
                }
                else
                {
                    return 0 ;
                }
            }

            // get the size of all the files in the current directory and recursivelly call all the subdirectories to get their file sizes
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

        private void srchbtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Treefile start = new Treefile(new DirectoryInfo(srchbox.Text));
                files.Clear();
                files.Add(start);

                //set the datacontext of our treeview to our list of tree files
                myTreeView.DataContext = files;
            }
            catch (System.ArgumentException)
            {
                MessageBox.Show("Your directory is not a valid format");
            }
            catch (System.IO.DirectoryNotFoundException)
            {
                MessageBox.Show("That directory was not found");
            }
            catch (System.UnauthorizedAccessException)
            {
                MessageBox.Show("This program does not have access to all the files in this directory");
            }
            catch (System.IO.PathTooLongException)
            {
                MessageBox.Show("This filepath does meet the required length of < 260 characters");
            }
        }
    }


}