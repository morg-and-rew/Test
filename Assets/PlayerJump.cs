using UnityEngine;
using Test.StateMachine;
using Test.CharactersActions;
using Test.PlayerObject.Idle;

namespace Test.PlayerObject.Movement
{
    public class PlayerJump : FsmState
    {
        private readonly Rigidbody _rigidbody;
        private readonly float _jumpForce;
        private readonly Vector3 _jumpDirection;
        private readonly Animator _animator;
        private bool _isJumping;

        public PlayerJump(Fsm fsm, Rigidbody rigidbody, float jumpForce, Vector3 jumpDirection, Animator animator) : base(fsm)
        {
            _rigidbody = rigidbody;
            _jumpForce = jumpForce;
            _jumpDirection = jumpDirection;
            _animator = animator;
        }

        public override void Enter()
        {
            _isJumping = true;

            Jump();

            _animator.SetBool("isJumping", true);
            _animator.SetBool("isIdle", false);
            _animator.SetBool("isRunning", false);
        }

        public override void Exit()
        {
            _isJumping = false;
            _animator.SetBool("isJumping", false);
        }

        public override void Update()
        {
            if (_isJumping && FsmExample.IsGrounded)
                Fsm.SetState<PlayerIdle>(); 
            
            if (_isJumping)
                Fsm.SetState<PlayerMovement>();
        }

        private void Jump()
        {
            _rigidbody.AddForce(_jumpDirection * _jumpForce, ForceMode.Impulse);
            FsmExample.IsGrounded = false; 
        }
    }
}