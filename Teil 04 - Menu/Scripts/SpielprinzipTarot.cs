using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpielprinzipTarot : MonoBehaviour
{
    public int verbleibendeZuege
    {
        get { return container.Count; }
    }

    public Text hilfetext;

    /// <summary>
    /// Liste mit Spielobjekten, die als Container für die
    /// Ablage der Karten dienen. 
    /// </summary>
    public List<GameObject> container;

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
        Kartenstapel stapel = GetComponent<Kartenstapel>();

        Debug.Log("Klick auf den Stapel.");
        if (stapel.istLeer())
        {
            Debug.Log("Der Stapel ist leer!");
            return;
        }

        Kartenbeschreibung gezogeneKarte = stapel.hebeZufaelligeKarteAb();
        
        stapel.kartenContainer = container[0];
        container.RemoveAt(0);

        stapel.erzeugeKarteInSzene().setzeBeschreibung(gezogeneKarte, false);

        aktualisiereHilfetext();

        if (stapel.karten.Count == 0 || verbleibendeZuege == 0) gameObject.SetActive(false);
    }


}
