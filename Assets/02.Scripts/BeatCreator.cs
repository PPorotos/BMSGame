using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatCreator : MonoBehaviour
{
    public int keyMode;

    public float bpm;

    public bool isStart = false;
    public bool isTic = false;

    float nextTime = 0f;
    float nextSample = 0f;

    float secondPerBar = 0f;
    float secondPerBeat = 0f;
    float samplePerBar = 0f;
    float samplePerBeat = 0f;
    public float beatPerBar;

    public float timeRateBySpeed;

    public AudioClip ticSound;
    public AudioClip bgmSound;
    AudioSource ticPlayer;
    AudioSource bgmPlayer;

    int beatCount = 0;

    public List<NoteObj> noteObj_Line_1;
    public List<NoteObj> noteObj_Line_2;
    public List<NoteObj> noteObj_Line_3;
    public List<NoteObj> noteObj_Line_4;
    public List<NoteObj> noteObj_Line_5;
    public List<NoteObj> bar_Line;

    int noteIndex1 = 0;
    int noteIndex2 = 0;
    int noteIndex3 = 0;
    int noteIndex4 = 0;
    int noteIndex5 = 0;
    int barIndex = 0;

    bool isBgmPlay = false;

    private void Start()
    {
        ticPlayer = gameObject.AddComponent<AudioSource>();
        ticPlayer.clip = ticSound;

        bgmPlayer = gameObject.AddComponent<AudioSource>();
        bgmPlayer.clip = bgmSound;


    }

    private void Update()
    {
        if (isStart == true)
        {
            StartCoroutine(Create());
        }
    }

    IEnumerator Create()
    {

        secondPerBar = 60.0f / bpm * 4f;
        secondPerBeat = 60.0f / bpm * 4f / beatPerBar;
        samplePerBar = secondPerBar * bgmPlayer.clip.frequency;
        samplePerBeat = secondPerBeat * bgmPlayer.clip.frequency;
        yield return null;

        if(Time.time >= (secondPerBar*(timeRateBySpeed-1))&& isBgmPlay == false)
        {
            bgmPlayer.Play();
            isBgmPlay = true;
        }

        if (Time.time >= nextTime && isBgmPlay == false)
        {
            if (noteObj_Line_1.Count > noteIndex1)
            {
                if (nextTime >= (noteObj_Line_1[noteIndex1].noteTime))
                {
                    noteObj_Line_1[noteIndex1].isStart = true;
                    noteIndex1++;
                }
            }
            if (noteObj_Line_2.Count > noteIndex2)
            {
                if (nextTime >= (noteObj_Line_2[noteIndex2].noteTime))
                {
                    noteObj_Line_2[noteIndex2].isStart = true;
                    noteIndex2++;
                }
            }
            if (noteObj_Line_3.Count > noteIndex3)
            {
                if (nextTime >= (noteObj_Line_3[noteIndex3].noteTime))
                {
                    noteObj_Line_3[noteIndex3].isStart = true;
                    noteIndex3++;
                }
            }
            if (noteObj_Line_4.Count > noteIndex4)
            {
                if (nextTime >= (noteObj_Line_4[noteIndex4].noteTime))
                {
                    noteObj_Line_4[noteIndex4].isStart = true;
                    noteIndex4++;
                }
            }
            if (noteObj_Line_5.Count > noteIndex5)
            {
                if (nextTime >= (noteObj_Line_5[noteIndex5].noteTime))
                {
                    noteObj_Line_5[noteIndex5].isStart = true;
                    noteIndex5++;
                }
            }
            if (bar_Line.Count > barIndex)
            {
                if (nextTime >= bar_Line[barIndex].noteTime)
                {
                    bar_Line[barIndex].isStart = true;
                    barIndex++;
                }
            }

            nextTime += secondPerBeat;
        }
        if (bgmPlayer.timeSamples >= nextSample && isBgmPlay == true)
        {

            if (noteObj_Line_1.Count > noteIndex1)
            {
                if (bgmPlayer.timeSamples >= (noteObj_Line_1[noteIndex1].noteTime - (secondPerBar * (timeRateBySpeed - 1))) * bgmPlayer.clip.frequency)
                {
                    noteObj_Line_1[noteIndex1].isStart = true;
                    noteIndex1++;
                }
            }
            if (noteObj_Line_2.Count > noteIndex2)
            {
                if (bgmPlayer.timeSamples >= (noteObj_Line_2[noteIndex2].noteTime - (secondPerBar * (timeRateBySpeed - 1))) * bgmPlayer.clip.frequency)
                {
                    noteObj_Line_2[noteIndex2].isStart = true;
                    noteIndex2++;
                }
            }
            if (noteObj_Line_3.Count > noteIndex3)
            {
                if (bgmPlayer.timeSamples >= (noteObj_Line_3[noteIndex3].noteTime - (secondPerBar * (timeRateBySpeed - 1))) * bgmPlayer.clip.frequency)
                {
                    noteObj_Line_3[noteIndex3].isStart = true;
                    noteIndex3++;
                }
            }
            if (noteObj_Line_4.Count > noteIndex4)
            {
                if (bgmPlayer.timeSamples >= (noteObj_Line_4[noteIndex4].noteTime - (secondPerBar * (timeRateBySpeed - 1))) * bgmPlayer.clip.frequency)
                {
                    noteObj_Line_4[noteIndex4].isStart = true;
                    noteIndex4++;
                }
            }
            if (noteObj_Line_5.Count > noteIndex5)
            {
                if (bgmPlayer.timeSamples >= (noteObj_Line_5[noteIndex5].noteTime - (secondPerBar * (timeRateBySpeed - 1))) * bgmPlayer.clip.frequency)
                {
                    noteObj_Line_5[noteIndex5].isStart = true;
                    noteIndex5++;
                }
            }
            if (bar_Line.Count > barIndex)
            {
                if (bgmPlayer.timeSamples >= (bar_Line[barIndex].noteTime - (secondPerBar * (timeRateBySpeed - 1))) * bgmPlayer.clip.frequency)
                {
                    bar_Line[barIndex].isStart = true;
                    barIndex++;
                }
            }

            if (beatCount % (4 * beatPerBar / 16) == 0)
            {
                if (isTic)
                {
                    ticPlayer.Play();
                }
            }
            nextSample += samplePerBeat;
            beatCount++;
        }
    }
}
