using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WpfGui.CustomFramework
{
    public class BindableBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        /// Used when setting property in setter, notifies if value changes
        protected virtual void SetProperty<T>(ref T member, T val, [CallerMemberName] string propertyName = null)
        {
            if (object.Equals(member, val)) return;
            member = val;
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        /// Used when we want to change the property value from outside setter, e.i. when a property value change affects another property value
        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}