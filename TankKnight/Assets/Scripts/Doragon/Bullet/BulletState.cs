using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletState : Singleton<BulletState>
{
    // シングルトンインスタンス
    private static BulletState instance;

    // データを保存する変数
    public float bulletSpeed=5;
    public float bulletInterval=10;
    public float bulletPower=10;
    public float buletLifeTime=10;
    public float bulletNum=1;

   /*[System.NonSerialized] */public int[] currentLv;
   
}
