using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class ColorChanger : MonoBehaviour
{
    private Material _material;
    private Color _defaultColor = Color.white;

    private bool _isColorDefault => _material.color == _defaultColor;

    private void Awake() =>
        _material = GetComponent<MeshRenderer>().material;

    private void OnEnable() =>
        _material.color = _defaultColor;

    public void ChangeColor(Color color)
    {
        if (_isColorDefault)
            _material.color = color;
    }
}
