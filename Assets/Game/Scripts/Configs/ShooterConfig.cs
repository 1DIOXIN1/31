using UnityEngine;

[CreateAssetMenu(menuName = "Configs/Gameplay/ShooterConfig", fileName = "ShooterConfig")]
public class ShooterConfig : ScriptableObject
{
    [field: SerializeField] public Bullet Prefab { get; private set;}
    [field: SerializeField] public float MoveSpeed { get; private set;} = 10;
    [field: SerializeField] public int Damage { get; private set;} = 50;
}
