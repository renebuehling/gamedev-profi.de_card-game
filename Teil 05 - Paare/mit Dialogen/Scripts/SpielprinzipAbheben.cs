using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpielprinzipAbheben : MonoBehaviour
{
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

        stapel.erzeugeKarteInSzene().setzeBeschreibung(gezogeneKarte, false);

        if (stapel.karten.Count == 0) gameObject.SetActive(false);
    }
}
