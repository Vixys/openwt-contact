﻿using OpenWT.Contact.Application.Data;

namespace OpenWT.Contact.Application.Contract
{
    public interface IServiceBase<TDto, in TId>
        where TDto : IDto<TId> 
        where TId : struct
    {
        TDto Create(TDto contactCreation);
        TDto GetById(TId contactId);
        void DeleteById(TId contactId);
        TDto Update(TId contactId, TDto contact);
    }
}