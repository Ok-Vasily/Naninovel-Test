using UnityEngine;
using Naninovel;
using Naninovel.Commands;
using System.Collections.Generic;
using System.Linq;

public class ItemClickHandler : MonoBehaviour, DestroySpawned.IParameterized, DestroySpawned.IAwaitable
{
    protected float FadeOutTime { get; private set; }

    [SerializeField] private string _itemName;
    [SerializeField] private string _variableName;
    [SerializeField] private float _defaultFadeOutTime = 0.35f;

    private readonly Tweener<FloatTween> _opacityTweener = new();
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.SetOpacity(1);
    }

    private void OnMouseDown() => HandleClick();

    private void HandleClick()
    {
        var variableManager = Engine.GetService<ICustomVariableManager>();
        variableManager.SetVariableValue(_variableName, "true");
    }

    public void SetDestroyParameters(IReadOnlyList<string> parameters)
    {
        FadeOutTime = Mathf.Abs(parameters?.ElementAtOrDefault(0)?.AsInvariantFloat() ?? _defaultFadeOutTime);
    }

    public async UniTask AwaitDestroyAsync(AsyncToken asyncToken = default)
    {
        if (_opacityTweener.Running)
            _opacityTweener.CompleteInstantly();

        var time = asyncToken.Completed ? 0 : FadeOutTime;
        var tween = new FloatTween(1, 0, time, _spriteRenderer.SetOpacity);
        await _opacityTweener.RunAsync(tween, asyncToken, _spriteRenderer);
    }
}