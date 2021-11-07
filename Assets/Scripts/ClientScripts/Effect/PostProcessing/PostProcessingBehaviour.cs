using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;
using UnityEngine.Rendering;

public class PostProcessingBehaviour : MonoBehaviour
{
    [SerializeField] private Volume _volume;
    private Vignette _vignette;
    private ChromaticAberration _chromaticAberration;

    private bool _fillRed = false;
    private float _intense = 0;
    private bool _direction;

    public static PostProcessingBehaviour Instance;
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        if (_volume.profile.TryGet<Vignette>(out var vignette))
            _vignette = vignette;
        if (_volume.profile.TryGet<ChromaticAberration>(out var chromaticAberration))
            _chromaticAberration = chromaticAberration;
    }

    public void FillRedVignette()
    {
        _fillRed = true;
        _direction = false;
    }

    private void FixedUpdate()
    {
        if (_fillRed)
        {
            if (_direction is false)
            {
                _intense += Time.fixedDeltaTime * 3.5f;
                if (_intense >= 0.95f)
                {

                    _direction = true;
                }
            }
            else
            {
                _intense -= Time.fixedDeltaTime * 0.5f;
                if (_intense <= 0f)
                {
                    _direction = false;
                    _fillRed = false;
                }
            }

            _vignette.color.value = new Color(_intense, 0f, 0f);
            _vignette.intensity.value = _intense/2f;
            _chromaticAberration.intensity.value = _intense;


        }
    }
}
