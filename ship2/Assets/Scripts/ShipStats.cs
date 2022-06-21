using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Shipstat
{
    [CreateAssetMenu(fileName = ("shipName"), menuName = "Düşman oluştur")]

    public class ShipStats : ScriptableObject
    {
        public enum enemyType {PlayerShip , Basic, Advanced, Longship }


        [Space(15)]
        [Header("Ayarlar")]
        public enemyType type;
        public string shipName;
        public GameObject prefab;

        [Space(15)]
        [Header("İstatistikler")]
        [Space(40)]
        public float attack;
        public float attackSpeed;
        public float range;
        public float health;
        public float xpGain;
    }
}
