using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowBulletDate : MonoBehaviour
{
    /// <summary>
    /// 0,弾速、1連射速度、2威力、3射程、4発射個数　
    /// </summary>
    [Header("必用なコスト")] public int[] lvCost;
    [Header("現在のコスト")] public　int currentCost;//他のスクリプトに書き込む変数

    [SerializeField] float[] growSpeed;//+する弾速
    [SerializeField] float[] growInterval;//+する連射する速度
    [SerializeField] float[] growPower;//+する連射する速度
    [SerializeField] float[] growLifeTime;
    [SerializeField] float[] growbulletNum;

    [SerializeField] int[] NextLvCost;//次の経験値


    //弾の種類
    /// <summary>
    /// 0：貫通弾　1：遅延弾 2:散弾　3スリップダメージ弾　4ロングバレット　5ホーミング　6カーブ　
    /// </summary>
    [SerializeField] int[] bulletKindPlice;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    #region 弾の種類の解放
    public void LiberationPiercingBullet()
    {
        if(currentCost>= bulletKindPlice[0])
        {
            KindDate.Instance.canPiercingBullet = true;
        }
    }
    public void LiberationSlowBullet()
    {
        if (currentCost >= bulletKindPlice[1])
        {
            KindDate.Instance.canSlowBullet = true;
        }
    }
    public void LiberationDiffusionBullet()
    {
        if (currentCost >= bulletKindPlice[2])
        {
            KindDate.Instance.canDiffusionBullet = true;
        }
    }
    public void LiberationSlipDamegeBullet()
    {
        if (currentCost >= bulletKindPlice[2])
        {
            KindDate.Instance.canSlipDamegeBullet = true;
        }
    }
    public void LiberationLongBullet()
    {
        if (currentCost >= bulletKindPlice[2])
        {
            KindDate.Instance.canLongBullet = true;
        }
    }
    public void LiberationHomingBullet()
    {
        if (currentCost >= bulletKindPlice[2])
        {
            KindDate.Instance.canHomingBullet = true;
        }
    }
    public void LiberationCurveBullet()
    {
        if (currentCost >= bulletKindPlice[2])
        {
            KindDate.Instance.canCurveBullet = true;
        }
    }
    #endregion

    #region 弾の成長
    public void GrowSpeed()
    {
        if (lvCost[0] <= currentCost)
        {
            switch(BulletState.Instance.currentLv[0])
            {
                case 1:
                    GrowState(ref BulletState.Instance.bulletSpeed, growSpeed, 0, 0, 0);
                    BulletState.Instance.currentLv[0]++;
                    Debug.Log(BulletState.Instance.bulletSpeed);
                    break;
                case 2:
                    GrowState(ref BulletState.Instance.bulletSpeed, growSpeed, 1, 1, 0);
                    BulletState.Instance.currentLv[0]++;
                    Debug.Log(BulletState.Instance.bulletSpeed);
                    break;
                case 3:
                    GrowState(ref BulletState.Instance.bulletSpeed, growSpeed, 2, 2, 0);
                    BulletState.Instance.currentLv[0]++;
                    Debug.Log(BulletState.Instance.bulletSpeed);
                    break;
                case 4:
                    GrowState(ref BulletState.Instance.bulletSpeed, growSpeed, 3, 3, 0);
                    BulletState.Instance.currentLv[0]++;
                    Debug.Log(BulletState.Instance.bulletSpeed);
                    break;
                case 5:
                    GrowState(ref BulletState.Instance.bulletSpeed, growSpeed, 4, 4, 0);
                    BulletState.Instance.currentLv[0]++;
                    Debug.Log(BulletState.Instance.bulletSpeed);
                    break;
                case 6:
                    GrowState(ref BulletState.Instance.bulletSpeed, growSpeed, 5, 5, 0);
                    BulletState.Instance.currentLv[0]++;
                    Debug.Log(BulletState.Instance.bulletSpeed);
                    break;
            }
        }
    }
    public void GrowInterval()
    {
        if (lvCost[1] <= currentCost)
        {
            if (BulletState.Instance.currentLv[1] == 1)
            {
                GrowState(ref BulletState.Instance.bulletInterval,growInterval, 0, 0,1);
                BulletState.Instance.currentLv[1]++;
                Debug.Log(BulletState.Instance.bulletInterval);

            }
            else if (BulletState.Instance.currentLv[1] == 2)
            {
                GrowState(ref BulletState.Instance.bulletInterval, growInterval, 1, 1, 1);
                BulletState.Instance.currentLv[1]++;
                Debug.Log(BulletState.Instance.bulletInterval);
            }
            else if (BulletState.Instance.currentLv[1] == 3)
            {
                GrowState(ref BulletState.Instance.bulletInterval, growInterval, 2, 2, 1);
                BulletState.Instance.currentLv[1]++;
                Debug.Log(BulletState.Instance.bulletInterval);
            }
            else if (BulletState.Instance.currentLv[1] == 4)
            {
                GrowState(ref BulletState.Instance.bulletInterval, growInterval, 3, 3, 1);
                BulletState.Instance.currentLv[1]++;
                Debug.Log(BulletState.Instance.bulletInterval);
            }
            else if (BulletState.Instance.currentLv[1] == 5)
            {
                GrowState(ref BulletState.Instance.bulletInterval, growInterval, 4, 4, 1);
                BulletState.Instance.currentLv[1]++;
                Debug.Log(BulletState.Instance.bulletInterval);
            }
            else if (BulletState.Instance.currentLv[1] == 6)
            {
                GrowState(ref BulletState.Instance.bulletInterval, growInterval, 5, 5, 1);
                BulletState.Instance.currentLv[1]++;
                Debug.Log(BulletState.Instance.bulletInterval);
            }
        }
    }
    public void GrowPower()
    {
        if (lvCost[2] <= currentCost)
        {
            if (BulletState.Instance.currentLv[1] == 1)
            {
                GrowState(ref BulletState.Instance.bulletPower,growPower, 0, 0,2);
                BulletState.Instance.currentLv[1]++;

            }
            else if (BulletState.Instance.currentLv[1] == 2)
            {
                GrowState(ref BulletState.Instance.bulletPower, growPower, 1, 1, 2);
                BulletState.Instance.currentLv[1]++;
            }
            else if (BulletState.Instance.currentLv[1] == 3)
            {
                GrowState(ref BulletState.Instance.bulletPower, growPower, 2, 2, 2);
                BulletState.Instance.currentLv[1]++;
            }
            else if (BulletState.Instance.currentLv[1] == 4)
            {
                GrowState(ref BulletState.Instance.bulletPower, growPower, 3, 3, 2);
                BulletState.Instance.currentLv[1]++;
            }
            else if (BulletState.Instance.currentLv[1] == 5)
            {
                GrowState(ref BulletState.Instance.bulletPower, growPower, 4, 4, 2);
                BulletState.Instance.currentLv[1]++;
            }
            else if (BulletState.Instance.currentLv[1] == 6)
            {
                GrowState(ref BulletState.Instance.bulletPower, growPower, 5, 5, 2);
                BulletState.Instance.currentLv[1]++;
            }
        }
    }
    public void GrowLifeTime()
    {
        if (lvCost[3] <= currentCost)
        {
            if (BulletState.Instance.currentLv[1] == 1)
            {
                GrowState(ref BulletState.Instance.buletLifeTime,growLifeTime, 0, 0,3);
                BulletState.Instance.currentLv[1]++;

            }
            else if (BulletState.Instance.currentLv[1] == 2)
            {
                GrowState(ref BulletState.Instance.buletLifeTime, growLifeTime, 1, 1, 3);
                BulletState.Instance.currentLv[1]++;
            }
            else if (BulletState.Instance.currentLv[1] == 3)
            {
                GrowState(ref BulletState.Instance.buletLifeTime, growLifeTime, 2, 2, 3);
                BulletState.Instance.currentLv[1]++;
            }
            else if (BulletState.Instance.currentLv[1] == 4)
            {
                GrowState(ref BulletState.Instance.buletLifeTime, growLifeTime, 3, 3, 3);
                BulletState.Instance.currentLv[1]++;
            }
            else if (BulletState.Instance.currentLv[1] == 5)
            {
                GrowState(ref BulletState.Instance.buletLifeTime, growLifeTime, 4, 4, 3);
                BulletState.Instance.currentLv[1]++;
            }
            else if (BulletState.Instance.currentLv[1] == 6)
            {
                GrowState(ref BulletState.Instance.buletLifeTime, growLifeTime, 5, 5, 3);
                BulletState.Instance.currentLv[1]++;
            }
        }
    }
    public void GrowBulletNum()
    {
        if (lvCost[4] <= currentCost)
        {
            if (BulletState.Instance.currentLv[1] == 1)
            {
                GrowState(ref BulletState.Instance.bulletNum,growbulletNum, 0, 0, 4);
                BulletState.Instance.currentLv[1]++;

            }
            else if (BulletState.Instance.currentLv[1] == 2)
            {
                GrowState(ref BulletState.Instance.bulletNum, growbulletNum, 1, 1, 4);
                BulletState.Instance.currentLv[1]++;
            }
            else if (BulletState.Instance.currentLv[1] == 3)
            {
                GrowState(ref BulletState.Instance.bulletNum, growbulletNum, 2, 2, 4);
                BulletState.Instance.currentLv[1]++;
            }
            else if (BulletState.Instance.currentLv[1] == 4)
            {
                GrowState(ref BulletState.Instance.bulletNum, growbulletNum, 3, 3, 4);
                BulletState.Instance.currentLv[1]++;
            }
            else if (BulletState.Instance.currentLv[1] == 5)
            {
                GrowState(ref BulletState.Instance.bulletNum, growbulletNum, 4, 4, 4);
                BulletState.Instance.currentLv[1]++;
            }
            else if (BulletState.Instance.currentLv[1] == 6)
            {
                GrowState(ref BulletState.Instance.bulletNum, growbulletNum, 5, 5, 4);
                BulletState.Instance.currentLv[1]++;
            }
        }
    }
    void GrowState(ref float state,float[] growStat,int upStat,int nextCost,int lv)
    {
        state = growStat[upStat];//成長
        currentCost -= lvCost[lv];//経験値の消費
        lvCost[lv] = NextLvCost[nextCost];//次の経験値を代入
       
    }

    #endregion
}
