using UnityEngine;
using UnityEngine.UI;

namespace OldPackage
{
    public class FlashingImage : MonoBehaviour
    {

        Image image;
        private float targetTime;
        public float FlashTime = 1.0f;

        // Use this for initialization
        void Start()
        {

            targetTime = Time.time + FlashTime;
            image = GetComponent<Image>();

        }

        void Update()
        {

            if (targetTime < Time.time)
            {
                image.enabled = !image.enabled;

                targetTime = Time.time + FlashTime;
            }

        }
    }
}