namespace Application.Common
{
    public class ValidationResult
    {
        public bool IsSuccess => !ValidationItems.Any();
        public List<string> ValidationItems { get; set; } = new();
    }

}