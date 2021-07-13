
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class RigRotator : MonoBehaviour
{
    [Tooltip("Nesneni etrafinda dönecek olan kamera")]
    public GameObject mainCamera;
    [Tooltip("Etrafında dönülecek olan nesne")]
    public GameObject targetObject;
    [Tooltip("Kameranın Dönme Hızını belirten slider")]
    public Slider mouseSensivitySlider;
    [Tooltip("Kameranın Dönme Hızı")]
    public float mouseSensivity = 100f;
    [Range(.1f, 25)]
    [Tooltip("Kamera ve nesne arasındaki Başlangıç mesafesi")]
    public float rigCamDistance = 3;

    bool rotating = false;
    GameManager3DV GameManager;

    GameObject clickedObject;

    void Start()
    {
        GameManager = GameObject.Find("GameManager").GetComponent<GameManager3DV>();
        if (mainCamera == null) mainCamera = Camera.main.gameObject;
        // rigCamDistance = Vector3.Distance(targetObject.transform.position, mainCamera.transform.position);

        mouseSensivitySlider.value = mouseSensivity;

        // look at the center
        rotateCameraAroundPoint(mainCamera.transform, targetObject.transform.position, 0, 0, rigCamDistance);
    }

    void Update()
    {
        // /* For Mobile */
        if (Input.touchCount >= 1)
        {
            if (Input.touches[0].phase == TouchPhase.Began)
            {
                Ray aim = mainCamera.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(aim, out RaycastHit hit))
                {
                    if (hit.transform.gameObject.layer != 5)
                    {
                        rotating = true;
                    }
                }
                else
                {
                    rotating = true;
                }
            }
            if (Input.touches[0].phase == TouchPhase.Ended)
            {
                rotating = false;
            }
        }
        // /* For Pc */
        // if (Input.GetMouseButtonDown(0))
        // {

        //     Ray aim = mainCamera.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
        //     if (Physics.Raycast(aim, out RaycastHit hit))
        //     {
        //         if (hit.transform.gameObject.layer != 5)
        //         {
        //             rotating = true;
        //         }
        //     }
        //     else
        //     {
        //         rotating = true;
        //     }
        // }
        // if (Input.GetMouseButtonUp(0))
        // {
        //     rotating = false;
        // }
        if (Input.mouseScrollDelta != Vector2.zero && GameManager.activeModel)
        {
            float maxZoom = GameManager.activeModel.minMaxZoomDistance.y;
            float minZoom = GameManager.activeModel.minMaxZoomDistance.x;

            if (Input.mouseScrollDelta.y < 0 && rigCamDistance < maxZoom)
                rigCamDistance += Input.mouseScrollDelta.y * rigCamDistance / -20;
            if (Input.mouseScrollDelta.y > 0 && rigCamDistance > minZoom)
                rigCamDistance += Input.mouseScrollDelta.y * rigCamDistance / -20;

            recalculateDistance(rigCamDistance, mainCamera);
        }

        if (rotating)
        {
            float mouseX = Mathf.Clamp(Input.touches[0].deltaPosition.x, -1, 1) * mouseSensivity * Time.deltaTime;
            float mouseY = Mathf.Clamp(Input.touches[0].deltaPosition.y, -1, 1) * mouseSensivity * Time.deltaTime;
            
            //Debug.Log($"x: {mouseX} y: {mouseY}");

            rotateCameraAroundPoint(mainCamera.transform, targetObject.transform.position, mouseX, mouseY, rigCamDistance);
        }

    }

    public void recalculateDistance(float distace, GameObject camera)
    {
        rigCamDistance = distace;
        float xRotation = camera.transform.rotation.eulerAngles.x;
        float yRotation = camera.transform.rotation.eulerAngles.y;

        Quaternion cameraRotation = Quaternion.Euler(xRotation, yRotation, 0f);

        camera.transform.position = cameraRotation * (Vector3.back * rigCamDistance + targetObject.transform.position);
        mainCamera.transform.position = camera.transform.position;
    }
    
    public void rotateCameraAroundPoint(Transform cam, Vector3 targetPoint, float xAngle, float yAngle, float distaceFromPoint)
    {
        float xRotation = cam.rotation.eulerAngles.x;
        float yRotation = cam.rotation.eulerAngles.y;
        float threshold = .5f;
        xRotation -= yAngle;
        yRotation += xAngle;
        Quaternion cameraRotation = Quaternion.Euler(xRotation, yRotation, 0f);
        Vector3 cameraPosition = cameraRotation * (Vector3.back * distaceFromPoint + targetPoint);
        float distance = Vector3.Distance(new Vector3(0, cameraPosition.y, 0), cameraPosition);
        if (distance < threshold) return;
        cam.localRotation = cameraRotation;
        cam.position = cameraPosition;
    }
    public void changeMouseSensivity()
    {
        rotating = false;
        mouseSensivity = mouseSensivitySlider.value;
    }
}
