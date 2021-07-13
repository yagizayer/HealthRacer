using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodCell : MonoBehaviour
{
    [SerializeField]
    private float _speed;

    private AntibodyGameManager _manager;

    private void Start()
    {
        _manager = GameObject.Find("AntibodyGameManager").GetComponent<AntibodyGameManager>();
    }

    private void FixedUpdate()
    {
        if (_manager.isGameRunning)
        {
            transform.Translate(Vector2.right * _speed);
        }
    }
}
