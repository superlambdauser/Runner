using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] PlayerControl player;
    [SerializeField] FollowPlayer followingCamera;

    public void Initialize(PlayerControl player, FollowPlayer followingCamera)
    {
        this.player = player;
        this.followingCamera = followingCamera;
    }

    // Update is called once per frame
    void Update()
    {
        player.Process();
        followingCamera.Process();
    }
}
