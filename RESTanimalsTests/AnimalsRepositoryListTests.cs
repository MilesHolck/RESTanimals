using RESTanimals.Models;
using RESTanimals.Repos;

namespace RESTanimalsTests
{
    public class AnimalsRepositoryListTests
    {
        private IAnimalsRepository _repo;

        // Kører før HVER test -> hver test får en frisk, tom liste
        public AnimalsRepositoryListTests()
        {
            _repo = new AnimalsRepositoryList(includesTestData: false);
            _repo.AddAnimal(new Animal { Name = "Fido", Age = 3, PrimaryColor = "Brun" });
            _repo.AddAnimal(new Animal { Name = "Bisser", Age = 5, PrimaryColor = "Sort" });
            _repo.AddAnimal(new Animal { Name = "Bella", Age = 1, PrimaryColor = "Hvid" });
        }

        // ---------- Constructor ----------

        [Fact]
        public void Constructor_WithTestData_Has3Animals()
        {
            IAnimalsRepository repo = new AnimalsRepositoryList(includesTestData: true);
            Assert.Equal(3, repo.GetAnimals().Count());
        }

        [Fact]
        public void Constructor_Default_IsEmpty()
        {
            IAnimalsRepository repo = new AnimalsRepositoryList();
            Assert.Empty(repo.GetAnimals());
        }

        // ---------- GET ----------

        [Fact]
        public void GetAnimals_NoFilters_ReturnsAll()
        {
            var animals = _repo.GetAnimals();
            Assert.Equal(3, animals.Count());
        }

        [Fact]
        public void GetAnimals_NameStartsWith_FiltersCorrectly()
        {
            var animals = _repo.GetAnimals(nameStartsWith: "f");
            Assert.Single(animals);
            Assert.Equal("Fido", animals.First().Name);
        }

        [Fact]
        public void GetAnimals_MinAge_FiltersCorrectly()
        {
            var animals = _repo.GetAnimals(minAge: 3);
            Assert.Equal(2, animals.Count());
        }

        [Fact]
        public void GetAnimals_SortByAgeDesc_OldestFirst()
        {
            var animals = _repo.GetAnimals(sortOrder: "age_desc").ToList();
            Assert.Equal("Misser", animals[0].Name);
            Assert.Equal("Bella", animals[2].Name);
        }

        [Fact]
        public void GetAnimals_SortByNameAsc_Alphabetical()
        {
            var animals = _repo.GetAnimals(sortOrder: "name_asc").ToList();
            Assert.Equal("Bella", animals[0].Name);
        }

        [Fact]
        public void GetAnimals_InvalidSortOrder_Throws()
        {
            Assert.Throws<ArgumentException>(() => _repo.GetAnimals(sortOrder: "invalid").ToList());
        }

        [Fact]
        public void GetAnimalById_Existing_ReturnsAnimal()
        {
            Animal? animal = _repo.GetAnimalById(2);
            Assert.NotNull(animal);
            Assert.Equal("Misser", animal.Name);
        }

        [Fact]
        public void GetAnimalById_NonExisting_ReturnsNull()
        {
            Assert.Null(_repo.GetAnimalById(999));
        }

        // ---------- ADD ----------

        [Fact]
        public void AddAnimal_GivesNextId()
        {
            //Arrange
            Animal a = new Animal { Name = "Rex", Age = 4, PrimaryColor = "Grå" };
            //Act
            Animal added = _repo.AddAnimal(a);
            //Assert
            Assert.Equal(4, added.Id);
            Assert.Equal(4, _repo.GetAnimals().Count());
        }

        [Fact]
        public void AddAnimal_Invalid_Throws_AndIsNotAdded()
        {
            Animal a = new Animal { Name = "R", Age = 4, PrimaryColor = "Grå" };
            Assert.Throws<ArgumentException>(() => _repo.AddAnimal(a));
            Assert.Equal(3, _repo.GetAnimals().Count());
        }

        // ---------- UPDATE ----------

        [Fact]
        public void UpdateAnimal_Existing_UpdatesValues()
        {
            Animal newValues = new Animal { Name = "Fido", Age = 10, PrimaryColor = "Gul" };

            Animal? updated = _repo.UpdateAnimal(1, newValues);

            Assert.NotNull(updated);
            Assert.Equal(1, updated.Id);
            Assert.Equal(10, updated.Age);
            Assert.Equal("Gul", updated.PrimaryColor);
        }

        [Fact]
        public void UpdateAnimal_NonExisting_ReturnsNull()
        {
            Animal newValues = new Animal { Name = "Fido", Age = 10, PrimaryColor = "Gul" };
            Assert.Null(_repo.UpdateAnimal(999, newValues));
        }

        [Fact]
        public void UpdateAnimal_Invalid_Throws()
        {
            Animal newValues = new Animal { Name = "Fido", Age = -5, PrimaryColor = "Gul" };
            Assert.Throws<ArgumentOutOfRangeException>(() => _repo.UpdateAnimal(1, newValues));
        }

        // ---------- DELETE ----------

        [Fact]
        public void DeleteAnimal_Existing_RemovesIt()
        {
            Animal? deleted = _repo.DeleteAnimal(1);

            Assert.NotNull(deleted);
            Assert.Equal(1, deleted.Id);
            Assert.Null(_repo.GetAnimalById(1));
            Assert.Equal(2, _repo.GetAnimals().Count());
        }

        [Fact]
        public void DeleteAnimal_NonExisting_ReturnsNull()
        {
            Assert.Null(_repo.DeleteAnimal(999));
            Assert.Equal(3, _repo.GetAnimals().Count());
        }
    }
}
