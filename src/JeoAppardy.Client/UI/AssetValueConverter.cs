using System;
using Windows.ApplicationModel;
using Windows.UI.Xaml.Data;
using JeoAppardy.Client.Api;

namespace JeoAppardy.Client.UI
{
  public class AssetValueConverter : IValueConverter
  {
    public object Convert(object value, Type targetType, object parameter, string language)
    {
      var answer = value as Api.Answer;
      if (answer != null)
      {
        var asset = answer.Asset;
        var installedLocation = Package.Current.InstalledLocation;
        switch (answer.Type)
        {
          case AnswerType.File:
            return $"{installedLocation.Path}\\Assets\\{asset}";
          case AnswerType.Image:
            return $"{installedLocation.Path}\\Assets\\{asset}";
          case AnswerType.Text:
          default:
            return asset;
        }
      }
      return "uuups";
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
      throw new NotImplementedException();
    }
  }
}