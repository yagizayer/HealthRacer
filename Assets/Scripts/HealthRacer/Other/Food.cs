using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Food", menuName = "Food")]
public class Food : ScriptableObject
{
    [Tooltip("Yiyeceğin adı")]
    public string _name;
    [Tooltip("Yiyeceğin Abur Cubur Puanı")]
    public int ACP;// abur cubur puanı
    [Tooltip("Yiyeceğin Besin Puanı")]
    public int BP;// besin puanı

    [Tooltip("Yiyeceğin 3 boyutlu şekli(Mesh)")]
    public Mesh shape;
    [Tooltip("Yiyeceğin Materyali")]
    public Material material;
    [Tooltip("Yiyeceğin UI kısmında görülecek olan sprite")]
    public Sprite sprite;

}
