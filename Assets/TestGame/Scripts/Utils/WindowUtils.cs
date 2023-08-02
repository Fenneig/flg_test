﻿using UnityEngine;

namespace TestGame.Scripts.Utils
{
    public static class WindowUtils
    {
        private static GameObject _window;
        private static Canvas _canvas;
        
        private static void CreateWindow()
        {
            Object.Instantiate(_window, _canvas.transform);
        }

        public static void CreateWindow(string resourcePath)
        {
            _window = Resources.Load<GameObject>(resourcePath);
            _canvas = GameObject.FindWithTag("MainUICanvas").GetComponent<Canvas>();
            
            CreateWindow();
        }
    }
}