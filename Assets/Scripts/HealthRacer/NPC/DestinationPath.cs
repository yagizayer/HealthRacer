using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestinationPath : MonoBehaviour
{
    [Tooltip("NPC'nin gitmesi gereken noktaları içeren Transform nesnesi")]
    public Transform targetsParent;
    [Tooltip("NPC'nin gitmesi gereken hedef noktaların listesi(GameObject)")]
    public List<Transform> targets;
    [Tooltip("NPC'nin gitmesi gereken geçerli Hedef noktanın indexi")]
    public int curretTarget;
    private void Awake()
    {
        if (targetsParent != null)
        {
            foreach (Transform item in targetsParent)
            {
                targets.Add(item);
            }
        }
    }

}
