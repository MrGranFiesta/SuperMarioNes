using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "Level1")]
public class Level : ScriptableObject
{
    [SerializeField] private string _sceneName;

    public string SceneName { get { return _sceneName; } }
}
