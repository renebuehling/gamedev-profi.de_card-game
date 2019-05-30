using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MauMauPartei : MonoBehaviour
{
    public SpielprinzipMauMau spielprinzip;

    public virtual void WennKarteHinzugefuegtWurde(Karte karteInSzene)
    {
        // Definiert durch Unterklassen
    }

    public virtual void RundeStarten()
    {
        Debug.Log(gameObject.name+" startet die Runde.");
    }

    public virtual void RundeBeenden()
    {
        Debug.Log(gameObject.name + " beendet die Runde.");
        spielprinzip.NaechsterSpieler();
    }
}
