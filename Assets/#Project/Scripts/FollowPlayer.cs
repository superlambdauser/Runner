using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float distanceFromPlayer;
    [SerializeField] private float cameraHeight;


    public void Initialize(Transform player, float distanceFromPlayer, float cameraHeight)
    {
        this.player = player;
        this.distanceFromPlayer = distanceFromPlayer;
        this.cameraHeight = cameraHeight;

        Follow();
    }

    public void Follow()
    {
        transform.position = new Vector3 (player.position.x, cameraHeight, player.position.z - distanceFromPlayer);
    }


    void Start()
    {
        if (player == null)
        {
            GameObject.FindFirstObjectByType<PlayerControl>();
        }
    }

    public void Process()
    {
        Follow();
    }
}
