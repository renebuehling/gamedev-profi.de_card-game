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
}
