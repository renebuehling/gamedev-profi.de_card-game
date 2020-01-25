using System.Collections;
using UnityEngine;

/// <summary>
/// Deaktiviert eine Komponente nach einem Frame. 
/// 
/// Anwendungsbeispiel: 
/// In Abschnitt 6 Paare finden dem Kartenlayout-Objekt hinzufügen und GridLayoutGroup als komponente setzen.
/// Dadurch bleiben Lücken stehen, wenn Karten entfernt wurden, anstatt das Layout aufzurücken.
/// </summary>
public class VerzoegertDeaktivieren : MonoBehaviour
{
    [Tooltip("Zeiger auf die Komponente, die deaktiviert werden soll. ")]
    public Behaviour komponente;
    
    IEnumerator Start()
    {
        yield return new WaitForEndOfFrame();
        if (komponente != null) komponente.enabled = false;
    }
}
