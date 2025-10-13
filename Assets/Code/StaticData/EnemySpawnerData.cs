using System;
using UnityEngine;

namespace Code.StaticData
{
    [Serializable]
    public class EnemySpawnerData
    {
        public string Id;
        public Vector3 Position;

        public EnemySpawnerData(string id, Vector3 position)
        {
            Id = id;
            Position = position;
        }
    }
}