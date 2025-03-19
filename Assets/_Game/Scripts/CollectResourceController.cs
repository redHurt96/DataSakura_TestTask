using System;
using UnityEngine;
using static UnityEngine.Physics;
using static UnityEngine.QueryTriggerInteraction;

namespace Game.Entity.Movement
{
    public sealed class CollectResourceController : MonoBehaviour
    {
        private static readonly int Collect = Animator.StringToHash("Collect");
        
        public event Action Started;
        
        [SerializeField] private Animator _animator;
        [SerializeField] private CollectEventObserver _collectEventObserver;
        [SerializeField] private float _collectRadius;
        [SerializeField] private LayerMask _layerMask;
        
        private bool _isCollecting;
        
        private readonly Collider[] _collider = new Collider[1];

        private void Start() => 
            _collectEventObserver.Triggered += CollectResources;

        private void OnDestroy() => 
            _collectEventObserver.Triggered -= CollectResources;

        private void Update()
        {
            int colliders = OverlapSphereNonAlloc(transform.position, _collectRadius, _collider, _layerMask, Collide);
            
            if (colliders > 0 && !_isCollecting)
            {
                StartCollecting();
            }
        }

        [ContextMenu(nameof(StartCollecting))]
        private void StartCollecting()
        {
            _animator.SetTrigger(Collect);
            Started?.Invoke();
            _isCollecting = true;
        }

        private void CollectResources()
        {
            Collider[] colliders = OverlapSphere(transform.position, _collectRadius, _layerMask, Collide);

            foreach (Collider collider in colliders)
            {
                if (!collider.TryGetComponent(out Resource resource))
                    continue;
                
                resource.Collect();
            }
            
            _isCollecting = false;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _collectRadius);
        }
    }
}