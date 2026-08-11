using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ObjectPoolManager : MonoBehaviour
{
    public static ObjectPoolManager Instance { get; private set; }

    [Header("Pool Settings")]
    [SerializeField] private bool _addDontDestroyOnLoad = false;
    [SerializeField] private int _defaultCapacity = 10;
    [SerializeField] private int _maxSize = 100;

    private Transform _emptyHolder;
    private Transform _particleSystemHolder;
    private Transform _gameObjectsHolder;
    private Transform _soundFXHolder;

    // Prefab -> Pool
    private readonly Dictionary<GameObject, ObjectPool<GameObject>> _objectPools = new();

    // Clone -> Pool
    private readonly Dictionary<GameObject, ObjectPool<GameObject>> _cloneToPool = new();


    public enum PoolCategory
    {
        ParticleSystem,
        GameObjects,
        SoundFX
    }


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        if (_addDontDestroyOnLoad)
            DontDestroyOnLoad(gameObject);

        SetUpHolders();
    }


    private void SetUpHolders()
    {
        _emptyHolder = new GameObject("Object Pools").transform;
        _particleSystemHolder = CreateHolder("Particle Effects", _emptyHolder);
        _gameObjectsHolder = CreateHolder("GameObjects", _emptyHolder);
        _soundFXHolder = CreateHolder("Sound FX", _emptyHolder);
    }


    private Transform CreateHolder(string holderName, Transform parent)
    {
        GameObject holder = new GameObject(holderName);
        holder.transform.SetParent(parent);
        return holder.transform;
    }

    private ObjectPool<GameObject> CreatePool(GameObject prefab, PoolCategory poolCategory)
    {
        if (prefab == null)
        {
            Debug.LogError("Cannot create pool. Prefab is null.");
            return null;
        }

        if (_objectPools.TryGetValue(prefab, out ObjectPool<GameObject> existingPool))
            return existingPool;

        Transform parent = GetParentByCategory(poolCategory);

        ObjectPool<GameObject> pool = new ObjectPool<GameObject>(
            createFunc: () => CreateObject(prefab, parent),
            actionOnGet: OnGetObject,
            actionOnRelease: OnReleaseObject,
            actionOnDestroy: OnDestroyObject,
            collectionCheck: true,
            defaultCapacity: _defaultCapacity,
            maxSize: _maxSize
        );

        _objectPools.Add(prefab, pool);

        return pool;
    }

    private GameObject CreateObject(GameObject prefab, Transform parent)
    {
        GameObject obj = Instantiate(prefab, parent);
        obj.SetActive(false);
        return obj;
    }

    private void OnGetObject(GameObject obj)
    {
        if (obj == null) return;

        obj.SetActive(true);

        if (obj.TryGetComponent<IPoolable>(out IPoolable poolable))
            poolable.OnSpawn();
    }

    private void OnReleaseObject(GameObject obj)
    {
        if (obj == null) return;

        if (obj.TryGetComponent<IPoolable>(out IPoolable poolable))
            poolable.OnDespawn();

        obj.SetActive(false);
    }

    private void OnDestroyObject(GameObject obj)
    {
        if (obj == null) return;

        _cloneToPool.Remove(obj);
        Destroy(obj);
    }

    private Transform GetParentByCategory(PoolCategory poolCategory)
    {
        switch (poolCategory)
        {
            case PoolCategory.ParticleSystem: return _particleSystemHolder;
            case PoolCategory.GameObjects: return _gameObjectsHolder;
            case PoolCategory.SoundFX: return _soundFXHolder;
            default: return _gameObjectsHolder;
        }
    }

    public T SpawnObject<T>(T prefab, Vector3 spawnPosition, Quaternion spawnRotation,
        PoolCategory poolCategory = PoolCategory.GameObjects) where T : Component
    {
        if (prefab == null)
        {
            Debug.LogError("Cannot spawn object. Prefab is null.");
            return null;
        }

        GameObject prefabObject = prefab.gameObject;
        ObjectPool<GameObject> pool = GetOrCreatePool(prefabObject, poolCategory);
        GameObject obj = pool.Get();

        if (obj == null)
        {
            Debug.LogError($"Failed to spawn {prefab.name}");
            return null;
        }

        if (!_cloneToPool.ContainsKey(obj))
            _cloneToPool.Add(obj, pool);

        obj.transform.SetPositionAndRotation(spawnPosition, spawnRotation);

        T component = obj.GetComponent<T>();

        if (component == null)
        {
            Debug.LogError($"Object {prefab.name} doesn't have component of type {typeof(T)}");
            pool.Release(obj);
            return null;
        }

        return component;
    }

    public GameObject SpawnObject(GameObject prefab, Vector3 spawnPosition, Quaternion spawnRotation,
        PoolCategory poolCategory = PoolCategory.GameObjects)
    {
        if (prefab == null)
        {
            Debug.LogError("Cannot spawn object. Prefab is null.");
            return null;
        }

        ObjectPool<GameObject> pool = GetOrCreatePool(prefab, poolCategory);
        GameObject obj = pool.Get();

        if (obj == null) return null;

        if (!_cloneToPool.ContainsKey(obj))
            _cloneToPool.Add(obj, pool);

        obj.transform.SetPositionAndRotation(spawnPosition, spawnRotation);

        return obj;
    }

    private ObjectPool<GameObject> GetOrCreatePool(GameObject prefab, PoolCategory poolCategory)
    {
        if (_objectPools.TryGetValue(prefab, out ObjectPool<GameObject> pool))
            return pool;

        return CreatePool(prefab, poolCategory);
    }

    public void ReturnToPool(GameObject obj)
    {
        if (obj == null) return;

        if (!_cloneToPool.TryGetValue(obj, out ObjectPool<GameObject> pool))
        {
            Debug.LogWarning($"Trying to return {obj.name} which is not pooled.");
            return;
        }

        pool.Release(obj);
    }

    public void ClearPool(GameObject prefab)
    {
        if (prefab == null) return;

        if (_objectPools.TryGetValue(prefab, out ObjectPool<GameObject> pool))
        {
            pool.Clear();
            _objectPools.Remove(prefab);
        }
    }

    public void ClearAllPools()
    {
        foreach (ObjectPool<GameObject> pool in _objectPools.Values)
            pool.Clear();

        _objectPools.Clear();
        _cloneToPool.Clear();
    }


    public int GetActiveCount(GameObject prefab)
    {
        if (_objectPools.TryGetValue(prefab, out ObjectPool<GameObject> pool))
            return pool.CountActive;

        return 0;
    }


    public int GetInactiveCount(GameObject prefab)
    {
        if (_objectPools.TryGetValue(prefab, out ObjectPool<GameObject> pool))
            return pool.CountInactive;

        return 0;
    }
}
