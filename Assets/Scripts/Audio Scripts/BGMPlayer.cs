using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMPlayer : MonoBehaviour
{
    public BGMBanks bgmBnk;
    public int BGMIndex, index;
    public GameObject SpeakerPrefab;
    public AudioSource sound;
    [SerializeField] private AudioClip bgmLoaded;
    public float sfxVol = 1f;
    private float loopOffset;
    private bool isLoop;
    [SerializeField] private bool isDone = true;
    public float totalSecs, aveDelta;
    public int FPS;

    void Awake()
    {
        // Check if an instance of the script already exists

    }

    void Start()
    {
        GameObject[] soundObjects = GameObject.FindGameObjectsWithTag("Speaker");
        if (soundObjects.Length > 1)
        {
            for (int i = 1; i < soundObjects.Length; i++)
            {
                Destroy(soundObjects[i]);
            }
        }

        GameObject soundObject = soundObjects[0];

        if (soundObject == null)
        {
            soundObject = Instantiate(SpeakerPrefab);
            DontDestroyOnLoad(gameObject);

        }
        else
        {
            DontDestroyOnLoad(gameObject);


        }
        sound = soundObject.GetComponent<AudioSource>();

        LoadAndPlayBGM(BGMIndex);
        // Use the sound variable as needed
        StartCoroutine(PlayBGM());
    }

    private void AverageDeltaTime()
    {
        if (totalSecs < 1)
        {
            FPS++;
            totalSecs += Time.deltaTime;
        }
        else
        {
            aveDelta = totalSecs / FPS;
            totalSecs = 0;
            FPS = 0;
        }
    }

    private void Update()
    {   
        //Debug.LogWarning("Sound is Playing: "+ sound.isPlaying);
        StartCoroutine(PlayBGM());
    }

    public void LoadAndPlayBGM(int tempIndex)
    {
        index = tempIndex;
        StartCoroutine(PlayBGM());
    }
    IEnumerator PlayBGM()
    {
        while (true) // Use a loop
        {
            if (index != BGMIndex || sound.clip == null || bgmBnk.BGM[index].Track == null)
            {
                sound.Stop();
                sound.time = 0;
                BGMIndex = index;
                bgmLoaded = bgmBnk.BGM[BGMIndex].Track;
                sound.clip = bgmLoaded;
                loopOffset = bgmBnk.BGM[BGMIndex].loopOffset;
                isLoop = bgmBnk.BGM[BGMIndex].isLooping;
                sound.Play();
            }

            if (sound.clip != null)
            {
                AverageDeltaTime();

                if (isLoop && sound.time >= sound.clip.length - aveDelta)
                {
                    //sound.Stop();
                    Debug.LogWarning("Time at loop: " + Time.time);
                    sound.time = loopOffset;
                    Debug.LogError("Sound.Time: " + sound.time);
                    Debug.LogError("Looped");
                    sound.Play();
                    isDone = false;
                }
                else if (!isLoop && sound.time >= sound.clip.length)
                {
                    sound.Stop();
                }
            }

            // Add a yield return null to allow Unity to update and not lock the frame
            yield return true;
        }
    }









    //public bool LoadAndPlayBGM(int newBGMIndex)
    //{
    //    if (newBGMIndex < 0 || newBGMIndex >= bgmBnk.BGM.Count)
    //    {
    //        Debug.LogWarning("Invalid BGMIndex");
    //        return false;
    //    }

    //    // Skip loading if the track is already loaded
    //    if (BGMIndex == newBGMIndex && sound.clip != null)
    //        return true;

    //    BGMIndex = newBGMIndex;
    //    bgmLoaded = bgmBnk.BGM[BGMIndex].Track;
    //    loopOffset = bgmBnk.BGM[BGMIndex].loopOffset;
    //    isLoop = bgmBnk.BGM[BGMIndex].isLooping;
    //    isDone = false;

    //    return true;
    //}

    //IEnumerator PlayBGM()
    //{
    //    yield return Time.fixedDeltaTime;
    //    AverageDeltaTime();

    //    if (BGMIndex >= 0 && BGMIndex < bgmBnk.BGM.Count)
    //    {
    //        if (sound.clip == null || (!TwoClipIsEqual() && isDone))
    //        {
    //            sound.Stop();
    //            sound.clip = bgmLoaded;
    //            sound.Play();
    //        }

    //        if (!isDone)
    //        {
    //            if (!isLoop && sound.timeSamples >= bgmLoaded.samples)
    //            {
    //                sound.Stop();
    //                sound.clip = null;
    //                isDone = true;
    //            }
    //            else if (isLoop && sound.timeSamples >= (sound.clip.samples - (sound.clip.frequency * aveDelta)))
    //            {
    //                Debug.LogWarning("Time at loop: " + Time.time);
    //                sound.timeSamples = (int)loopOffset;
    //                sound.Play();
    //                Debug.LogError("Looped");
    //                isDone = false;
    //            }
    //        }
    //    }
    //    else
    //    {
    //        Debug.LogWarning("Invalid BGMIndex");
    //    }

    //    StartCoroutine(PlayBGM());
    //}

    //private bool TwoClipIsEqual()
    //{
    //    if (sound.clip == null || bgmLoaded == null)
    //    {
    //        return false;
    //    }

    //    float[] data1 = new float[sound.clip.samples * sound.clip.channels];
    //    float[] data2 = new float[bgmLoaded.samples * bgmLoaded.channels];

    //    sound.clip.GetData(data1, 0);
    //    bgmLoaded.GetData(data2, 0);

    //    for (int i = 0; i < data1.Length; i++)
    //    {
    //        if (data1[i] != data2[i])
    //        {
    //            return false;
    //        }
    //    }

    //    return true;
    //}

    //private void AverageDeltaTime()
    //{
    //    if (totalSecs < 1)
    //    {
    //        FPS++;
    //        totalSecs += Time.deltaTime;
    //    }
    //    else
    //    {
    //        aveDelta = totalSecs / FPS;
    //        totalSecs = 0;
    //        FPS = 0;
    //    }
    //}
}
