using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace VideoOrganizer
{
   internal class EntryRecordXmlRepository
   {
      public EntryRecord ReadEntryRecord(string databasePath = "database.xml")
      {
         if (!File.Exists(databasePath))
            return null;

         var xs = new XmlSerializer(typeof(EntryRecord));
         using (var xr = new XmlTextReader(databasePath))
         {
            return (EntryRecord)xs.Deserialize(xr);
         }
      }

      public void SaveEntryRecord(EntryRecord entry, string databasePath = "database.xml")
      {
         var xs = new XmlSerializer(typeof(EntryRecord));
         using (var xw = new XmlTextWriter(databasePath, Encoding.UTF8) { Formatting = Formatting.Indented })
         {
            xs.Serialize(xw, entry);
         }
      }
   }
}