using UnityEngine;
using TMPro;

public class PlayerView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI playerNameText;
    [SerializeField] private TextMeshProUGUI victoryPointsText;
    [SerializeField] private TextMeshProUGUI incomeText;
    [SerializeField] private TextMeshProUGUI creditsText;

    private Player _player;

    public void Initialize(Player player)
    {
        _player = player;
        UpdatePlayerView();
    }

    private void UpdatePlayerView()
    {
        if (_player == null) return;
        
        playerNameText.text = _player.PlayerName;
        victoryPointsText.text = $"Victory Points: {_player.VictoryPoints}";
        incomeText.text = $"Income: {_player.Income}";
        creditsText.text = $"Credits: {_player.Credits}";
    }

    public void OnPlayerDataChanged()
    {
        UpdatePlayerView();
    }
}
