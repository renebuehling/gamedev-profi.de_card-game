using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SpielprinzipPatienceEndablage : MonoBehaviour, IDropHandler
{
    private bool passt(Kartenbeschreibung neueKarte)
    {
        StapelContainer endablageContainer = GetComponent<StapelContainer>();
        Kartenbeschreibung alteKarte = endablageContainer.ObersteKarte();
        Debug.Log("Endablage überprüft: " + neueKarte + " kann gelegt werden auf: " + alteKarte);

        if (alteKarte == null)
            return (neueKarte.Wert == 2); //in leere Endablage kann nur die 2 gelegt werden
        else
            return 
                (alteKarte.Farbe==neueKarte.Farbe)
                &&
                (neueKarte.Wert == alteKarte.Wert+1)
                ;

    }

    public Kartenstapel kartenstapel;

    public void OnDrop(PointerEventData eventData)
    {
        StapelContainer spalte = eventData.pointerDrag.GetComponent<StapelContainer>();
        if (spalte != null) //es wurde eine spalte/container auf die Endablage gezogen
        {
            if (spalte.ziehendeKarten.Count == 1) //wenn mehrere karten einer anderen spalte hierher gezogen werden
            {
                Karte untersteKarte = spalte.ziehendeKarten[0];
                if(passt(untersteKarte.aktuelleBeschreibung())) //Kann Karte abgelegt werden?
                {
                    //Kartenstapel kartenstapel = FindObjectOfType<Kartenstapel>();
                    kartenstapel.kartenContainer = gameObject;
                    kartenstapel.erzeugeKarteInSzene().setzeBeschreibung(untersteKarte.aktuelleBeschreibung(), false);

                    spalte.EntferneObersteKarte();

                    SpielprinzipPatienceHarfe erweiterung = kartenstapel.GetComponent<SpielprinzipPatienceHarfe>();
                    erweiterung.PruefeObGewonnen();
                }
            }
        }
    }
}
