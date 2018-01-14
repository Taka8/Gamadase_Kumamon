using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class TimerUserInterface : MonoBehaviour {

    [SerializeField] Text timeLabel;

	void Start () {

        // 表示初期化
        SetTimeLabel(TimeManager.Instance.CurrentTime);

        // イベント登録
        TimeManager.Instance.OnChangeTime.Subscribe(SetTimeLabel).AddTo(gameObject);

	}
	
    void SetTimeLabel(float value)
    {

        timeLabel.text = value.ToString("F0");

    }

}
