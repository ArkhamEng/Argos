namespace Argos.Models.Interfaces
{
    public interface ISelectable
    {
        string Value { get; }
        
        string Text { get; }
    }


    public class Selectable : ISelectable
    {
        public string Value { get; set; }

        public string Text { get; set; }
    }
}