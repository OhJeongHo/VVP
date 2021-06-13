using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource audioSource;
    //public AudioClip gameBG;
    public AudioClip LaserChager;
    public AudioClip LaserShot;




    //public AudioClip audioClipApplause;
    //public AudioClip potion;
    //public AudioClip finish;
    //public AudioClip portalSound;
    //public AudioClip panpareSound;
    //public AudioClip titleBG;
    //public AudioClip mainBG;


    public static SoundManager instance;

    void Awake()
    {
        if (SoundManager.instance == null)
        {
            SoundManager.instance = this;
        }
    }

    public void LaserChagerr()
    {
        audioSource.PlayOneShot(LaserChager);



    }

    public void LaserShott()
    {
        audioSource.PlayOneShot(LaserShot);



    }

    //public void TitleBGM()
    //{
    //    audioSource.PlayOneShot(titleBG);



    //}


    //public void MainBGM()
    //{
    //    audioSource.PlayOneShot(mainBG);



    //}

    //public void RankingBGM()
    //{
    //    audioSource.PlayOneShot(rankingBG);
    //}


    //public void ApplauseSound()

    //{
    //    audioSource.PlayOneShot(audioClipApplause);
    //}


    //public void potionSound()
    //{
    //    audioSource.PlayOneShot(potion);
    //}


    //public void finished()
    //{
    //    audioSource.PlayOneShot(finish);
    //}

    //public void portalSnd()
    //{
    //    audioSource.PlayOneShot(portalSound);
    //}

    //public void pangpare()
    //{
    //    audioSource.PlayOneShot(panpareSound);
    //}


}