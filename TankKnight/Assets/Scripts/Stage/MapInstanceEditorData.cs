using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapInstanceEditorData : ScriptableObject
{
    [SerializeField] private float gridSizeX;
    [SerializeField] private float gridSizeY;
    [SerializeField] private GameObject instanceObj;
    [SerializeField] private GameObject tileDirector;

    public float getGridSizeX { get => gridSizeX; private set => gridSizeX = value; }
    public float getGridSizeY { get => gridSizeY; private set => gridSizeY = value; }
    public GameObject getInstanceObj { get => instanceObj; private set => instanceObj = value; }
    public GameObject getTileDirector { get => tileDirector; set => tileDirector = value; }
}
