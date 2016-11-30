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

        public Audience GetAudience(int audienceId)
        {
            return _unitOfWork.AudienceRepository.GetAudienceById((AudiencesEnum)audienceId);
        }

        public void UpdateAudience(Audience audience)
        {
            _unitOfWork.AudienceRepository.UpdateAudience(audience);
        }

        public void CloseAudience(int audienceId)
        {
            var currentAudience = _unitOfWork.AudienceRepository.GetAudienceById((AudiencesEnum)audienceId);
            if (currentAudience != null)
            {
                currentAudience.IsBookingAvailable = false;
                _unitOfWork.AudienceRepository.UpdateAudience(currentAudience);

            }
        }

        public void OpenAudience(int audienceId)
        {
            var currentAudience = _unitOfWork.AudienceRepository.GetAudienceById((AudiencesEnum)audienceId);
            if (currentAudience != null)
            {
                currentAudience.IsBookingAvailable = true;
                _unitOfWork.AudienceRepository.UpdateAudience(currentAudience);
            }
        }

        public bool IsFree(int audienceId, DateTime dateTime, int duration)
        {
            var events = _unitOfWork.EventRepository.GetAllEvents();
            events = events.Where(x => x.AudienceId == (AudiencesEnum) audienceId);

            var endOfEvent = dateTime.AddMinutes(duration);

            if (endOfEvent.Hour < (int) BookingHoursBoundsEnum.Upper && (int)dateTime.DayOfWeek != 0 && (int) dateTime.DayOfWeek != 6) //check for weekend
            {
                foreach (var _event in events)
                {
                    var endOfCurrentEvent = _event.EventDateTime.AddMinutes(_event.Duration);
                    if (_event.EventDateTime >= dateTime && endOfCurrentEvent <= endOfEvent && dateTime.Hour >= (int) BookingHoursBoundsEnum.Lower)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public IEnumerable<Audience> GetAllAudiences()
        {
            return _unitOfWork.AudienceRepository.GetAllAudiences();
        }

        public IEnumerable<AudiencesEnum> GetAvailableAudiencesIds()
        {
            var aud = _unitOfWork.AudienceRepository.GetAllAudiences();
            aud = aud.Where(x => x.IsBookingAvailable);

            List<AudiencesEnum> avialableAudiencesIds = new List<AudiencesEnum>();
            foreach (var item in aud)
            {
                avialableAudiencesIds.Add(item.Id);
            }
            return avialableAudiencesIds;
        }

        public IDictionary<AudiencesEnum, string> GetAllAudiencesNames()
        {
            var auds = _unitOfWork.AudienceRepository.GetAllAudiences();

            Dictionary<AudiencesEnum, string> names = new Dictionary<AudiencesEnum, string>();
            foreach (var item in auds)
            {
                names.Add(item.Id, item.Name);
            }
            return names;
        }

        public IDictionary<AudiencesEnum, string> GetAvailableAudiencesNames()
        {
            Dictionary<AudiencesEnum, string> names = new Dictionary<AudiencesEnum, string>();

            var auds = _unitOfWork.AudienceRepository.GetAllAudiences();
            auds = auds.Where(x => x.IsBookingAvailable);

            foreach (var item in auds)
            {
                names.Add(item.Id, item.Name);
            }
            return names;
        }
    }
}
