using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.Windows;

public class PipesManager : MonoBehaviour
{
    [SerializeField] private Level _level;
    [SerializeField] private PipeEntryDirection _entryDirection;
    private Coroutine _coroutine;

    private bool _isCompleteTimeDelay = false;

    private void Update()
    {
        if (!_isCompleteTimeDelay) return;

        Vector2 moveInput = InputManager.InputActions.Player.Move.ReadValue<Vector2>();
        if (IsEntryPipe(moveInput))
        {
            LevelManager.LoadLevel(_level);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (TagsUtils.IsPlayer(collision.gameObject)) return;
        _coroutine = StartCoroutine(DelayTimer());
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (TagsUtils.IsPlayer(collision.gameObject)) return;
        StopCoroutine(_coroutine);
        _isCompleteTimeDelay = false;
    }

    private bool IsEntryPipe(Vector2 moveInput)
    {
        return _entryDirection switch
        {
            PipeEntryDirection.Right => moveInput.x > 0.5f,
            PipeEntryDirection.Left => moveInput.x < -0.5f,
            PipeEntryDirection.Down => moveInput.y < -0.5f,
            _ => throw new System.NotImplementedException(),
        };
    }

    public IEnumerator DelayTimer()
    {
        yield return new WaitForSeconds(1);
        _isCompleteTimeDelay = true;
    }
}
