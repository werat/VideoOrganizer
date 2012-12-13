using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace VideoOrganizer
{
   [XmlType("Video")]
   public class VideoRecord
   {
      public string FullName { get; set; }

      [XmlIgnore]
      public string Path { get { return FullName.Substring(0, FullName.LastIndexOf('\\')); } }

      [XmlIgnore]
      public string Name { get { return FullName.Substring(FullName.LastIndexOf('\\') + 1); } }

      [XmlIgnore]
      public string Extension { get { return Path.Substring(Path.LastIndexOf('.') + 1); } }

      [XmlAttribute]
      public bool Watched { get; set; }

      [XmlAttribute]
      public DateTime WatchedTime { get; set; }
   }
}