using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System.Collections;

public class TitleUIScript : MonoBehaviour
{

    [SerializeField] EventSystem eventSystem;
    [SerializeField] GameObject InitialObject;
    [SerializeField] GameObject StageSelectObject;

    [Header("Buttons")]
    [SerializeField]
    Button[] buttons;

    [Header("Audio Clips")]
    [SerializeField]
    AudioClip titleBgm;
    [SerializeField] AudioClip select;
    [SerializeField] AudioClip submit;

    enum Mode
    {

        Title,
        StageSelect,
        Selected

    }

    Mode mode = Mode.Title;

    IEnumerator LoadCoroutine(int sceneBuildIndex)
    {

        // mode切り替え
        mode = Mode.Selected;

        // 効果音の再生
        SoundManager.Instance.PlaySe(submit);

        yield return new WaitForSeconds(2f);

        // フェードアウト
        FadeManager.Instance.FadeOutAndLoadScene(1f, sceneBuildIndex);

    }

    public void LoadStage(int sceneBuildeIndex)
    {

        foreach (Button b in buttons)
        {
            Destroy(b);
        }

        StartCoroutine(LoadCoroutine(sceneBuildeIndex));

    }

    IEnumerator Start()
    {

        yield return null;

        // フェードイン
        FadeManager.Instance.SetAlfa(1f);
        FadeManager.Instance.FadeIn(1f);

        // BGMの再生
        SoundManager.Instance.PlayBgm(titleBgm);

    }

    void Update()
    {

        switch (mode)
        {

            case Mode.Title:

                if (Input.GetButton("Submit"))
                {
                    transitionSelect();      // ステージセレクトへ
                }

                break;

            case Mode.StageSelect:

                if (Input.GetButton("Cancel"))
                {
                    transitionTitle();      // タイトルへ
                }

                break;

            case Mode.Selected:

                break;

            default:
                transitionTitle();
                break;

        }

        // アプリケーションの終了
        if (Input.GetKeyDown(KeyCode.Escape)) Application.Quit();

    }

    void transitionTitle()
    {

        StageSelectObject.SetActive(false);     // StageSelectを非表示にする

        InitialObject.SetActive(true);          // Titleを表示する

        mode = Mode.Title;                      // modeの切り替え

    }

    void transitionSelect()
    {

        InitialObject.SetActive(false);         // Titleを非表示にする

        StageSelectObject.SetActive(true);      // StageSelectを表示する

        eventSystem.SetSelectedGameObject(buttons[0].gameObject);

        mode = Mode.StageSelect;                // modeの切り替え

    }

}
