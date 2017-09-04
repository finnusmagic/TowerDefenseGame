using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager> {

    [SerializeField] AudioClip arrow;
    [SerializeField] AudioClip fireball;
    [SerializeField] AudioClip rock;
    [SerializeField] AudioClip death;
    [SerializeField] AudioClip gameover;
    [SerializeField] AudioClip hit;
    [SerializeField] AudioClip level;
    [SerializeField] AudioClip newGame;
    [SerializeField] AudioClip towerBuilt;

    public AudioClip Arrow
    {
        get
        {
            return arrow;
        }
    }
    public AudioClip Fireball
    {
        get
        {
            return fireball;
        }
    }
    public AudioClip Rock
    {
        get
        {
            return rock;
        }
    }
    public AudioClip Death
    {
        get
        {
            return death;
        }
    }
    public AudioClip Gameover
    {
        get
        {
            return gameover;
        }
    }
    public AudioClip Hit
    {
        get
        {
            return hit;
        }
    }
    public AudioClip Level
    {
        get
        {
            return level;
        }
    }
    public AudioClip NewGame
    {
        get
        {
            return newGame;
        }
    }
    public AudioClip TowerBuilt
    {
        get
        {
            return towerBuilt;
        }
    }
}
