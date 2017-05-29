﻿using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace JeoAppardy.Client.UI
{
  public abstract class ViewModel : INotifyPropertyChanged
  {
    public event PropertyChangedEventHandler PropertyChanged;

    public void RaisePropertyChanged([CallerMemberName]string propertyName = null)
    {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public void Set<T>(ref T storage, T value, [CallerMemberName]string propertyName = null)
    {
      if (!Equals(storage, value))
      {
        storage = value;
        RaisePropertyChanged(propertyName);
      }
    }
  }
}
