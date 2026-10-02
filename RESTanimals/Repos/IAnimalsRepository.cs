using RESTanimals.Models;

namespace RESTanimals.Repos
{
    public interface IAnimalsRepository
    {
        IEnumerable<Animal> GetAnimals(string? nameStartsWith = null, int? minAge = null, string? sortOrder = null);
        Animal? GetAnimalById(int id);
        Animal AddAnimal(Animal animal);
        Animal? UpdateAnimal(int id, Animal updatedAnimal);
        Animal? DeleteAnimal(int id);
    }
}
