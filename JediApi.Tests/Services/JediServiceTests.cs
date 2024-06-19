using JediApi.Models;
using JediApi.Repositories;
using JediApi.Services;
using Moq;

namespace JediApi.Tests.Services
{
    public class JediServiceTests
    {
        // não mexer
        private readonly JediService _service;
        private readonly Mock<IJediRepository> _repositoryMock;

        public JediServiceTests()
        {
            // não mexer
            _repositoryMock = new Mock<IJediRepository>();
            _service = new JediService(_repositoryMock.Object);
        }

        [Fact]
        public async Task GetById_Success()
        {
            var idJedi = 1;
            var JediEsperado = new Jedi { Id = idJedi, Name = "Luke Skywalker" };
            _repositoryMock.Setup(repo => repo.GetByIdAsync(idJedi)).ReturnsAsync(JediEsperado);

            var result = await _service.GetByIdAsync(idJedi);

            Assert.Equal(JediEsperado.Id, result.Id);
            Assert.Equal(JediEsperado.Name, result.Name);
        }

        [Fact]
        public async Task GetById_NotFound()
        {
            var idJedi = 1;
            _repositoryMock.Setup(repo => repo.GetByIdAsync(idJedi));

            var result = await _service.GetByIdAsync(idJedi);

            Assert.Null(result);
        }

        [Fact]
        public async Task GetAll()
        {
            var listaJedisEsperados = new List<Jedi>
            {
                new Jedi { Id = 1, Name = "Luke Skywalker" },
                new Jedi { Id = 2, Name = "Obi-Wan Kenobi" }
            };

            _repositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(listaJedisEsperados);

            var result = await _service.GetAllAsync();

            Assert.Equal(listaJedisEsperados[0].Id, result[0].Id);
            Assert.Equal(listaJedisEsperados[1].Id, result[1].Id);
        }
    }
}
