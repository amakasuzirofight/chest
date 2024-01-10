using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
[CreateAssetMenu(fileName = "Data", menuName = "tankData")]
public class TankData : ScriptableObject
{
    [SerializeField, Header("HP")] private float hp;
    [SerializeField, Header("�ړ����x")] private float moveSpeed;
    [SerializeField, Header("�C����񑬓x")] private float barrelRollSpeed;
    [SerializeField, Header("���˃X�p��")] private float shotSpan;
    [SerializeField, Header("��C�����_���[�W")] private float kanonDamage;


}
