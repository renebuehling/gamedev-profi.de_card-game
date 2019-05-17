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
            rt.pivot = new Vector2(0, 1);

            rt.anchoredPosition = new Vector2(0,i*-20f);
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
    }

}
