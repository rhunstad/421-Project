﻿using System;
using System.Collections.Generic;
using System.Text;
using Uplift.DataAccess.Data.Repository.IRepository;

namespace Uplift.DataAccess.Data.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        IItemRepository Item { get; }

        void Save();
    }
}
