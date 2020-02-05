using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class VehicleSpawnPoint : MonoBehaviour
{
    public GameObject _prefab;

    public bool _running = true;

    [HideInInspector]
    public List<GameObject> _pool = new List<GameObject>();

    public int _poolMax = 5;

    [HideInInspector]
    public int _poolIndex = 0;

    protected List<IAction> Actions { get; } = new List<IAction>();

    IEnumerator Start()
    {
        var index = 0;
        while (_running && Actions.Count > 0)
        {
            var action = Actions[index];
            yield return action.Execute();
            index++;
            index %= Actions.Count;
        }
    }

    public void GameOver()
    {
        _running = false;
        foreach (var go in _pool)
        {
            Destroy(go);
        }
    }

    public GameObject GetNextVehicle()
    {
        if (_pool.Count < _poolMax)
        {
            _pool.Add(Instantiate(_prefab) as GameObject);
        }

        var instance = _pool[_poolIndex];
        _poolIndex++;
        _poolIndex %= _poolMax;
        return instance;
    }
}
