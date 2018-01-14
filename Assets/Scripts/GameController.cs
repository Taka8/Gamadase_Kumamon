using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameController : SingletonMonoBehaviour<GameController>
{

    public bool countFlg = true;
    [SerializeField] AudioClip clearClip;

    bool returnTitleFlag;

    private Player2D[] players = new Player2D[4];
    public Player2D[] Players { get { return players; } }

    [Header("Player")]

    [SerializeField]
    Player2D playerPrefab;
    public Player2D PlayerPrefab { get { return playerPrefab; } }

    [SerializeField] Texture[] playerTextures = new Texture[4];

    [SerializeField] bool[] SpawnFlag = new bool[4];

    [Header("Prefabs")]
    [SerializeField]
    GameObject coinPrefab;
    public GameObject CoinPrefab { get { return coinPrefab; } }

    float screenLeft;
    float screenRight;

    public float ScreenLeft { get { return screenLeft; } }
    public float ScreenRight { get { return screenRight; } }

    private IEnumerator GoalCoroutine(Player2D player)
    {

        returnTitleFlag = true;

        // 残り時間を止める
        countFlg = false;

        // BGMをフェードアウト
        SoundManager.Instance.StopBgm(1);

        // PlayerNumのプレーヤーが着地するまで待つ
        while (player.GroundChecker.IsGrounded == false)
        {
            yield return null;
        }

        // 効果音
        SoundManager.Instance.PlaySe(clearClip);

        // クリアメッセージを表示
        GameUiManager.Instance.ShowClearMessage();

        foreach (Player2D p in players)
        {

            if (p == null) continue;

            p.Goal();

        }

        yield return new WaitForSeconds(6.0f);

        FadeManager.Instance.FadeOut(1f);

        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene("Title");

        yield break;

    }

    IEnumerator Start()
    {

        yield return null;

        // フェードイン
        FadeManager.Instance.SetAlfa(1f);
        FadeManager.Instance.FadeIn(1f);

        // BGM再生
        SoundManager.Instance.PlayBgm(StageConfig.Instance.Bgm);

        // タイマー処理
        TimeManager.Instance.SetTime(StageConfig.Instance.Time);
        TimeManager.Instance.Play();

        // Player 1 生成
        GeneratePlayer(0, StageConfig.Instance.PlayerSpawn.position);

        // 生存チェック
        while (CheckPlayersAlive())
        {
            yield return new WaitForSeconds(1f);
        }

        // ゲームオーバー
        FadeManager.Instance.FadeOutAndLoadScene(2, 4);

    }

    void GeneratePlayer(int index, Vector2 position)
    {

        players[index] = Instantiate(PlayerPrefab, position, Quaternion.identity);

        Material m = new Material(players[index].MainRenderer.material);
        m.mainTexture = playerTextures[index];

        players[index].MainRenderer.material = m;

        players[index].PlayerID = index + 1;

        SpawnFlag[index] = true;

        GameUiManager.Instance.SetPlayerUi(players[index], index);

    }

    bool CheckPlayersAlive()
    {

        foreach (Player2D player in players)
        {

            if (player) return true;

        }

        return false;

    }

    void Update()
    {

        // 他プレイヤー参加
        for (int i = 1; i < 4; i++)
        {

            if (Input.GetButtonDown("Fire " + (i + 1)) && SpawnFlag[i] == false)
            {

                GeneratePlayer(i, players[0].transform.position);

            }

        }

        // タイトル画面へ
        if (returnTitleFlag == false && Input.GetKeyDown(KeyCode.Escape))
        {

            FadeManager.Instance.FadeOutAndLoadScene(1, 0);
            returnTitleFlag = true;

        }

        // ゲーム画面の左端と右端の座標を計算
        CalculateScreenSide();

    }

    public void Goal(Player2D player)
    {

        StartCoroutine(GoalCoroutine(player));

        TimeManager.Instance.Stop();

    }

    void CalculateScreenSide()
    {

        screenLeft = Camera.main.ViewportToWorldPoint(new Vector3(0, 0.5f, 15)).x;
        screenRight = Camera.main.ViewportToWorldPoint(new Vector3(1, 0.5f, 15)).x;

    }

}
