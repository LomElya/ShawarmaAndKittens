using Action = System.Action;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
   public event Action OnInitialized;

   [SerializeField] protected CharacterView _view;

   public void Initialized() => OnInitialized?.Invoke();

   public virtual void OnUpdate() { }
}
