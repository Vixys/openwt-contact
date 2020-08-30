using System;

namespace OpenWT.Contact.Application.Data
{
    public interface IDto<TId>
        where TId : struct
    {
        TId? Id { get; set; }
    }
}