using UnityEngine;

public class BlockBehaviour : MonoBehaviour
{
    public int Height;
    public BlockType Type;
    private void Start()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y + Height, transform.position.z);
    }

    public void TryPlaceStructure (GameObject go)
    {
        Instantiate(go,new Vector3(transform.position.x, transform.position.y+1, transform.position.z), Quaternion.Euler(new Vector3(0, 0, 0)), transform);
    }
}

public enum BlockType
{
    Water,
    Sand,
    Grass,
    Stone,
    Snow
}