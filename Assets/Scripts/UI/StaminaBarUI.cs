using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBarUI : MonoBehaviour
{
    public RectTransform Icon;
    public Image IconImage;
    
    public float minX;
    public float maxX;

    public Sprite fallingTexture;
    
    private Sprite _defaultTexture;
    private PlayerMov _playerMovement;
    
    private Vector3 _defaultIconScale;
    
    void Start()
    {
        _playerMovement = FindObjectOfType<PlayerMov>();
        _defaultTexture = IconImage.sprite;
        _defaultIconScale = Icon.localScale;
    }
    
    void Update()
    {
        // Get the direction
        int dir = 1;
        if (Mathf.Abs(_playerMovement.staminaBalance - 0.5f) >= 0.2f)
        {
            //IconImage.sprite = fallingTexture;
            dir = _playerMovement.staminaBalance > 0.5f ? 1 : -1;
        }
        else
        {
            //IconImage.sprite = _defaultTexture;
            dir = _playerMovement.staminaBalance > 0.5f ? 1 : -1;
        }
        
        // flip the icon according to the direction
        var localScale = Icon.localScale;
        localScale = new Vector3(_defaultIconScale.x * dir, _defaultIconScale.y, 0.1f);
        Icon.localScale = localScale;

        float staminaBalance = 1 - _playerMovement.staminaBalance;
        float x = Mathf.Lerp(minX, maxX, staminaBalance);
        Icon.localPosition = new Vector2(x, Icon.localPosition.y);
    }
}
