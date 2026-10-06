using RESTanimals.Models;

namespace RESTanimals.Repos
{
    // "Database" = en List i hukommelsen. Ingen connectionstring nødvendig.
    // Data forsvinder når programmet genstartes.
    public class AnimalsRepositoryList : IAnimalsRepository
    {
        private readonly List<Animal> _animals = new List<Animal>();
        private int _nextId = 1;

        public AnimalsRepositoryList(bool includesTestData = false)
        {
            if (includesTestData)
            {
                AddAnimal(new Animal { Name = "Fido", Age = 3, PrimaryColor = "Brun" });
                AddAnimal(new Animal { Name = "Beta", Age = 5, PrimaryColor = "Sort" });
                AddAnimal(new Animal { Name = "Bella", Age = 1, PrimaryColor = "Hvid" });
            }
        }

        public IEnumerable<Animal> GetAnimals(string? nameStartsWith = null, int? minAge = null, string? sortOrder = null)
        {
            IEnumerable<Animal> result = _animals.ToList(); // kopi, så listen ikke ændres udefra

            if (nameStartsWith != null)
            {
                result = result.Where(a => a.Name != null && a.Name.StartsWith(nameStartsWith, StringComparison.OrdinalIgnoreCase));
            }

            if (minAge != null)
            {
                result = result.Where(a => a.Age >= minAge);
            }

            switch (sortOrder?.ToLower())
            {
                case null:
                case "":
                    break;
                case "name":
                case "name_asc":
                    result = result.OrderBy(a => a.Name);
                    break;
                case "name_desc":
                    result = result.OrderByDescending(a => a.Name);
                    break;
                case "age":
                case "age_asc":
                    result = result.OrderBy(a => a.Age);
                    break;
                case "age_desc":
                    result = result.OrderByDescending(a => a.Age);
                    break;
                default:
                    throw new ArgumentException($"Invalid sortOrder value: {sortOrder}. Valid values are: name_asc, name_desc, age_asc, age_desc.");
            }

            return result;
        }

        public Animal? GetAnimalById(int id)
        {
            return _animals.FirstOrDefault(a => a.Id == id);
        }

        public Animal AddAnimal(Animal animal)
        {
            animal.Validate();
            animal.Id = _nextId++;
            _animals.Add(animal);
            return animal;
        }

        public Animal? UpdateAnimal(int id, Animal updatedAnimal)
        {
            updatedAnimal.Validate();
            Animal? animal = GetAnimalById(id);
            if (animal == null)
            {
                return null;
            }
            animal.Name = updatedAnimal.Name;
            animal.Age = updatedAnimal.Age;
            animal.PrimaryColor = updatedAnimal.PrimaryColor;
            return animal;
        }

        public Animal? DeleteAnimal(int id)
        {
            Animal? animal = GetAnimalById(id);
            if (animal != null)
            {
                _animals.Remove(animal);
            }
            return animal;
        }
    }
}
