using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleGameManager : MonoBehaviour
{
    [SerializeField] protected GameObject _loadVRMButton;
    [SerializeField] protected GameObject _gameStartButton;
    [SerializeField] protected RuntimeAnimatorController _animatorController;
    protected GameObject _playerModel;
    void Awake()
    {
        var loadingManager = GetComponent<VRMLoadManager>();
        loadingManager.OnModelLoaded += Initialize;
    }

    protected void Initialize(GameObject model)
    {
        _playerModel = model;
        var script = _playerModel.AddComponent<PlayerController>();
        script.SetAnimatorController(_animatorController);

        _loadVRMButton.SetActive(false);
        _gameStartButton.SetActive(true);
    }
}
