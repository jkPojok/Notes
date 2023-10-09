namespace Application.Dto
{
    public class NoteDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string? Title { get; set; }
        public string Text { get; set; } = string.Empty;
        public string? DateOfCreation { get; set; }
        public string? DateOfEditing { get; set; }
    }
}
