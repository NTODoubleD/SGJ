using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image _foregroundImage;
    [SerializeField] private float _updateSpeedSeconds;

    private void Awake()
    {
        GetComponentInParent<DamageSystem>().OnHealthChanged += HandleHealthChanged;
    }

    private void HandleHealthChanged(int health, int maxHealth)
    {

        StartCoroutine(ChangeToHealth((float)health / (float)maxHealth));
    }

    private IEnumerator ChangeToHealth(float health)
    {
        float preChangePct = _foregroundImage.fillAmount;
        float elapsed = 0f;

        while (elapsed < _updateSpeedSeconds)
        {
            elapsed += Time.deltaTime;
            _foregroundImage.fillAmount = Mathf.Lerp(preChangePct, health, elapsed / _updateSpeedSeconds);
            yield return null;
        }

        _foregroundImage.fillAmount = health;
    }

    private void LateUpdate()
    {
        transform.LookAt(Camera.main.transform);
        transform.Rotate(0, 180, 0);
    }

}
