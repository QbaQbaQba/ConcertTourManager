using System;
using System.Collections.Generic;
using System.Text;
using ConcertTourManager.App.Common;
using ConcertTourManager.Domain.Entity;

namespace ConcertTourManager.App.Concrete
{
    public class ConcertService : BaseService<Concert>
    {
        public ConcertService() : base()
        {

        }
        public ConcertService(Tour tour)
        {
            Items = tour.Concerts;
        }

    }
}
