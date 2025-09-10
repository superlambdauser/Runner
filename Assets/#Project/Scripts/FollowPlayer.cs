
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float distance = 5;
    [SerializeField] private float cameraHeight = 3;



    void Update()
    {
        transform.position = new Vector3 (player.position.x, cameraHeight, player.position.z - distance);
    }
}
