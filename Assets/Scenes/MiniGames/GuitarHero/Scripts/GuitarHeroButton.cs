using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    
    private List<GuiratHeroPiece> _pieces = new List<GuiratHeroPiece>();


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
    }

}
