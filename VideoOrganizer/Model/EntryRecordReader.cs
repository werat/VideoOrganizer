using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace VideoOrganizer
{
   internal class EntryRecordReader
   {
      private DirectoryRecordReader reader = new DirectoryRecordReader();

      public EntryRecord Read(string[] paths)
      {
         var entry = new EntryRecord();
         foreach (var path in paths)
         {
            if (!Directory.Exists(path)) continue;

            var record = reader.Read(new DirectoryInfo(path));
            entry.RootDirectories.Add(record);
         }
         return entry;
      }
   }
}