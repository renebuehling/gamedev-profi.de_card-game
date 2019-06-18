using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;

/// <summary>
/// Zeichnet die Daten einer Kartenbeschreibung in der Szene
/// und regelt die Interaktionsmöglichkeiten.
/// </summary>
public class Karte : MonoBehaviour
{
    /// <summary>
    /// Zeiger auf die Daten der umgedrehten Karte.
    /// </summary>
    private Kartenbeschreibung beschreibung;

    /// <summary>
    /// Weist der Kartendarstellung einen neuen Kartenwert zu.
    /// Die Karte ist sofort sichtbar, wenn kannAngeklicktWerden false ist.
    /// </summary>
    /// <param name="neueBeschreibung">Neuer Wert der Karte.</param>
    /// <param name="kannAngeklicktWerden">Wenn true, spielt die Karte bei Klick die Umdrehanimation ab.</param>
    public void setzeBeschreibung(Kartenbeschreibung neueBeschreibung, bool kannAngeklicktWerden)
    {
        beschreibung = neueBeschreibung;
        if (!kannAngeklicktWerden) { GetComponent<Image>().sprite = neueBeschreibung.kartenbild; }
        GetComponent<Button>().enabled = kannAngeklicktWerden;
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private bool KarteSchonUmgedreht()
    {
        return GetComponent<Image>().sprite == beschreibung.kartenbild;
    }

    private bool UmdrehenGestartet()
    {
        return GetComponent<PlayableDirector>().state == PlayState.Playing;
    }

    public void OnClick()
    {
        if (UmdrehenGestartet() || KarteSchonUmgedreht()) { return;  }

        Debug.Log("OnClick wurde aufgerufen. ");

        GetComponent<PlayableDirector>().Play();        
    }

    public void TauscheKartenbild()
    {
        if (!Application.isPlaying) { return; }

        Debug.Log("Jetzt wird das Kartenbild getauscht.");
        GetComponent<Image>().sprite = beschreibung.kartenbild;
    }

}
