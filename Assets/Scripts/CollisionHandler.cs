using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(ColorChanger))]
public class CollisionHandler : MonoBehaviour
{
    private ColorChanger _colorChanger;

    private void Awake() =>
        _colorChanger = GetComponent<ColorChanger>();

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Painter painter) == false)
            return;

        _colorChanger.ChangeColor(painter.GetColor());
    }
}
