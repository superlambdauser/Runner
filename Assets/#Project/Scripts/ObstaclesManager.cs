using System.Collections.Generic;
using UnityEngine;

public class ObstaclesManager : MonoBehaviour
{

    private List<ObstaclesBehaviour> obstacles;


    public void ObstacleCollision()
    {

    }
    public void Initialize(List<ObstaclesBehaviour> obstacles, PlayerControl player)
    {
        this.obstacles = obstacles;
    }

    // Should make a method that deletes every obstacle in the list if it has passed players prosition froma certain amout & put it in the update ? 

}
