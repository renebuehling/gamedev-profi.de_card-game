using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Erweiterungsmodul für SpielprinzipPatience, 
/// wird von dort aus aufgerufen, wenn nötig.
/// </summary>
public class SpielprinzipPatienceHarfe : MonoBehaviour
{
    public List<StapelContainer> spalten; //als feld, damit Reihenfolge zuverlässig vorliegt

    public bool testModus = false;

    private void LegeAlle(int mitDerFarbe, Kartenstapel ausDemStapel, int inDieSpalteIndex)
    {
        for(int wert=14; wert >= 2; wert--)
        {
            Kartenbeschreibung k = ausDemStapel.hebeKarteAb(wert,mitDerFarbe);
            ausDemStapel.kartenContainer = spalten[inDieSpalteIndex].gameObject;
            Karte karteInSzene = ausDemStapel.erzeugeKarteInSzene();
            karteInSzene.setzeBeschreibung(k, false);
        }
        spalten[inDieSpalteIndex].KartenAnordnen();
    }

    public void ErweiterungStarten(Kartenstapel stapel)
    {
        if (testModus)
        {
            LegeAlle(1, stapel, 0);
            LegeAlle(2, stapel, 1);
            LegeAlle(13, stapel, 2);
            LegeAlle(14, stapel, 3);
        }
        else
        {
            //Spalten mit einer Auswahl zufälliger Karten befüllen
            for (int i = 0; i < spalten.Count; i++)
            {
                for (int bereitsErzeugt = 0; bereitsErzeugt <= i; bereitsErzeugt++)
                {
                    Kartenbeschreibung k = stapel.hebeObersteKarteAb();
                    stapel.kartenContainer = spalten[i].gameObject;
                    Karte karteInSzene = stapel.erzeugeKarteInSzene();
                    karteInSzene.setzeBeschreibung(k, false);
                    if (bereitsErzeugt < i) karteInSzene.TauscheKartenbild(); //für alle außer letzter Karte
                    spalten[i].KartenAnordnen();
                }
            }
        }
    }

    public void PruefeObGewonnen()
    {
        int kinderGesamt = 0;
        foreach(StapelContainer spalte in spalten)
        {
            kinderGesamt += spalte.transform.childCount;
            if (kinderGesamt > 1) return;
        }

        SpielprinzipPatience sp = GetComponent<SpielprinzipPatience>();
        sp.dialogGewonnen.SetActive(true);
    }
}
