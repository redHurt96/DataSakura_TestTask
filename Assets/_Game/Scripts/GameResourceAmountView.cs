using System;
using TMPro;
using UniRx;
using UnityEngine;

namespace Game.Entity.Movement
{
    public sealed class GameResourceAmountView : MonoBehaviour
    {
        [SerializeField] private GameResourceType gameResourceType;
        [SerializeField] private TMP_Text amountText;

        GameResourceService _gameResourceService;
        private void Start()
        {
            _gameResourceService = ServiceLocator.Instance.GameResourceService;
            _gameResourceService.GetResource(gameResourceType)
                .Subscribe(UpdateAmount)
                .AddTo(this);
        }
        
        private void UpdateAmount(int amount) => amountText.text = amount.ToString();
        
    }
}