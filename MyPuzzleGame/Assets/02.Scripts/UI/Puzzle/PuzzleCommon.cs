using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public enum NodeType : int
    {
        Empty = 0,
        Fill = 1
    }

    public enum PieceType : int
    {
        None = -1,
        PhysicsAttack = 0,
        MagicAttack = 1,
        Photion = 2,
        Skull = 3,
        Coin = 4
    }
}
