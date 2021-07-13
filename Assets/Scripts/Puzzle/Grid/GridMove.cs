using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMove : MonoBehaviour
{
    Camera cam;
    Vector2 firstPos;

    GameObject[] containerArray;
    GridController control;
    private void OnMouseDrag()
    {
        Vector3 pos = cam.ScreenToWorldPoint(Input.mousePosition);
        pos.z = 0;
        transform.position = pos;
    }
    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.GetComponent<ParticleSystem>().Stop();
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        firstPos = transform.position;

        containerArray = GameObject.FindGameObjectsWithTag("container");
        control = GameObject.Find("Controller").GetComponent<GridController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            foreach(GameObject container in containerArray)
            {
                if(container.name == gameObject.name)
                {
                    float distance = Vector3.Distance(container.transform.position, transform.position);
                    if(distance <= 1)
                    {
                        GameObject.FindGameObjectWithTag("MusicBtn").GetComponent<AudioSource>().PlayOneShot(GameObject.FindGameObjectWithTag("MusicBtn").GetComponent<GeneralController>().Sounds[1],1);
                        transform.position = container.transform.position;
                        control.increaseNumber();
                        this.enabled = false;
                        this.gameObject.GetComponent<ParticleSystem>().Play();
                        this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
                    }
                    else
                    {
                       // GameObject.FindGameObjectWithTag("Music").GetComponent<AudioSource>().PlayOneShot(GameObject.FindGameObjectWithTag("Music").GetComponent<GeneralController>().Sounds[2],1);
                        transform.position = firstPos;
                    }
                }
            }
        }
    }
}
