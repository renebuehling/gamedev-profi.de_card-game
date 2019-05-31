using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MauMauPartei : MonoBehaviour
{
    public SpielprinzipMauMau spielprinzip;

    public string anzeigename;

    public virtual void WennKarteHinzugefuegtWurde(Karte karteInSzene)
    {
        // Definiert durch Unterklassen
        KartenAnordnen();
    }

    public virtual void RundeStarten()
    {
        Debug.Log(gameObject.name+" startet die Runde.");
    }

    public virtual void RundeBeenden()
    {
        Debug.Log(gameObject.name + " beendet die Runde.");
        spielprinzip.NaechsterSpieler();
    }

    private void KartenAnordnen()
    {
        float breite = 125; //gewünschte Kartengröße
        float hoehe = 180;  //gewünschte Kartengröße
        float halbeBreite = breite / 2;
        float halbeHoehe = hoehe / 2; 
        for (int i = 0; i < transform.childCount; i++)
        {
            RectTransform rt = transform.GetChild(i).GetComponent<RectTransform>();
            rt.anchorMin = new Vector2(0, 1);
            rt.anchorMax = new Vector2(0, 1);
            rt.sizeDelta = new Vector2(breite, hoehe);
            rt.anchoredPosition = new Vector2(
                           (i * halbeBreite) + halbeBreite, //i*halbeBreite, um die Karten halb zu versetzen;+halbeBreite weil Pivotpunkt im Kartenmittelpunkt liegt
                           -halbeHoehe // "-" weil vertikale Koordinaten nach unten gehen; halbe Höhe, weil Pivotpunkt im Kartenmittelpunkt liegt
                           ); 
        }
    }
}
