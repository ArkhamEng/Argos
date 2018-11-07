namespace Argos.Models.BaseTypes
{
    public class JResponse
    {
        public int Id { get; set; }

        public string Result { get; set; }

        public string Header { get; set; }

        public string Body { get; set; }

        public string Extra { get; set; }

        public int Code { get; set; }
    }
}