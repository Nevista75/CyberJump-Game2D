using UnityEngine;

public enum UfoMoveType
{
    Straight,
    Zigzag,
    Fast
}

[CreateAssetMenu(menuName = "Enemy/UFO Data")]
public class UfoData : ScriptableObject
{
    public float speed = 5f;

    [Header("Zigzag")]
    public float zigzagHeight = 3f;
    public float zigzagSpeed = 5f;
    
    [Header("Fast Mode Settings")]
    public float fastSpeedMultiplier = 3f;
}