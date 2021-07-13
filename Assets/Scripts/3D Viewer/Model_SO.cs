using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Model", menuName = "Model", order = 1)]
public class Model_SO : ScriptableObject
{
    public new string name;
    public Mesh mesh;
    [Range(.0001f, 500)]
    public float meshScale = 1f;
    public Mesh previewMesh;
    [Range(.0001f, 500)]
    public float meshPreviewScale = 1f;
    public Vector3 localPositionOffset = Vector3.zero;
    public Vector3 eulerRotation = Vector3.zero;
    public Vector3 rotationAxis = new Vector3(0, 1, 0);
    public List<Material> materials;
    public Vector2 minMaxZoomDistance = new Vector2(.1f, 15);
}
