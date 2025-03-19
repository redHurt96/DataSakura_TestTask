using System;
using Cinemachine;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Game.Entity.Movement
{
    public class CameraShake : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera _cinemachineVirtualCamera;
        [SerializeField] private CollectEventObserver _collectEventObserver;
        [SerializeField] private float _intensity;
        [SerializeField] private float _shakeTime;

        private void Start() => 
            _collectEventObserver.Triggered += Shake;

        private void OnDestroy() => 
            _collectEventObserver.Triggered -= Shake;

        private async void Shake()
        {
            var perlin = _cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
            
            perlin.m_AmplitudeGain = _intensity;

            await UniTask.Delay(TimeSpan.FromSeconds(_shakeTime));
            
            perlin.m_AmplitudeGain = 0f;
        }
    }
}