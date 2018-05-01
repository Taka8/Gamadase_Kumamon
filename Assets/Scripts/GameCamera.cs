using UnityEngine;
using System.Collections;

public class GameCamera : MonoBehaviour
{

    [SerializeField] Vector2 posMin = new Vector2(10, 0);
    [SerializeField] Vector2 posMax = new Vector2(256, 0);
    [SerializeField] Vector3 offset = new Vector3(0, 1, -15);

    [SerializeField] float maxDistanceDelta = 1f;

    Transform mainCamTransform;                                 // メインカメラのトランスフォーム

    void Start()
    {
        // メインカメラのトランスフォームを保持
        mainCamTransform = Camera.main.transform;
    }

    // カメラの移動はすべてLateUpdaateで行うのが好ましい
    void LateUpdate()
    {

        // プレーヤが存在すれば移動
        if (GameController.Instance.CheckPlayersAlive())
        {
            Vector3 targetPos = GetDeltaPlayerPos();
            targetPos += offset;
            targetPos.Set(Mathf.Clamp(targetPos.x, posMin.x, posMax.x), Mathf.Clamp(targetPos.y, posMin.y, posMax.y), targetPos.z);
            mainCamTransform.position = Vector3.MoveTowards(mainCamTransform.position, targetPos, maxDistanceDelta);
        }
        // プレーヤが存在しなければ移動しない
        else
        {

        }
    }

    Vector2 GetDeltaPlayerPos()
    {

        Vector2 targetPos = Vector2.zero;

        float minX, maxX, minY, maxY;

        maxX = maxY = float.NegativeInfinity;
        minX = minY = float.PositiveInfinity;

        foreach (Player2D p in GameController.Instance.Players)
        {

            if (p == null) continue;

            float x = p.transform.position.x;
            minX = Mathf.Min(minX, x);
            maxX = Mathf.Max(maxX, x);

            float y = p.transform.position.y;
            minY = Mathf.Min(minY, y);
            maxY = Mathf.Max(maxY, y);
        }


        targetPos.Set(minX + maxX, minY + maxY);
        targetPos /= 2;

        return targetPos;
    }

    void OnDrawGizmosSelected()
    {

        Gizmos.color = Color.red;

        Vector3 leftBottom = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, -offset.z));
        Vector3 rightTop = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, -offset.z));

        Vector3 center = (leftBottom + rightTop) / 2;
        Vector3 size = rightTop - leftBottom;
        size.z = 0;

        Gizmos.DrawWireCube(center, size);
    }

}
