
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float distance = 5;
    [SerializeField] private float cameraHeight = 3;

    public void Initialize(Transform player)
    {
        this.player = player;

        Follow();
    }
    public void Follow()
    {
        transform.position = new Vector3 (player.position.x, cameraHeight, player.position.z - distance);
    }
    void Update()
    {
        Follow();
    }
}
