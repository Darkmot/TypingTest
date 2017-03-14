using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextDatabase : MonoBehaviour {

    public TextAsset dictionaryFile;
    private List<List<string>> line;
	void Start () {
        string wholeText = dictionaryFile.text;
        List<string> eachLine = new List<string>();
        eachLine.AddRange(wholeText.Split("\n"[0]));

        int curLength = 0;
        line = new List<List<string>>();
        line.Add(new List<string>());

        foreach (string newLine in eachLine)
        {
            while (newLine.Length > curLength)
            {
                line.Add(new List<string>());
                curLength++;
            }
            line[curLength].Add(newLine.ToUpper());
        }
        GameManager.InitGame();
    }

    public string GetRandomWord(int wordLength)
    {
        if ((wordLength > line.Count + 1) || (line[wordLength].Count == 0))
        {
            return null;
        }
        else
        {
            return line[wordLength][Random.Range(0, line[wordLength].Count)];
        }
    }
}
