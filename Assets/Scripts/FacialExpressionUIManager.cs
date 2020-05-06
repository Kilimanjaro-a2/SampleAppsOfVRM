using UnityEngine;
using VRM;

namespace KiliWare.SampleVRMApp
{
    public class FacialExpressionUIManager : MonoBehaviour
    {
        [SerializeField] protected FacialExpressionController _facialExpressionController;
        [SerializeField] protected GameObject _contentsParent;
        [SerializeField] protected GameObject _facialExpressionButtonPrefab;
        
        void Awake()
        {
            var clips = _facialExpressionController.GetClips();
            if (clips != null && _contentsParent != null && _facialExpressionButtonPrefab != null)
            {
                foreach(var clip in clips)
                {
                    var button = Instantiate(_facialExpressionButtonPrefab);
                    var buttonScript = button.GetComponent<FacialExpressionButtonController>();
                    buttonScript.SetText(clip.name);
                    buttonScript.Preset = clip.Preset;
                    buttonScript.OnFacialExpressionButtonPressed += ChangeBlendShape;

                    button.transform.SetParent(_contentsParent.transform);
                }
            }
            else
            {
                if (clips == null)
                {
                    Debug.LogError("Couldn't get BlendShapeClips. Please check the codes on FacialExpressionController");
                }
                if (_contentsParent == null)
                {
                    Debug.LogError("Please set _contentsParent");
                }
                if (_facialExpressionButtonPrefab == null)
                {
                    Debug.LogError("Please set _facialExpressionButtonPrefab");
                }
            }
        }

        protected void ChangeBlendShape(BlendShapePreset preset)
        {
            _facialExpressionController.SetPreset(preset);
        }
    }
}