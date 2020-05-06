using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace KiliWare.SampleVRMApp
{
    public class SceneController : MonoBehaviour
    {
        [SerializeField] protected List<string> _sceneNames;
        [SerializeField] protected int _currentIndex;
        [SerializeField] protected Text _currentSceneText;
        protected bool _hasPressed;
        void Awake()
        {
            _currentSceneText.text = _sceneNames[_currentIndex];
        }

        public void MovePrevious()
        {
            _currentIndex--;
            if (_currentIndex < 0)
            {
                _currentIndex = _sceneNames.Count - 1;
            }
            MoveScene(_currentIndex);
        }

        public void MoveNext()
        {
            _currentIndex++;
            if (_currentIndex >= _sceneNames.Count)
            {
                _currentIndex = 0;
            }
            MoveScene(_currentIndex);
        }


        protected void MoveScene(int index)
        {
            if (!_hasPressed)
            {
                _hasPressed = true;
                SceneManager.LoadScene(_sceneNames[index]);
            }
        }
    }
}