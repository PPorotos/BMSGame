using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject notePrefab;
    public GameObject barPrefab;
    public Text comboText;
    public Text scoreText;

    public string bmsName = "Lovely_Summer.txt";

    public bool isTic;
    public int combo = 0;
    public int count = 0;
    public int score = 0;

    float beatPerBar = 32f;
    int defaultSpeed = 7;
    int timeRateBySpeed = 2;

    GameObject note;
    NoteObj noteSC;
    BmsParser bmsLoader;
    Bms bms;
    GameObject plane_Top;
    BeatCreator beatCreator;

    ClickButton button;

    string[] lineData;

    private void Awake()
    {
        TextAsset ta = Resources.Load("test") as TextAsset;
        lineData = ta.text.Split('\n');

        //lineData = File.ReadAllLines("Assets/Resources/Lovely_Summer.txt");
        bmsLoader = GameObject.Find("BmsLoader").GetComponent<BmsParser>();
        beatCreator = GameObject.Find("BeatCreator").GetComponent<BeatCreator>();
        bms = bmsLoader.getBms();

        comboText.fontSize = 40;
        scoreText.fontSize = 40;

    }

    private IEnumerator Start()
    {
        bmsLoader.BmsLoad(lineData);

        GameObject plane_Top = GameObject.Find("Top");
        float startPositonY = plane_Top.transform.position.y;

        GameObject lineJudgment = GameObject.Find("Bottom");
        float judgmentPositionY = lineJudgment.transform.position.y;

        GameObject lineCenter = GameObject.Find("LineCenter");

        List<NoteObj> noteObj_Line_1 = new List<NoteObj>();
        List<NoteObj> noteObj_Line_2 = new List<NoteObj>();
        List<NoteObj> noteObj_Line_3 = new List<NoteObj>();
        List<NoteObj> noteObj_Line_4 = new List<NoteObj>();
        List<NoteObj> noteObj_Line_5 = new List<NoteObj>();
        List<NoteObj> bar_Line = new List<NoteObj>();

        float destroyDelayPositionY = 30;
        float destroyDelayTime = bms.getTotalPlayTime() + 1;

        float secondPerBar = 60.0f / (float)bms.getBpm() * 4.0f;

        foreach (BarData barData in bms.getBarDataList())
        {
            float linePositionX = lineCenter.transform.position.x;
            bool isLongChannel = false;

            int channel = barData.getChannel();
            if (channel == 11 || channel == 51)
            {
                linePositionX = lineCenter.transform.position.x - 4;
            }
            else if (channel == 12 || channel == 52)
            {
                linePositionX = lineCenter.transform.position.x - 2;
            }
            else if (channel == 13 || channel == 53)
            {
                linePositionX = lineCenter.transform.position.x;
            }
            else if (channel == 14 || channel == 54)
            {
                linePositionX = lineCenter.transform.position.x + 2;
            }
            else if (channel == 15 || channel == 55)
            {
                linePositionX = lineCenter.transform.position.x + 4;
            }

            if (channel == 51 || channel == 52 || channel == 53 || channel == 54 || channel == 55)
            {
                isLongChannel = true;
            }

            foreach (Dictionary<int, float> noteData in barData.getNoteDataList())
            {
                foreach (int key in noteData.Keys)
                {
                    // 단노트.
                    if (isLongChannel == false && key != 0 && channel != 16)
                    {
                        float noteTime = noteData[key];

                        note = (GameObject)Instantiate(notePrefab, new Vector3(linePositionX, startPositonY, 0), Quaternion.identity);
                        MeshRenderer noteRenderer = note.GetComponent<MeshRenderer>();
                        noteSC = note.GetComponent<NoteObj>();
                        noteSC.speed = defaultSpeed;
                        noteSC.destroyPositionY = judgmentPositionY - destroyDelayPositionY;
                        noteSC.destroyDelayTime = destroyDelayTime;
                        noteSC.noteTime = noteTime;
                        noteSC.channel = channel;

                        if (channel == 11)
                        {
                            noteRenderer.material.color = Color.blue;
                            noteSC.noteBlockNum = 1;
                            noteObj_Line_1.Add(noteSC);
                            button = GameObject.Find("Button1").GetComponent<ClickButton>();
                            button.notePosition.Add(noteSC.gameObject);
                        }
                        else if (channel == 12)
                        {
                            noteRenderer.material.color = Color.red;
                            noteSC.noteBlockNum = 2;
                            noteObj_Line_2.Add(noteSC);
                            button = GameObject.Find("Button2").GetComponent<ClickButton>();
                            button.notePosition.Add(noteSC.gameObject);
                        }
                        else if (channel == 13)
                        {
                            noteRenderer.material.color = Color.green;
                            noteSC.noteBlockNum = 3;
                            noteObj_Line_3.Add(noteSC);
                            button = GameObject.Find("Button3").GetComponent<ClickButton>();
                            button.notePosition.Add(noteSC.gameObject);
                        }
                        else if (channel == 14)
                        {
                            noteRenderer.material.color = Color.red;
                            noteSC.noteBlockNum = 4;
                            noteObj_Line_4.Add(noteSC);
                            button = GameObject.Find("Button4").GetComponent<ClickButton>();
                            button.notePosition.Add(noteSC.gameObject);
                        }
                        else if (channel == 15)
                        {
                            noteRenderer.material.color = Color.blue;
                            noteSC.noteBlockNum = 5;
                            noteObj_Line_5.Add(noteSC);
                            button = GameObject.Find("Button5").GetComponent<ClickButton>();
                            button.notePosition.Add(noteSC.gameObject);
                        }
                    }
                }
            }
        }

        noteObj_Line_1.Sort(delegate (NoteObj a, NoteObj b) {
            return a.noteTime.CompareTo(b.noteTime);
        });
        noteObj_Line_2.Sort(delegate (NoteObj a, NoteObj b) {
            return a.noteTime.CompareTo(b.noteTime);
        });
        noteObj_Line_3.Sort(delegate (NoteObj a, NoteObj b) {
            return a.noteTime.CompareTo(b.noteTime);
        });
        noteObj_Line_4.Sort(delegate (NoteObj a, NoteObj b) {
            return a.noteTime.CompareTo(b.noteTime);
        });
        noteObj_Line_5.Sort(delegate (NoteObj a, NoteObj b) {
            return a.noteTime.CompareTo(b.noteTime);
        });
        bar_Line.Sort(delegate (NoteObj a, NoteObj b) {
            return a.noteTime.CompareTo(b.noteTime);
        });

        beatCreator.noteObj_Line_1 = noteObj_Line_1;
        beatCreator.noteObj_Line_2 = noteObj_Line_2;
        beatCreator.noteObj_Line_3 = noteObj_Line_3;
        beatCreator.noteObj_Line_4 = noteObj_Line_4;
        beatCreator.noteObj_Line_5 = noteObj_Line_5;
        beatCreator.bar_Line = bar_Line;

        //beatCreator.bpm = (float)bms.getBpm();
        beatCreator.beatPerBar = beatPerBar;
        beatCreator.timeRateBySpeed = timeRateBySpeed;

        AudioClip bgm = Resources.Load("Assets/Resources/Sound/" + bmsName) as AudioClip;
        beatCreator.bgmSound = bgm;

        beatCreator.isTic = isTic;

        yield return new WaitForEndOfFrame();

        beatCreator.isStart = true;
    }

    private void Update()
    {
        comboText.text = "Combo : " + combo.ToString();
        scoreText.text = "Score : " + score.ToString();

    }




}
