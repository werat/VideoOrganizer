using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using VideoOrganizer.ViewModel;

namespace VideoOrganizer
{
   /// <summary>
   /// Interaction logic for App.xaml
   /// </summary>
   public partial class App : Application
   {
      private UnitOfWork unitOfWork;
      private MainWindowViewModel mainViewModel;

      protected override void OnStartup(StartupEventArgs e)
      {
         base.OnStartup(e);

         unitOfWork = new UnitOfWork();

         var view = new MainWindow();
         mainViewModel = new MainWindowViewModel(view, unitOfWork);
         view.DataContext = mainViewModel;
         mainViewModel.Show();
      }

      protected override void OnExit(ExitEventArgs e)
      {
         //VideoDatabase.Save();
         mainViewModel.Close();
         base.OnExit(e);
      }
   }
}