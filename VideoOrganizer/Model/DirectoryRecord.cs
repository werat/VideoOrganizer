using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using VideoOrganizer.Properties;

namespace VideoOrganizer
{
   [XmlType("Directory")]
   public class DirectoryRecord
   {
      [XmlAttribute]
      public string FullName { get; set; }

      [XmlIgnore]
      public string Name { get { return FullName.Substring(FullName.LastIndexOf('\\') + 1); } }

      [XmlArray]
      public List<DirectoryRecord> Directories { get; set; }

      [XmlArray]
      public List<VideoRecord> Videos { get; set; }

      [XmlIgnore]
      public bool Empty { get { return !Videos.Any() && !Directories.Any(dr => !dr.Empty); } }

      [XmlAttribute]
      public bool IsExpanded { get; set; }

      [XmlAttribute]
      public bool IsSelected { get; set; }

      public DirectoryRecord()
      {
         Directories = new List<DirectoryRecord>();
         Videos = new List<VideoRecord>();
      }
   }
}