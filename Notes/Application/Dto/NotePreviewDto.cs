namespace Application.Dto
{
    public class NotePreviewDto
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public string Text { get; set; } = string.Empty;
    }
}
