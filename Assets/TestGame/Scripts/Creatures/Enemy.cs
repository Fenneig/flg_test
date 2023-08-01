using Spine;
using UnityEngine;
using Event = Spine.Event;

namespace TestGame.Scripts.Creatures
{
    public class Enemy : Character
    {
        protected override void HandleAnimationState(TrackEntry trackentry, Event e)
        {
            Debug.Log($"Enemy animation get {e.Data.Name} state");
        }
        
        protected override void Attack()
        {
            Debug.Log("Attack");
        }

        public override void Hit()
        {
            Destroy(gameObject);
        }
    }
}