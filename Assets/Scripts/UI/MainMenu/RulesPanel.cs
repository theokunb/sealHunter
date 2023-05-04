using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

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

    private void Start()
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

        foreach(var input in  inputControls)
        {
            var view = Instantiate(_template, _container);
            view.Render(input);
        }
    }
}
