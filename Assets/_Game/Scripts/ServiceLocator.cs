using UnityEngine;

namespace Game.Entity.Movement
{
    public sealed class ServiceLocator : MonoBehaviour
    {
        public static ServiceLocator Instance { get; private set; }
        
        public static InputService InputService => Instance.inputService;
        public GameResourceService GameResourceService => gameResourceService;
        
        [SerializeField] private InputService inputService;
        [SerializeField] private GameResourceService gameResourceService;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);
        }
        
    }
}