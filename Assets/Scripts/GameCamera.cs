using UnityEngine;
using System.Collections;

public class GameCamera : MonoBehaviour
{

    [SerializeField] Vector2 posMin = new Vector2(10, 0);
    [SerializeField] Vector2 posMax = new Vector2(256, 0);
    [SerializeField] Vector3 offset = new Vector3(0, 1, -15);

    [SerializeField] float maxDistanceDelta = 1f;

    Transform mainCamTransform;

    void Start()
    {

        mainCamTransform = Camera.main.transform;

    }

    void LateUpdate()
    {

        Vector3 targetPos = GetPosAmongPlayers();

        targetPos += offset;

        targetPos.Set(Mathf.Clamp(targetPos.x, posMin.x, posMax.x), Mathf.Clamp(targetPos.y, posMin.y, posMax.y), targetPos.z);

        mainCamTransform.position = Vector3.MoveTowards(mainCamTransform.position, targetPos, maxDistanceDelta);

    }

    Vector2 GetPosAmongPlayers()
    {
        Vector2 targetPos = Vector2.zero;
        int count = 0;

        foreach (Player2D p in GameController.Instance.Players)
        {

            if (p == null) continue;
            
            count++;

            targetPos.x += p.transform.position.x;
            targetPos.y += p.transform.position.y;
        }
        
        if (count == 0) return mainCamTransform.position;

        targetPos /= count;
        
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
