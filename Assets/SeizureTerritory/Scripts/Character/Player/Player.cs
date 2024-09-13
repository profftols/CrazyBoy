using UnityEngine;

public class Player : Character
{
    [SerializeField] private MonoBehaviour _inputSourceBehaviour;

    private ICharacterInputSource _inputSource;
    
    private void Awake()
    {
        _inputSource = (ICharacterInputSource)_inputSourceBehaviour;
        Render = GetComponent<Renderer>();
    }

    private void Update()
    {
        var movement = new Vector3(_inputSource.MovementInput.x, 0f, _inputSource.MovementInput.y);
        movement *= Speed + bonusSpeed;
        ControllerCharacter.SimpleMove(movement);
    }
}