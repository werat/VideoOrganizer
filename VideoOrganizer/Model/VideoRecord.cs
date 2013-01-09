using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace VideoOrganizer
{
   [XmlType("Video")]
   public class VideoRecord : INotifyPropertyChanged
   {
      public string FullName { get; set; }

      [XmlIgnore]
      public string Path { get { return FullName.Substring(0, FullName.LastIndexOf('\\')); } }

      [XmlIgnore]
      public string Name { get { return FullName.Substring(FullName.LastIndexOf('\\') + 1); } }

      [XmlIgnore]
      public string Extension { get { return Path.Substring(Path.LastIndexOf('.') + 1); } }

      [XmlIgnore]
      private bool _watched;

      [XmlAttribute]
      public bool Watched
      {
         get { return _watched; }
         set
         {
            _watched = value;
            if (_watched)
            {
               WatchedTime = DateTime.Now;
               NotifyPropertyChanged("WatchedTime");
            }
            NotifyPropertyChanged("Watched");
         }
      }

      [XmlAttribute]
      public DateTime WatchedTime { get; set; }

      public event PropertyChangedEventHandler PropertyChanged;

      protected void NotifyPropertyChanged(params string[] names)
      {
         var handler = PropertyChanged;
         if (handler != null)
         {
            foreach (var name in names)
            {
               handler(this, new PropertyChangedEventArgs(name));
            }
         }
      }
   }
}