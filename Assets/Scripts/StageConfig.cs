using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageConfig : SingletonMonoBehaviour<StageConfig>
{

    [SerializeField] AudioClip bgm;
    [SerializeField] float time;
    [SerializeField] Transform playerSpawn;

    public AudioClip Bgm { get { return bgm; } }
    public float Time { get { return time; } }
    public Transform PlayerSpawn { get { return playerSpawn; } }

}
