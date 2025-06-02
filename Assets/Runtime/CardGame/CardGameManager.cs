using Naninovel;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

[InitializeAtRuntime]
public class CardGameManager : IEngineService
{
    private CardGameUI _gameUI;
    private List<CardController> _cards;
    private CardController _selectedCard1;
    private CardController _selectedCard2;
    private bool _isInputBlocked;

    public UniTask InitializeServiceAsync() => UniTask.CompletedTask;

    public void DestroyService() { }

    public void ResetService() 
    {
        if (_cards == null) return;

        foreach (var card in _cards)
            Object.Destroy(card.gameObject);

        _cards.Clear();
    }

    public async UniTask StartGame(int totalPairs)
    {
        _gameUI = Engine.GetService<UIManager>().GetUI<CardGameUI>();
        _gameUI.Show();
        InitializeCards(totalPairs);
        await WaitForGameCompletion();
        _gameUI.Hide();
    }

    private void InitializeCards(int totalPairs)
    {
        var cardIds = Enumerable.Range(0, totalPairs)
            .SelectMany(i => new[] { i, i })
            .OrderBy(_ => Random.value)
            .ToList();

        _cards = _gameUI.InitializeGrid(cardIds);
    }

    private async UniTask WaitForGameCompletion()
    {
        while (!AllCardsMatched())
            await UniTask.Yield();

        await UniTask.Delay(500);
    }

    private bool AllCardsMatched()
    {
        foreach (var card in _cards)
            if (!card.IsMatched) return false;

        return true;
    }

    public void OnCardClicked(CardController card)
    {
        if (_isInputBlocked || card.IsMatched || card == _selectedCard1) return;

        card.Flip().Forget();

        if (_selectedCard1 == null)
        {
            _selectedCard1 = card;
        }
        else
        {
            _selectedCard2 = card;
            CheckMatch();
        }
    }

    private async void CheckMatch()
    {
        _isInputBlocked = true;

        if (_selectedCard1.CardID == _selectedCard2.CardID)
        {
            _selectedCard1.MarkAsMatched();
            _selectedCard2.MarkAsMatched();
        }
        else
        {
            await UniTask.Delay(1000);
            await UniTask.WhenAll(_selectedCard1.Flip(), _selectedCard2.Flip());
        }

        _selectedCard1 = _selectedCard2 = null;
        _isInputBlocked = false;
    }
}