using UnityEngine;

namespace KiliWare.SampleVRMApp
{
    public class DescriptionUIManager : MonoBehaviour
    {
        [SerializeField] protected GameObject _description;
        protected GameObject _instantiated;
        void Awake()
        {
            var loadingManager = GetComponent<VRMLoadManager>();
            loadingManager.OnModelLoaded += EnableDescription;
        }
        protected void EnableDescription(GameObject _)
        {
            if (_description == null)
            {
                return;
            }
            _description.SetActive(true);
        }
    }
}