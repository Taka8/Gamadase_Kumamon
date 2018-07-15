using UnityEngine;
using System.Collections;

namespace OldPackage
{
    public class MovingFloorScript : MonoBehaviour
    {

        public Vector3 point;
        public float time = 1.0f;
        public float delay = 0.1f;

        void Start()
        {

            iTween.MoveBy(gameObject, iTween.Hash("amount", point, "easeType", "linear", "time", time, "loopType", "pingPong", "delay", delay));

        }

        void OnCollisionEnter2D(Collision2D c)
        {
            c.transform.parent = transform;
        }

        void OnCollisionExit2D(Collision2D c)
        {
            c.transform.parent = null;
        }

    }
}