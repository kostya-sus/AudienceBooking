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
        }

        public void CloseAudience(AudiencesEnum audienceId)
        {
            var currentAudience = _unitOfWork.AudienceRepository.GetAudienceById(audienceId);
            if (currentAudience != null)
            {
                currentAudience.IsBookingAvailable = false;
                _unitOfWork.AudienceRepository.UpdateAudience(currentAudience);
            }
        }

        public void OpenAudience(AudiencesEnum audienceId)
        {
            var currentAudience = _unitOfWork.AudienceRepository.GetAudienceById(audienceId);
            if (currentAudience != null)
            {
                currentAudience.IsBookingAvailable = true;
                _unitOfWork.AudienceRepository.UpdateAudience(currentAudience);
            }
        }

        public bool IsFree(AudiencesEnum audienceId, DateTime dateTime, int duration)
        {
            var events = _unitOfWork.EventRepository.GetAllEvents();
            events = events.Where(x => x.AudienceId == audienceId);

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
            var availableAudiences = _unitOfWork.AudienceRepository.GetAllAudiences().Where(x => x.IsBookingAvailable);
            return availableAudiences.Select(a => a.Id);
        }

        public IDictionary<AudiencesEnum, string> GetAllAudiencesNames()
        {
            var audiences = _unitOfWork.AudienceRepository.GetAllAudiences();

            Dictionary<AudiencesEnum, string> names = new Dictionary<AudiencesEnum, string>();
            foreach (var item in audiences)
            {
                names.Add(item.Id, item.Name);
            }
            return names;
        }

        public IDictionary<AudiencesEnum, string> GetAvailableAudiencesNames()
        {
            Dictionary<AudiencesEnum, string> names = new Dictionary<AudiencesEnum, string>();

            var audiences = _unitOfWork.AudienceRepository.GetAllAudiences();
            audiences = audiences.Where(x => x.IsBookingAvailable);

            foreach (var item in audiences)
            {
                names.Add(item.Id, item.Name);
            }
            return names;
        }
    }
}
