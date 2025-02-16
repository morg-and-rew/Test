using UnityEngine;
using Test.StateMachine;
using Test.PlayerObject.Movement;

namespace Test.PlayerObject.Idle
{
    public class PlayerIdle : FsmState
    {
        private readonly Fsm _fsm;
        private readonly Animator _animator;

        public PlayerIdle(Fsm fsm, Animator animator) : base(fsm)
        {
            _fsm = fsm;
            _animator = animator;
        }

        public override void Enter()
        {
            _animator.SetBool("isIdle", true);
            _animator.SetBool("isRunning", false);
            _animator.SetBool("isJumping", false);
        }

        public override void Exit()
        {
            _animator.SetBool("isIdle", false);
        }

        public override void Update()
        {
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
                _fsm.SetState<PlayerMovement>();
            else if (Input.GetKeyDown(KeyCode.Space) && FsmExample.IsGrounded)
                _fsm.SetState<PlayerJump>();
        }
    }
}