using UnityEngine;

public class E_F_H_Page : MonoBehaviour
{
    // Controll main UI on "E-F-H" map. "E-F-H_MatchType" controls main player's part on this map

    public static E_F_H_Page instance;

    [SerializeField] private MatchSO _matchSO;

    [SerializeField] private GameObject _gamePlayWindow;

    [Header("Win Window")]
    [SerializeField] private GameObject _winWindow;
    [SerializeField] private CurrentScore _currentScore;
    [SerializeField] private YourRecord _yourRecord;

    [Header("Loose Window")]
    [SerializeField] private GameObject _looseWindow;

    void Awake()
    {
        MessageScript.instance.ShowMessage(MessageScript.instance.StayUnderTheLightAndFindTheExit, 10);
    }

    void OnEnable()
    {
        instance ??= this;

        _gamePlayWindow.SetActive(true);

        if (SecondsCount.instance != null)
        {
            SecondsCount.instance.Beggin(0, SecondsCount.instance.GetSeconds());
        }
    }

    public void OnWin()
    {
        if (SecondsCount.instance.GetSeconds() < _matchSO.GetRecordForTheScene())
        {
            _matchSO.SetRecordForTheScene(SecondsCount.instance.GetSeconds());
        }
        else if (_matchSO.GetRecordForTheScene() < 5)
        {
            _matchSO.SetRecordForTheScene(SecondsCount.instance.GetSeconds());
        }

        _yourRecord.SetRecotrdText(_matchSO.GetRecordForTheScene());
        _currentScore.SetScoreText(SecondsCount.instance.GetSeconds());

        PauseUnpause.SetPause(true);
        _gamePlayWindow.SetActive(false);
        _winWindow.SetActive(true);
    }

    public void OnLoose()
    {
        _currentScore.SetScoreText(SecondsCount.instance.GetSeconds());
        _yourRecord.SetRecotrdText(PlayerPrefs.GetInt(""));

        PauseUnpause.SetPause(true);
        _gamePlayWindow.SetActive(false);
        _looseWindow.SetActive(true);
    }

    public void OnReplay()
    {
        Destroy(PlayerHealthData.instance.gameObject);
        PauseUnpause.SetPause(false);
        StartCoroutine(SceneLoadController.LoadScene(2));
    }
}
