using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpielprinzipPatience : MonoBehaviour
{
    /// <summary>
    /// Zeichner für die abgehobene Karte, bevor sie auf dem Tisch platziert wird.
    /// </summary>
    public Karte vorschauKarte;

    public GameObject dialogGewonnen;
    public GameObject dialogFehler;

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

    private bool WertPasst(Kartenbeschreibung alteKarte, Kartenbeschreibung neueKarte)
    {
        if (alteKarte == null) return (neueKarte.Wert == 14);
        else if (alteKarte.Wert - neueKarte.Wert == 1) return true;
        else return false;
    }

    private bool FarbePasst(Kartenbeschreibung alteKarte, Kartenbeschreibung neueKarte)
    {
        if (alteKarte == null) return true;
        else if (Mathf.Abs(alteKarte.Farbe-neueKarte.Farbe) > 10) return true;
        else return false;
    }

    public void LegeKarteAuf(StapelContainer container)
    {
        if (vorschauKarte.aktuelleBeschreibung() == null) //keine Karte aufgedeckt oder übrig
        {
            return;
        }
        Kartenbeschreibung neueKarte = vorschauKarte.aktuelleBeschreibung();

        if (LegeKarteAuf(container, neueKarte))
        {
            vorschauKarte.setzeBeschreibung(null, false);
            KlickAufStapel();
            Kartenstapel stapel = GetComponent<Kartenstapel>(); //Verweis auf Kartenstapelscript
            if (stapel.istLeer() && vorschauKarte.aktuelleBeschreibung() == null) //alle karten abgelegt
                dialogGewonnen.SetActive(true);
        }        
    }

    public bool LegeKarteAuf(StapelContainer container, Kartenbeschreibung neueKarte)
    { 
        Kartenbeschreibung alteKarte = container.ObersteKarte();
        
        if (WertPasst(alteKarte, neueKarte) && FarbePasst(alteKarte,neueKarte))
        {
            Debug.Log("Lege die Karte auf " + container);
            Kartenstapel stapel = GetComponent<Kartenstapel>(); //Verweis auf Kartenstapelscript
            stapel.kartenContainer = container.gameObject; //dem Stapel sagen, wo erzeugeKarteInSzene die Karte unterordnen soll
            Karte karteInSzene = stapel.erzeugeKarteInSzene(); //GameObject zum Zeichnen der Karte in der Szene anlegen
            karteInSzene.setzeBeschreibung(neueKarte, false); //Kartenwert und Darstellung verbinden & sofort aufdecken
            
            container.KartenAnordnen();
            return true; //Karte passt und wurde verschoben
        }
        else //passt nicht 
        {
            dialogFehler.SetActive(true);
            return false; //Karte passt nicht, nichts weiter machen
        }
    }
}
