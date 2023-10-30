using Mono.Cecil;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    static bool init = false;
    static AudioSource AudioSource;
    static Dictionary<AudioName, AudioClip> audioClips = new Dictionary<AudioName, AudioClip>();
    // Start is called before the first frame update
    public static bool Initialized
    {
        get { return init; }
    }

    public static void Initialize(AudioSource source)
    {
        init = true;
        AudioSource = source;
        audioClips.Add(AudioName.BombExplode, Resources.Load<AudioClip>("BombExplode"));
        audioClips.Add(AudioName.PlayerHit, Resources.Load<AudioClip>("PlayerHit"));

    }

    public static void Play(AudioName name)
    {
        //Debug.Log("chay vao day");
        AudioSource.PlayOneShot(audioClips[name]);
    }
}
