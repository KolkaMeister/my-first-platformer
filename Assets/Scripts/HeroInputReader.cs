using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HeroInputReader : MonoBehaviour
{
    private GeneralUIController UIController;
    private Hero hero;
    void Awake()
    {
        hero = GetComponent<Hero>();
        UIController = FindObjectOfType<GeneralUIController>();
    }
    public void SayKeyWord(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            hero.KeyWord();
        }
    }
    public void OnMovement(InputAction.CallbackContext context)
    {
        var direction = context.ReadValue<Vector2>();
        hero.SetDirection(direction);
    }
    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            hero.Interact();
        }
    }
    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            hero.Attack();
        }
    }
    public void OnThrow(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            hero.ThrowStarted();
        }
        if (context.canceled)
        {
            hero.ThrowComplete();
        }
    }
    public void OnHeal(InputAction.CallbackContext context)
    {
        if (context.canceled)
        {
            hero.Heal();
        }
    }
    public void OnHook(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            hero.hookPressed = true;
        }
        if (context.canceled)
        {
            hero.hookPressed = false;
        }
    }
    public void NextSelection(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            hero.ChangeSelection();
        }
    }
    public void DivineShield(InputAction.CallbackContext context)
    {
        if (context.canceled)
        {
            hero.abilityManager.Get("divine_protection").Use();
        }
    }
    public void Teleport(InputAction.CallbackContext context)
    {
        if (context.canceled)
        {
            hero.abilityManager.Get("teleport").Use();
        }
    }
    public void UseCnadle(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            hero.UseCandle();
        }
    }
    public void OpenPerksWindow(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            UIController.OpenPerksWindow();
        }
    }
    public void OpenStatsWindow(InputAction.CallbackContext context)
    {

        if (context.performed)
        {
            UIController.OpenStatsWindow();
        }

    }

}
