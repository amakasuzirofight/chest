using System;
//https://wizardia.hateblo.jp/entry/2023/06/28/100000
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class TileData : MonoBehaviour
{
    public int tileNumber = -1;

    private void Start()
    {
    }
    public void ChangeTileMaterial(Material mat)
    {
        gameObject.GetComponent<MeshRenderer>().material = mat;
    }
}
