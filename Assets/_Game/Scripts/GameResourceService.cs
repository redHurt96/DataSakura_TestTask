using System;
using UniRx;
using UnityEngine;

namespace Game.Entity.Movement
{
    public sealed class GameResourceService : MonoBehaviour
    {
        public IntReactiveProperty Wood { get; } = new(0);
        public IntReactiveProperty Stone { get; } = new(0);
        
        public IObservable<int> GetResource(GameResourceType type) =>
            type switch
            {
                GameResourceType.Wood => Wood,
                GameResourceType.Stone => Stone,
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
            };
    }
}