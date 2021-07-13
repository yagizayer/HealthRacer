using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    [SerializeField]
    private float _destroyTime;

    private AntibodyGameManager _manager;

    private void Start()
    {
        _manager = GameObject.Find("AntibodyGameManager").GetComponent<AntibodyGameManager>();
    }

    private void Update()
    {
        if (_manager.isGameRunning)
        {
            if(_destroyTime > 0)
            {
                _destroyTime -= Time.deltaTime;
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
