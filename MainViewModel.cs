using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using KeyboardDoubleClickFinder.Annotations;

namespace KeyboardDoubleClickFinder
{


    public static class ObservableCollectionExtensions
    {
        public static void AddSorted<T>(this IList<T> list, T item, IComparer<T> comparer = null)
        {
            if (comparer == null)
                comparer = Comparer<T>.Default;

            int i = 0;
            while (i < list.Count && comparer.Compare(list[i], item) < 0)
                i++;

            list.Insert(i, item);
        }
    }


    public class MainViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<KeyViewModel> Keys { get; } = new();

        private Dictionary<Key, KeyViewModel> _dict = new();

        public MainViewModel()
        {
            /*foreach (var key in Enum.GetValues<Key>() )
            {
                HandleKey(key);
            }*/
        }

        public void Handler(KeyEventArgs e)
        {

            if (!e.IsRepeat)
            {
                HandleKey(e.Key);
            }
        }

        private void HandleKey(Key key)
        {
            if (_dict.ContainsKey(key))
            {
                _dict[key].Pressed();
            }
            else
            {
                KeyViewModel vm = new() { Key = key };
                Keys.AddSorted(vm);
                _dict[key] = vm;
            }
        }

        public void Reset()
        {
            foreach (var keyViewModel in Keys)
            {
                keyViewModel.Reset();
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
