using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Virus : MonoBehaviour
{
    [SerializeField]
    private VirusType _virusType;

    [SerializeField]
    private GameObject _smileEffect;

    [SerializeField]
    private GameObject _brokenHeartEffect;

    public VirusType VirusType
    {
        set { _virusType = value; }
    }

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
            transform.Translate(Vector2.right * _speed * _manager.gameSpeedMultiplier);
        }
    }

    private IEnumerator DestroyOnTime(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Antibody":
                if (collision.gameObject.GetComponent<Antibody>().AntibodyType == _virusType)
                {
                    _manager.AddScore(100);
                    //Destroy(collision.gameObject);
                    //Destroy(gameObject);
                    _speed = 0;
                    collision.gameObject.GetComponent<Antibody>().Speed = 0;
                    collision.gameObject.transform.SetParent(transform);
                    GetComponent<BoxCollider2D>().enabled = false;
                    collision.gameObject.GetComponent<BoxCollider2D>().enabled = false;
                    switch (_virusType)
                    {
                        case VirusType.E:
                            collision.gameObject.transform.localPosition = Constants.VirusEPos;
                            break;
                        case VirusType.C:
                            collision.gameObject.transform.localPosition = Constants.VirusCPos;
                            break;
                        case VirusType.Cube:
                            collision.gameObject.transform.localPosition = Constants.VirusCubePos;
                            break;
                        case VirusType.Arrow:
                            collision.gameObject.transform.localPosition = Constants.VirusArrowPos;
                            break;
                    }

                    Instantiate(_smileEffect, transform.position, Quaternion.identity);
                    StartCoroutine(DestroyOnTime(1f));
                }
                else
                {
                    Destroy(collision.gameObject);
                    //Debug.Log("Destroyed");
                }
                break;

            case "Border":
                _manager.HitPlayer();
                Instantiate(_brokenHeartEffect, Vector2.zero, Quaternion.identity);
                Destroy(gameObject);
                break;
        }

    }
}
