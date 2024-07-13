using UnityEngine;
using System.Collections.Generic;
using System;

public class Pool<T>
{
    private Func<T> _preloadFunc;
    private Action<T> _getAction;
    private Action<T> _returnAction;

    private Queue<T> _pool = new();
    private List<T> _active = new();

    public Pool(Func<T> preloadFunc, Action<T> getAction, Action<T> returnAction, int preloadCount)
    {
        _preloadFunc = preloadFunc;
        _getAction = getAction;
        _returnAction = returnAction;

        if (preloadFunc == null)
        {
            Debug.LogError("Preload Function is null");
            return;
        }

        for (int i = 0; i < preloadCount; i++)
            Return(preloadFunc());

        CurrentSpawns = 0;
    }

    public int TotalSpawns { get; private set; }
    public int CurrentSpawns { get; private set; }

    public T Get()
    {
        T item = _pool.Count > 0 ? _pool.Dequeue() : _preloadFunc();

        _getAction(item);
        _active.Add(item);

        TotalSpawns++;
        CurrentSpawns++;

        return item;
    }

    public void Return(T item)
    {
        _returnAction(item);

        _pool.Enqueue(item);
        _active.Remove(item);

        CurrentSpawns--;
    }

    public void ReturnAll() =>
        _active.ForEach(item => Return(item));
}
