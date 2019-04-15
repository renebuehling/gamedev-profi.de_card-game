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

    //public Image kartendarstellung;

    public GameObject kartenPrefab;

    public GameObject kartenContainer;

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

        //kartendarstellung.sprite = gezogeneKarte.kartenbild;
        //kartendarstellung.gameObject.SetActive(true);
        GameObject karteInSzene = Instantiate(kartenPrefab,kartenContainer.transform);
        karteInSzene.GetComponent<Image>().sprite = gezogeneKarte.kartenbild;
        karteInSzene.GetComponent<Button>().enabled = false;

        if (karten.Count == 0) gameObject.SetActive(false);
    }
}
