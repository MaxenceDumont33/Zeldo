using UnityEngine;
using UnityEngine.InputSystem;

public class Bow : MonoBehaviour
{
    [SerializeField] private GameObject arrow;
    [SerializeField] private GameObject spawnPosition;
    public void UseBow(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            ObjectSpawnHandler.Instance.ShootArrow(arrow, spawnPosition);
        }
    }
}
