using UnityEngine;

[RequireComponent(typeof(Collider))]
public class ObstaclesBehaviour : MonoBehaviour
{
    [SerializeField] private ObstaclesManager manager;
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            manager.ObstacleCollision();
        }
    }
}
