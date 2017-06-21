using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using JeoAppardy.Client.Api;

namespace JeoAppardy.Client.UI
{
  public class AnswerTemplateSelector : DataTemplateSelector
  {
    public DataTemplate TextTemplate { get; set; }
    public DataTemplate FileTemplate { get; set; }
    public DataTemplate ImageTemplate { get; set; }

    protected override DataTemplate SelectTemplateCore(object item, DependencyObject container)
    {
      var answer = item as Api.Answer;
      if (answer != null)
      {
        switch (answer.Type)
        {
          case AnswerType.File:
            return FileTemplate;
          case AnswerType.Image:
            return ImageTemplate;
          case AnswerType.Text:
          default:
            return TextTemplate;
        }
      }
      return base.SelectTemplateCore(item, container);
    }

    protected override DataTemplate SelectTemplateCore(object item)
    {
      return base.SelectTemplateCore(item);
    }
  }
}