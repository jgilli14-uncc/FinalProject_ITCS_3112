# Game Library & Recommendation System

A console application for managing a personal video game library 
with a built-in recommendation system.

## Team Members
JP Gilliam
William Sittiphone
Jacob Younan

## Requirements
- .NET 10.0 SDK (https://dotnet.microsoft.com/download)

## How to Run
1. Clone the repository
2. cd FinalProject_ITCS_3112
3. dotnet run

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
