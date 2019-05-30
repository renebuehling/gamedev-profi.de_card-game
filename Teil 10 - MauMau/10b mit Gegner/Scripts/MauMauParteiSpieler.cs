using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MauMauParteiSpieler : MauMauPartei
{
    public Button btnRundeBeenden;

    public override void RundeStarten()
    {
        base.RundeStarten();
        btnRundeBeenden.interactable = false;
        spielprinzip.stapel.GetComponent<Button>().interactable = true;
        SetzeHandkartenInteractable(true);
    }

    public override void RundeBeenden()
    {
        btnRundeBeenden.interactable = false;
        base.RundeBeenden();
    }


    override public void WennKarteHinzugefuegtWurde(Karte karteInSzene)
    {
        base.WennKarteHinzugefuegtWurde(karteInSzene);
        karteInSzene.GetComponent<Button>().onClick.AddListener(KlickAufHandStapelKarte);
    }
    

    private void KlickAufHandStapelKarte()
    {
        if (spielprinzip.WerIstDran()!=this)
        {
            Debug.LogWarning("Du bist nicht dran! ");
            return;
        }

        Karte karteInSzene = EventSystem.current.currentSelectedGameObject.GetComponent<Karte>();

        Debug.Log("Handstapelkarte angeklickt: " + karteInSzene.aktuelleBeschreibung());
        if (spielprinzip.Passt(karteInSzene))
        {
            spielprinzip.LegeAb(karteInSzene);
            spielprinzip.stapel.GetComponent<Button>().interactable = false;
            SetzeHandkartenInteractable(false);
            btnRundeBeenden.interactable = true;
        }
        else
            Debug.Log("Die Karte " + karteInSzene + " passt NICHT auf " + spielprinzip.offeneKarte);
    }

    public void KlickAufKartenstapel()
    {
        if (spielprinzip.WerIstDran() != this)
        {
            Debug.LogWarning("Du bist nicht dran! ");
            return;
        }

        spielprinzip.GibKarten(1, this);
        spielprinzip.stapel.GetComponent<Button>().interactable = false;
        btnRundeBeenden.interactable = true;
    }

    private void SetzeHandkartenInteractable(bool neuerZustand)
    {
        foreach (Button b in GetComponentsInChildren<Button>()) b.interactable = neuerZustand;
    }

}
