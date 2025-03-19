using UnityEngine;

namespace Game.Entity.Movement
{
    internal struct CollectMessage
    {
        public GameResourceType GameResourceType;
        public int Amount;
        public Vector3 Position;
    }
}