using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GHButtonType
{
    Up,
    Dowm,
    Left,
    Right
}


[RequireComponent(typeof(Collider2D))]
public class GuitarHeroButton : MonoBehaviour
{
    [SerializeField] private GHButtonType _type;
    [SerializeField] private Sprite normal, color;
    private Image image;

    private List<GuiratHeroPiece> _pieces = new List<GuiratHeroPiece>();

    private void Awake()
    {
        image = GetComponent<Image>();
        normal = image.sprite;
    }

    private void Update()
    {
        bool a = false;
        switch (_type)
        {
            case GHButtonType.Up:
                a = Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.UpArrow);
                break;
            case GHButtonType.Dowm:
                a = Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow);
                break;
            case GHButtonType.Left:
                a = Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.LeftArrow);
                break;
            case GHButtonType.Right:
                a = Input.GetKey(KeyCode.F) || Input.GetKey(KeyCode.RightArrow);
                break;
        }
        if (a && image.sprite == normal)
            image.sprite = color;
        else if(!a && image.sprite != normal) 
            image.sprite = normal;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _pieces.Add(collision.GetComponent<GuiratHeroPiece>());
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _pieces.Remove(collision.GetComponent<GuiratHeroPiece>());
    }

    public void TryDestroyCurrentObject()
    {
        if (_pieces.Count > 0)
            _pieces[0].TryDestroy();
        else
            GuitarHeroManager.OnLifeLost.Invoke();

    }

}
