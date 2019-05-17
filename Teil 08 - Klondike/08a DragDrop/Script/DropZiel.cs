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
            if (spielprinzip.LegeKarteAuf(ziel, spalte.ObersteKarte())) //wurde abgelegt
            {
                spalte.EntferneObersteKarte();
            }
        }
    }
}
