using System;

namespace EdDbLIb
{
    public class Major
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public int MinSAT { get; set; }
        public override string ToString()
        {
            return $" {Id} | {Code} | {Description} | {MinSAT} ";
        }
    }
}
