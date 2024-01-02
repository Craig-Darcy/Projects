using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Plays footstep sounds based on player movement and terrain
public class PlayerFootsteps : MonoBehaviour
{
    //References to other scripts 
    public GroundedMaterialCheck checkGrounded;
    public AudioSource audioSource;

    //Arrays for audio clips
    public AudioClip[] pavementClips;
    public AudioClip[] dirtClips;
    public AudioClip[] woodClips;
    public AudioClip[] metalClips;
    public AudioClip[] carpetClips;

    AudioClip previousAudioClip;//Stores previous clip played

    //Player varibles
    CharacterController player;
    float currentSpeed;
    public bool isWalking;
    public float distanceCovered;
    public float modifier = 25f;
    float timeInAir;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<CharacterController>();//Sets player
    }

    // Update is called once per frame
    void Update()
    {
        //Calls methods that check the speed and state of the player
        GetPlayerSpeed();
        IsPlayerWalking();
        PlayOnFall();

        if(isWalking == true)//Plays footstep audio based on the distance the player has covered to match the timing of steps better
        {
            distanceCovered += (currentSpeed * Time.deltaTime) * modifier;
            if(distanceCovered > 1)
            {
                TriggerNextClip();
                distanceCovered = 0;
            }
        }
    }

    void GetPlayerSpeed()
    {
        float speed = player.velocity.magnitude;
        currentSpeed = speed;
    }

    void IsPlayerWalking()
    {
        if(currentSpeed > 0 && checkGrounded.playerGrounded)
        {
            isWalking = true;
        }
        else
        {
            isWalking = false;
        }
    }

    AudioClip GetClipFromArray(AudioClip[] clipArray) // Gets a random audio clip based on the terrain while also avoiding the same audio repeating
    {
        int attempts = 3;
        AudioClip selectedAudioClip = clipArray[Random.Range(0 , clipArray.Length - 1)];

        while(selectedAudioClip == previousAudioClip && attempts > 0)
        {
            selectedAudioClip = clipArray[Random.Range(0, clipArray.Length - 1)];
            attempts--;
        }
        previousAudioClip = selectedAudioClip;
        return selectedAudioClip;
    }

    void TriggerNextClip()
    {
        //Randomizes pitch and volume slightly
        audioSource.pitch = Random.Range(0.9f, 1.1f);
        audioSource.volume = Random.Range(0.7f, 1f);

        //Uses the checkGrounded script to select the array that is needed for the audio clip
        if(checkGrounded.playerGrounded == true)
        {
            if(checkGrounded.isOnPavement == true)
            {
                audioSource.PlayOneShot(GetClipFromArray (pavementClips), 1);
            }
            if (checkGrounded.isOnWoodFloor == true)
            {
                audioSource.PlayOneShot(GetClipFromArray(woodClips), 1);
            }
            if (checkGrounded.isOnMetalFloor == true)
            {
                audioSource.PlayOneShot(GetClipFromArray(metalClips), 1);
            }
            if (checkGrounded.isOnTerrain == true)
            {
                audioSource.PlayOneShot(GetClipFromArray(dirtClips), 1);
            }
            if (checkGrounded.isOnCarpet == true)
            {
                audioSource.PlayOneShot(GetClipFromArray(carpetClips), 1);
            }
        }
    }

    //Used for sounds when the player lands on the ground
    void PlayOnFall()
    {
        if (!checkGrounded.playerGrounded)
        {
            timeInAir += Time.deltaTime;
        }
        else if(timeInAir > 0.25f)
        {
            TriggerNextClip();
            timeInAir = 0;
        }
    }
}
