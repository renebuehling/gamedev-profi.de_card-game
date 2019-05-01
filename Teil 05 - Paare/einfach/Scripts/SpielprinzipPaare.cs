using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpielprinzipPaare : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Kartenstapel stapel = GetComponent<Kartenstapel>();
        verdoppleKartenIn(stapel);

        while(!stapel.istLeer())
        {
            Kartenbeschreibung gezogeneKarte = stapel.hebeZufaelligeKarteAb();
            Karte k = stapel.erzeugeKarteInSzene();
            k.setzeBeschreibung(gezogeneKarte, true);
            k.nachUmdrehenBenachrichten = wennKarteUmgedrehtWurde;
        }        

        gameObject.SetActive(false);
    }

    private void verdoppleKartenIn(Kartenstapel stapel)
    {
        for(int i=stapel.karten.Count-1; i>=0;i--)
        {
            Kartenbeschreibung karte = stapel.karten[i];
            stapel.karten.Add(karte);
        }
    }

    private Karte letzteKarte = null;

    private void wennKarteUmgedrehtWurde(Karte umgedrehteKarte)
    {
        Debug.Log("Spielprinzip weiß: Karte "+umgedrehteKarte+" wurde umgedreht.");

        if (!umgedrehteKarte.KarteSchonUmgedreht()) return;
        if (letzteKarte!=null) //dies ist die zweite Karte
        {
            if (letzteKarte.hatSelbenWert(umgedrehteKarte)) //paar gefunden
            {
                Debug.Log("Die beiden Karten sind gleich. " + letzteKarte + " " + umgedrehteKarte);
                Destroy(letzteKarte.gameObject);
                Destroy(umgedrehteKarte.gameObject);
            }
            else
            {
                Debug.Log("Die beiden Karten sind unterschiedlich: "+letzteKarte+" "+umgedrehteKarte);
                umgedrehteKarte.Umdrehen();
                letzteKarte.Umdrehen();
            }
            letzteKarte = null; 
        }
        else //dies ist die erste Karte
        {
            letzteKarte = umgedrehteKarte;
        }
    }
}
