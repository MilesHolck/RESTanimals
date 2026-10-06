namespace RESTanimals.Models
{
    public class Animal
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public int Age { get; set; }

        public string? PrimaryColor { get; set; }

        public int ? SecondaryColor { get; set; } = 0;

        // Validering – kastes hvis data er ugyldigt (bruges i repo og testes i AnimalTests)
        public void ValidateName()
        {
            if (Name == null)
                throw new ArgumentNullException(nameof(Name), "Name må ikke være null");
            if (Name.Length < 2)
                throw new ArgumentException("Name skal være mindst 2 tegn langt");
        }

        public void ValidateAge()
        {
            if (Age < 0)
                throw new ArgumentOutOfRangeException(nameof(Age), "Age må ikke være negativ");
        }

        public void ValidatePrimaryColor()
        {
            if (string.IsNullOrWhiteSpace(PrimaryColor))
                throw new ArgumentException("PrimaryColor skal udfyldes");
        }

        public void Validate()
        {
            ValidateName();
            ValidateAge();
            ValidatePrimaryColor();
        }

        public override string ToString()
        {
            return $"Animal Id: {Id}, Name: {Name}, Age: {Age}, PrimaryColor: {PrimaryColor}";
        }
    }
}
