using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletState : Singleton<BulletState>
{
    // �V���O���g���C���X�^���X
    private static BulletState instance;

    // �f�[�^��ۑ�����ϐ�
    public float bulletSpeed=5;
    public float bulletInterval=10;
    public float bulletPower=10;
    public float buletLifeTime=10;
    public float bulletNum=1;

   /*[System.NonSerialized] */public int[] currentLv;
   
}
