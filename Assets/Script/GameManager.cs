using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    Title,
    Start,
    Play,
    Paused,
    Intermission,
    Score
}

public class GameManager : MonoBehaviour {

    private static GameManager _instance;
    public TextDatabase textDB;
    public Transform gameplayScreen;
    public PlayerShip playerShip;
    public List<BaseEnemy> enemyList;
    public GameObject enemySmallPrefab;
    public GameObject enemyMedPrefab;
    public GameObject enemyFighterPrefab;
    public GameObject enemyBigPrefab;
    public GameObject enemyBulletPrefab;
    public GameState gameState;
    public int enemyNumber;
    public int enemySpeed;
    public int level;

    private KeyCode[] letterKeys = { KeyCode.A,KeyCode.B,KeyCode.C,KeyCode.D,KeyCode.E,KeyCode.F,KeyCode.G,KeyCode.H,
                                   KeyCode.I,KeyCode.J,KeyCode.K,KeyCode.L,KeyCode.M,KeyCode.N,KeyCode.O,KeyCode.P,
                                   KeyCode.Q,KeyCode.R,KeyCode.S,KeyCode.T,KeyCode.U,KeyCode.V,KeyCode.W,KeyCode.X,
                                   KeyCode.Y,KeyCode.Z};
    private char[] letters = {'A','B','C','D','E','F','G','H','I','J','K','L','M','N','O','P','Q','R','S','T','U','V','W','X','Y','Z'};
    char currentKey;
    BaseEnemy currentEnemy;



    bool IsLetterPressed()
    {
        int indexLetter = 0;
        foreach (KeyCode letter in letterKeys)
        {
            if (Input.GetKeyDown(letter))
            {
                currentKey = letters[indexLetter];
                return true;
            }
            indexLetter++;
        }
        return false;
    }
	void Awake () {
        if (_instance == null)
        {
            _instance = this;            
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
	}

    public static TextDatabase TextDB
    {
        get
        {
            return _instance.textDB;
        }
    }
    public static string GetLetter(int letterIndex)
    {
        return "" + _instance.letters[letterIndex];
    }
    public static void InitLevel()
    {
        _instance.level = 0;
        InitNextLevel();
    }
    public static int Level
    {
        get
        {
            return _instance.level;
        }
    }
    public static void InitNextLevel()
    {
        _instance.level++;
        _instance.enemyList = new List<BaseEnemy>();
        int startRandom = 640;
        for (int i = 0; i < _instance.enemyNumber + (_instance.level / 4); i++)
        {
            GameObject g = Instantiate(_instance.enemySmallPrefab);
            g.transform.SetParent(_instance.gameplayScreen, false);
            BaseEnemy enemy = g.GetComponent<BaseEnemy>();          
            enemy.InitEnemy(new Vector2(Random.Range(10, 320), startRandom),_instance.enemySpeed + (_instance.level * 1) );
            startRandom = Random.Range(startRandom, startRandom + 20);
            _instance.enemyList.Add(enemy);
        }
        for (int i = 0; i < (_instance.level / 3); i++)
        {
            GameObject g = Instantiate(_instance.enemyMedPrefab);
            g.transform.SetParent(_instance.gameplayScreen, false);
            MedEnemy enemy = g.GetComponent<MedEnemy>();
            enemy.InitEnemy(new Vector2(Random.Range(10, 320), startRandom), _instance.enemySpeed + (_instance.level * 1) - 5);
            startRandom = Random.Range(startRandom, startRandom + 30);
            _instance.enemyList.Add(enemy);
        }
        for (int i = 0; i < (_instance.level / 6); i++)
        {
            GameObject g = Instantiate(_instance.enemyBigPrefab);
            g.transform.SetParent(_instance.gameplayScreen, false);
            BigEnemy enemy = g.GetComponent<BigEnemy>();
            enemy.InitEnemy(new Vector2(Random.Range(10, 320), startRandom), _instance.enemySpeed + (_instance.level * 1) - 10);
            startRandom = Random.Range(startRandom, startRandom + 40);
            _instance.enemyList.Add(enemy);
        }
        _instance.currentEnemy = null;
        _instance.playerShip.gameObject.SetActive(true);
        _instance.gameState = GameState.Play;

    }
    public static void AddEnemy(BaseEnemy enemy)
    {
        _instance.enemyList.Add(enemy);
    }

    public static PlayerShip Player
    {
        get
        {
            return _instance.playerShip; 
        }
    }

    public static GameState State
    {
        get
        {
            return _instance.gameState;
        }
        set
        {
            _instance.gameState = value;
        }
    }
    public static Transform GameScreen
    {
        get
        {
            return _instance.gameplayScreen;
        }
    }
    public static GameObject EnemyFighterPrefab
    {
        get
        {
            return _instance.enemyFighterPrefab;
        }
    }
    public static GameObject EnemyBulletPrefab
    {
        get
        {
            return _instance.enemyBulletPrefab;
        }
    }

    void Update()
    {
        if (_instance.gameState == GameState.Play)
        {
            if ( (_instance.playerShip.alive)&&(IsLetterPressed()))
            {
                bool letterHit = false;
                if (_instance.currentEnemy == null)
                {
                    foreach (BaseEnemy enemy in enemyList)
                    {
                        if ((enemy.wordToHit.Length > 0) && (enemy.wordToHit[0] == currentKey))
                        {
                            _instance.currentEnemy = enemy;
                            enemy.transform.SetAsLastSibling();
                            enemy.SetSelected(true);
                            if (enemy.wordToHit.Length > 1)
                            {
                                enemy.ResizeWord(enemy.wordToHit.Substring(1, enemy.wordToHit.Length - 1));
                            }
                            else
                            {
                                enemy.ResizeWord("");
                                _instance.currentEnemy = null;
                            }
                            _instance.playerShip.ShotEnemy(enemy);
                            letterHit = true;
                            break;
                        }
                    }
                }
                else
                {
                    if ((_instance.currentEnemy.wordToHit.Length > 0) && (_instance.currentEnemy.wordToHit[0] == currentKey))
                    {
                        BaseEnemy prevEnemy = _instance.currentEnemy;
                        _instance.currentEnemy.SetSelected(true);
                        _instance.currentEnemy.transform.SetAsLastSibling();
                        if (_instance.currentEnemy.wordToHit.Length > 1)
                        {
                            _instance.currentEnemy.ResizeWord(_instance.currentEnemy.wordToHit.Substring(1, _instance.currentEnemy.wordToHit.Length - 1));
                        }
                        else
                        {
                            _instance.currentEnemy.ResizeWord("");
                            _instance.currentEnemy = null;
                        }
                        _instance.playerShip.ShotEnemy(prevEnemy);
                        letterHit = true;
                    }
                }

                _instance.playerShip.AddCombo(letterHit);
            }
            else if (Input.GetKeyDown(KeyCode.Backspace))
            {
                ClearCurrentEnemy();
            }
            else if (Input.GetKeyDown(KeyCode.Return))
            {
                _instance.playerShip.BlastEMP();
            }
        }
    }


    public static void ClearCurrentEnemy()
    {
        if (_instance.currentEnemy != null)
        {
            _instance.currentEnemy.SetSelected(false);
            _instance.currentEnemy = null;
        }
    }

    public static void ClearEnemyList()
    {
        int enemyCount = _instance.enemyList.Count;
        for (int i = enemyCount - 1; i >= 0; i--)
        {
            GameObject g = _instance.enemyList[i].gameObject;
            _instance.enemyList.RemoveAt(i);
            Destroy(g);
        }
    }
    public static bool RemoveEnemy(BaseEnemy enemy)
    {
        bool removed = _instance.enemyList.Remove(enemy);
        Destroy(enemy.gameObject);

        if (_instance.enemyList.Count <= 0)
        {
            _instance.playerShip.gameplayScreen.intermission.StartIntermission(_instance.level+1,_instance.playerShip.score);
            GameManager.State = GameState.Intermission;
        }
        return removed;
    }
    public static int EnemyCount
    {
        get
        {
            return _instance.enemyList.Count;
        }
    }
}
