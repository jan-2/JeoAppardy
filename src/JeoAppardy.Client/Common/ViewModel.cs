﻿using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace JeoAppardy.Client.Common
{
  public abstract class Notifyable : INotifyPropertyChanged
  {
    public event PropertyChangedEventHandler PropertyChanged;

    public void RaisePropertyChanged([CallerMemberName] string propertyName = null)
    {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public void Set<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
    {
      if (!Equals(storage, value))
      {
        storage = value;
        RaisePropertyChanged(propertyName);
      }
    }
  }

  public class ViewModel : Notifyable
  {
  }
}