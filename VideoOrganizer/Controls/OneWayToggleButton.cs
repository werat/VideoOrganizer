using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls.Primitives;

namespace VideoOrganizer.Controls
{
   public class OneWayToggleButton : ToggleButton
   {
      protected override void OnClick()
      {
         RaiseEvent(new RoutedEventArgs(ClickEvent, this));
         if (Command != null && Command.CanExecute(CommandParameter))
            Command.Execute(CommandParameter);
      }
   }
}