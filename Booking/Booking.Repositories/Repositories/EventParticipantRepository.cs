using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Booking.Models;
using Booking.Repositories.Interfaces;

namespace Booking.Repositories.Repositories
{
    public class EventParticipantRepository : IEventParticipantRepository
    {
        private readonly BookingDbContext _context;

        public EventParticipantRepository(BookingDbContext context)
        {
            _context = context;
        }

        public IQueryable GetAllEventParticipants()
        {
            return _context.EventParticipants;
        }

        public EventParticipant GetEventParticipantById(Guid id)
        {
            return _context.EventParticipants.Find(id);
        }

        public void CreateEventParticipant(EventParticipant participant)
        {
            _context.EventParticipants.Add(participant);
        }

        public void DeleteEventParticipant(EventParticipant participant)
        {
            _context.EventParticipants.Remove(participant);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
