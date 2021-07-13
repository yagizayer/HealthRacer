using UnityEngine;

public class Antibody : MonoBehaviour
{

    [SerializeField]
    private VirusType _antibodyType;

    public VirusType AntibodyType
    {
        get { return _antibodyType; }
        set { _antibodyType = value; }
    }

    [SerializeField]
    private float _speed;

    public float Speed
    {
        set { _speed = value; }
    }

    private AntibodyGameManager _manager;

    private void FixedUpdate()
    {
        if (_manager.isGameRunning)
        {
            transform.Translate(Vector2.left * _speed);
        }
    }

    private void Start()
    {
        _manager = GameObject.Find("AntibodyGameManager").GetComponent<AntibodyGameManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "AntibodyKiller") { Destroy(gameObject);}
    }
}
