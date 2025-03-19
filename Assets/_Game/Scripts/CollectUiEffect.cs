using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Entity.Movement
{
    public class CollectUiEffect : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private TextMeshProUGUI _amount;
        [SerializeField] private CanvasGroup _canvasGroup;

        [SerializeField] private float _flyDuration;

        public void Initialize(Sprite icon, int amount, Vector3 position, Vector3 target, GameResourceType gameResourceType)
        {
            _icon.sprite = icon;
            _amount.text = amount.ToString();
            RectTransform rectTransform = transform as RectTransform;
            rectTransform.anchoredPosition = position;
            
            Sequence sequence = DOTween.Sequence();
            sequence.Append(rectTransform
                .DOPath(new[] { target }, _flyDuration)
                .SetEase(Ease.InSine));
            sequence.Join(rectTransform.DOScale(Vector3.zero, _flyDuration).SetEase(Ease.InSine));
            sequence.Join(_canvasGroup.DOFade(0f, _flyDuration).SetEase(Ease.InSine));
            sequence.AppendCallback(() =>
            {
                AddResource(gameResourceType, amount);
                Destroy(gameObject);
            });
        }

        private void AddResource(GameResourceType gameResourceType, int amount)
        {
            GameResourceService resourcesService = ServiceLocator.Instance.GameResourceService;
            
            switch (gameResourceType)
            {
                case GameResourceType.Wood:
                    resourcesService.Wood.Value += amount;
                    break;
                case GameResourceType.Stone:
                    resourcesService.Stone.Value += amount;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}