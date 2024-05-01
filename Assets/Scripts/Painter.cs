using UnityEngine;

public class Painter : MonoBehaviour
{
    private Color[] _colors;

    private void Awake() =>
        SetColors();

    public Color GetColor()
    {
        int index = Random.Range(0, _colors.Length);

        return _colors[index];
    }

    private void SetColors() =>
        _colors = new[]
        {
            Color.red,
            Color.green,
            Color.blue,
            Color.cyan,
            Color.magenta,
            Color.yellow
        };
}
