using System.Collections;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Terrain))]
[RequireComponent(typeof(BombSpawner))]
public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private float _spawnTime;
    [SerializeField] private Cube _prefab;
    [SerializeField] private TMP_Text _text;

    private Bounds _bounds;
    private Pool<Cube> _pool;
    private int _preloadCount = 3;
    private float _yOffset = 10;
    private BombSpawner _bombSpawner;

    private void Awake()
    {
        _bounds = GetComponent<TerrainCollider>().bounds;
        _pool = new(PreloadFunc, GetAction, ReturnAction, _preloadCount);
        _bombSpawner = GetComponent<BombSpawner>();
    }

    private void Start() =>
        StartCoroutine(Spawn());

    private void Update()
    {
        _text.text = $"Кубы\n" +
            $"всего:{_pool.TotalSpawns}\n" +
            $"сейчас:{_pool.CurrentSpawns} ";
    }

    private Vector3 GetSpawnPoint()
    {
        float x = Random.Range(_bounds.min.x, _bounds.max.x);
        float y = Random.Range(_bounds.min.y, _bounds.max.y) + _yOffset;
        float z = Random.Range(_bounds.min.z, _bounds.max.z);

        return new Vector3(x, y, z);
    }

    private IEnumerator Spawn()
    {
        WaitForSeconds wait = new(_spawnTime);

        while (true)
        {
            Cube cube = _pool.Get();

            cube.transform.position = GetSpawnPoint();
            cube.SetDisableAction(() =>
            {
                _pool.Return(cube);
                _bombSpawner.Spawn(cube.transform.position);
            });

            yield return wait;
        }
    }

    private Cube PreloadFunc() =>
        Instantiate(_prefab);

    private void GetAction(Cube cube) =>
        cube.gameObject.SetActive(true);

    private void ReturnAction(Cube cube) =>
        cube.gameObject.SetActive(false);
}
