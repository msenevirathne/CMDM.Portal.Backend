namespace CMDM.Domain
{
    public class Customer
    {
        public int CustNo { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Source { get; set; }
        public int? ParentCustNo { get; set; }
    }
}
