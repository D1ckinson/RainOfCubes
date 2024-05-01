using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(LifeManager))]
[RequireComponent(typeof(CollisionHandler))]
public class Cube : MonoBehaviour
{
    private LifeManager _lifeManager;
    private UnityAction _disable;

    private void Awake() =>
        _lifeManager = GetComponent<LifeManager>();

    public void SetDisableAction(UnityAction disable)
    {
        _disable = disable;
        _lifeManager.LifeOver += _disable;
    }

    private void OnDisable() =>
        _lifeManager.LifeOver -= _disable;
}
