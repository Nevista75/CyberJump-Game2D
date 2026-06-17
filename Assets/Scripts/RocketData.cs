using UnityEngine;

[CreateAssetMenu(fileName = "RocketData", menuName = "CyberJump/Rocket Data")]
public class RocketData : ScriptableObject
{
    [Header("Movement")]
    public float[] SpeedValues = { 5f, 10.4f, 12.96f, 15.6f, 19.27f };

    [Header("Physics")]
    public int Gravity = 1;
}
