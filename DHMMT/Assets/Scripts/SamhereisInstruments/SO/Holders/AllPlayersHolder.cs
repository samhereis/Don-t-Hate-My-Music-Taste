using IdentityCards;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Holders
{
    [CreateAssetMenu(fileName = "AllPlayersHolder ", menuName = "Scriptables/Holder/AllPlayersHolder")]
    public sealed class AllPlayersHolder : ScriptableObject
    {
        private const string CURRENT_PLAYER_KEY = "CurrentPlayer";

        public APlayerIdentityCard currentPlayer { get { return _currentPlayer != null ? _currentPlayer : _currentPlayer = _playerIdentityCards[PlayerPrefs.GetInt(CURRENT_PLAYER_KEY, 0)]; } private set { _currentPlayer = value; } }
        [SerializeField] private APlayerIdentityCard _currentPlayer;

        [SerializeField] private List<APlayerIdentityCard> _playerIdentityCards;

        public APlayerIdentityCard[] GetAllPlayers()
        {
            return _playerIdentityCards.ToArray();
        }

        public APlayerIdentityCard GetNextPlayer(APlayerIdentityCard current)
        {
            int index = _playerIdentityCards.IndexOf(current);

            if (index == _playerIdentityCards.Count - 1) return _playerIdentityCards.First(); else return _playerIdentityCards[++index];
        }

        public APlayerIdentityCard GetPrevuiusPlayer(APlayerIdentityCard current)
        {
            int index = _playerIdentityCards.IndexOf(current);

            if (index == 0) return _playerIdentityCards.Last(); else return _playerIdentityCards[--index];
        }

        public void SelectPlayer(APlayerIdentityCard aPlayer)
        {
            _currentPlayer = aPlayer;

            int index = _playerIdentityCards.IndexOf(_currentPlayer);
            PlayerPrefs.SetInt(CURRENT_PLAYER_KEY, index);
        }
    }
}