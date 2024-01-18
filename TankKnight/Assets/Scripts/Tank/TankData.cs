using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
[CreateAssetMenu(fileName = "Data", menuName = "tankData")]
public class TankData : ScriptableObject
{
    [SerializeField, Header("HP")] private float hp;
    [SerializeField, Header("ˆÚ“®‘¬“x")] private float moveSpeed;
    [SerializeField, Header("–C‘äù‰ñ‘¬“x")] private float barrelRollSpeed = 15.0f;
    [Tooltip("–C‘ä‚Ì‰‘¬‚©‚çÅ‘å‘¬“x‚É“ü‚é•b”"),Range(0,1.0f)] public float barreAccelerationTime = 0.2f;
    [Tooltip("–C‘ä‚ÌÅ‘å‘¬“x‚©‚ç’â~‚Ü‚Å‚Ì•b”"), Range(0, 1.0f)] public float barrelDecelerationTime = 0.2f;
    [SerializeField, Header("”­ËƒXƒpƒ“")] private float shotSpan;
    [SerializeField, Header("‘å–C‰Šúƒ_ƒ[ƒW")] private float kanonDamage;

}
