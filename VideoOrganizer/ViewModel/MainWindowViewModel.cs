using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using VideoOrganizer.Properties;
using VideoOrganizer.View;

namespace VideoOrganizer.ViewModel
{
   internal class MainWindowViewModel : ViewModelBase<IShellView>
   {
      private UnitOfWork unitOfWork;

      public EntryRecord Entry { get; set; }

      private DirectoryRecord selectedDirectory;

      public DirectoryRecord SelectedDirectory
      {
         get { return selectedDirectory; }
         set { SetProperty(ref selectedDirectory, value, "SelectedDirectory"); }
      }

      public ICommand OpenFolder { get; private set; }

      public MainWindowViewModel(IShellView view, UnitOfWork unitOfWork)
         : base(view)
      {
         this.unitOfWork = unitOfWork;
         Entry = unitOfWork.GetEntry();

         OpenFolder = new DelegateCommand(
            () =>
            {
               System.Diagnostics.Process.Start(SelectedDirectory.FullName);
            },
            () => SelectedDirectory != null);

         ViewCore.Closed += new EventHandler(ViewCore_Closed);

         // Restore the window size when the values are valid.
         if (Settings.Default.Left >= 0 && Settings.Default.Top >= 0
             && Settings.Default.Width > 0 && Settings.Default.Height > 0
             && Settings.Default.Left + Settings.Default.Width <= SystemParameters.VirtualScreenWidth
             && Settings.Default.Top + Settings.Default.Height <= SystemParameters.VirtualScreenHeight)
         {
            ViewCore.Left = Settings.Default.Left;
            ViewCore.Top = Settings.Default.Top;
            ViewCore.Height = Settings.Default.Height;
            ViewCore.Width = Settings.Default.Width;
         }
         ViewCore.IsMaximized = Settings.Default.IsMaximized;
      }

      public void Show()
      {
         ViewCore.Show();
      }

      public void Close()
      {
         unitOfWork.SaveEntry(Entry);
         ViewCore.Close();
      }

      private void ViewCore_Closed(object sender, EventArgs e)
      {
         Settings.Default.Left = ViewCore.Left;
         Settings.Default.Top = ViewCore.Top;
         Settings.Default.Height = ViewCore.Height;
         Settings.Default.Width = ViewCore.Width;
         Settings.Default.IsMaximized = ViewCore.IsMaximized;
         Settings.Default.Save();
      }
   }
}