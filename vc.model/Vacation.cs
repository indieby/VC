using System;

namespace vc.model
{
    public class Vacation
    {
        public int Id { get; set; }
        public virtual Employee Employee { get; set; }
        public int EmployeeId { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
    }
}
