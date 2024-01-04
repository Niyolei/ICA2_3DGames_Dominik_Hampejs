using UnityEngine;


namespace ICA2.My_Assets.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "FightData", menuName = "ScriptableObjects/FightData", order = 0)]
    public class FightData: ScriptableObject
    {
        public Vector3 playerPosition;
    }
}