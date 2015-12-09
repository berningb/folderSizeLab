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
            public long parent { get; set; }
            public DirectoryInfo myDir { get; set; }
            private ObservableCollection<Treefile> childTopicsValue = new ObservableCollection<Treefile>();
            private ObservableCollection<FileInfo> childfilevalues = new ObservableCollection<FileInfo>();



            /*
            PERSONAL FEATURE: Cody Clawson
                I added the values to represent the number of files in a directory and the
                number of subdirectories a directory has. 
                This helped me learn how to do a basic bind to an object inside of a custom class
                using a template. This is not all the features i wanted to add, but we lost a lot 
                of time trying to get the basic structure to display properly
            */

            public int numberoffiles { get; set; }
            public int numberofdirectories { get; set; }


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
                numberoffiles = 0;
                numberofdirectories = 0;
                myDir = d;
                parent = this.Size;
                this.percentsize = calculatepercent();
                foreach (var child in d.EnumerateDirectories())
                {
                    this.numberofdirectories += 1;
                    Treefile childtree = new Treefile(child, this.Size);
                    ChildDirectories.Add(childtree);
                }
                foreach (var c in myDir.EnumerateFiles())
                {
                    this.numberoffiles += 1;
                    Childfiles.Add(c);
                }
            }
            //constructor takes a directory and a parent and gets its size and name and creates a new treefile for all of its subdirectories
            // the parent is used to get the percent of parent parameter

            public Treefile(DirectoryInfo d, long parent)
            {
                Name = d.Name;
                numberoffiles = 0;
                numberofdirectories = 0;
                Size = getSize(d);
                myDir = d;
                this.parent = parent;
                this.percentsize = calculatepercent();
                foreach (var child in d.EnumerateDirectories())
                {
                    this.numberofdirectories += 1;
                    Treefile childtree = new Treefile(child, this.Size);
                    ChildDirectories.Add(childtree);
                }
                foreach (var c in myDir.EnumerateFiles())
                {
                    this.numberoffiles += 1;
                    Childfiles.Add(c);
                }
            }

            private decimal calculatepercent()
            {
                if (this.Size != 0)
                {
                    //Console.WriteLine("" + this.Size + "/ " + parent + "=" + (decimal)this.Size / parent);
                    return (decimal)(parent / this.Size);
                }
                else
                {
                    return 0;
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