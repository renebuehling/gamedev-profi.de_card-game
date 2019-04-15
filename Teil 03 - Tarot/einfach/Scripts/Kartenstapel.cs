using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Zeichnet den Kartenstapel und regelt seine Interaktion.
/// </summary>
public class Kartenstapel : MonoBehaviour
{
    public List<Kartenbeschreibung> karten;

    public GameObject kartenPrefab;

    public GameObject kartenContainer;

    public int verbleibendeZuege = 4;

    public Text hilfetext;

    private void aktualisiereHilfetext()
    {
        if (verbleibendeZuege == 0)
        {
            hilfetext.text = "Sieh Dir die Karten an, die Du gezogen hast.";
        }
        else
        {
            hilfetext.text = "Ziehe " + verbleibendeZuege + " Karten!";
        }
    }

    private void Start()
    {
        aktualisiereHilfetext();
    }


    public void KlickAufStapel()
    {
        Debug.Log("Klick auf den Stapel.");
        if (karten.Count == 0)
        {
            Debug.Log("Der Stapel ist leer!");
            return;
        }

        int zufaelligeStapelPosition = Random.Range(0,karten.Count);

        Kartenbeschreibung gezogeneKarte = karten[zufaelligeStapelPosition];
        Debug.Log("Gezogene Karte = " + gezogeneKarte.kartenbild + " von Position="+zufaelligeStapelPosition);
        karten.Remove(gezogeneKarte);
        verbleibendeZuege -= 1; // ist das gleiche wie verbleibendeZuege = verbleibendeZuege - 1;

        GameObject karteInSzene = Instantiate(kartenPrefab,kartenContainer.transform);
        karteInSzene.GetComponent<Karte>().setzeBeschreibung(gezogeneKarte,false);

        aktualisiereHilfetext();

        if (karten.Count == 0 || verbleibendeZuege==0) gameObject.SetActive(false);
    }
}
