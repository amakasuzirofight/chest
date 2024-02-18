using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouceClickController : MonoBehaviour
{
    [SerializeField] Material tileSelectedMat;
    [SerializeField] Material tileNormalMat;
    [SerializeField] Camera camera;
    private int touchedTileNum;
    private TileData touchedTileData;
    private void Start()
    {
        touchedTileNum = -1;
    }
    private void FixedUpdate()
    {
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit = new RaycastHit();
        bool isHit = Physics.Raycast(ray, out hit);
        if (isHit)
        {
            if (hit.transform.CompareTag("Tile"))
            {
                TileData data = hit.transform.gameObject.GetComponent<TileData>();
                if (touchedTileNum != data.tileNumber)
                {
                    //以前のタイルと違うタイルに触れていたら色をもどす
                    if (touchedTileData != null)
                    {
                        touchedTileData.ChangeTileMaterial(tileNormalMat);
                    }
                    hit.transform.gameObject.GetComponent<TileData>().ChangeTileMaterial(tileSelectedMat);
                    touchedTileNum = data.tileNumber;
                    touchedTileData = data;
                }
            }
        }
    }

}
