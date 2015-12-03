﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        static public ObservableCollection<Topic> Topics = new ObservableCollection<Topic>();
        public MainWindow()
        {
            InitializeComponent();

            Topics.Add(new Topic("Using Controls and Dialog Boxes", -1));
            Topics.Add(new Topic("Getting Started with Controls", 1));
            Topic DataGridTopic = new Topic("DataGrid", 4);
            DataGridTopic.ChildTopics.Add(
                new Topic("Default Keyboard and Mouse Behavior in the DataGrid Control", -1));
            DataGridTopic.ChildTopics.Add(
                new Topic("How to: Add a DataGrid Control to a Page", -1));
            DataGridTopic.ChildTopics.Add(
                new Topic("How to: Display and Configure Row Details in the DataGrid Control", 1));
            Topics.Add(DataGridTopic);
            myTreeView.DataContext = Topics;
          }

        public class Topic
        {
            public string Title { get; set; }
            public int Rating { get; set; }
            private ObservableCollection<Topic> childTopicsValue = new ObservableCollection<Topic>();
            public ObservableCollection<Topic> ChildTopics
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
            public Topic() { }
            public Topic(string title, int rating)
            {
                Title = title;
                Rating = rating;
            }
        }
    }


}
