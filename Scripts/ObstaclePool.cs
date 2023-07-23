using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclePool : MonoBehaviour
{
    private GameObject _columnPrefabs;
    private GameObject[] _columnPool;
    private int _poolSize = 5;
    private int _currentColIndex = 0;
    private float _spawnRate = 3f;
    private float _spawnPosX = 10f;
    private float _columnMaxPosY = 3f;
    private float _columnMinPosY = -0.5f;

    // Use this for initialization
    private void Start()
    {
        _columnPrefabs = Resources.Load("Columns") as GameObject;

        _columnPool = new GameObject[_poolSize];
        for (int i = 0; i < _columnPool.Length; i++)
        {
            _columnPool[i] = Instantiate(_columnPrefabs, new Vector2(-15f, -25f), Quaternion.identity);
        }
    }

    public void InvokeColumnSpawn()
    {
        InvokeRepeating("Spawn", 0f, _spawnRate);
    }

    private void Spawn()
    {
        if (GameManager.instance.IsGameOver)
        {
            return;
        }

        float _positionY = Random.Range(_columnMinPosY, _columnMaxPosY);

        _columnPool[_currentColIndex].transform.position = new Vector2(_spawnPosX, _positionY);
        _currentColIndex = (_currentColIndex + 1) % _poolSize;
    }
}