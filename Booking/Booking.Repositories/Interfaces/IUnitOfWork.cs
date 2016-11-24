using System;

namespace Booking.Repositories.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IAudienceRepository AudienceRepository { get; }

        IEventRepository EventRepository { get; }

        IEventParticipantRepository EventParticipantRepository { get; }

        void Save();
    }
}