namespace JeoAppardy.Client
{
  public enum StateEnum
  {
    /// <summary>
    /// Konnte nicht ermittelt werden
    /// </summary>
    Undefined = 0,
    /// <summary>
    /// Das Spiel hat begonnen, keine Spieler hat gedr�ckt
    /// </summary>
    offen = 1,
    /// <summary>
    /// Spiel ist beendet, wenigstens ein Spieler hat gedr�ckt
    /// </summary>
    beendet = 2
  }
}