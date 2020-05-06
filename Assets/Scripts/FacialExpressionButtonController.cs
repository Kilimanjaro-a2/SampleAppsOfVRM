using System;
using UnityEngine;
using VRM;
using UnityEngine.UI;

namespace KiliWare.SampleVRMApp
{
    public class FacialExpressionButtonController : MonoBehaviour
    {
        public BlendShapePreset Preset;
        public event Action<BlendShapePreset> OnFacialExpressionButtonPressed;

        public void SetText(string text)
        {
            GetComponentInChildren<Text>().text = text;
        }

        public void OnButtonPressed()
        {
            OnFacialExpressionButtonPressed?.Invoke(Preset);
        }
    }
}