using UnityEngine;
using UnityEngine.UI;

public class GameUiManager : SingletonMonoBehaviour<GameUiManager> {

    [SerializeField] PlayerUserInterface[] playerUi;
    [SerializeField] Image clearMessage;

    void Start()
    {
        clearMessage.enabled = false;    
    }

    public void SetPlayerUi(Player2D player, int index)
    {

        playerUi[index].gameObject.SetActive(true);
        playerUi[index].SetPlayer2D(player);

    }

    public void ShowClearMessage()
    {

        clearMessage.enabled = true;

    }

}
