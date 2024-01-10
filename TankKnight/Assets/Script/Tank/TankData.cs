using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
[CreateAssetMenu(fileName = "Data", menuName = "tankData")]
public class TankData : ScriptableObject
{
    [SerializeField, Header("HP")] private float hp;
    [SerializeField, Header("移動速度")] private float moveSpeed;
    [SerializeField, Header("砲台旋回速度")] private float barrelRollSpeed;
    [SerializeField, Header("発射スパン")] private float shotSpan;
    [SerializeField, Header("大砲初期ダメージ")] private float kanonDamage;


}
