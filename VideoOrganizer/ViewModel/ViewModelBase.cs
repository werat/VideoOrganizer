using System.Collections.Generic;
using System.ComponentModel;

namespace VideoOrganizer.ViewModel
{
   public abstract class ViewModelBase<TView> : INotifyPropertyChanged
   {
      private readonly TView view;

      /// <summary>
      /// Initializes a new instance of the <see cref="ViewModel&lt;TView&gt;"/> class and
      /// attaches itself as <c>DataContext</c> to the view.
      /// </summary>
      /// <param name="view">The view.</param>
      protected ViewModelBase(TView view)
      {
         this.view = view;
      }

      /// <summary>
      /// Gets the associated view as specified view type.
      /// </summary>
      /// <remarks>
      /// Use this property in a ViewModel class to avoid casting.
      /// </remarks>
      protected TView ViewCore { get { return view; } }

      public event PropertyChangedEventHandler PropertyChanged;

      protected void SetProperty<T>(ref T field, T value, string name)
      {
         if (!EqualityComparer<T>.Default.Equals(field, value))
         {
            field = value;
            var handler = PropertyChanged;
            if (handler != null)
            {
               handler(this, new PropertyChangedEventArgs(name));
            }
         }
      }
   }
}