using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace KiliWare.SampleVRMApp
{
    public class SimpleGameManager : MonoBehaviour
    {
        [SerializeField] protected GameObject _loadVRMButton;
        [SerializeField] protected GameObject _gameStartButton;
        [SerializeField] protected GameObject _deadMessage;
        [SerializeField] protected Text _scoreText;
        [SerializeField] protected RuntimeAnimatorController _animatorController;
        [SerializeField] protected float _stageLimitX = 4f;
        protected PlayerController _playerController;
        protected GameObject _playerModel;
        protected bool _isGameStarted = false;
        protected int _destroyCount = 0;
        void Awake()
        {
            var loadingManager = GetComponent<VRMLoadManager>();
            loadingManager.OnModelLoaded += InitializeAfterModelLoaded;   
        }

        void Update()
        {
            if(Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene("6. SimpleGame");
            }
        }

        // Will be called from a "GameStart" button.
        public void StartGame()
        {
            _isGameStarted = true;
            _gameStartButton.SetActive(false);
            StartCoroutine(SpawnEnemyCoroutine());
        }

        protected void EndGame()
        {
            _isGameStarted = false;
            _deadMessage.SetActive(true);
        }

        protected void InitializeAfterModelLoaded(GameObject model)
        {
            _playerModel = model;
            _playerController = _playerModel.AddComponent<PlayerController>();
            _playerController.SetAnimatorController(_animatorController);
            _playerController.OnDead += EndGame;

            _loadVRMButton.SetActive(false);
            _gameStartButton.SetActive(true);
        }

        protected IEnumerator SpawnEnemyCoroutine()
        {
            while(_isGameStarted)
            {
                yield return new WaitForSeconds(Random.Range(0.5f, 4f));
                var go = Instantiate(Resources.Load("EnemyCube")) as GameObject;
                var range = Random.Range(0, 2);
                var spawnPos = new Vector3(_stageLimitX, 1, 0);
                var script = go.GetComponent<CubeController>();
                script.OnBeDestroid += OnCubeDestroid;
                if (range < 1)
                {
                    spawnPos.x = -_stageLimitX;
                    script.SetDirection(false);
                    script.SetSpeed(Random.Range(1.5f, 4f));
                }
                go.transform.position = spawnPos;
            }
        }

        protected void OnCubeDestroid(CubeController cs)
        {
            cs.OnBeDestroid -= OnCubeDestroid;
            _destroyCount++;
            _scoreText.text = "Score: " + _destroyCount.ToString();
        }
    }
}