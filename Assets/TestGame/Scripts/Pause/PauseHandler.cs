using System.Collections.Generic;
using TestGame.Scripts.Interfaces;

namespace TestGame.Scripts.Pause
{
    public class PauseHandler : IPausable
    {
        private readonly List<IPausable> _handlers;
        
        public bool IsPaused { get; private set; }
        
        public PauseHandler()
        {
            _handlers = new List<IPausable>();
        }
        
        public void Register(IPausable handler)
        {
            _handlers.Add(handler);
        }

        public void Unregister(IPausable handler)
        {
            if (_handlers.Contains(handler)) 
                _handlers.Remove(handler);
        }
        
        public void SetPause(bool isPaused)
        {
            IsPaused = isPaused;
            
            foreach (var handler in _handlers)
                handler.SetPause(isPaused);
        }
    }
}