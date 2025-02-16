using UnityEngine;
using Test.StateMachine;
using Test.CharactersActions;
using Test.PlayerObject.Idle;

namespace Test.PlayerObject.Movement
{
    public class PlayerMovement : FsmState
    {
        private readonly Fsm _fsm;
        private readonly Transform _transform;
        private readonly float _maxSpeed;
        private readonly MovementAction _movementAction;
        private readonly Animator _animator;

        private const float MovementIncrement = 1f;
        private const float DefaultMovementValue = 0f;
        private const float Acceleration = 10f;
        private const float Deceleration = 5f;

        private Vector3 _currentVelocity = Vector3.zero;

        public PlayerMovement(Fsm fsm, Transform transform, float maxSpeed, MovementAction movementAction, Animator animator) : base(fsm)
        {
            _fsm = fsm;
            _transform = transform;
            _maxSpeed = maxSpeed;
            _movementAction = movementAction;
            _animator = animator;
        }

        public override void Enter()
        {
            _animator.SetBool("isRunning", true);
            _animator.SetBool("isIdle", false);
            _animator.SetBool("isJumping", false);
        }

        public override void Exit()
        {
            _animator.SetBool("isRunning", false);
        }

        public override void Update()
        {
            Vector3 inputDirection = ReadInput();

            if (inputDirection == Vector3.zero)
            {
                _currentVelocity = Vector3.MoveTowards(_currentVelocity, Vector3.zero, Deceleration * Time.deltaTime);

                if (_currentVelocity == Vector3.zero)
                    _fsm.SetState<PlayerIdle>();
            }
            else
                _currentVelocity = Vector3.MoveTowards(_currentVelocity, inputDirection * _maxSpeed, Acceleration * Time.deltaTime);

            Move(_currentVelocity);

            if (Input.GetKeyDown(KeyCode.Space) && FsmExample.IsGrounded)
                _fsm.SetState<PlayerJump>();
        }

        private Vector3 ReadInput()
        {
            float moveX = DefaultMovementValue;
            float moveZ = DefaultMovementValue;

            if (Input.GetKey(KeyCode.W))
            {
                moveZ += MovementIncrement;
            }
            if (Input.GetKey(KeyCode.S))
            {
                moveZ -= MovementIncrement;
            }
            if (Input.GetKey(KeyCode.A))
            {
                moveX -= MovementIncrement;
            }
            if (Input.GetKey(KeyCode.D))
            {
                moveX += MovementIncrement;
            }

            Vector3 inputDirection = new Vector3(moveX, DefaultMovementValue, moveZ).normalized;

            Vector3 cameraForward = Camera.main.transform.forward;
            Vector3 cameraRight = Camera.main.transform.right;
            cameraForward.y = 0;
            cameraRight.y = 0;
            cameraForward.Normalize();
            cameraRight.Normalize();

            return cameraForward * inputDirection.z + cameraRight * inputDirection.x;
        }


        private void Move(Vector3 velocity)
        {
            _movementAction.UpdateMove(_transform, velocity, _maxSpeed);

            if (velocity != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(velocity);
                _transform.rotation = Quaternion.Slerp(_transform.rotation, targetRotation, Time.deltaTime * 10f);
            }
        }
    }
}