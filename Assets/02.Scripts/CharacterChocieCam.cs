using UnityEngine;
using TMPro;
public class CharacterChoiceCam : MonoBehaviour
{
    public enum CharacterClass
    {
        Warrior,
        Thief,
        Wizard
    }
    public CharacterClass currentCharacter = CharacterClass.Warrior;
    public float moveDistance = 10.0f;
    public TextMeshProUGUI ClassText;
    public void MoveCameraLeft()
    {
        if (currentCharacter > CharacterClass.Warrior)
        {
            currentCharacter = (CharacterClass)((int)currentCharacter - 1);
            MoveCamera(-moveDistance);
        }
    }

    public void MoveCameraRight()
    {
        if (currentCharacter < CharacterClass.Wizard) 
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
            case CharacterClass.Warrior:
                ClassText.text = "전사";

                break;
            case CharacterClass.Thief:
                ClassText.text = "도적";

                break;
            case CharacterClass.Wizard:
                ClassText.text = "마법사";

                break;
        }
    }
}