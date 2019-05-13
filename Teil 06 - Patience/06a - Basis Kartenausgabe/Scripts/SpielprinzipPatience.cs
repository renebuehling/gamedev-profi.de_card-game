using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpielprinzipPatience : MonoBehaviour
{
    /// <summary>
    /// Zeichner für die abgehobene Karte, bevor sie auf dem Tisch platziert wird.
    /// </summary>
    public Karte vorschauKarte;

    private void Start()
    {
        Kartenstapel stapel = GetComponent<Kartenstapel>(); //Verweis auf Kartenstapelscript
        stapel.mischeStapel();
        KlickAufStapel();
    }

    public void KlickAufStapel()
    {
        Debug.Log("Klick auf Stapel");
        Kartenstapel stapel = GetComponent<Kartenstapel>(); //Verweis auf Kartenstapelscript

        if (vorschauKarte.aktuelleBeschreibung()!=null)
        {
            stapel.legeKarteZurueck(vorschauKarte.aktuelleBeschreibung());
        }

        if (stapel.istLeer())
        {
            stapel.gameObject.SetActive(false);
        }
        else
        {
            Kartenbeschreibung gezogeneKarte = stapel.hebeObersteKarteAb(); //Karte vom Stapel abheben (nur Wert)
            vorschauKarte.setzeBeschreibung(gezogeneKarte, false);

            if (stapel.istLeer())
            {
                stapel.gameObject.SetActive(false);
            }
        }
    }

    public void LegeKarteAuf(StapelContainer container)
    {
        if(vorschauKarte.aktuelleBeschreibung()==null) //keine Karte aufgedeckt oder übrig
        {
            return;
        }

        Debug.Log("Lege die Karte auf "+container);
        Kartenstapel stapel = GetComponent<Kartenstapel>(); //Verweis auf Kartenstapelscript
        stapel.kartenContainer = container.gameObject; //dem Stapel sagen, wo erzeugeKarteInSzene die Karte unterordnen soll
        Karte karteInSzene = stapel.erzeugeKarteInSzene(); //GameObject zum Zeichnen der Karte in der Szene anlegen
        karteInSzene.setzeBeschreibung(vorschauKarte.aktuelleBeschreibung(),false); //Kartenwert und Darstellung verbinden & sofort aufdecken
        vorschauKarte.setzeBeschreibung(null, false);
        KlickAufStapel();

        container.KartenAnordnen();
    }
}
