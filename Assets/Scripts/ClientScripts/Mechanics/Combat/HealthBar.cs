using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider _Slider;
    [SerializeField] private float _updateSpeedSeconds;
    [SerializeField] private DamageSystem _damageSystem;

    private void Awake()
    {
        _damageSystem.OnHealthChanged += HandleHealthChanged;
    }

    private void HandleHealthChanged(int health, int maxHealth)
    {

        StartCoroutine(ChangeToHealth((float)health / (float)maxHealth));
    }

    private IEnumerator ChangeToHealth(float health)
    {
        if (_Slider != null)
        {
            float preChangePct = _Slider.value;
            float elapsed = 0f;

            while (elapsed < _updateSpeedSeconds)
            {
                elapsed += Time.deltaTime;
                _Slider.value = Mathf.Lerp(preChangePct, health, elapsed / _updateSpeedSeconds);
                yield return null;
            }

            _Slider.value = health;
        }

    }

    private void LateUpdate()
    {
        transform.LookAt(PlayerBehaviour.Instance.PlayerCamera.transform);
        transform.Rotate(0, 180, 0);
    }

}
