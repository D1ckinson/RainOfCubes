using System.Collections;
using UnityEngine;
using UnityEngine.Events;


public class LifeManager : MonoBehaviour
{
    public event UnityAction LifeOver;

    private float _minLifeTime = 2;
    private float _maxLifeTime = 5;

    private void OnEnable() =>
        StartCoroutine(StartLifeTimer());

    private IEnumerator StartLifeTimer()
    {
        float lifeTime = Random.Range(_minLifeTime, _maxLifeTime);

        yield return new WaitForSeconds(lifeTime);

        LifeOver?.Invoke();
    }
}
