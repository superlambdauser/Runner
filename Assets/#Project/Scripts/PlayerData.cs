using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Game Design/PlayerData")]
public class PlayerData : ScriptableObject
{
    [field: SerializeField] public float StartingSpeed { get; private set; } 
    [field: SerializeField] public float SpeedIncrease { get; private set; }
    [field: SerializeField] public float JumpSpeed { get; private set; }
}
