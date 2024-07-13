using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class LifeManager : MonoBehaviour
{
    public event UnityAction LifeOver;

    private float _minLifeTime = 2;
    private float _maxLifeTime = 5;

    public float StartTime { get; private set; }
    public float TimeLeft { get; private set; }

    private void OnEnable()
    {
        StartTime = Random.Range(_minLifeTime, _maxLifeTime);
        TimeLeft = StartTime;

        StartCoroutine(StartLifeTimer());
    }

    private IEnumerator StartLifeTimer()
    {
        while (TimeLeft > 0)
        {
            TimeLeft -= Time.deltaTime;

            yield return null;
        }

        LifeOver?.Invoke();
    }
}
