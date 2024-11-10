using UnityEngine;

namespace LikeAGTA.Characters.Data
{
    [CreateAssetMenu(fileName = "PlayerData", menuName = "Characters/Player Data")]
    public class PlayerDataSO : ScriptableObject
    {
        public Player Prefab;
        public Vector3 SpawnPosition = new(54.5299988f, -1.31099999f, -54.2299995f);
    }
}