using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpielprinzipTarot : MonoBehaviour
{
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
        Kartenstapel stapel = GetComponent<Kartenstapel>();

        Debug.Log("Klick auf den Stapel.");
        if (stapel.istLeer())
        {
            Debug.Log("Der Stapel ist leer!");
            return;
        }

        Kartenbeschreibung gezogeneKarte = stapel.hebeZufaelligeKarteAb();
        verbleibendeZuege -= 1; // ist das gleiche wie verbleibendeZuege = verbleibendeZuege - 1;

        stapel.erzeugeKarteInSzene().setzeBeschreibung(gezogeneKarte, false);

        aktualisiereHilfetext();

        if (stapel.karten.Count == 0 || verbleibendeZuege == 0) gameObject.SetActive(false);
    }


}
