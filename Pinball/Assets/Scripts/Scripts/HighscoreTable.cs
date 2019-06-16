using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighscoreTable : MonoBehaviour {
    private Transform entryContainer;
    private Transform entryTemplate;
    private List<Transform> highscoreEntryTransformList;

    private void Awake() {
        entryContainer = transform.Find("HighscoreEntryContainer");
        entryTemplate = entryContainer.Find("HighscoreEntryTemplate");

        entryTemplate.gameObject.SetActive(false);

        //AddHighscoreEntry(137210, "FDP");
        RemoveLastScoreTable();        

        string jsonString = PlayerPrefs.GetString("HighscoreTable");
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);

        highscoreEntryTransformList = new List<Transform>();
        foreach (HighScoreEntry highScoreEntry in highscores.highscoreEntryList) {
            CreatingHighscoreEntryTransform(highScoreEntry, entryContainer, highscoreEntryTransformList);
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
        Highscores highscores;

        string json;
        string jsonString;

        if (PlayerPrefs.HasKey("HighscoreTable")) {
            //Load saved Highscore
            jsonString = PlayerPrefs.GetString("HighscoreTable");
            highscores = JsonUtility.FromJson<Highscores>(jsonString);

            // Add new entry to Highscores
            highscores.highscoreEntryList.Add(highScoreEntry);

            SortListTable(highscores);

            // Save updated Highscores
            json = JsonUtility.ToJson(highscores);

            PlayerPrefs.SetString("HighscoreTable", json);

            PlayerPrefs.Save();
        }
        //else {
        //    HighScoreEntry defaultHighScoreEntry = new HighScoreEntry { score = 1000, name = "AIR" };            
        //    json = JsonUtility.ToJson(defaultHighScoreEntry);
        //    PlayerPrefs.SetString("HighscoreTable", json);
        //    PlayerPrefs.Save();
        //}
        //PlayerPrefs.DeleteKey("HighscoreTable");

    }

    private void RemoveLastScoreTable() {
        //Load saved scores 
        string jsonString = PlayerPrefs.GetString("HighscoreTable");
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);

        Debug.Log("First " + PlayerPrefs.GetString("HighscoreTable"));        

        int savedScore = 5;

        if (savedScore <= highscores.highscoreEntryList.Count) {
            int deleteCount = highscores.highscoreEntryList.Count - savedScore;
            highscores.highscoreEntryList.Reverse();
            for (int i = 0; i < deleteCount; i++) {
                //Remove last score table         
                highscores.highscoreEntryList.RemoveAt(i);
            }
            SortListTable(highscores);
            //Save updated scores 
            string json = JsonUtility.ToJson(highscores);
            PlayerPrefs.SetString("HighscoreTable", json);
            PlayerPrefs.Save();
            Debug.Log(PlayerPrefs.GetString("HighscoreTable"));
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
