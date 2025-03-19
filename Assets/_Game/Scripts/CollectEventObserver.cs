using System;
using UnityEngine;

namespace Game.Entity.Movement
{
    public sealed class CollectEventObserver : MonoBehaviour
    {
        public event Action Triggered;

        private void OnCollect() => 
            Triggered?.Invoke();
    }
}