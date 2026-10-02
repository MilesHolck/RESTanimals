# RESTanimals

Et simpelt REST API om dyr i ASP.NET Core (.NET 10). **Ingen database** – data ligger i en `List<Animal>` i hukommelsen, så der er ingen connectionstring eller hemmeligheder, og projektet kan trygt ligge på GitHub.

## Struktur

```
RESTanimals.slnx
├── RESTanimals/                    (Web API)
│   ├── Models/Animal.cs            Id, Name, Age, PrimaryColor + validering
│   ├── Repos/IAnimalsRepository.cs Interface
│   ├── Repos/AnimalsRepositoryList.cs  List-implementering (CRUD + filter/sortering)
│   ├── Controllers/AnimalsController.cs
│   ├── Program.cs                  DI (Singleton), CORS, Swagger
│   └── RESTanimals.http            Klar-til-brug requests
└── RESTanimalsTests/               (xUnit)
    ├── AnimalTests.cs              Tester validering
    └── AnimalsRepositoryListTests.cs  Tester repo'et
```

## Endpoints

| Metode | URL | Svar |
|---|---|---|
| GET | `/api/Animals?nameStartsWith=f&minAge=2&sortOrder=age_desc` | 200 / 204 / 400 |
| GET | `/api/Animals/{id}` | 200 / 404 |
| POST | `/api/Animals` | 201 / 400 |
| PUT | `/api/Animals/{id}` | 200 / 400 / 404 |
| DELETE | `/api/Animals/{id}` | 200 / 404 |

`sortOrder`: `name_asc`, `name_desc`, `age_asc`, `age_desc`

Eksempel-body til POST/PUT:
```json
{ "name": "Rex", "age": 4, "primaryColor": "Grå" }
```

## Git workflow (forslag)

```bash
git init
git add .
git commit -m "Initial commit: RESTanimals med List-repo"
git branch -M main
git remote add origin https://github.com/<brugernavn>/RESTanimals.git
git push -u origin main
```

Ny feature = ny branch:
```bash
git checkout -b feature/sortering
# ...kod, test...
git add .
git commit -m "Tilføj sortering på alder"
git push -u origin feature/sortering
# Lav en Pull Request på GitHub -> merge til main
git checkout main
git pull
```

`.gitignore` sørger for at `bin/`, `obj/` og `.vs/` ikke kommer med.
