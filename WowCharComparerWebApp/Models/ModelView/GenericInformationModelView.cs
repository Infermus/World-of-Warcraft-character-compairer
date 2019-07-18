namespace WowCharComparerWebApp.Models.ModelView
{
    public class GenericInformationModelView
    {
        public string Title { get; private set; }

        public string Message { get; private set; }

        public GenericInformationModelView(string title, string message)
        {
            Title = title;
            Message = message;
        }
    }
}
