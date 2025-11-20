using UnityEngine;

[CreateAssetMenu(fileName = "GameData", menuName = "Game Design/GameData")]
public class GameData : ScriptableObject
{
    [Header("Player")]
    [field: SerializeField] public PlayerData Player { get; private set; }

    [Header("Camera")]
    [field: SerializeField] public CameraData Camera { get; private set; }
}
