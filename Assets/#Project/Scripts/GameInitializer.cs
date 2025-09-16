using System.Collections.Generic;
using UnityEngine;


public class GameInitializer : MonoBehaviour
{
    const float OBSTACLESIZE = 1f;
    const float PLAYERSIZE = 1f;
    const float OBSTACLEXGAP = 0.5f;
    const float PLAYERZSTART = 5f;
    const float OBSTACLEZSTART = PLAYERZSTART + 3f;
    [SerializeField] private ObstaclesBehaviour obstaclePrefab;
    [SerializeField] private FollowPlayer followingCamera;
    [SerializeField] private PlayerControl player;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private Transform road;
    [SerializeField] private Vector3 startingPlayerPosition;
    [SerializeField] private float zGap = 0.5f;
    private List<ObstaclesBehaviour> obstacles = new();
    private float roadLength;
    private float roadWidth;
    private float finishLine;


    void Start()
    {
        roadLength = road.localScale.z * 10; // Plane is a 10x10 units game object -> scale.z * 2 = 20 units long
        roadWidth = road.localScale.x * 10;
        startingPlayerPosition = new Vector3(road.position.x, road.position.y + PLAYERSIZE / 2, 0 - (roadLength / 2) + PLAYERZSTART);
        finishLine = roadLength/2 - PLAYERSIZE;

        GenerateRunner();
    }


    private void InstantiateObjects()
    {
        // Point at the Scene instance of obstacleManager instead of the prefab
        player = Instantiate(player);
        followingCamera = Instantiate(followingCamera);
        gameManager = Instantiate(gameManager);
        Instantiate(road); // The road does not need arguments -> no initialization -> no instantiation like the other objects.
    }
    private void InitializeObjects()
    {
        // also must init road 
        player.Initialize(startingPlayerPosition, finishLine);
        followingCamera.Initialize(player.GetComponent<Transform>());
        gameManager.Initialize(player, followingCamera);
    }
    private ObstaclesBehaviour GenerateObstacle(Vector3 clonePosition)
    {
        ObstaclesBehaviour obstacleClone = Instantiate(obstaclePrefab, clonePosition, Quaternion.identity);
        return obstacleClone;
    }

    void GenerateObstacles()
    {
        Vector3 position;
        position.y = road.position.y + OBSTACLESIZE / 2;
        float z = -roadLength / 2 + OBSTACLEZSTART;

        while (z < finishLine)
        {
            for (float i = -roadWidth / 2; i < roadWidth / 2; i += OBSTACLESIZE + OBSTACLEXGAP)
            {
                float rnd = Random.Range(-roadWidth / 2 + OBSTACLESIZE / 2, roadWidth / 2 - OBSTACLESIZE / 2);
                position.x = rnd;
                position.z = z;

                obstacles.Add(GenerateObstacle(position));
                z += OBSTACLESIZE + zGap;

                if(z >= finishLine) break; // Not optimal... Rethink the while loop as a for loop
            }
        }

        Debug.Log($"z = {z} | roadlength = {roadLength} | finishLine = {finishLine} ");
    }

    private void GenerateRunner()
    {
        GenerateObstacles();
        InstantiateObjects();
        
        InitializeObjects();
    }
}
