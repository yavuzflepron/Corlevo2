namespace DataLayer.CQRS.Queries.Response
{
    public class GetProductByIdQueryResponse
    {
        public Guid ProductID { get; set; }
        public string? Name { get; set; }
        public double Price { get; set; }
    }
}