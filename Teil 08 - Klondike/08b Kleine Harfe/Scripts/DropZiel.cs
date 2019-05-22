using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropZiel : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("OnDrop! "+eventData);

        StapelContainer ziel = GetComponent<StapelContainer>();
        SpielprinzipPatience spielprinzip = FindObjectOfType<SpielprinzipPatience>();
        //spielprinzip.LegeKarteAuf(ziel);

        Karte karte = eventData.pointerDrag.GetComponent<Karte>();
        if (karte != null) //es wurde eine (vorschau)karte gezogen
        {
            spielprinzip.LegeKarteAuf(ziel);
            return; //ondrop abbrechen
        }

        StapelContainer spalte = eventData.pointerDrag.GetComponent<StapelContainer>();
        if (spalte!=null) //es wurde eine andere spalte/container gezogen
        {
            if(spalte.ziehendeKarten.Count>0) //wenn mehrere karten einer anderen spalte hierher gezogen werden
            {
                foreach (Karte k in spalte.ziehendeKarten) Debug.Log("- gezogen wurde: "+k);

                Karte untersteKarte = spalte.ziehendeKarten[0];
                if (spielprinzip.LegeKarteAuf(ziel, untersteKarte.aktuelleBeschreibung())) //unterste Karte der Ziehenden gemäß der Spielregeln ablegbar?
                {
                    for(int i=1; i<spalte.ziehendeKarten.Count ;i++)
                    {
                        spielprinzip.LegeKarteAuf(ziel, spalte.ziehendeKarten[i].aktuelleBeschreibung());
                    }
                    spalte.EntferneZiehendeKarten();
                }

            }
            else if (spielprinzip.LegeKarteAuf(ziel, spalte.ObersteKarte())) //wurde abgelegt
            {
                spalte.EntferneObersteKarte();
            }
        }
    }
}
