using AutoMapper;
using Newtonsoft.Json;
using PlagarismChecker.Application.Common.Interfaces;
using PlagarismChecker.Application.Common.Mappings;
using PlagarismChecker.Application.Common.Models;
using PlagarismChecker.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PlagarismChecker.Application.Users.Queries.GetUserSearchHistory
{
    public class GetUserSearchHistoryDto : IMapFrom<List<UserHistory>>
    {
        public string Username { get; set; }

        public List<SearchHistory> SearchHistories { get; set; }

        public void Mapping(Profile profile) =>
            profile.CreateMap<List<UserHistory>, GetUserSearchHistoryDto>()
            .ForMember(d => d.Username, opt => opt.MapFrom(s => s.FirstOrDefault().Username))
            .ForMember(d => d.SearchHistories, opt => opt.MapFrom(s => MapSearchHistories(s)));

        private List<SearchHistory> MapSearchHistories(List<UserHistory> userHistories)
        {
            List<SearchHistory> searchHistories = new List<SearchHistory>();

            foreach (var userHistory in userHistories)
            {
                searchHistories.Add(new SearchHistory
                {
                    Request = JsonConvert.DeserializeObject<Request>(userHistory.Request),
                    Result = JsonConvert.DeserializeObject<Result>(userHistory.Result),
                    RequestDate = DateTime.Now
                });
            }

            return searchHistories;
        }
    }
}
