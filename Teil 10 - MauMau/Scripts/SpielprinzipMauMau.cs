using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SpielprinzipMauMau : MonoBehaviour
{
    public Kartenstapel stapel;
    public Karte offeneKarte;

    public GameObject spielerHandstapel;

    void Start()
    {
        stapel.mischeStapel();
        GibKarten(5, spielerHandstapel);
        offeneKarte.setzeBeschreibung(stapel.hebeZufaelligeKarteAb(), false);        
    }

    private void GibKarten(int anzahl, GameObject handstapel)
    {
       for(int i=0; i<anzahl ; i++)
       {
            Kartenbeschreibung karteAusStapel = stapel.hebeObersteKarteAb();
            stapel.kartenContainer = handstapel;
            Karte karteInSzene = stapel.erzeugeKarteInSzene();
            karteInSzene.setzeBeschreibung(karteAusStapel, true);
            karteInSzene.Umdrehen();
            karteInSzene.GetComponent<Button>().onClick.AddListener(KlickAufHandStapelKarte);
       }
    }

    private void KlickAufHandStapelKarte()
    {
        Karte karteInSzene = EventSystem.current.currentSelectedGameObject.GetComponent<Karte>();

        Debug.Log("Handstapelkarte angeklickt: "+karteInSzene.aktuelleBeschreibung());
        if (Passt(karteInSzene))
            LegeAb(karteInSzene);
        else
            Debug.Log("Die Karte "+karteInSzene+" passt NICHT auf "+offeneKarte);
    }

    private bool Passt(Karte karte)
    {
        return  (karte.aktuelleBeschreibung().Farbe == offeneKarte.aktuelleBeschreibung().Farbe)
                ||
                (karte.aktuelleBeschreibung().Wert == offeneKarte.aktuelleBeschreibung().Wert)
                ;
    }

    private void LegeAb(Karte handstapelkarteInSzene)
    {
        stapel.legeKarteZurueck(offeneKarte.aktuelleBeschreibung());
        stapel.mischeStapel();

        offeneKarte.setzeBeschreibung(handstapelkarteInSzene.aktuelleBeschreibung(), false);
        Destroy(handstapelkarteInSzene.gameObject); //Typischer Fehler: .gameObject vergessen
    }

    public void KlickAufKartenstapel()
    {
        GibKarten(1, spielerHandstapel);
    }

}
