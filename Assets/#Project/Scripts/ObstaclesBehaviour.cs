using UnityEngine;

[RequireComponent(typeof(Collider))]
public class ObstaclesBehaviour : MonoBehaviour
{
    [SerializeField] private ObstaclesManager manager;

    public void Initialize(ObstaclesManager manager)
    {
        this.manager = manager;
    }
    void OnCollisionEnter(Collision collision)
    {

    }
}
