using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MapEvent", menuName = "RSO/MapEvent", order = 0)]
public class RSO_MapEventStorage : ScriptableObject
{

    public List<Vector2Int> ListEnnemiPos = new List<Vector2Int>();
    public List<Vector2Int> ListHousePos = new List<Vector2Int>(); //ListHousePos[0] = Main house 
    public Vector2Int TowerPos;

}
