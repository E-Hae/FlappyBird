using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public static string playerName;
    public static int score;
    public static List<KeyValuePair<string, int>> ranking = new List<KeyValuePair<string, int>>();

    public Text scoreText;
    public Text lifeText;

    [SerializeField] private GameObject _GameOverUI;
    [SerializeField] private GameObject _resultUI;
    [SerializeField] private RankingText[] _rankingTexts;

    [SerializeField] private Text _introText;

    [SerializeField] private Bird _bird;
    [SerializeField] private ObstaclePool _obstaclePool;

    private bool _isGameOver;

    public bool IsGameOver
    {
        get
        {
            return _isGameOver;
        }
        set
        {
            if (value)
            {
                _GameOverUI.SetActive(true);
                StartCoroutine(ShowRankingCoroutine());
            }

            _isGameOver = value;
        }
    }

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        IsGameOver = false;

        score = 0;

        StartCoroutine(StartGameCoroutine());
    }

    private IEnumerator StartGameCoroutine()
    {
        _introText.enabled = true;

        for (int i = 3; i > 0; i--)
        {
            _introText.text = i.ToString();
            yield return new WaitForSeconds(1f);
        }

        _introText.enabled = false;
        _bird.animator.enabled = true;
        _bird.rigid.constraints = RigidbodyConstraints2D.None | RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;

        _obstaclePool.InvokeColumnSpawn();
    }

    private int GetRankingCount()
    {
        if (ranking.Count < 10)
        {
            return ranking.Count;
        }
        else
        {
            return 10;
        }
    }

    private IEnumerator ShowRankingCoroutine()
    {
        ranking.Add(new KeyValuePair<string, int>(playerName, score));
        List<KeyValuePair<string, int>> sortRanking = ranking.OrderBy(x => x.Value).Reverse().ToList();

        yield return new WaitForSeconds(3f);

        _resultUI.SetActive(true);

        for (int i = 0; i < GetRankingCount(); i++)
        {
            _rankingTexts[i].numberText.text = (i + 1).ToString();
            _rankingTexts[i].nameText.text = sortRanking[i].Key.ToString();
            _rankingTexts[i].scoreText.text = sortRanking[i].Value.ToString();
            yield return null;
        }
    }

    public void LoadScene(string _sceneName)
    {
        SceneManager.LoadScene(_sceneName);
    }
}