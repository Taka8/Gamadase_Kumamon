using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{

    [RequireComponent(typeof(PlayerMover))]
    public class Player : MonoBehaviour
    {

        [SerializeField] PlayerMover pm;

        void Reset()
        {
            pm = GetComponent<PlayerMover>();
        }

        void Start()
        {

        }

        void Update()
        {
            
        }

        void FixedUpdate()
        {
            pm.FixedMove(Input.GetAxis("Horizontal"));
            pm.FixedJump(Input.GetKey(KeyCode.Space));
        }

    }

}