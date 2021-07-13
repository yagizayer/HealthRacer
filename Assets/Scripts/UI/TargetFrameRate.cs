using UnityEngine;

public class TargetFrameRate : MonoBehaviour
{
    [SerializeField] private int targetFrameRate;
    void Start()
    {
        Application.targetFrameRate = targetFrameRate;
    }
}
