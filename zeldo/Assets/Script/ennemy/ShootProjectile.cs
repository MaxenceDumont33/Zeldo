using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class ShootProjectile : MonoBehaviour
{
    [SerializeField]private float TimeBetweenShot;
    private float time;
    [SerializeField] private GameObject projectile;
    [SerializeField] private GameObject spawnPosition;

    private void Update()
    {
        time += Time.deltaTime;
        if (time>= TimeBetweenShot)
        {
            ObjectSpawnHandler.Instance.ShootArrow(projectile,spawnPosition);
            time = 0;
        }
    }
}
