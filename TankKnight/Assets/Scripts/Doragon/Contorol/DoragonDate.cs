using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoragonDate : Singleton<DoragonDate>
{
    public int HP;
    public float moveSpeed;
    public int[] nextLvExp;

    [System.NonSerialized] public int CurrentLv=1;
    /*[System.NonSerialized] */public int currentExp;//���̃X�N���v�g�ɏ����ϐ�
    // Start is called before the first frame update
   
}
