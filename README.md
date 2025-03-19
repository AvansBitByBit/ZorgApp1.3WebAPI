# ZorgApp1.3WebAPI

ZorgApp1.3WebAPI is a web API designed to support a game for patient kids who have broken a bone and need a cast. The API includes a character creator to ensure all kids feel represented in the game.

## Project Structure

### Controllers

- **CharacterController.cs**: Handles HTTP requests related to character creation and retrieval.
  - **CreateCharacter**: Creates a new character.
  - **GetCharacters**: Retrieves all characters associated with the authenticated user.

### Models

- **Character.cs**: Represents the character entity with properties such as `Name`, `HairColor`, `SkinColor`, `EyeColor`, `Gender`, and `UserId`.

### Services

- **CharacterService.cs**: Contains business logic for managing characters.
  - **CreateCharacterAsync**: Creates a new character and associates it with the authenticated user.
  - **GetCharactersAsync**: Retrieves all characters associated with the authenticated user.

### Interfaces

- **ICharacterService.cs**: Defines the contract for the character service.
  - **CreateCharacterAsync**: Method to create a new character.
  - **GetCharactersAsync**: Method to retrieve all characters associated with the authenticated user.

- **ICharacterRepository.cs**: Defines the contract for the character repository.
  - **CreateCharacterAsync**: Method to create a new character in the database.
  - **GetCharactersByUserIdAsync**: Method to retrieve all characters associated with a specific user ID.

- **IAuthenticationService.cs**: Defines the contract for the authentication service.
  - **GetCurrentAuthenticatedUserId**: Method to get the ID of the currently authenticated user.

### Repository

- **CharacterRepository.cs**: Handles database operations for characters using Dapper.
  - **CreateCharacterAsync**: Inserts a new character record into the database.
  - **GetCharactersByUserIdAsync**: Retrieves all characters associated with a specific user ID from the database.

### Program.cs

- Configures services and middleware for the application.
  - Registers `ICharacterRepository`, `ICharacterService`, and `IDbConnection` for dependency injection.
  - Configures Identity Framework for user authentication.
  - Sets up Swagger for API documentation.

## How It Works

1. **Character Creation**:
   - The `CharacterController` receives a POST request to create a new character.
   - The `CharacterService` sets the `UserId` of the character to the currently authenticated user's ID and calls the `CharacterRepository` to insert the character into the database.

2. **Character Retrieval**:
   - The `CharacterController` receives a GET request to retrieve all characters.
   - The `CharacterService` gets the ID of the currently authenticated user and calls the `CharacterRepository` to retrieve all characters associated with that user ID.

## Running the Application


3. **API Documentation**:
   - Access Swagger for API documentation at:
     ```
     https://localhost:xxxx/swagger
     ```

## Conclusion

This project demonstrates how to create a web API with character creation functionality, user authentication, and database operations using Dapper. The code is organized into controllers, models, services, interfaces, and repositories to ensure a clean and maintainable structure.
