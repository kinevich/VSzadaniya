using LibraryManagement.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagement.Services
{
    public interface IStatisticsService
    {
        IEnumerable<LibraryAmountDistrictVM> GetLibraryAmountDistrict();

        IEnumerable<AuthorAmountGenreVM> GetAuthorAmountGenre();

        IEnumerable<TopAuthorLibraryVM> GetTopAuthorLibrary();
    }
}
