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
| OOP Feature      | File Name                    | Line Numbers | Purpose                                                         |
|------------------|------------------------------|--------------|-----------------------------------------------------------------|
| Inheritance      | BasicUser.cs                 | 3-23         | BasicUser inherits from User to share basic fields and methods. |
| Inheritance      | SuperUser.cs                 | 3-15         | SuperUser inherits from User to share basic fields and methods. |
| Interface        | IAuthSystem                  | 1-9          | ...                                                             |
| Interface        | IGameRepository              | 1-11         | ...                                                             |
| Interface        | IRecommendationSystem        | 1-7          | ...                                                             |
| Polymorphism     | RecommendationByGenre.cs     | 1-46         | ...                                                             |
| Polymorphism     | RecommendationByPublisher.cs | 1-46         | ...                                                             |
| Access Modifiers | Menu.cs                      | 11-16        | ...                                                             |
| Struct           | UserCredentials.cs           | 1-15         | ...                                                             |
| Enum             | AuthLevel.cs                 | 1-8          | ...                                                             |
| Data Structure   | GameRepository.cs            | 8-56         | ...                                                             |
| I/O              | Menu.cs                      | All          | ...                                                             |

## Design Patterns
- Singleton
- Factory
- Strategy

## Design Decisions
- 