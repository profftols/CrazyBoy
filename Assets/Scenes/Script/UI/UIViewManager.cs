using System.Linq;
using UnityEngine;

public class UIViewManager : MonoBehaviour
{
    [SerializeField] protected UIPanel[] panels;
    
    private UIDefeatGame _defeatGame;
    private UIVictoryGame _victoryGame;

    protected virtual void OnEnable()
    {
        EventBus.OnDefeatGame += DefeatScreen;
        EventBus.OnVictoryGame += VictoryScreen;
    }
    
    private void Start()
    {
        _defeatGame = panels.OfType<UIDefeatGame>().FirstOrDefault();
        _victoryGame = panels.OfType<UIVictoryGame>().FirstOrDefault();
    }

    protected virtual void OnDisable()
    {
        EventBus.OnDefeatGame -= DefeatScreen;
        EventBus.OnVictoryGame -= VictoryScreen;
    }

    private void DefeatScreen(float value)
    {
        _defeatGame.Show();
        _defeatGame.ViewScreen(value);
    }

    private void VictoryScreen(float value)
    {
        _victoryGame.Show();
        _victoryGame.ViewScreen(value);
    }
}