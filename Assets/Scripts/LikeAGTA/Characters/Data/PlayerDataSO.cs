using RDTools;
using UnityEngine;
using UnityEngine.Serialization;

namespace LikeAGTA.Characters.Data
{
    [CreateAssetMenu(fileName = "PlayerData", menuName = "Characters/Player Data")]
    public class PlayerDataSO : ScriptableObject
    {
        [FormerlySerializedAs("prefab")] [FormerlySerializedAs("_prefab")] [ShowAssetPreview] public Player Prefab;
    }
}