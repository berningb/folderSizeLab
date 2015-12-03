using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileTree
{
    class TreeFile
    {



        public TreeFile(DirectoryInfo d)
        {
            this.directory = d;
            this.name = d.Name;
            this.size = getsize(d);
        }

        private String name;

        public String Name
        {
            get { return name; }
            set { name = value; }
        }

        private long size;

        public long Size
        {
            get { return size; }
            set { size = value; }
        }

        private DirectoryInfo directory;

        public DirectoryInfo Directory
        {
            get { return directory; }
            set { directory = value; }
        }


        public long getsize(DirectoryInfo d)
        {

            long size = 0;

            foreach (FileInfo file in d.GetFiles("*", SearchOption.AllDirectories))
            {
                size += file.Length;
            }


            return size;

        }
    }
}