using UnityEngine;

namespace KiliWare.SampleVRMApp
{
    public class VRMSeparateLoadManager : VRMLoadManager
    {
        protected GameObject _leftInstantiatedVRM;
        protected GameObject _rightInstantiatedVRM;
        public void OnLeftSelectButtonClicked()
        {
            OnModelLoaded += OnLeftVRMSelected;
            OnLoadButtonClicked();
        }

        public void OnRightSelectButtonClicked()
        {
            OnModelLoaded += OnRightVRMSelected;
            OnLoadButtonClicked();
        }

        protected void OnLeftVRMSelected(GameObject model)
        {
            OnModelLoaded -= OnLeftVRMSelected;
            Debug.Log("Left VRM has been loaded");
            model.transform.position = new Vector3(1, 0, 0);

            if (_leftInstantiatedVRM)
            {
                Destroy(_leftInstantiatedVRM);
            }
            _leftInstantiatedVRM = model;
        }

        protected void OnRightVRMSelected(GameObject model)
        {
            OnModelLoaded -= OnRightVRMSelected;
            Debug.Log("Right VRM has been loaded");
            model.transform.position = new Vector3(-1, 0, 0);

            if (_rightInstantiatedVRM)
            {
                Destroy(_rightInstantiatedVRM);
            }
            _rightInstantiatedVRM = model;
        }
    }
}