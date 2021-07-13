using UnityEngine;
using UnityEngine.EventSystems;

public class RotateScript : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{

    private enum RotateDirection { right, left }
    [SerializeField] private RotateDirection rotateDirection;
    [SerializeField] private Transform _rotateTarget;
    [SerializeField] private float _rotateSpeed = 8f;
    [SerializeField] private float _scaleChangeAmount;
    private bool _rotateRight = false; 
    private bool _rotateLeft = false;

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        transform.localScale *= _scaleChangeAmount;
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        transform.localScale /= _scaleChangeAmount;
    }

    public void OnPointerDown(PointerEventData pointerEventData)
    {
        Debug.Log("OnPointerDown");
        if (rotateDirection == RotateDirection.right)
        {
            _rotateRight = true;
        }

        else
        {
            _rotateLeft = true;
        }
    }

    public void OnPointerUp(PointerEventData pointerEventData)
    {
        if (rotateDirection == RotateDirection.right)
        {
            _rotateRight = false;
        }

        else
        {
            _rotateLeft = false;
        }
    }

    private void Update()
    {
        if (_rotateRight)
        {
            Debug.Log("RotateTarget");
            _rotateTarget.eulerAngles = new Vector3(
                _rotateTarget.eulerAngles.x,
                _rotateTarget.eulerAngles.y + _rotateSpeed,
                _rotateTarget.eulerAngles.z);
        }

        else if(_rotateLeft)
        {
            _rotateTarget.eulerAngles = new Vector3(
                _rotateTarget.eulerAngles.x, 
                _rotateTarget.eulerAngles.y - _rotateSpeed, 
                _rotateTarget.eulerAngles.z);
        }
    }
}
