using UnityEngine;
using TMPro;
public class CharacterChoiceCam : MonoBehaviour
{
    public enum CharacterClass
    {
        Fire,
        Water,
        Dark,
        Light
    }
    public CharacterClass currentCharacter = CharacterClass.Fire;
    public float moveDistance = 10.0f;
    public TextMeshProUGUI ClassText;
    public void MoveCameraLeft()
    {
        if (currentCharacter > CharacterClass.Fire)
        {
            currentCharacter = (CharacterClass)((int)currentCharacter - 1);
            MoveCamera(-moveDistance);
        }
    }

    public void MoveCameraRight()
    {
        if (currentCharacter < CharacterClass.Light) 
        {
            currentCharacter = (CharacterClass)((int)currentCharacter + 1);
            MoveCamera(moveDistance);
        }
    }

    private void MoveCamera(float distance)
    {
        transform.position = new Vector3(transform.position.x + distance, transform.position.y, transform.position.z);
        ClassNameChange();
    }
    private void ClassNameChange()
    {
        switch (currentCharacter)
        {
            case CharacterClass.Fire:
                ClassText.text = "적마법사";

                break;
            case CharacterClass.Water:
                ClassText.text = "청마법사";

                break;
            case CharacterClass.Dark:
                ClassText.text = "흑마법사";

                break;
            case CharacterClass.Light:
                ClassText.text = "백마법사";

                break;
        }
    }
}