/***********************************************************************
 * Module:  AppointmentRepository.cs
 * Purpose: Definition of the Class Repository.AppointmentRepository
 ***********************************************************************/

using Model.Communication;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Repository.CommunicationRepositories
{
    public class SoftwareRatingRepository : ISoftwareRatingRepository
    {
        public JSONStream<SoftwareRating> _stream;

        public SoftwareRatingRepository(JSONStream<SoftwareRating> stream)
        {
            _stream = stream;
        }

        public SoftwareRating Create(SoftwareRating entity)
        {
            List<SoftwareRating> softwareRatingList = _stream.GetAll().ToList();
            softwareRatingList.Add(entity);
            _stream.SaveAll(softwareRatingList);
            return entity;
        }
    }
}