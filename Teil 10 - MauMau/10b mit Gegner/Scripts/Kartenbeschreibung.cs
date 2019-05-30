using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Enthält die inhaltlichen Daten einer Karte.
/// Die Darstellung, Interaktion, usw. in der Programmoberfläche
/// geschieht über Karte.cs.
/// </summary>
[System.Serializable]
public class Kartenbeschreibung
{
    /// <summary>
    /// Zeiger auf das Bild der umgedrehten Karte.
    /// </summary>
    public Sprite kartenbild;

    /// <summary>
    /// Wert der Karte (10, 9, ...) zum Vergleichen von Karten miteinander.
    /// </summary>
    public int Wert=0;

    /// <summary>
    /// Codiert die Farbe der Karte (Pik, Kreuz, Karo, Herz, ...) als Zahl, anhand derer
    /// sich die Farben zweier Karten leicht vergleichen lassen. 
    /// </summary>
    public int Farbe = 0;

    public override string ToString()
    {
        return base.ToString()+"[Wert="+Wert+", Farbe="+Farbe+"]";
    }
}
