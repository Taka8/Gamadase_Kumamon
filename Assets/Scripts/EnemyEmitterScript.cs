using UnityEngine;
using System.Collections;

public class EnemyEmitterScript : MonoBehaviour
{

    public float length = 30;
    public GameObject enemyPrefab;
    public GameObject Enemy;

    private enum State
    {
        prepare,
        isWaiting,
        isEmitting
    }

    [SerializeField]
    State state = State.isWaiting;

    void Start()
    {

        if (Enemy) Enemy.SetActive(false);

    }

    void Update()
    {

        switch (state)
        {
            case State.prepare:

                if (Enemy == false && Mathf.Abs(Camera.main.transform.position.x - transform.position.x) > length)
                {

                    Enemy = Instantiate(enemyPrefab, transform, false) as GameObject;
                    Enemy.SetActive(false);

                    state = State.isWaiting;

                }

                break;

            case State.isWaiting:

                if (Mathf.Abs(Camera.main.transform.position.x - transform.position.x) < length)
                {

                    Enemy.SetActive(true);

                    state = State.isEmitting;

                }

                break;

            case State.isEmitting:

                if (Enemy == false) state = State.prepare;

                else {

                    if (Mathf.Abs(Camera.main.transform.position.x - Enemy.transform.position.x) > length)
                    {

                        Destroy(Enemy);

                        state = State.prepare;

                    }

                }

                break;

            default:
                state = State.isWaiting;
                break;

        }

    }
}
