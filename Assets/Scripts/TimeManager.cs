using UnityEngine;
using UniRx;

public class TimeManager : SingletonMonoBehaviour<TimeManager>
{

    [SerializeField] float currentTime;
    [SerializeField] bool isPlaying;

    public bool IsPlaying { get { return isPlaying; } }

    Subject<float> timeSubject = new Subject<float>();

    public IObservable<float> OnChangeTime
    {
        get
        {
            return timeSubject;
        }
    }

    public float CurrentTime
    {

        get
        {

            if (currentTime < 0) currentTime = 0;

            return currentTime;

        }

    }

    void Start()
    {

        // イベント登録
        OnChangeTime.Subscribe(SetTime);

    }

    void Update()
    {

        if (isPlaying && 0 < currentTime)
        {

            timeSubject.OnNext(currentTime - Time.deltaTime);

        }

    }

    public void SetTime(float value)
    {
        currentTime = value;
    }

    public void Play()
    {

        isPlaying = true;

    }

    public void Stop()
    {

        isPlaying = false;

    }

}
