using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class RulesPanel : MonoBehaviour
{
    private const string LocalizationTableName = "UI";
    private const string FireKey = "Shoot";
    private const string SwitchKey = "Switch";
    private const string BuyKey = "Buy";
    private const string ReloadKey = "Reload";
    private const string PauseKey = "Pause";


    [SerializeField] private InputInfoView _template;
    [SerializeField] private Transform _container;

    private PlayerInput _playerInput;
    private InputMap _inputMap;
    private List<InputInfoView> _viewElements = new List<InputInfoView>();

    private void OnEnable()
    {
        _playerInput = new PlayerInput();
        _inputMap = new InputMap();

        _inputMap.ParseActions(_playerInput.bindings);
        FillData(_inputMap);
    }

    private void FillData(InputMap inputMap)
    {
        var inputControls = inputMap.GetInputInfo(new List<string>()
        {
            FireKey,
            SwitchKey,
            BuyKey,
            ReloadKey,
            PauseKey
        }).Select(input => new InputInfo(LocalizationSettings.StringDatabase.GetLocalizedString(LocalizationTableName, input.Label), input.KeyboardLabel))
        .ToList();

        if(_viewElements.Count == 0)
        {
            foreach (var input in inputControls)
            {
                var view = Instantiate(_template, _container);
                view.Render(input);

                _viewElements.Add(view);
            }
        }
        else
        {
            foreach (var element in _viewElements)
            {
                element.Render(inputControls.Find(x => x.KeyboardLabel == element.Key));
            }
        }

        
    }
}
