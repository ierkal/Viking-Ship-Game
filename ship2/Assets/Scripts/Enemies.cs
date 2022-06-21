using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Shipstat
{
    [CreateAssetMenu(fileName = ("shipName"), menuName = "Düşman oluştur")]

    public class Ships : ScriptableObject
    {
        public enum enemyType { Basic, Advanced, Longship }


        [Space(15)]
        [Header("Ayarlar")]
        public enemyType type;
        public string shipName;
        public GameObject prefab;

        [Space(15)]
        [Header("İstatistikler")]
        [Space(40)]
        public int attack;
        public float attackSpeed;
        public float range;
        public float health;
        public float xpGain;
    }
}
