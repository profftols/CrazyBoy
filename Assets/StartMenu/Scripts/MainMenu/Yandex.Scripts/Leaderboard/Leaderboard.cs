using UnityEngine;
using UnityEngine.UI;

public class Leaderboard : MonoBehaviour
{
    [SerializeField] private Button _buttonLeaderboard;

    private const string LeaderboardName = "Leaderboard";

    private void OnEnable()
    {
        _buttonLeaderboard.onClick.AddListener(OnViewLeaderTable);
    }

    private void OnDisable()
    {
        _buttonLeaderboard.onClick.RemoveListener(OnViewLeaderTable);
    }

    private void OnViewLeaderTable()
    {
        
    }
}