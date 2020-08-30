namespace OpenWT.Contact.Data.Contract
{
    public interface IEntity<TId>
    {
        public TId Id { get; set; }
    }
}