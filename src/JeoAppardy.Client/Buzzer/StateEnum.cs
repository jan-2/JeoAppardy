namespace JeoAppardy.Client.Buzzer
{
  public enum StateEnum
  {
    /// <summary>
    /// Konnte nicht ermittelt werden
    /// </summary>
    Undefined = 0,

    /// <summary>
    /// Das Spiel hat begonnen, keine Spieler hat gedrückt
    /// </summary>
    offen = 1,

    /// <summary>
    /// Spiel ist beendet, wenigstens ein Spieler hat gedrückt
    /// </summary>
    beendet = 2
  }
}