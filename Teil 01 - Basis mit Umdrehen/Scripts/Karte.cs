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

    private bool KarteSchonUmgedreht()
    {
        return GetComponent<Image>().sprite == kartenbild;
    }

    private bool UmdrehenGestartet()
    {
        return GetComponent<PlayableDirector>().state == PlayState.Playing;
    }

    public void OnClick()
    {
        if (UmdrehenGestartet() || KarteSchonUmgedreht()) { return;  }

        Debug.Log("OnClick wurde aufgerufen. ");

        GetComponent<PlayableDirector>().Play();        
    }

    public void TauscheKartenbild()
    {
        if (!Application.isPlaying) { return; }

        Debug.Log("Jetzt wird das Kartenbild getauscht.");
        GetComponent<Image>().sprite = kartenbild;
    }

}
