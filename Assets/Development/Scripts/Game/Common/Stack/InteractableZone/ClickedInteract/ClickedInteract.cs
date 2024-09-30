using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickedInteract : InteractableZoneBase
{
    /// <summary>
    /// Скрипт для взаимодействия с грилем и зоной готовки. 
    /// В зоне готовки должны получить СЫРУЮ шаурму. 
    /// В гриле - ГОТОВУЮ.
    /// 
    /// в ЗОНЕ ГОТОВКИ выбираем рецепт какую шаурму сделать(сырую), запускается таймер
    /// в ГРИЛЕ меню какую шаурму готовить, после выбора, начать ее готовить, начнется миниигра. Удаляется старая, добавляется новая, готовая
    /// </summary>
    /// <param name="enteredInteractable"></param>


    public override void Entered(Interactable enteredInteractable)
    {
        //Событие на прерывание возможно сюда
    }

    public override void Exited(Interactable otherInteractable)
    {
        //ui.Action -= onAction;
        //ui.ExitAction -= onExitUI;
        //ui.Hide();
    }

    public override void Interact()
    {
        //ui.Action += onAction;
        //ui.Show();
    }

    /// <summary>
    /// Событие на начало готовки
    /// </summary>
    private void onAction()
    {
        //ui.Action -= onAction;
        //Готовка
        //??Отключаить управление??
        //ui.ExitAction += onExitUI;
    }

    /// <summary>
    /// Событие на прерывание готовки
    /// </summary>
    private void onExitUI()
    {
        //ui.ExitAction -= onExitUI;
        //Exit(EnteredInteractable);
    }

    /// <summary>
    /// Событие,когда готовка завершилась, добавить в инвентарь
    /// </summary>
    private void AddStack(StackableType type)
    {
        EnteredStack.AddToStack(type);
    }
}
