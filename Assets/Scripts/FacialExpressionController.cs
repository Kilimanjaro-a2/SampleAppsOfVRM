using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRM;

namespace KiliWare.SampleVRMApp
{
    public class FacialExpressionController : MonoBehaviour
    {
        protected VRMBlendShapeProxy _blendShapeProxy;
        public List<BlendShapeClip> GetClips()
        {
            if (_blendShapeProxy == null)
            {
                _blendShapeProxy = GetComponent<VRMBlendShapeProxy>();
            } 
            if (_blendShapeProxy.BlendShapeAvatar.Clips.Count == 0)
            {
                _blendShapeProxy.BlendShapeAvatar.CreateDefaultPreset();
            }
            return _blendShapeProxy.BlendShapeAvatar.Clips;
        }
        public void SetPreset(BlendShapePreset preset)
        {
            StartCoroutine(SetBlendShape(preset));
        }
        protected IEnumerator SetBlendShape(BlendShapePreset preset)
        {
            yield return new WaitForEndOfFrame();
            ResetBlendShape();
            yield return new WaitForEndOfFrame();
            _blendShapeProxy.ImmediatelySetValue(preset, 1.0f);
        }
        protected void ResetBlendShape()
        {
            foreach(var clip in _blendShapeProxy.BlendShapeAvatar.Clips)
            {
                _blendShapeProxy.ImmediatelySetValue(clip.Preset, 0f);
            }
        }
    }
}