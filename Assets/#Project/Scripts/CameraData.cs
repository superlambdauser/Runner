using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Game Design/CameraData")]
public class CameraData : ScriptableObject
{
    [field: SerializeField] public float DistanceFromPlayer { get; private set; }
    [field: SerializeField] public float Height { get; private set; }
}
