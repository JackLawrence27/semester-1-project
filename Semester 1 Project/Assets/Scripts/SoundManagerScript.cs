﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour
{

    public static AudioClip playerJumpSound, playerAttackSound, boarHurtOne, boarHurtTwo, boarDeath, boarRun;
    static AudioSource audioSrc;

    void Start()
    {
        //loading all the sounds into the script from the unity editor
        playerJumpSound = Resources.Load<AudioClip>("jump_snd");
        playerAttackSound = Resources.Load<AudioClip>("attack_snd");
        boarHurtOne = Resources.Load<AudioClip>("boar_hurt1");
        boarHurtTwo = Resources.Load<AudioClip>("boar_hurt2");
        boarDeath = Resources.Load<AudioClip>("boar_death");
        boarRun = Resources.Load<AudioClip>("boar_run");

        audioSrc = GetComponent<AudioSource>();
    }
    public static void PlaySound (string clip)
    {
        //Cycling through which sound to play based on what's called by the player.
        switch(clip){
            case "jump_snd":
                audioSrc.PlayOneShot(playerJumpSound);
                    break;
            case "attack_snd":
                audioSrc.PlayOneShot(playerAttackSound);
                break;
            case "boar_hurt1":
                audioSrc.PlayOneShot(boarHurtOne);
                break;
            case "boar_hurt2":
                audioSrc.PlayOneShot(boarHurtTwo);
                break;
            case "boar_death":
                audioSrc.PlayOneShot(boarDeath);
                break;
            case "boar_run":
                audioSrc.PlayOneShot(boarRun);
                break;
        }
    }
}
