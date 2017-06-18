using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace JeoAppardy.Client.Api
{
  [JsonConverter(typeof(StringEnumConverter))]
  public enum AnswerType
  {
    [EnumMember(Value = "text")]
    Text,
    [EnumMember(Value = "file")]
    File,
    [EnumMember(Value = "image")]
    Image
  }
}