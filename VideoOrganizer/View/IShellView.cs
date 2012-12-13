using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace VideoOrganizer.View
{
   public interface IShellView
   {
      double Left { get; set; }

      double Top { get; set; }

      double Width { get; set; }

      double Height { get; set; }

      bool IsMaximized { get; set; }

      event CancelEventHandler Closing;

      event EventHandler Closed;

      void Show();

      void Close();
   }
}