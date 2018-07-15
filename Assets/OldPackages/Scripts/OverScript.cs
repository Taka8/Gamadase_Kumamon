using UnityEngine;
using System.Collections;

namespace OldPackage
{
    public class OverScript : MonoBehaviour
    {

        [SerializeField] AudioClip overClip;
        [SerializeField] float duration = 6f;

        IEnumerator Start()
        {

            // フェードイン
            FadeManager.Instance.SetAlfa(1f);
            FadeManager.Instance.FadeIn(1f);

            // BGMの停止・効果音再生
            SoundManager.Instance.StopBgm();
            SoundManager.Instance.PlaySe(overClip);

            float t = Time.time + duration;

            // duration秒待つ
            while (Time.time < t)
            {

                yield return null;

                if (Input.GetButtonDown("Submit")) break;

            }

            // タイトルへ
            FadeManager.Instance.FadeOutAndLoadScene(1f, 0);

        }

    }
}