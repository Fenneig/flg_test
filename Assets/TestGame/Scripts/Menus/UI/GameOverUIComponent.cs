using TestGame.Scripts.Model;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace TestGame.Scripts.Menus.UI
{
    public class GameOverUIComponent : MonoBehaviour
    {
        [Header("Text")] 
        [SerializeField] private Text _gameOverReasonText;
        [Header("Buttons")]
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _exitButton;

        private void Awake()
        {
            _restartButton.onClick.AddListener(OnRestart);
            _exitButton.onClick.AddListener(OnExit);
            GameSession.Instance.GameOverReason.OnChanged +=
                (newValue, _) => _gameOverReasonText.text = newValue;
        }
        
        private void OnRestart()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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
            _restartButton.onClick.RemoveListener(OnRestart);
            _exitButton.onClick.RemoveListener(OnExit);
        }
    }
}