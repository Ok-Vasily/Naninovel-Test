using Naninovel;
using Naninovel.UI;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardGameUI : CustomUI
{
    [SerializeField] private GridLayoutGroup _grid;
    [SerializeField] private CardController _cardPrefab;

    private List<Color> _frontColors = new() { Color.blue, Color.green, Color.magenta, Color.red, Color.yellow };

    public override UniTask InitializeAsync()
    {
        Hide();
        return base.InitializeAsync();
    }

    public List<CardController> InitializeGrid(List<int> cardIds)
    {
        var cards = new List<CardController>();

        foreach (int id in cardIds)
        {
            CardController card = Instantiate(_cardPrefab, _grid.transform);

            while (id >= _frontColors.Count)
                _frontColors.Add(Random.ColorHSV(0, 0.9f, 0, 0.9f, 0, 0.9f, 1, 1));

            card.Initialize(id, _frontColors[id]);
            cards.Add(card);
        }

        return cards;
    }
}
