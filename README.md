# Game Library & Recommendation System

A console application for managing a personal video game library with a built-in rating system, and a recommendation
system. Made for people who play video games and want to find new games to play.

## Team Members
JP Gilliam<br>
William Sittiphone<br>
Jacob Younan<br>

## Requirements
- .NET 10.0 SDK (https://dotnet.microsoft.com/download)

## How to Run
1. Open terminal
2. Navigate to the project directory using "cd FinalProject_ITCS_3112"
3. Run the program using "dotnet run"

## OOP Features
| OOP Feature      | File Name                    | Line Numbers | Purpose                                                                                          |
|------------------|------------------------------|--------------|--------------------------------------------------------------------------------------------------|
| Inheritance      | BasicUser.cs                 | All          | BasicUser inherits from User to share basic fields and methods.                                  |
| Inheritance      | SuperUser.cs                 | All          | SuperUser inherits from User to share basic fields and methods.                                  |
| Interface        | IAuthSystem                  | All          | Defines a system for users to login, logout, and verify their account.                           |
| Interface        | IGameRepository              | All          | Defines a repository for storing games.                                                          |
| Interface        | IRecommendationSystem        | All          | Defines a system for generating game recommendations.                                            |
| Polymorphism     | RecommendationByGenre.cs     | 7-45         | Overrides RecommendationSystem's filter method to filter for user's most played genre.           |
| Polymorphism     | RecommendationByPublisher.cs | 7-45         | Overrides RecommendationSystem's filter method to filter for most popular publisher.             |
| Access Modifiers | Menu.cs                      | 11-16        | Services are private to prevent them from being accidentally modified.                           |
| Struct           | UserCredentials.cs           | All          | Stores user credentials, allowing users to have unique IDs, usernames, and passwords.            |
| Enum             | AuthLevel.cs                 | All          | Stores values that define if a user is unverified, verified, or verified as admin.               |
| Data Structure   | GameRepository.cs            | 8-56         | Uses list data structure to store and manage games.                                              |
| I/O              | Menu.cs                      | All          | Console commands like WriteLine(), ReadLine(), Clear(), etc. are used throughout the Menu class. |

## Design Patterns
| Pattern Name | Category   | File Name               | Line Numbers | Rationale                                                                                                                                                                                                                               |
|--------------|------------|-------------------------|--------------|-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| Singleton    | Creational | GameRepository.cs       | 7-21         | Ensures the program only uses one game repository. The class has a private constructor of itself, private static field of its own datatype, and a static getInstance method of itself.                                                  |
| Template     | Behavioral | FileService.cs          | All          | Defines the skeleton for an algorithm of files, allowing for subclasses to override specific steps. GameFileService, RatingFileService, and UserFileService inherit and override the three protected abstract methods from FileService. |
| Strategy     | Behavioral | RecommendationSystem.cs | All          | Allows for interchangeable recommendation filters. The class' interface defines the action method that returns a list of games, and there are three filter types: default, genre, and publisher.                                        |

## Design Decisions
- The repositories (GameRepository, RatingRepository, and UserRepository) follow the singleton pattern because only one 
instance of each repository is required for the program to function.
- FileService follows the template pattern because it allows for the creation of subclasses (GameFile, RatingFile, 
UserFile) that override specific steps of the algorithm.
- RecommendationSystem follows the strategy pattern because it allows for interchangeable recommendation filters 
(RecommendationByDefault, RecommendationByGenre, RecommendationByPublisher).
- The factory method pattern was originally going to be used for adding different types of games or users, but the 
current implementation for both games and users was more convenient.
- Some interfaces, like IGameRepository, IFileService, and IRecommendationSystem, were used to implement design patterns.
- Other interfaces, like IAccountService, IAuthSystem, IBackLog, and ILibraryService, were used to define different 
functionalities within the program.