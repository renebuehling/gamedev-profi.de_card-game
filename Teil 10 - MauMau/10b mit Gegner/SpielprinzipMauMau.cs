using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SpielprinzipMauMau : MonoBehaviour
{
    public Kartenstapel stapel;
    public Karte offeneKarte;

    public List<MauMauPartei> spielparteien;

    private int indexAktuellerSpieler = -1;

    public MauMauPartei WerIstDran()
    {
        return spielparteien[indexAktuellerSpieler];
    }

    public void NaechsterSpieler()
    {
        indexAktuellerSpieler += 1;
        if (indexAktuellerSpieler == spielparteien.Count) indexAktuellerSpieler = 0; //Ende der Liste -> weiter mit dem ersten Spieler
        WerIstDran().RundeStarten();
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.N)) WerIstDran().RundeBeenden();
    }

    void Start()
    {
        stapel.mischeStapel();

        foreach(MauMauPartei partei in spielparteien)
            GibKarten(5, partei);

        offeneKarte.setzeBeschreibung(stapel.hebeZufaelligeKarteAb(), false);
        NaechsterSpieler();
    }

    public void GibKarten(int anzahl, MauMauPartei spielpartei)
    {
       bool istSpieler = spielpartei is MauMauParteiSpieler;

       for(int i=0; i<anzahl ; i++)
       {
            Kartenbeschreibung karteAusStapel = stapel.hebeObersteKarteAb();
            stapel.kartenContainer = spielpartei.gameObject;
            Karte karteInSzene = stapel.erzeugeKarteInSzene();
            karteInSzene.setzeBeschreibung(karteAusStapel, istSpieler);
            if (istSpieler) karteInSzene.Umdrehen();
            else karteInSzene.TauscheKartenbild();
            spielpartei.WennKarteHinzugefuegtWurde(karteInSzene);
       }
    }

    

    public bool Passt(Karte karte)
    {
        return  (karte.aktuelleBeschreibung().Farbe == offeneKarte.aktuelleBeschreibung().Farbe)
                ||
                (karte.aktuelleBeschreibung().Wert == offeneKarte.aktuelleBeschreibung().Wert)
                ;
    }

    public void LegeAb(Karte handstapelkarteInSzene)
    {
        stapel.legeKarteZurueck(offeneKarte.aktuelleBeschreibung());
        stapel.mischeStapel();

        offeneKarte.setzeBeschreibung(handstapelkarteInSzene.aktuelleBeschreibung(), false);
        Destroy(handstapelkarteInSzene.gameObject); //Typischer Fehler: .gameObject vergessen
    }



}
