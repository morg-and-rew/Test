using UnityEngine;
using Test.StateMachine;
using Test.CharactersActions;
using Test.PlayerObject.Movement;
using Test.PlayerObject.Idle;

namespace Test.StateMachine
{
    public class FsmExample : MonoBehaviour
    {
        private Fsm _fsm;
        private MovementAction _movementAction;
        private Rigidbody _rigidbody;
        private Animator _animator;
        private float _speed = 3f;
        private Vector3 jumpDirection = Vector3.up;
        private float _jumpForce = 5f;

        public static bool IsGrounded { get; set; } = false;

        private void Start()
        {
            _fsm = new Fsm();
            _movementAction = new MovementAction();
            _rigidbody = GetComponent<Rigidbody>();
            _animator = GetComponent<Animator>();

            if (_rigidbody == null)
            {
                _rigidbody = gameObject.AddComponent<Rigidbody>();
                _rigidbody.useGravity = true;
                _rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
            }

            _fsm.AddState(new PlayerIdle(_fsm, _animator));
            _fsm.AddState(new PlayerMovement(_fsm, transform, _speed, _movementAction, _animator));
            _fsm.AddState(new PlayerJump(_fsm, _rigidbody, _jumpForce, jumpDirection, _animator));

            _fsm.SetState<PlayerIdle>();
        }

        private void Update()
        {
            _fsm.Update();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Ground ground))
            {
                IsGrounded = true;
            }
        }
    }
}