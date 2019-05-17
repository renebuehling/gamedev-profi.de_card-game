using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

        GetComponent<Image>().enabled = false; //gameObject.SetActive(false);
    }

    private void verdoppleKartenIn(Kartenstapel stapel)
    {
        for(int i=stapel.karten.Count-1; i>=0;i--)
        {
            Kartenbeschreibung karte = stapel.karten[i];
            stapel.karten.Add(karte);
        }
    }

    private Karte ersteKarte = null;
    private Karte zweiteKarte = null;

    public GameObject dialogGleichesPaar;
    public GameObject dialogUngleichesPaar;
    public GameObject dialogGewonnen;
    public GameObject dialogGameOver;
    public Text hilfetext;

    private int verbleibendeZuege = 5;

    private void wennKarteUmgedrehtWurde(Karte umgedrehteKarte)
    {
        Debug.Log("Spielprinzip weiß: Karte "+umgedrehteKarte+" wurde umgedreht.");

        if (!umgedrehteKarte.KarteSchonUmgedreht()) return; //verhindern, dass das umdrehen sofort wieder das zurück Umdrehen auslöst
        if (ersteKarte!=null) //dies ist die zweite Karte
        {
            zweiteKarte = umgedrehteKarte;
            if (ersteKarte.hatSelbenWert(umgedrehteKarte)) //paar gefunden
            {
                Debug.Log("Die beiden Karten sind gleich. " + ersteKarte + " " + umgedrehteKarte);

                verbleibendeZuege = 5;
                hilfetext.text = "Finde alle Paare! Noch " + verbleibendeZuege + " Versuche.";

                Kartenstapel stapel = GetComponent<Kartenstapel>();
                if(stapel.kartenContainer.transform.childCount<=2) //das waren die letzten beiden Karten -> gewonnen
                {
                    dialogGewonnen.SetActive(true);
                }
                else //es sind noch mehrere Karten auf dem Brett -> nur diese beiden wegräumen, dann weiter spielen
                {
                    dialogGleichesPaar.SetActive(true);
                    StartCoroutine(dialogAutomatischAusblenden());
                }
            }
            else //unterschiedliche karten
            {
                Debug.Log("Die beiden Karten sind unterschiedlich: "+ersteKarte+" "+umgedrehteKarte);

                verbleibendeZuege = verbleibendeZuege - 1;
                hilfetext.text = "Finde alle Paare! Noch "+verbleibendeZuege+" Versuche.";
                if (verbleibendeZuege == 0)
                {
                    dialogGameOver.SetActive(true);
                }
                else
                {
                    dialogUngleichesPaar.SetActive(true);
                    StartCoroutine(dialogAutomatischAusblenden());
                }
            }             
        }
        else //dies ist die erste Karte
        {
            ersteKarte = umgedrehteKarte;
        }
    }

    private IEnumerator dialogAutomatischAusblenden()
    {

        yield return new WaitForSeconds(3);
        if (dialogGleichesPaar.activeSelf)
        {
            ButtonKlickPaarSammeln();
        }
        else //dialogUngleichesPaar ist aktiv
        {
            ButtonKlickPaarVerwerfen();
        }
    }

    /// <summary>
    /// Funktion des Weiter-Buttons im Paar-Gefunden-Dialog.
    /// </summary>
    public void ButtonKlickPaarSammeln()
    {
        Destroy(ersteKarte.gameObject);
        Destroy(zweiteKarte.gameObject);
        dialogGleichesPaar.SetActive(false);
        ersteKarte = null;
        zweiteKarte = null;
    }

    /// <summary>
    /// Funktion des Weiter-Buttons im Ungleiches-Paar-Dialog.
    /// </summary>
    public void ButtonKlickPaarVerwerfen()
    {
        ersteKarte.Umdrehen();
        zweiteKarte.Umdrehen();
        dialogUngleichesPaar.SetActive(false);
        ersteKarte = null;
        zweiteKarte = null;
    }


}
