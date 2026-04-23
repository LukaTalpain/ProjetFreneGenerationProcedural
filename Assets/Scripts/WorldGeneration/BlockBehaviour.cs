using UnityEngine;

public class BlockBehaviour : MonoBehaviour
{
    public int Height;
    private void Start()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y + Height, transform.position.z);
    }
}
