public class NormalStackHolder : StackViewBase
{
    protected override void onAdd(StackableType stackable)
    {
        InvokeMoveEnded(stackable);
    }

    protected override void onRemove(StackableType stackable)
    {
    }
}
