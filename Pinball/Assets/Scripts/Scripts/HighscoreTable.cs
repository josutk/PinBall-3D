using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighscoreTable : MonoBehaviour {
    private Transform entryContainer;
    private Transform entryTemplate;
    private List<Transform> highscoreEntryTransformList;
    public int saveScoreTrashold = 10;

    private string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    private string initials = "";
    public float moveDelay = 0.5f;
    private int letterSelect = 0;
    private int stepper = 0;
    private GameObject nextText;
    public GameObject gameDialog;
    public GameObject movimentMsg;
    private bool readyToMove = true;
    public Text[] Letters = null;
    private int scoreS;
    private Color selectedColor = Color.green;
    private Color AlfaColor = Color.yellow;
    private GameScript game;

    private SignalHandlerScript signalHandler;

    private void Awake() {
        entryContainer = transform.Find("HighscoreEntryContainer");
        entryTemplate = entryContainer.Find("HighscoreEntryTemplate");

        entryTemplate.gameObject.SetActive(false);
        
        scoreS = PlayerPrefs.GetInt("Score");
        if (PlayerPrefs.HasKey("HighscoreTable")) {
            string jsonString = PlayerPrefs.GetString("HighscoreTable");
            Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);

            highscoreEntryTransformList = new List<Transform>();
            foreach (HighScoreEntry highScoreEntry in highscores.highscoreEntryList) {
                CreatingHighscoreEntryTransform(highScoreEntry, entryContainer, highscoreEntryTransformList);
            }
        }
        else {
            HighScoreEntry defaultHighscoreEntry = new HighScoreEntry { score = 100, name = "AAA" };
            string defaultJson = JsonUtility.ToJson(defaultHighscoreEntry);
            PlayerPrefs.SetString("HighscoreTable", defaultJson);
            PlayerPrefs.Save();            
        }
    }

    void Start() {
        Letters[letterSelect].text = alphabet[stepper].ToString();
        Letters[letterSelect].color = AlfaColor;
        Input.ResetInputAxes();
        nextText = GameObject.Find("Button");
        game = Finder.GetGameController();
        signalHandler = Finder.GetSignalHandler();
    }

    void Update() {        
        if(!game.IsMenuLoaded)
        {
            Debug.Log("Menu isn't loaded!");
            gameDialog.SetActive(true);
            movimentMsg.SetActive(true);
            EnterName();
        }
    }

    private void CreatingHighscoreEntryTransform(HighScoreEntry highScoreEntry,
                                                 Transform container, List<Transform> transformList) {
        float templateHeight = 30f;
        Transform entryTransform = Instantiate(entryTemplate, container);
        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
        entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * transformList.Count);
        entryTransform.gameObject.SetActive(true);

        int rank = transformList.Count + 1;
        string rankString;
        switch (rank) {
            default: rankString = rank + "TH"; break;
            case 1: rankString = "1ST"; break;
            case 2: rankString = "2ND"; break;
            case 3: rankString = "3RD"; break;
        }

        entryTransform.Find("PositionText").GetComponent<Text>().text = rankString;

        int score = highScoreEntry.score;
        entryTransform.Find("ScoreText").GetComponent<Text>().text = score.ToString();

        string name = highScoreEntry.name;
        entryTransform.Find("NameText").GetComponent<Text>().text = name;

        // Set background visible odds and evens, easier to read.
        entryTransform.Find("Background").gameObject.SetActive(rank % 2 == 1);

        if (rank == 1) {
            // Highlight first
            entryTransform.Find("PositionText").GetComponent<Text>().color = Color.green;
            entryTransform.Find("ScoreText").GetComponent<Text>().color = Color.green;
            entryTransform.Find("NameText").GetComponent<Text>().color = Color.green;
        }

        transformList.Add(entryTransform);
    }

    private void AddHighscoreEntry(int score, string name) {
        // Create HighscoreEntry
        HighScoreEntry highScoreEntry = new HighScoreEntry { score = score, name = name };

        if (PlayerPrefs.HasKey("HighscoreTable")) {
            //Load saved Highscore
            string jsonString = PlayerPrefs.GetString("HighscoreTable");
            Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);

            int hsCount = highscores.highscoreEntryList.Count;

            if (highscores.highscoreEntryList.Count > 0) {
                HighScoreEntry lowestScore = highscores.highscoreEntryList[highscores.highscoreEntryList.Count - 1];
                
                if (lowestScore != null && saveScoreTrashold > 0 &&
                    highscores.highscoreEntryList.Count >= saveScoreTrashold && score > lowestScore.score) {                    
                    RemoveLastScoreTable();
                    hsCount--;
                }
            }
            if (hsCount <= saveScoreTrashold) {
                // Add new entry to Highscores
                jsonString = PlayerPrefs.GetString("HighscoreTable");
                highscores = JsonUtility.FromJson<Highscores>(jsonString);                
                highscores.highscoreEntryList.Add(highScoreEntry);

                SortListTable(highscores);

                // Save updated Highscores
                string json = JsonUtility.ToJson(highscores);
                PlayerPrefs.SetString("HighscoreTable", json);
                PlayerPrefs.Save();                
            }
        }
    }

    private void RemoveLastScoreTable() {
        //Load saved scores 
        string jsonString = PlayerPrefs.GetString("HighscoreTable");
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);
        
        if (saveScoreTrashold <= highscores.highscoreEntryList.Count) {
            int deleteCount = highscores.highscoreEntryList.Count - saveScoreTrashold;
            
            highscores.highscoreEntryList.Reverse();
            for (int i = 0; i <= deleteCount; i++) {
                //Remove last score table         
                highscores.highscoreEntryList.RemoveAt(i);                
            }
            SortListTable(highscores);
            //Save updated scores 
            string json = JsonUtility.ToJson(highscores);
            PlayerPrefs.SetString("HighscoreTable", json);
            PlayerPrefs.Save();            
        }
    }

    private void ClearScoreTable() {
        //Load saved scores 
        string jsonString = PlayerPrefs.GetString("HighscoreTable");
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);

        //Clear scores table 
        highscores.highscoreEntryList.Clear();

        //Save updated scores 
        string json = JsonUtility.ToJson(highscores);
        PlayerPrefs.SetString("HighscoreTable", json);
        PlayerPrefs.Save();
    }

    private void SortListTable(Highscores highscores) {
        for (int i = 0; i < highscores.highscoreEntryList.Count; i++) {
            for (int j = i + 1; j < highscores.highscoreEntryList.Count; j++) {
                if (highscores.highscoreEntryList[j].score > highscores.highscoreEntryList[i].score) {
                    //Swap
                    HighScoreEntry temp = highscores.highscoreEntryList[i];
                    highscores.highscoreEntryList[i] = highscores.highscoreEntryList[j];
                    highscores.highscoreEntryList[j] = temp;
                }
            }
        }
    }

    private void EnterName() {
        if (initials.Length < 3) {
            if (signalHandler.buttons.rightButton && readyToMove) {
            //if (Input.GetKey("up") && readyToMove) {
                if (stepper < alphabet.Length - 1) {
                    stepper++;
                    Letters[letterSelect].text = alphabet[stepper].ToString();
                    readyToMove = false;
                    Invoke("ResetReadyToMove", moveDelay);
                }
            }

            if (signalHandler.buttons.leftButton && readyToMove) {
                //if (Input.GetKey("down") && readyToMove) {
                if (stepper > 0) {
                    stepper--;
                    Letters[letterSelect].text = alphabet[stepper].ToString();
                    readyToMove = false;
                    Invoke("ResetReadyToMove", moveDelay);
                }
            }

            if (signalHandler.buttons.select && readyToMove) {
                if (letterSelect <= Letters.Length - 1) {
                    initials = initials + alphabet[stepper].ToString();

                    if (letterSelect == Letters.Length - 1) {
                        letterSelect = 3; // breaks loop then sets name 
                        string nameFromInput = initials;
                        //int scoreTest = 47;

                        foreach (GameObject scores in GameObject.FindGameObjectsWithTag("Template")) {
                            Destroy(scores);
                        }

                        AddHighscoreEntry(scoreS, nameFromInput);

                        string jsonString = PlayerPrefs.GetString("HighscoreTable");
                        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);

                        highscoreEntryTransformList = new List<Transform>();
                        foreach (HighScoreEntry highScoreEntry in highscores.highscoreEntryList) {
                            CreatingHighscoreEntryTransform(highScoreEntry, entryContainer, highscoreEntryTransformList);
                        }
                        game.LoadMenu();
                    }

                    if (letterSelect < Letters.Length - 1) {
                        letterSelect++;
                        Letters[letterSelect].color = AlfaColor;
                        Letters[letterSelect - 1].color = selectedColor;
                        readyToMove = false;
                        Invoke("ResetReadyToMove", moveDelay);
                    }

                    stepper = 0;
                }
            }
        }
    }

    void ResetReadyToMove() {
        readyToMove = true;
    }

    private class Highscores {
        public List<HighScoreEntry> highscoreEntryList;
    }

    // Representes a single high score entry
    [System.Serializable]
    private class HighScoreEntry {
        public int score;
        public string name;
    }
}
