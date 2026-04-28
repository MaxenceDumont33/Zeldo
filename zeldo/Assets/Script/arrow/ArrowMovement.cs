using UnityEngine;

public class ArrowMovement : MonoBehaviour
{
    [SerializeField] private int speed;
    // Update is called once per frame
    void Update()
    {
        transform.position += transform.up * (speed * Time.deltaTime);
    }
}
