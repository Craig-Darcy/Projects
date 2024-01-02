using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PageType { Text, Texture } //Enumerator for the type of page (some pages will be texture based images other are text based notes)

//Creates a scriptable object that allows for cases to be made easily and attached to game objects
[CreateAssetMenu(fileName ="newPage", menuName ="Notes System/newPage")]

public class PageScript : ScriptableObject
{

    public string pageName = string.Empty;
    public int clueNumber;
    public PageType type = PageType.Text;
    [TextArea(8, 16)]
    public string pageText = string.Empty;
    public Sprite pageTexture = null;
    public bool useEasyReader;

}
