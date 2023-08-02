using System.Globalization;
using TestGame.Scripts.Model.Data.Properties;
using UnityEngine;
using UnityEngine.UI;

namespace TestGame.Scripts.Menus.UI
{
    public class AudioSettingsWidget : MonoBehaviour
    {
        [SerializeField] private Slider _slider;
        [SerializeField] private Text _value;

        private FloatPersistentProperty _model;

        private void Start()
        {
            _slider.onValueChanged.AddListener(OnSliderValueChange);
        }

        private void OnSliderValueChange(float value)
        {
            _model.Value = value;
        }

        public void SetModel(FloatPersistentProperty model)
        {
            _model = model;
            _model.OnChanged += OnValueChanged;
            OnValueChanged(_model.Value, _model.Value);
        }

        private void OnValueChanged(float newValue, float oldValue)
        {
            var valueText = Mathf.Round(newValue * 100f);
            _value.text = valueText.ToString(CultureInfo.InvariantCulture);
            _slider.normalizedValue = newValue;
        }

        private void OnDestroy()
        {
            _slider.onValueChanged.RemoveListener(OnSliderValueChange);
            _model.OnChanged -= OnValueChanged;
        }
    }
}