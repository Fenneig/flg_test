using TestGame.Scripts.Model.Data.Properties;
using UnityEngine;

namespace TestGame.Scripts.Model.Data
{
    [CreateAssetMenu(menuName = "Data/GameSettings", fileName = "GameSettings")]
    public class GameSettings : ScriptableObject
    {
        [SerializeField] private FloatPersistentProperty _master;
        [SerializeField] private FloatPersistentProperty _music;
        [SerializeField] private FloatPersistentProperty _effects;

        public FloatPersistentProperty Master => _master;
        public FloatPersistentProperty Music => _music;
        public FloatPersistentProperty Effects => _effects;

        private static GameSettings _instance;
        public static GameSettings I => _instance == null ? LoadGameSettings() : _instance;
        
        private const float DEFAULT_SOUND_VALUE = .2f;

        private static GameSettings LoadGameSettings()
        {
            return _instance = Resources.Load<GameSettings>("GameSettings");
        }

        private void OnEnable()
        {
            _master = new FloatPersistentProperty(DEFAULT_SOUND_VALUE, SoundSettings.Master.ToString());
            _music = new FloatPersistentProperty(DEFAULT_SOUND_VALUE, SoundSettings.Music.ToString());
            _effects = new FloatPersistentProperty(DEFAULT_SOUND_VALUE, SoundSettings.Effects.ToString());
        }

        private void OnValidate()
        {
            _master.Validate();
            _music.Validate();
            _effects.Validate();
        }
    }

    public enum SoundSettings 
    {
        Master,
        Music,
        Effects
    }
}