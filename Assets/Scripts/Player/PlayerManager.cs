using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerManager : MonoBehaviour
{
    private GameObject playerGo = null;
    [SerializeField] private RSE_PlayerGoSpawned playerGoSpawnedEvent;
    [SerializeField] private RSE_InputEvent inputEvent;
    [SerializeField] private RSO_BlockStorage blockStorage;
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private Tilemap eventTilemap;
    [SerializeField] private RSO_Settings settings;

    private void OnEnable()
    {
        playerGoSpawnedEvent.PlayerGoSpawnedEvent += connectPlayerToGo;
        inputEvent.zPressed += () => MovePlayer(moveDirection.forward);
        inputEvent.sPressed += () => MovePlayer(moveDirection.backward);
        inputEvent.qPressed += () => MovePlayer(moveDirection.left);
        inputEvent.dPressed += () => MovePlayer(moveDirection.right);
    }

    private void OnDisable()
    {
        playerGoSpawnedEvent.PlayerGoSpawnedEvent -= connectPlayerToGo;
        inputEvent.zPressed -= () => MovePlayer(moveDirection.forward);
        inputEvent.sPressed -= () => MovePlayer(moveDirection.backward);
        inputEvent.qPressed -= () => MovePlayer(moveDirection.left);
        inputEvent.dPressed -= () => MovePlayer(moveDirection.right);
    }


    private void connectPlayerToGo(GameObject go)
    {
        playerGo = go;
    }

    private void MovePlayer (moveDirection dir)
    {
        if (playerGo == null) return;
        int height; 
        switch (dir)
        {
            case moveDirection.forward:
                if (Mathf.Abs((int)playerGo.transform.position.z ) == 0)
                {
                    print ("Can't move forward, out of bounds");
                    break;
                }
                height = GetHeightFromTile(tilemap.GetTile(new Vector3Int((int)playerGo.transform.position.x, Mathf.Abs((int)playerGo.transform.position.z+1))));
                playerGo.transform.position = new Vector3(playerGo.transform.position.x, height, playerGo.transform.position.z + 1);
                break;



            case moveDirection.backward:
                if ((int)playerGo.transform.position.z  == -99 )
                {
                    print("Can't move backward, out of bounds");
                    break;
                }
                height = GetHeightFromTile(tilemap.GetTile(new Vector3Int((int)playerGo.transform.position.x, Mathf.Abs((int)playerGo.transform.position.z - 1))));
                playerGo.transform.position = new Vector3(playerGo.transform.position.x, height, playerGo.transform.position.z - 1);
                break;




            case moveDirection.left:
                if ((int)playerGo.transform.position.x == 0)
                {
                    break;
                }
                height = GetHeightFromTile(tilemap.GetTile(new Vector3Int((int)playerGo.transform.position.x-1, Mathf.Abs((int)playerGo.transform.position.z ))));
                playerGo.transform.position = new Vector3(playerGo.transform.position.x - 1, height, playerGo.transform.position.z);
                break;





            case moveDirection.right:
                if ((int)playerGo.transform.position.x  == 99)
                {
                    break;
                }
                height = GetHeightFromTile(tilemap.GetTile(new Vector3Int((int)playerGo.transform.position.x+1, Mathf.Abs((int)playerGo.transform.position.z ))));
                playerGo.transform.position = new Vector3(playerGo.transform.position.x + 1, height, playerGo.transform.position.z);
                break;
        }
    }

    private int GetHeightFromTile(TileBase tile)
    {
        for (int i = 0; i < blockStorage.blocks.Count; i++)
        {
            if (blockStorage.blocks[i].tileBase == tile)
            {
                return i + 1;
            }
        }
        Debug.LogError("Tile not found in block storage");
        return -1;
    }
}

public enum moveDirection
{
    forward,
    backward,
    left,
    right
}
