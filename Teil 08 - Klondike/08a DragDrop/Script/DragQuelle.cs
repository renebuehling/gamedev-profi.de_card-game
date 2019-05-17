using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragQuelle : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public Image ghost;

    public void OnBeginDrag(PointerEventData eventData)
    {
        StapelContainer container = GetComponent<StapelContainer>();
        if (container != null)
        {
            if (container.ObersteKarte() == null) //Drag abbrechen, wenn gezogene Spalte leer
            {
                eventData.pointerDrag = null;
                return;
            }
            else ghost.sprite = container.ObersteKarte().kartenbild;
        }
        else 
            ghost.sprite = GetComponent<Image>().sprite;


        ghost.gameObject.SetActive(true);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("OnDrag! "+eventData);
        ghost.transform.position = eventData.pointerCurrentRaycast.worldPosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        ghost.gameObject.SetActive(false);
    }
}
