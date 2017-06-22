using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "Level", order = 1)]
public class LevelData : ScriptableObject
{
    public Vector3 origin;
    public Vector2 spacing;

    public int rows;
    public int columns;

    public int maxDistricts;
    public int maxPrecinctSize;
}
