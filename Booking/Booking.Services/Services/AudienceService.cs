using System;
using System.Collections.Generic;
using System.Linq;
using Booking.Enums;
using Booking.Models;
using Booking.Repositories.Interfaces;
using Booking.Services.Interfaces;

namespace Booking.Services.Services
{
    public class AudienceService : IAudienceService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AudienceService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Audience GetAudience(AudiencesEnum audienceId)
        {
            return _unitOfWork.AudienceRepository.GetAudienceById(audienceId);
        }

        public void UpdateAudience(Audience audience)
        {
            _unitOfWork.AudienceRepository.UpdateAudience(audience);
            _unitOfWork.Save();
        }

        public void CloseAudience(AudiencesEnum audienceId)
        {
            var currentAudience = _unitOfWork.AudienceRepository.GetAudienceById(audienceId);
            if (currentAudience != null)
            {
                currentAudience.IsBookingAvailable = false;
                _unitOfWork.AudienceRepository.UpdateAudience(currentAudience);
                _unitOfWork.Save();
            }
        }

        public void OpenAudience(AudiencesEnum audienceId)
        {
            var currentAudience = _unitOfWork.AudienceRepository.GetAudienceById(audienceId);
            if (currentAudience != null)
            {
                currentAudience.IsBookingAvailable = true;
                _unitOfWork.AudienceRepository.UpdateAudience(currentAudience);
                _unitOfWork.Save();
            }
        }

        public bool IsFree(AudiencesEnum audienceId, DateTime dateTime, int duration, Guid? currentEventId)
        {
            var events =
                _unitOfWork.EventRepository.GetAllEvents()
                    .Where(x => x.AudienceId == audienceId && x.Id != currentEventId.Value);

            var endOfEvent = dateTime.AddMinutes(duration);

            if (dateTime.DayOfWeek != 0 && (int) dateTime.DayOfWeek != 6)
            {
                if ((endOfEvent.Hour < (int) BookingHoursBoundsEnum.Upper ||
                     (endOfEvent.Hour == (int) BookingHoursBoundsEnum.Upper & endOfEvent.Minute == 0))
                    && dateTime.Hour >= (int) BookingHoursBoundsEnum.Lower)
                {
                    foreach (var currentEvent in events)
                    {
                        var endOfCurrentEvent = currentEvent.EventDateTime.AddMinutes(currentEvent.Duration);
                        if (dateTime <= currentEvent.EventDateTime && currentEvent.EventDateTime < endOfEvent)
                        {
                            return false;
                        }
                        if (dateTime > currentEvent.EventDateTime && dateTime < endOfCurrentEvent)
                        {
                            return false;
                        }
                    }
                    return true;
                }
            }
            return false;
        }

        public IEnumerable<Audience> GetAllAudiences()
        {
            return _unitOfWork.AudienceRepository.GetAllAudiences();
        }

        public IEnumerable<Audience> GetAvailableAudiences()
        {
            return _unitOfWork.AudienceRepository.GetAllAudiences().Where(x => x.IsBookingAvailable);
        }
    }
}