using TestGame.Scripts.Model.Data.Properties;
using TestGame.Scripts.Pause;
using TestGame.Scripts.Utils;
using UnityEngine;

namespace TestGame.Scripts.Model
{
    public class GameSession : MonoBehaviour
    {
        public PauseHandler PauseHandler { get; private set; }

        public static GameSession Instance;

        public StringProperty GameOverReason { get; private set; }

        private const string PAUSE_UI_PATH = "UI/Windows/PauseUI";
        private const string GAME_OVER_UI_PATH = "UI/Windows/GameOverUI";

        private void Awake()
        {
            Instance = this;
            InitModels();
        }

        private void InitModels()
        {
            GameOverReason = new StringProperty();
            PauseHandler = new PauseHandler();
        }

        private void Update()
        {
            if (!PauseHandler.IsPaused && Input.GetKeyDown(KeyCode.Escape)) SetPause(true);
        }

        public void SetPause(bool isPaused)
        {
            PauseHandler.SetPause(isPaused);
            if (isPaused) WindowUtils.CreateWindow(PAUSE_UI_PATH);
        }

        public void GameOver(string reason)
        {
            PauseHandler.SetPause(true);
            WindowUtils.CreateWindow(GAME_OVER_UI_PATH);
            GameOverReason.Value = reason;
        }
    }
}