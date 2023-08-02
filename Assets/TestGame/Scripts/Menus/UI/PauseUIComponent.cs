using TestGame.Scripts.Model;
using UnityEngine;
using UnityEngine.UI;

namespace TestGame.Scripts.Menus.UI
{
    public class PauseUIComponent : MonoBehaviour
    {
        [Header("Buttons")]
        [SerializeField] private Button _returnButton;
        [SerializeField] private Button _exitButton;

        private void Start()
        {
            _returnButton.onClick.AddListener(OnReturn);
            _exitButton.onClick.AddListener(OnExit);
        }

        private void OnReturn()
        {
            GameSession.Instance.SetPause(false);
            Destroy(gameObject);
        }

        private void OnExit()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }

        private void OnDestroy()
        {
            _returnButton.onClick.RemoveListener(OnReturn);
            _exitButton.onClick.RemoveListener(OnExit);
        }
    }
}