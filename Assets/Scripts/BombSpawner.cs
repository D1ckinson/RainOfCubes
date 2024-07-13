using TMPro;
using UnityEngine;

public class BombSpawner : MonoBehaviour
{
    [SerializeField] private float _spawnTime;
    [SerializeField] private Bomb _prefab;
    [SerializeField] private TMP_Text _text;

    private Pool<Bomb> _pool;
    private int _preloadCount = 3;

    private void Awake() =>
        _pool = new(PreloadFunc, GetAction, ReturnAction, _preloadCount);

    private void Update()
    {
        _text.text = $"Бомбы\n" +
            $"всего:{_pool.TotalSpawns}\n" +
            $"сейчас:{_pool.CurrentSpawns} ";
    }

    public void Spawn(Vector3 spawnPoint)
    {
        Bomb bomb = _pool.Get();

        bomb.transform.position = spawnPoint;
        bomb.SetDisableAction(() => _pool.Return(bomb));
    }

    private Bomb PreloadFunc() =>
        Instantiate(_prefab);

    private void GetAction(Bomb cube) =>
        cube.gameObject.SetActive(true);

    private void ReturnAction(Bomb cube) =>
        cube.gameObject.SetActive(false);
}
