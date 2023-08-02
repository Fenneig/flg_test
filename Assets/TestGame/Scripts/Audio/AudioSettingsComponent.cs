using System;
using TestGame.Scripts.Model.Data;
using TestGame.Scripts.Model.Data.Properties;
using UnityEngine;

namespace TestGame.Scripts.Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioSettingsComponent : MonoBehaviour
    {
        [SerializeField] SoundSettings _mode;
        private AudioSource _source;
        private FloatPersistentProperty _model;

        public AudioSource Source => _source;

        private void Awake()
        {
            _source = GetComponent<AudioSource>();
            _model = FindProperty();
            _model.OnChanged += OnSoundSettingsChange;

            OnSoundSettingsChange(_model.Value, _model.Value);
        }

        private void OnSoundSettingsChange(float newValue, float oldValue)
        {
            _source.volume = newValue;
        }

        private FloatPersistentProperty FindProperty()
        {
            switch (_mode)
            {
                case SoundSettings.Master: return GameSettings.I.Master;
                case SoundSettings.Music: return GameSettings.I.Music;
                case SoundSettings.Effects: return GameSettings.I.Effects;
            }
            throw new ArgumentException("Undefined argument");
        }

        private void OnDestroy()
        {
            _model.OnChanged -= OnSoundSettingsChange;
        }

    }
}