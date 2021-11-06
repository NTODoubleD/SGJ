using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuiratHeroPiece : MonoBehaviour
{
    public float speed;
    
    [SerializeField] private Sprite UpSprite, DownSprite, LeftSprite, RightSprite;
    private GHButtonType _type;
    private Image _image;

    private Dictionary<GHButtonType, Sprite> _typeToSprite;
    

    private void Awake()
    {
        _typeToSprite = new Dictionary<GHButtonType, Sprite>()
        {
            { GHButtonType.Up, UpSprite },
            { GHButtonType.Dowm, DownSprite },
            { GHButtonType.Left, LeftSprite },
            { GHButtonType.Right, RightSprite }

        };
        _image = GetComponent<Image>();
    }

    public void SetType(GHButtonType type)
    {
        _type = type;
        _image.sprite = _typeToSprite[_type];
    }

    public GHButtonType GetType()
    {
        return _type;
    }

    private void Update()
    {
        transform.Translate(Vector2.down * Time.deltaTime * speed);
        if (transform.position.y <= -100)
        {
            GuitarHeroManager.OnLifeLost?.Invoke();
            TryDestroy();
        }
    }

    public void TryDestroy()
    {
        Destroy(gameObject);
    }

    
}
