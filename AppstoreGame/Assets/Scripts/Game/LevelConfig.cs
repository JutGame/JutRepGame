using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelConfig", menuName = "LevelConfig")]
public class LevelConfig : ScriptableObject
{
    public int NeedScores;
    public List<Pipe> Pipes;
}
