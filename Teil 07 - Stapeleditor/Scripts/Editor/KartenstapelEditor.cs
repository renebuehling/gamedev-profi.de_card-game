using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Kartenstapel))]
public class KartenstapelEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("Karten einlesen"))
        {
            foreach(UnityEngine.Object o in Selection.objects)
            {
                Debug.Log("- Object: "+o);
                Sprite sprite = null;
                if (o is Sprite)
                {
                    sprite = (Sprite) o;
                }
                else if (o is Texture2D)
                {
                    string[] guid = AssetDatabase.FindAssets(o.name+" t:Sprite");
                    if (guid.Length>0)
                    {
                        sprite = (Sprite) AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(guid[0]),typeof(Sprite));
                    }
                }

                if (sprite!=null)
                {
                    Kartenbeschreibung karte = new Kartenbeschreibung();
                    karte.kartenbild = sprite;

                    string n = sprite.name;
                    if (n.StartsWith("ace_"))
                    {
                        karte.Wert = 14;
                    }
                    else if (n.StartsWith("king_"))
                    {
                        karte.Wert = 13;
                    }
                    else if (n.StartsWith("queen_"))
                    {
                        karte.Wert = 12;
                    }
                    else if (n.StartsWith("jack_"))
                    {
                        karte.Wert = 11;
                    }
                    else 
                    {
                        for(int i=2; i<11 ;i++)
                        {
                            if (n.StartsWith(i+"_of_"))
                            {
                                karte.Wert = i;
                                break;
                            }
                        }
                    }


                    if (n.IndexOf("spades")>-1)
                    {
                        karte.Farbe = 1;
                    }
                    else if (n.IndexOf("clubs") > -1)
                    {
                        karte.Farbe = 2;
                    }
                    else if (n.IndexOf("hearts") > -1)
                    {
                        karte.Farbe = 13;
                    }
                    else if (n.IndexOf("diamonds") > -1)
                    {
                        karte.Farbe = 14;
                    }


                    Kartenstapel stapel = (Kartenstapel) target;
                    stapel.karten.Add(karte);
                }

            }
        }
    }
}
