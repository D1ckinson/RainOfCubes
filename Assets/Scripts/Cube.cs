using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(LifeManager))]
[RequireComponent(typeof(Rigidbody))]
public class Cube : MonoBehaviour
{
    private LifeManager _lifeManager;
    private UnityAction _disable;
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _lifeManager = GetComponent<LifeManager>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void SetDisableAction(UnityAction disable)
    {
        _disable = disable;
        _lifeManager.LifeOver += _disable;
    }

    private void OnDisable()
    {
        _lifeManager.LifeOver -= _disable;
        _rigidbody.velocity = Vector3.zero;
    }
}
