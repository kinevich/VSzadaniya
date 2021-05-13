using LibraryManagement.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagement.Services
{
    public interface IStatisticsService
    {
        public IEnumerable<LibraryAmountDistrictVM> GetLibraryAmountDistrict();

        public IEnumerable<AuthorAmountGenreVM> GetAuthorAmountGenre();

        public IEnumerable<TopAuthorLibraryVM> GetTopAuthorLibrary();
    }
}
