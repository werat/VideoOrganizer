using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using VideoOrganizer.Properties;
using VideoOrganizer.View;

namespace VideoOrganizer
{
   /// <summary>
   /// Interaction logic for MainWindow.xaml
   /// </summary>
   public partial class MainWindow : Window, IShellView
   {
      internal List<EntryRecord> Entries { get; set; }

      public MainWindow()
      {
         InitializeComponent();

         Entries = new List<EntryRecord>();
      }

      private void fileViewItem_MouseDoubleClick(object sender, RoutedEventArgs e)
      {
         var vr = (VideoRecord)fileView.SelectedItem;

         if (vr == null) return;
         if (!vr.Watched)
            vr.WatchedTime = DateTime.Now;
         vr.Watched = true;
         fileView.Items.Refresh();

         System.Diagnostics.Process.Start(vr.FullName);
      }

      public bool IsMaximized
      {
         get { return WindowState == WindowState.Maximized; }
         set
         {
            if (value)
            {
               WindowState = WindowState.Maximized;
            }
            else if (WindowState == WindowState.Maximized)
            {
               WindowState = WindowState.Normal;
            }
         }
      }
   }
}