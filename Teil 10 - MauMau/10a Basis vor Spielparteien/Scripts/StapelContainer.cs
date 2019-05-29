using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Ablagefläche für mehrere Karten in Form einer Spalte.
/// </summary>
public class StapelContainer : MonoBehaviour
{
    /// <summary>
    /// Berechnet das Layout der Karten im Container. 
    /// </summary>
    public void KartenAnordnen()
    {
        for(int i=0; i<transform.childCount; i++)
        {
            RectTransform rt = transform.GetChild(i).GetComponent<RectTransform>();

            rt.anchorMin = new Vector2(0,1);
            rt.anchorMax = new Vector2(0, 1);
            //rt.pivot = new Vector2(0, 1);

            //rt.anchoredPosition = new Vector2(0,i*-20f);
            rt.anchoredPosition = new Vector2(125/2, (-180/2) + (i * -20f)); //mit Mittelpunkt oben links ausrichten
            rt.sizeDelta = new Vector2(125,180);
        }
    }

    /// <summary>
    /// Liefert die oberste Karte auf dem Stapel der Karten in diesem Container.
    /// </summary>
    /// <returns>Beschreibung der obersten Karte oder null, wenn der Container noch leer ist.</returns>
    public Kartenbeschreibung ObersteKarte()
    {
        if (transform.childCount == 0) return null;
        return transform.GetChild(transform.childCount - 1).GetComponent<Karte>().aktuelleBeschreibung();
    }

    public void EntferneObersteKarte()
    {
        if (transform.childCount == 0) return;

        Destroy(transform.GetChild(transform.childCount-1).gameObject);

        if (transform.childCount >= 2) //wir brauchen mindestens 2 = die gelöschte + die darunterliegende
        {
            Karte k = transform.GetChild(transform.childCount - 2).GetComponent<Karte>();
            //Debug.Log("Nächste Karte auf Container/Spalte = " + k);
            if(!k.KarteSchonUmgedreht())
            {
                k.Umdrehen();
            }
        }
    }

    /// <summary>
    /// Teilliste von Karten, die zwischen Containern gezogen werden.
    /// </summary>
    public List<Karte> ziehendeKarten = new List<Karte>();

    /// <summary>
    /// Sammelt die Karten, die auf der karteUnterMaus liegen (inkl. der Karte selbst)
    /// in der Liste ziehendeKarten.
    /// </summary>
    /// <param name="karteUnterMaus"></param>
    /// <returns>Ist dies ein gültiger Zug?</returns>
    public bool SammleKartenAb(Karte karteUnterMaus)
    {
        for(int i=karteUnterMaus.transform.GetSiblingIndex(); i < transform.childCount ; i++)
        {
            Karte k = transform.GetChild(i).GetComponent<Karte>();
            if (!k.KarteSchonUmgedreht()) return false;
            ziehendeKarten.Add(k);
        }
        return true;
    }

    public void EntferneZiehendeKarten()
    {
        if (ziehendeKarten[0].transform.GetSiblingIndex()>=1 ) //es liegt noch mind. 1 karte unter der untersten des gezogenen teilstapels
        {
            Karte k = transform.GetChild(ziehendeKarten[0].transform.GetSiblingIndex()-1).GetComponent<Karte>();
            //Debug.Log("Nächste Karte auf Container/Spalte = " + k);
            if (!k.KarteSchonUmgedreht())
            {
                k.Umdrehen();
            }
        }


        foreach (Karte k in ziehendeKarten)
            Destroy(k.gameObject);

        ziehendeKarten.Clear();
    }

}
