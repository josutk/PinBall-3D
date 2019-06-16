using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighscoreTable : MonoBehaviour{
    private Transform entryContainer;
    private Transform entryTemplate;
    private List<Transform> highscoreEntryTransformList;

    private void Awake(){
        entryContainer = transform.Find("HighscoreEntryContainer");
        entryTemplate = entryContainer.Find("HighscoreEntryTemplate");
        
        entryTemplate.gameObject.SetActive(false);        

        AddHighscoreEntry(100400, "RGI");
        //ClearScoreTable();
        string jsonString = PlayerPrefs.GetString("HighscoreTable");
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);

        for(int i = 0; i < highscores.highscoreEntryList.Count; i++){
            for(int j = i + 1; j < highscores.highscoreEntryList.Count; j++){
                if(highscores.highscoreEntryList[j].score > highscores.highscoreEntryList[i].score){
                    //Swap
                    HighScoreEntry temp = highscores.highscoreEntryList[i];
                    highscores.highscoreEntryList[i] = highscores.highscoreEntryList[j];
                    highscores.highscoreEntryList[j] = temp;
                }
            }
        }

        highscoreEntryTransformList = new List<Transform>();
        foreach (HighScoreEntry highScoreEntry in highscores.highscoreEntryList){
            CreatingHighscoreEntryTransform(highScoreEntry, entryContainer, highscoreEntryTransformList);
        }
    }

    private void CreatingHighscoreEntryTransform(HighScoreEntry highScoreEntry,
                                                 Transform container, List<Transform> transformList){
        float templateHeight = 30f;
        Transform entryTransform = Instantiate(entryTemplate, container);
        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
        entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * transformList.Count);
        entryTransform.gameObject.SetActive(true); 

        int rank = transformList.Count + 1;
        string rankString;
        switch(rank){
            default: rankString =rank + "TH"; break;
            case 1: rankString ="1ST"; break;
            case 2: rankString ="2ND"; break;
            case 3: rankString ="3RD"; break;
        }

        entryTransform.Find("PositionText").GetComponent<Text>().text = rankString;

        int score = highScoreEntry.score;
        entryTransform.Find("ScoreText").GetComponent<Text>().text = score.ToString();

        string name = highScoreEntry.name;
        entryTransform.Find("NameText").GetComponent<Text>().text = name;

        // Set background visible odds and evens, easier to read.
        entryTransform.Find("Background").gameObject.SetActive(rank % 2 == 1);

        if(rank == 1){
            // Highlight first
            entryTransform.Find("PositionText").GetComponent<Text>().color = Color.green;
            entryTransform.Find("ScoreText").GetComponent<Text>().color = Color.green;
            entryTransform.Find("NameText").GetComponent<Text>().color = Color.green;
        }
        
        transformList.Add(entryTransform);
    }

    private void AddHighscoreEntry(int score, string name){
        // Create HighscoreEntry
        HighScoreEntry highScoreEntry = new HighScoreEntry { score = score, name = name };

        //Load saved Highscore
        string jsonString = PlayerPrefs.GetString("HighscoreTable");
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);

        // Add new entry to Highscores
        highscores.highscoreEntryList.Add(highScoreEntry);

        // Save updated Highscores
        string json = JsonUtility.ToJson(highscores);
        PlayerPrefs.SetString("HighscoreTable", json);
        PlayerPrefs.Save();
    }

    private void ClearScoreTable() { 
        //Load saved scores 
        string jsonString = PlayerPrefs.GetString("HighscoreTable"); 
        Highscores scores = JsonUtility.FromJson<Highscores>(jsonString); 

        //Clear scores table 
        scores.highscoreEntryList.Clear(); 

        //Save updated scores 
        string json = JsonUtility.ToJson(scores); 
        PlayerPrefs.SetString("HighscoreTable", json); 
        PlayerPrefs.Save(); 
    }

    private class Highscores {
        public List<HighScoreEntry> highscoreEntryList;
    }

    // Representes a single high score entry
    [System.Serializable]
    private class HighScoreEntry{
        public int score;
        public string name;
    }
}
