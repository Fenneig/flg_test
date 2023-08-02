using TestGame.Scripts.Model.Data;
using UnityEngine;

namespace TestGame.Scripts.Menus.UI
{
    public class AudioSettingsUIComponent : MonoBehaviour
    {
        [Header("Widgets")]
        [SerializeField] private AudioSettingsWidget _masterWidget;
        [SerializeField] private AudioSettingsWidget _musicWidget;
        [SerializeField] private AudioSettingsWidget _effectsWidget;

        private void Awake()
        {
            _masterWidget.SetModel(GameSettings.I.Master);
            _musicWidget.SetModel(GameSettings.I.Music);
            _effectsWidget.SetModel(GameSettings.I.Effects);
        }
    }
}