﻿using AutoMapper;
using EPlast.BussinessLayer.DTO.Club;
using EPlast.BussinessLayer.DTO.UserProfiles;
using EPlast.BussinessLayer.Interfaces.Club;
using EPlast.BussinessLayer.Services.Club;
using EPlast.DataAccess.Entities;
using EPlast.DataAccess.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Query;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Xunit;

namespace EPlast.XUnitTest.Services.ClubTests
{
    public class ClubServiceTests
    {
        private readonly Mock<IRepositoryWrapper> _repoWrapper;
        private readonly Mock<IMapper> _mapper;
        private readonly IClubService _clubService;

        public ClubServiceTests()
        {
            _repoWrapper = new Mock<IRepositoryWrapper>();
            _mapper = new Mock<IMapper>();
            var env = new Mock<IHostingEnvironment>();
            _clubService = new ClubService(_repoWrapper.Object, _mapper.Object, env.Object);
        }

        [Fact]
        public async Task GetAllClubsAsync_ReturnsListOfClubDto()
        {
            //arrange
            _repoWrapper.Setup(r => r.Club.GetAllAsync(null,
                    It.IsAny<Func<IQueryable<Club>, IIncludableQueryable<Club, object>>>()))
                .ReturnsAsync(new List<Club> { new Club() });

            //act
            await _clubService.GetAllClubsAsync();

            //assert
            _mapper.Verify(m => m.Map<IEnumerable<Club>, IEnumerable<ClubDTO>>(It.IsAny<IEnumerable<Club>>()));
        }


        [Fact]
        public async Task GetClubProfileAsync_ById_ReturnsClubProfileDto()
        {
            //arrange
            _repoWrapper.Setup(r => r.Club.GetFirstOrDefaultAsync(It.IsAny<Expression<Func<Club, bool>>>(),
                    It.IsAny<Func<IQueryable<Club>, IIncludableQueryable<Club, object>>>()))
                .ReturnsAsync(It.IsAny<Club>());
            _mapper.Setup(m => m.Map<Club, ClubDTO>(It.IsAny<Club>()))
                .Returns(() => new ClubDTO()
                {
                    ID = It.IsAny<int>(),
                    ClubMembers = new List<ClubMembersDTO>()
                    {
                        new ClubMembersDTO
                        {
                            User = new UserDTO(),
                            IsApproved = It.IsAny<bool>(),
                        }
                    },
                    ClubAdministration = new List<ClubAdministrationDTO>()
                    {
                        new ClubAdministrationDTO
                        {
                            AdminType = new AdminType(),
                            StartDate = It.IsAny<DateTime>(),
                            ClubMembers = new ClubMembersDTO
                            {
                                User = new UserDTO(),
                                IsApproved = It.IsAny<bool>(),
                            }
                        }
                    }
                });

            //act
            var result = await _clubService.GetClubProfileAsync(It.IsAny<int>());

            //assert
            Assert.NotNull(result);
            Assert.IsType<ClubProfileDTO>(result);
        }


        [Fact]
        public async Task GetClubInfoByIdAsync_ReturnsClubDto()
        {
            //arrange
            _repoWrapper.Setup(r => r.Club.GetFirstOrDefaultAsync(It.IsAny<Expression<Func<Club, bool>>>(),
                    It.IsAny<Func<IQueryable<Club>, IIncludableQueryable<Club, object>>>()))
                .ReturnsAsync(new Club());

            //act
            await _clubService.GetClubInfoByIdAsync(It.IsAny<int>());

            //assert
            _mapper.Verify(m => m.Map<Club, ClubDTO>(It.IsAny<Club>()));
        }

        [Fact]
        public async Task GetClubInfoByIdAsync_NotFound_ReturnsNull()
        {
            //arrange
            _repoWrapper.Setup(r => r.Club.GetFirstOrDefaultAsync(It.IsAny<Expression<Func<Club, bool>>>(),
                    It.IsAny<Func<IQueryable<Club>, IIncludableQueryable<Club, object>>>()))
                .ReturnsAsync((Club)null);

            //act
            await _clubService.GetClubInfoByIdAsync(It.IsAny<int>());

            //assert
            _mapper.Verify(m => m.Map<Club, ClubDTO>(new Club()), Times.Never);
            _mapper.Verify(m => m.Map<Club, ClubDTO>(null));
        }

        [Fact]
        public async Task UpdateAsync_ReturnsClubDto()
        {
            //arrange
            var mockFile = new Mock<IFormFile>();
            _repoWrapper.Setup(r => r.Club.GetFirstOrDefaultAsync(It.IsAny<Expression<Func<Club, bool>>>(),
                    It.IsAny<Func<IQueryable<Club>, IIncludableQueryable<Club, object>>>()))
                .ReturnsAsync(new Club());
            _mapper.Setup(m => m.Map<Club, ClubDTO>(It.IsAny<Club>()))
                .Returns(() => new ClubDTO());

            //act
            await _clubService.UpdateAsync(new ClubDTO(), mockFile.Object);

            //assert
            _repoWrapper.Verify(r => r.Club.Update(It.IsAny<Club>()));
            _repoWrapper.Verify(r => r.SaveAsync());
        }

        [Fact]
        public async Task CreateAsync_ReturnsClubDto()
        {
            //arrange
            var mockFile = new Mock<IFormFile>();
            _repoWrapper.Setup(r => r.Club.GetFirstOrDefaultAsync(It.IsAny<Expression<Func<Club, bool>>>(),
                    It.IsAny<Func<IQueryable<Club>, IIncludableQueryable<Club, object>>>()))
                .ReturnsAsync(new Club());

            //act
            await _clubService.CreateAsync(new ClubDTO(), mockFile.Object);

            //assert
            _repoWrapper.Verify(r => r.Club.CreateAsync(It.IsAny<Club>()));
            _repoWrapper.Verify(r => r.SaveAsync());
        }
    }
}