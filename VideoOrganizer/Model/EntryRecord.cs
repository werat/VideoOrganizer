using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace VideoOrganizer
{
   public class EntryRecord
   {
      //public string Path { get; set; }

      [XmlArray]
      public ObservableCollection<DirectoryRecord> RootDirectories { get; set; }

      public EntryRecord()
      {
         RootDirectories = new ObservableCollection<DirectoryRecord>();
      }
   }
}