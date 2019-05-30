using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MauMauParteiComputerGegner : MauMauPartei
{
    public override void RundeStarten()
    {
        base.RundeStarten();
        StartCoroutine(ComputerZuegeAusfuehren());
    }


    private IEnumerator ComputerZuegeAusfuehren()
    {
        yield return new WaitForSeconds(0.5f);

        //1. schauen, ob eine Karte aus dem Handstapel abgelegt werden kann.
        bool konnteHandKarteLegen = false;
        foreach(Karte karteInHand in GetComponentsInChildren<Karte>())
        {
            if(spielprinzip.Passt(karteInHand))
            {
                Debug.Log("Computer legt ab: "+karteInHand);
                spielprinzip.LegeAb(karteInHand);
                konnteHandKarteLegen = true;
                break;
            }
        }

        //2. wenn keine Karte gelegt werden konnte, ziehen und schauen, ob diese neue passt.
        if (!konnteHandKarteLegen)
        {
            Debug.Log("Computer kann nicht, zieht Karte...");
            spielprinzip.GibKarten(1, this);
            yield return new WaitForSeconds(1f);

            Karte[] kinder = GetComponentsInChildren<Karte>();
            Karte neueKarte = kinder[kinder.Length - 1];
            if (spielprinzip.Passt(neueKarte))
            {
                Debug.Log("Computer legt ab: " + neueKarte);
                spielprinzip.LegeAb(neueKarte);
            }
            else
                Debug.Log("Computer kann auch die nachgezogene Karte nicht ablegen: " + neueKarte);
        }


        //Schluss: Zug beenden
        RundeBeenden();
    }

}
