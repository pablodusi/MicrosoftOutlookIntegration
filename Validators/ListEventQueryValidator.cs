namespace MicrosoftOutlookIntegration.Validators
{
    public class ListEventQueryValidator
    {
        public  ListEventQueryValidator(MicrosoftOutlookIntegrationApp.Entities.Event evento)
        {
            if (evento == null) throw new ArgumentNullException();
            if(string.IsNullOrEmpty(evento.Notas)) throw new ArgumentNullException();
            if(evento.InitialHour == null) throw new ArgumentNullException();
            if (evento.EndHour == null) throw new ArgumentNullException();
            if ( string.IsNullOrEmpty(evento.UserId)) throw new ArgumentNullException();
            if (string.IsNullOrEmpty(evento.Subject)) throw new ArgumentNullException();
            if (string.IsNullOrEmpty(evento.Body)) throw new ArgumentNullException();
            if (string.IsNullOrEmpty(evento.EventId)) throw new ArgumentNullException();
            if (string.IsNullOrEmpty(evento.EventIdCalendar)) throw new ArgumentNullException();
            if (string.IsNullOrEmpty(evento.Location)) throw new ArgumentNullException();
        }
    }
}