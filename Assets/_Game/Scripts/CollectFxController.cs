using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Game.Entity.Movement
{
    public sealed class CollectFxController : MonoBehaviour
    {
        [SerializeField] private CollectResourceController _collectResourceController;
        [SerializeField] private CollectEventObserver _collectEventObserver;
        [SerializeField] private ParticleSystem _trail;
        [SerializeField] private ParticleSystem _areaFx;
        [SerializeField] private float _fxPlayTime;

        private void Start()
        {
            _collectResourceController.Started += PlayTrailFx;
            _collectEventObserver.Triggered += PlayAreaFx;
        }

        private void OnDestroy()
        {
            _collectEventObserver.Triggered -= PlayAreaFx;
            _collectResourceController.Started -= PlayTrailFx;
        }

        private void PlayAreaFx() => 
            _areaFx.Play();

        private async void PlayTrailFx()
        {
            _trail.Play();

            await UniTask.Delay(TimeSpan.FromSeconds(_fxPlayTime));
            
            _trail.Stop();
        }
    }
}