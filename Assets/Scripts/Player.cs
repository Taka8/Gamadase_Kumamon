using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{

    [RequireComponent(typeof(PlayerMover))]
    [RequireComponent(typeof(PlayerAnimator))]
    public class Player : MonoBehaviour
    {

        [SerializeField] State myState;

        [SerializeField] PlayerMover pm;

        void Reset()
        {
            pm = GetComponent<PlayerMover>();
        }

        void Update()
        {

        }

        void FixedUpdate()
        {
            pm.FixedMove(Input.GetAxis("Horizontal"));
            pm.FixedJump(Input.GetKey(KeyCode.Space));
        }

        enum State
        {
            Idling,
            Walking,
            Running,
            UpInTheAir,
            DownInTheAir,
            Damaged,
            Attacking
        }

        void EnterState(State state)
        {
            myState = state;

            switch (state)
            {
                case State.Idling:
                    break;
                case State.Walking:
                    break;
                case State.Running:
                    break;
                case State.UpInTheAir:
                    break;
                case State.DownInTheAir:
                    break;
                case State.Damaged:
                    break;
                case State.Attacking:
                    break;
                default:
                    myState = State.Idling;
                    break;
            }
        }

        void ExecIdling()
        {

        }

        void ExecWalking()
        {

        }

        void ExecRunning()
        {

        }

        void InTheAir()
        {

        }
        void ExecDamaged()
        {

        }
        void ExecAttacking()
        {

        }

    }

}