using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Szenenwechsel : MonoBehaviour
{
    public string szenenname;

    public void JetztWechseln()
    {
        SceneManager.LoadScene(szenenname);
    }
}
