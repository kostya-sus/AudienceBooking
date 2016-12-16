﻿using System;
using System.Linq;
using Booking.Models;
using Booking.Models.EfModels;

namespace Booking.Repositories.Interfaces
{
    public interface IEventParticipantRepository : IDisposable
    {
        IQueryable GetAllEventParticipants();

        EventParticipant GetEventParticipantById(Guid id);

        void CreateEventParticipant(EventParticipant participant);

        void DeleteEventParticipant(EventParticipant participant);

        void Save();
    }
}