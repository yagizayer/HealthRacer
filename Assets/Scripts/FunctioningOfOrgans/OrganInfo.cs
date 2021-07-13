using UnityEngine;

public class OrganInfo : MonoBehaviour
{
    [SerializeField] private float _scaleChangeAmount;
    [SerializeField] private TextAsset _textAsset;
    [SerializeField] private float _positionChangeAmount;
    [SerializeField] private string _organName;

    private void OnMouseDown()
    {
        Controller.Instance.GiveInfo(_organName, _textAsset);
    }

    private void OnMouseEnter()
    {
        Selected(true);
    }

    private void OnMouseExit()
    {
        Selected(false);
    }

    private void Selected(bool selected)
    {
        if (selected)
        {
            transform.localScale *= _scaleChangeAmount;
            transform.position += new Vector3(0f, 0f, _positionChangeAmount);
        }
        else
        {
            transform.localScale /= _scaleChangeAmount;
            transform.position -= new Vector3(0f, 0f, _positionChangeAmount);
        }
    }
}
