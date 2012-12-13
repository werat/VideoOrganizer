using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using VideoOrganizer.Properties;

namespace VideoOrganizer
{
   internal class UnitOfWork
   {
      private EntryRecordReader reader = new EntryRecordReader();

      public EntryRecord GetEntry()
      {
         var paths = Settings.Default.Paths.Cast<string>().ToArray();
         var entry = reader.Read(paths);
         ValidateEntry(entry);
         return entry;
      }

      public void SaveEntry(EntryRecord entry)
      {
         var repository = new EntryRecordXmlRepository();
         repository.SaveEntryRecord(entry);
      }

      private void ValidateEntry(EntryRecord entry)
      {
         var repository = new EntryRecordXmlRepository();
         var xmlEntry = repository.ReadEntryRecord();
         if (xmlEntry != null)
         {
            foreach (var directory in entry.RootDirectories)
            {
               var storedDirectory = xmlEntry.RootDirectories.FirstOrDefault(dr => dr.FullName == directory.FullName);
               if (storedDirectory != null)
                  ValidateDirectory(directory, storedDirectory);
            }
         }
      }

      private void ValidateDirectory(DirectoryRecord validated, DirectoryRecord info)
      {
         validated.IsExpanded = info.IsExpanded;
         validated.IsSelected = info.IsSelected;
         foreach (var videoInfo in validated.Videos)
         {
            var storedInfo = info.Videos.FirstOrDefault(vr => vr.FullName == videoInfo.FullName);
            if (storedInfo != null)
            {
               videoInfo.Watched = storedInfo.Watched;
               videoInfo.WatchedTime = storedInfo.WatchedTime;
            }
         }
         foreach (var directoryInfo in validated.Directories)
         {
            var storedDirectory = info.Directories.FirstOrDefault(dr => dr.FullName == directoryInfo.FullName);
            if (storedDirectory != null)
               ValidateDirectory(directoryInfo, storedDirectory);
         }
      }
   }
}