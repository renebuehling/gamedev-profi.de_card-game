using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Zeichnet den Kartenstapel und regelt seine Interaktion.
/// </summary>
public class Kartenstapel : MonoBehaviour
{
    /// <summary>
    /// Liste der Karten-Datenobjekte.
    /// </summary>
    public List<Kartenbeschreibung> karten;

    /// <summary>
    /// Vorlage des Szenenobjekts zur Darstellung einer Karte.
    /// </summary>
    public GameObject kartenPrefab;

    /// <summary>
    /// Ziel für die Kopien des kartenPrefabs, einfacher Layoutcontainer.
    /// </summary>
    public GameObject kartenContainer;

    
    /// <summary>
    /// Testet ob alle Karten aus dem Stapel entnommen wurden.
    /// </summary>
    /// <returns>True, wenn keine Karten mehr abgehoben werden können und der Stapel ausgeblendet werden soll.</returns>
    public bool istLeer()
    {
        return karten.Count == 0;
    }

    /// <summary>
    /// Wählt eine zufällige Karte aus der Liste karten, entfernt sie aus der Liste
    /// und liefert sie zurück.
    /// </summary>
    /// <returns>Datenbeschreibung der zufällig aus dem Stapel gezogenen Karte.</returns>
    /// <see cref="erzeugeKarteInSzene"/>
    public Kartenbeschreibung hebeZufaelligeKarteAb()
    {
        int zufaelligeStapelPosition = Random.Range(0, karten.Count);

        Kartenbeschreibung gezogeneKarte = karten[zufaelligeStapelPosition];
        Debug.Log("Gezogene Karte = " + gezogeneKarte.kartenbild + " von Position=" + zufaelligeStapelPosition);
        karten.Remove(gezogeneKarte);

        return gezogeneKarte;
    }

    /// <summary>
    /// Wählt die oberste Karte aus der Liste karten, entfernt sie aus der Liste
    /// und liefert sie zurück.
    /// </summary>
    /// <returns>Datenbeschreibung der aus dem Stapel gezogenen Karte.</returns>
    /// <see cref="erzeugeKarteInSzene"/>
    public Kartenbeschreibung hebeObersteKarteAb()
    {
        Kartenbeschreibung gezogeneKarte = karten[0];
        Debug.Log("Gezogene Karte = " + gezogeneKarte.kartenbild + " von oberster Position");
        karten.Remove(gezogeneKarte);

        return gezogeneKarte;
    }

    /// <summary>
    /// Erzeugt eine Prefab-Instanz des Kartenprototyps, platziert sie im gewünschten 
    /// Container und liefert die Karte-Komponente, die die Darstellung steuert.
    /// </summary>
    /// <returns>Karten-Darsteller in der Szene.</returns>
    public Karte erzeugeKarteInSzene()
    {
        return Instantiate(kartenPrefab, kartenContainer.transform).GetComponent<Karte>();
    }

    /// <summary>
    /// Fügt die Karte am Ende des Stapels ein.
    /// </summary>
    /// <param name="karte">Zurückzulegende Karte.</param>
    public void legeKarteZurueck(Kartenbeschreibung karte)
    {
        karten.Add(karte);
    }
    
    /// <summary>
    /// Mischt den Stapel durch.
    /// </summary>
    public void mischeStapel()
    {
        List<Kartenbeschreibung> kartenGemischt = new List<Kartenbeschreibung>(); //Ergebnis
        while(karten.Count>0)
        {
            kartenGemischt.Add(hebeZufaelligeKarteAb());
        }
        karten = kartenGemischt;
    }
}
