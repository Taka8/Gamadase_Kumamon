using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

[RequireComponent(typeof(Image))]
[RequireComponent(typeof(Canvas))]
public class FadeManager : SingletonMonoBehaviour<FadeManager>
{

    [SerializeField] Image image;

    void Reset()
    {

        GetComponent<Canvas>().sortingOrder = 999;
        image = GetComponent<Image>();

    }

    /// <summary>
    /// Faderのアルファをセットします
    /// </summary>
    /// <param name="a"></param>
    public void SetAlfa(float a)
    {
        image.color = new Color(0, 0, 0, a);
    }

    /// <summary>
    /// duration秒かけてフェードインします
    /// </summary>
    /// <param name="duration"></param>
    /// <returns></returns>
    public void FadeIn(float duration)
    {
        image.DOFade(0, duration);
    }

    /// <summary>
    /// duration秒かけてフェードアウトします
    /// </summary>
    /// <param name="duration"></param>
    /// <returns></returns>
    public void FadeOut(float duration)
    {
        image.DOFade(1, duration);
    }

    public void FadeOutAndLoadScene(float duration, int sceneBuildIndex)
    {

        SceneManager.LoadSceneAsync(sceneBuildIndex).allowSceneActivation = false;

        image.DOFade(1, duration).OnComplete(() => { SceneManager.LoadScene(sceneBuildIndex); });

    }

}
