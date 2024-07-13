using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(LifeManager))]
[RequireComponent(typeof(Rigidbody))]
public class Bomb : MonoBehaviour
{
    [SerializeField] private float _force;
    [SerializeField] private float _radius;

    private LifeManager _lifeManager;
    private UnityAction _disable;
    private Material _material;
    private Rigidbody _rigidbody;
    private Color _defaultColor = Color.black;
    private Color _finaleColor = Color.clear;

    public void SetDisableAction(UnityAction disable)
    {
        _disable = disable;
        _lifeManager.LifeOver += _disable;
    }

    public void Explode() =>
        GetExplodableObjects().ForEach(item => item.AddExplosionForce(_force, transform.position, _radius));

    private void Awake()
    {
        _lifeManager = GetComponent<LifeManager>();
        _material = GetComponent<MeshRenderer>().material;
        _rigidbody = GetComponent<Rigidbody>();

        SetDefaultColor();
    }

    private void Update()
    {
        float ratio = 1f - _lifeManager.TimeLeft / _lifeManager.StartTime;
        _material.color = Color.Lerp(_defaultColor, _finaleColor, ratio);
    }

    private void OnEnable()
    {
        _lifeManager.LifeOver += Explode;
        SetDefaultColor();
    }

    private void OnDisable()
    {
        _lifeManager.LifeOver -= _disable;
        _lifeManager.LifeOver -= Explode;
        _rigidbody.velocity = Vector3.zero;
    }

    private void SetDefaultColor() =>
        _material.color = _defaultColor;

    private List<Rigidbody> GetExplodableObjects()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, _radius);

        return hits.Where(hit => hit.attachedRigidbody != null).Select(hit => hit.attachedRigidbody).ToList();
    }
}
