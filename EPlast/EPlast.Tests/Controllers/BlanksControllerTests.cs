﻿using EPlast.BLL;
using EPlast.BLL.DTO.Blank;
using EPlast.BLL.Interfaces.Blank;
using EPlast.WebApi.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EPlast.Tests.Controllers
{
    [TestFixture]
    class BlanksControllerTests
    {
        Mock<IBlankBiographyDocumentService> _mockBiographyService;
        Mock<IBlankAchievementDocumentService> _mockBlankAchievementDocumentService;
        Mock<IBlankExtractFromUPUDocumentService> _mockBlankExtractFromUPUDocumentService;
        Mock<IPdfService> _pdfService;
        BlanksController _blanksController;

        [SetUp]
        public void SetUp()
        {
            _mockBiographyService = new Mock<IBlankBiographyDocumentService>();
            _mockBlankAchievementDocumentService = new Mock<IBlankAchievementDocumentService>();
            _mockBlankExtractFromUPUDocumentService = new Mock<IBlankExtractFromUPUDocumentService>();
            _pdfService = new Mock<IPdfService>();
            _blanksController = new BlanksController(_mockBiographyService.Object,
                _mockBlankAchievementDocumentService.Object,
                _mockBlankExtractFromUPUDocumentService.Object,
                _pdfService.Object);
        }

        [Test]
        public async Task AddBiographyDocument_ReturnsCreatedObjectResult()
        {
            //Arrange
            _mockBiographyService
                .Setup(x => x.AddDocumentAsync(It.IsAny<BlankBiographyDocumentsDTO>()))
                .ReturnsAsync(GetBlankBiographyDocumentsDTO());

            //Act
            var document = await _blanksController.AddBiographyDocument(GetBlankBiographyDocumentsDTO());
            CreatedResult createdResult = document as CreatedResult;

            //Assert
            _mockBiographyService.Verify();
            Assert.NotNull(document);
            Assert.IsInstanceOf<ObjectResult>(document);
            Assert.AreEqual(StatusCodes.Status201Created, createdResult.StatusCode);
        }

        [Test]
        public async Task AddAchievementDocument_ReturnsCreatedObjectResult()
        {
            //Arrange
            _mockBlankAchievementDocumentService
                .Setup(x => x.AddDocumentAsync(It.IsAny<List<AchievementDocumentsDTO>>()))
                .ReturnsAsync(new List<AchievementDocumentsDTO>());
            //Act
            var document = await _blanksController.AddAchievementDocument(new List<AchievementDocumentsDTO>());
            CreatedResult createdResult = document as CreatedResult;

            //Assert
            _mockBlankAchievementDocumentService.Verify();
            Assert.NotNull(document);
            Assert.IsInstanceOf<ObjectResult>(document);
            Assert.AreEqual(StatusCodes.Status201Created, createdResult.StatusCode);
        }

        [Test]
        public async Task AddExtractFromUPUDocument_ReturnsCreatedObjectResult()
        {
            //Arrange
            _mockBlankExtractFromUPUDocumentService
                .Setup(x => x.AddDocumentAsync(It.IsAny<ExtractFromUPUDocumentsDTO>()))
                .ReturnsAsync(new ExtractFromUPUDocumentsDTO());

            //Act
            var document = await _blanksController.AddExtractFromUPUDocument(new ExtractFromUPUDocumentsDTO());
            CreatedResult createdResult = document as CreatedResult;

            //Assert
            _mockBlankExtractFromUPUDocumentService.Verify();
            Assert.NotNull(document);
            Assert.IsInstanceOf<ObjectResult>(document);
            Assert.AreEqual(StatusCodes.Status201Created, createdResult.StatusCode);
        }

        [Test]
        public async Task GetDocumentByUserId_ReturnsOkObjectResult()
        {
            //Arrange
            _mockBiographyService
                .Setup(x => x.GetDocumentByUserId("gh34tg"))
                .ReturnsAsync(GetBiographyDocumentsDTO());

            //Act
            var document = await _blanksController.GetDocumentByUserId("gh34tg");
            OkObjectResult result = document as OkObjectResult;

            //Assert
            _mockBiographyService.Verify();
            Assert.NotNull(document);
            Assert.IsInstanceOf<ObjectResult>(document);
            Assert.AreEqual(StatusCodes.Status200OK, result.StatusCode);
        }

        [Test]
        public async Task GetExtractFromUPUByUserId_ReturnsOkObjectResult()
        {
            //Arrange
            _mockBlankExtractFromUPUDocumentService
                .Setup(x => x.GetDocumentByUserId("gh34tg"))
                .ReturnsAsync(GetExtractFromUPUDocumentsDTO());

            //Act
            var document = await _blanksController.GetExtractFromUPUByUserId("gh34tg");
            OkObjectResult result = document as OkObjectResult;

            //Assert
            _mockBlankExtractFromUPUDocumentService.Verify();
            Assert.NotNull(document);
            Assert.IsInstanceOf<ObjectResult>(document);
            Assert.AreEqual(StatusCodes.Status200OK, result.StatusCode);
        }

        [Test]
        public async Task RemoveExtractFromUPUDocument_ReturnsNoContent()
        {
            //Arrange
            int documentId = 1;
            _mockBlankExtractFromUPUDocumentService
                .Setup(x => x.DeleteFileAsync(documentId));

            //Act
            var document = await _blanksController.RemoveExtractFromUPUDocument(documentId);
            var statusCodeDocument = document as StatusCodeResult;

            //Assert
            _mockBlankExtractFromUPUDocumentService.Verify();
            Assert.NotNull(document);
            Assert.AreEqual(StatusCodes.Status204NoContent, statusCodeDocument.StatusCode);
        }

        [Test]
        public async Task RemoveDocument_ReturnsNoContent()
        {
            //Arrange
            int documentId = 1;
            _mockBiographyService
                .Setup(x => x.DeleteFileAsync(documentId));

            //Act
            var document = await _blanksController.RemoveDocument(documentId);
            var statusCodeDocument = document as StatusCodeResult;

            //Assert
            _mockBiographyService.Verify();
            Assert.NotNull(document);
            Assert.AreEqual(StatusCodes.Status204NoContent, statusCodeDocument.StatusCode);
        }

        [Test]
        public async Task GetFileBase64_ReturnsOkObjectResult()
        {
            // Arrange
            _mockBiographyService
               .Setup(x => x.DownloadFileAsync(It.IsAny<string>()))
               .ReturnsAsync(GetExtractFromUPUDocumentsDTO().FileName);

            //Act
            var document = await _blanksController.GetFileBase64(GetExtractFromUPUDocumentsDTO().FileName);
            OkObjectResult result = document as OkObjectResult;

            //Assert
            _mockBiographyService.Verify();
            Assert.NotNull(document);
            Assert.IsInstanceOf<ObjectResult>(document);
            Assert.AreEqual(GetExtractFromUPUDocumentsDTO().FileName, result.Value);
        }

        [Test]
        public async Task GetFileExtractFromUPUBase64_ReturnsOkObjectResult()
        {
            // Arrange
            _mockBlankExtractFromUPUDocumentService
               .Setup(x => x.DownloadFileAsync(new string("Dogovir")))
               .ReturnsAsync(new string("Dogovir"));

            //Act
            var document = await _blanksController.GetFileExtractFromUPUBase64("Dogovir");
            OkObjectResult result = document as OkObjectResult;

            //Assert
            _mockBlankExtractFromUPUDocumentService.Verify();
            Assert.NotNull(document);
            Assert.IsInstanceOf<ObjectResult>(document);
            Assert.AreEqual("Dogovir", result.Value);
        }

        [TestCase(1, 1, "userId")]
        public async Task GetPartOfAchievement(int pageNumber, int pageSize, string userId)
        {
            //Arrange
            _mockBlankAchievementDocumentService
               .Setup(x => x.GetPartOfAchievementAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>()))
               .ReturnsAsync(new List<AchievementDocumentsDTO>());

            //Act
            var result = await _blanksController.GetPartOfAchievement(pageNumber, pageSize, userId);

            //Assert
            _mockBlankAchievementDocumentService.Verify();
            Assert.NotNull(result);
            Assert.IsInstanceOf<ObjectResult>(result);
        }

        [TestCase("userId")]
        public async Task GetAchievementDocumentsByUserId_ReturnsOkObjectResult(string userId)
        {
            //Arrange
            _mockBlankAchievementDocumentService.Setup(x => x.GetDocumentsByUserIdAsync(It.IsAny<string>()))
                .ReturnsAsync(new List<AchievementDocumentsDTO>());

            //Act
            var result = await _blanksController.GetAchievementDocumentsByUserId(userId);

            //Assert
            _mockBlankAchievementDocumentService.Verify();
            Assert.NotNull(result);
            Assert.IsInstanceOf<ObjectResult>(result);
        }

        [Test]
        public async Task RemoveAchievementDocument_ReturnsNoContent()
        {
            //Arrange
            int documentId = 1;
            _mockBlankAchievementDocumentService
                .Setup(x => x.DeleteFileAsync(documentId));

            //Act
            var document = await _blanksController.RemoveAchievementDocument(documentId);
            var statusCodeDocument = document as StatusCodeResult;

            //Assert
            _mockBlankAchievementDocumentService.Verify();
            Assert.NotNull(document);
            Assert.AreEqual(StatusCodes.Status204NoContent, statusCodeDocument.StatusCode);
        }

        [Test]
        public async Task GetFileAchievementBase64_ReturnsOkObjectResult()
        {
            // Arrange
            _mockBlankAchievementDocumentService
               .Setup(x => x.DownloadFileAsync(It.IsAny<string>()))
               .ReturnsAsync(GetExtractFromUPUDocumentsDTO().FileName);

            //Act
            var document = await _blanksController.GetFileAchievementBase64(GetExtractFromUPUDocumentsDTO().FileName);
            OkObjectResult result = document as OkObjectResult;

            //Assert
            _mockBlankAchievementDocumentService.Verify();
            Assert.NotNull(document);
            Assert.IsInstanceOf<ObjectResult>(document);
            Assert.AreEqual(GetExtractFromUPUDocumentsDTO().FileName, result.Value);
        }

        private BlankBiographyDocumentsDTO GetBlankBiographyDocumentsDTO()
        {
            BlankBiographyDocumentsDTO BlankBiographyDTO = new BlankBiographyDocumentsDTO()
            {
                ID = 1,
                FileName = "Dogovir",
                BlobName = "BlobName",
                UserId = "gh34tg"
            };
            return BlankBiographyDTO;
        }

        public BlankBiographyDocumentsDTO GetBiographyDocumentsDTO()
        {
            return new BlankBiographyDocumentsDTO
            {
                ID = 1,
                FileName = "Dogovir",
                BlobName = "BlobName",
                UserId = "gh34tg"

            };
        }

        public ExtractFromUPUDocumentsDTO GetExtractFromUPUDocumentsDTO()
        {
            return new ExtractFromUPUDocumentsDTO
            {
                ID = 1,
                FileName = "Dogovir",
                BlobName = "BlobName",
                UserId = "gh34tg"

            };
        }

        [Test]
        public async Task GetPdfService_ReturnsObjRes()
        {
            //Arrange
            _pdfService
                .Setup(p => p.BlankCreatePDFAsync(It.IsAny<string>()));

            //Act
            var result = await _blanksController.GetGenerationFile(It.IsAny<string>());

            //Assert
            _pdfService.Verify();
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<OkObjectResult>(result);
        }
    }
}
