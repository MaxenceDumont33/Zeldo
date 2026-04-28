using UnityEngine;

public class ObjectSpawnHandler : MonoBehaviour
{
    private static ObjectSpawnHandler instance;
    public static ObjectSpawnHandler Instance => instance;

    private void Awake()
    {
        if (instance)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }
    public void ShootArrow(GameObject projectile, GameObject spawnPoint)
    {
        GameObject firedProjectile = Instantiate(projectile, spawnPoint.transform.position,spawnPoint.transform.rotation);
        firedProjectile.transform.parent = null;
    }
}
