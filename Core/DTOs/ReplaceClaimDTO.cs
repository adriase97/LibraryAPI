namespace Core.DTOs
{
    public class ReplaceClaimDTO
    {
        public string OldType { get; set; } = string.Empty;
        public string OldValue { get; set; } = string.Empty;
        public string NewType { get; set; } = string.Empty;
        public string NewValue { get; set; } = string.Empty;
    }
}
