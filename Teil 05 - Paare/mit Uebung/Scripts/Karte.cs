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

    override public string ToString()
    {
        return beschreibung.kartenbild.name;
    }

    /// <summary>
    /// Ermittelt, ob zwei Karten den gleichen Wert haben. 
    /// </summary>
    /// <param name="wieKarte">Zu vergleichende Karte</param>
    /// <returns>True, wenn diese und die andere Karte die gleiche Beschreibung haben.</returns>
    public bool hatSelbenWert(Karte wieKarte)
    {
        return beschreibung == wieKarte.beschreibung;
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool KarteSchonUmgedreht()
    {
        return GetComponent<Image>().sprite == beschreibung.kartenbild;
    }

    private bool UmdrehenGestartet()
    {
        return GetComponent<PlayableDirector>().state == PlayState.Playing;
    }

    /// <summary>
    /// Vermerk, ob sich eine Karte gerade dreht, 
    /// wird von der animierten Karte gesetzt.
    /// </summary>
    private static bool karteIstInBewegung = false;

    public void OnClick()
    {
        if (karteIstInBewegung) { return; }
        karteIstInBewegung = true;

        if (UmdrehenGestartet() || KarteSchonUmgedreht()) { return;  }

        Debug.Log("OnClick wurde aufgerufen. ");
        Umdrehen();
    }

    public void Umdrehen()
    {        
        GetComponent<PlayableDirector>().Play();        
    }


    public Sprite rueckseite;

    public void TauscheKartenbild()
    {
        if (!Application.isPlaying) { return; }

        Debug.Log("Jetzt wird das Kartenbild getauscht.");

        if (KarteSchonUmgedreht()) //karte schon zu sehen -> wieder verdecken
        {
            GetComponent<Image>().sprite = rueckseite;
        }
        else //karte noch verdeckt -> jetzt aufdecken
        {
            GetComponent<Image>().sprite = beschreibung.kartenbild;
        }

    }

    /// <summary>
    /// Beschreibung wie eine Funktion aussehen muss, die von außen 
    /// gesetzt und von Karte bei Eintritt eines Ereignisses aufgerufen wird.
    /// </summary>
    /// <param name="karte">Die Karte, die den Aufruf verursachte.</param>
    public delegate void FunktionMitKarte(Karte karte);


    public FunktionMitKarte nachUmdrehenBenachrichten=null;

    /// <summary>
    /// Wird von der Umdrehen-Animationszeitleiste am Ende aufgerufen.
    /// </summary>
    public void UmdrehenAbgeschlossen()
    {
        if (nachUmdrehenBenachrichten != null) nachUmdrehenBenachrichten(this);

        karteIstInBewegung = false;
    }

}
