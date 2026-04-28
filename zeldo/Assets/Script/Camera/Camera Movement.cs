using System.Collections;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    private bool cameraIsMoving = false;
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Projectile"))
        {
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            CameraMove(collision.transform.position,transform.position);
        }
    }
    private Vector2 DirectionToAxis(Vector2 direction)
    {
        direction = Vector2.Normalize(direction);
        float dotX = Vector2.Dot(direction, Vector2.right);
        float dotY = Vector2.Dot(direction, Vector2.up);
        

        return Mathf.Abs(dotX/MapDictionary.Instance.gridSizeX) > Mathf.Abs(dotY / MapDictionary.Instance.gridSizeY)
            ? new Vector2(Mathf.Sign(dotX), 0f)
            : new Vector2(0f, Mathf.Sign(dotY));
    }
    private void CameraMove(Vector2 playerPos, Vector2 cameraPos)
    {
        Vector2 direction = DirectionToAxis(playerPos-cameraPos);
        if (direction.x == 0 && cameraIsMoving == false)
        {
            cameraIsMoving = true;
            StartCoroutine(MoveCameraFunc(direction, Vector2.up,MapDictionary.Instance.gridSizeY));
        }
        else if (cameraIsMoving == false && direction.y == 0)
        {
            cameraIsMoving = true;
            StartCoroutine(MoveCameraFunc(direction, Vector2.right, MapDictionary.Instance.gridSizeX));
        }
    }
    private IEnumerator MoveCameraFunc(Vector2 moveSign,Vector2 moveDirection,int moveLength) //tester avec math.lerp pour rendre fluide
    {
        for (int i = 0; i < moveLength; i++)
        {
            Vector3 newPos = moveDirection * moveSign;
            transform.position += newPos;
            yield return new WaitForSeconds(.01f);
            print("hfhf");
        }
        cameraIsMoving = false;
        MapDictionary.Instance.RefreshDictionary();
        yield return null;
    }
}
