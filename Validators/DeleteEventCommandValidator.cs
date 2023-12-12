namespace MicrosoftOutlookIntegration.Validators
{
    public class DeleteEventCommandValidator
    {
        public  DeleteEventCommandValidator(string id)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentNullException();
        }
    }
}