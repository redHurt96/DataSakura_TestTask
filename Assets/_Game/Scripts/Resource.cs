using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UniRx;
using UnityEngine;

namespace Game.Entity.Movement
{
    [RequireComponent(typeof(Collider))]
    internal class Resource : MonoBehaviour
    {
        [SerializeField] private GameResourceType _gameResourceType;
        [SerializeField] private int _amount;
        [SerializeField] private Transform _model;
        [SerializeField] private ParticleSystem _fx;
        [SerializeField] private float _scaleFxDuration = .5f;
        [SerializeField] private float _restoreTime = 3f;
        [SerializeField] private float _restoreFxDuration = .5f;
        
        private Collider _collider;

        private void Start() => 
            _collider = GetComponent<Collider>();

        public void Collect()
        {
            _collider.enabled = false;
            PlayFx();
            Restore();
        }

        private void PlayFx()
        {
            _fx.Play();
            _model
                .DOScale(Vector3.zero, _scaleFxDuration)
                .SetEase(Ease.InBack);
            
            MessageBroker.Default.Publish(new CollectMessage
            {
                Amount = _amount,
                GameResourceType = _gameResourceType,
                Position = transform.position,
            });
        }

        private async UniTask Restore()
        {
            await UniTask.Delay(TimeSpan.FromSeconds(_restoreTime));
            _model
                .DOScale(Vector3.one, _restoreFxDuration)
                .SetEase(Ease.OutBack);
            _collider.enabled = true;
        }
    }
}