using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game.Entity.Movement
{
    [Serializable]
    public sealed class CollectUiEffectConfig
    {
        public RectTransform Target;
        public Sprite Icon;
        public GameResourceType GameResourceType;
    }
}