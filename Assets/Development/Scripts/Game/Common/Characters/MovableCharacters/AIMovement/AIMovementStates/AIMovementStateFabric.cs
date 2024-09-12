using UnityEngine;
using Zenject;

public class AIMovementStateFabric
{
    [Inject]
    private DiContainer _container;

    public AIMovementState CreateState(AIMovementStateType stateType, AIMovementStateType targetType, AIMovement parent)
    {
        AIMovementState state;

        switch (stateType)
        {
            //Заходит в кафе и ждет очередь на кассе
            case AIMovementStateType.JoinCafe:
                state = new JoinCafeAIMovableState(stateType, parent);
                break;
            //Ждет когда его обслужат на кассе
            case AIMovementStateType.WaitTakingOrder:
                state = new WaitTakingOrderState(stateType, parent);
                break;

            //Ждет очереди на выдаче
            case AIMovementStateType.JoinDeliveryTable:
                state = new JoinDeliveryTableState(stateType, parent);
                break;
            //Ждет когда его обслужат на выдаче заказа
            case AIMovementStateType.WaitReceiptOrder:
                state = new WaitReceiptOrderState(stateType, parent);
                break;

            //Уходит из кафе
            case AIMovementStateType.LeaveCafe:
                state = new LeaveState(stateType, parent);
                break;

            default:
                Debug.LogErrorFormat("Нет switch-case для типа \"{0}\" ", stateType);
                state = new LeaveState(stateType, parent);
                break;
        }

        Transition transition = state.GetTransition(targetType);

        _container.Inject(state);
        _container.Inject(transition);

        state.SetTransition(transition);

        return state;
    }
}
