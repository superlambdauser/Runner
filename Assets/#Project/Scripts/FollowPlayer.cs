using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float distanceFromPlayer = 5f;
    [SerializeField] private float cameraHeight = 3f;


    public void Initialize(Transform player)
    {
        this.player = player;

        Follow();
    }

    public void Follow()
    {
        transform.position = new Vector3 (0, cameraHeight, player.position.z - distanceFromPlayer);
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
