using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
[CreateAssetMenu(fileName = "Data", menuName = "tankData")]
public class TankData : ScriptableObject
{
    [SerializeField, Header("HP")] private float hp;
    [SerializeField, Header("�ړ����x")] private float moveSpeed;
    [SerializeField, Header("�C����񑬓x")] private float barrelRollSpeed = 15.0f;
    [Tooltip("�C��̏�������ő呬�x�ɓ���b��"),Range(0,1.0f)] public float barreAccelerationTime = 0.2f;
    [Tooltip("�C��̍ő呬�x�����~�܂ł̕b��"), Range(0, 1.0f)] public float barrelDecelerationTime = 0.2f;
    [SerializeField, Header("���˃X�p��")] private float shotSpan;
    [SerializeField, Header("��C�����_���[�W")] private float kanonDamage;

}
