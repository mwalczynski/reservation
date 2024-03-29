﻿namespace Reservation.Shared.Infrastructure
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    public interface IUnitOfWork
    {
        Task<int> CommitAsync(CancellationToken cancellationToken = default, Guid? internalCommandId = null);
    }
}