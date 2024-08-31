namespace Global.Motorcycle.View.Domain.Contracts.Validation
{
    public interface IValidableEntity
    {
        ISet<string> Errors { get; }
        bool Validate();
    }
}
