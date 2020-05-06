using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SimpleGameManager : MonoBehaviour
{
    [SerializeField] protected GameObject _loadVRMButton;
    [SerializeField] protected GameObject _gameStartButton;
    [SerializeField] protected RuntimeAnimatorController _animatorController;
    [SerializeField] protected float _stageLimitX = 4f;
    protected GameObject _playerModel;
    void Awake()
    {
        var loadingManager = GetComponent<VRMLoadManager>();
        loadingManager.OnModelLoaded += Initialize;

        StartCoroutine(SpawnEnemyCoroutine());
    }

    protected void Initialize(GameObject model)
    {
        _playerModel = model;
        var script = _playerModel.AddComponent<PlayerController>();
        script.SetAnimatorController(_animatorController);

        _loadVRMButton.SetActive(false);
        _gameStartButton.SetActive(true);
    }

    protected IEnumerator SpawnEnemyCoroutine()
    {
        while(true)
        {
            yield return new WaitForSeconds(Random.Range(0.5f, 4f));
            var go = Instantiate(Resources.Load("EnemyCube")) as GameObject;
            var range = Random.Range(0, 2);
            var spawnPos = new Vector3(_stageLimitX, 1, 0);
            if (range < 1)
            {
                spawnPos.x = -_stageLimitX;
                go.GetComponent<CubeController>().SetDirection(false);
            }
            go.transform.position = spawnPos;
        }
    }
}
