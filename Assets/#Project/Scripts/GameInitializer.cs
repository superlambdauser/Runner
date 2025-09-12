using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class GameInitializer : MonoBehaviour
{
    const float OBSTACLESIZE = 1f;
    const float XGAP = 0.5f;
    [SerializeField] private ObstaclesBehaviour obstaclePrefab;
    [SerializeField] private ObstaclesManager obstaclesManager;
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
        float z = 0f;

        do
        {
            for (float i = -roadWidth; i < roadWidth; i += OBSTACLESIZE + XGAP)
            {
                float rnd = Random.Range(-roadWidth, roadWidth);
                position.x = rnd;
                position.z = z;

                obstacles.Add(GenerateObstacle(position));

                z += OBSTACLESIZE + zGap;
            }
        } while (z < roadLength);

        InstantiateObjects();
    }
}
