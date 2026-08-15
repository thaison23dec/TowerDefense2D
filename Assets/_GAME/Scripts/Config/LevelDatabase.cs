using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelDatabase", menuName = "Level Database")]
public class LevelDatabase : ScriptableObject
{
    public List<LevelData> levels;
}
