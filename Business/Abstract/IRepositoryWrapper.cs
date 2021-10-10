﻿using DataAccess.Abstract;

namespace Business.Abstract
{
    public interface IRepositoryWrapper
    {
        IProductRepository Product { get; }
        ICategoryRepository Category { get; }

        void Save();
    }
}
