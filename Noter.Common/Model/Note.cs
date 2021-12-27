namespace Noter.Common.Model
{
    public class Note
    {
        public string Text { get; set; }
        public string Title { get; set; }

        public Note(string? text, string? title)
        {
            Text = text;
            Title = title;
        }
    }
}
