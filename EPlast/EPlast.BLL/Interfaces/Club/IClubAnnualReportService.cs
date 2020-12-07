﻿using EPlast.BLL.DTO.Club;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace EPlast.BLL.Interfaces.Club
{
    public interface IClubAnnualReportService
    {
        /// <summary>
        /// Method to get all the information in the club annual report
        /// </summary>
        /// <param name="claimsPrincipal">Authorized user</param>
        /// <param name="id">Annual report identification number</param>
        /// <returns>Annual report model</returns>
        /// <exception cref="System.UnauthorizedAccessException">Thrown when user hasn't access to annual report</exception>
        /// <exception cref="System.NullReferenceException">Thrown when annual report doesn't exist</exception>
        Task<ClubAnnualReportDTO> GetByIdAsync(ClaimsPrincipal claimsPrincipal, int id);

        /// <summary>
        /// Method to get all club reports that the user has access to
        /// </summary>
        /// <param name="claimsPrincipal">Authorized user</param>
        /// <returns>List of annual report model</returns>
        Task<IEnumerable<ClubAnnualReportDTO>> GetAllAsync(ClaimsPrincipal claimsPrincipal);

        /// <summary>
        /// Method to create new club annual report
        /// </summary>
        /// <param name="claimsPrincipal">Authorized user</param>
        /// <param name="clubAnnualReportDTO">Annual report model</param>
        /// <exception cref="System.InvalidOperationException">Thrown when city has created annual report</exception>
        /// <exception cref="System.UnauthorizedAccessException">Thrown when user hasn't access to city</exception>
        /// <exception cref="System.NullReferenceException">Thrown when city doesn't exist</exception>
        Task CreateAsync(ClaimsPrincipal claimsPrincipal, ClubAnnualReportDTO clubAnnualReportDTO);

        /// <summary>
        /// Method to confirm club annual report
        /// </summary>
        /// <param name="claimsPrincipal">Authorized user</param>
        /// <param name="id">Annual report identification number</param>
        /// <exception cref="System.UnauthorizedAccessException">Thrown when user hasn't access to annual report</exception>
        /// <exception cref="System.NullReferenceException">Thrown when annual report doesn't exist</exception>
        Task ConfirmAsync(ClaimsPrincipal claimsPrincipal, int id);
    }
}
