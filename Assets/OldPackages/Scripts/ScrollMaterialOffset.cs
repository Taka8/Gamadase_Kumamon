using UnityEngine;
using System.Collections;

namespace OldPackage
{
    public class ScrollMaterialOffset : MonoBehaviour
    {

        public Vector2 speed = new Vector2(0.1f, 0.2f);
        public float cameraLength = 30;

        private Material ma;

        void Start()
        {

            ma = GetComponent<Renderer>().material;

        }

        void Update()
        {

            if (Mathf.Abs(Camera.main.transform.position.x - transform.position.x) < cameraLength)
            {

                Vector2 afterOffset = (ma.mainTextureOffset + speed * Time.deltaTime);

                afterOffset.Set(afterOffset.x % 1, afterOffset.y % 1);

                ma.mainTextureOffset = afterOffset;

            }

        }
    }
}