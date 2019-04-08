using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;

public class Karte : MonoBehaviour
{
    /// <summary>
    /// Zeiger auf das Bild der umgedrehten Karte.
    /// </summary>
    public Sprite kartenbild;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick()
    {
        Debug.Log("OnClick wurde aufgerufen.");

        GetComponent<PlayableDirector>().Play();        
    }

    public void TauscheKartenbild()
    {
        GetComponent<Image>().sprite = kartenbild;
    }

}
