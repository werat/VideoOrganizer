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
using VideoOrganizer.ViewModel;

namespace VideoOrganizer
{
   /// <summary>
   /// Interaction logic for MainWindow.xaml
   /// </summary>
   public partial class MainWindow : Window, IShellView
   {
      public MainWindow()
      {
         InitializeComponent();
      }

      private void fileViewItem_MouseDoubleClick(object sender, RoutedEventArgs e)
      {
         PlayVideo();
      }

      public bool IsMaximized
      {
         get
         {
            return WindowState == WindowState.Maximized;
         }
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

      private void fileView_PreviewKeyDown(object sender, KeyEventArgs e)
      {
         var list = (ListBox)sender;
         if (e.Key == Key.Enter && list.SelectedItem != null)
         {
            PlayVideo();

            return;
         }

         if (e.Key == Key.Down && list.SelectedIndex == list.Items.Count - 1)
         {
            //.Next will navigate to the first item in the list rather than skip to the next control. We will use Down or Right.
            //this.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));

            this.MoveFocus(new TraversalRequest(FocusNavigationDirection.Down));
            return;
         }

         base.OnKeyUp(e);
      }

      private void PlayVideo()
      {
         var vr = (VideoRecord)fileView.SelectedItem;
         if (vr == null) return;

         vr.Watched = true;
         fileView.Items.Refresh();

         System.Diagnostics.Process.Start(vr.FullName);
      }

      private void directoryView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
      {
         var model = DataContext as MainWindowViewModel;
         if (model != null)
         {
            model.SelectedDirectory = (DirectoryRecord)e.NewValue;
         }
      }
   }
}