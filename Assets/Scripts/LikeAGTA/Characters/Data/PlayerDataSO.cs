using UnityEngine;

namespace LikeAGTA.Characters.Data
{
    [CreateAssetMenu(fileName = "PlayerData", menuName = "Characters/Player Data")]
    public class PlayerDataSO : ScriptableObject
    {
        public Player Prefab;
    }
}