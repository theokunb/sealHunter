using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Blood : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void Trigger(BloodQuality bloodQuality)
    {
        switch (bloodQuality)
        {
            case BloodQuality.Low:
                _animator.SetTrigger(Constants.BloodAnimatorController.Low);
                break;
            case BloodQuality.Normal:
                _animator.SetTrigger(Constants.BloodAnimatorController.Normal);
                break;
            case BloodQuality.High:
                _animator.SetTrigger(Constants.BloodAnimatorController.High);
                break;
        }
    }
}

[System.Serializable]
public enum BloodQuality
{
    Low,
    Normal,
    High,
}
