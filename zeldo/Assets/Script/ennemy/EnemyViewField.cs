using UnityEngine;

public class EnemyViewField : MonoBehaviour
{
    LayerMask detectedLayer;
    LayerMask playerLayer;
    float time = 2;

    private void Start()
    {
        playerLayer = LayerMask.GetMask("Player");
        detectedLayer = LayerMask.GetMask("Wall");
    }
    void Update()
    {
        Collider2D playerDetection = Physics2D.OverlapCircle(transform.position, 2, playerLayer);
        if (playerDetection == true)
        {
            RaycastHit2D obstacleDetection = Physics2D.Raycast(transform.position,playerDetection.transform.position,2,detectedLayer);
            if(obstacleDetection == false)
            {
                if (time > 2)
                {
                    time = 0;
                    PathFinding.Instance.StartLookingForPath(transform.position,playerDetection.transform.position);
                }
            }
        }
        time += Time.deltaTime;
    }
}
