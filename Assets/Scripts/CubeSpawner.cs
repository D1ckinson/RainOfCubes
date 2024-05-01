using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Terrain))]
public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private float _spawnTime;
    [SerializeField] private Cube _prefab;

    private Bounds _bounds;
    private Pool<Cube> _pool;
    private int _preloadCount = 3;
    private float _yOffset = 10;

    private void Awake()
    {
        _bounds = GetComponent<TerrainCollider>().bounds;
        _pool = new(PreloadFunc, GetAction, ReturnAction, _preloadCount);
    }

    private void Start() =>
        StartCoroutine(Spawn());

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
            cube.SetDisableAction(() => _pool.Return(cube));

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
