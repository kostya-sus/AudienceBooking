using System;
using System.Collections.Generic;

namespace Booking.Models.Helpers
{
    internal static class RandomChoiceExtension
    {
        public static T Choice<T>(this Random random, IList<T> collection)
        {
            return collection[random.Next(collection.Count)];
        }
    }
}