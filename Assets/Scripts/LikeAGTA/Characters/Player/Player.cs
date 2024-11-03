using RDTools.AutoAttach;
using System;
using Unity.Cinemachine;
using UnityEngine;

namespace LikeAGTA.Characters
{
    public class Player : Character
    {
        [SerializeField, Attach] PlayerMovement _playerMovement;
    }
}