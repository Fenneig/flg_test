using TestGame.Scripts.Interfaces;
using UnityEngine;

namespace TestGame.Scripts.Environment
{
    public class Exit : MonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D other)
        {
            Debug.Log(other.gameObject.name);
            if (other.gameObject.TryGetComponent<IExitable>(out var exitComponent))
            {
                exitComponent.Exit();
            }
        }
    }
}