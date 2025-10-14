using UnityEngine;

namespace Code.StaticData
{
    [CreateAssetMenu(fileName = "HeroData", menuName = "Static Data/Hero")]
    public class HeroStaticData : ScriptableObject
    {
        [Range(50, 200)] public int Health;
        [Range(5, 50)] public float Damage;
        [Range(25, 100)] public int Ammo;

        public float SpeedFactor;

        [Range(10, 200)] public float MoveSpeed;
        [Range(10, 200)] public float LookSpeed;

        [Range(0f, 0.6f)] public float AttackCooldown;
        [Range(0, 3)] public float ReloadCooldown;

        public GameObject Prefab;
    }
}