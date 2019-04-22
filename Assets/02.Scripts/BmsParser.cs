using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using UnityEngine.UI;

public class BmsParser : MonoBehaviour
{
    public string bmsFileName;
    public bool isFinishLoad;
    public Bms bms;
    BeatCreator beatCreator;

    public Text title;
    public Text artist;
    public Text bpm;


    public Bms getBms()
    {
        return bms;
    }
    private void Awake()
    {
        bms = gameObject.GetComponent<Bms>();
        beatCreator = GameObject.Find("BeatCreator").GetComponent<BeatCreator>();
    }
    private void Start()
    {
        title.fontSize = 30;
        artist.fontSize = 30;
        bpm.fontSize = 30;
    }
    public void BmsLoad(string[] lineData)
    {

        BarData barData;
        foreach (string line in lineData)
        {
            if (line.StartsWith("#"))
            {
                string[] data = line.Split(' ');

                // 데이터 섹션이 아니면서 헤더 데이터가 없는 경우에는 건너 뜀.
                if (data[0].IndexOf(":") == -1 && data.Length == 1)
                {
                    continue;
                }

                // 헤더 섹션.
                if (data[0].Equals("#TITLE"))
                {
                    bms.setTitle(data[1]);
                    title.text = "Title : " + data[1];
                }
                else if (data[0].Equals("#ARTIST"))
                {
                    bms.setArtist(data[1]);
                    artist.text = "Artist : " + data[1];
                }
                else if (data[0].Equals("#BPM"))
                {
                    bms.setBpm(float.Parse(data[1]));
                    beatCreator.bpm = (float)float.Parse(data[1]);
                    bpm.text = "BPM : " + data[1];
                }
                else if (data[0].Equals("#PLAYER"))
                {
                }
                else if (data[0].Equals("#GENRE"))
                {
                }
                else if (data[0].Equals("#PLAYLEVEL"))
                {
                }
                else if (data[0].Equals("#RANK"))
                {
                }
                else if (data[0].Equals("#TOTAL"))
                {
                }
                else if (data[0].Equals("#VOLWAV"))
                {
                }
                else if (data[0].Equals("#MIDIFILE"))
                {
                }
                else if (data[0].Substring(0, 4).Equals("#WAV"))
                {
                }
                else if (data[0].Equals("#BMP"))
                {
                }
                else if (data[0].Equals("#STAGEFILE"))
                {
                }
                else if (data[0].Equals("#VIDEOFILE"))
                {
                }
                else if (data[0].Equals("#BGA"))
                {
                }
                else if (data[0].Equals("#STOP"))
                {
                }
                else if (data[0].Equals("#LNTYPE"))
                {
                    bms.setLnType(int.Parse(data[1]));
                }
                else if (data[0].Equals("#LNOBJ"))
                {
                }
                else if (data[0].IndexOf(":") != -1)
                {
                    // 위의 경우에 모두 해당하지 않을 경우, 데이터 섹션.
                    int bar = 0;
                    Int32.TryParse(data[0].Trim().Substring(1, 3), out bar);

                    int channel = 0;
                    Int32.TryParse(data[0].Trim().Substring(4, 2), out channel);

                    string noteStr = data[0].Trim().Substring(7);
                    List<Dictionary<int, float>> noteData = getNoteDataOfStr(noteStr, bar, bms.getBpm()); // 노트 데이터 생성

                    barData = gameObject.AddComponent<BarData>();
                    barData.setBar(bar);
                    barData.setChannel(channel);
                    barData.setNoteDataList(noteData);
                    //barData.debug();

                    bms.addBarData(barData);
                }
            }
        }

        if (bms.getBarDataList().Count != 0)
        {
            isFinishLoad = true;
        }

        //bms.debug();
    }

    private List<Dictionary<int, float>> getNoteDataOfStr(string str, int bar, double bpm)
    {

        string tempStr = str.Trim();
        List<Dictionary<int, float>> noteDataList = new List<Dictionary<int, float>>();

        float barCount = (float)bar;
        float totalBeatOfBar = 0; // 현재 Bar의 총 노트수.
        if (tempStr.Length != 0)
        {
            totalBeatOfBar = tempStr.Length / 2; // '00', '01' 등 2개가 하나의 노트이므로 /2를 한다.
        }

        float secondPerBar = 60.0f / (float)bpm * 4.0f;     // Bar 당 시간(초).
        float preSecond = barCount * secondPerBar;          // 앞선 bar의 흐른시간.

        float beatCount = 0;

        while (true)
        {

            int key = 0;
            Int32.TryParse(tempStr.Substring(0, 2), out key);

            float time = 0;
            if (key != 0)
            {
                time = preSecond + (secondPerBar / totalBeatOfBar * beatCount);
            }
            else
            {
                time = 0;
            }

            Dictionary<int, float> noteData = new Dictionary<int, float>();
            noteData.Add(key, time);

            noteDataList.Add(noteData);
            bms.setTotalPlayTime(time);
            bms.totalBarCount = bar;

            if (tempStr.Length > 2)
            {
                tempStr = tempStr.Substring(2);
            }
            else
            {
                break;
            }

            beatCount++;
        }

        // 총노트수 증가.
        foreach (Dictionary<int, float> noteData in noteDataList)
        {
            foreach (int key in noteData.Values)
            {
                if (key != 0)
                {
                    bms.sumTotalNoteCount();
                }
            }
        }

        return noteDataList;
    }

}
