using UnityEngine;
using Utils.Extension_Methods;

namespace Game.Entity.Movement
{
    public sealed class MovementController : MonoBehaviour
    {
        [SerializeField] private float movementSpeed;
        [SerializeField] Animator _animator;
        [SerializeField] private CharacterController _characterController;
        
        public void Update()
        {
            var moveInput = -ServiceLocator.InputService.MoveInput;

            _characterController.Move(moveInput.ToVector3Plane() * (movementSpeed * Time.deltaTime));
            
            var isMoving = moveInput != Vector2.zero;

            if (isMoving)
                transform.forward = moveInput.ToVector3Plane();
            
            _animator.SetBool("IsMoving", isMoving);
            _animator.SetFloat("MoveSpeed", moveInput.magnitude);
        }
    }
}