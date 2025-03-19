using System.Linq;
using UniRx;
using Unity.Mathematics;
using UnityEngine;

namespace Game.Entity.Movement
{
    public sealed class CollectUiEffectController : MonoBehaviour
    {
        [SerializeField] private CollectUiEffectConfig[] _configs;
        [SerializeField] private CollectUiEffect _prefab;
        [SerializeField] private Transform _parent;
        
        private Camera _camera;

        private void Start()
        {
            _camera = Camera.main;
            MessageBroker.Default
                .Receive<CollectMessage>()
                .Subscribe(PlayEffect)
                .AddTo(this);
        }

        private void PlayEffect(CollectMessage message)
        {
            Vector2 position = _camera.WorldToScreenPoint(message.Position);
            CollectUiEffect instance = Instantiate(_prefab, _parent);
            CollectUiEffectConfig config = _configs.First(x => x.GameResourceType == message.GameResourceType);
            instance.Initialize(config.Icon, message.Amount, position, config.Target.position, message.GameResourceType);
        }
    }
}