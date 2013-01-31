using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Xml.Serialization;
using VideoOrganizer.Properties;

namespace VideoOrganizer
{
   internal class DirectoryRecordReader
   {
      public List<string> AllowedExtensions { get; private set; }

      public DirectoryRecordReader()
      {
         AllowedExtensions = new List<string>(Settings.Default.AllowedExtensions.Cast<string>());
      }

      public DirectoryRecordReader(params string[] extensions)
      {
         AllowedExtensions = new List<string>(extensions);
      }

      public DirectoryRecord Read(DirectoryInfo di)
      {
         if (!di.Exists) throw new DirectoryNotFoundException();

         var dr = new DirectoryRecord() { FullName = di.FullName };

         foreach (var file in di.GetFiles())
         {
            if (AllowedExtensions.Contains(file.Extension))
            {
               var vi = new VideoRecord() { FullName = file.FullName, };
               dr.Videos.Add(vi);
            }
         }
         dr.Videos.Sort((left, right) => left.Name.CompareTo(right.Name));

         foreach (var subdirectory in di.GetDirectories())
         {
            var subdr = Read(subdirectory);
            if (!subdr.Empty)
            {
               dr.Directories.Add(subdr);
            }
         }
         dr.Directories.Sort((left, right) => left.FullName.CompareTo(right.FullName));
         return dr;
      }
   }
}