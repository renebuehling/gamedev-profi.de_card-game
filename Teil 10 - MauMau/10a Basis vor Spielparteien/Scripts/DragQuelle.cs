using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragQuelle : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public Image ghost;
    public Transform ghostGroup;

    private void geistErzeugen(Sprite sprite)
    {
        GameObject neuerGeist = Instantiate(ghost.gameObject, ghostGroup);
        neuerGeist.GetComponent<Image>().sprite = sprite;
        neuerGeist.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -20 * neuerGeist.transform.GetSiblingIndex());
        neuerGeist.SetActive(true); 
    }

    private void geisterLoeschen()
    {
        for (int i = 0; i < ghostGroup.transform.childCount; i++)
            Destroy(ghostGroup.transform.GetChild(i).gameObject);
    }

    /// <summary>
    /// Zeiger auf den gezogenen Container (falls ein solcher die DragQuelle war).
    /// </summary>
    private StapelContainer container = null;

    public void OnBeginDrag(PointerEventData eventData)
    {
        container = GetComponent<StapelContainer>();
        if (container != null)
        {
            if (container.ObersteKarte() == null) //Drag abbrechen, wenn gezogene Spalte leer
            {
                eventData.pointerDrag = null;
                return;
            }
            else
            {
                //ghost.sprite = container.ObersteKarte().kartenbild;

                Karte karteUnterMaus = eventData.pointerPressRaycast.gameObject.GetComponent<Karte>();
                if (karteUnterMaus != null)
                {
                    Debug.Log("KarteunterMaus=" + karteUnterMaus);
                    if (!container.SammleKartenAb(karteUnterMaus)) eventData.pointerDrag = null; //Versuch verdeckte Karte zu ziehen -> Abbruch!
                    else //karten können gezogen werden
                    {
                        foreach (Karte k in container.ziehendeKarten) geistErzeugen(k.aktuelleBeschreibung().kartenbild);
                    }
                }

            }
        }
        else
            geistErzeugen(GetComponent<Image>().sprite); //ghost.sprite = GetComponent<Image>().sprite;


        ghostGroup.gameObject.SetActive(true);
    }

    public void OnDrag(PointerEventData eventData)
    {
        //Debug.Log("OnDrag! "+eventData);
        ghostGroup.transform.position = eventData.pointerCurrentRaycast.worldPosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        ghostGroup.gameObject.SetActive(false);
        geisterLoeschen();
        if (container!=null) //es wurde Container/Spalte gezogen
        {
            container.ziehendeKarten.Clear();
            container = null;
        }
    }
}
