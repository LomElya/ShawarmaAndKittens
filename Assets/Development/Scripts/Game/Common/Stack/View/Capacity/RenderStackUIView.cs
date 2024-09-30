public abstract class RenderStackUIView : StackUIView
{
    protected override void OnInit(StackStorage stack, IStackableContainer stackableContainer) => 
        Render(_stack.Count, _stack.Capacity, _stackableContainer.FindTopPositionY());

    protected abstract void Render(int currentCount, int capacity, float topPositionY);

    protected override void OnAdded(StackableType stackable) => Render(_stack.Count, _stack.Capacity, _stackableContainer.FindTopPositionY());
    protected override void OnRemoved(StackableType stackable) => Render(_stack.Count, _stack.Capacity, _stackableContainer.FindTopPositionY());
    protected override void OnCapacityChanged() => Render(_stack.Count, _stack.Capacity, _stackableContainer.FindTopPositionY());
}
