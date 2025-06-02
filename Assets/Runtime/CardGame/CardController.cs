using Naninovel;
using UnityEngine;
using UnityEngine.UI;

public class CardController : MonoBehaviour
{
    [SerializeField] private Image _frontImage;
    [SerializeField] private Image _backImage;
    [SerializeField] private Button _button;

    public int CardID { get; private set; }
    public bool IsMatched { get; private set; }

    private CardGameManager _gameService;

    public void Initialize(int id, Color color)
    {
        CardID = id;
        _frontImage.color = color;
        _frontImage.enabled = false;
        _backImage.enabled = true;
        _gameService = Engine.GetService<CardGameManager>();
        _button.onClick.AddListener(OnClick);
    }
    public void MarkAsMatched()
    {
        IsMatched = true;
        _button.interactable = false;
    }

    public async UniTask Flip()
    {
        await ScaleShrink();
        _backImage.enabled = !_backImage.enabled;
        _frontImage.enabled = !_frontImage.enabled;
        await ScaleExpand();
    }

    private async UniTask ScaleShrink()
    {
        var scale = Vector2.one;

        while (transform.localScale.x > 0)
        {
            scale.x -= 0.1f;
            transform.localScale = scale;
            await UniTask.Yield();
        }

        scale.x = 0;
        transform.localScale = scale;
    }

    private async UniTask ScaleExpand()
    {
        var scale = new Vector2(0, 1);

        while (transform.localScale.x < 1)
        {
            scale.x += 0.1f;
            transform.localScale = scale;
            await UniTask.Yield();
        }

        scale.x = 1;
        transform.localScale = scale;
    }

    private void OnClick() => _gameService.OnCardClicked(this);
}