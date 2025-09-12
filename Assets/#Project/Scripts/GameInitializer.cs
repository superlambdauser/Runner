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
    [SerializeField] private ObstaclesManager obstaclesManager;
    [SerializeField] private FollowPlayer followingCamera;
    [SerializeField] private PlayerControl player;
    [SerializeField] private Transform road;
    [SerializeField] private Vector3 startingPlayerPosition;
    [SerializeField] private float zGap = 0.5f;
    private float roadLength;
    private float roadWidth;
    private List<ObstaclesBehaviour> obstacles = new();


    void Start()
    {
        roadLength = road.localScale.z * 10; // Plane is a 10x10 units game object -> scale.z * 2 = 20 units long
        roadWidth = road.localScale.x * 10;
        startingPlayerPosition = new Vector3(road.position.x, road.position.y + PLAYERSIZE / 2, 0 - (roadLength/2) + PLAYERZSTART);

        GenerateRunner();
        InitializeObjects();
    }
    // Update is called once per frame
    void Update()
    {

    }


    private void InstantiateObjects()
    {
        obstaclesManager = Instantiate(obstaclesManager); // Point at the Scene instance of obstacleManager instead of the prefab
        player = Instantiate(player);
    }
    private void InitializeObjects()
    {
        // also must init road 
        player.Initialize(startingPlayerPosition);
        obstaclesManager.Initialize(obstacles);
    }
    private ObstaclesBehaviour GenerateObstacle(Vector3 clonePosition)
    {
        ObstaclesBehaviour obstacleClone = Instantiate(obstaclePrefab, clonePosition, Quaternion.identity);
        return obstacleClone;
    }
    private void GenerateRunner()
    {
        Vector3 position;
        position.y = road.position.y + OBSTACLESIZE / 2;
        float z = OBSTACLEZSTART;

        do
        {
            for (float i = -roadWidth/2 + OBSTACLEXGAP; i < roadWidth - OBSTACLEXGAP; i += OBSTACLESIZE + OBSTACLEXGAP)
            {
                float rnd = Random.Range(-roadWidth/2 + OBSTACLESIZE/2, roadWidth/2 - OBSTACLESIZE/2);
                position.x = rnd;
                position.z = z;

                obstacles.Add(GenerateObstacle(position));

                z += OBSTACLESIZE + zGap;
            }
        } while (z < roadLength);

        InstantiateObjects();
    }
}
