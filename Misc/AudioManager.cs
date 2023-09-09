using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    [SerializeField] private AudioClip doorSound;

    /*----------------INITIALIZATION METHODS-----------------------------*/

    // Reference to the AudioSource component
    private AudioSource audioSource; 

    private void Awake()
    {
        // Add an AudioSource component to the object with which the AudioManager is associated
        audioSource = gameObject.AddComponent<AudioSource>(); 
    }

    /*---------------- METHODS-----------------------------*/

    public void PlayClip(AudioClip sound)
    {
        if (sound == null)
        {
            // Log a warning if the sound is null
            Debug.LogWarning("Trying to play a null sound."); 
            return; 
        }

        // Set the clip of the AudioSource to the provided sound
        audioSource.clip = sound; 

        // Play the sound
        audioSource.Play();

        // Start a coroutine to destroy the AudioSource after the clip has finished playing
        StartCoroutine(DestroyAudioSourceAfterClipFinished(sound.length)); 
    }

    private IEnumerator DestroyAudioSourceAfterClipFinished(float clipLength)
    {
        // Wait for the duration of the clip
        yield return new WaitForSeconds(clipLength); 

        // Stop the AudioSource
        audioSource.Stop(); 

        // Clear the clip of the AudioSource
        audioSource.clip = null; 
    }

    // Called by event from animation
    public void PlayDoorSound()
    {
        PlayClip(doorSound);
    }
}