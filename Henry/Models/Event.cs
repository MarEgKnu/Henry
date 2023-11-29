using System.ComponentModel.DataAnnotations;

namespace Henry.Models
{
    public class Event
    {
        private int _id;
        private string _name;
        private string _description;
        private DateTime _dateTime;

        public int Id { get { return _id; }  set { _id = value; } }
        [Display(Name = "Event Name")]
        [Required(ErrorMessage = "Name of the event is required"), MaxLength(30)]
        public string Name { get { return _name; }  set { _name = value; } }
        public string Description { get { return _description; } set { _description = value; } }
        [Required(ErrorMessage = "Date is required")]
        [Range(typeof(DateTime), "28/11/2023", "12/11/2034")]
        public DateTime DateTime { get { return _dateTime; }  set { _dateTime = value; } }

        public Event()
        {
        }
        public override bool Equals(object? obj)  //needed to decide how to determine if event is equal to other event
        {
            if (obj == null) return false;
            if (!(obj is Event)) return false;
            if (((Event)obj).Id == this.Id)
                return true;
            return false;
        }
    }
}
