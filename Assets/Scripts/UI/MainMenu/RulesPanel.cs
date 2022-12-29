using UnityEngine;
using UnityEngine.UI;

public class RulesPanel : MonoBehaviour
{
    [SerializeField] private Button _closeButton;
    [SerializeField] private Button _nextButton;
    [SerializeField] private InputInfoView _template;
    [SerializeField] private Transform _container;

    private PlayerInput _playerInput;
    private InputMap _inputMap;

    private void OnEnable()
    {
        _closeButton.Select();
        _closeButton.onClick.AddListener(OnCloseSelected);
    }

    private void OnDisable()
    {
        _closeButton.onClick.RemoveListener(OnCloseSelected);
    }

    private void Start()
    {
        _playerInput = new PlayerInput();
        _inputMap = new InputMap();

        _inputMap.ParseActions(_playerInput.bindings);
        FillData(_inputMap);
    }

    private void FillData(InputMap inputMap)
    {
        foreach (var inputInfo in inputMap.GetInputInfo())
        {
            var inputInfoView = Instantiate(_template, _container);
            inputInfoView.Render(inputInfo);
        }
    }

    private void OnCloseSelected()
    {
        _nextButton.Select();
        gameObject.SetActive(false);
    }
}
