namespace CMDM.Manager.DTOs
{
    public class MergeCustomersDto
    {
        public int ParentCustNo { get; set; }
        public List<int> ChildCustNos { get; set; }
        //public Customer UpdatedParentDetails { get; set; }
    }
}
