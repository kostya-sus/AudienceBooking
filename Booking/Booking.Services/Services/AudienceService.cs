using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public bool IsFree(AudiencesEnum audienceId, DateTime dateTime, int duration)
        {
            var events = _unitOfWork.EventRepository.GetAllEvents().Where(x => x.AudienceId == audienceId);
            var endOfEvent = dateTime.AddMinutes(duration);

            if ((int)dateTime.DayOfWeek != 0 && (int)dateTime.DayOfWeek != 6)  //check for weekend
            {
                if ((endOfEvent.Hour < (int)BookingHoursBoundsEnum.Upper || (endOfEvent.Hour == (int)BookingHoursBoundsEnum.Upper & endOfEvent.Minute == 0))
                     && dateTime.Hour >= (int)BookingHoursBoundsEnum.Lower)
                {
                    foreach (var currentEvent in events)
                    {
                        var endOfCurrentEvent = currentEvent.EventDateTime.AddMinutes(currentEvent.Duration);
                        if ((currentEvent.EventDateTime < endOfEvent) && endOfCurrentEvent <= endOfEvent)
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

        public IEnumerable<AudiencesEnum> GetAvailableAudiencesIds()
        {
            var availableAudiences = _unitOfWork.AudienceRepository.GetAllAudiences().Where(x => x.IsBookingAvailable);
            return availableAudiences.Select(a => a.Id);
        }

        public IDictionary<AudiencesEnum, string> GetAllAudiencesNames()
        {
            var audiences = _unitOfWork.AudienceRepository.GetAllAudiences();
            return GetAudiencesNamesFromQuery(audiences);
        }

        public IDictionary<AudiencesEnum, string> GetAvailableAudiencesNames()
        {
            var audiences = _unitOfWork.AudienceRepository.GetAllAudiences().Where(x=>x.IsBookingAvailable);
            return GetAudiencesNamesFromQuery(audiences);
        }

        private IDictionary<AudiencesEnum, string> GetAudiencesNamesFromQuery(IQueryable<Audience> query)
        {
            Dictionary<AudiencesEnum, string> names = new Dictionary<AudiencesEnum, string>();
            foreach (var item in query)
            {
                names[item.Id] = item.Name;
            }
            return names;
        }
    }
}
