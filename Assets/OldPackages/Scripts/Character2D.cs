using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OldPackage
{

    [RequireComponent(typeof(Collider2D))]
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Animator))]
    public class Character2D : MonoBehaviour
    {

        protected void SetIsKinematicAll(bool value, Rigidbody[] rigidbodies)
        {
            foreach (Rigidbody r in rigidbodies)
            {
                r.isKinematic = value;
            }
        }

        protected void SetVelocityZeroAll(Rigidbody[] rigidbodies)
        {
            foreach (Rigidbody r in rigidbodies)
            {
                r.velocity = Vector3.zero;
            }
        }

        protected void SetRendererEnableAll(bool value, Renderer[] renderers)
        {
            foreach (Renderer m in renderers)
            {
                m.enabled = value;
            }
        }

    }
}